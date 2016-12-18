using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StudySite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                  name: "Homepage",
                  url: "",
                  defaults: new { controller = "Home", action = "Index" }
              );

            routes.MapRoute(
                  name: "SpecifyPage",
                  url: "{pageName}",
                  defaults: new { controller = "Home", action = "Index" }
              );
        }
    }
}

