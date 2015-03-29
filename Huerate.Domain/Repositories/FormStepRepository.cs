/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Linq;
using Huerate.Domain.Entities;
using System.Data.Entity;

namespace Huerate.Domain.Repositories
{
    internal class FormStepRepository : IntKeyEfRepository<FormStep>, IFormStepRepository
    {
        private readonly Lazy<OrderableRepository<FormStep, int>> _ordererLazy;

        public FormStepRepository(HuerateContext context) : base(context)
        {
            _ordererLazy = new Lazy<OrderableRepository<FormStep, int>>(() => new OrderableRepository<FormStep, int>(this, formStep => fs => fs.RestaurantAccountId == formStep.RestaurantAccountId));
        }

        public List<FormStep> GetFormSteps(RestaurantAccount restaurantAccount)
        {
            return Fetch().Where(fs => fs.RestaurantAccountId == restaurantAccount.Id && fs.IsEnabled).OrderBy(fs => fs.Order).ToList();
        }

        public List<FormStep> GetEagerLoadedFormSteps(RestaurantAccount restaurantAccount)
        {
            var query = Fetch().Where(fs => fs.RestaurantAccountId == restaurantAccount.Id && fs.IsEnabled);

            var query1 = query.OfType<PercentQuestionsFormStep>().Include("PercentQuestions").ToList();
            var query2 = query.OfType<YesNoQuestionsFormStep>().Include("YesNoQuestions").ToList();
            var query3 = query.Where(fs => !(fs is PercentQuestionsFormStep || fs is YesNoQuestionsFormStep)).ToList();

            return query3.Concat(query1).Concat(query2).OrderBy(fs => fs.Order).ToList();
        }

        public void MoveUp(int id)
        {
            _ordererLazy.Value.MoveUp(id);
        }

        public void MoveDown(int id)
        {
            _ordererLazy.Value.MoveDown(id);
        }
    }
}