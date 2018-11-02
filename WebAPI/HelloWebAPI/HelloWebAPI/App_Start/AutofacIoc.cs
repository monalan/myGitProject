using Autofac;
using Autofac.Integration.WebApi;
using System.Web.Http;
using System.Reflection;
using Hello.DAL.myIOC;

namespace HelloWebAPI.App_Start
{
    /// <summary>
    /// Autofac 注册
    /// </summary>
    public class AutofacIoc
    {
        /// <summary>
        /// Autofac 注册
        /// </summary>
        public static void Register()
        {
            var configuration = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();
            //注册控制器
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            builder.RegisterModule<APIDbModule>();
            IContainer container = builder.Build();
            //将依赖关系解析器设置为Autofac。
            var resolver = new AutofacWebApiDependencyResolver(container);
            configuration.DependencyResolver = resolver;
        }
    }
}