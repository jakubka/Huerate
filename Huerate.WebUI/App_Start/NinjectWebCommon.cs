/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Web.Mvc;
using Huerate.Domain;
using Huerate.Services;
using Huerate.Services.UrlService;
using Huerate.WebUI.Filters;
using Huerate.WebUI.Services;
using Ninject.Activation;
using Ninject.Web.Mvc.FilterBindingSyntax;

[assembly: WebActivator.PreApplicationStartMethod(typeof (Huerate.WebUI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof (Huerate.WebUI.App_Start.NinjectWebCommon), "Stop")]

namespace Huerate.WebUI.App_Start
{
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof (OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof (NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = NinjectKernelFactory.GetKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            kernel.Bind<UrlHelper>().ToMethod(ctx => new UrlHelper(((MvcHandler)ctx.Kernel.Get<HttpContextBase>().CurrentHandler).RequestContext)).InRequestScope();

            kernel.Bind<IUrlService>().To<UrlService>().InRequestScope();

            kernel.BindFilter<TrackPageVisitActionFilter>(FilterScope.Controller, 1);
            kernel.BindFilter<MembershipAuthorizeFilter>(FilterScope.Controller, 2).WhenControllerHas<MembershipAuthorizeFilterAttribute>();
            kernel.BindFilter<MembershipAuthorizeFilter>(FilterScope.Action, 2).WhenActionMethodHas<MembershipAuthorizeFilterAttribute>();
            kernel.BindFilter<HandleExceptionFilter>(FilterScope.Controller, 3);

            return kernel;
        }
    }
}