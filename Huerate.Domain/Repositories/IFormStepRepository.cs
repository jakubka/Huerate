/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Collections.Generic;
using Huerate.Domain.Entities;

namespace Huerate.Domain.Repositories
{
    public interface IFormStepRepository : IRepository<FormStep, int>, IOrderableRepository<FormStep, int>
    {
        List<FormStep> GetFormSteps(RestaurantAccount restaurantAccount);

        List<FormStep> GetEagerLoadedFormSteps(RestaurantAccount restaurantAccount);
    }
}