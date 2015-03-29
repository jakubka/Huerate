/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Huerate.Domain.Entities;
using Huerate.Services.Email;

namespace Huerate.EmailSchedulerConsole
{
/*
    public class EmailScheduler
    {
        public void PrepareScheduledEmails(string sourceConnectionString, IScheduledOutgoingEmailService scheduledOutgoingEmailService)
        {
            var restaurantContactContext = new RestaurantContactContext(sourceConnectionString);

            var restaurantContactsEmails = (from rc in restaurantContactContext.RestaurantContacts
                                            where rc.EmailAddress != null
                                            select rc.EmailAddress).Distinct().ToList();

            var outgoingEmailData = new OutgoingEmailData()
                                        {
                                            Body = File.ReadAllText("EmailBody.txt"),
                                            IsBodyHtml = true,
                                            Subject = "Prosba o spolupráci při tvorbě diplomové práce"
                                        };

            foreach (var restaurantContactEmail in restaurantContactsEmails)
            {
                try
                {
                    new System.Net.Mail.MailAddress(restaurantContactEmail);
                }
                catch
                {
                    Console.WriteLine("Email " + restaurantContactEmail + " is not valid");
                    continue;
                }


                scheduledOutgoingEmailService.Repository.Add(new ScheduledOutgoingEmail()
                                                                 {
                                                                     BodySubstitutions = new List<EmailBodySubstitution>()
                                                                                             {
                                                                                                 new EmailBodySubstitution()
                                                                                                     {
                                                                                                         Index = 0,
                                                                                                         Text = restaurantContactEmail
                                                                                                     }
                                                                                             },
                                                                     ReceiverAddress = restaurantContactEmail,
                                                                     OutgoingEmailData = outgoingEmailData,
                                                                 });
            }


            scheduledOutgoingEmailService.Repository.Add(new ScheduledOutgoingEmail()
                                                             {
                                                                 BodySubstitutions = new List<EmailBodySubstitution>()
                                                                                         {
                                                                                             new EmailBodySubstitution()
                                                                                                 {
                                                                                                     Index = 0,
                                                                                                     Text = "jakub.kadlubiec@gmail.com"
                                                                                                 }
                                                                                         },
                                                                 ReceiverAddress = "jakub.kadlubiec@gmail.com",
                                                                 OutgoingEmailData = outgoingEmailData,
                                                                 ScheduledSendDateTime = DateTime.UtcNow.AddHours(4),
                                                             });

            scheduledOutgoingEmailService.Save();

            Console.WriteLine(restaurantContactsEmails.Count);
        }

        public void ScheduleEmailsToBeSend(DateTime sendDateTime, int numberOfEmails, IScheduledOutgoingEmailService scheduledOutgoingEmailService)
        {
            scheduledOutgoingEmailService.ScheduleEmailsToBeSend(sendDateTime, numberOfEmails);
        }
    }
*/
}