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
    /// frm_systable 业务类
    /// 编码：金寿吉
    /// 时间：2011年06月21日
    /// 最后修改：
    /// </summary>
    public partial class Frm_systableBU
    {
        private const String TableName = "frm_systable";
        
        #region 一般的DAL
        /// <summary>
        /// 功能：插入一条数据
        /// <param name="data1">要插入的数据</param>
        /// </summary>
        public void InsertData(Dictionary<string,object> data1)
        {
            JTable tab1 = new JTable(TableName);
            tab1.InsertData(data1);
            tab1.Close();
        }

        /// <summary>
        /// 功能：删除数据
        /// </summary>
        /// <param name="condition">要删除数据的条件</param>
        public void DeleteData(List<SearchField> condition)
        {
            JTable tab1 = new JTable(TableName);
            tab1.DeleteData(condition);
            tab1.Close();
        }

        /// <summary>
        /// 功能：更新数据
        /// </summary>
        /// <param name="condition">更新数据的条件</param>
        /// <param name="newData">新数据的值</param>
        public void UpdateData(List<SearchField> condition,Dictionary<string,object> newData)
        {
            JTable tab1 = new JTable(TableName);
            tab1.EditData(newData, condition);
            tab1.Close();
        }


        /// <summary>
        /// 功能：查询特定的数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="curPage">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="orderBy">排序表达式</param>
        /// <param name="totalRow">返回的总行数</param>
        /// <param name="fs">要显示的字段</param>
        /// <returns></returns>
        public DataSet SearchData(List<SearchField> condition, int curPage,
            int pageSize,String orderBy, out int totalRow, params String[] fs)
        {
            totalRow = 0;
            JTable tab1 = new JTable(TableName);
            tab1.PageSize = pageSize;
            if (String.IsNullOrEmpty(orderBy) == false)
            {
                tab1.OrderBy = orderBy;
            }
            DataSet ds1 = tab1.SearchData(condition, curPage, fs);
            totalRow = tab1.GetTotalRow();
            tab1.Close();
            return ds1;
        }
        #endregion

        #region 业务方法
        public DataSourceResult GetList(String DataSourceID,String pageSize)
        {
            String OrderBy = String.Empty;
            String SearchCondition = String.Empty;
            int CurPage = 1;

            //当前的排序
            if (String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.QueryString[DataSourceID + "OrderBy"]) == false)
            {
                OrderBy = System.Web.HttpContext.Current.Request.QueryString[DataSourceID + "OrderBy"];
            }

            //当前查询条件
            if (String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.QueryString[DataSourceID + "SearchCondition"]) == false)
            {
                SearchCondition = System.Web.HttpContext.Current.Request.QueryString[DataSourceID + "SearchCondition"];
            }

            //当前页
            if (String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.QueryString[DataSourceID + "CurPage"]) == false)
            {
                String CurPage1 = System.Web.HttpContext.Current.Request.QueryString[DataSourceID + "CurPage"];
                if (WebFrame.Util.JValidator.IsInt(CurPage1))
                {
                    CurPage = int.Parse(CurPage1);
                }
            }
            /*----------自己的业务处理代码-------------------------------------*/
            int totalRow = 0;
            JTable tab1 = new JTable(TableName);
            tab1.PageSize = int.Parse(pageSize);
            if (String.IsNullOrEmpty(OrderBy) == false)
            {
                tab1.OrderBy = OrderBy;
            }
            List<SearchField> condition = new List<SearchField>();
            if (String.IsNullOrEmpty(SearchCondition) == false)
            {
                condition.Add(new SearchField(SearchCondition, "", SearchOperator.UserDefine));
            }
            DataSet ds1 = tab1.SearchData(condition, CurPage, "*");
            totalRow = tab1.GetTotalRow();
            tab1.Close();

            //业务代码处理结束后，返回的结果
            DataSourceResult da1 = new DataSourceResult();
            da1.DataTable = ds1.Tables[0];
            da1.CurPage = CurPage;
            da1.TotalRow =totalRow;

            return da1;
        }
        #endregion

        #region Example
        /*
        /// <summary>
        /// 功能：事物处理代码参考
        /// </summary>
        public void ExampleForTrans()
        {
            JConnect conn1 = JConnect.GetConnect();     //得到默认的数据库连接
            JTable tab1 =null;
            conn1.BeginTrans();
            try
            {
                tab1 = new JTable(conn1, TableName);
                //......

                conn1.CommitTrans();                  //提交事物处理
            }
            catch (Exception err)
            {
                conn1.RollBackTrans();               //回滚事物处理
            }
            finally
            {
                if (tab1 != null) tab1.Close();
            }
        }

        /// <summary>
        /// 功能：使用Update更新数据，适合查询到数据后更新再提交
        /// </summary>
        public void ExampleForUpdate()
        {
            JTable tab1 = new JTable(TableName);
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("id", "1", SearchFieldType.NumericType));
            DataSet ds1 = tab1.SearchData(condition, -1, "test1", "test2","id");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                DataRow dr1 = ds1.Tables[0].Rows[0];
                dr1["test1"] = "abcd";
                dr1["test2"] = "dd";
                tab1.Update(ds1.Tables[0]);
            }
            tab1.Close();
        }*/
        #endregion
    }
}
