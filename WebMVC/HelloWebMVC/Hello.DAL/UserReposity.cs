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
    public class UserReposity : IRepository<UserEntity>
    {
        //private static readonly ILog _log = LogManager.GetLogger(typeof(UserReposity));

        /// <summary>
        /// 用于数据管理的DbConnection类
        /// </summary>
        public IDbSession DbSession { get; set; }

        public int Add(UserEntity item)
        {
            try
            {
                var result = DbSession.DbConnection.Execute("insert into ea03(a03002, a03003, a03005,a03008) values (@UserName, @UserPassword, @PermissionCode, @Authority)",
                                   new
                                   {
                                       @UserName = item.UserName,
                                       @UserPassword = item.UserPassword,
                                       @PermissionCode = item.PermissionCode,
                                       @Authority = item.ChildCompanyAuthority
                                   });
                return FindByUsername(item.UserName).ID;
                //return result;
            }
            catch (Exception ex)
            {
                LogUtils.logger.Error("新增用户信息出错！", ex);
                throw ex;
            }
        }

        /// <summary>
        /// 主键获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserEntity FindById(int id)
        {
            try
            {
                UserEntity client = new UserEntity();
                var sql = @"select a03001 as ID , a03002 as UserName, a03003 as UserPassword,a03005 as PermissionCode,a03008 as ChildCompanyAuthority,
                            r01001 as CompanyID, r01011 as ChildCompanyName
                            from ea03 left join er01 on INSTR(CONCAT(',',a03008,','),CONCAT(',',r01011,','))>0
                            where a03001 = @UserID";
                return DbSession.DbConnection.Query<UserEntity, ChildCompanyEntity, UserEntity>(sql, (u, cc) =>
                {
                    if (client.ID <= 0)
                    {
                        if (cc != null)
                            u.Companys.Add(cc);
                        client = u;
                        return u;
                    }
                    else
                    {
                        if (cc != null)
                            client.Companys.Add(cc);
                        return client;
                    }
                }, new { @UserID = id }, splitOn: "CompanyID").FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogUtils.logger.Error("获取用户信息出错！", ex);

                throw ex;
            }
        }

        public UserEntity FindByUsername(string username)
        {
            try
            {
                UserEntity client = new UserEntity();
                var sql = @"select a03001 as ID , a03002 as UserName, a03003 as UserPassword,a03005 as PermissionCode,a03008 as ChildCompanyAuthority,
                            r01001 as CompanyID, r01011 as ChildCompanyName
                            from ea03 left join er01 on INSTR(CONCAT(',',a03008,','),CONCAT(',',r01011,','))>0
                            where binary a03002 = @UserName";
                return DbSession.DbConnection.Query<UserEntity, ChildCompanyEntity, UserEntity>(sql, (u, cc) =>
                {
                    if (client.ID <= 0)
                    {
                        u.Companys.Add(cc);
                        client = u;
                        return u;
                    }
                    else
                    {
                        client.Companys.Add(cc);
                        return client;
                    }
                }, new { @UserName = username }, splitOn: "CompanyID").FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogUtils.logger.Error("获取用户信息出错！", ex);

                throw ex;
            }
        }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        public List<UserEntity> FindBySpecification()
        {
            try
            {
                var sql = @"select a03001 as ID , a03002 as UserName, a03003 as UserPassword,a03005 as PermissionCode,a03008 as ChildCompanyAuthority from ea03";
                return DbSession.DbConnection.Query<UserEntity>(sql).ToList<UserEntity>();
            }
            catch (Exception ex)
            {
                LogUtils.logger.Error("获取用户信息出错！", ex);

                throw ex;
            }
        }

        /// <summary>
        /// 获取所有用户名
        /// </summary>
        /// <returns></returns>
        public List<UserEntity> FindAllUserName()
        {
            try
            {
                var sql = @"select a03001 as ID , a03002 as UserName from ea03";
                return DbSession.DbConnection.Query<UserEntity>(sql).ToList<UserEntity>();
            }
            catch (Exception ex)
            {
                LogUtils.logger.Error("获取用户信息出错！", ex);

                throw ex;
            }
        }

        public List<UserEntity> FindBySpecification(int take, int skip)
        {
            throw new NotImplementedException();
        }

        public int Remove(int id)
        {
            try
            {
                var result = DbSession.DbConnection.Execute("delete from ea03 where a03001=@UserID",
                                   new { UserID = id });
                return result;
            }
            catch (Exception ex)
            {
                LogUtils.logger.Error("删除用户信息出错！", ex);
                throw ex;
            }
        }

        public int Remove(UserEntity item)
        {
            throw new NotImplementedException();
        }

        public int Update(UserEntity item)
        {
            try
            {
                var result = DbSession.DbConnection.Execute("update ea03 set a03002 = @UserName, a03003 = @UserPassword, a03005 = @PermissionCode,a03008 = @Authority where a03001 = @UserID",
                                   new
                                   {
                                       @UserName = item.UserName,
                                       @UserPassword = item.UserPassword,
                                       @PermissionCode = item.PermissionCode,
                                       @Authority = item.ChildCompanyAuthority,
                                       @UserID = item.ID
                                   });
                return result;
            }
            catch (Exception ex)
            {
                LogUtils.logger.Error("修改用户信息出错！", ex);
                throw ex;
            }
        }

        public List<UserEntity> FindBySpecification(UserEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
