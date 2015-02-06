﻿using System.Web.Mvc;

namespace SchoolSite.Web.Areas.Admin
{
    internal static class RouteConfig
    {
        internal static void RegisterRoutes(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_Default",
                "Admin/{controller}/{action}/{id}",
                new {controller = "News", action = "Index", area = "Admin", id = ""},
                new[] { "SpringSoftware.Web.Areas.Admin.Controllers" });
        }
    }
}