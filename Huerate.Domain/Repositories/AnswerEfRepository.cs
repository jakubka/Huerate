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
    internal class AnswerEfRepository : IntKeyEfRepository<Answer>, IAnswerRepository
    {
        public AnswerEfRepository(HuerateContext context) : base(context)
        {
        }

        public YesNoAnswer GetExistingYesNoAnswer(Guid answerSetGuid, int questionId, string language)
        {
            return Fetch().OfType<YesNoAnswer>().SingleOrDefault(a => a.AnswerSetId == answerSetGuid && a.YesNoQuestionId == questionId && a.Language == language);
        }

        public PercentAnswer GetExistingPercentAnswer(Guid answerSetGuid, int questionId, string language)
        {
            return Fetch().OfType<PercentAnswer>().SingleOrDefault(a => a.AnswerSetId == answerSetGuid && a.PercentQuestionId == questionId && a.Language == language);
        }

        public IQueryable<TextAnswer> GetTextAnswers(RestaurantAccount restaurantAccount)
        {
            return from a in Fetch().OfType<TextAnswer>()
                   where a.TextQuestionFormStep.RestaurantAccountId == restaurantAccount.Id
                   orderby a.Created descending 
                   select a;
        }

        public IQueryable<TextAnswer> GetTextAnswers(RestaurantAccount restaurantAccount, DateTime answeredOn)
        {
            return from a in Fetch().OfType<TextAnswer>()
                   where a.TextQuestionFormStep.RestaurantAccountId == restaurantAccount.Id
                   where
                       a.Created.Day == answeredOn.Day &&
                       a.Created.Month == answeredOn.Month &&
                       a.Created.Year == answeredOn.Year
                   orderby a.Created descending 
                   select a;
        }
    }
}