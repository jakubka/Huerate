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
    internal class AnswerSetEfRepository : GuidKeyEfRepository<AnswerSet>, IAnswerSetRepository
    {
        public AnswerSetEfRepository(HuerateContext context)
            : base(context)
        {
        }

        public AnswerSet GetByGuid(Guid guid, int restaurantAccountId)
        {
            return Fetch().SingleOrDefault(set => set.Id == guid && set.RestaurantAccountId == restaurantAccountId);
        }
    }
}