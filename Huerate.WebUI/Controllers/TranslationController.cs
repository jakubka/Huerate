/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Mvc;
using Huerate.Domain.Entities;
using Huerate.Services.AnswerSets;
using Huerate.Services.Answers;
using Huerate.Services.Membership;
using Huerate.Services.Settings;
using Huerate.Services.Translations;
using Huerate.WebUI.Filters;
using Newtonsoft.Json;

namespace Huerate.WebUI.Controllers
{
    public class TranslationController : ControllerBase
    {
        public TranslationController(IMembershipService membershipService, ISettingsService settingsService, ITranslationService translationService) : base(membershipService, settingsService, translationService)
        {
        }

        public ContentResult GetAllScript(string language)
        {
            var translations = new Dictionary<string, IDictionary<string, string>>();

            foreach (var l in language.Split(';'))
            {
                translations[l] = TranslationService.GetAllDefaultResources(l);
            }

            return Content("var translations = " + JsonConvert.SerializeObject(translations), "application/json");
        }


        public ContentResult GetCustomTranslationsScript(int? restaurantAccountId = null)
        {
            var translations = TranslationService.GetAllCustomResources(restaurantAccountId ?? CurrentUser.Id);

            return Content("var customTranslations = " + JsonConvert.SerializeObject(translations), "application/json");
        }

        public JsonResult GetCustomTranslations()
        {
            return Json(TranslationService.GetAllCustomResources(CurrentUser.Id), JsonRequestBehavior.AllowGet);
        }

        [MembershipAuthorizeFilter]
        public void SetTranslation(string codeName, string value, string language)
        {
            TranslationService.CreateOrUpdateCustomTranslation(codeName, value, language, CurrentUser);
        }
    }
}