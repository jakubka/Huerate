/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Web.Mvc;
using Huerate.Domain.Entities;
using Huerate.Services.AnswerSets;
using Huerate.Services.Answers;
using Huerate.Services.Membership;
using Huerate.Services.Settings;
using Huerate.Services.Translations;

namespace Huerate.WebUI.Controllers
{
    public class AnswerController : ControllerBase
    {
        private IAnswerService AnswerService { get; set; }

        public AnswerController(IAnswerService answerService, IMembershipService membershipService, ISettingsService settingsService, ITranslationService translationService) : base(membershipService, settingsService, translationService)
        {
            AnswerService = answerService;
        }

        [HttpPost]
        public ActionResult Vote(Guid answerSetGuid, int questionId, int value, string language)
        {
            AnswerService.CreatePercentAnswer(answerSetGuid, questionId, value, language);

            return Json(new { result = "OK" });
        }

        [HttpPost]
        public ActionResult VoteBool(Guid answerSetGuid, int questionId, bool value, string language)
        {
            AnswerService.CreateYesNoAnswer(answerSetGuid, questionId, value, language);

            return Json(new { result = "OK" });
        }

        [HttpPost]
        public JsonResult GiveTextAnswer(Guid answerSetGuid, int formStepId, string text, string language)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return Json(new { Status = "OK" });
            }

            AnswerService.CreateTextAnswer(answerSetGuid, formStepId, text, language);

            return Json(new { Status = "OK" });
        }
    }
}
