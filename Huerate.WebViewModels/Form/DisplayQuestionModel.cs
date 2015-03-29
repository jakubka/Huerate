/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

namespace Huerate.WebViewModels.Form
{
    public abstract class FormQuestionModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
    }

    public class FormYesNoQuestionModel : FormQuestionModel
    {
    }

    public class FormPercentQuestionModel : FormQuestionModel
    {
        public string ImagePath { get; set; }
    }
}