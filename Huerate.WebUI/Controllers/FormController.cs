/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Linq;
using System.Web.Mvc;
using Huerate.Domain.Entities;
using Huerate.Domain.Repositories;
using Huerate.Logging;
using Huerate.Services;
using Huerate.Services.Membership;
using Huerate.Services.Questions;
using Huerate.Services.RestaurantAccounts;
using Huerate.Services.Settings;
using Huerate.Services.Translations;
using Huerate.WebViewModels;
using Newtonsoft.Json;

namespace Huerate.WebUI.Controllers
{
    public class FormController : ControllerBase
    {
        private ILogService LogService { get; set; }
        private IRestaurantAccountService RestaurantAccountService { get; set; }

        public FormController(ILogService logService, IRestaurantAccountService restaurantAccountService, IMembershipService membershipService, ISettingsService settingsService, ITranslationService translationService) : base(membershipService, settingsService, translationService)
        {
            LogService = logService;
            RestaurantAccountService = restaurantAccountService;
        }

        public ActionResult Index(string codeName)
        {
            try
            {
                var model = RestaurantAccountService.GetRestaurantAccountFormModel(codeName);

                return View(model);
            }
            catch (RestaurantNotFoundServiceException e)
            {
                return new HttpNotFoundResult(e.Message);
            }
            catch (Exception e)
            {
                LogService.Error("Error on FormController.Index", e);

                return Content(e.Message);
            }
        }
    }
}