/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Huerate.Domain.Entities
{
    public enum FormStepType
    {
        PercentQuestions,
        YesNoQuestions,
        TextQuestion,
        TextInfo,
        Confirmation,
        LanguageSelection,
    }

    public abstract class FormStep : IPrimaryKeyEntity<int>, IOrderableEntity
    {
        public int Id { get; set; }

        [ForeignKey("RestaurantAccount")]
        public int RestaurantAccountId { get; set; }

        public RestaurantAccount RestaurantAccount { get; set; }

        public int Order { get; set; }

        public bool IsEnabled { get; set; }

        public abstract FormStepType FormStepType { get; }
    }
    
    [Table("PercentQuestionsFormSteps")]
    public class PercentQuestionsFormStep : FormStep
    {
        public PercentQuestionsFormStep()
        {
            PercentQuestions = new HashSet<PercentQuestion>();
        }

        public ICollection<PercentQuestion> PercentQuestions { get; set; }

        public int DisplayQuestionsCount { get; set; }

        public override FormStepType FormStepType
        {
            get { return FormStepType.PercentQuestions; }
        }
    }

    [Table("YesNoQuestionsFormSteps")]
    public class YesNoQuestionsFormStep : FormStep
    {
        public YesNoQuestionsFormStep()
        {
            YesNoQuestions = new HashSet<YesNoQuestion>();
        }

        public ICollection<YesNoQuestion> YesNoQuestions { get; set; }

        public int DisplayQuestionsCount { get; set; }

        public override FormStepType FormStepType
        {
            get { return FormStepType.YesNoQuestions; }
        }
    }

    [Table("TextQuestionFormSteps")]
    public class TextQuestionFormStep : FormStep
    {
        public TextQuestionFormStep()
        {
            TextAnswers = new HashSet<TextAnswer>();
        }

        public string TitleText { get; set; }
        public string InfoText { get; set; }
        public string SubmitButtonText { get; set; }

        public ICollection<TextAnswer> TextAnswers { get; set; }

        public override FormStepType FormStepType
        {
            get { return FormStepType.TextQuestion; }
        }
    }

    [Table("TextInfoFormSteps")]
    public class TextInfoFormStep : FormStep
    {
        public string TitleText { get; set; }
        public string InfoText { get; set; }
        public string SubmitButtonText { get; set; }

        public override FormStepType FormStepType
        {
            get { return FormStepType.TextInfo; }
        }
    }

    [Table("ConfirmationFormSteps")]
    public class ConfirmationFormStep : FormStep
    {
        public string TitleText { get; set; }
        public string InfoText { get; set; }

        public override FormStepType FormStepType
        {
            get { return FormStepType.Confirmation; }
        }
    }

    [Table("LanguageSelectionFormSteps")]
    public class LanguageSelectionFormStep : FormStep
    {
        public string TitleText { get; set; }

        public override FormStepType FormStepType
        {
            get { return FormStepType.LanguageSelection; }
        }
    }
}