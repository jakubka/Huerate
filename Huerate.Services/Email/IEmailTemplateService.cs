/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Huerate.Domain.Entities;
using Huerate.Domain.Repositories;

namespace Huerate.Services.Email
{
    public interface IEmailTemplateService : IDataService<IEmailTemplateRepository, EmailTemplate, int>
    {
        Email GetEmailFromTemplate(string templateCodeName, string language, Dictionary<string, object> templateParameters = null);

        Email GetEmailFromTemplate(EmailTemplate emailTemplate, Dictionary<string, object> templateParameters = null);
    }
}