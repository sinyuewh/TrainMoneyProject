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
    public class STATIONINFO
    {
        private const String TableName = "STATIONINFO";
        private JConnect daConnect = null;

        public DataTable GetListData(List<SearchField> condition,
         int PageSize, int CurPage, out int TotalRow)
        {
            return this.GetListData(condition, PageSize, CurPage, "STATIONNAME", out TotalRow, "*");
        }

        public DataTable GetListData(List<SearchField> condition,
          int PageSize, int CurPage, String orderBy,
          out int TotalRow, params String[] Fields)
        {
            TotalRow = 0;
            this.daConnect = JConnect.GetConnect();
            DataTable dt1 = null;
            JTable tab1 = new JTable(daConnect, TableName);
            tab1.PageSize = PageSize;
            if (String.IsNullOrEmpty(orderBy) == false)
            {
                tab1.OrderBy = orderBy;
            }
            dt1 = tab1.SearchData(condition, CurPage, Fields).Tables[0];
            TotalRow = tab1.GetTotalRow();          //得到总的数据行
            tab1.Close();
            return dt1;
        }
    }
}
