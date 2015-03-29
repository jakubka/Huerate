/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using Huerate.Domain.Entities;
using Huerate.Domain.Repositories;

namespace Huerate.Domain
{
    public interface IUnitOfWork
    {
        IQuestionRepository QuestionRepository { get; }

        IAnswerRepository AnswerRepository { get; }

        IRestaurantAccountRepository RestaurantAccountRepository { get; }

        IAnswerSetRepository AnswerSetRepository { get; }

        IRepository<NewsletterSubscription, int> NewsletterSubscriptionRepository { get; }

        IRepository<Activity, int> ActivityRepository { get; }

        IRepository<ActivityType, ActivityTypeEnum> ActivityTypeRepository { get; }

        IRepository<Contact, int> ContactRepository { get; }

        IEmailTemplateRepository EmailTemplateRepository { get; }

        IScheduledOutgoingEmailRepository ScheduledOutgoingEmailRepository { get; }

        IFormStepRepository FormStepRepository { get; }

        ICustomTranslationRepository CustomTranslationRepository { get; }

        TRepository GetRepositoryForEntity<TRepository, TEntity, TKey>()
            where TEntity : IPrimaryKeyEntity<TKey>
            where TRepository : IRepository<TEntity, TKey>;

        IRepository<TEntity, TKey> GetRepositoryForEntity<TEntity, TKey>()
            where TEntity : IPrimaryKeyEntity<TKey>;

        int Save();
    }
}