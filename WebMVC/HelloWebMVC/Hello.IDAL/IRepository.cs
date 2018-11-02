using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello.IDAL
{
    /// <summary>
    /// 通用的CRUD操作接口，如果有其它特殊需求，则需要重新定义相应的接口，然后具体的业务类通过实现多接口的方式来实现复杂的查询
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// 保存操作
        /// </summary>
        /// <param name="item"></param>
        int Add(T item);

        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        int Update(T item);

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="item"></param>
        int Remove(T item);

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="id">主键</param>
        int Remove(int id);

        /// <summary>
        /// 获取单条实例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T FindById(int id);

        /// <summary>
        /// 通过用户名获取记录
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        T FindByUsername(string username);

        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns></returns>
        List<T> FindBySpecification();

        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns></returns>
        List<T> FindBySpecification(T item);

        /// <summary>
        /// 获取全部用户名
        /// </summary>
        /// <returns></returns>
        List<T> FindAllUserName();

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        List<T> FindBySpecification(int take, int skip);
    }
}
