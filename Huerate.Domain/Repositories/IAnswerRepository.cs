/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Linq;
using Huerate.Domain.Entities;

namespace Huerate.Domain.Repositories
{
    public interface IAnswerRepository : IRepository<Answer, int>
    {
        YesNoAnswer GetExistingYesNoAnswer(Guid answerSetGuid, int questionId, string language);

        PercentAnswer GetExistingPercentAnswer(Guid answerSetGuid, int questionId, string language);

        IQueryable<TextAnswer> GetTextAnswers(RestaurantAccount restaurantAccount);
        IQueryable<TextAnswer> GetTextAnswers(RestaurantAccount restaurantAccount, DateTime answeredOn);
    }
}