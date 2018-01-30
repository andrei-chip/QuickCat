using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QuickCat
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
           
              routes.MapRoute(
              name: "AdminIndex",
              url: "AdminIndex",
              defaults: new { controller = "Admin", action = "AdminIndex", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "Register",
              url: "Register",
              defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional }
          );
            routes.MapRoute(
               name: "Index",
               url: "Index",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "Index2",
              url: "Index2",
              defaults: new { controller = "Home", action = "Index2", id = UrlParameter.Optional }
          );
            routes.MapRoute(
               name: "Login",
               url: "Login",
               defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
