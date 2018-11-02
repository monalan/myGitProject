using Hello.IDAL;
using Hello.Model;
using System.Web.Http;

namespace HelloWebAPI.Controllers
{
    public class PersonInfoController : ApiController
    {
        /// <summary>
        /// 数据查询
        /// </summary>
        public IGetReport GReport { get; set; }

        /// <summary>
        /// 获取具体报告内容
        /// </summary>
        /// <returns></returns>
        [Route("api/outcome/entity")]
        public ReportEntity GetReportEntity(string checkid)
        {
            return GReport.GetEntity(checkid);
        }
    }
}
