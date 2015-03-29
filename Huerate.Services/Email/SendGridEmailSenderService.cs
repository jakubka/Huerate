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
using System.Threading.Tasks;
using Huerate.Logging;
using Huerate.Services.Settings;
using SendGrid;
using System.Net.Mail;
using SendGrid.Transport;

namespace Huerate.Services.Email
{
    internal class SendGridEmailSenderService : EmailSenderServiceBase
    {
        public SendGridEmailSenderService(ILogService logService, ISettingsService settingsService, IEmailTemplateService emailTemplateService) : base(logService, settingsService, emailTemplateService)
        {
        }

        protected override Task<string> SendEmailInternalAsync(Email email)
        {
            var sendGridSettings = SettingsService.SendGridSettings;

            var message = Mail.GetInstance();
            message.AddTo(email.Receivers);
            message.From = new MailAddress(sendGridSettings.FromAddress, sendGridSettings.FromName);
            message.Subject = email.Subject;
            message.Html = email.HtmlBody;
            message.Text = email.PlainTextBody;


            var credentials = new NetworkCredential(sendGridSettings.Username, sendGridSettings.Password);

            var transportWeb = Web.GetInstance(credentials);

            transportWeb.Deliver(message);

            return Task.FromResult(sendGridSettings.FromAddress);
        }
    }
}