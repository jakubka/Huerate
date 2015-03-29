/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Linq;
using Huerate.Domain.Entities;

namespace Huerate.Domain.Repositories
{
    internal class ActivityTypeEfRepository : EfRepositoryBase<ActivityType, ActivityTypeEnum>
    {
        public ActivityTypeEfRepository(HuerateContext context)
            : base(context)
        {
        }

        public override ActivityType GetById(ActivityTypeEnum id)
        {
            return Fetch().SingleOrDefault(entity => entity.Id == id);
        }
    }
}