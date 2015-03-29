/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using Huerate.Domain;
using Huerate.Domain.Entities;
using Huerate.Domain.Repositories;
using Huerate.Logging;
using Huerate.Services.Translations;
using Huerate.WebViewModels;
using Huerate.WebViewModels.Admin;
using Huerate.WebViewModels.Form;

namespace Huerate.Services.Questions
{
    internal class QuestionService : DataServiceBase<IQuestionRepository, Question, int>, IQuestionService
    {
        private ITranslationService TranslationService { get; set; }

        public QuestionService(ITranslationService translationService, IUnitOfWork unitOfWork, ILogService logService) : base(unitOfWork, logService)
        {
            TranslationService = translationService;
        }

        public List<ResultsQuestionModel> GetDayResultsQuestions(RestaurantAccount restaurantAccount, DateTime answersOn)
        {
            var questionsQuery = Repository.Fetch().Where(q => q.OwnerId == restaurantAccount.Id).OrderBy(q => q.Order);

            var query1 = questionsQuery.OfType<PercentQuestion>().OrderBy(q => q.PercentQuestionsFormStep.Order).ThenBy(q => q.Order).Select(GetPercentDayResultsConverter(answersOn));
            var query2 = questionsQuery.OfType<YesNoQuestion>().OrderBy(q => q.YesNoQuestionsFormStep.Order).ThenBy(q => q.Order).Select(GetYesNoDayResultsConverter(answersOn));

            return query1.Concat(query2).ToList();
        }

        public List<ResultsQuestionModel> GetOverallResultsQuestions(RestaurantAccount restaurantAccount)
        {
            var questionsQuery = Repository.Fetch().Where(q => q.OwnerId == restaurantAccount.Id).OrderBy(q => q.Order);

            var query1 = questionsQuery.OfType<PercentQuestion>().OrderBy(q => q.PercentQuestionsFormStep.Order).ThenBy(q => q.Order).Select(q =>
                                                                                                                                             new ResultsQuestionModel
                                                                                                                                                 {
                                                                                                                                                     QuestionText = q.Text,
                                                                                                                                                     AnswersCount = q.PercentAnswers.AsQueryable().Count(),
                                                                                                                                                     AverageAnswer = q.PercentAnswers.AsQueryable().DefaultIfEmpty().Average(a => (double?)a.Value),
                                                                                                                                                 });
            var query2 = questionsQuery.OfType<YesNoQuestion>().OrderBy(q => q.YesNoQuestionsFormStep.Order).ThenBy(q => q.Order).Select(q =>
                                                                                                                                         new ResultsQuestionModel
                                                                                                                                             {
                                                                                                                                                 QuestionText = q.Text,
                                                                                                                                                 AnswersCount = q.YesNoAnswers.AsQueryable().Count(),
                                                                                                                                                 AverageAnswer = q.YesNoAnswers.AsQueryable().DefaultIfEmpty().Average(a => a == null ? null : (double?)(a.Value ? 100 : 0)),
                                                                                                                                             });

            return query1.Concat(query2).ToList();
        }

        public List<FormYesNoQuestionModel> GetYesNoQuestionsForm(YesNoQuestionsFormStep formStep)
        {
            return Repository.GetFormYesNoQuestions(formStep)
                             .Select(GetFormYesNoConverter())
                             .ToList();
        }

        public List<FormPercentQuestionModel> GetPercentQuestionsForm(PercentQuestionsFormStep formStep)
        {
            return Repository.GetFormPercentQuestions(formStep)
                             .Select(GetFormPercentConverter())
                             .ToList();
        }

        public void SwapQuestionsOrder(int firstQuestionId, int secondQuestionId)
        {
            var firstQuestion = Repository.GetById(firstQuestionId);
            var secondQuestion = Repository.GetById(secondQuestionId);

            var tmp = firstQuestion.Order;
            firstQuestion.Order = secondQuestion.Order;
            secondQuestion.Order = tmp;

            Save();
        }

        public PercentQuestion CreateEmptyPercentQuestion(PercentQuestionsFormStep formStep)
        {
            var question = new PercentQuestion()
                               {
                                   Created = DateTime.UtcNow,
                                   OwnerId = formStep.RestaurantAccountId,
                                   PercentQuestionsFormStepId = formStep.Id,
                                   Text = Guid.NewGuid().ToString(),
                                   ImagePath = "~/Content/Images/Form/prostredi-img.png",
                                   Order = Repository.Fetch().OfType<PercentQuestion>().Where(q => q.PercentQuestionsFormStepId == formStep.Id).DefaultIfEmpty().Max(q => (int?)q.Order) ?? 0 + 1,
                               };

            Repository.Add(question);

            Save();

            return question;
        }

        public YesNoQuestion CreateEmptyYesNoQuestion(YesNoQuestionsFormStep formStep)
        {
            var question = new YesNoQuestion()
                               {
                                   Created = DateTime.UtcNow,
                                   OwnerId = formStep.RestaurantAccountId,
                                   YesNoQuestionsFormStepId = formStep.Id,
                                   Text = Guid.NewGuid().ToString(),
                                   Order = Repository.Fetch().OfType<PercentQuestion>().Where(q => q.PercentQuestionsFormStepId == formStep.Id).DefaultIfEmpty().Max(q => (int?)q.Order) ?? 0 + 1,
                               };

            Repository.Add(question);

            Save();

            return question;
        }

        public override void Delete(Question entity)
        {
            base.Delete(entity);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        private static Expression<Func<YesNoQuestion, ResultsQuestionModel>> GetYesNoDayResultsConverter(DateTime answersOn)
        {
            Expression<Func<YesNoAnswer, bool>> timeFilter = a => a.Created.Month == answersOn.Month && a.Created.Day == answersOn.Day && a.Created.Year == answersOn.Year;

            return
                q =>
                new ResultsQuestionModel
                    {
                        QuestionText = q.Text,
                        AnswersCount = q.YesNoAnswers.AsQueryable().Count(timeFilter),
                        AverageAnswer = q.YesNoAnswers.AsQueryable().Where(timeFilter).DefaultIfEmpty().Average(a => a == null ? null : (double?)(a.Value ? 100 : 0)),
                    };
        }

        private static Expression<Func<PercentQuestion, ResultsQuestionModel>> GetPercentDayResultsConverter(DateTime answersOn)
        {
            Expression<Func<PercentAnswer, bool>> timeFilter = a => a.Created.Month == answersOn.Month && a.Created.Day == answersOn.Day && a.Created.Year == answersOn.Year;

            return
                q =>
                new ResultsQuestionModel
                    {
                        QuestionText = q.Text,
                        AnswersCount = q.PercentAnswers.AsQueryable().Count(timeFilter),
                        AverageAnswer = q.PercentAnswers.AsQueryable().Where(timeFilter).DefaultIfEmpty().Average(a => a == null ? null : (double?)a.Value),
                    };
        }

        private static Expression<Func<YesNoQuestion, FormYesNoQuestionModel>> GetFormYesNoConverter()
        {
            return
                q =>
                new FormYesNoQuestionModel()
                    {
                        QuestionId = q.Id,
                        QuestionText = q.Text,
                    };
        }

        private static Expression<Func<PercentQuestion, FormPercentQuestionModel>> GetFormPercentConverter()
        {
            return
                q =>
                new FormPercentQuestionModel()
                    {
                        QuestionId = q.Id,
                        QuestionText = q.Text,
                        ImagePath = q.ImagePath,
                    };
        }
    }
}