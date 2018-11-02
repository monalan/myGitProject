using System;
using System.Collections.Generic;

namespace Hello.Model
{
    [Serializable]
    public class UserEntity
    {
        public int ID { get; set; }
        
        public string UserName { get; set; }
        
        public string UserPassword { get; set; }

        /// <summary>
        /// 权限1是管理员，0是普通用户
        /// </summary>
        public string PermissionCode { get; set; }
        
        public string ChildCompanyAuthority { get; set; }
        public List<ChildCompanyEntity> Companys { get; set; }
        public UserEntity()
        {
            Companys = new List<Model.ChildCompanyEntity>();
        }
    }
}
