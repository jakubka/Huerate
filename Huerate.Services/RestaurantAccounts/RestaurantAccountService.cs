/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Huerate.Domain;
using Huerate.Domain.Entities;
using Huerate.Domain.Repositories;
using Huerate.Logging;
using Huerate.Services.DefaultData;
using Huerate.Services.Email;
using Huerate.Services.Names;
using Huerate.Services.Questions;
using Huerate.Services.Settings;
using Huerate.Services.UrlService;
using Huerate.WebViewModels;
using Huerate.WebViewModels.Form;

namespace Huerate.Services.RestaurantAccounts
{
    internal class RestaurantAccountService : DataServiceBase<IRestaurantAccountRepository, RestaurantAccount, int>, IRestaurantAccountService
    {
        private IDefaultDataService DefaultDataService { get; set; }
        private INameService NameService { get; set; }
        private ISettingsService SettingsService { get; set; }
        private IEmailSenderService EmailSenderService { get; set; }
        private IUrlService UrlService { get; set; }
        private IQuestionService QuestionService { get; set; }

        public RestaurantAccountService(IDefaultDataService defaultDataService, INameService nameService, ISettingsService settingsService, IEmailSenderService emailSenderService, IUrlService urlService, IQuestionService questionService, IUnitOfWork unitOfWork, ILogService logService) : base(unitOfWork, logService)
        {
            DefaultDataService = defaultDataService;
            NameService = nameService;
            SettingsService = settingsService;
            EmailSenderService = emailSenderService;
            UrlService = urlService;
            QuestionService = questionService;
        }

        public RestaurantAccount CreateRestaurantAccount(string restaurantName, string email, string password, string currentLanguage)
        {
            string codeName = GetUniqueCodeName(restaurantName);

            RestaurantAccount restaurantAccount = new RestaurantAccount
                                                      {
                                                          DisplayName = restaurantName,
                                                          CodeName = codeName,
                                                          Email = email,
                                                          IsLockedOut = false,
                                                          CreationDate = DateTime.UtcNow,
                                                          AccountGuid = Guid.NewGuid(),
                                                          LastLoginDate = DateTime.UtcNow,
                                                          DefaultLanguage = currentLanguage,
                                                          Languages = "cs;en",
                                                      };

            SetPassword(restaurantAccount, password);

            Create(restaurantAccount);

            DefaultDataService.FillNewRestauratAccount(restaurantAccount);

            var templateParameters = new Dictionary<string, object>()
                                         {
                                             {"RestaurantName", restaurantAccount.DisplayName},
                                             {"SignInPage", UrlService.Action("SignIn", "HomePage", protocol: "http")},
                                             {"LostPasswordPage", UrlService.Action("LostPassword", "HomePage", protocol: "http")},
                                             {"Email", restaurantAccount.Email},
                                         };

            EmailSenderService.SendEmailFromTemplateAsync("SignUp", currentLanguage, new[] {restaurantAccount.Email}, templateParameters);

            LogService.DebugExecution("RestaurantAccount created. Email: " + restaurantAccount.Email);

            return restaurantAccount;
        }

        public void ChangePasswordToGenerated(string restaurantAccountEmail)
        {
            var restaurantAccount = Repository.GetRestaurantAccountByCodeNameOrEmail(restaurantAccountEmail);

            if (restaurantAccount == null)
            {
                throw new ServiceException(string.Format("Restaurace s emailem {0} v systému neexistuje", restaurantAccountEmail));
            }

            string newPassword = GenerateNewPassword();

            SetPassword(restaurantAccount, newPassword);

            UnitOfWork.Save();

            var templateParameters = new Dictionary<string, object>()
                                         {
                                             {"RestaurantName", restaurantAccount.DisplayName},
                                             {"SignInPage", UrlService.Action("SignIn", "HomePage", null, "http")},
                                             {"Email", restaurantAccount.Email},
                                             {"NewPassword", newPassword},
                                         };

            EmailSenderService.SendEmailFromTemplateAsync("LostPassword", "cs", new[] {restaurantAccount.Email}, templateParameters);

            LogService.DebugExecution("Lost password recovered, email: " + restaurantAccountEmail);
        }

        public void ChangePassword(RestaurantAccount account, string newPassword)
        {
            SetPassword(account, newPassword);

            UnitOfWork.Save();

            LogService.DebugExecution("Password changed without verification, id " + account.Id);
        }

        public void ChangePassword(RestaurantAccount restaurant, string oldPassword, string newPassword)
        {
            if (!ValidateRestaurantAccount(restaurant.CodeName, oldPassword))
            {
                throw new ServiceException("wrong current password");
            }

            SetPassword(restaurant, newPassword);

            UnitOfWork.Save();

            LogService.DebugExecution("Password changed, id " + restaurant.Id);
        }

        public void LockRestaurantAccount(RestaurantAccount user)
        {
            user.IsLockedOut = true;
            user.Email += Guid.NewGuid().ToString();
            user.CodeName += Guid.NewGuid().ToString();

            UnitOfWork.Save();

            LogService.DebugExecution("Lock, id " + user.Id);
        }

        public void UnlockRestaurantAccount(RestaurantAccount user)
        {
            user.IsLockedOut = false;

            UnitOfWork.Save();

            LogService.DebugExecution("Unlock, id " + user.Id);
        }

        public bool ValidateRestaurantAccount(string codeNameOrEmail, string plainTextPassword)
        {
            RestaurantAccount restaurantAccount;

            return ValidateRestaurantAccount(codeNameOrEmail, plainTextPassword, out restaurantAccount);
        }

        public bool ValidateRestaurantAccount(string codeNameOrEmail, string plainTextPassword, out RestaurantAccount restaurantAccount)
        {
            restaurantAccount = UnitOfWork.RestaurantAccountRepository.GetRestaurantAccountByCodeNameOrEmail(codeNameOrEmail);

            if (restaurantAccount == null)
            {
                return false;
            }

            if (restaurantAccount.IsLockedOut)
            {
                return false;
            }

            string password = HashPassword(plainTextPassword, restaurantAccount.PasswordSalt);

            if (password != restaurantAccount.Password)
            {
                return false;
            }

            return true;
        }

        public FormModel GetRestaurantAccountFormModel(string restaurantCodeName)
        {
            RestaurantAccount restaurantAccount = Repository.GetUnlockedRestaurantAccountByCodeName(restaurantCodeName);

            if (restaurantAccount == null)
            {
                throw new RestaurantNotFoundServiceException(string.Format("Restaurace s kódem {0} nebyla nalezena", restaurantCodeName));
            }

            FormModel model = new FormModel
                                  {
                                      RestaurantAccountDisplayName = restaurantAccount.DisplayName,
                                      RestaurantAccountId = restaurantAccount.Id,
                                      Languages = restaurantAccount.LanguagesSet,
                                      FormSteps = new List<FormStepModel>(),
                                  };

            var steps = UnitOfWork.FormStepRepository.GetFormSteps(restaurantAccount);
            
            foreach (var formStep in steps)
            {
                FormStepModel stepModel = null;
                if (formStep is PercentQuestionsFormStep)
                {
                    stepModel = new FormPercentQuestionsStepModel()
                                    {
                                        PercentQuestions = QuestionService.GetPercentQuestionsForm((PercentQuestionsFormStep)formStep),
                                    };
                }
                else if (formStep is YesNoQuestionsFormStep)
                {
                    stepModel = new FormYesNoQuestionsStepModel()
                                    {
                                        YesNoQuestions = QuestionService.GetYesNoQuestionsForm((YesNoQuestionsFormStep)formStep)
                                    };
                }
                else if (formStep is TextInfoFormStep)
                {
                    var textInfoFormStep = ((TextInfoFormStep)formStep);
                    stepModel = new FormTextInfoStepModel()
                    {
                        InfoText = textInfoFormStep.InfoText,
                        SubmitButtonText = textInfoFormStep.SubmitButtonText,
                        TitleText = textInfoFormStep.TitleText,
                    };
                }
                else if (formStep is TextQuestionFormStep)
                {
                    var textQuestionStep = (TextQuestionFormStep)formStep;
                    stepModel = new FormTextQuestionStepModel()
                    {
                        InfoText = textQuestionStep.InfoText,
                        SubmitButtonText = textQuestionStep.SubmitButtonText,
                        TitleText = textQuestionStep.TitleText,
                    };
                }
                else if (formStep is ConfirmationFormStep)
                {
                    var confirmationStep = (ConfirmationFormStep)formStep;
                    stepModel = new FormConfirmationStepModel()
                    {
                        InfoText = confirmationStep.InfoText,
                        TitleText = confirmationStep.TitleText,
                    };
                }
                else if (formStep is LanguageSelectionFormStep)
                {
                    stepModel = new FormLanguageSelectionStepModel()
                    {
                        TitleText = ((LanguageSelectionFormStep)formStep).TitleText
                    };
                }

                stepModel.FormStepId = formStep.Id;
                stepModel.FormStepType = formStep.FormStepType;

                model.FormSteps.Add(stepModel);
            }

            return model;
        }


        #region "Private methods"

        private string GenerateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[32];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }

        private string HashPassword(string plainTextPassword, string salt)
        {
            byte[] pwdBytes = Encoding.UTF8.GetBytes(salt + plainTextPassword);
            byte[] hashBytes = SHA256.Create().ComputeHash(pwdBytes);

            return Convert.ToBase64String(hashBytes);
        }

        private string GenerateNewPassword()
        {
            string newPassword = System.Web.Security.Membership.GeneratePassword(8, 0);

            Random rand = new Random();

            return Regex.Replace(newPassword, @"[^a-zA-Z0-9]", m => rand.Next(0, 9).ToString(CultureInfo.InvariantCulture));
        }

        private void SetPassword(RestaurantAccount restaurantAccount, string password)
        {
            string salt = GenerateSalt();
            string hashedPassword = HashPassword(password, salt);

            restaurantAccount.Password = hashedPassword;
            restaurantAccount.PasswordSalt = salt;
        }


        private string GetUniqueCodeName(string displayName)
        {
            string originalCodeName = NameService.ToCodeName(displayName);

            int counter = 1;
            string newCodeName = originalCodeName;
            while (UnitOfWork.RestaurantAccountRepository.GetRestaurantAccountByCodeName(newCodeName) != null)
            {
                newCodeName = originalCodeName + counter++;
            }

            return newCodeName;
        }

        #endregion
    }
}