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
                url: "WOMAN/page/{page}/{sort}",
                defaults: new { controller = "Home", action = "categoryPage", catalogAId = 1, catalogBId = 0, page = UrlParameter.Optional , sort=UrlParameter.Optional }
            );
               routes.MapRoute(
                name: "tops/long",
                url: "WOMAN/tops/long/page/{page}/{sort}",
                defaults: new { controller = "Home", action = "categoryPage", catalogAId = 1, catalogBId = 1, page = UrlParameter.Optional , sort=UrlParameter.Optional }
            );
               routes.MapRoute(
                name: "tops/short",
                url: "WOMAN/tops/short/page/{page}/{sort}",
                defaults: new { controller = "Home", action = "categoryPage", catalogAId = 1, catalogBId = 2, page = UrlParameter.Optional , sort=UrlParameter.Optional }
            );
               routes.MapRoute(
                name: "top/svest",
                url: "WOMAN/top/svest/page/{page}/{sort}",
                defaults: new { controller = "Home", action = "categoryPage", catalogAId = 1, catalogBId = 3, page = UrlParameter.Optional , sort=UrlParameter.Optional }
            );
               routes.MapRoute(
                name: "shirts",
                url: "WOMAN/shirts/page/{page}/{sort}",
                defaults: new { controller = "Home", action = "categoryPage", catalogAId = 1, catalogBId = 4, page = UrlParameter.Optional , sort=UrlParameter.Optional }
            );
               routes.MapRoute(
                name: "dresses",
                url: "WOMAN/dresses/page/{page}/{sort}",
                defaults: new { controller = "Home", action = "categoryPage", catalogAId = 1, catalogBId = 5, page = UrlParameter.Optional , sort=UrlParameter.Optional }
            );
               routes.MapRoute(
                name: "outerwear",
                url: "WOMAN/outerwear/page/{page}/{sort}",
                defaults: new { controller = "Home", action = "categoryPage", catalogAId = 1, catalogBId = 6, page = UrlParameter.Optional , sort=UrlParameter.Optional }
            );
               routes.MapRoute(
                name: "bottoms/pants",
                url: "WOMAN/bottoms/pants/page/{page}/{sort}",
                defaults: new { controller = "Home", action = "categoryPage", catalogAId = 1, catalogBId = 7, page = UrlParameter.Optional , sort=UrlParameter.Optional }
            );
               routes.MapRoute(
                name: "bottoms/shorts",
                url: "WOMAN/bottoms/shorts/page/{page}/{sort}",
                defaults: new { controller = "Home", action = "categoryPage", catalogAId = 1, catalogBId = 8, page = UrlParameter.Optional , sort=UrlParameter.Optional }
            );
               routes.MapRoute(
                name: "bottoms/skirt",
                url: "WOMAN/bottoms/skirt/page/{page}/{sort}",
                defaults: new { controller = "Home", action = "categoryPage", catalogAId = 1, catalogBId = 9, page = UrlParameter.Optional , sort=UrlParameter.Optional }
            );
               routes.MapRoute(
                name: "underwear/briefs",
                url: "WOMAN/underwear/briefs/page/{page}/{sort}",
                defaults: new { controller = "Home", action = "categoryPage", catalogAId = 1, catalogBId = 10, page = UrlParameter.Optional , sort=UrlParameter.Optional }
            );

                      

              routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
