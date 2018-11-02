using Autofac;
using Hello.IDAL;
using Hello.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello.DAL.myIOC
{
    public class DbModule : Module
    {
        /// <summary>
        /// 实现Autofac注册
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(paramName: nameof(builder));

            // 注册数据库对象，这样做是为了实现操作多个数据库
            builder.RegisterType<DbSession>().Named<IDbSession>("DbSession").InstancePerDependency();

            //注册登录用业务类
            builder.Register<Authentication>(x => new Authentication()
            {
                DbSession = x.ResolveNamed<IDbSession>("DbSession")
            }).As<IAuthentication>()
            .InstancePerDependency();

            //注册用户管理业务类
            builder.Register<UserReposity>(x => new UserReposity()
            {
                DbSession = x.ResolveNamed<IDbSession>("DbSession")
            }).As<IRepository<UserEntity>>()
            .InstancePerDependency();
        }
    }
}
