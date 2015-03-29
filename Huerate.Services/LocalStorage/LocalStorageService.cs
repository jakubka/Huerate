/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using Microsoft.WindowsAzure.ServiceRuntime;

namespace Huerate.Services.LocalStorage
{
    internal class LocalStorageService : ILocalStorageService
    {
        public string GetLocalStoragePath(string localStorageName)
        {
            if (RoleEnvironment.IsAvailable)
            {
                return RoleEnvironment.GetLocalResource(localStorageName).RootPath;
            }
            return System.IO.Path.GetTempPath();
        }
    }
}