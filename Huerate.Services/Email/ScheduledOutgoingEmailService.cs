/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Huerate.Domain;
using Huerate.Domain.Entities;
using Huerate.Domain.Repositories;
using Huerate.Logging;

namespace Huerate.Services.Email
{
    internal class ScheduledOutgoingEmailService : DataServiceBase<IScheduledOutgoingEmailRepository, ScheduledOutgoingEmail, int>, IScheduledOutgoingEmailService
    {
        public ScheduledOutgoingEmailService(IUnitOfWork unitOfWork, ILogService logService) : base(unitOfWork, logService)
        {
        }


        public void ScheduleEmailsToBeSend(DateTime sendDateTime, int numberOfEmails)
        {
            var emails = Repository.GetRandomNonScheduledEmails(numberOfEmails);

            emails.ForEach(e => e.ScheduledSendDateTime = sendDateTime);

            Save();
        }

        public void ScheduleEmailsToBeSend(DateTime sendDateTime, IEnumerable<string> emailAddresses)
        {
            var emails = Repository.Fetch().Where(e => emailAddresses.Contains(e.ReceiverAddress)).ToList();

            emails.ForEach(e =>
                               {
                                   e.ScheduledSendDateTime = sendDateTime;
                                   e.SendDateTime = null;
                               });

            Save();
        }
    }
}
