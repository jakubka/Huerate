/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Globalization;
using System.Web.Mvc;
using Huerate.Domain.Entities;
using Huerate.Services.Membership;
using Huerate.Services.Settings;
using Huerate.Services.Translations;
using Huerate.WebViewModels;

namespace Huerate.WebUI.Controllers
{
    public class AccountController : ControllerBase
    {
        public AccountController(IMembershipService membershipService, ISettingsService settingsService, ITranslationService translationService) : base(membershipService, settingsService, translationService)
        {
        }

        public ActionResult Logout(string redirect = null)
        {
            MembershipService.SignOut();

            if (redirect != null)
            {
                return Redirect(redirect);
            }
            
            return RedirectToAction("Index", "HomePage"); 
        }
    }
}
