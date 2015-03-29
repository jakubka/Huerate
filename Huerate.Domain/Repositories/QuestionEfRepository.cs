/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Linq;
using System.Linq.Expressions;
using Huerate.Domain.Entities;

namespace Huerate.Domain.Repositories
{
    internal class QuestionEfRepository : IntKeyEfRepository<Question>, IQuestionRepository, IOrderableRepository<Question, int>
    {
        private readonly Lazy<OrderableRepository<Question, int>> _ordererLazy;

        public QuestionEfRepository(HuerateContext context)
            : base(context)
        {
            Func<Question, Expression<Func<Question, bool>>> filterCreator = question =>
                                                                                 {
                                                                                     if (question is YesNoQuestion)
                                                                                     {
                                                                                         return q => ((YesNoQuestion)q).YesNoQuestionsFormStepId == ((YesNoQuestion)question).YesNoQuestionsFormStepId;
                                                                                     }
                                                                                     return q => ((PercentQuestion)q).PercentQuestionsFormStepId == ((PercentQuestion)question).PercentQuestionsFormStepId;
                                                                                 };

            Func<OrderableRepository<Question, int>> valueFactory = () => new OrderableRepository<Question, int>(this, filterCreator);
            _ordererLazy = new Lazy<OrderableRepository<Question, int>>(valueFactory);
        }

        public IQueryable<YesNoQuestion> GetAllYesNoQuestions(YesNoQuestionsFormStep formStep)
        {
            return from q in Fetch().OfType<YesNoQuestion>()
                   where q.YesNoQuestionsFormStepId == formStep.Id
                   orderby q.Order
                   select q;
        }

        public IQueryable<PercentQuestion> GetAllPercentQuestions(PercentQuestionsFormStep formStep)
        {
            return from q in Fetch().OfType<PercentQuestion>()
                   where q.PercentQuestionsFormStepId == formStep.Id
                   orderby q.Order
                   select q;
        }

        public IQueryable<YesNoQuestion> GetFormYesNoQuestions(YesNoQuestionsFormStep formStep)
        {
            return (from q in GetAllYesNoQuestions(formStep)
                    orderby Guid.NewGuid()
                    select q).Take(formStep.DisplayQuestionsCount).OrderBy(q => q.Order);
        }

        public IQueryable<PercentQuestion> GetFormPercentQuestions(PercentQuestionsFormStep formStep)
        {
            return (from q in GetAllPercentQuestions(formStep)
                    orderby Guid.NewGuid()
                    select q).Take(formStep.DisplayQuestionsCount).OrderBy(q => q.Order);
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