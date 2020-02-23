using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace VidlyTutorial
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
            name: "ControllerAndActionOnly",
            routeTemplate: "api/{controller}/{action}",
            defaults: new { },
            constraints: new { action = @"^[a-zA-Z]+([\s][a-zA-Z]+)*$" });

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
