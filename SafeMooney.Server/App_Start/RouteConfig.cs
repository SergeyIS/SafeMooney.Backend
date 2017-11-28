using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace SafeMooney.Server
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.RouteExistingFiles = true;
        }
    }
}

