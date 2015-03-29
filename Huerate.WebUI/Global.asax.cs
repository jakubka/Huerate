/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Web;
using System.Web.Routing;
using Huerate.Domain.Entities;
using Huerate.Logging;
using Huerate.Services;
using Huerate.Services.Contacts;
using Huerate.Services.Membership;
using Huerate.WebUI.App_Start;
using Ninject;

namespace Huerate.WebUI
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            LogConfiguration.ConfigureLogging(AppenderEnum.TableStorage);

            WebUIRouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest()
        {
            string refParam = Request.QueryString["ref"];

            if (refParam != null)
            {
                var contactService = HuerateDependencyResolver.Get<IContactService>();

                contactService.TrackActivity(ActivityTypeEnum.Ref, refParam);

                RedirectToUrlWithoutRef();
            }
        }

        private void RedirectToUrlWithoutRef()
        {
            UriBuilder uriBuilder = new UriBuilder(Request.Url);
            var values = HttpUtility.ParseQueryString(uriBuilder.Query);
            values.Remove("ref");
            uriBuilder.Query = values.ToString();

            Response.Redirect(uriBuilder.ToString(), true);
        }
    }
}