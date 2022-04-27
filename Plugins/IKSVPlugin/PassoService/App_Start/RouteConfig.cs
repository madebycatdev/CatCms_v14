using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PassoService
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Error",
                url: "hata",
                defaults: new { controller = "Home", action = "Error", id = UrlParameter.Optional }
             );

            routes.MapRoute(
                 name: "Logout",
                 url: "cikis-yap",
                 defaults: new { controller = "Home", action = "Logout", id = UrlParameter.Optional }
              );
            routes.MapRoute(
             name: "RenewProduct",
             url: "uyelik-yenile",
             defaults: new { controller = "Home", action = "RenewProduct", id = UrlParameter.Optional }
               );

            routes.MapRoute(
             name: "Welcome",
             url: "hosgeldiniz",
             defaults: new { controller = "Home", action = "Welcome", id = UrlParameter.Optional }
            );
            routes.MapRoute(
              name: "Payment",
              url: "odeme",
              defaults: new { controller = "Home", action = "Payment", result = UrlParameter.Optional,message = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "GetTowns",
            url: "gettowns",
            defaults: new { controller = "Home", action = "GetTowns", cityId = UrlParameter.Optional }
            );

            routes.MapRoute(
             name: "Signup",
             url: "uye-ol/kart-{id}",
             defaults: new { controller = "Home", action = "Signup", id = UrlParameter.Optional }
           );

            routes.MapRoute(
              name: "UserInfo",
              url: "uye-bilgileri",
              defaults: new { controller = "Home", action = "UserInfo", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "Forgotpin",
              url: "sifremi-unuttum",
              defaults: new { controller = "Home", action = "ForgotPin", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Signin",
                url: "uye-girisi",
                defaults: new { controller = "Home", action = "Signin", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Signin", id = UrlParameter.Optional }
            );
        }
    }
}
