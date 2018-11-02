using System;
using Hello.IDAL;
using Hello.Model;
using Component;
using Component.Utils;
using Dapper;
using System.Linq;

namespace Hello.DAL
{
    public class GetReportRepository : IGetReport
    {
        /// <summary>
        /// 用于数据管理的DbConnection类
        /// </summary>
        public IDbSession DbSession { get; set; }

        public ReportEntity GetEntity(string checkid)
        {
            try
            {
                string sql = @"
                          SELECT a01001 as PfID, a01002 as PfName,a01003 as PfGender,a01004 as PfAge,a01013 as PfPRCID,(CASE WHEN a01019 LIKE '%1%' THEN '儿童' WHEN a01019 LIKE '%3%' THEN '老人' ELSE '成人' END) as PfPersonGroupCode, a02005 as PfChekTime
                          from ea01 LEFT JOIN ea02 on a01001 = a02002 where a02001=@CheckNo 
                          ";
                ReportEntity rte = DbSession.DbConnection.Query<ReportEntity>(sql, new { CheckNo = checkid }).FirstOrDefault();
                //统一日期格式
                rte.PfChekTime = Convert.ToDateTime(rte.PfChekTime).ToString("yyyy-MM-dd HH:mm:ss");
                //根据出生日期计算年龄
                rte.PfAge = CalculatReport.GetAge(Convert.ToDateTime(rte.PfAge), rte.PfChekTime);
                return rte;
            }
            catch (Exception ex)
            {
                LogUtils.logger.Error("GetReportRepository:GetReportEntity()" + ex.ToString());
                return new ReportEntity();
            }
        }      
    }
}
