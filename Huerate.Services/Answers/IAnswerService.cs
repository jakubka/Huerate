/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Huerate.Domain.Entities;
using Huerate.Domain.Repositories;
using Huerate.WebViewModels.Admin;

namespace Huerate.Services.Answers
{
    public interface IAnswerService : IDataService<IAnswerRepository, Answer, int>
    {
        void CreatePercentAnswer(Guid answerSetGuid, int questionId, int value, string language);

        void CreateYesNoAnswer(Guid answerSetGuid, int questionId, bool value, string language);

        void CreateTextAnswer(Guid answerSetGuid, int formStepId, string text, string language);

        List<TextAnswerModel> GetDayTextAnswers(RestaurantAccount restaurantAccount, DateTime answeredOn);
        List<TextAnswerModel> GetAllTextAnswers(RestaurantAccount restaurantAccount);
    }
}
