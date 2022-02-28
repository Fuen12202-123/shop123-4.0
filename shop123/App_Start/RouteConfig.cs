using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace shop123
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.MapMvcAttributeRoutes();



            routes.MapRoute(
               name: "Order",
               url: "Order",
               defaults: new { controller = "Order", action = "OrderDetails" }
           );  
            routes.MapRoute(
               name: "Checkout",
               url: "Checkout",
               defaults: new { controller = "ShoppingCart", action = "checkout" }
           ); 
            routes.MapRoute(
               name: "ShoppingCart",
               url: "ShoppingCart",
               defaults: new { controller = "ShoppingCart", action = "ShoppingCar" }
           ); 
            routes.MapRoute(
               name: "Allspu",
               url: "Allspu/page/{page}",
               defaults: new { controller = "Home", action = "Allspu",  page = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "BABY",
               url: "BABY/{catalogBId}/page/{page}/{sort}",
               defaults: new { controller = "Home", action = "categoryPage", catalogAId = 4, catalogBId = UrlParameter.Optional, page = UrlParameter.Optional, sort = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "KIDS",
               url: "KIDS/{catalogBId}/page/{page}/{sort}",
               defaults: new { controller = "Home", action = "categoryPage", catalogAId = 3, catalogBId = UrlParameter.Optional, page = UrlParameter.Optional, sort = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "MAN",
               url: "MAN/{catalogBId}/page/{page}/{sort}",
               defaults: new { controller = "Home", action = "categoryPage", catalogAId = 2, catalogBId = UrlParameter.Optional, page = UrlParameter.Optional, sort = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "WOMAN",
                url: "WOMAN/{catalogBId}/page/{page}/{sort}",
                defaults: new { controller = "Home", action = "categoryPage", catalogAId = 1, catalogBId = UrlParameter.Optional, page = UrlParameter.Optional , sort=UrlParameter.Optional }
            );
                      

              routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
