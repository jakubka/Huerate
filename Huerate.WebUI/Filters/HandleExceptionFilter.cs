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
using System.Web.Routing;
using Huerate.Domain.Entities;
using Huerate.Logging;
using Huerate.Services.Contacts;
using Huerate.Services.Email;
using Huerate.WebUI.Controllers;

namespace Huerate.WebUI.Filters
{
    public class HandleExceptionFilter : IExceptionFilter
    {
        private IContactService ContactService { get; set; }
        private IEmailSenderService EmailSenderService { get; set; }
        private ILogService LogService { get; set; }

        public HandleExceptionFilter(IContactService contactService, IEmailSenderService emailSenderService, ILogService logService)
        {
            ContactService = contactService;
            EmailSenderService = emailSenderService;
            LogService = logService;
        }

        public void OnException(ExceptionContext filterContext)
        {
            string controllerName = (string)filterContext.RouteData.Values["controller"];
            string actionName = (string)filterContext.RouteData.Values["action"];

            string message = string.Format("Unhandled exception in controller {0}, action {1}. ", controllerName, actionName);

            if (controllerName == "HomePage" && actionName == "Error")
            {
                message += "Error happened on the Error action - will not redirect";
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {{"action", "Error"}, {"controller", "HomePage"}, {"errorDescription", filterContext.Exception.Message}});
            }

            LogService.Error(message, filterContext.Exception);
            EmailSenderService.SendErrorEmailAsync(message, filterContext.Exception);

            filterContext.ExceptionHandled = true;
        }
    }
}