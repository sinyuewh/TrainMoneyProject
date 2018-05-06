using System;
using System.Collections.Generic;

using System.Text;
using System.Data;

using WebFrame;
using WebFrame.Data;
using WebFrame.Util;
using WebFrame.Designer;
using WebFrame.ExpControl;

namespace BusinessRule
{
    /// <summary>
    /// 线点别名业务类
    /// </summary>
    public class TrainAliasBU
    {
        /// <summary>
        /// 得到所有别名的List项
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        public List<String> GetAlias(String A)
        {
            List<String> list0 = new List<string>();
            if (String.IsNullOrEmpty(A) == false)
            {
                JTable tab1 = new JTable("TRAINALIAS");
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("TRAINNAME", A));
                DataRow dr1 = tab1.GetFirstDataRow(condition, " TRAINALIAS ");
                if (dr1 != null)
                {
                    String t1 = dr1[0].ToString();
                    if (String.IsNullOrEmpty(t1) == false)
                    {
                        String[] s1 = t1.Replace("，", ",").Split(',');
                        foreach (String m in s1)
                        {
                            if (list0.Contains(m) == false)
                            {
                                list0.Add(m);
                            }
                        }
                    }
                }
                tab1.Close();

                //把自己加入
                if (list0.Contains(A) == false)
                {
                    list0.Add(A);
                }
            }
            return list0;
        }
    }
}
