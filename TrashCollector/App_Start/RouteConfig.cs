using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TrashCollector
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "EmployeeRouters",
            url: "EmployeeRouters/PickUp/{id}/{memberid}",
            defaults: new { controller = "PersonCAFDetail", action = "Create", id = @"\d+", memberid = @"\d+" }
            );

            routes.MapRoute(
            name: "SetSchedule",
            url: "SetSchedules/DeletePickUp/{id}/{memberid}",
            defaults: new { controller = "PersonCAFDetail", action = "Create", id = @"\d+", memberid = @"\d+" }
            );
            
        }
    }
}
