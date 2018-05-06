using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using WebFrame;
using WebFrame.Data;
using WebFrame.Util;
using BusinessRule;

namespace KORWeb.WebSite.Handler
{
    /// <summary>
    /// GetStationNameValue 的摘要说明
    /// </summary>
    public class GetStationNameValue : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";
            string returnValue = String.Empty;
            String AutoKind = context.Request.QueryString["AutoKind"];
            switch (AutoKind)
            {
                case "GetName":
                    returnValue = this.GetStationName(context);
                    break;
                case "GetCorpName":
                    returnValue = this.GetCorpName(context);
                    break;
                case "GetKzId":
                    returnValue = this.GetKzId(context);
                    break;
                default:
                    break;
            }

            context.Response.Write(returnValue);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 得到车站名称
        /// </summary>
        /// <param name="sname"></param>
        /// <returns></returns>
        private string GetStationName(HttpContext context)
        {
            string AppNameOrWSpell = context.Request.Params["q"];
            StringBuilder builder = new StringBuilder();

            if (String.IsNullOrEmpty(AppNameOrWSpell) == false)
            {
                STATIONINFO bu1 = new STATIONINFO();
                List<SearchField> condition = new List<SearchField>();
                SearchField search1 = new SearchField("ABBNAME", AppNameOrWSpell, SearchOperator.Contains);
                SearchField search2 = new SearchField("WHOLESPELL", AppNameOrWSpell, SearchOperator.Contains);
                SearchField search3 = new SearchField("STATIONNAME", AppNameOrWSpell, SearchOperator.Contains);

                condition.Add(search1 | search2 | search3);
                int rows = 0;
                DataTable dt = bu1.GetListData(condition, -1, -1, out rows);
                //DataTable newdt = this.SelectDistinct(dt, "NAME1");//返回过滤后的DataTable
               
                foreach (DataRow dr in dt.Rows)
                {
                    builder.AppendLine(dr["STATIONNAME"].ToString());
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// 得到公司名称
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetCorpName(HttpContext context)
        {
            string AppNameOrWSpell = context.Request.Params["q"];
            StringBuilder builder = new StringBuilder();

            if (String.IsNullOrEmpty(AppNameOrWSpell) == false)
            {
                CORPINFO bu1 = new CORPINFO();
                List<SearchField> condition = new List<SearchField>();
                SearchField search1 = new SearchField("ABBNAME", AppNameOrWSpell, SearchOperator.Contains);
                SearchField search2 = new SearchField("WHOLESPELL", AppNameOrWSpell, SearchOperator.Contains);
                SearchField search3 = new SearchField("CORPNAME", AppNameOrWSpell, SearchOperator.Contains);

                condition.Add(search1 | search2 | search3);
                int rows = 0;
                DataTable dt = bu1.GetListData(condition, -1, -1, out rows);
                //DataTable newdt = this.SelectDistinct(dt, "NAME1");//返回过滤后的DataTable

                foreach (DataRow dr in dt.Rows)
                {
                    builder.AppendLine(dr["CORPNAME"].ToString());
                }
            }
            return builder.ToString();
        }


        /// <summary>
        /// 得到客专ID
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetKzId(HttpContext context)
        {
            string AppNameOrWSpell = context.Request.Params["q"];
            StringBuilder builder = new StringBuilder();

            if (String.IsNullOrEmpty(AppNameOrWSpell) == false)
            {
                CORPINFO bu1 = new CORPINFO();
                TrainShouRu1 tsbu = new TrainShouRu1();

                List<SearchField> condition = new List<SearchField>();
                SearchField search1 = new SearchField("ABBNAME", AppNameOrWSpell, SearchOperator.Contains);
                SearchField search2 = new SearchField("WHOLESPELL", AppNameOrWSpell, SearchOperator.Contains);
                SearchField search3 = new SearchField("CORPNAME", AppNameOrWSpell, SearchOperator.Contains);

                condition.Add(search1 | search2 | search3);
                int rows = 0;
                DataTable dt = bu1.GetListData(condition, -1, -1, out rows);
                //DataTable newdt = this.SelectDistinct(dt, "NAME1");//返回过滤后的DataTable

                foreach (DataRow dr in dt.Rows)
                {
                    DataTable dt1 = tsbu.GetkzList(dr["CORPNAME"].ToString());
                    if (dt1 != null)
                    {
                       foreach (DataRow dr1 in dt1.Rows)
                       {
                           builder.AppendLine(dr1["Title"].ToString().Trim());
                       }
                    }
                }
            }
            return builder.ToString();
        }
        /// <summary>
        /// 返回执行Select distinct后的DataTable
        /// </summary>
        /// <param name="SourceTable">源数据表</param>
        /// <param name="FieldNames">字段集</param>
        /// <returns></returns>
        private DataTable SelectDistinct(DataTable SourceTable, params string[] FieldNames)
        {
            object[] lastValues;
            DataTable newTable;
            DataRow[] orderedRows;

            if (FieldNames == null || FieldNames.Length == 0)
                throw new ArgumentNullException("FieldNames");

            lastValues = new object[FieldNames.Length];
            newTable = new DataTable();

            foreach (string fieldName in FieldNames)
                newTable.Columns.Add(fieldName, SourceTable.Columns[fieldName].DataType);

            orderedRows = SourceTable.Select("", string.Join(",", FieldNames));

            foreach (DataRow row in orderedRows)
            {
                if (!fieldValuesAreEqual(lastValues, row, FieldNames))
                {
                    newTable.Rows.Add(createRowClone(row, newTable.NewRow(), FieldNames));

                    setLastValues(lastValues, row, FieldNames);
                }
            }

            return newTable;
        }

        private bool fieldValuesAreEqual(object[] lastValues, DataRow currentRow, string[] fieldNames)
        {
            bool areEqual = true;

            for (int i = 0; i < fieldNames.Length; i++)
            {
                if (lastValues[i] == null || !lastValues[i].Equals(currentRow[fieldNames[i]]))
                {
                    areEqual = false;
                    break;
                }
            }

            return areEqual;
        }

        private DataRow createRowClone(DataRow sourceRow, DataRow newRow, string[] fieldNames)
        {
            foreach (string field in fieldNames)
                newRow[field] = sourceRow[field];

            return newRow;
        }

        private void setLastValues(object[] lastValues, DataRow sourceRow, string[] fieldNames)
        {
            for (int i = 0; i < fieldNames.Length; i++)
                lastValues[i] = sourceRow[fieldNames[i]];
        } 
    }
}