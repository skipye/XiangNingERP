using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class DateTimeExtension
    {
        public static string Format(this DateTime date, string formatString = "yyyy-MM-dd")
        {
            return date.ToString(formatString);
        }
        public static string Format(this DateTime? date, string formatString = "yyyy-MM-dd")
        {
            if (date.HasValue)
            {
                return date.Value.ToString(formatString);
            }

            return string.Empty;

        }
        /// <summary>
        /// <para> 功    能： 日期转换 </para>
        /// <para> 如果是当天，显示时间；如果是当年，显示月日，或者显示短日期类型
        /// <para> 作    者： 韩保新 </para>
        /// <para> 创建日期： 2011-7-29</para>
        /// </summary>
        /// <param name="date"></param>
        /// <param name="defautString"></param>
        /// <returns></returns>
        public static string FormatDayTime(this DateTime date, string defautString = "")
        {
            DateTime dt = DateTime.Now;
            if (date != null)
            {
                if (date.Year == dt.Year)
                {
                    if (date.Day == dt.Day)
                        return string.Format("{0}/{1}", date.Hour, date.Minute);
                    else
                        return string.Format("{0}/{1}", date.Month, date.Day);
                }
                date.ToString("yyyy-MM-dd");
            }
            return string.Empty;
        }
    }
}
