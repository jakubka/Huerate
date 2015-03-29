/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;

namespace Huerate.Configuration
{
    public static class HuerateConfiguration
    {
        public static T GetSetting<T>(string key)
        {
            var setting = GetSetting(key);

            if (setting == null)
            {
                return default(T);
            }

            return (T)Convert.ChangeType(setting, typeof(T));
        }

        public static string GetSetting(string key)
        {
            return CloudConfigurationManager.GetSetting(key);
        }
    }
}
