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
using System.Threading;
using System.Web;
using Huerate.Logging;
using Huerate.Services;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace Huerate.WebUI
{
    public class HuerateWebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            var config = DiagnosticMonitor.GetDefaultInitialConfiguration();

            config.WindowsEventLog.ScheduledTransferPeriod = TimeSpan.FromMinutes(1);
            config.Directories.ScheduledTransferPeriod = TimeSpan.FromMinutes(5);


            DiagnosticMonitor.Start("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString", config);

            return base.OnStart();
        }
    }
}