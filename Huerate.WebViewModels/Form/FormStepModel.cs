/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Collections.Generic;
using Huerate.Domain.Entities;

namespace Huerate.WebViewModels.Form
{
    public abstract class FormStepModel
    {
        public int FormStepId { get; set; }

        public FormStepType FormStepType { get; set; }
        public string ViewName
        {
            get { return FormStepType.ToString() + "StepPartial"; }
        }
    }

    public class FormPercentQuestionsStepModel : FormStepModel
    {
        public List<FormPercentQuestionModel> PercentQuestions { get; set; }
    }

    public class FormYesNoQuestionsStepModel : FormStepModel
    {
        public List<FormYesNoQuestionModel> YesNoQuestions { get; set; }
    }

    public class FormTextQuestionStepModel : FormStepModel
    {
        public string TitleText { get; set; }
        public string InfoText { get; set; }
        public string SubmitButtonText { get; set; }
    }

    public class FormTextInfoStepModel : FormStepModel
    {
        public string TitleText { get; set; }
        public string InfoText { get; set; }
        public string SubmitButtonText { get; set; }
    }

    public class FormConfirmationStepModel : FormStepModel
    {
        public string TitleText { get; set; }
        public string InfoText { get; set; }
    }

    public class FormLanguageSelectionStepModel : FormStepModel
    {
        public string TitleText { get; set; }
    }
}