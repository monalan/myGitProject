using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace HelloWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            RegisterFormatters(config.Formatters);
        }

        /// <summary>
        /// 设置WebAPI返回格式为json
        /// </summary>
        /// <param name="formatters"></param>
        private static void RegisterFormatters(MediaTypeFormatterCollection formatters)
        {
            formatters.Add(new JQueryMvcFormUrlEncodedFormatter());
            formatters.Add(new XmlMediaTypeFormatter());

            formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));
            formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/javascript"));
            formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/x-www-form-urlencoded"));

            formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            formatters.JsonFormatter.SerializerSettings.Converters.Add(new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
            formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            formatters.XmlFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/xml"));
            formatters.XmlFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/xml"));
            formatters.XmlFormatter.UseXmlSerializer = true;
            formatters.XmlFormatter.Indent = true;
        }
    }
}
