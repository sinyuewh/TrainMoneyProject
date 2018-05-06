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

namespace WebSite.AppCode
{
    /// <summary>
    /// 系统升级类
    /// </summary>
    public class Upgrade
    {
        /// <summary>
        /// 系统升级处理
        /// </summary>
        public static void Go()
        {
            String time1 = ConfigurationManager.AppSettings["gradeDate"];
            if (String.IsNullOrEmpty(time1) == false)
            {
                DateTime t1 = DateTime.Parse(time1);
                if (DateTime.Today <= t1)
                {
                    //1在LINESTATION中增加高铁联络线标志
                    String sql1 = "SELECT COUNT(*) FROM USER_TAB_COLUMNS WHERE TABLE_NAME = 'LINESTATION' AND COLUMN_NAME = 'GTLLX'";
                    JCommand comm1 = new JCommand();
                    comm1.CommandText = sql1;
                    DataRow dr1 = comm1.GetFirstDataRow();
                    bool hasdata = false;
                    if (dr1 != null)
                    {
                        int count1 = int.Parse(dr1[0].ToString());
                        if (count1 > 0) hasdata = true;
                    }

                    if (hasdata == false)
                    {
                        sql1 = "alter table LINESTATION add gtllx varchar2(10)";
                        comm1.CommandText = sql1;
                        comm1.ExecuteNoQuery();
                    }

                    //2修改视图
                    sql1 = @"create or replace view linestationview as
                             select LINESTATION.ID,linestation.fee1,linestation.fee2,linestation.fee3,linestation.shipflag,linestation.gtllx,
                             bigstationlist.name1 aname1,
                             b2.name1 bname1,
                             LINESTATION.LINEID,LINESTATION.NUM,LINESTATION.DQH,LINESTATION.ASTATION,LINESTATION.BSTATION,LINESTATION.MILES,
                             LINESTATION.MILESCLASS,LINESTATION.DIRECTION,LINESTATION.JNFLAG,TRAINLINE.LineType,TRAINLINE.LineName
                             from LINESTATION inner join TRAINLINE on LineStation.LineID=TRAINLINE.LineID
                             left outer join bigstationlist on linestation.astation=bigstationlist.name1
                             left outer join bigstationlist b2 on linestation.bstation=b2.name1";
                    comm1.CommandText = sql1;
                    comm1.ExecuteNoQuery();

                    //修改其他的数据--关联列车
                    sql1 = "SELECT COUNT(*) FROM USER_TAB_COLUMNS WHERE TABLE_NAME = 'NEWTRAIN' AND COLUMN_NAME = 'GLTRAIN'";
                    comm1.CommandText = sql1;
                    dr1 = comm1.GetFirstDataRow();
                    hasdata = false;
                    if (dr1 != null)
                    {
                        int count1 = int.Parse(dr1[0].ToString());
                        if (count1 > 0) hasdata = true;
                    }

                    if (hasdata == false)
                    {
                        sql1 = "alter table NEWTRAIN add GLTRAIN varchar2(50)";
                        comm1.CommandText = sql1;
                        comm1.ExecuteNoQuery();
                    }

                    //修改其他的数据--关联年份
                    sql1 = "SELECT COUNT(*) FROM USER_TAB_COLUMNS WHERE TABLE_NAME = 'NEWTRAIN' AND COLUMN_NAME = 'GLYEAR'";
                    comm1.CommandText = sql1;
                    dr1 = comm1.GetFirstDataRow();
                    hasdata = false;
                    if (dr1 != null)
                    {
                        int count1 = int.Parse(dr1[0].ToString());
                        if (count1 > 0) hasdata = true;
                    }

                    if (hasdata == false)
                    {
                        sql1 = "alter table NEWTRAIN add GLYEAR integer";
                        comm1.CommandText = sql1;
                        comm1.ExecuteNoQuery();
                    }

                    //修改其他的数据--关联列车
                    sql1 = "SELECT COUNT(*) FROM USER_TAB_COLUMNS WHERE TABLE_NAME = 'NEWTRAIN' AND COLUMN_NAME = 'GLTRAIN'";
                    comm1.CommandText = sql1;
                    dr1 = comm1.GetFirstDataRow();
                    hasdata = false;
                    if (dr1 != null)
                    {
                        int count1 = int.Parse(dr1[0].ToString());
                        if (count1 > 0) hasdata = true;
                    }

                    if (hasdata == false)
                    {
                        sql1 = "alter table NEWTRAIN add GLTRAIN varchar2(50)";
                        comm1.CommandText = sql1;
                        comm1.ExecuteNoQuery();
                    }

                    //修改其他的数据--关联月份
                    sql1 = "SELECT COUNT(*) FROM USER_TAB_COLUMNS WHERE TABLE_NAME = 'NEWTRAIN' AND COLUMN_NAME = 'GLMONTH'";
                    comm1.CommandText = sql1;
                    dr1 = comm1.GetFirstDataRow();
                    hasdata = false;
                    if (dr1 != null)
                    {
                        int count1 = int.Parse(dr1[0].ToString());
                        if (count1 > 0) hasdata = true;
                    }

                    if (hasdata == false)
                    {
                        sql1 = "alter table NEWTRAIN add GLMONTH integer";
                        comm1.CommandText = sql1;
                        comm1.ExecuteNoQuery();
                    }


                    //关闭数据库操作
                    comm1.Close();
                }
            }
        }


        ///public static void CreateStation
    }
}
