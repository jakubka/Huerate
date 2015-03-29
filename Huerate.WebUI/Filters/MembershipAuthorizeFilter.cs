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
using Huerate.Logging;
using Huerate.Services.Contacts;
using Huerate.Services.Membership;

namespace Huerate.WebUI.Filters
{
    public class MembershipAuthorizeFilter : IAuthorizationFilter
    {
        private ILogService LogService { get; set; }
        private IMembershipService MembershipService { get; set; }

        public MembershipAuthorizeFilter(ILogService logService, IMembershipService membershipService)
        {
            LogService = logService;
            MembershipService = membershipService;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!MembershipService.IsRestaurantAccountOnline)
            {
                LogService.Info("User redirected to sign in page");
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "SignIn" }, { "controller", "HomePage" } });
            }
        }
    }
}