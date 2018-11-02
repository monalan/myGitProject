using Component.Extend;
using Hello.IDAL;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;

namespace Hello.DAL.myIOC
{
    public sealed class DbSession : IDbSession
    {
        bool _disposed;

        public DbSession()
        {
            DbConnection = new MySqlConnection("DbString:edbes".ConnectionStrings());
        }

        /// <summary>
        /// 用于管理数据库连接
        /// </summary>
        public DbConnection DbConnection { get; set; }

        /// <summary>
        /// 关闭DbConnection
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
                return;

            this._disposed = true;

            if (this.DbConnection == null)
                return;

            if (this.DbConnection.State != ConnectionState.Closed)
                this.DbConnection.Close();
        }
    }
}
