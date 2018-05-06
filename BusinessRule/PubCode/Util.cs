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
using org.in2bits.MyXls;

namespace BusinessRule.PubCode
{
    public class Util
    {
        #region 数据的合并
        //合并担担车收入
        public static int MergeData_SHOUROU()
        {
            JConnect conn1 = JConnect.GetConnect();
            conn1.BeginTrans();
            int count = 0;
            JTable tab1=null;
            try
            {
                tab1 = new JTable(conn1,"");
                tab1.TableName = "NEWTRAINSHOUROU";
                tab1.OrderBy = "byear,bmonth,trainname,astation,bstation";
                DataTable dt1 = tab1.SearchData(null, -1, "*").Tables[0];

                for (int i = dt1.Rows.Count - 1; i > 0; i--)
                {
                    DataRow dr1 = dt1.Rows[i];
                    DataRow dr0 = dt1.Rows[i - 1];
                    if (dr1["byear"].ToString() == dr0["byear"].ToString()
                        && dr1["bmonth"].ToString() == dr0["bmonth"].ToString()
                        && dr1["trainname"].ToString() == dr0["trainname"].ToString()
                        && dr1["astation"].ToString() == dr0["astation"].ToString()
                        && dr1["bstation"].ToString() == dr0["bstation"].ToString()
                        )
                    {
                        for (int k = 1; k <= 6; k++)
                        {
                            dr0["SHOUROU" + k] = double.Parse(dr0["SHOUROU" + k].ToString())
                                + double.Parse(dr1["SHOUROU" + k].ToString());
                        }
                        dr1.Delete();
                        count++;
                    }
                }
                if (count > 0)
                {
                    tab1.Update(dt1);
                }
                conn1.CommitTrans();
            }
            catch (Exception err)
            {
                conn1.RollBackTrans();
                count = 0;
            }
            if(tab1!=null) tab1.Close();

            return count;
        }

        //合并数据
        /// <summary>
        /// kind=0 --- 担担车收入
        /// kind=1 --- 线路使用费
        /// kind=2 --- 机车牵引费
        /// kind=3 --- 电网和接触网费 
        /// kind=4 --- 列车运输人数
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static int MergeData(int kind)
        {
            JConnect conn1 = JConnect.GetConnect();
            conn1.BeginTrans();
            int count = 0;
            JTable tab1 = null;
            String tableName = String.Empty;
            String[] fs = null;

            if (kind == 0)
            {
                tableName = "NEWTRAINSHOUROU";
                fs = new String[] { "SHOUROU1", "SHOUROU2","SHOUROU3","SHOUROU4","SHOUROU5","SHOUROU6" };
            }
            else if (kind == 1)
            {
                tableName ="NEWTRAINXIANLUFEE";
                fs=new String[] { "GONGLI1", "GONGLI2","GONGLI3","FEE1","FEE2","FEE3" };
            }
            else if (kind == 2)
            {
                tableName = "NEWTRAINQIANYINFEE";
                fs = new String[] { "ZL1", "ZL2", "ZL3", "FEE1", "FEE2", "FEE3" };
            }
            else if (kind == 3)
            {
                tableName = "NEWTRAINSERVERPEOPLE";
                fs = new String[] { "PC1", "PC2", "PC3", "FEE1", "FEE2", "FEE3" };
            }

            if (String.IsNullOrEmpty(tableName) == false)
            {
                try
                {
                    tab1 = new JTable(conn1, "");
                    tab1.TableName = tableName;
                    tab1.OrderBy = "byear,bmonth,trainname,astation,bstation";
                    DataTable dt1 = tab1.SearchData(null, -1, "*").Tables[0];

                    for (int i = dt1.Rows.Count - 1; i > 0; i--)
                    {
                        DataRow dr1 = dt1.Rows[i];
                        DataRow dr0 = dt1.Rows[i - 1];
                        if (dr1["byear"].ToString() == dr0["byear"].ToString()
                            && dr1["bmonth"].ToString() == dr0["bmonth"].ToString()
                            && dr1["trainname"].ToString() == dr0["trainname"].ToString()
                            && dr1["astation"].ToString() == dr0["astation"].ToString()
                            && dr1["bstation"].ToString() == dr0["bstation"].ToString()
                            )
                        {
                            foreach (String m in fs)
                            {
                                dr0[m] = double.Parse(dr0[m].ToString()) + double.Parse(dr1[m].ToString());
                            }
                            dr1.Delete();
                            count++;
                        }
                    }
                    if (count > 0)
                    {
                        tab1.Update(dt1);
                    }
                    conn1.CommitTrans();
                }
                catch (Exception err)
                {
                    conn1.RollBackTrans();
                    count = 0;
                }
                if (tab1 != null) tab1.Close();
            }

            return count;
        }

        #endregion

        //得到默认的车底数
        public static double GetDefaultCds(int total, ETrainType type1)
        {
            double result = 1;
            double  speed = 0;
            int type0=(int)type1;

            if (type1 == ETrainType.空调车25G || type1 == ETrainType.空调车25K
                || type1 == ETrainType.空调车25T || type1 == ETrainType.绿皮车25B)
            {
                result = 2;
                speed=double.Parse(CheXianProfile.Data[(ECommTrainType)type0].SPEED);

                if (speed > 0)
                {
                    if (total < 1000)
                    {
                        double total1 = total * 1.5;
                        double t1 = (total1 / speed) * 2 / 24.0 + 0.00001;
                        result = Math.Ceiling(t1);
                    }
                    else
                    {
                        double total1 = total;
                        double t1 = (total1 / speed) * 2 / 24.0 + 0.00001;
                        result = Math.Ceiling(t1);
                        if (result < 2) result = 2;
                    }
                }
            }
            else
            {
                result = 1;
                EHighTrainType type2 = (EHighTrainType)type0;
                DataTable dt1 = HighTrainProfile.Data;
                if (dt1.Rows.Count > 0)
                {
                    DataRow[] drs = dt1.Select("HIGHTRAINTYPE='"+type2.ToString()+"'");
                    if (drs != null && drs.Length > 0)
                    {
                        speed = double.Parse(drs[0]["speed"].ToString());
                    }
                }

                if (speed > 0)
                {
                    double total1 = total;
                    double t1 = (total1 / speed) * 2 / 18.0 + 0.00001;
                    result = Math.Ceiling(t1);
                }
            }
            

            /*
            if (type1 == ETrainType.空调车25G || type1 == ETrainType.空调车25K
                || type1 == ETrainType.空调车25T || type1 == ETrainType.绿皮车25B)
            {
                if (result < 2) result = 2;
            }
            else
            {
                if (result < 1) result = 1;
            }*/

            return result;
        }

        //得到默认的天数
        public static double GetDefaultCds1(int total, ETrainType type1)
        {
            double result = 1;
            double speed = 0;
            int type0 = (int)type1;

            if (type1 == ETrainType.空调车25G || type1 == ETrainType.空调车25K
                || type1 == ETrainType.空调车25T || type1 == ETrainType.绿皮车25B)
            {
                result = 2;
                speed = double.Parse(CheXianProfile.Data[(ECommTrainType)type0].SPEED);
            }
            else
            {
                result = 1;
                EHighTrainType type2 = (EHighTrainType)type0;
                DataTable dt1 = HighTrainProfile.Data;
                if (dt1.Rows.Count > 0)
                {
                    DataRow[] drs = dt1.Select("HIGHTRAINTYPE='" + type2.ToString() + "'");
                    if (drs != null && drs.Length > 0)
                    {
                        speed = double.Parse(drs[0]["speed"].ToString());
                    }
                }
            }


            if (speed > 0)
            {
                double temp1 = (total / speed) * 2;
                if (temp1 < 24)
                {
                    temp1 = ((total / speed) * 2) / 16;
                }
                else
                {
                    temp1 = temp1 / 24;
                }
                result = Math.Ceiling(temp1);
            }

            return result;
        }


        public static String ExportData(String modelName)
        {
            String FileName = String.Empty;
            /*------------------------------------------------------*/
            return String.Empty;
        }
        /// <summary>
        /// 读取XLS数据到数据库
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static DataSet xsldata(string filepath,String SheetName)
        {
            string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
            System.Data.OleDb.OleDbConnection Conn = new System.Data.OleDb.OleDbConnection(strCon);

            Conn.Open();
            DataTable dt = Conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            String tableName = dt.Rows[0][2].ToString().Trim();

            string strCom = String.Format("SELECT * FROM [{0}]", tableName);

           
            System.Data.OleDb.OleDbDataAdapter myCommand = new System.Data.OleDb.OleDbDataAdapter(strCom, Conn);
            DataSet ds = new DataSet();
            myCommand.Fill(ds);
            Conn.Close();
            return ds;
        }

        /// <summary>
        /// 得到线路级别
        /// </summary>
        /// <returns></returns>
        public static Dictionary<String, int> GetLineClass()
        {
            Dictionary<String, int> LineClass = new Dictionary<string, int>();
            LineClass.Add("特1", 1);    /*--300公里----*/
            LineClass.Add("特2", 2);    /*--200公里----*/
            LineClass.Add("1+", 3);     /*--普通线路----*/
            LineClass.Add("1", 4);
            LineClass.Add("2+", 5);
            LineClass.Add("2", 6);
            LineClass.Add("3", 7);
            LineClass.Add("3-", 8);
            return LineClass;
        }

        /// <summary>
        /// 将XLS列车数据导入到系统中
        /// </summary>
        /// <returns></returns>
        public static DataSet GetTrainDataToSystem()
        {
            DataSet ds1 = null;
            return ds1;
        }


        /// <summary>
        /// 将列车现有的数据导入到XLS表中
        /// </summary>
        public static void ImportTrainDataToSystem(String FileName)
        {
            String[] fs = new String[] { "车次","始发站","终到站","列车类型",
                                         "单程距离","开行趟数","车底组数","硬座","软座",
                                         "硬卧","软卧","餐车","发电车","行李车","邮政车"
                                        };

            String[] fs1 = new String[] {"TRAINNAME","ASTATION","BSTATION","TRAINTYPE",
                                         "MILE","KXTS","CDZS","YINZUO","RUANZUO","OPENYINWO" 
                                         ,"RUANWO","CANCHE","FADIANCHE","SHUYINCHE","YOUZHENGCHE"};

            JTable tab1 = new JTable("NEWTRAIN");
            Dictionary<String, object> data1 = new Dictionary<string, object>();
            int num = 1;

            String FileName1 = HttpContext.Current.Server.MapPath("~/Attachment/" + FileName);
            if (File.Exists(FileName1))
            {
                DataSet ds1 = PubCode.Util.xsldata(FileName1, "Table1");
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                  
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        if (dr1["车次"].ToString().Trim() != String.Empty)
                        {
                            data1.Clear();
                            for (int i = 0; i < fs.Length; i++)
                            {
                                object obj1 = dr1[fs[i]];

                                data1.Add(fs1[i], obj1);
                            }
                            data1.Add("num", num);
                            data1.Add("TRAINBIGKIND", 0);
                            num++;
                            tab1.InsertData(data1);
                        }
                    }
                }
            }
            tab1.Close();
        }


        /// <summary>
        /// 将列车的收入数据导入到系统中
        /// </summary>
        public static void ImportTrainShouRouDataToSystem(String FileName, bool Append)
        {
            JTable tab1 = new JTable("NEWTRAINSHOUROU");
            Dictionary<String, object> data1 = new Dictionary<string, object>();
         
            String FileName1 = HttpContext.Current.Server.MapPath("~/Attachment/" + FileName);
            if (File.Exists(FileName1))
            {
                DataSet ds1 = PubCode.Util.xsldata(FileName1, "Table1");
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    String[] fs1 = new String[] {"TRAINNAME","ASTATION","BSTATION","SHOUROU1",
                                                 "SHOUROU2","SHOUROU3","SHOUROU4","SHOUROU5","SHOUROU6"};

                    String[] fs2 = new String[] { "YinZuo", "RuanZuo", "OpenYinWo", "CloseYinWo", "RuanWo", 
                                          "AdvanceRuanWo", "CanChe", "FaDianChe", "ShuYinChe", "YouZhengChe" };

                    int index = 0;
                    int year = DateTime.Now.Year;
                    int month = DateTime.Now.Month;
                    DataSet oldData = null;                   //表示以前的老数据
                    String[] vArr = new String[] {"SHOUROU1","SHOUROU2","SHOUROU3","SHOUROU4","SHOUROU5","SHOUROU6" };
                    JTable tab2 = new JTable("NEWTRAINSHOUROU");

                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        if (index == 0)
                        {
                            String str1 = dr1[3].ToString().Replace("－","-"); //清算日期：2011年12月01日－2011年12月31日
                            String str0 = str1.Split('-')[1];
                            DateTime time0 = DateTime.Parse(str0);
                            year = time0.Year;
                            month = time0.Month;

                            //删除以前的老数据
                            List<SearchField> condition = new List<SearchField>();
                            condition.Add(new SearchField("byear", year + "", SearchFieldType.NumericType));
                            condition.Add(new SearchField("bmonth", month + "", SearchFieldType.NumericType));
                            
                            if (Append == false)
                            {
                                tab2.DeleteData(condition);  //删除以前的数据
                            }
                            oldData = tab2.SearchData(condition, -1, "*");
                        }

                        if (index >= 2)   //开始导入数据
                        {
                            if (dr1[0].ToString() != String.Empty
                                     && dr1[0].ToString() != "合计"
                                     && dr1[1].ToString() != String.Empty
                                     && dr1[2].ToString() != String.Empty)
                            {
                                //导入新数据
                                int col = 0;
                                data1.Clear();
                                foreach (String m in fs1)
                                {
                                    data1[m] = dr1[col];
                                    col++;
                                }

                                if (data1.Count > 0)
                                {
                                    data1["byear"] = year;
                                    data1["bmonth"] = month;

                                    data1["SHOUROU1"] = data1["SHOUROU1"].ToString().Replace(",", "");
                                    data1["SHOUROU2"] = data1["SHOUROU2"].ToString().Replace(",", "");
                                    data1["SHOUROU3"] = data1["SHOUROU3"].ToString().Replace(",", "");
                                    data1["SHOUROU4"] = data1["SHOUROU4"].ToString().Replace(",", "");
                                    data1["SHOUROU5"] = data1["SHOUROU5"].ToString().Replace(",", "");
                                    data1["SHOUROU6"] = data1["SHOUROU6"].ToString().Replace(",", "");

                                    //得到列车默认的车厢配置数据
                                    int[] bianzhu = GetTrainDefaultBianZhu(data1["TRAINNAME"].ToString());
                                    for (int i = 0; i < bianzhu.Length; i++)
                                    {
                                        if (bianzhu[i] != 0)
                                        {
                                            data1[fs2[i]] = bianzhu[i];
                                        }
                                    }

                                    String filter = "TRAINNAME='{0}' and ASTATION='{1}' and BSTATION='{2}' and byear={3} and bmonth={4}";
                                    filter = string.Format(filter, data1["TRAINNAME"], data1["ASTATION"], data1["BSTATION"],data1["byear"],data1["bmonth"]);
                                    DataRow[] oldRows = oldData.Tables[0].Select(filter);
                                    DataRow oldRow = null;
                                    if (oldRows.Length > 0)
                                    {
                                        oldRow = oldRows[0];
                                        foreach (String m1 in vArr)
                                        {
                                            if (oldRow[m1].ToString().Trim() == String.Empty)
                                            {
                                                if (data1[m1].ToString().Trim() != String.Empty)
                                                {
                                                    oldRow[m1] = data1[m1];
                                                }
                                            }
                                            else
                                            {
                                                if (data1[m1].ToString().Trim() != String.Empty)
                                                {
                                                    oldRow[m1] = double.Parse(oldRow[m1].ToString())+ double.Parse(data1[m1].ToString());
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        oldRow = oldData.Tables[0].NewRow();
                                        foreach (KeyValuePair<string, object> k in data1)
                                        {
                                            oldRow[k.Key] = k.Value;   //新增设备
                                        }
                                        oldData.Tables[0].Rows.Add(oldRow);
                                    }

                                    /*
                                    if (Append == false)
                                    {
                                        tab1.InsertData(data1);
                                    }
                                    else
                                    {
                                        
                                    }*/
                                }
                            }
                        }

                        index++;
                    }

                    if (oldData != null)
                    {
                        tab2.Update(oldData.Tables[0]);
                    }
                    tab2.Close();
                }
            }
            tab1.Close();
        }

        /// <summary>
        /// 将列车的线路使用费导入系统
        /// </summary>
        public static void ImportTrainXianLuFeeToSystem(String FileName,bool Append)
        {
            JTable tab1 = new JTable("NEWTRAINXIANLUFEE");
            Dictionary<String, object> data1 = new Dictionary<string, object>();

            String FileName1 = HttpContext.Current.Server.MapPath("~/Attachment/" + FileName);
            if (File.Exists(FileName1))
            {
                DataSet ds1 = PubCode.Util.xsldata(FileName1, "Table1");
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    String[] fs1 = new String[] {"TRAINNAME","ASTATION","BSTATION","GONGLI1",
                                                 "GONGLI2","GONGLI3","Fee1","Fee2","Fee3"};
                    int[] col = new int[] { 0,1,2,3,4,5,7,8,9};


                    int index = 0;
                    int year = DateTime.Now.Year;
                    int month = DateTime.Now.Month;
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        if (index == 0)
                        {
                            String str1 = dr1[4].ToString().Replace("－", "-"); //清算日期：2011年12月01日－2011年12月31日
                            String str0 = str1.Split('-')[1];
                            DateTime time0 = DateTime.Parse(str0);
                            year = time0.Year;
                            month = time0.Month;

                            //删除以前的老数据
                            if (Append == false)
                            {
                                List<SearchField> condition = new List<SearchField>();
                                condition.Add(new SearchField("byear", year + "", SearchFieldType.NumericType));
                                condition.Add(new SearchField("bmonth", month + "", SearchFieldType.NumericType));
                                JTable tab2 = new JTable("NEWTRAINXIANLUFEE");
                                tab2.DeleteData(condition);
                                tab2.Close();
                            }
                        }

                        if (index >= 2)   //开始导入数据
                        {
                            if (dr1[0].ToString() != String.Empty
                                     && dr1[0].ToString() != "合计"
                                     && dr1[1].ToString() != String.Empty
                                     && dr1[2].ToString() != String.Empty)
                            {
                                data1.Clear();
                                for(int i=0;i<fs1.Length;i++)
                                {
                                    data1[fs1[i]] = dr1[col[i]];
                                }

                                if (data1.Count > 0)
                                {
                                    data1["byear"] = year;
                                    data1["bmonth"] = month;

                                    for (int j = 3; j <= 8; j++)
                                    {
                                        data1[fs1[j]] = data1[fs1[j]].ToString().Replace(",", "");
                                        if (data1[fs1[j]].ToString().Trim() == "-")
                                        {
                                            data1[fs1[j]] = "0";
                                        }
                                    }

                                    tab1.InsertData(data1);
                                }
                            }
                        }

                        index++;
                    }
                }
            }
            tab1.Close();
        }

        /// <summary>
        /// 将列车的机车牵引费导入到系统
        /// </summary>
        public static void ImportTrainPersonCountToSystem(String FileName, bool Append)
        {
            JTable tab1 = new JTable("NEWTRAINSERVERPEOPLE");
            try
            {
                Dictionary<String, object> data1 = new Dictionary<string, object>();

                String FileName1 = HttpContext.Current.Server.MapPath("~/Attachment/" + FileName);
                if (File.Exists(FileName1))
                {
                    DataSet ds1 = PubCode.Util.xsldata(FileName1, "Table1");
                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        String[] fs1 = new String[] {"TRAINNAME","ASTATION","BSTATION","PC1",
                                                 "PC2","PC3","PRICE","Fee1","Fee2","Fee3"};
                        int[] col = new int[] { 0, 1, 2, 3, 4, 5, 7, 8, 9, 10 };


                        int index = 0;
                        int year = DateTime.Now.Year;
                        int month = DateTime.Now.Month;
                        foreach (DataRow dr1 in ds1.Tables[0].Rows)
                        {
                            if (index == 0)
                            {
                                String str1 = dr1[3].ToString().Replace("－－", "-").Replace("--", "-").Replace("－", "-"); //清算日期：2011年12月01日－2011年12月31日
                                String str0 = str1.Split('-')[1];
                                DateTime time0 = DateTime.Parse(str0);
                                year = time0.Year;
                                month = time0.Month;

                                //删除以前的老数据
                                if (Append == false)
                                {
                                    List<SearchField> condition = new List<SearchField>();
                                    condition.Add(new SearchField("byear", year + "", SearchFieldType.NumericType));
                                    condition.Add(new SearchField("bmonth", month + "", SearchFieldType.NumericType));
                                    JTable tab2 = new JTable("NEWTRAINSERVERPEOPLE");
                                    tab2.DeleteData(condition);
                                    tab2.Close();
                                }
                            }

                            if (index >= 2)   //开始导入数据
                            {
                                if (dr1[0].ToString() != String.Empty
                                         && dr1[0].ToString() != "合计"
                                         && dr1[1].ToString() != String.Empty
                                         && dr1[2].ToString() != String.Empty)
                                {
                                    data1.Clear();
                                    for (int i = 0; i < fs1.Length; i++)
                                    {
                                        data1[fs1[i]] = dr1[col[i]];
                                    }

                                    if (data1.Count > 0)
                                    {
                                        data1["byear"] = year;
                                        data1["bmonth"] = month;

                                        for (int j = 3; j <= 9; j++)
                                        {
                                            data1[fs1[j]] = data1[fs1[j]].ToString().Replace(",", "");
                                            if (data1[fs1[j]].ToString().Trim() == "-")
                                            {
                                                data1[fs1[j]] = "0";
                                            }
                                        }

                                        tab1.InsertData(data1);
                                    }
                                }
                            }

                            index++;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                tab1.Close();
            }
        }

        /// <summary>
        /// 将列车的机车牵引费导入到系统
        /// </summary>
        public static void ImportTrainQianYinFeeToSystem(String FileName,bool Append)
        {
            JTable tab1 = new JTable("NEWTRAINQIANYINFEE");
            Dictionary<String, object> data1 = new Dictionary<string, object>();

            String FileName1 = HttpContext.Current.Server.MapPath("~/Attachment/" + FileName);
            if (File.Exists(FileName1))
            {
                DataSet ds1 = PubCode.Util.xsldata(FileName1, "Table1");
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    String[] fs1 = new String[] {"TRAINNAME","ASTATION","BSTATION","ZL1",
                                                 "ZL2","ZL3","Fee1","Fee2","Fee3"};
                    int[] col = new int[] { 0, 1, 2, 3, 4, 5, 7, 8, 9 };


                    int index = 0;
                    int year = DateTime.Now.Year;
                    int month = DateTime.Now.Month;
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        if (index == 0)
                        {
                            String str1 = dr1[4].ToString().Replace("－", "-"); //清算日期：2011年12月01日－2011年12月31日
                            String str0 = str1.Split('-')[1];
                            DateTime time0 = DateTime.Parse(str0);
                            year = time0.Year;
                            month = time0.Month;

                            //删除以前的老数据
                            if (Append == false)
                            {
                                List<SearchField> condition = new List<SearchField>();
                                condition.Add(new SearchField("byear", year + "", SearchFieldType.NumericType));
                                condition.Add(new SearchField("bmonth", month + "", SearchFieldType.NumericType));
                                JTable tab2 = new JTable("NEWTRAINQIANYINFEE");
                                tab2.DeleteData(condition);
                                tab2.Close();
                            }
                        }

                        if (index >= 2)   //开始导入数据
                        {
                            if (dr1[0].ToString() != String.Empty
                                     && dr1[0].ToString() != "合计"
                                     && dr1[1].ToString() != String.Empty
                                     && dr1[2].ToString() != String.Empty)
                            {
                                data1.Clear();
                                for (int i = 0; i < fs1.Length; i++)
                                {
                                    data1[fs1[i]] = dr1[col[i]];
                                }

                                if (data1.Count > 0)
                                {
                                    data1["byear"] = year;
                                    data1["bmonth"] = month;

                                    for (int j = 3; j <= 8; j++)
                                    {
                                        data1[fs1[j]] = data1[fs1[j]].ToString().Replace(",", "");
                                        if (data1[fs1[j]].ToString().Trim() == "-")
                                        {
                                            data1[fs1[j]] = "0";
                                        }
                                    }

                                    tab1.InsertData(data1);
                                }
                            }
                        }

                        index++;
                    }
                }
            }
            tab1.Close();
        }

        /// <summary>
        /// 将列车的电费和接触网使用费导入到系统中
        /// </summary>
        public static void ImportTrainDianFeeToSystem(String FileName, bool Append)
        {
            JTable tab1 = new JTable("NewTrainDianFee");
            Dictionary<String, object> data1 = new Dictionary<string, object>();

            String FileName1 = HttpContext.Current.Server.MapPath("~/Attachment/" + FileName);
            if (File.Exists(FileName1))
            {
                DataSet ds1 = PubCode.Util.xsldata(FileName1, "Table1");
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    String[] fs1 = new String[] {"TRAINNAME","ASTATION","BSTATION","ZL1",
                                                 "ZL2","ZL3","Fee1","Fee2","Fee3"};
                    int[] col = new int[] { 0, 1, 2, 3, 4, 5, 7, 8, 9 };


                    int index = 0;
                    int year = DateTime.Now.Year;
                    int month = DateTime.Now.Month;
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        if (index == 0)
                        {
                            String str1 = dr1[4].ToString().Replace("－", "-"); //清算日期：2011年12月01日－2011年12月31日
                            String str0 = str1.Split('-')[1];
                            DateTime time0 = DateTime.Parse(str0);
                            year = time0.Year;
                            month = time0.Month;

                            //删除以前的老数据
                            if (Append == false)
                            {
                                List<SearchField> condition = new List<SearchField>();
                                condition.Add(new SearchField("byear", year + "", SearchFieldType.NumericType));
                                condition.Add(new SearchField("bmonth", month + "", SearchFieldType.NumericType));
                                JTable tab2 = new JTable("NewTrainDianFee");
                                tab2.DeleteData(condition);
                                tab2.Close();
                            }
                        }

                        if (index >= 2)   //开始导入数据
                        {
                            if (dr1[0].ToString() != String.Empty
                                     && dr1[0].ToString() != "合计"
                                     && dr1[1].ToString() != String.Empty
                                     && dr1[2].ToString() != String.Empty)
                            {
                                data1.Clear();
                                for (int i = 0; i < fs1.Length; i++)
                                {
                                    data1[fs1[i]] = dr1[col[i]];
                                }

                                if (data1.Count > 0)
                                {
                                    data1["byear"] = year;
                                    data1["bmonth"] = month;

                                    for (int j = 3; j <= 8; j++)
                                    {
                                        data1[fs1[j]] = data1[fs1[j]].ToString().Replace(",", "");
                                        if (data1[fs1[j]].ToString().Trim() == "-")
                                        {
                                            data1[fs1[j]] = "0";
                                        }
                                    }

                                    tab1.InsertData(data1);
                                }
                            }
                        }

                        index++;
                    }
                }
            }
            tab1.Close();
        }

        /// <summary>
        /// 将线路站点数据导入导入到系统中
        /// </summary>
        /// <param name="FileName"></param>
        public static void importTrainLineDataToSystem(String FileName)
        {
            String FileName1 = HttpContext.Current.Server.MapPath("~/Attachment/" + FileName);
            if (File.Exists(FileName1))
            {
                //得到线路站点的数据
                JTable tab1 = new JTable("TrainLine");
                tab1.OrderBy = "LineName";
                DataTable dt0 = tab1.SearchData(null, -1, "*").Tables[0];


                string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
                System.Data.OleDb.OleDbConnection Conn = new System.Data.OleDb.OleDbConnection(strCon);

                Conn.Open();
                DataTable dt = Conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    String tableName = dt.Rows[i][2].ToString().Trim();
                    foreach (DataRow dr0 in dt0.Rows)
                    {
                        String AStation = dr0["AStation"].ToString();
                        String BStation = dr0["BStation"].ToString();
                        int miles0 = int.Parse(dr0["Miles"].ToString());

                        DataTable dt1 = GetLineStationTable(Conn, tableName, dr0["LineName"].ToString(),
                            out AStation, out BStation, out miles0);

                        if (dt1.Rows.Count > 0)   //表示有数据
                        {
                            dr0["Astation"] = AStation;
                            dr0["Bstation"] = BStation;
                            dr0["miles"] = miles0;

                            //更新线路中的数据
                            Line.SetMiddleStation(dr0["LineID"].ToString(), dt1);
                        }
                    }
                    tab1.Update(dt0);       //更新老站点中的相关数据
                }


                Conn.Close();

                //得到线路的数据
                tab1.Close();

            }
        }

        /// <summary>
        /// 将客专公司电费导入到系统中
        /// </summary>
        /// <param name="FileName"></param>
        public static void ImportGSCorpElecFeeToSystem(String FileName)
        {
            String FileName1 = HttpContext.Current.Server.MapPath("~/Attachment/" + FileName);
            JTable tab1 = new JTable("GSCORPELECFEE");
            Dictionary<String, object> data1 = new Dictionary<string, object>();

            String[] fs = new String[] { "公司名称","铁路局","接触网使用费","电费"};

            String[] fs1 = new String[] {"CORPNAME","RWBUREAU","NETFEE","ELECFEE"};


            if (File.Exists(FileName1))
            {
                DataSet ds1 = PubCode.Util.xsldata(FileName1, "Table1");
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        if (dr1["公司名称"].ToString().Trim() != String.Empty)
                        {
                            data1.Clear();
                            for (int i = 0; i < fs.Length; i++)
                            {
                                object obj1 = dr1[fs[i]];

                                data1.Add(fs1[i], obj1);
                            }
                            tab1.InsertData(data1);
                        }
                    }
                }

                //得到客专电费的数据
                tab1.Close();
            }
        }

        /// <summary>
        /// 将客运机车牵引费导入到系统中
        /// </summary>
        /// <param name="FileName"></param>
        public static void ImportGTTrainDragFeeToSystem(String FileName)
        {
            String FileName1 = HttpContext.Current.Server.MapPath("~/Attachment/" + FileName);
            JTable tab1 = new JTable("GTTRAINDRAGFEE");
            Dictionary<String, object> data1 = new Dictionary<string, object>();

            String[] fs = new String[] { "线别", "交路", "机种", "牵引费","接触网电费"};

            String[] fs1 = new String[] { "LINETYPE","CROSSROAD","MACTYPE","DRAGFEE", "NETFEE"};


            if (File.Exists(FileName1))
            {
                DataSet ds1 = PubCode.Util.xsldata(FileName1, "Table1");
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        if (dr1["线别"].ToString().Trim() != String.Empty)
                        {
                            data1.Clear();
                            for (int i = 0; i < fs.Length; i++)
                            {
                                object obj1 = dr1[fs[i]];

                                data1.Add(fs1[i], obj1);
                            }
                            tab1.InsertData(data1);
                        }
                    }
                }

                //得到客专电费的数据
                tab1.Close();
            }
        }

        private static DataTable GetLineStationTable(OleDbConnection Conn,
            String tableName,String LineName,out String begStation,
            out String endStation,out int miles0)
        {
            System.Data.DataTable dt1 = new System.Data.DataTable();
            dt1.Columns.Add("Astation");
            dt1.Columns.Add("Bstation");
            dt1.Columns.Add("Miles", typeof(int));

            dt1.Columns.Add("JnFlag");
            dt1.Columns.Add("Dqh");

            dt1.Columns.Add("ShipFlag");
            dt1.Columns.Add("Fee1",typeof(int));
            dt1.Columns.Add("Fee2",typeof(int));
           
            //设置默认值
            miles0 = 0;
            begStation = String.Empty;
            endStation = String.Empty;

            string strCom = String.Format("SELECT * FROM [{0}] where 线名='{1}' ", tableName, LineName);
            System.Data.OleDb.OleDbDataAdapter myCommand = new System.Data.OleDb.OleDbDataAdapter(strCom, Conn);
            DataSet ds = new DataSet();
            myCommand.Fill(ds);

            if (ds != null && ds.Tables[0].Rows.Count > 1)
            {
                DataTable dt = ds.Tables[0];
                if (dt.Columns.Contains("始发站") == false
                    && dt.Columns.Contains("到达站") == false)
                {
                    SetData1(dt, dt1, out begStation, out endStation, out miles0);
                }
                else
                {
                    SetData2(dt, dt1, out begStation, out endStation, out miles0);
                }
            }

            return dt1;
        }

        //数据导入格式1
        private static void SetData1(DataTable dt, DataTable dt1, out String begStation,
            out String endStation, out int miles0)
        {
            //设置默认值
            miles0 = 0;
            begStation = String.Empty;
            endStation = String.Empty;
            for (int i = 0; i < dt.Rows.Count - 1; i++)
            {
                String AStation = dt.Rows[i]["站名"].ToString();
                String BStation = dt.Rows[i + 1]["站名"].ToString();

                int mile = int.Parse(dt.Rows[i + 1]["里程"].ToString())
                    - int.Parse(dt.Rows[i]["里程"].ToString());

                System.Data.DataRow dr1 = dt1.NewRow();
                dr1["Astation"] = AStation;
                dr1["Bstation"] = BStation;
                dr1["Miles"] = mile;

                //设置局内标志
                if (dt.Columns.Contains("局内") && dt.Rows[i + 1]["局内"].ToString().Trim() != String.Empty)
                {
                    dr1["jnflag"] = "1";
                }

                //设置电气化标志
                if (dt.Columns.Contains("电气化") && dt.Rows[i + 1]["电气化"].ToString().Trim() != String.Empty)
                {
                    dr1["dqh"] = "1";
                }

                //设置轮渡标志
                if (dt.Columns.Contains("轮渡标志") && dt.Rows[i + 1]["轮渡标志"].ToString().Trim() != String.Empty)
                {
                    dr1["ShipFlag"] = "1";
                }

                //设置内燃牵引费 
                if (dt.Columns.Contains("内燃牵引费") && dt.Rows[i + 1]["内燃牵引费"].ToString().Trim() != String.Empty)
                {
                    dr1["Fee1"] = int.Parse(dt.Rows[i + 1]["内燃牵引费"].ToString().Trim());
                }

                //设置电力牵引费  
                if (dt.Columns.Contains("电力牵引费") && dt.Rows[i + 1]["电力牵引费"].ToString().Trim() != String.Empty)
                {
                    dr1["Fee2"] = int.Parse(dt.Rows[i + 1]["电力牵引费"].ToString().Trim());
                }

                dt1.Rows.Add(dr1);

                miles0 = miles0 + mile;
                if (i == 0) begStation = AStation;
                if (i == dt.Rows.Count - 2) endStation = BStation;
            }
        }

        private static void SetData2(DataTable dt, DataTable dt1, out String begStation,
           out String endStation, out int miles0)
        {
            //设置默认值
            miles0 = 0;
            begStation = String.Empty;
            endStation = String.Empty;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String AStation = dt.Rows[i]["始发站"].ToString();
                String BStation = dt.Rows[i]["到达站"].ToString();
                int mile = int.Parse(dt.Rows[i]["里程"].ToString());

                System.Data.DataRow dr1 = dt1.NewRow();
                dr1["Astation"] = AStation;
                dr1["Bstation"] = BStation;
                dr1["Miles"] = mile;

                //设置局内标志
                if (dt.Columns.Contains("局内") && dt.Rows[i]["局内"].ToString().Trim() != String.Empty)
                {
                    dr1["jnflag"] = "1";
                }

                //设置电气化标志
                if (dt.Columns.Contains("电气化") && dt.Rows[i]["电气化"].ToString().Trim() != String.Empty)
                {
                    dr1["dqh"] = "1";
                }

                //设置轮渡标志
                if (dt.Columns.Contains("轮渡标志") && dt.Rows[i]["轮渡标志"].ToString().Trim() != String.Empty)
                {
                    dr1["ShipFlag"] = "1";
                }

                //设置内燃牵引费 
                if (dt.Columns.Contains("内燃牵引费") && dt.Rows[i]["内燃牵引费"].ToString().Trim() != String.Empty)
                {
                    dr1["Fee1"] = int.Parse(dt.Rows[i]["内燃牵引费"].ToString().Trim());
                }

                //设置电力牵引费  
                if (dt.Columns.Contains("电力牵引费") && dt.Rows[i]["电力牵引费"].ToString().Trim() != String.Empty)
                {
                    dr1["Fee2"] = int.Parse(dt.Rows[i]["电力牵引费"].ToString().Trim());
                }

                dt1.Rows.Add(dr1);

                miles0 = miles0 + mile;
                if (i == 0) begStation = AStation;
                if (i == dt.Rows.Count - 1) endStation = BStation;
            }
        }

        /// <summary>
        /// 得到列车默认的车厢编组数据
        /// 规则：得到上次的编组数据，如果没有，从列车的数据中导入
        /// </summary>
        private static int[] GetTrainDefaultBianZhu(String TrainName)
        {
            int[] bianzhu = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            String[] fs = new String[] { "YinZuo", "RuanZuo", "OpenYinWo", "CloseYinWo", "RuanWo", 
                                          "AdvanceRuanWo", "CanChe", "FaDianChe", "ShuYinChe", "YouZhengChe" };
            
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("TrainName", TrainName));
            JTable tab1 = new JTable();
            /*
            tab1.TableName = "NEWTRAINSHOUROU";
            tab1.OrderBy = "byear desc,bmonth desc";
            DataRow dr1 = tab1.GetFirstDataRow(condition, "*"); */

            DataRow dr1 = null;
            if (dr1 == null 
                || dr1[0].ToString().Trim() == String.Empty)
            {
                tab1.TableName = "NEWTRAIN";
                tab1.OrderBy = String.Empty;
                condition.Clear();
                condition.Add(new SearchField("instr(TrainName,'"+TrainName+"')>0","",SearchOperator.UserDefine));
                dr1 = tab1.GetFirstDataRow(condition, "*");
            }
            if (dr1 != null)
            {
                for (int i = 0; i < bianzhu.Length; i++)
                {
                    if (dr1[fs[i]].ToString().Trim() != String.Empty)
                    {
                        bianzhu[i] = int.Parse(dr1[fs[i]].ToString().Trim());
                    }
                }
            }

            tab1.Close();
            return bianzhu;
        }
    }
}
