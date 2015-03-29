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
using System.Web;
using System.Web.Mvc;
using Huerate.Services.Settings;
using Huerate.Services.UrlService;

namespace Huerate.WebUI.Services
{
    public class UrlService : IUrlService
    {
        private UrlHelper UrlHelper { get; set; }
        private ISettingsService SettingsService { get; set; }

        public UrlService(UrlHelper urlHelper, ISettingsService settingsService)
        {
            UrlHelper = urlHelper;
            SettingsService = settingsService;
        }

        public string Action(string actionName, string controllerName, object routeValues = null, string protocol = null)
        {
            return UrlHelper.Action(actionName, controllerName, routeValues, protocol);
        }

        public string GetAbsolutePathToWebObject(string relativePath)
        {
            if (SettingsService.ContentFilesOnBlob)
            {
                relativePath = relativePath.Trim('~', '/');

                return SettingsService.ContentFilesBlobUri + relativePath;
            }
            else
            {
                return UrlHelper.Content(relativePath);
            }
        }
    }
}