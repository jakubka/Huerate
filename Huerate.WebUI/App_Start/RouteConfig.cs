/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

namespace Huerate.WebUI.App_Start
{
    public class WebUIRouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "HomePageIndex",
                url: "",
                defaults: new {controller = "HomePage", action = "Index"}
                );

            routes.MapRoute(
                name: "Admin",
                url: "admin",
                defaults: new {controller = "Admin", action = "Index"}
                );

            //routes.MapRoute(
            //    name: "Lunchtime",
            //    url: "lt/{codename}",
            //    defaults: new { controller = "Question", action = "Lunchtime" }
            //);

            routes.MapRoute(
                name: "Help",
                url: "help",
                defaults: new {controller = "HomePage", action = "Help"}
                );


            routes.MapRoute(
                name: "SignIn",
                url: "signin",
                defaults: new {controller = "HomePage", action = "SignIn"}
                );

            routes.MapRoute(
                name: "SignUp",
                url: "signup",
                defaults: new {controller = "HomePage", action = "SignUp"}
                );

            // Explicit adding routes for controllers
            var q = from type in Assembly.GetExecutingAssembly().GetTypes()
                    where type.IsClass
                    where !type.IsAbstract
                    let typeName = type.Name
                    where typeName.EndsWith("Controller")
                    where typeName != "FormController"
                    // Exclude form controller
                    select typeName.Substring(0, typeName.Length - "Controller".Length);

            foreach (var controllerName in q)
            {
                routes.MapRoute(
                    name: controllerName + "ExplicitControllerRoute",
                    url: controllerName.ToLower() + "/{action}/{id}",
                    defaults: new {controller = controllerName, action = "Index", id = UrlParameter.Optional}
                    );
            }

            routes.MapRoute(
                name: "QrCode",
                url: "QrCode/{restaurantCodeName}",
                defaults: new {controller = "Image", action = "QrCode"});

            routes.MapRoute(
                name: "RestaurantForm",
                url: "{codeName}",
                defaults: new {controller = "Form", action = "Index"}
                );
        }
    }
}