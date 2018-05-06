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
    public class DBUpgrade
    {
        public static void GoUpdate()
        {
                //2在LINESTATION中增加客专标示ID
                String sql2 = "SELECT COUNT(*) FROM USER_TAB_COLUMNS WHERE TABLE_NAME = 'LINESTATION' AND COLUMN_NAME = 'KZID'";
                JCommand comm2 = new JCommand();
                comm2.CommandText = sql2;
                DataRow dr2 = comm2.GetFirstDataRow();
                bool hasdata2 = false;
                if (dr2 != null)
                {
                    int count1 = int.Parse(dr2[0].ToString());
                    if (count1 > 0) hasdata2 = true;
                }

                if (hasdata2 == false)
                {
                    sql2 = "alter table LINESTATION add KZID int";
                    comm2.CommandText = sql2;
                    comm2.ExecuteNoQuery();
                }

                //3在LINESTATION中增加长交路标示ID
                String sql3 = "SELECT COUNT(*) FROM USER_TAB_COLUMNS WHERE TABLE_NAME = 'LINESTATION' AND COLUMN_NAME = 'CJLID'";
                JCommand comm3 = new JCommand();
                comm3.CommandText = sql3;
                DataRow dr3 = comm3.GetFirstDataRow();
                bool hasdata3 = false;
                if (dr3 != null)
                {
                    int count1 = int.Parse(dr3[0].ToString());
                    if (count1 > 0) hasdata3 = true;
                }

                if (hasdata3 == false)
                {
                    sql3 = "alter table LINESTATION add CJLID int";
                    comm3.CommandText = sql3;
                    comm3.ExecuteNoQuery();
                }

                //关闭数据库操作
                comm2.Close();
                comm3.Close();

                String sqlExist = "select count(*) from user_tables where table_name = 'STATIONINFO'";
                //4在增加站点快捷信息表
                String sql4 = "create table STATIONINFO(STATIONNAME VARCHAR(50) not null primary key,ABBNAME VARCHAR(50) not null, WHOLESPELL VARCHAR(50) not null)";
                String []sqlval = new String[]{"insert into STATIONINFO values('武汉','wh','wuhan')"};
                JCommand comm4 = new JCommand();
                comm4.CommandText = sqlExist;
                bool hasdata4 = false;
                DataRow dr4 = comm4.GetFirstDataRow();
                if (dr4 != null)
                {
                    int count1 = int.Parse(dr4[0].ToString());
                    if (count1 > 0) hasdata4 = true;
                }
                if (hasdata4 == false)
                {
                    comm4.CommandText = sql4;
                    comm4.ExecuteNoQuery();

                    comm4.CommandText = sqlval[0];
                    comm4.ExecuteNoQuery();
                }

                //5增加客专电费表
                sqlExist = "select count(*) from user_tables where table_name = 'GSCORPELECFEE'";
                String sql5 = " CREATE TABLE GSCORPELECFEE(	NUM int not null primary key,CORPNAME varchar(50) not null,RWBUREAU varchar(50) not null,"
                                + "NETFEE int not null,ELECFEE int not null) ";
                sqlval = new String[]{"insert into GSCORPELECFEE (NUM, CORPNAME, RWBUREAU,NETFEE,ELECFEE)values (6, '京津城际', '北京局',300,400)",
                                      "insert into GSCORPELECFEE (NUM, CORPNAME, RWBUREAU,NETFEE,ELECFEE)values (8, '石太客专', '太原局',280,300)",
                                      "insert into GSCORPELECFEE (NUM, CORPNAME, RWBUREAU,NETFEE,ELECFEE)values (7, '石太客专', '北京局',280,300)"};

                JCommand comm5 = new JCommand();
                comm5.CommandText = sqlExist;
                bool hasdata5 = false;
                DataRow dr5 = comm5.GetFirstDataRow();
                if (dr5 != null)
                {
                    int count1 = int.Parse(dr5[0].ToString());
                    if (count1 > 0) hasdata5 = true;
                }
                if (hasdata5 == false)
                {
                    comm5.CommandText = sql5;
                    comm5.ExecuteNoQuery();

                    comm5.CommandText = "create sequence GSCorpElecFee_Sequence increment by 1 start with 1 minvalue 1 maxvalue 9999999999999 nocache order";
                    comm5.ExecuteNoQuery();

                    comm5.CommandText = "create or replace trigger GSCorpElecFee_trigger before insert on GSCorpElecFee for each row begin select GSCorpElecFee_Sequence.nextval into:new.num from sys.dual; end;";
                    comm5.ExecuteNoQuery();

                    for (int i = 0; i < sqlval.Length;i++ )
                    {
                        comm5.CommandText = sqlval[i];
                        comm5.ExecuteNoQuery();
                    }
                }

                 //6增加长交路牵引费表
                sqlExist = "select count(*) from user_tables where table_name = 'GTTRAINDRAGFEE'";
                String sql6 = "CREATE TABLE GTTRAINDRAGFEE(	NUM int not null primary key,LINETYPE varchar(150) not null,CROSSROAD varchar(150) not null,"
                               + "MACTYPE varchar(50) not null,DRAGFEE int not null,NETFEE int)";
                sqlval = new String[]{"insert into GTTRAINDRAGFEE(NUM, LINETYPE,CROSSROAD,MACTYPE,DRAGFEE)values (4, '焦柳,宁西,宁启线', '襄樊-合肥-南通','内燃',418)",
                                      "insert into GTTRAINDRAGFEE(NUM, LINETYPE,CROSSROAD,MACTYPE,DRAGFEE)values (5, '合九,京九线', '连云港（东）-合肥-南昌','内燃',280)",
                                      "insert into GTTRAINDRAGFEE(NUM, LINETYPE,CROSSROAD,MACTYPE,DRAGFEE)values (3, '武九，京九线', '南昌-深圳西','内燃',418)"};

                JCommand comm6 = new JCommand();
                comm6.CommandText = sqlExist;
                bool hasdata6 = false;
                DataRow dr6 = comm6.GetFirstDataRow();
                if (dr6 != null)
                {
                    int count1 = int.Parse(dr6[0].ToString());
                    if (count1 > 0) hasdata6 = true;
                }
                if (hasdata6 == false)
                {
                    comm6.CommandText = sql6;
                    comm6.ExecuteNoQuery();

                    comm6.CommandText = "create sequence GTTrainDragFee_Sequence increment by 1 start with 1 minvalue 1 maxvalue 9999999999999 nocache order";
                    comm6.ExecuteNoQuery();

                    comm6.CommandText = "create or replace trigger GTTrainDragFee_trigger before insert on GTTrainDragFee for each row begin select GTTrainDragFee_Sequence.nextval into:new.num from sys.dual; end;";
                    comm6.ExecuteNoQuery();

                    for (int i = 0; i < sqlval.Length; i++)
                    {
                        comm6.CommandText = sqlval[i];
                        comm6.ExecuteNoQuery();
                    }
                }

                comm4.Close();
                comm5.Close();
                comm6.Close();

                sqlExist = "select count(*) from user_tables where table_name = 'CORPINFO'";
                String sql7 = "CREATE TABLE CORPINFO(CORPNAME varchar(50) not null primary key, ABBNAME varchar(50) not null,WHOLESPELL varchar(50) not null)";
                sqlval = new String[]{"insert into CORPINFO(CORPNAME, ABBNAME, WHOLESPELL)values ('京津城际', 'jjcj','jingjinchengji')",
                                      "insert into CORPINFO(CORPNAME, ABBNAME, WHOLESPELL)values ('石太客专', 'stkz','shitaikezhuan')",
                                      "insert into CORPINFO(CORPNAME, ABBNAME, WHOLESPELL)values ('郑西客专', 'zxkz','zhengxikezhuan')",
                                      "insert into CORPINFO(CORPNAME, ABBNAME, WHOLESPELL)values ('沪汉蓉公司', 'hhrgs','huhanronggongsi')",
                                      "insert into CORPINFO(CORPNAME, ABBNAME, WHOLESPELL)values ('武广客专', 'wgkz','wuguangkezhuan')",
                                      "insert into CORPINFO(CORPNAME, ABBNAME, WHOLESPELL)values ('胶济客专', 'jjkz','jiaojikezhaun')",
                                      "insert into CORPINFO(CORPNAME, ABBNAME, WHOLESPELL)values ('沪宁公司', 'hngs','huninggongsi')",
                                      "insert into CORPINFO(CORPNAME, ABBNAME, WHOLESPELL)values ('沪杭公司', 'hhgs','huhanggongsi')",
                                      "insert into CORPINFO(CORPNAME, ABBNAME, WHOLESPELL)values ('合武安徽', 'hwah','hewuanhui')",
                                      "insert into CORPINFO(CORPNAME, ABBNAME, WHOLESPELL)values ('浙江沿海', 'zjyh','zhejiangyanhai')",
                                      "insert into CORPINFO(CORPNAME, ABBNAME, WHOLESPELL)values ('昌九城际', 'cjcj','changjiuchengji')",
                                      "insert into CORPINFO(CORPNAME, ABBNAME, WHOLESPELL)values ('东南沿海', 'dnyh','dongnanyanhai')",
                                      "insert into CORPINFO(CORPNAME, ABBNAME, WHOLESPELL)values ('市域公司', 'sygs','shiyugongsi')",
                                      "insert into CORPINFO(CORPNAME, ABBNAME, WHOLESPELL)values ('哈大客专', 'hdkz','hadakezhuan')",
                                      "insert into CORPINFO(CORPNAME, ABBNAME, WHOLESPELL)values ('京石客专', 'jskz','jingshikezhuan')",
                                      "insert into CORPINFO(CORPNAME, ABBNAME, WHOLESPELL)values ('石武客专', 'swkz','shiwukezhuan')",
                                      "insert into CORPINFO(CORPNAME, ABBNAME, WHOLESPELL)values ('长吉城际', 'cjcj','changjichengji')",
                                      "insert into CORPINFO(CORPNAME, ABBNAME, WHOLESPELL)values ('广深港公司', 'gsggs','guangshenganggongsi')",
                                      "insert into CORPINFO(CORPNAME, ABBNAME, WHOLESPELL)values ('广珠城际', 'gzcj','guangzhuchengji')",
                                      "insert into CORPINFO(CORPNAME, ABBNAME, WHOLESPELL)values ('海南东环', 'hndh','hainandonghuan')",
                                      "insert into CORPINFO(CORPNAME, ABBNAME, WHOLESPELL)values ('京沪客专', 'jhkz','jinghukezhuan')",
                                      "insert into CORPINFO(CORPNAME, ABBNAME, WHOLESPELL)values ('龙岩公司(龙彰线)', 'lygs','longyangongsi')"};

                JCommand comm7 = new JCommand();
                comm7.CommandText = sqlExist;
                bool hasdata7 = false;
                DataRow dr7 = comm7.GetFirstDataRow();
                if (dr7 != null)
                {
                    int count1 = int.Parse(dr7[0].ToString());
                    if (count1 > 0) hasdata7 = true;
                }
                if (hasdata7 == false)
                {
                    comm7.CommandText = sql7;
                    comm7.ExecuteNoQuery();

                    for (int i = 0; i < sqlval.Length; i++)
                    {
                        comm7.CommandText = sqlval[i];
                        comm7.ExecuteNoQuery();
                    }
                }

                comm7.Close();
            }
        }
 }
