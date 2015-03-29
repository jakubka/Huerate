/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Huerate.Domain;
using Huerate.Domain.Entities;
using Huerate.Domain.Repositories;
using Huerate.Logging;
using Huerate.Services;
using Huerate.Services.Email;

namespace Huerate.ConsoleTest
{
    public class Program
    {
        private static void Main(string[] args)
        {
            LogConfiguration.ConfigureLogging(AppenderEnum.Console | AppenderEnum.RollingFile);

            var repository = HuerateDependencyResolver.Get<IUnitOfWork>();
            var logger = HuerateDependencyResolver.Get<ILogService>();

            logger.Debug("Start");

            var steps = repository.FormStepRepository.Fetch().Where(s => !(s is YesNoQuestionsFormStep));

           foreach (var step in steps)
            {
                if (step is YesNoQuestionsFormStep)
                {
                    Console.WriteLine((step as YesNoQuestionsFormStep).YesNoQuestions.Count);
                }
            }

            //var emailSender = HuerateDependencyResolver.Get<IEmailSenderService>();

            //var templateService = HuerateDependencyResolver.Get<IEmailTemplateService>();

            //var dict = new Dictionary<string, object>
            //    {
            //        {"ReceiverEmailAddress", "JO TO JE"}
            //    };

            //var template = templateService.GetEmailFromTemplate("Invitation", null, dict);
            

            //Email email = new Email()
            //                  {
            //                      HtmlBody = "HTML",
            //                      Subject = "SUBJ",
            //                      PlainTextBody = "PLAIN",
            //                      Receivers = new string[] {"jakub.kadlubiec@gmail.com"},
            //                  };

            //emailSender.SendEmailAsync(email).Wait();

            //var mails = (from scheduledEmail in repository.ScheduledOutgoingEmailRepository.Fetch()
            //             where scheduledEmail.SendDateTime == null
            //             where scheduledEmail.ScheduledSendDateTime != null
            //             select scheduledEmail)
            //             .AsEnumerable()
            //             .Where(m => string.Compare(m.ReceiverAddress, "hradni@vinarna-spilberk.wz.cz", true, new CultureInfo("cs")) <= 0)
            //             .ToList();

            //logger.Info(mails.Count + " mails found");

            //foreach (var scheduledOutgoingEmail in mails)
            //{
            //    logger.Info(scheduledOutgoingEmail.ReceiverAddress);
            //    scheduledOutgoingEmail.SendDateTime = scheduledOutgoingEmail.ScheduledSendDateTime.Value.AddMinutes(30);
            //}

            //repository.Save();


            //var questions = repository.QuestionRepository.Fetch().Where(q => q.Text == "Nebyla hudba příliš hlasitá?").ToList();

            //foreach (var source in questions)
            //{
            //    source.Text = "Byla hudba příliš hlasitá?";
            //}

            //repository.Save();


            
            logger.Debug("End");
            Console.ReadLine();
        }
    }
}