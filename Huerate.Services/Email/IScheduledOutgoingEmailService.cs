/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using Huerate.Domain.Entities;
using Huerate.Domain.Repositories;
using Huerate.Services;

namespace Huerate.Services.Email
{
    public interface IScheduledOutgoingEmailService : IDataService<IScheduledOutgoingEmailRepository, ScheduledOutgoingEmail, int>
    {
        void ScheduleEmailsToBeSend(DateTime sendDateTime, int numberOfEmails);

        void ScheduleEmailsToBeSend(DateTime sendDateTime, IEnumerable<string> emailAddresses);
    }
}