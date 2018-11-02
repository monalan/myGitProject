using Autofac;
using Hello.DAL.myIOC;
using Autofac.Integration.Mvc;
using System.Reflection;
using System.Web.Mvc;

namespace HelloWebMVC
{
    public class AutofacIoc
    {
        /// <summary>
        /// Autofac 注册
        /// </summary>
        public static void Register()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            builder.RegisterModule<DbModule>();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}