/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;

namespace Huerate.WebViewModels.Admin
{
    public class ResultsModel
    {
        public string DisplayName { get; set; }
        public int RestaurantAccountId { get; set; }
        public List<ResultsQuestionModel> Questions { get; set; }
        public List<TextAnswerModel> TextAnswers { get; set; }
    }

    public class DayResultsModel : ResultsModel
    {
        public DateTime Day { get; set; }
        public int DayModifier { get; set; }
    }
}