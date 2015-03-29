/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Globalization;
using System.Web.Mvc;
using Huerate.Domain;
using Huerate.Domain.Entities;
using Huerate.Services.Membership;
using Huerate.Services.Settings;
using Huerate.Services.Translations;
using System.Collections.Generic;

namespace Huerate.WebUI.Controllers
{
    public abstract class ControllerBase : Controller
    {
        #region "Fields"

        private readonly Lazy<string> _currentLanguageLazy;

        #endregion

        #region "Properties"

        protected ITranslationService TranslationService { get; private set; }

        protected IMembershipService MembershipService { get; private set; }

        protected ISettingsService SettingsService { get; private set; }

        protected RestaurantAccount CurrentUser
        {
            get { return MembershipService.CurrentRestaurantAccount; }
        }

        protected bool IsUserOnline
        {
            get { return MembershipService.IsRestaurantAccountOnline; }
        }

        protected string CurrentLanguage
        {
            get { return _currentLanguageLazy.Value; }
        }

        #endregion

        #region "Constructor"

        protected ControllerBase(IMembershipService membershipService, ISettingsService settingsService, ITranslationService translationService)
        {
            MembershipService = membershipService;
            SettingsService = settingsService;
            TranslationService = translationService;

            _currentLanguageLazy = new Lazy<string>(GetCurrentLanguage);
        }

        #endregion

        #region "Methods"

        protected string Tr(string resourceCodeName)
        {
            return TranslationService.Translate(resourceCodeName, CurrentLanguage);
        }

        protected string TrFormat(string resourceCodeName, params object[] formatArguments)
        {
            return TranslationService.TranslateFormat(resourceCodeName, CurrentLanguage, formatArguments);
        }

        protected void AddMessage(string message)
        {
            if (!TempData.ContainsKey("Messages"))
            {
                TempData["Messages"] = new List<string>();
            }

            ((List<string>)TempData["Messages"]).Add(message);
        }

        private string GetCurrentLanguage()
        {
            string lang = Request.QueryString["lang"];

            if (lang != null)
            {
                return lang;
            }

            if (HttpContext.Request.Url == null)
            {
                return SettingsService.DefaultLanguage;
            }

            string dnsSafeHost = HttpContext.Request.Url.DnsSafeHost;
            if (dnsSafeHost.EndsWith(".com"))
            {
                return "en";
            }
            if (dnsSafeHost.EndsWith(".cz"))
            {
                return "cs";
            }
            return SettingsService.DefaultLanguage;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.CurrentUser = CurrentUser;
            ViewBag.CurrentLanguage = CurrentLanguage;

            base.OnActionExecuting(filterContext);
        }



        #endregion
    }
}