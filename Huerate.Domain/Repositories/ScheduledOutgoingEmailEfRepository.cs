/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Huerate.Domain.Entities;

namespace Huerate.Domain.Repositories
{
    internal class ScheduledOutgoingEmailEfRepository : IntKeyEfRepository<ScheduledOutgoingEmail>, IScheduledOutgoingEmailRepository
    {
        public ScheduledOutgoingEmailEfRepository(HuerateContext context) : base(context)
        {
        }

        public override IQueryable<ScheduledOutgoingEmail> Fetch()
        {
            return DbSet.Include(scheduledEmail => scheduledEmail.EmailTemplate);
        }

        public List<ScheduledOutgoingEmail> GetNotSentScheduledEmails(DateTime scheduledBefore, int maxCount)
        {
            return (from scheduledEmail in Fetch()
                    where scheduledEmail.SendDateTime == null
                          && scheduledEmail.ScheduledSendDateTime != null
                          && scheduledEmail.ScheduledSendDateTime < scheduledBefore
                    orderby scheduledEmail.ScheduledSendDateTime, scheduledEmail.Id
                    select scheduledEmail
                    ).Take(maxCount).ToList();
        }

        public List<ScheduledOutgoingEmail> GetRandomNonScheduledEmails(int count)
        {
            return (from email in Fetch()
                    where email.ScheduledSendDateTime == null && email.SendDateTime == null
                    orderby Guid.NewGuid()
                    select email).Take(count).ToList();
        }
    }
}