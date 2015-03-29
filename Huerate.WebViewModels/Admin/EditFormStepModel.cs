/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using AutoMapper;
using Huerate.Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Huerate.WebViewModels.Admin
{
    public static class EditFormStepModelFactory
    {
        static EditFormStepModelFactory()
        {
            Mapper.CreateMap<FormStep, EditFormStepModel>()
                  .ForMember(editStep => editStep.FormStepId, expression => expression.MapFrom(fs => fs.Id))
                  .Include<PercentQuestionsFormStep, EditPercentQuestionsStepModel>()
                  .Include<YesNoQuestionsFormStep, EditYesNoQuestionsStepModel>()
                  .Include<TextInfoFormStep, EditTextInfoStepModel>()
                  .Include<TextQuestionFormStep, EditTextQuestionStepModel>()
                  .Include<ConfirmationFormStep, EditConfirmationStepModel>()
                  .Include<LanguageSelectionFormStep, EditLanguageSelectionStepModel>();

            Mapper.CreateMap<PercentQuestionsFormStep, EditPercentQuestionsStepModel>();
            Mapper.CreateMap<YesNoQuestionsFormStep, EditYesNoQuestionsStepModel>();
            Mapper.CreateMap<TextInfoFormStep, EditTextInfoStepModel>();
            Mapper.CreateMap<TextQuestionFormStep, EditTextQuestionStepModel>();
            Mapper.CreateMap<ConfirmationFormStep, EditConfirmationStepModel>();
            Mapper.CreateMap<LanguageSelectionFormStep, EditLanguageSelectionStepModel>();

            Mapper.CreateMap<Question, EditQuestionModel>()
                  .ForMember(editQuestion => editQuestion.QuestionId, cex => cex.MapFrom(q => q.Id))
                  .ForMember(editQuestion => editQuestion.QuestionText, cex => cex.MapFrom(q => q.Text))
                  .Include<PercentQuestion, EditQuestionModel>()
                  .Include<YesNoQuestion, EditQuestionModel>();

            Mapper.CreateMap<PercentQuestion, EditQuestionModel>();
            Mapper.CreateMap<YesNoQuestion, EditQuestionModel>();

            Mapper.AssertConfigurationIsValid();
        }

        public static EditFormStepModel GetEditFormStepModel(FormStep formStep)
        {
            return Mapper.Map<FormStep, EditFormStepModel>(formStep);
        }

        public static EditQuestionModel GetQuestion(Question question)
        {
            return Mapper.Map<Question, EditQuestionModel>(question);
        }
    }

    public abstract class EditFormStepModel
    {
        public int FormStepId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FormStepType FormStepType { set; get; }

        public string FormStepTypeString
        {
            get { return FormStepType.ToString(); }
        }
    }

    public class EditPercentQuestionsStepModel : EditFormStepModel
    {
        public int DisplayQuestionsCount { get; set; }

        public IList<EditQuestionModel> PercentQuestions { get; set; }
    }

    public class EditYesNoQuestionsStepModel : EditFormStepModel
    {
        public int DisplayQuestionsCount { get; set; }

        public IList<EditQuestionModel> YesNoQuestions { get; set; }
    }

    public class EditTextQuestionStepModel : EditFormStepModel
    {
        public string TitleText { get; set; }
        public string InfoText { get; set; }
        public string SubmitButtonText { get; set; }
    }

    public class EditTextInfoStepModel : EditFormStepModel
    {
        public string TitleText { get; set; }
        public string InfoText { get; set; }
        public string SubmitButtonText { get; set; }
    }

    public class EditConfirmationStepModel : EditFormStepModel
    {
        public string TitleText { get; set; }
        public string InfoText { get; set; }
    }
    
    public class EditLanguageSelectionStepModel : EditFormStepModel
    {
        public string TitleText { get; set; }
    }
}