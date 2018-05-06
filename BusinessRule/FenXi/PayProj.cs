using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using WebFrame;
using WebFrame.Data;
using WebFrame.Util;
using WebFrame.Designer;

namespace BusinessRule
{
    public class PayProj
    {
        /// <summary>
        /// 得到长交路表示列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetPayPrjList()
        {
            JTable tab = new JTable("PAYPROJ");
            tab.CommandText = "select * from PAYPROJ";
            return tab.SearchData(-1).Tables[0];

        }

    }
}
