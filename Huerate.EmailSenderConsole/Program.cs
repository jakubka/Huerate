/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Threading.Tasks;
using Huerate.Logging;
using Huerate.Services;
using Huerate.Services.Email;

namespace EmailSenderConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            LogConfiguration.ConfigureLogging(AppenderEnum.Console | AppenderEnum.RollingFile);

            Console.WriteLine("Press enter to start sending emails..");
            Console.ReadLine();

            StartSending().Wait(); 

            Console.WriteLine("Press enter to quit");
            Console.ReadLine();
        }

        private static async Task StartSending()
        {
            var log = HuerateDependencyResolver.Get<ILogService>();
            var emailScheduler = HuerateDependencyResolver.Get<IScheduledEmailSenderService>();

            log.Info("Starting.");

            int emailsSent = await emailScheduler.StartSendingScheduledEmails();

            log.Info("Ended. " + emailsSent + " emails sent.");
        }
    }
}
