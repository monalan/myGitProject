using Component;
using Component.Encrypt;
using Component.Utility;
using Dapper;
using Hello.IDAL;
using Hello.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello.DAL
{
    public class Authentication : IAuthentication
    {
        /// <summary>
        /// 用于管理DbConnection的类
        /// </summary>
        public IDbSession DbSession { get; set; }

        /// <summary>
        /// 用户登录验证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="scope">附加信息</param>
        /// <returns></returns>
        public OperationResult Sign(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return new OperationResult(OperationResultType.ParamError);
            var strsql = @"select a03001 as UserID , a03002 as UserName, a03003 as UserPassword,a03005 as PermissionCode,a03008 as ChildCompanyAuthority from ea03 where binary a03002 = @username";
            var result = new OperationResult
            {
                ResultType = OperationResultType.QueryNull,
                Message = "用户名或密码错误"
            };
            UserEntity userinfo = DbSession.DbConnection.QueryFirstOrDefault<UserEntity>(strsql, new
            {
                @username = username
            });
            if (userinfo != null)
            {
                if (userinfo.UserPassword == CMD5.MD5String(password))
                {
                    result.ResultType = OperationResultType.Success;
                    result.Message = null;
                    result.AppendData = userinfo;
                }
            }
            return result;
        }

        /// <summary>
        /// 设置新密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public int UpdatePwd(string username, string password)
        {
            int res = 0;

            try
            {
                var sql = @"update ea03 set a03003 = @UserPassword where binary a03002 = @UserName";
                res = DbSession.DbConnection.Execute(sql, new { @UserName = username, @UserPassword = CMD5.MD5String(password) });
            }
            catch (Exception ex)
            {
                LogUtils.logger.Error("更改密码出错！userName：" + username, ex);
            }

            return res;
        }
    }
}
