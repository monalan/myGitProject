using System;
using System.Data.Common;

namespace Hello.IDAL
{
    /// <summary>
    /// 公共的数据库连接管理接口，用于使用Autofac时可以自动关闭DbConnection，
    /// 之所以把它作为接口，主要是为了实现分库操作，当要添加新的数据库的时候，
    /// 就可以新建一个类继承该接口，然后在Autofac中进行注册即可。
    /// </summary>
    public interface IDbSession : IDisposable
    {
        /// <summary>
        /// 用于管理数据库连接
        /// </summary>
        DbConnection DbConnection { get; }
    }
}
