using System.Web.Mvc;
using System.Web.Routing;

namespace HelloWebAPI.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Help", action = "Index", id = UrlParameter.Optional }
            ).DataTokens.Add("Areas", "HelpPage");
        }
    }
}