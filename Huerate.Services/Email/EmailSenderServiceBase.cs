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
using Huerate.Domain;
using Huerate.Logging;
using Huerate.Services.Settings;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace Huerate.Services.Email
{
    internal abstract class EmailSenderServiceBase : IEmailSenderService
    {
        protected ILogService LogService { get; set; }
        protected ISettingsService SettingsService { get; set; }
        protected IEmailTemplateService EmailTemplateService { get; set; }

        protected EmailSenderServiceBase(ILogService logService, ISettingsService settingsService, IEmailTemplateService emailTemplateService)
        {
            LogService = logService;
            SettingsService = settingsService;
            EmailTemplateService = emailTemplateService;
        }

        public async Task<string> SendEmailAsync(Email email)
        {
            if (!SettingsService.SendEmails)
            {
                string logPattern = @"
Debug send email:
Receivers: {0}
Subject: {1}
Body: {2}";
                LogService.InfoFormat(logPattern, string.Join(", ", email.Receivers), email.Subject, email.HtmlBody ?? email.PlainTextBody);

                return null;
            }

            return await SendEmailInternalAsync(email);
        }

        protected abstract Task<string> SendEmailInternalAsync(Email email);

        public async Task SendErrorEmailAsync(string message, Exception exception = null)
        {
            var parameters = new Dictionary<string, object>
                                 {
                                     {"Message", message},
                                     {"Exception", exception},
                                     {"Deployment", RoleEnvironment.IsAvailable ? RoleEnvironment.DeploymentId : "Not Azure"},
                                     {"Instance", RoleEnvironment.IsAvailable ? RoleEnvironment.CurrentRoleInstance.Id : "Not Azure"},
                                 };

            await SendEmailFromTemplateAsync("Error", null, SettingsService.MonitoringEmailAddresses, parameters);
        }

        public async Task SendEmailFromTemplateAsync(string templateCodeName, string language, IList<string> receivers, Dictionary<string, object> templateParameters = null)
        {
            var email = EmailTemplateService.GetEmailFromTemplate(templateCodeName, language, templateParameters);

            email.Receivers = receivers;

            await SendEmailAsync(email).ConfigureAwait(false);
        }
    }
}