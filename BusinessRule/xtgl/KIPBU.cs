using System;
using System.Collections.Generic;
using System.Web;
using System.Data;

using WebFrame;
using WebFrame.Data;
using WebFrame.Util;
using WebFrame.Designer;
using WebFrame.ExpControl;
using System.Configuration;

namespace BusinessRule
{
    public class KIPBU
    {
        private const String TableName = "jstrinfo";
        private static JConnect daConnect = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="corpDic"></param>
        public static bool SetIPs(string ips)
        {
            bool succ = false;
            String sql = "update JSTRINFO set STRTEXT='"+ips+"' where STRID = 'KIP'";

            JCommand comm = new JCommand();

            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("STRID", "KIP"));

            daConnect = JConnect.GetConnect();
            JTable tab1 = new JTable(daConnect, TableName);
            bool hasdata = tab1.HasData(condition);
            if (!hasdata)
            {
                String insertsql = "insert into jstrinfo (STRID)values ('KIP')";
                comm.CommandText = insertsql;
                comm.ExecuteNoQuery();
            }
            
            comm.CommandText = sql;

            try
            {
               comm.ExecuteNoQuery();
               succ = true;
            }
            catch (Exception err)
            {
                succ = false;
            }
             
            return succ;
        }

        public static string GetIPs()
        {
            daConnect = JConnect.GetConnect();

            JTable tab1 = new JTable(daConnect, TableName);

            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("STRID", "KIP"));
            bool hasdata = tab1.HasData(condition);
            DataRow data1 = tab1.GetFirstDataRow(condition, "STRTEXT");
            tab1.Close();

            if (!hasdata)
            {
                return "";
            }
            else
            {
                return data1["STRTEXT"].ToString().Trim();
            }
        }
    }
}
