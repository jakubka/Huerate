/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using Huerate.Domain.Entities;
using Huerate.Domain.Repositories;
using Huerate.WebViewModels;
using Huerate.WebViewModels.Admin;
using Huerate.WebViewModels.Form;

namespace Huerate.Services.Questions
{
    public interface IQuestionService : IDataService<IQuestionRepository, Question, int>
    {
        List<ResultsQuestionModel> GetDayResultsQuestions(RestaurantAccount restaurantAccount, DateTime answersOn);
        List<ResultsQuestionModel> GetOverallResultsQuestions(RestaurantAccount restaurantAccount);

        List<FormYesNoQuestionModel> GetYesNoQuestionsForm(YesNoQuestionsFormStep formStep);

        List<FormPercentQuestionModel> GetPercentQuestionsForm(PercentQuestionsFormStep formStep);
        void SwapQuestionsOrder(int firstQuestionId, int secondQuestionId);

        PercentQuestion CreateEmptyPercentQuestion(PercentQuestionsFormStep formStep);
        YesNoQuestion CreateEmptyYesNoQuestion(YesNoQuestionsFormStep formStep);
    }
}