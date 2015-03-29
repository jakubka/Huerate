/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Huerate.Domain;
using Huerate.Domain.Entities;
using Huerate.Domain.Repositories;
using Huerate.Logging;
using Huerate.Services.Settings;
using RazorEngine;
using RazorEngine.Templating;

namespace Huerate.Services.Email
{
    internal class EmailTemplateService : DataServiceBase<IEmailTemplateRepository, EmailTemplate, int>, IEmailTemplateService
    {
        public EmailTemplateService(IUnitOfWork unitOfWork, ILogService logService) : base(unitOfWork, logService)
        {
        }

        public Email GetEmailFromTemplate(string templateCodeName, string language, Dictionary<string, object> templateParameters = null)
        {
            var template = Repository.GetEmailTemplate(templateCodeName, language);

            if (template == null)
            {
                throw new ServiceException(string.Format("Email template with code name {0} and language {1} not found", templateCodeName, language));
            }

            return GetEmailFromTemplate(template, templateParameters);
        }

        public Email GetEmailFromTemplate(EmailTemplate emailTemplate, Dictionary<string, object> templateParameters = null)
        {
            DynamicViewBag viewBag = new DynamicViewBag();

            viewBag.AddDictionaryValues(templateParameters);

            var email = new Email()
            {
                Subject = !string.IsNullOrWhiteSpace(emailTemplate.SubjectTemplate) ? Razor.Parse(emailTemplate.SubjectTemplate, null, viewBag, null) : null,
                HtmlBody = !string.IsNullOrWhiteSpace(emailTemplate.HtmlBodyTemplate) ? Razor.Parse(emailTemplate.HtmlBodyTemplate, null, viewBag, null) : null,
                PlainTextBody = !string.IsNullOrWhiteSpace(emailTemplate.PlainTextBodyTemplate) ? Razor.Parse(emailTemplate.PlainTextBodyTemplate, null, viewBag, null) : null,
            };

            return email;
        }
    }
}