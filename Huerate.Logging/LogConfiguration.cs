/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace Huerate.Logging
{
    public static class LogConfiguration
    {
        private static PatternLayout GetPaternLayout(string conversionPattern = "%d [%t] %-5p %c %m%n")
        {
            PatternLayout patternLayout = new PatternLayout
                                              {
                                                  ConversionPattern = conversionPattern
                                              };

            patternLayout.ActivateOptions();

            return patternLayout;
        }

        private static IAppender GetConsoleAppender()
        {
            return new ConsoleAppender()
                       {
                           Layout = GetPaternLayout(),
                           Target = "Console.Out",
                           Name = "ConsoleAppender",
                       };
        }

        private static IAppender GetRollingFileAppender()
        {
            RollingFileAppender rollingFileAppender = new RollingFileAppender
                                                          {
                                                              Layout = GetPaternLayout(),
                                                              AppendToFile = true,
                                                              RollingStyle = RollingFileAppender.RollingMode.Size,
                                                              MaxSizeRollBackups = 4,
                                                              MaximumFileSize = "1MB",
                                                              StaticLogFileName = true,
                                                              File = "log.txt",
                                                              ImmediateFlush = true,
                                                              Name = "RollingFileAppender",
                                                          };
            rollingFileAppender.ActivateOptions();

            return rollingFileAppender;
        }

        private static IAppender GetTableStorageAppender()
        {
            var tableStorageAppender = new TableStorageAppender()
                                           {
                                               Layout = GetPaternLayout("%message%newline"), 
                                               Name = "TableStorageAppender",
                                           };

            tableStorageAppender.ActivateOptions();

            return tableStorageAppender;
        }

        public static void ConfigureLogging(AppenderEnum appenders)
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            hierarchy.ResetConfiguration();

            if (appenders.HasFlag(AppenderEnum.Console))
            {
                hierarchy.Root.AddAppender(GetConsoleAppender());
            }
            if (appenders.HasFlag(AppenderEnum.RollingFile))
            {
                hierarchy.Root.AddAppender(GetRollingFileAppender());
            }
            if (appenders.HasFlag(AppenderEnum.TableStorage))
            {
                hierarchy.Root.AddAppender(GetTableStorageAppender());
            }

            hierarchy.Root.Level = Level.All;
            hierarchy.Configured = true;
        }
    }
}