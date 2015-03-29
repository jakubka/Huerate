/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using Huerate.Domain;
using Huerate.Logging;
using Huerate.Services.AnswerSets;
using Huerate.Services.Answers;
using Huerate.Services.Cache;
using Huerate.Services.Contacts;
using Huerate.Services.DefaultData;
using Huerate.Services.Email;
using Huerate.Services.FormSteps;
using Huerate.Services.ImageProcessing;
using Huerate.Services.LocalStorage;
using Huerate.Services.Membership;
using Huerate.Services.Names;
using Huerate.Services.NewsletterSubscriptions;
using Huerate.Services.Questions;
using Huerate.Services.RestaurantAccounts;
using Huerate.Services.Settings;
using Huerate.Services.Translations;
using Microsoft.ApplicationServer.Caching;
using Ninject;
using Ninject.Activation;
using Ninject.Web.Common;

namespace Huerate.Services
{
    public static class NinjectKernelFactory
    {
        private static readonly Lazy<IKernel> KernelLazy = new Lazy<IKernel>(() =>
                                                                        {
                                                                            var kernel = new StandardKernel();
                                                                            AddBindings(kernel);

                                                                            return kernel;
                                                                        }, true);

        public static IKernel GetKernel()
        {
            return KernelLazy.Value;
        }

        private static void AddBindings(IKernel kernel)
        {
            // Domain:
            kernel.Bind<IUnitOfWork>().To<EfUnitOfWork>().InRequestScope();

            // Settings:
            kernel.Bind<ISettingsService>().To<SettingsService>().InRequestScope();

            // Membership:
            kernel.Bind<IMembershipPersistenceService>().To<MembershipPersistenceService>().InRequestScope();
            kernel.Bind<IMembershipService>().To<MembershipService>().InRequestScope();

            // Contacts:
            kernel.Bind<IContactService>().To<ContactService>().InRequestScope();
            kernel.Bind<IContactPersistenceService>().To<CookieContactPersistenceService>();

            // Names
            kernel.Bind<INameService>().To<NameService>().InSingletonScope();

            // Translations
            kernel.Bind<ITranslationService>().To<TranslationService>();

            // Answer
            kernel.Bind<IAnswerService>().To<AnswerService>();

            // AnswerSet
            kernel.Bind<IAnswerSetService>().To<AnswerSetService>();

            // NewsletterSubscription
            kernel.Bind<INewsletterSubscriptionService>().To<NewsletterSubscriptionService>();

            // RestaurantAccount
            kernel.Bind<IRestaurantAccountService>().To<RestaurantAccountService>();

            // Questions
            kernel.Bind<IQuestionService>().To<QuestionService>();

            // Emails
            kernel.Bind<IEmailSenderService>().To<SendGridEmailSenderService>();
            kernel.Bind<IScheduledEmailSenderService>().To<ScheduledEmailSenderService>();
            kernel.Bind<IScheduledOutgoingEmailService>().To<ScheduledOutgoingEmailService>();
            kernel.Bind<IEmailTemplateService>().To<EmailTemplateService>();

            // Logging
            kernel.Bind<ILogService>().ToMethod(GetLogService);

            // Cache
            kernel.Bind<ICacheService>().ToMethod(_ => new CacheService(new DataCache())).InSingletonScope();

            // LocalStorage
            kernel.Bind<ILocalStorageService>().To<LocalStorageService>().InSingletonScope();

            // QrCode
            kernel.Bind<IQrCodeService>().To<QrCodeService>().InSingletonScope();

            // DefaultData
            kernel.Bind<IDefaultDataService>().To<DefaultDataService>().InRequestScope();

            // FormSteps
            kernel.Bind<IFormStepService>().To<FormStepService>();
        }


        private static ILogService GetLogService(IContext context)
        {
            string logName = context.Request.Target != null ? context.Request.Target.Member.ReflectedType.Name : "MainLog";

            return new LogService(logName);
        }
    }
}