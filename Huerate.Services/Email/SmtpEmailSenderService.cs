/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Huerate.Logging;
using Huerate.Services.Settings;

namespace Huerate.Services.Email
{
    internal class SmtpEmailSenderService : EmailSenderServiceBase
    {
        public SmtpEmailSenderService(ILogService logService, ISettingsService settingsService, IEmailTemplateService emailTemplateService) : base(logService, settingsService, emailTemplateService)
        {
        }

        protected override async Task<string> SendEmailInternalAsync(Email email)
        {
            SmtpSettings smtpSettings = SettingsService.SmtpSettings;
            try
            {
                using (SmtpClient smtpClient = GetSmtpClient(smtpSettings))
                {
                    using (var emailMessage = GetEmailMessage(email, smtpSettings.FromAddress))
                    {
                        await smtpClient.SendMailAsync(emailMessage).ConfigureAwait(false);

                        LogService.DebugExecution("Email sent to " + string.Join(", ", email.Receivers));

                        return smtpSettings.FromAddress;
                    }
                }
            }
            catch (Exception e)
            {
                throw new ServiceException("Error while sending email from " + smtpSettings.FromAddress, e);
            }
        }

        private MailMessage GetEmailMessage(Email email, string fromAddress)
        {
            var mailMessage = new MailMessage()
                                   {
                                       From = new MailAddress(fromAddress),
                                       Subject = email.Subject,
                                   };

            if (email.HtmlBody != null)
            {
                mailMessage.Body = email.HtmlBody;
                mailMessage.IsBodyHtml = true;
            }
            else
            {
                mailMessage.Body = email.PlainTextBody;
                mailMessage.IsBodyHtml = false;
            }

            foreach (var receiver in email.Receivers)
            {
                mailMessage.To.Add(receiver);
            }

            return mailMessage;
        }

        private static SmtpClient GetSmtpClient(SmtpSettings smtpSettings)
        {
            return new SmtpClient(smtpSettings.ServerAddress)
                       {
                           EnableSsl = smtpSettings.EnableSsl,
                           Port = smtpSettings.ServerPort,
                           UseDefaultCredentials = false,
                           Credentials = new NetworkCredential(smtpSettings.Username, smtpSettings.Password)
                       };
        }

    }
}