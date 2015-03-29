/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Globalization;
using Microsoft.WindowsAzure.Storage.Table.DataServices;

namespace Huerate.Logging
{
    internal sealed class LogEventEntity : TableServiceEntity
    {
        public LogEventEntity()
        {
            var now = DateTime.UtcNow;

            // Hours are partitions
            PartitionKey = ((int)(DateTime.MaxValue - now).TotalHours).ToString(CultureInfo.InvariantCulture);
            RowKey = ((DateTime.MaxValue - now).Ticks%TimeSpan.TicksPerHour).ToString(CultureInfo.InvariantCulture);

            LogTime = now.ToString(CultureInfo.InvariantCulture);
        }

        public string LogTime { get; set; }
        public string Identity { get; set; }
        public string ThreadName { get; set; }
        public string LoggerName { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Domain { get; set; }
        public string RoleInstance { get; set; }
        public string DeploymentId { get; set; }
    }
}