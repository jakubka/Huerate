/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Web.Mvc;
using Huerate.Domain.Entities;
using Huerate.Services;
using Huerate.Services.Contacts;
using Huerate.Services.Membership;
using Huerate.Services.NewsletterSubscriptions;
using Huerate.Services.RestaurantAccounts;
using Huerate.Services.Settings;
using Huerate.Services.Translations;
using Huerate.WebViewModels;

namespace Huerate.WebUI.Controllers
{
    public class HomePageController : ControllerBase
    {
        #region "Preamble"

        private INewsletterSubscriptionService NewsletterSubscriptionService { get; set; }
        private IRestaurantAccountService RestaurantAccountService { get; set; }
        private IContactService ContactService { get; set; }

        public HomePageController(INewsletterSubscriptionService newsletterSubscriptionService, IRestaurantAccountService restaurantAccountService, IContactService contactService, IMembershipService membershipService, ISettingsService settingsService, ITranslationService translationService) : base(membershipService, settingsService, translationService)
        {
            NewsletterSubscriptionService = newsletterSubscriptionService;
            RestaurantAccountService = restaurantAccountService;
            ContactService = contactService;
        }

        #endregion


        #region "Index"

        public ViewResult Index()
        {
            return View();
        }

        public ActionResult NewsletterSubscribe(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return RedirectToAction("Index", new { saved = 0 });
            }

            NewsletterSubscriptionService.Create(new NewsletterSubscription()
                                                     {
                                                         Created = DateTime.UtcNow,
                                                         Email = email,
                                                         IPAddress = HttpContext.Request.UserHostAddress
                                                     });

            ContactService.TrackActivity(ActivityTypeEnum.NewsletterSubscribe, email);

            return RedirectToAction("Index", new {saved = 1});
        }

        #endregion


        #region "SignIn"

        public ViewResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(SignInModel model)
        {
            if (ModelState.IsValid)
            {
                RestaurantAccount user;

                if (RestaurantAccountService.ValidateRestaurantAccount(model.CodeNameOrEmail, model.Password, out user))
                {
                    MembershipService.SignIn(user, model.RememberMe);

                    ContactService.TrackActivity(ActivityTypeEnum.SignIn, model.CodeNameOrEmail);

                    return RedirectToAction("Index", "Admin");
                }

                ModelState.AddModelError("", Tr("Home_IncorrectEmailOrPassword"));
            }

            ContactService.TrackActivity(ActivityTypeEnum.SignInFail, model.CodeNameOrEmail);

            return View(model);
        }

        #endregion


        #region "SignUp"

        public ViewResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var restaurantAccount = RestaurantAccountService.CreateRestaurantAccount(model.DisplayName, model.Email, model.Password, CurrentLanguage);

                    MembershipService.SignIn(restaurantAccount, false);

                    ContactService.TrackActivity(ActivityTypeEnum.SignUp, model.DisplayName);

                    return RedirectToAction("Index", "Admin");
                }
                catch (ServiceException mEx)
                {
                    ModelState.AddModelError("", mEx.Message);
                }
            }

            ContactService.TrackActivity(ActivityTypeEnum.SignUpFail, model.DisplayName, model.Email);

            return View(model);
        }

        #endregion


        #region "Error"

        public ViewResult Error(string errorDescription)
        {
            return View(new ErrorModel {ErrorDescription = errorDescription});
        }

        #endregion


        #region "Help"

        public ViewResult Help()
        {
            return View();
        }

        #endregion


        #region "LostPassword"

        public ActionResult LostPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LostPassword(LostPasswordModel lostPasswordModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    RestaurantAccountService.ChangePasswordToGenerated(lostPasswordModel.EmailAddress);

                    ContactService.TrackActivity(ActivityTypeEnum.LostPasswordRecovery, lostPasswordModel.EmailAddress);

                    lostPasswordModel.SuccessMessage = TrFormat("LostPassword_Success", lostPasswordModel.EmailAddress);

                    return View(lostPasswordModel);
                }
                catch (ServiceException e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            ContactService.TrackActivity(ActivityTypeEnum.LostPasswordRecoveryFail, lostPasswordModel.EmailAddress);

            return View(lostPasswordModel);
        }

        #endregion
    }
}