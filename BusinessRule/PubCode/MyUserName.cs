using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using WebFrame.Data;
using WebFrame;
using System.Web;
using System.IO;
using WebFrame.Util;
using System.Data.OleDb;

namespace BusinessRule
{
    public class MyUserName
    {
        private const String TableName = "MYUSERNAME";

        /// <summary>
        /// 判断当前登录的用户是否是管理员
        /// </summary>
        public static bool IsAdmin
        {
            get
            {
                if (HttpContext.Current.Session["isAdmin"] == null)
                {
                    return false;
                }
                else
                {
                    return (bool)HttpContext.Current.Session["isAdmin"];
                }
            }
        }

        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public bool Login(String userName, String passWord)
        {
            bool succ = false;
            JTable tab1 = new JTable(TableName);
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("UserName", userName));
            DataSet ds1 = tab1.SearchData(condition, 1, "*");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                DataRow dr1 = ds1.Tables[0].Rows[0];
                if (JString.MD5(passWord) == dr1["password"].ToString()
                    || passWord == dr1["password"].ToString())
                {
                    succ = true;
                    JCookie.SetCookieValue("CurrentLogin", userName);
                    if (dr1["isAdmin"].ToString().Trim() == "1")
                    {
                        HttpContext.Current.Session["isAdmin"] = true;
                    }
                    else
                    {
                        HttpContext.Current.Session["isAdmin"] = false;
                    }

                    dr1["LastLogin"] = DateTime.Now.ToString();   
                    if (passWord == dr1["password"].ToString())
                    {
                        dr1["password"] = JString.MD5(passWord);
                    }
                    tab1.Update(ds1.Tables[0]);
                }
            }
            ds1.Dispose();
            tab1.Close();
            return succ;
        }

        /// </summary>
        /// <param name="newPassWord"></param>
        /// <returns></returns>
        public bool UpdatePassWord(String UserName,String oldPassWord, String newPassWord)
        {
            bool succ = false;
            if (String.IsNullOrEmpty(UserName) == false)
            {
                JTable tab1 = new JTable(TableName);
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("UserName", UserName));
                DataSet ds1 = tab1.SearchData(condition, 1, "*");
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    DataRow dr1 = ds1.Tables[0].Rows[0];
                    if (JString.MD5(oldPassWord) == dr1["password"].ToString())
                    {
                        succ = true;
                        dr1["password"] = JString.MD5(newPassWord);
                        tab1.Update(ds1.Tables[0]);
                    }
                }
                ds1.Dispose();
                tab1.Close();
            }
            return succ;
        }
    }
}
