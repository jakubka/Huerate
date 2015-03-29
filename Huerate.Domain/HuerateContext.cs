/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using Huerate.Configuration;
using Huerate.Domain.Entities;
using Huerate.Logging;

namespace Huerate.Domain
{
    internal class HuerateContextFactory : IDbContextFactory<HuerateContext>
    {
        public string ConnectionString { get; set; }

        public HuerateContextFactory()
        {
        }

        public HuerateContextFactory(string connectionString = null)
        {
            ConnectionString = connectionString;
        }

        public HuerateContext Create()
        {
            return new HuerateContext(new LogService("HuerateContext"), ConnectionString ?? HuerateConfiguration.GetSetting("HuerateContext"));
        }
    }


    internal class HuerateContext : DbContext
    {
        private readonly ILogService _logService;

        static HuerateContext()
        {
            Database.SetInitializer(new HuerateDatabaseInitializer());
        }

        public HuerateContext(ILogService logService, string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            _logService = logService;
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerSet> AnswerSets { get; set; }
        public DbSet<NewsletterSubscription> NewsletterSubscriptions { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<RestaurantAccount> RestaurantAccounts { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ScheduledOutgoingEmail> ScheduledOutgoingEmails { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<FormStep> FormSteps { get; set; }
        public DbSet<CustomTranslation> CustomTranslations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //modelBuilder.Entity<YesNoQuestion>()
            //            .HasRequired(q => q.Owner)
            //            .WithMany(r => r.)
            //            .HasForeignKey(q => q.OwnerId);
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                if (_logService != null)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        _logService.WarnFormat(@"Entity of type ""{0}"" in state ""{1}"" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            _logService.WarnFormat(@"- Property: ""{0}"", Error: ""{1}""", ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                }

                throw new DomainException("Validation failed", e);
            }
        }
    }
}