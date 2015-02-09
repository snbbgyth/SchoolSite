﻿using System.Web.Mvc;
using System.Web.Routing;

namespace SchoolSite.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               "Default",
               "{controller}/{action}/{id}",
               new { controller = "Home", action = "Index", id = UrlParameter.Optional, Area = "Home" },
              new string[] { "SchoolSite.Web.Controllers" }
          );

        }
    }
}
