using System;

namespace Component
{
    public static class CalculatReport
    {
        public static string GetAge(DateTime birth, string createTime)
        {
            string strAge = string.Empty;                         // 年龄的字符串表示
            int[] intlist = CalculateAge(birth, createTime);
            int intDay = intlist[0];
            int intMonth = intlist[1];
            int intYear = intlist[2];
            // 格式化年龄输出
            if (intYear >= 1)                                            // 年份输出
            {
                strAge = intYear.ToString() + "岁";
            }

            if (intMonth > 0 && intYear <= 5)                           // 五岁以下可以输出月数
            {
                strAge += intMonth.ToString() + "月";
            }

            if (intDay >= 0 && intYear < 1)                              // 一岁以下可以输出天数
            {
                if (strAge.Length == 0 || intDay > 0)
                {
                    strAge += intDay.ToString() + "天";
                }
            }
            return strAge;
        }

        public static int[] CalculateAge(DateTime birth, string createTime)
        {
            DateTime dtNow = Convert.ToDateTime(createTime);
            string strAge = string.Empty;                         // 年龄的字符串表示
            int intYear = 0;                                    // 岁
            int intMonth = 0;                                    // 月
            int intDay = 0;                                    // 天
            int[] intlist = new int[3];
            // 如果没有设定出生日期, 返回空
            if (birth == null)
            {
                return null;
            }
            // 计算天数
            intDay = dtNow.Day - birth.Day;
            if (intDay < 0)
            {
                dtNow = dtNow.AddMonths(-1);
                intDay += DateTime.DaysInMonth(dtNow.Year, dtNow.Month);
            }
            // 计算月数
            intMonth = dtNow.Month - birth.Month;
            if (intMonth < 0)
            {
                intMonth += 12;
                dtNow = dtNow.AddYears(-1);
            }
            // 计算年数
            intYear = dtNow.Year - birth.Year;
            intlist[0] = intDay;
            intlist[1] = intMonth;
            intlist[2] = intYear;
            return intlist;
        }
    }
}
