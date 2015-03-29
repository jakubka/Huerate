/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Huerate.Domain.Entities;
using Huerate.Logging;
using Huerate.Services.Answers;
using Huerate.Services.Email;
using Huerate.Services.FormSteps;
using Huerate.Services.Membership;
using Huerate.Services.Questions;
using Huerate.Services.RestaurantAccounts;
using Huerate.Services.Settings;
using Huerate.Services.Translations;
using Huerate.WebUI.Filters;
using Huerate.WebViewModels;
using Huerate.WebViewModels.Admin;
using Newtonsoft.Json;

namespace Huerate.WebUI.Controllers
{
    [MembershipAuthorizeFilter]
    public class AdminController : ControllerBase
    {
        private ILogService LogService { get; set; }
        private IFormStepService FormStepService { get; set; }
        private IAnswerService AnswerService { get; set; }
        private IEmailSenderService EmailSenderService { get; set; }
        private IQuestionService QuestionService { get; set; }
        private IRestaurantAccountService RestaurantAccountService { get; set; }

        public AdminController(ILogService logService, IFormStepService formStepService, IAnswerService answerService, IEmailSenderService emailSenderService, IQuestionService questionService, IRestaurantAccountService restaurantAccountService, IMembershipService membershipService, ISettingsService settingsService, ITranslationService translationService) : base(membershipService, settingsService, translationService)
        {
            LogService = logService;
            FormStepService = formStepService;
            AnswerService = answerService;
            EmailSenderService = emailSenderService;
            QuestionService = questionService;
            RestaurantAccountService = restaurantAccountService;
        }

        public ActionResult Index(int day = 0)
        {
            if (day > 0)
            {
                day = 0;
            }

            DateTime selectedDay = DateTime.UtcNow.Date.AddDays(day);

            var restaurantAccount = CurrentUser;

            DayResultsModel model = new DayResultsModel
                                        {
                                            Day = selectedDay,
                                            DayModifier = day,
                                            DisplayName = restaurantAccount.DisplayName,
                                            RestaurantAccountId = restaurantAccount.Id,
                                            Questions = QuestionService.GetDayResultsQuestions(restaurantAccount, selectedDay),
                                            TextAnswers = AnswerService.GetDayTextAnswers(restaurantAccount, selectedDay),
                                        };

            return View("DayResults", model);
        }

        public ActionResult OverallResults()
        {
            var restaurantAccount = CurrentUser;

            ResultsModel model = new ResultsModel
                                     {
                                         DisplayName = restaurantAccount.DisplayName,
                                         RestaurantAccountId = restaurantAccount.Id,
                                         Questions = QuestionService.GetOverallResultsQuestions(restaurantAccount),
                                         TextAnswers = AnswerService.GetAllTextAnswers(restaurantAccount),
                                     };

            return View("OverallResults", model);
        }


        public ActionResult EditForm()
        {
            var currentRestaurant = CurrentUser;

            var formSteps = FormStepService.Repository.GetEagerLoadedFormSteps(currentRestaurant);

            var model = new EditFormModel
                            {
                                Languages = currentRestaurant.LanguagesSet,
                                FormStepTypes = Enum.GetNames(typeof(FormStepType)),
                                FormSteps = formSteps.Select(EditFormStepModelFactory.GetEditFormStepModel).ToList()
                            };
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateNumberOfDisplayedQuestions(int formStepId, int numberOfDisplayedQuestions)
        {
            try
            {
                var formStep = FormStepService.GetById(formStepId);

                if (formStep == null || !(formStep is YesNoQuestionsFormStep || formStep is PercentQuestionsFormStep))
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Content("Form step not found");
                }

                if (formStep.RestaurantAccountId != CurrentUser.Id)
                {
                    Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return Content("Form step not yours");
                }

                if (formStep is YesNoQuestionsFormStep)
                {
                    ((YesNoQuestionsFormStep)formStep).DisplayQuestionsCount = numberOfDisplayedQuestions;
                }
                else
                {
                    ((PercentQuestionsFormStep)formStep).DisplayQuestionsCount = numberOfDisplayedQuestions;
                }

                FormStepService.Save();

                return Content("OK");
            }
            catch (Exception e)
            {
                EmailSenderService.SendErrorEmailAsync(string.Format("Error while updating form step, id: {0}, new no: {1}", formStepId, numberOfDisplayedQuestions), e);

                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Content(e.Message);
            }
        }

        [HttpPost]
        public ActionResult DeleteQuestion(int questionId)
        {
            try
            {
                var question = QuestionService.GetById(questionId);

                if (question == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Content("Question not found");
                }

                if (question.OwnerId != CurrentUser.Id)
                {
                    Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return Content("Question not yours");
                }

                QuestionService.Delete(question);

                return Content("OK");
            }
            catch (Exception e)
            {
                EmailSenderService.SendErrorEmailAsync(string.Format("Error while deleting question, id: {0}", questionId), e);

                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Content(e.Message);
            }
        }

        [HttpPost]
        public ContentResult DeleteFormStep(int formStepId)
        {
            var formStep = FormStepService.GetById(formStepId);

            FormStepService.Delete(formStep);

            return Content("OK");
        }


        [HttpPost]
        public ContentResult SwapFormStepsOrder(int firstFormStepId, int secondFormStepId)
        {
            try
            {
                FormStepService.SwapFormStepsOrder(firstFormStepId, secondFormStepId);
            }
            catch (Exception e)
            {
                LogService.Error("Error while swapping form steps order", e);

                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Content(e.Message);
            }

            return Content("OK");
        }

        [HttpPost]
        public ContentResult SwapQuestionsOrder(int firstQuestionId, int secondQuestionId)
        {
            try
            {
                QuestionService.SwapQuestionsOrder(firstQuestionId, secondQuestionId);
            }
            catch (Exception e)
            {
                LogService.Error("Error while swapping questions order", e);

                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Content(e.Message);
            }

            return Content("OK");
        }

        [HttpPost]
        public ContentResult SetLanguages(IEnumerable<string> languages)
        {
            try
            {
                CurrentUser.LanguagesSet = new HashSet<string>(languages);

                RestaurantAccountService.Save();
            }
            catch (Exception e)
            {
                LogService.Error("Error while saving languages", e);

                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Content(e.Message);
            }

            return Content("OK");
        }


        public JsonResult CreateQuestion(int formStepId)
        {
            var formStep = FormStepService.GetById(formStepId);

            Question question;

            if (formStep is YesNoQuestionsFormStep)
            {
                question = QuestionService.CreateEmptyYesNoQuestion((YesNoQuestionsFormStep)formStep);
            }
            else if (formStep is PercentQuestionsFormStep)
            {
                question = QuestionService.CreateEmptyPercentQuestion((PercentQuestionsFormStep)formStep);
            }
            else
            {
                throw new Exception("Unknown form step type");
            }

            return Json(EditFormStepModelFactory.GetQuestion(question));
        }

        [HttpPost]
        public JsonResult CreateFormStep(FormStepType formStepType)
        {
            try
            {
                var formStep = FormStepService.CreateFormStep(formStepType, CurrentUser);

                return Json(EditFormStepModelFactory.GetEditFormStepModel(formStep));
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(new {result = e.Message});
            }
        }

        public ViewResult AccountSettings()
        {
            return View();
        }

        public ViewResult RestaurantSettings()
        {
            var currentRestaurant = CurrentUser;

            return View(new RestaurantInfoSettingsModel
                            {
                                CodeName = currentRestaurant.CodeName,
                                DisplayName = currentRestaurant.DisplayName,
                            });
        }

        public ViewResult Help()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteAccount()
        {
            RestaurantAccountService.LockRestaurantAccount(CurrentUser);

            MembershipService.SignOut();

            return RedirectToAction("Index", "HomePage");
        }

        [HttpPost]
        public ActionResult ChangeEmail(ChangeEmailModel model)
        {
            if (ModelState.IsValid)
            {
                CurrentUser.Email = model.NewEmail;

                RestaurantAccountService.Save();

                AddMessage(Tr("Admin_EmailChangedSuccessfully"));
            }

            return View("AccountSettings");
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (RestaurantAccountService.ValidateRestaurantAccount(CurrentUser.Email, model.CurrentPassword))
                {
                    RestaurantAccountService.ChangePassword(CurrentUser, model.NewPassword1);

                    AddMessage(Tr("Admin_PasswordChangedSuccessfully"));
                }
                else
                {
                    ModelState.AddModelError("", Tr("Admin_WrongCurrentPassword"));
                }
            }

            return View("AccountSettings");
        }

        [HttpPost]
        public ActionResult RestaurantSettings(RestaurantInfoSettingsModel model)
        {
            if (ModelState.IsValid)
            {
                var restaurant = RestaurantAccountService.GetById(CurrentUser.Id);

                restaurant.CodeName = model.CodeName;
                restaurant.DisplayName = model.DisplayName;

                RestaurantAccountService.Save();

                AddMessage(Tr("Admin_RestaurantSettingsChanged"));
            }

            return View("RestaurantSettings");
        }
    }
}