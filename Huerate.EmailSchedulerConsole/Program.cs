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
using Huerate.Domain;
using Huerate.Domain.Entities;
using Huerate.Services;
using Huerate.Services.Email;
using Huerate.Services.RestaurantAccounts;

namespace Huerate.EmailSchedulerConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Press enter to schedule emails");
            Console.ReadLine();

            HuerateDependencyResolver.SetSpecificImplementation<IUnitOfWork>(new EfUnitOfWork(null));
            var unitOfWork = HuerateDependencyResolver.Get<IUnitOfWork>();
            var scheduledOutgoingEmailService = HuerateDependencyResolver.Get<IScheduledOutgoingEmailService>();

            //var emailTemplateService = HuerateDependencyResolver.Get<IEmailTemplateService>();

            ////var restaurantService = HuerateDependencyResolver.Get<IRestaurantAccountService>();

            //var emailTemplate = emailTemplateService.Repository.Fetch().Single(et => et.CodeName == "NewFeatures1");

            ////foreach (var email in new[] {"jakub.kadlubiec@gmail.com"})
            //foreach (var email in unitOfWork.RestaurantAccountRepository.GetAll().Select(ra => ra.Email))
            //{
            //    scheduledOutgoingEmailService.Create(new ScheduledOutgoingEmail()
            //                                             {
            //                                                 ReceiverAddress = email,
            //                                                 ScheduledSendDateTime = DateTime.UtcNow,
            //                                                 EmailTemplate = emailTemplate,
            //                                             });
            //    Console.WriteLine("Scheduled email " + email);
            //}

            //Console.WriteLine("End");
            //Console.ReadLine();

            //return;


            Console.WriteLine("Scheduling");

            var now = DateTime.UtcNow;

            var scheduledTime = new DateTime(now.Year, now.Month, now.Day, 8, 30, 0);

            scheduledTime = scheduledTime.ToUniversalTime();

            if (scheduledTime < now)
            {
                scheduledTime = scheduledTime.AddDays(1);
            }

            Console.WriteLine("Schedule time local: {0} and utc: {1}", scheduledTime.ToLocalTime(), scheduledTime);

            Console.WriteLine("Unscheduled emails: {0}", unitOfWork.ScheduledOutgoingEmailRepository.Fetch().Count(e => e.ScheduledSendDateTime == null));
            Console.WriteLine("Scheduled emails: {0}", unitOfWork.ScheduledOutgoingEmailRepository.Fetch().Count(e => e.ScheduledSendDateTime != null && e.SendDateTime == null));
            Console.WriteLine("Sent emails: {0}", unitOfWork.ScheduledOutgoingEmailRepository.Fetch().Count(e => e.SendDateTime != null));


            try
            {
                Console.WriteLine("How many emails to schedule:");
                int emailsToSchedule = int.Parse(Console.ReadLine());

                Console.WriteLine("Scheduling {0} emails to be send at {1} local time", emailsToSchedule, scheduledTime.ToLocalTime().ToLongTimeString());

                scheduledOutgoingEmailService.ScheduleEmailsToBeSend(scheduledTime, emailsToSchedule);
                scheduledOutgoingEmailService.ScheduleEmailsToBeSend(DateTime.UtcNow.AddHours(3), new[] {"jakub.kadlubiec@gmail.com"});
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("All ready");
            Console.ReadLine();
        }
    }
}