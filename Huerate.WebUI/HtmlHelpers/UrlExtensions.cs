/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Web.Mvc;
using Huerate.Services;
using Huerate.Services.Settings;
using Huerate.Services.Translations;
using Huerate.Services.UrlService;

namespace Huerate.WebUI.HtmlHelpers
{
    public static class UrlExtensions
    {
        public static string WebObjectPath(this UrlHelper urlHelper, string relativePath)
        {
            return HuerateDependencyResolver.Get<IUrlService>().GetAbsolutePathToWebObject(relativePath);
        }
    }
}