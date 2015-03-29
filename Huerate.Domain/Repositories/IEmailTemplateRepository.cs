/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Huerate.Domain.Entities;

namespace Huerate.Domain.Repositories
{
    public interface IEmailTemplateRepository : IRepository<EmailTemplate, int>
    {
        EmailTemplate GetEmailTemplate(string templateCodeName, string language);
    }
}
