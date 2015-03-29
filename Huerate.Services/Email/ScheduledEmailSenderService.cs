/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Huerate.Domain;
using Huerate.Domain.Entities;
using Huerate.Logging;

namespace Huerate.Services.Email
{
    internal class ScheduledEmailSenderService : ServiceBase, IScheduledEmailSenderService
    {
        private IEmailSenderService EmailSenderService { get; set; }
        private IEmailTemplateService EmailTemplateService { get; set; }
        private bool _sendingAborted;

        public ScheduledEmailSenderService(IEmailSenderService emailSenderService, IEmailTemplateService emailTemplateService, IUnitOfWork unitOfWork, ILogService logService)
            : base(unitOfWork, logService)
        {
            EmailSenderService = emailSenderService;
            EmailTemplateService = emailTemplateService;
        }

        public void AbortSending()
        {
            _sendingAborted = true;
        }

        public async Task<int> StartSendingScheduledEmails()
        {
            int totalEmailsSent = 0;
            _sendingAborted = false;

            while (!_sendingAborted)
            {
                var emailsToSend = UnitOfWork.ScheduledOutgoingEmailRepository.GetNotSentScheduledEmails(DateTime.UtcNow, 30);

                if (emailsToSend.Count == 0)
                {
                    LogService.DebugExecution("Nothing to send now, waiting 5 minutes");

                    await Task.Delay(TimeSpan.FromMinutes(5));
                }
                else
                {
                    LogService.DebugExecution("Will send " + emailsToSend.Count + " emails");

                    foreach (var scheduledOutgoingEmail in emailsToSend)
                    {
                        if (_sendingAborted)
                        {
                            break;
                        }

                        LogService.DebugExecution("Sending email to " + scheduledOutgoingEmail.ReceiverAddress);
                        await SendEmail(scheduledOutgoingEmail);
                        LogService.Debug("Will wait 20 seconds before sending another one");
                        await Task.Delay(TimeSpan.FromSeconds(20));
                    }

                    LogService.DebugExecution(emailsToSend.Count + " emails sent");
                    totalEmailsSent += emailsToSend.Count;
                }
            }

            LogService.Info("Sending emails aborted. Returning");

            return totalEmailsSent;
        }

        private async Task SendEmail(ScheduledOutgoingEmail scheduledOutgoingEmail)
        {
            try
            {
                var email = GetEmail(scheduledOutgoingEmail);

                string senderEmail = await EmailSenderService.SendEmailAsync(email);

                scheduledOutgoingEmail.SentFromAddress = senderEmail;
                scheduledOutgoingEmail.SendDateTime = DateTime.UtcNow;

                LogService.DebugExecution("Email to " + scheduledOutgoingEmail.ReceiverAddress + " successfully sent");
            }
            catch (ServiceException e)
            {
                LogService.Error("Error while sending email to " + scheduledOutgoingEmail.ReceiverAddress, e);
                scheduledOutgoingEmail.SentFromAddress = "ERROR";
            }

            UnitOfWork.Save();
        }

        private Email GetEmail(ScheduledOutgoingEmail scheduledOutgoingEmail)
        {
            var templateParameters = new Dictionary<string, object>
                                         {
                                             {"ReceiverEmailAddress", scheduledOutgoingEmail.ReceiverAddress}
                                         };

            var email = EmailTemplateService.GetEmailFromTemplate(scheduledOutgoingEmail.EmailTemplate, templateParameters);

            email.Receivers = new[] {scheduledOutgoingEmail.ReceiverAddress};

            return email;
        }
    }
}