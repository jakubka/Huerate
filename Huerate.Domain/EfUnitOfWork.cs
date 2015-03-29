/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Data.Entity.Validation;
using System.Linq;
using Huerate.Domain.Entities;
using Huerate.Domain.Repositories;
using Huerate.Logging;

namespace Huerate.Domain
{
    public class EfUnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ILogService _logService;
        private readonly HuerateContext _context;
        private IQuestionRepository _questionRepository;
        private IAnswerRepository _answerRepository;
        private IRestaurantAccountRepository _restaurantAccountRepository;
        private IAnswerSetRepository _answerSetRepository;
        private IRepository<NewsletterSubscription, int> _newsletterSubscriptionRepository;
        private IRepository<Activity, int> _activityRepository;
        private IRepository<ActivityType, ActivityTypeEnum> _activityTypeRepository;
        private IRepository<Contact, int> _contactRepository;
        private IScheduledOutgoingEmailRepository _scheduledOutgoingEmailRepository;
        private IEmailTemplateRepository _emailTemplateRepository;
        private IFormStepRepository _formStepRepository;
        private ICustomTranslationRepository _customTranslationRepository;



        public EfUnitOfWork(ILogService logService)
        {
            _logService = logService;
            _context = new HuerateContextFactory().Create();
        }

        public EfUnitOfWork(ILogService logService, string connectionString)
        {
            _logService = logService;
            _context = new HuerateContextFactory(connectionString).Create();
        }

        public IQuestionRepository QuestionRepository
        {
            get { return _questionRepository ?? (_questionRepository = new QuestionEfRepository(_context)); }
        }

        public IAnswerRepository AnswerRepository
        {
            get { return _answerRepository ?? (_answerRepository = new AnswerEfRepository(_context)); }
        }

        public IRestaurantAccountRepository RestaurantAccountRepository
        {
            get { return _restaurantAccountRepository ?? (_restaurantAccountRepository = new RestaurantAccountEfRepository(_context)); }
        }

        public IAnswerSetRepository AnswerSetRepository
        {
            get { return _answerSetRepository ?? (_answerSetRepository = new AnswerSetEfRepository(_context)); }
        }

        public IRepository<NewsletterSubscription, int> NewsletterSubscriptionRepository
        {
            get { return _newsletterSubscriptionRepository ?? (_newsletterSubscriptionRepository = new IntKeyEfRepository<NewsletterSubscription>(_context)); }
        }

        public IRepository<Activity, int> ActivityRepository
        {
            get { return _activityRepository ?? (_activityRepository = new IntKeyEfRepository<Activity>(_context)); }
        }

        public IRepository<ActivityType, ActivityTypeEnum> ActivityTypeRepository
        {
            get { return _activityTypeRepository ?? (_activityTypeRepository = new ActivityTypeEfRepository(_context)); }
        }

        public IRepository<Contact, int> ContactRepository
        {
            get { return _contactRepository ?? (_contactRepository = new IntKeyEfRepository<Contact>(_context)); }
        }

        public IEmailTemplateRepository EmailTemplateRepository
        {
            get { return _emailTemplateRepository ?? (_emailTemplateRepository = new EmailTemplateRepository(_context)); }
        }

        public IScheduledOutgoingEmailRepository ScheduledOutgoingEmailRepository
        {
            get { return _scheduledOutgoingEmailRepository ?? (_scheduledOutgoingEmailRepository = new ScheduledOutgoingEmailEfRepository(_context)); }
        }

        public IFormStepRepository FormStepRepository
        {
            get { return _formStepRepository ?? (_formStepRepository = new FormStepRepository(_context)); }
        }

        public ICustomTranslationRepository CustomTranslationRepository
        {
            get { return _customTranslationRepository ?? (_customTranslationRepository = new CustomTranslationEfRepository(_context)); }
        }

        public TRepository GetRepositoryForEntity<TRepository, TEntity, TKey>() where TRepository : IRepository<TEntity, TKey> where TEntity : IPrimaryKeyEntity<TKey>
        {
            return (from propertyInfo in GetType().GetProperties()
                    where propertyInfo.CanRead
                    let getMethod = propertyInfo.GetMethod
                    where typeof(TRepository).IsAssignableFrom(getMethod.ReturnType)
                    select (TRepository)getMethod.Invoke(this, null)).FirstOrDefault();
        }

        public IRepository<TEntity, TKey> GetRepositoryForEntity<TEntity, TKey>() where TEntity : IPrimaryKeyEntity<TKey>
        {
            return GetRepositoryForEntity<IRepository<TEntity, TKey>, TEntity, TKey>();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        ~EfUnitOfWork()
        {
            Dispose();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}