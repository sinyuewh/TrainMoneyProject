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
    public class BigStationBU
    {
        private const String TableName = "BIGSTATIONLIST";
        private JConnect daConnect = null;

        public static String GetBigList(String num)
        {
            String result = String.Empty;
            if (String.IsNullOrEmpty(num) == false)
            {
                JTable tab1 = new JTable("BIGSTATIONLIST");
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("parentnum", num, SearchFieldType.NumericType));
                DataSet ds1=tab1.SearchData(condition, -1, "name1");
                foreach (DataRow dr1 in ds1.Tables[0].Rows)
                {
                    if (result == String.Empty)
                    {
                        result = dr1["name1"].ToString();
                    }
                    else
                    {
                        result = result + "," + dr1["name1"].ToString();
                    }
                }
                ds1.Dispose();
                tab1.Close();
            }
            return result;
        }

        public DataTable GetListData(List<SearchField> condition,
           int PageSize, int CurPage, out int TotalRow)
        {
            return this.GetListData(condition, PageSize, CurPage, "DATAID", out TotalRow, "*");
        }

        public DataTable GetListData(List<SearchField> condition,
          int PageSize, int CurPage, String orderBy,
          out int TotalRow, params String[] Fields)
        {
            TotalRow = 0;
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

        public void DeleteData(String num)
        {
            JTable tab1 = new JTable();
            tab1.MyConnect.BeginTrans();
            try
            {
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("parentnum", num, SearchFieldType.NumericType));
                tab1.TableName = "BIGSTATIONLIST";
                tab1.DeleteData(condition);

                tab1.TableName = "BIGSTATION";
                condition.Clear();
                condition.Add(new SearchField("num", num, SearchFieldType.NumericType));
                tab1.DeleteData(condition);
                tab1.MyConnect.CommitTrans();
            }
            catch
            {
                tab1.MyConnect.RollBackTrans();
            }
            finally
            {
                if (tab1 != null) tab1.Close();
            }
        }

        public static void SetBigList(String num, String bigList)
        {
            if (String.IsNullOrEmpty(bigList) == false
                && String.IsNullOrEmpty(num)==false)
            {
                String[] str1 = bigList.Replace("，", ",").Split(',');
                JTable tab1 = new JTable("BIGSTATIONLIST");
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("parentnum", num, SearchFieldType.NumericType));
                tab1.DeleteData(condition);

                DataSet ds1 = tab1.SearchData(condition, -1, "*");
                int i = 1;
                bool isEdit = false;
                foreach (String m in str1)
                {
                    if (isEdit == false) isEdit = true;
                    DataRow dr1 = ds1.Tables[0].NewRow();
                    dr1["num"] = i;
                    dr1["parentnum"] = num;
                    dr1["name1"] = m;
                    i++;
                    ds1.Tables[0].Rows.Add(dr1);
                }
                if (isEdit)
                {
                    tab1.Update(ds1.Tables[0]);
                }
                tab1.Close();

            }
        }
    }
}
