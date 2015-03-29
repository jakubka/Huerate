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
    public interface IQuestionRepository : IRepository<Question, int>, IOrderableRepository<Question, int>
    {
        IQueryable<YesNoQuestion> GetAllYesNoQuestions(YesNoQuestionsFormStep formStep);

        IQueryable<PercentQuestion> GetAllPercentQuestions(PercentQuestionsFormStep formStep);
        
        IQueryable<YesNoQuestion> GetFormYesNoQuestions(YesNoQuestionsFormStep formStep);

        IQueryable<PercentQuestion> GetFormPercentQuestions(PercentQuestionsFormStep formStep);
    }
}