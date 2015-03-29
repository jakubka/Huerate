/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Huerate.Logging;
using Huerate.Services;
using Huerate.Services.Email;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

namespace EmailSenderWorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        private IScheduledEmailSenderService _scheduledEmailSenderService;
        private ILogService _log;

        public override bool OnStart()
        {
            LogConfiguration.ConfigureLogging(AppenderEnum.TableStorage);

            _log = HuerateDependencyResolver.Get<ILogService>();

            try
            {
                _scheduledEmailSenderService = HuerateDependencyResolver.Get<IScheduledEmailSenderService>();

                return base.OnStart();
            }
            catch (Exception e)
            {
                _log.Error("Error in OnStart", e);

                return false;
            }
        }

        public override void Run()
        {
            _log.Info("Starting worker role for sending emails");

            try
            {
                var task = _scheduledEmailSenderService.StartSendingScheduledEmails();

                task.Wait();

                _log.Info(task.Result + " emails sent");
            }
            catch (Exception e)
            {
                _log.Error("Error while sending emails", e);
            }

            _log.Info("Worker role for sending emails ended (Method Run)");
        }

        public override void OnStop()
        {
            _log.Info("OnStop called, aborting sending");
            _scheduledEmailSenderService.AbortSending();

            Thread.Sleep(TimeSpan.FromMinutes(1));

            _log.Info("OnStop ending");

            base.OnStop();
        }
    }
}
