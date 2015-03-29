/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Huerate.Domain.Entities;
using Huerate.Services.Contacts;
using Huerate.WebUI.Controllers;

namespace Huerate.WebUI.Filters
{
    public class TrackPageVisitActionFilter : IActionFilter
    {
        private IContactService ContactService { get; set; }

        public TrackPageVisitActionFilter(IContactService contactService)
        {
            ContactService = contactService;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ActivityTypeEnum activityType = ActivityTypeEnum.GenericPageVisit;

            if (filterContext.Controller is AdminController)
            {
                activityType = ActivityTypeEnum.AdminPageVisit;
            }
            else if (filterContext.Controller is FormController)
            {
                activityType = ActivityTypeEnum.FormPageVisit;
            }
            else if (filterContext.Controller is HomePageController)
            {
                activityType = ActivityTypeEnum.HomePageVisit;
            }

            ContactService.TrackActivity(activityType, filterContext.RequestContext.HttpContext.Request.RawUrl);
        }
    }
}