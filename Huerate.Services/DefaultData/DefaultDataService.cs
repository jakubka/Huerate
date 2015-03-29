/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Linq;
using Huerate.Domain;
using Huerate.Domain.Entities;
using Huerate.Logging;
using Huerate.Services.Translations;

namespace Huerate.Services.DefaultData
{
    internal class DefaultDataService : ServiceBase, IDefaultDataService
    {
        private ITranslationService TranslationService { get; set; }

        public DefaultDataService(ITranslationService translationService, IUnitOfWork unitOfWork, ILogService logService) : base(unitOfWork, logService)
        {
            TranslationService = translationService;
        }

        public void FillNewRestauratAccount(RestaurantAccount restaurantAccount)
        {
            var fsr = UnitOfWork.FormStepRepository;

            int order = 1;

            fsr.Add(GetFormStep<LanguageSelectionFormStep>(restaurantAccount, order++));
            fsr.Add(GetFormStep<TextInfoFormStep>(restaurantAccount, order++));

            var overall = GetFormStep<PercentQuestionsFormStep>(restaurantAccount, order++);
            overall.DisplayQuestionsCount = 1;
            overall.PercentQuestions.Add(CreatePercentQuestion(overall, "Question_Overall_Text", "~/Content/Images/Form/welcome_pic.png", 1));

            fsr.Add(overall);

            var special = GetFormStep<PercentQuestionsFormStep>(restaurantAccount, order++);
            special.DisplayQuestionsCount = 3;
            special.PercentQuestions.Add(CreatePercentQuestion(overall, "Question_Food_Text", "~/Content/Images/Form/jidlo-img.png", 1));
            special.PercentQuestions.Add(CreatePercentQuestion(overall, "Question_Service_Text", "~/Content/Images/Form/prostredi-img.png", 2));
            special.PercentQuestions.Add(CreatePercentQuestion(overall, "Question_Enviroment_Text", "~/Content/Images/Form/obsluha-img.png", 3));

            fsr.Add(special);

            var yesNoStep = GetFormStep<YesNoQuestionsFormStep>(restaurantAccount, order++);

            AddYesNoQuestions(yesNoStep, "Question_Overall_", 3, 1);
            AddYesNoQuestions(yesNoStep, "Question_Food_", 4, 10);
            AddYesNoQuestions(yesNoStep, "Question_Service_", 3, 20);
            AddYesNoQuestions(yesNoStep, "Question_Enviroment_", 7, 30);

            fsr.Add(yesNoStep);

            fsr.Add(GetFormStep<TextQuestionFormStep>(restaurantAccount, order++));

            fsr.Add(GetFormStep<ConfirmationFormStep>(restaurantAccount, order));

            UnitOfWork.Save();
        }

        public TFormStep GetFormStep<TFormStep>(RestaurantAccount restaurantAccount, int? order = null) where TFormStep : FormStep
        {
            var factories = new Dictionary<Type, Func<RestaurantAccount, FormStep>>();

            factories[typeof(TextInfoFormStep)] = account => new TextInfoFormStep()
                                                                 {
                                                                     SubmitButtonText = TranslationService.CreateCustomResourcesInAllLanguages("Form_IntroFormStep_SubmitButtonText", restaurantAccount),
                                                                     TitleText = TranslationService.CreateCustomResourcesInAllLanguages("Form_IntroFormStep_TitleText", restaurantAccount, restaurantAccount.DisplayName),
                                                                     InfoText = TranslationService.CreateCustomResourcesInAllLanguages("Form_IntroFormStep_InfoText", restaurantAccount),
                                                                 };
            factories[typeof(PercentQuestionsFormStep)] = account => new PercentQuestionsFormStep()
                                                                         {
                                                                             DisplayQuestionsCount = 3
                                                                         };
            factories[typeof(YesNoQuestionsFormStep)] = account => new YesNoQuestionsFormStep()
                                                                       {
                                                                           DisplayQuestionsCount = 5
                                                                       };
            factories[typeof(TextQuestionFormStep)] = account => new TextQuestionFormStep()
                                                                     {
                                                                         SubmitButtonText = TranslationService.CreateCustomResourcesInAllLanguages("Form_TextQuestionFormStep_SubmitButtonText", restaurantAccount),
                                                                         TitleText = TranslationService.CreateCustomResourcesInAllLanguages("Form_TextQuestionFormStep_TitleText", restaurantAccount),
                                                                         InfoText = TranslationService.CreateCustomResourcesInAllLanguages("Form_TextQuestionFormStep_InfoText", restaurantAccount),
                                                                     };
            factories[typeof(ConfirmationFormStep)] = account => new ConfirmationFormStep()
                                                                     {
                                                                         TitleText = TranslationService.CreateCustomResourcesInAllLanguages("Form_ConfirmationFormStep_TitleText", restaurantAccount),
                                                                         InfoText = TranslationService.CreateCustomResourcesInAllLanguages("Form_ConfirmationFormStep_InfoText", restaurantAccount),
                                                                     };
            factories[typeof(LanguageSelectionFormStep)] = account => new LanguageSelectionFormStep()
                                                                          {
                                                                              TitleText = TranslationService.CreateCustomResourcesInAllLanguages("Form_LanguageSelectionFormStep_TitleText", restaurantAccount),
                                                                          };

            TFormStep formStep = (TFormStep)factories[typeof(TFormStep)](restaurantAccount);

            formStep.RestaurantAccount = restaurantAccount;
            formStep.Order = order ?? (UnitOfWork.FormStepRepository.Fetch().Where(fs => fs.RestaurantAccountId == restaurantAccount.Id).DefaultIfEmpty().Max(fs => (int?)fs.Order) ?? 0) + 1;
            formStep.IsEnabled = true;

            return formStep;
        }

        public FormStep GetFormStep(FormStepType formStepType, RestaurantAccount restaurantAccount, int? order = null)
        {
            switch (formStepType)
            {
                case FormStepType.TextInfo:
                    return GetFormStep<TextInfoFormStep>(restaurantAccount, order);
                case FormStepType.PercentQuestions:
                    return GetFormStep<PercentQuestionsFormStep>(restaurantAccount, order);
                case FormStepType.YesNoQuestions:
                    return GetFormStep<YesNoQuestionsFormStep>(restaurantAccount, order);
                case FormStepType.TextQuestion:
                    return GetFormStep<TextQuestionFormStep>(restaurantAccount, order);
                case FormStepType.Confirmation:
                    return GetFormStep<ConfirmationFormStep>(restaurantAccount, order);
                case FormStepType.LanguageSelection:
                    return GetFormStep<LanguageSelectionFormStep>(restaurantAccount, order);
            }

            return null;
        }


        #region "Private methods"

        private void AddYesNoQuestions(YesNoQuestionsFormStep step, string questionStart, int numberOfQuestions, int startOrder)
        {
            foreach (var questionName in Enumerable.Range(1, numberOfQuestions).Select(i => questionStart + i))
            {
                step.YesNoQuestions.Add(new YesNoQuestion()
                                            {
                                                Created = DateTime.UtcNow,
                                                Owner = step.RestaurantAccount,
                                                YesNoQuestionsFormStepId = step.Id,
                                                Text = TranslationService.CreateCustomResourcesInAllLanguages(questionName, step.RestaurantAccount),
                                                Order = startOrder++,
                                            });
            }
        }


        private PercentQuestion CreatePercentQuestion(FormStep step, string resourceString, string imagePath, int order)
        {
            return new PercentQuestion()
                       {
                           Created = DateTime.UtcNow,
                           Owner = step.RestaurantAccount,
                           PercentQuestionsFormStepId = step.Id,
                           Text = TranslationService.CreateCustomResourcesInAllLanguages(resourceString, step.RestaurantAccount),
                           ImagePath = imagePath,
                           Order = order,
                       };
        }

        #endregion
    }
}