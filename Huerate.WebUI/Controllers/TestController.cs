/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Huerate.Domain.Entities;
using Huerate.Logging;
using Huerate.Services.AnswerSets;
using Huerate.Services.Answers;
using Huerate.Services.Email;
using Huerate.Services.Membership;
using Huerate.Services.RestaurantAccounts;
using Huerate.Services.Settings;
using Huerate.Services.Translations;

namespace Huerate.WebUI.Controllers
{
    public class TestController : ControllerBase
    {
        private ILogService LogService { get; set; }
        private IAnswerService AnswerService { get; set; }
        private IAnswerSetService AnswerSetService { get; set; }
        private IRestaurantAccountService RestaurantAccountService { get; set; }
        private IEmailSenderService EmailSenderService { get; set; }

        private readonly Lazy<List<string>> _textAnswers = new Lazy<List<string>>(() => new List<string>()
                                                                                            {
                                                                                                "Great self brewed beer (especially dark one) but a bit poor service.",
                                                                                                "Skvělé originální pivo, které je rozhodně povinnost ochutnat. Průměrná kuchyně (tj. ne špatná, ale ani nenadchne). Vše bohužel kazí snad už legendární obsluha, která je většinou hodně nepříjemná.",
                                                                                                "V hospodě podávají delikátní pivo :)",
                                                                                                "Jen škoda, že je tam vždy strašně zahulíno (do nekuřáckých prostor aby se člověk prosekával), jinak obsluha povětšinou fajn a pivo skvělé",
                                                                                                "Vyborna pikantní topinka",
                                                                                                "Skvělé pivo a hotovky, obzvlášť vrabec se zelím. Ale často až moc lidí.",
                                                                                            });

        public TestController(ILogService logService, IAnswerService answerService, IAnswerSetService answerSetService, IRestaurantAccountService restaurantAccountService, IMembershipService membershipService, ISettingsService settingsService, ITranslationService translationService, IEmailSenderService emailSenderService) : base(membershipService, settingsService, translationService)
        {
            LogService = logService;
            AnswerService = answerService;
            AnswerSetService = answerSetService;
            RestaurantAccountService = restaurantAccountService;
            EmailSenderService = emailSenderService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Exception()
        {
            throw new Exception("Expected exception");
        }

        [HttpPost]
        public JsonResult CreateTestAccount()
        {
            try
            {
                var email = "bobek@outlook.com";

                var account = RestaurantAccountService.Repository.GetRestaurantAccountByEmail(email);

                if (account != null)
                {
                    RestaurantAccountService.ChangePassword(account, "jakub");
                }
                else
                {
                    account = RestaurantAccountService.CreateRestaurantAccount("Restaurace u Jakuba", email, "jakub", CurrentLanguage);
                }

                MembershipService.SignIn(account, false);

                return Json(new {result = "OK"});
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;

                return Json(new {result = e.ToString()});
            }
        }

        [HttpPost]
        public JsonResult CreateTestAnswers()
        {
            //try
            //{
            //    var restaurantAccount = RestaurantAccountService.Repository.GetRestaurantAccountByCodeNameOrEmail("bobek@outlook.com");

            //    Random rand = new Random();

            //    foreach (var answerSetGuid in Enumerable.Range(1, 100).Select(_ => Guid.NewGuid()))
            //    {
            //        foreach (var question in restaurantAccount.Questions)
            //        {
            //            if (rand.NextDouble() > 0.3)
            //            {
            //                int maxValue = question.IsPredefined ? 100 : 1;
            //                AnswerService.CreateAnswer(answerSetGuid, question.Id, rand.Next(0, maxValue + 1));
            //            }
            //        }

            //        foreach (var i in Enumerable.Range(0, rand.Next(5, 20)))
            //        {
            //            AnswerSetService.SetAnswerText(answerSetGuid, restaurantAccount.Id, _textAnswers.Value[i % _textAnswers.Value.Count]);
            //        }
            //    }

            //    return Json(new {result = "OK"});
            //}
            //catch (Exception e)
            ////{
            //    Response.StatusCode = 500;

            //    return Json(new {result = e.Message});
            //}

            Response.StatusCode = 500;

            return Json(new {result = "Not implemented"});
        }

        [HttpPost]
        public async Task<JsonResult> SendEmail()
        {
            var email = new Email()
                            {
                                HtmlBody = "This is <h1>HTML</h2> body",
                                PlainTextBody = "This is PLAIN TEXT body",
                                Receivers = new List<string>() {"jakub.kadlubiec@gmail.com"},
                                Subject = "Huerate test mail",
                            };

            await EmailSenderService.SendEmailAsync(email);

            return Json(new {result = "OK"});
        }
    }
}