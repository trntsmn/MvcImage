using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcImage
{
    public class MvcApplication : System.Web.HttpApplication
    {
        
		/// <summary>
		/// 
		/// </summary>
		/// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Clear();

            //Main upload controller route to create a dynamic image
            routes.MapRoute("Uploads.Dynamic", "{controller}/{directory}/{action}/{size}/{*path}", new { controller = "Uploads", directory = "{directory}", action = "Dynamic", dimensions = "{size}", path = "{*path}" });
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RegisterRoutes(RouteTable.Routes);
        }
    }
}
