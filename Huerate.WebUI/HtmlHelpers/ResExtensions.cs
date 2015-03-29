/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Globalization;
using System.Web.Mvc;
using Huerate.Services;
using Huerate.Services.Settings;
using Huerate.Services.Translations;
using Ninject;

namespace Huerate.WebUI.HtmlHelpers
{
    public static class ResExtensions
    {
        private static readonly ITranslationService TranslationService = HuerateDependencyResolver.Get<ITranslationService>();
        private static readonly ISettingsService SettingsService = HuerateDependencyResolver.Get<ISettingsService>();

        public static string Tr(this HtmlHelper htmlHelper, string code)
        {
            return TranslationService.Translate(code, htmlHelper.ViewContext.ViewBag.CurrentLanguage ?? SettingsService.DefaultLanguage);
        }

        public static string TrFormat(this HtmlHelper htmlHelper, string code, params object[] formatArguments)
        {
            return string.Format(htmlHelper.Tr(code), formatArguments);
        }
    }
}