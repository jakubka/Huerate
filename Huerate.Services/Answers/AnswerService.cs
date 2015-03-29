/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Linq;
using Huerate.Domain;
using Huerate.Domain.Entities;
using Huerate.Domain.Repositories;
using Huerate.Logging;
using Huerate.Services.AnswerSets;
using Huerate.WebViewModels.Admin;

namespace Huerate.Services.Answers
{
    internal class AnswerService : DataServiceBase<IAnswerRepository, Answer, int>, IAnswerService
    {
        private IAnswerSetService AnswerSetService { get; set; }

        public AnswerService(IAnswerSetService answerSetService, IUnitOfWork unitOfWork, ILogService logService)
            : base(unitOfWork, logService)
        {
            AnswerSetService = answerSetService;
        }

        public List<TextAnswerModel> GetDayTextAnswers(RestaurantAccount restaurantAccount, DateTime answeredOn)
        {
            return Repository.GetTextAnswers(restaurantAccount, answeredOn).Select(a => new TextAnswerModel()
                                                                                            {
                                                                                                Created = a.Created,
                                                                                                TextAnswer = a.Value,
                                                                                            }).ToList();
        }

        public List<TextAnswerModel> GetAllTextAnswers(RestaurantAccount restaurantAccount)
        {
            return Repository.GetTextAnswers(restaurantAccount).Select(a => new TextAnswerModel()
                                                                                {
                                                                                    Created = a.Created,
                                                                                    TextAnswer = a.Value,
                                                                                }).ToList();
        }

        public void CreateTextAnswer(Guid answerSetGuid, int formStepId, string text, string language)
        {
            var formStep = UnitOfWork.FormStepRepository.GetById(formStepId) as TextQuestionFormStep;

            if (formStep == null)
            {
                throw new ServiceException("Form step not found");
            }

            Repository.Add(new TextAnswer()
                               {
                                   AnswerSet = AnswerSetService.GetOrCreateAnswerSet(answerSetGuid, formStep.RestaurantAccountId),
                                   Created = DateTime.UtcNow,
                                   TextQuestionFormStep = formStep,
                                   Value = text,
                                   Language = language,
                               });

            UnitOfWork.Save();
        }

        public void CreatePercentAnswer(Guid answerSetGuid, int questionId, int value, string language)
        {
            PercentQuestion question = UnitOfWork.QuestionRepository.GetById(questionId) as PercentQuestion;

            if (question == null)
            {
                throw new ServiceException("Question not found");
            }

            AnswerSet answerSet = AnswerSetService.GetOrCreateAnswerSet(answerSetGuid, question.OwnerId);

            PercentAnswer answer = Repository.GetExistingPercentAnswer(answerSetGuid, questionId, language);

            if (answer == null)
            {
                Repository.Add(answer = new PercentAnswer
                                            {
                                                AnswerSet = answerSet,
                                                Created = DateTime.UtcNow,
                                                PercentQuestion = question,
                                                Value = value,
                                                Language = language,
                                            });
            }
            else
            {
                answer.Created = DateTime.UtcNow;
                answer.Value = value;
            }

            UnitOfWork.Save();

            LogService.DebugExecution("Percent answer created: " + answer.Id);
        }

        public void CreateYesNoAnswer(Guid answerSetGuid, int questionId, bool value, string language)
        {
            YesNoQuestion question = UnitOfWork.QuestionRepository.GetById(questionId) as YesNoQuestion;

            if (question == null)
            {
                throw new ServiceException("Question not found");
            }

            AnswerSet answerSet = AnswerSetService.GetOrCreateAnswerSet(answerSetGuid, question.OwnerId);

            YesNoAnswer answer = Repository.GetExistingYesNoAnswer(answerSetGuid, questionId, language);

            if (answer == null)
            {
                Repository.Add(answer = new YesNoAnswer
                                            {
                                                AnswerSet = answerSet,
                                                Created = DateTime.UtcNow,
                                                YesNoQuestion = question,
                                                Value = value,
                                                Language = language,
                                            });
            }
            else
            {
                answer.Created = DateTime.UtcNow;
                answer.Value = value;
            }

            UnitOfWork.Save();

            LogService.DebugExecution("YesNo answer created: " + answer.Id);
        }
    }
}