/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Huerate.Domain.Entities;
using Huerate.Services;
using Huerate.Services.DefaultData;
using Huerate.Services.Translations;
using Question = Huerate.DomainOld.Entities.Question;
using RestaurantAccount = Huerate.DomainOld.Entities.RestaurantAccount;

namespace Huerate.DatabaseDataTransfer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var oldUnitOfWork = new DomainOld.EfUnitOfWork();
            var newUnitOfWork = new Domain.EfUnitOfWork(null);

            /*
             * Transfer:
             * 1. RestaurantAccounts, Contacts, Activities, EmailTemplates, ScheduledEmails, Stays the same
             * 2. Add Cultures to RestaurantAccounts (cs only)
             * 3. Add answer sets
             * 4. Create default form steps for restaurants (default data service)
             *  - add questions to according form steps
             *  - add answers
             * 
             * 
             * Manual update of Coloseum and Austerlitz
            */

            // delete * from Questions
            // delete * from Answers

            var sw = Stopwatch.StartNew();

            HuerateDependencyResolver.SetSpecificImplementation<Domain.IUnitOfWork>(newUnitOfWork);

            var defaultDataService = HuerateDependencyResolver.Get<IDefaultDataService>();
            var translationService = HuerateDependencyResolver.Get<ITranslationService>();


            //Console.WriteLine(ReferenceEquals(HuerateDependencyResolver.Get<Domain.IUnitOfWork>(), HuerateDependencyResolver.Get<Domain.IUnitOfWork>()));

            //Console.ReadLine();

            //return;

            var allNewRestaurants = newUnitOfWork.RestaurantAccountRepository.Fetch().ToList();

            foreach (var restaurantAccount in allNewRestaurants)
            {
                int order = 1;

                string language = "cs";

                restaurantAccount.Languages = language;
                restaurantAccount.DefaultLanguage = language;

                var oldRestaurant = oldUnitOfWork.RestaurantAccountRepository.GetById(restaurantAccount.Id);

                newUnitOfWork.FormStepRepository.Add(defaultDataService.GetFormStep<TextInfoFormStep>(restaurantAccount, order++));

                var overallFormStep = defaultDataService.GetFormStep<PercentQuestionsFormStep>(restaurantAccount, order++);

                FillPercentFormStep(new[] {oldRestaurant.OverallQuestion}, translationService, restaurantAccount, overallFormStep);

                newUnitOfWork.FormStepRepository.Add(overallFormStep);

                var specialFormStep = defaultDataService.GetFormStep<PercentQuestionsFormStep>(restaurantAccount, order++);

                FillPercentFormStep(new[] {oldRestaurant.FirstSpecialQuestion, oldRestaurant.SecondSpecialQuestion, oldRestaurant.ThirdSpecialQuestion}, translationService, restaurantAccount, specialFormStep);

                newUnitOfWork.FormStepRepository.Add(specialFormStep);
                
                
                var yesNoFormStep = defaultDataService.GetFormStep<YesNoQuestionsFormStep>(restaurantAccount, order++);

                FillYesNoFormStep(oldRestaurant.Questions.Where(q => !q.IsPredefined), translationService, restaurantAccount, yesNoFormStep);

                newUnitOfWork.FormStepRepository.Add(yesNoFormStep);

                var textQuestionFormStep = defaultDataService.GetFormStep<TextQuestionFormStep>(restaurantAccount, order++);

                textQuestionFormStep.TextAnswers = oldRestaurant.AnswerSets.Where(answerSet => !string.IsNullOrWhiteSpace(answerSet.TextAnswer)).Select(answerSet => new TextAnswer()
                                                                                                    {
                                                                                                        Created = answerSet.Created,
                                                                                                        AnswerSetId = answerSet.Id,
                                                                                                        Language = language,
                                                                                                        Value = answerSet.TextAnswer,
                                                                                                    }).ToList();

                newUnitOfWork.FormStepRepository.Add(textQuestionFormStep);

                newUnitOfWork.FormStepRepository.Add(defaultDataService.GetFormStep<ConfirmationFormStep>(restaurantAccount, order++));
            }


            newUnitOfWork.Save();


            //foreach (var restaurantAccount in oldUnitOfWork.RestaurantAccountRepository.Fetch().Take(10))
            //{
            //    Console.WriteLine(restaurantAccount.CodeName);
            //}

            //Console.WriteLine("NEW:");
            //foreach (var restaurantAccount in newUnitOfWork.RestaurantAccountRepository.Fetch().Take(10))
            //{
            //    Console.WriteLine(restaurantAccount.CodeName);
            //}
            //Console.WriteLine("END");
            //var que = new YesNoQuestion
            //              {
            //                  OwnerId = 1,
            //                  Created = DateTime.Now,
            //                  Text = "prease",
            //              };
            //newUnitOfWork.QuestionRepository.Add(que);
            //newUnitOfWork.Save();
            //foreach (var q in newUnitOfWork.QuestionRepository.Fetch().Take(10))
            //{
            //    Console.WriteLine(q.Text);
            //}

            Console.WriteLine(sw.Elapsed);
            Console.WriteLine("END");

            Console.ReadLine();
        }

        private static void FillPercentFormStep(IEnumerable<Question> oldQuestions, ITranslationService translationService, Domain.Entities.RestaurantAccount restaurantAccount, PercentQuestionsFormStep formStep)
        {
            int order = 1;

            string language = restaurantAccount.DefaultLanguage;

            foreach (var oldQuestion in oldQuestions)
            {
                var newQuestion = new PercentQuestion()
                                      {
                                          Created = oldQuestion.Created,
                                          Text = translationService.CreateCustomTranslation(language, oldQuestion.Text, restaurantAccount),
                                          OwnerId = restaurantAccount.Id,
                                          ImagePath = oldQuestion.ImagePath ?? "~/Content/Images/Form/welcome_pic.png",
                                          Order = order++,
                                      };

                newQuestion.PercentAnswers = oldQuestion.Answers.Select(a => new PercentAnswer()
                                                                                 {
                                                                                     AnswerSetId = a.AnswerSetId,
                                                                                     Language = language,
                                                                                     Value = a.Value,
                                                                                     Created = a.Created,
                                                                                 }).ToList();

                formStep.PercentQuestions.Add(newQuestion);
            }
        }

        private static void FillYesNoFormStep(IEnumerable<Question> oldQuestions, ITranslationService translationService, Domain.Entities.RestaurantAccount restaurantAccount, YesNoQuestionsFormStep formStep)
        {
            int order = 1;

            string language = restaurantAccount.DefaultLanguage;

            foreach (var oldQuestion in oldQuestions)
            {
                var newQuestion = new YesNoQuestion()
                {
                    Created = oldQuestion.Created,
                    Text = translationService.CreateCustomTranslation(language, oldQuestion.Text, restaurantAccount),
                    OwnerId = restaurantAccount.Id,
                    Order = order++,
                };

                newQuestion.YesNoAnswers = oldQuestion.Answers.Select(a => new YesNoAnswer()
                {
                    AnswerSetId = a.AnswerSetId,
                    Language = language,
                    Value = a.Value > 0,
                    Created = a.Created,
                }).ToList();

                formStep.YesNoQuestions.Add(newQuestion);
            }
        }
    }
}