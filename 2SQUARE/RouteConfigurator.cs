using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcContrib.Routing;

namespace _2SQUARE
{
    public class RouteConfigurator
    {
        public virtual void RegisterRoutes()
        {
            RouteCollection routes = RouteTable.Routes;
            routes.Clear();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            MvcRoute.MappUrl("{controller}/{action}/{id}")
                .WithDefaults(new { controller = "Home", action = "Index", id = "" })
                .AddWithName("Default", routes);
        }
    }
}