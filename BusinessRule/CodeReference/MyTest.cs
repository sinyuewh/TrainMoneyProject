using System;
using System.Collections.Generic;

using System.Text;
using WebFrame;

namespace BusinessRule
{
    public class MyTest
    {
        public DataSourceResult GetList(String DataSourceID)
        {
            String OrderBy = String.Empty;
            String SearchCondition = String.Empty;
            int CurPage = 1;

            //当前的排序
            if (String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.QueryString[DataSourceID + "OrderBy"]) == false)
            {
                OrderBy  = System.Web.HttpContext.Current.Request.QueryString[DataSourceID+ "OrderBy"];
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

            
            //业务代码处理结束后，返回的结果
            DataSourceResult da1 = new DataSourceResult();
            da1.DataTable = new System.Data.DataTable();
            da1.CurPage = 2;
            da1.TotalRow = 3;

            return da1;
        }
    }
}
