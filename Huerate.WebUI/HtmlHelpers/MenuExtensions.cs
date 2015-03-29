/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Huerate.WebUI.HtmlHelpers
{
    public static class MenuExtensions
    {
        public static MvcHtmlString MenuLink(this HtmlHelper helper,
                                       string text,
                                       string action,
                                       string controller,
                                       object routeValues = null,
                                       object htmlAttributes = null,
                                       string currentClass = "active")
        {
            RouteValueDictionary attributes = new RouteValueDictionary(htmlAttributes);

            string currentController = helper.ViewContext.RouteData.Values["controller"] as string ?? "home";
            string currentAction = helper.ViewContext.RouteData.Values["action"] as string ?? "index";
            
            string page = string.Format("{0}:{1}", currentController, currentAction).ToLower();
            string thisPage = string.Format("{0}:{1}", controller, action).ToLower();
            
            attributes["class"] = (page == thisPage) ? currentClass : "";

            return helper.ActionLink(text, action, controller, new RouteValueDictionary(routeValues), attributes);
        }
    }
}