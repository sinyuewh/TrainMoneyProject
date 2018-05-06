using System;
using System.Collections.Generic;

using System.Text;
using System.Data;

using WebFrame;
using WebFrame.Data;
using WebFrame.Util;
using WebFrame.Designer;
using WebFrame.ExpControl;
using System.Web.UI.WebControls;

namespace BusinessRule
{
    /// <summary>
    /// 税金的利率
    /// </summary>
    public class SRateProfileBU
    {
        //更新某年的税金数据
        public static void UpdateData(int year, double srate)
        {
            JTable tab1 = new JTable("SRATEPROFILE");
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("byear", year + ""));
            DataSet ds1 = tab1.SearchData(condition, -1, "*");
            DataTable dt1 = ds1.Tables[0];
            if (dt1.Rows.Count > 0)
            {
                DataRow dr1 = dt1.Rows[0];
                dr1["srate"] = srate ;
                tab1.Update(ds1.Tables[0]);
            }
            tab1.Close();
        }

        //增加某年的税金
        public static void AddRate(int year)
        {
            double result = 0.9676;
            JTable tab1 = new JTable("SRATEPROFILE");
            tab1.OrderBy = "byear desc";
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("byear", year + ""));
            DataSet ds1 = tab1.SearchData(condition, -1, "*");
            DataTable dt1 = ds1.Tables[0];
            if (dt1.Rows.Count == 0)
            {
                DataRow dr1 = dt1.NewRow();
                dr1["byear"] = year;
                dr1["srate"] = result;
                dt1.Rows.Add(dr1);
                tab1.Update(ds1.Tables[0]);
            }
            tab1.Close();
        }
        /// <summary>
        /// 得到某年的税金
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static double GetRate(int year)
        {
            double result = 0.9676;
            JTable tab1 = new JTable("SRATEPROFILE");
            tab1.OrderBy = "byear desc";
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("byear", year + ""));
            DataRow dr1 = tab1.GetFirstDataRow(condition, "srate");
            if (dr1 != null)
            {
                result = double.Parse(dr1[0].ToString());
            }
            else
            {
                condition.Clear();
                dr1 = tab1.GetFirstDataRow(null, "srate");
                if (dr1 != null)
                {
                    result = double.Parse(dr1[0].ToString());
                }
            }
            tab1.Close();
            return result;
        }


        /// <summary>
        /// 得到当年的税金
        /// </summary>
        /// <returns></returns>
        public static double GetRate()
        {
            int year1 = DateTime.Now.Year;
            return GetRate(year1);
        }
    }
}
