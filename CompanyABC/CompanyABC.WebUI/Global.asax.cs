using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CompanyABC.WebUI.Infrastructure;
using CompanyABC.WebUI.App_Start;

namespace CompanyABC.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Configure Model Bindings
            System.Web.Mvc.ModelBinders.Binders.Add(typeof(Guid), new CompanyABC.WebUI.ModelBinders.GuidModelBinder());
            // Configure dependency injection container
            DependencyResolver.SetResolver(new NinjectDependencyResolver());
        }
    }
}