/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Data.Services.Client;
using System.Linq;
using System.Threading.Tasks;
using Huerate.Configuration;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Table.DataServices;
using log4net.Appender;
using log4net.Core;

namespace Huerate.Logging
{
    internal class TableStorageAppender : AppenderSkeleton
    {
        private static bool tableCreated;

        private static void CreateTableIfNotExists(CloudTable cloudTable)
        {
            if (!tableCreated)
            {
                tableCreated = true;

                cloudTable.CreateIfNotExists();
            }
        }


        private TableServiceContext _dataContext;

        public TableStorageAppender()
        {
            ConnectionStringKey = "LogsStorageConnectionString";

            string instanceName = "Logs";

            if (RoleEnvironment.IsAvailable)
            {
                instanceName += RoleEnvironment.CurrentRoleInstance.Role.Name + RoleEnvironment.CurrentRoleInstance.Id;
                instanceName = string.Concat(instanceName.Where(char.IsLetterOrDigit));
            }
            else
            {
                instanceName += "Local";
            }

            TableName = instanceName.Substring(0, Math.Min(instanceName.Length, 63));
        }

        public string ConnectionStringKey { get; set; }

        public string TableName { get; set; }

        public override void ActivateOptions()
        {
            try
            {
                base.ActivateOptions();

                var connectionString = HuerateConfiguration.GetSetting(ConnectionStringKey);
                var account = CloudStorageAccount.Parse(connectionString);

                var tableClient = account.CreateCloudTableClient();
                CreateTableIfNotExists(tableClient.GetTableReference(TableName));

                _dataContext = tableClient.GetTableServiceContext();
            }
            catch (Exception e)
            {
                ErrorHandler.Error("Could not start TableStorageAppender log", e);
            }
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            try
            {
                if (_dataContext != null)
                {
                    var entity = new LogEventEntity
                                     {
                                         RoleInstance = RoleEnvironment.IsAvailable ? RoleEnvironment.CurrentRoleInstance.Id : "Not Azure",
                                         DeploymentId = RoleEnvironment.IsAvailable ? RoleEnvironment.DeploymentId : "Not Azure",
                                         Message = RenderLoggingEvent(loggingEvent),
                                         Level = loggingEvent.Level.Name,
                                         LoggerName = loggingEvent.LoggerName,
                                         Domain = loggingEvent.Domain,
                                         ThreadName = loggingEvent.ThreadName,
                                         Identity = loggingEvent.Identity
                                     };

                    _dataContext.AddObject(TableName, entity);
                    _dataContext.SaveChanges();

                    IAsyncResult result = _dataContext.BeginSaveChanges(null, null);

                    Task.Factory.FromAsync(result, ar => { _dataContext.EndSaveChanges(ar); });
                }
            }
            catch (Exception e)
            {
                ErrorHandler.Error("Could not write log entry", e);
            }
        }
    }
}