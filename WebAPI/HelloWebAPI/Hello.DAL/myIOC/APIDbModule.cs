using Autofac;
using Hello.IDAL;
using System;

namespace Hello.DAL.myIOC
{
    public class APIDbModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(paramName: nameof(builder));

            // 注册数据库对象，这样做是为了实现操作多个数据库
            builder.RegisterType<DbSession>().Named<IDbSession>("DbSession").InstancePerDependency();

            builder.Register<GetReportRepository>(x => new GetReportRepository()
            {
                DbSession = x.ResolveNamed<IDbSession>("DbSession")
            }).As<IGetReport>()
           .InstancePerDependency();
        }
    }
}
