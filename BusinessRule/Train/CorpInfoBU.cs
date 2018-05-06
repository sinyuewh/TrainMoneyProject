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
    public class CorpInfoBU
    {
        /// <summary>
        /// 得到所有公司名称
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        public DataTable GetCorpName()
        {
            DataTable dt = new DataTable();
            JTable tab1 = new JTable("CORPINFO");

            DataSet ds1 = tab1.SearchData(null, -1, new String[] { "CORPNAME"});
            dt = ds1.Tables[0];

            return dt;
        }

    }
}
