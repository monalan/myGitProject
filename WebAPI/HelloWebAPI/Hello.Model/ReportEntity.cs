using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello.Model
{
   public class ReportEntity
    {
        #region 报告头信息
        /// <summary>
        /// 姓名
        /// </summary>
        public string PfName { get; set; }

        /// <summary>
        /// 个人编号
        /// </summary>
        public string PfID { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string PfGender { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string PfAge { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string PfPRCID { get; set; }

        /// <summary>
        /// 检测日期
        /// </summary>
        public string PfChekTime { get; set; }
        #endregion

        /// <summary>
        /// 人群类型
        /// </summary>
        public string PfPersonGroupCode { get; set; }

        /// <summary>
        /// 报告实体对应的json
        /// </summary>
        public object PReportEntity { get; set; }
    }
}
