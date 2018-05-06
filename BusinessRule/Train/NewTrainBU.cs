using System;
using System.Collections.Generic;

using System.Text;
using System.Data;

using WebFrame;
using WebFrame.Data;
using WebFrame.Util;
using WebFrame.Designer;
using WebFrame.ExpControl;
using System.Web.UI.WebControls;
using System.Data.OracleClient;

namespace BusinessRule
{
    public class NewTrainData
    {
        public String Num { get; set; }
        public String TrainName { get; set; }
        public String TrainBigKind { get; set; }
        public String TrainType { get; set; }
        public String AStation { get; set; }
        public String BStation { get; set; }
        public String Mile { get; set; }
        public String LineID { get; set; }
        public String Kxts { get; set; }
        public String Cdzs { get; set; }

        public String YinZuo { get; set; }
        public String RuanZuo { get; set; }
        public String OpenYinWo { get; set; }
        public String CloseYinWo { get; set; }
        public String RuanWo { get; set; }
        public String AdvanceRuanWo { get; set; }
        public String CanChe { get; set; }
        public String FaDianChe { get; set; }
        public String ShuYinChe { get; set; }
        public String YouZhengChe { get; set; }
    }


    public partial class NewTrainBU
    {
        private const  double SRATE=10000;
       // private const double Rate = 0.9676;

        /// <summary>
        /// 得到当前合适的列车编号
        /// </summary>
        /// <returns></returns>
        public int GetNexNum()
        {
            int result = 0;
            JTable tab = new JTable("NEWTRAIN");
            DataTable dt= tab.SearchData(null, -1, "max(num) num").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                result = int.Parse(dr[0].ToString()) + 1;
            }
            tab.Close();
            return result;
        }

        //检测所有的合适线路
        public static int CheckAllLine()
        {
            int result = 0;
            JTable tab1 = new JTable("NEWTRAIN");
            DataSet ds1 = tab1.SearchData(null, -1, "*");
            DataTable dt1 = ds1.Tables[0];
            foreach (DataRow dr1 in dt1.Rows)
            {
                String line1 = dr1["line"].ToString();
                if (String.IsNullOrEmpty(line1) == false)
                {
                    //检查线路的配置是否正确
                    String traintype = dr1["TRAINTYPE"].ToString();
                    ETrainType type1 = ETrainType.空调车25T;

                    if (traintype == "动车组")
                    {
                        type1 = ETrainType.动车CRH2A;
                    }
                    else if (traintype == "空调特快")
                    {
                        type1 = ETrainType.空调车25T;
                    }
                    else if (traintype == "空调快速")
                    {
                        type1 = ETrainType.空调车25G;
                    }
                    else if (traintype == "空调普快")
                    {
                        type1 = ETrainType.空调车25G;
                    }
                    else if (traintype == "快速")
                    {
                        type1 = ETrainType.空调车25K;
                    }
                    else if (traintype == "普快")
                    {
                        type1 = ETrainType.绿皮车25B;
                    }

                    String[] lineNodes = line1.Trim().Replace("-", ",").Split(',');
                    TrainLine lineObj = BusinessRule.Line.GetTrainLineByTrainTypeAndLineNoeds(type1, false, lineNodes);
                    if (lineObj != null)
                    {
                        if (lineObj.TotalMiles + "" != dr1["Mile"].ToString().Trim())
                        {
                            dr1["CHECKLINE"] = "0";            //线路的里程不正确
                            result++;
                        }
                        else
                        {
                            dr1["CHECKLINE"] = DBNull.Value;   //表示线路是正确的
                        }
                    }
                    else
                    {
                        dr1["CHECKLINE"] = "0";                 //线路配置不正确
                        result++;
                    }
                }
                else
                {
                    dr1["CHECKLINE"] = DBNull.Value;
                }
            }
            tab1.Update(ds1.Tables[0]);
            tab1.Close();
            return result;
        }

        //设置既有车次的类别分析
        public static void SetListControlTrainType(ListControl control, String blankValue)
        {
            JTable tab1 = new JTable("newtrainlilunzhiview");
            DataSet ds1=tab1.SearchData(null, -1, "distinct traintype");
            DataTable dt1 = ds1.Tables[0];
            foreach (DataRow dr1 in dt1.Rows)
            {
                ListItem list1 = new ListItem(dr1[0].ToString(), dr1[0].ToString());
                control.Items.Add(list1);
            }
            if (String.IsNullOrEmpty(blankValue) == false)
            {
                ListItem list0 = new ListItem(blankValue, "");
                control.Items.Insert(0, list0);
            }
            tab1.Close();
        }

        //得到既有车次的明细统计
        public static DataTable GetFenXiDataByMingXi(String trainkind,int year1, int month1)
        {
            JQuery query1 = new JQuery();

            //得到所有的可用关联表信息
            String sql0 = @"select TrainName,GLTrain from NEWTRAIN 
                          where GLTrain is not null and GLYEAR is not null and GLMonth is not null
                          and (GLYear<{0} or (GLyear={1} and GLMonth<={2}))";
            sql0 = String.Format(sql0, year1, year1, month1);
            query1.CommandText = sql0;
            DataTable TrainTable = query1.SearchData(-1).Tables[0];
            Dictionary<string, string> gldata = new Dictionary<string, string>();
            foreach (DataRow dr0 in TrainTable.Rows)
            {
                if (gldata.ContainsKey(dr0[0].ToString()) == false)
                {
                    gldata.Add(dr0[0].ToString(), dr0[1].ToString());
                }
            }

            //得到明细数据
            double SRate = SRateProfileBU.GetRate(year1);
            String sql = @"select a.byear,a.bmonth,a.trainname,pcount0,pcount1,spcount0,spcount1,shouru0,shouru1,sshouru0,sshouru1,0 szr0,0 szr1,0 szr2,fee0,sfee0,fee1,sfee1,0 yk0,0 yk1 from
                ( select byear,bmonth,trainname,sum(pcount) pcount0,sum(spcount) spcount0,sum(shouru) shouru0,sum(sshouru) sshouru0,sum(fee) fee0,sum(sfee) sfee0 from newtrainlilunzhiview t 
                where byear={0} and bmonth={1} and traintype='{2}'
                group by byear,bmonth,trainname ) a
                left outer join
                ( select byear,{3} bmonth,trainname,sum(pcount) pcount1,sum(spcount) spcount1,sum(shouru) shouru1,sum(sshouru) sshouru1,sum(fee) fee1,sum(sfee) sfee1 from newtrainlilunzhiview t 
                where byear={4} and bmonth<={5} and traintype='{6}'
                group by byear,trainname ) b 
                on a.byear=b.byear and a.bmonth=b.bmonth and a.trainname=b.trainname order by a.trainname";
            sql = String.Format(sql, year1, month1,trainkind, month1, year1, month1,trainkind);
            query1.CommandText = sql;
            DataSet ds1 = query1.SearchData(-1);
            DataTable dt1 = ds1.Tables[0];

            //设置关联表数据信息
            DataTable GlTable = new DataTable();
            foreach(DataColumn col1 in dt1.Columns)
            {
                GlTable.Columns.Add(col1.ColumnName, col1.DataType);
            }
            GlTable.PrimaryKey=new DataColumn[]{GlTable.Columns["trainname"]};  //设置主键

            String[] arrCol1 = new String[] { "pcount0","pcount1","spcount0","spcount1","shouru0","shouru1","sshouru0","sshouru1",
                                              "szr0","szr1","fee0","sfee0","fee1","sfee1","yk0","yk1" };
            String[] arrCol0 = new String[] { "byear","bmonth", "trainname", "pcount0","pcount1","spcount0","spcount1","shouru0",
                                             "shouru1","sshouru0","sshouru1","szr0","szr1","szr2","fee0","sfee0","fee1","sfee1","yk0","yk1"};

            foreach (DataRow dr1 in dt1.Rows)
            {
                String trainName = dr1["trainname"].ToString();
                String glName = String.Empty;
                if (gldata.ContainsKey(trainName))
                {
                    glName = gldata[trainName];
                }
                else
                {
                    glName = trainName;
                }

                //设置关联表的信息
                DataRow glrow = GlTable.Rows.Find(glName);
                if (glrow == null)
                {
                    glrow = GlTable.NewRow();
                    foreach (String m in arrCol0)
                    {
                        glrow[m] = dr1[m];
                    }
                    glrow["trainname"] = glName;
                    GlTable.Rows.Add(glrow);
                }
                else
                {
                    foreach (String m in arrCol1)
                    {
                        glrow[m] = double.Parse(glrow[m].ToString()) + double.Parse(dr1[m].ToString());
                    }
                }
            }         

            //计算上座率和盈亏
            foreach (DataRow dr1 in GlTable.Rows)
            {
                if (double.Parse(dr1["shouru0"].ToString()) > 0)
                {
                    dr1["szr0"] = double.Parse(dr1["sshouru0"].ToString()) * 100 / double.Parse(dr1["shouru0"].ToString());
                    dr1["szr1"] = double.Parse(dr1["sshouru1"].ToString()) * 100 / double.Parse(dr1["shouru1"].ToString());
                    dr1["szr2"] = double.Parse(dr1["fee0"].ToString()) * 100 / double.Parse(dr1["shouru0"].ToString());
                }
                dr1["yk0"] = double.Parse(dr1["sshouru0"].ToString()) * SRate - double.Parse(dr1["sfee0"].ToString());
                dr1["yk1"] = double.Parse(dr1["sshouru1"].ToString()) * SRate - double.Parse(dr1["sfee1"].ToString());
            }
            query1.Close();
            return GlTable;
        }

        //得到既有车次的分类统计
        public static DataTable GetFenXiDataByKind(int year1, int month1)
        {
            double SRate = SRateProfileBU.GetRate(year1);

            String sql = @"select a.byear,a.bmonth,a.traintype,pcount0,pcount1,spcount0,spcount1,shouru0,shouru1,sshouru0,sshouru1,0 szr0,0 szr1,fee0,sfee0,fee1,sfee1,0 yk0,0 yk1 from
                ( select byear,bmonth,traintype,sum(pcount) pcount0,sum(spcount) spcount0,sum(shouru) shouru0,sum(sshouru) sshouru0,sum(fee) fee0,sum(sfee) sfee0 from newtrainlilunzhiview t 
                where byear={0} and bmonth={1}
                group by byear,bmonth,traintype ) a
                left outer join
                ( select byear,{2} bmonth,traintype,sum(pcount) pcount1,sum(spcount) spcount1,sum(shouru) shouru1,sum(sshouru) sshouru1,sum(fee) fee1,sum(sfee) sfee1 from newtrainlilunzhiview t 
                where byear={3} and bmonth<={4}
                group by byear,traintype ) b 
                on a.byear=b.byear and a.bmonth=b.bmonth and a.traintype=b.traintype order by a.traintype";
            sql = String.Format(sql, year1, month1, month1, year1, month1);
           // JDebug.Print(sql);

            JQuery query1=new JQuery();
            query1.CommandText=sql;

            DataSet ds1 = query1.SearchData(-1);
            DataTable dt1 = ds1.Tables[0];
            foreach (DataRow dr1 in dt1.Rows)
            {
                if (double.Parse(dr1["shouru0"].ToString()) > 0)
                {
                    dr1["szr0"] = double.Parse(dr1["sshouru0"].ToString()) *100 / double.Parse(dr1["shouru0"].ToString());
                    dr1["szr1"] = double.Parse(dr1["sshouru1"].ToString()) *100 / double.Parse(dr1["shouru1"].ToString());
                }
                dr1["yk0"] = double.Parse(dr1["sshouru0"].ToString()) * SRate - double.Parse(dr1["sfee0"].ToString());
                dr1["yk1"] = double.Parse(dr1["sshouru1"].ToString()) * SRate - double.Parse(dr1["sfee1"].ToString());
            }
            query1.Close();
            return dt1;
        }

        //计算车次成本的理论值
        public static void CalLiLunValue(String trainName,int year1, int month1)
        {
            int day1 = 30;
            int yearday = 365;

            if (month1 == 1 || month1 == 3 || month1 == 5 || month1 == 7 || month1 == 8 || month1 == 10 || month1 == 12)
            {
                day1 = 31;
            }
            if (month1 == 2)
            {
                day1 = 28;
                if (DateTime.IsLeapYear(year1))
                {
                    day1 = 29;
                }
            }

            //计算新的数据
            JTable tab1 = new JTable();
            tab1.TableName = "NEWTRAIN";
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("trainname", trainName));
            DataSet ds1 = tab1.SearchData(condition, -1, "*");
            Dictionary<String, object> data1 = new Dictionary<string, object>();
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
               Train trainobj = null;
                trainobj = GetTrainObject(dr1);
                if (trainobj != null)
                {
                    //给Data1赋值
                    String TrainName = dr1["TrainName"].ToString();
                    String TrainType = dr1["TrainType"].ToString();
                    String[] t1 = TrainName.Split('/');
                    int trainCount = t1.Length;

                    //if (TrainName == "K1473")
                    // {
                    data1["TrainName"] = TrainName;
                    data1["TrainType"] = TrainType;
                    data1["byear"] = year1;
                    data1["bmonth"] = month1;

                    data1["shouru"] = trainCount * trainobj.GetShouRu() / 2 * day1 / yearday / SRATE;
                    data1["pcount"] = trainCount * trainobj.GetTotalPerson() * day1;

                    data1["fee1"] = trainCount * trainobj.GetFee1() ;                              /*--线路使用费---*/
                    data1["fee2"] = trainCount * trainobj.GetFee2() ;                              /*--机车牵引费---*/
                    data1["fee3"] = trainCount * trainobj.GetFee3() ;                              /*--电网和接触网使用费---*/
                    data1["fee4"] = trainCount * trainobj.GetFee4() ;                              /*---售票服务费-------*/
                    data1["fee5"] = trainCount * trainobj.GetFee5() ;                              /*--旅客使用费---*/
                    data1["fee6"] = trainCount * trainobj.GetFee6() ;                              /*--列车上水费---*/
                    data1["fee7"] = trainCount * trainobj.GetFee7() ;                               /*---人员和工资附加费---*/

                    data1["fee8"] = trainCount * (trainobj.GetFee8() );                             /*---车辆折旧费----*/
                    data1["fee9"] = trainCount * (trainobj.GetFee9() ) ;                            /*---日常检修成本费----*/
                    data1["fee10"] = trainCount * trainobj.GetFee10() ;                             /*---定期检修成本-----*/
                    data1["fee11"] = trainCount * (trainobj.GetFee11()) ;                           /*---车辆备用品------*/
                    data1["fee12"] = trainCount * trainobj.GetFee12();                              /*---空调车用油费-----*/
                    data1["fee13"] = trainCount * trainobj.GetFee13() ;                             /*---人员其他费用----*/
                    data1["fee14"] = trainCount * (trainobj.GetFee14()) ;                           /*---车辆购置利息----*/
                    
                }
            }
            tab1.Close();
        }

        //计算车次成本的理论值
        public static void CalLiLunValue(int year1,int month1)
        {
            int day1 = 30;
            int yearday = 365;

            if (month1 == 1 || month1 == 3 || month1 == 5 || month1 == 7 || month1 == 8 || month1 == 10 || month1 == 12)
            {
                day1 = 31;
            }
            if (month1 == 2)
            {
                day1 = 28;
                if (DateTime.IsLeapYear(year1))
                {
                    day1 = 29;
                }
            }

            //删除以前的数据
            JTable tab1 = new JTable("NEWTRAINLILUNZHI");
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("byear", year1 + "", SearchFieldType.NumericType));
            condition.Add(new SearchField("bmonth", month1 + "", SearchFieldType.NumericType));
            tab1.DeleteData(condition);

            //计算新的数据
            tab1.TableName = "NEWTRAIN";
            List<String> trainList = new List<string>();
            DataSet ds1 = tab1.SearchData(null, -1, "*");

            tab1.TableName = "NEWTRAINLILUNZHI";
            Dictionary<String, object> data1 = new Dictionary<string, object>();
            JTable tab2=new JTable();
            Train trainobj = null;
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                trainobj = null;
                trainobj = GetTrainObject(dr1);
                if (trainobj != null)
                {
                    data1.Clear();
                    //给Data1赋值
                    String TrainName = dr1["TrainName"].ToString();
                    String TrainType = dr1["TrainType"].ToString();
                    String[] t1 = TrainName.Split('/');
                    int trainCount = t1.Length;
                    
                    data1["TrainName"] = TrainName;
                    data1["TrainType"] = TrainType;
                    data1["byear"] = year1;
                    data1["bmonth"] = month1;

                    data1["shouru"] = trainobj.GetShouRu() / 2 * day1 / yearday / SRATE;
                    data1["pcount"] = trainobj.GetTotalPerson() * day1;

                    data1["fee1"] = trainobj.GetFee1() / 2.0 * day1 / yearday;                              /*--线路使用费---*/
                    data1["fee2"] = trainobj.GetFee2() / 2.0 * day1 / yearday;                              /*--机车牵引费---*/

                    data1["fee3"] = trainobj.GetFee3() / 2.0 * day1 / yearday;                              /*--电网和接触网使用费---*/
                    data1["fee4"] = trainobj.GetFee4() / 2.0 * day1 / yearday;                              /*---售票服务费-------*/
                    data1["fee5"] = trainobj.GetFee5() / 2.0 * day1 / yearday;                              /*--旅客使用费---*/
                    
                    /*--------------------------------------------------------*/
                    data1["fee6"] = trainobj.GetFee6() / 2.0 * day1 / yearday;                              /*--列车上水费---*/
                    data1["fee7"] = trainobj.GetFee7() / 2.0 * day1 / yearday;                              /*---人员和工资附加费---*/

                    data1["fee8"] = (trainobj.GetFee8() / trainobj.CheDiShu) * day1 / yearday;               /*---车辆折旧费----*/
                    data1["fee9"] = (trainobj.GetFee9() / trainobj.CheDiShu) * day1 / yearday;               /*---日常检修成本费----*/
                    data1["fee10"] = trainobj.GetFee10() / 2.0 * day1 / yearday;                            /*---定期检修成本-----*/
                    data1["fee11"] = (trainobj.GetFee11() / trainobj.CheDiShu) * day1 / yearday;            /*---车辆备用品------*/
                    data1["fee12"] = trainobj.GetFee12() / 2.0 * day1 / yearday;                            /*---空调车用油费-----*/
                    data1["fee13"] = trainobj.GetFee13() / 2.0 * day1 / yearday;                            /*---人员其他费用----*/
                    data1["fee14"] = (trainobj.GetFee14() / trainobj.CheDiShu) * day1 / yearday;            /*---车辆购置利息----*/
                    data1["fee15"] = trainobj.GetFee15() / 2.0 * day1 / yearday;                            /*--轮渡费---*/

                    //得到车次的真实数据
                    data1["sshouru"] = GetFactShouRu(tab2, TrainName, year1, month1);
                    int pcount = 0;
                    double fee = 0, price = 0;
                    GetFactPCountAndFee(tab2, TrainName, year1, month1, out pcount, out fee, out price);
                    data1["spcount"] = pcount;
                    if (price > 0)
                    {
                        data1["fee5"] = int.Parse(data1["pcount"].ToString()) * price / SRATE;  //重新计算理论旅客服务费
                    }

                    data1["sfee1"] = GetFactFee1(tab2, TrainName, year1, month1);
                    data1["sfee2"] = GetFactFee2(tab2, TrainName, year1, month1);
                    if (TrainType == "动车组")
                    {
                        data1["sfee3"] = GetFactFee3(tab2, TrainName, year1, month1);
                    }
                    else
                    {
                        data1["sfee3"] = 0;
                    }

                    double sRate = 0;
                    if (dr1["traintype"].ToString() == "动车组")
                    {
                        sRate = TrainProfile.SaleRateForHigh / 100d;
                    }
                    else
                    {
                        sRate = TrainProfile.SaleRateForComm / 100d;
                    }
                    data1["sfee4"] = double.Parse(data1["sshouru"].ToString()) * sRate;
                    data1["sfee5"] = fee;   //旅客服务费 */

                    //其他的数据同上
                    data1["sfee6"] = data1["fee6"];
                    data1["sfee7"] = data1["fee7"];
                    data1["sfee8"] = data1["fee8"];
                    data1["sfee9"] = data1["fee9"];
                    data1["sfee10"] = data1["fee10"];
                    data1["sfee11"] = data1["fee11"];
                    data1["sfee12"] = data1["fee12"];
                    data1["sfee13"] = data1["fee13"];
                    data1["sfee14"] = data1["fee14"];
                    data1["sfee15"] = data1["fee15"];

                    if (pcount > 0 && fee > 0)
                    {
                        tab1.InsertData(data1);
                    }
                }                
            }
            tab1.Close();
            tab2.Close();
        }

        public static void CalLiLunValue()
        {
            int year1 = DateTime.Now.Year;
            int month1 = DateTime.Now.Month;
            CalLiLunValue(year1, month1);
        }

        /// <summary>
        /// 根据数据行得到NewTrainData对象
        /// </summary>
        /// <param name="dr1"></param>
        /// <returns></returns>
        private static NewTrainData GetTrainInfo(DataRow dr1)
        {
            NewTrainData data1 = null;
            if (dr1 != null)
            {
                data1 = new NewTrainData();
                data1.Num = dr1["num"].ToString();
                data1.TrainName = dr1["TrainName"].ToString();
                data1.TrainBigKind = dr1["TrainBigKind"].ToString();
                data1.TrainType = dr1["TrainType"].ToString();
                data1.AStation = dr1["AStation"].ToString();
                data1.BStation = dr1["BStation"].ToString();
                data1.Mile = dr1["Mile"].ToString();
                data1.LineID = dr1["LineID"].ToString();
                data1.Kxts = dr1["Kxts"].ToString();
                data1.Cdzs = dr1["Cdzs"].ToString();

                data1.YinZuo = dr1["YinZuo"].ToString();
                data1.RuanZuo = dr1["RuanZuo"].ToString();
                data1.OpenYinWo = dr1["OpenYinWo"].ToString();
                data1.CloseYinWo = dr1["CloseYinWo"].ToString();
                data1.RuanWo = dr1["RuanWo"].ToString();
                data1.AdvanceRuanWo = dr1["AdvanceRuanWo"].ToString();
                data1.CanChe = dr1["CanChe"].ToString();
                data1.FaDianChe = dr1["FaDianChe"].ToString();
                data1.ShuYinChe = dr1["ShuYinChe"].ToString();

                data1.YouZhengChe = dr1["YouZhengChe"].ToString();   //邮政车（意义不明确）
            }
            return data1;
        }

        //根据列名得到列车的信息
        public NewTrainData GetTrainInfo(String trainName)
        {
            JTable tab1 = new JTable();
            NewTrainData data1 = null;
            tab1.TableName = "NEWTRAIN";
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("trainName", trainName));
            DataRow dr1 = tab1.GetFirstDataRow(condition, "*");
            if (dr1 != null)
            {
                data1 = NewTrainBU.GetTrainInfo(dr1); 
            }
            tab1.Close();
            return data1;
        }


        //得到某列车的实际人数和实际收入
        public void GetTrainFactPersonAndMoney(String TrainName,
            int year,int month,
            out int Person, out double Shouru)
        {
            Person = 0;
            Shouru = 0;
            if (String.IsNullOrEmpty(TrainName) == false)
            {
                JTable tab1 = new JTable("NEWTRAINSHOUROU");
                String trainName1 = TrainName.Replace("/", ",");
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("TrainName", trainName1, SearchOperator.Collection));
                condition.Add(new SearchField("byear", year+"", SearchFieldType.NumericType));
                condition.Add(new SearchField("bmonth", month+"", SearchFieldType.NumericType));
                DataRow dr1 = tab1.GetFirstDataRow(condition, "sum(shourou1)");
                if (dr1 != null)
                {
                    if (dr1[0].ToString() != String.Empty)
                    {
                        Shouru = double.Parse(dr1[0].ToString());
                    }
                }
                tab1.Close();
            }
        }


        //得到某列车的实际人数和实际收入的累计值
        public void GetTrainFactSumPersonAndMoney(String TrainName,
            int year, int month,
            out int Person, out double Shouru)
        {
            Person = 0;
            Shouru = 0;
            if (String.IsNullOrEmpty(TrainName) == false)
            {
                JTable tab1 = new JTable("NEWTRAINSHOUROU");
                String trainName1 = TrainName.Replace("/", ",");
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("TrainName", trainName1, SearchOperator.Collection));
                condition.Add(new SearchField("byear", year + "", SearchFieldType.NumericType));
                condition.Add(new SearchField("bmonth", month + "", SearchOperator.SmallerAndEqual,SearchFieldType.NumericType));
                DataRow dr1 = tab1.GetFirstDataRow(condition, "sum(shourou1)");
                if (dr1 != null)
                {
                    if (dr1[0].ToString() != String.Empty)
                    {
                        Shouru = double.Parse(dr1[0].ToString());
                    }
                }
                tab1.Close();
            }
        }

        //得到列车的支出数据
        public static void GetTrainZhiChuByYear(String TrainName, int year,
            out double[] sr1, out double[] sr2)
        {
            /*
            NewTrainBU bu1 = new NewTrainBU();
            NewTrainData data1=bu1.GetTrainInfo(TrainName);
            
            sr1 = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            sr2 = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            double zc1=0,zc2=0;
            for (int i = 0; i < sr1.Length; i++)
            {
                zc1 = 0; zc2 = 0;
                NewTrainZhiChuBU.GetTrainZhiChuByName(TrainName, data1.TrainType, year, i + 1, out zc1, out zc2);
                sr1[i] = Math.Round(zc1,0);
                sr2[i] = Math.Round(zc2,0);
            }*/

            sr1 = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            sr2 = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            JTable tab1 = new JTable("NEWTRAINLILUNZHIVIEW");

            String sql = "select sum(sfee),bmonth from NEWTRAINLILUNZHIVIEW where trainname = '" + TrainName + "'";
            sql = sql + " and byear=" + year + " Group by bmonth order by bmonth";
            tab1.CommandText = sql;
            DataSet ds1 = tab1.SearchData(-1);

            //得到关联列车的数据
            String sql2 = "select * from NEWTRAIN where GLTRAIN = '" + TrainName + "'";
            tab1.CommandText = sql2;
            DataSet glData = tab1.SearchData(-1);

            //数据汇总
            double sum1 = 0;
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                int month1 = int.Parse(dr1[1].ToString());
                double sr = Math.Ceiling(double.Parse(dr1[0].ToString()));
                sr1[month1 - 1] = sr;

                //增加关联表的数据
                foreach (DataRow glRow in glData.Tables[0].Rows)
                {
                    String glTrain = glRow["TrainName"].ToString();
                    int glYear = int.Parse(glRow["GlYear"].ToString());
                    int glMonth = int.Parse(glRow["GlMonth"].ToString());
                    if (glTrain.Trim() != String.Empty && glYear >= year)
                    {
                        sql = "select sum(sfee),bmonth from NEWTRAINLILUNZHIVIEW where trainname = '" + glRow["TrainName"].ToString() + "'";
                        sql = sql + " and byear=" + year + " Group by bmonth order by bmonth";
                        tab1.CommandText = sql;
                        DataSet glds = tab1.SearchData(-1);
                        foreach (DataRow row0 in glds.Tables[0].Rows)
                        {
                            month1 = int.Parse(row0[1].ToString());
                            sr = Math.Ceiling(double.Parse(row0[0].ToString()));
                            sr1[month1 - 1] = sr1[month1 - 1] + sr;
                        }
                    }
                }
            }

            for (int i = 0; i < sr1.Length; i++)
            {
                sum1 = sum1 + sr1[i];
                sr2[i] = sum1;
            }

            tab1.Close();
        }


        //得到某车次某年的按月全年收入和累计收入
        public static void GetTrainMoneyByYear(String TrainName, int year,
            out double[] sr1,out double[] sr2)
        {
            /*
            sr1 = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            sr2 = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            JTable tab1 = new JTable("NEWTRAINSHOUROU");
            String train1 = "'"+TrainName.Replace("/", "','")+"'";
                       
            String sql = "select sum(shourou1),bmonth from NEWTRAINSHOUROU where trainname in ("+train1+")";
            sql = sql + " and byear=" + year + " Group by bmonth order by bmonth";
            tab1.CommandText = sql;
            DataSet ds1 = tab1.SearchData(-1);
            double sum1 = 0;
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                int month1 = int.Parse(dr1[1].ToString());
                double sr= Math.Ceiling(double.Parse(dr1[0].ToString())/10000);
                sr1[month1 - 1] = sr;
            }

            for (int i = 0; i < sr1.Length; i++)
            {
                sum1 = sum1 + sr1[i];
                sr2[i] = sum1;
            }
            
            tab1.Close();*/

            sr1 = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            sr2 = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            JTable tab1 = new JTable("NEWTRAINLILUNZHIVIEW");

            String sql = "select sum(sshouru),bmonth from NEWTRAINLILUNZHIVIEW where trainname = '" + TrainName + "'";
            sql = sql + " and byear=" + year + " Group by bmonth order by bmonth";
            tab1.CommandText = sql;
            DataSet ds1 = tab1.SearchData(-1);

            //得到关联列车的数据
            String sql2 = "select * from NEWTRAIN where GLTRAIN = '" + TrainName + "'";
            tab1.CommandText = sql2;
            DataSet glData = tab1.SearchData(-1);


            double sum1 = 0;
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                int month1 = int.Parse(dr1[1].ToString());
                double sr = Math.Ceiling(double.Parse(dr1[0].ToString()) );
                sr1[month1 - 1] = sr;

                //增加关联表的数据
                foreach (DataRow glRow in glData.Tables[0].Rows)
                {
                    String glTrain = glRow["TrainName"].ToString();
                    int glYear = int.Parse(glRow["GlYear"].ToString());
                    int glMonth = int.Parse(glRow["GlMonth"].ToString());
                    if (glTrain.Trim() != String.Empty && glYear >= year)
                    {
                        sql = "select sum(sshouru),bmonth from NEWTRAINLILUNZHIVIEW where trainname = '" + glRow["TrainName"].ToString() + "'";
                        sql = sql + " and byear=" + year + " Group by bmonth order by bmonth";
                        tab1.CommandText = sql;
                        DataSet glds = tab1.SearchData(-1);
                        foreach (DataRow row0 in glds.Tables[0].Rows)
                        {
                            month1 = int.Parse(row0[1].ToString());
                            sr = Math.Ceiling(double.Parse(row0[0].ToString()));
                            sr1[month1 - 1] = sr1[month1 - 1]+sr;
                        }
                    }
                }
            }

            for (int i = 0; i < sr1.Length; i++)
            {
                sum1 = sum1 + sr1[i];
                sr2[i] = sum1;
            }

            tab1.Close();
        }

        //得到某车次某年的按月的编组信息
        public static void GetTrainBianZhuByYear(String TrainName, int year,
            out String[] sr1)
        {
            sr1 = new String[] { "1月", "2月", "3月", "4月", "5月", "6月",
                                 "7月", "8月", "9月", "10月", "11月", "12月","" };
            int YinZuo = 0;
            int RuanZuo = 0;
            int OpenYinWo = 0;
            int RuanWo = 0;

            int YinZuo1 = 0;
            int RuanZuo1 = 0;
            int OpenYinWo1 = 0;
            int RuanWo1 = 0;

            //得到标准的车厢编组配置
            JTable tab1 = new JTable("");
            tab1.TableName = "NewTrain";
            String sql = "select YinZuo,OpenYinWo,RuanWo,RuanZuo from NewTrain where trainname ='" + TrainName + "'";
            tab1.CommandText = sql;
            DataRow dr1 = tab1.GetFirstDataRow();
            if (dr1 != null)
            {
                if (dr1[0].ToString() != String.Empty) YinZuo = int.Parse(dr1[0].ToString());
                if (dr1[1].ToString() != String.Empty) OpenYinWo = int.Parse(dr1[1].ToString());
                if (dr1[2].ToString() != String.Empty) RuanWo = int.Parse(dr1[2].ToString());
                if (dr1[3].ToString() != String.Empty) RuanZuo = int.Parse(dr1[3].ToString());
            }

            //得到每月的编组配置
            tab1.TableName="NEWTRAINSHOUROU";
            String train1 = "'" + TrainName.Replace("/", "','") + "'";
            sql = "select YinZuo,OpenYinWo,RuanWo,RuanZuo,bmonth from NEWTRAINSHOUROU where trainname in (" + train1 + ")";
            sql = sql + " and byear=" + year + " order by bmonth";
            tab1.CommandText = sql;
            DataSet ds1 = tab1.SearchData(-1);
            int temp=0;
            foreach (DataRow dr0 in ds1.Tables[0].Rows)
            {
                if (dr0["bmonth"].ToString().Trim() != String.Empty)
                {
                    int month1 = int.Parse(dr0["bmonth"].ToString());
                    if (dr0[0].ToString() != String.Empty) YinZuo1 = int.Parse(dr0[0].ToString());
                    if (dr0[1].ToString() != String.Empty) OpenYinWo1 = int.Parse(dr0[1].ToString());
                    if (dr0[2].ToString() != String.Empty) RuanWo1 = int.Parse(dr0[2].ToString());
                    if (dr0[3].ToString() != String.Empty) RuanZuo1 = int.Parse(dr0[3].ToString());

                    if (YinZuo1 - YinZuo != 0)
                    {
                        temp=YinZuo1-YinZuo;
                        if (temp > 0)
                        {
                            sr1[month1 - 1] = sr1[month1 - 1] + "\n硬座+" + temp;
                        }
                        else
                        {
                            sr1[month1 - 1] = sr1[month1 - 1] + "\n硬座" + temp;
                        }
                    }
                    if (OpenYinWo1 - OpenYinWo != 0)
                    {
                        temp = OpenYinWo1 - OpenYinWo;
                        if (temp > 0)
                        {
                            sr1[month1 - 1] = sr1[month1 - 1] + "\n硬卧+" + temp;
                        }
                        else
                        {
                            sr1[month1 - 1] = sr1[month1 - 1] + "\n硬卧" + temp;
                        }
                    }
                    if (RuanZuo1 - RuanZuo != 0)
                    {
                        temp = RuanZuo1 - RuanZuo;
                        if (temp > 0)
                        {
                            sr1[month1 - 1] = sr1[month1 - 1] + "\n软座+" + temp;
                        }
                        else
                        {
                            sr1[month1 - 1] = sr1[month1 - 1] + "\n软座" + temp;
                        }
                    }
                    if (RuanWo1 - RuanWo != 0)
                    {
                        temp = OpenYinWo1 - OpenYinWo;
                        if (temp > 0)
                        {
                            sr1[month1 - 1] = sr1[month1 - 1] + "\n软卧+" + temp;
                        }
                        else
                        {
                            sr1[month1 - 1] = sr1[month1 - 1] + "\n软卧" + temp;
                        }
                    }
                }
            }

            tab1.Close();
        }

        //根据列车的类别返回列车的名字组
        public String GetTrainNameListByKind(String TrainKindName)
        {
            String result = String.Empty;
            if (String.IsNullOrEmpty(TrainKindName) == false)
            {
                JTable tab1 = new JTable("NEWTRAIN");
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("TrainType", TrainKindName));
                DataTable dt1 = tab1.SearchData(condition, -1, "TrainName").Tables[0];
                foreach (DataRow dr1 in dt1.Rows)
                {
                    if (result == String.Empty)
                    {
                        result = dr1[0].ToString();
                    }
                    else
                    {
                        result = result + "," + dr1[0].ToString();
                    }
                }
                tab1.Close();
            }
            return result.Replace("/", ",");

        }


        //得到列车的理论人数和收入
        public void GetTrainPersonAndMoney(NewTrainData data1,
            out int Person,out double Shouru)
        {
            Person = 0;
            Shouru = 0;

            CommTrain train1 = new CommTrain();
            HighTrain train2 = new HighTrain();

            //设置运行里程
            if (data1.Mile != String.Empty)
            {
                train1.YunXingLiCheng = int.Parse(data1.Mile);
                train2.YunXingLiCheng = int.Parse(data1.Mile);
            }

            #region 设置车厢编组
            if (data1.YinZuo.Trim() != String.Empty)
            {
                train1.YinZuo = int.Parse(data1.YinZuo);
            }
            if (data1.RuanZuo.Trim() != String.Empty)
            {
                train1.RuanZuo = int.Parse(data1.RuanZuo);

            }
            if (data1.OpenYinWo.Trim() != String.Empty)
            {
                train1.OpenYinWo = int.Parse(data1.OpenYinWo);
            } 
            if (data1.CloseYinWo.Trim() != String.Empty)
            {
                train1.CloseYinWo = int.Parse(data1.CloseYinWo);
            } 
            if (data1.RuanWo.Trim() != String.Empty)
            {
                train1.RuanWo = int.Parse(data1.RuanWo);
            }
            if (data1.AdvanceRuanWo .Trim() != String.Empty)
            {
                train1.AdvanceRuanWo  = int.Parse(data1.AdvanceRuanWo );
            } 
            if (data1.CanChe.Trim() != String.Empty)
            {
                train1.CanChe = int.Parse(data1.CanChe);
            } 
            if (data1.FaDianChe.Trim() != String.Empty)
            {
                train1.FaDianChe = int.Parse(data1.FaDianChe);
            } 
            if (data1.ShuYinChe.Trim() != String.Empty)
            {
                train1.ShuYinChe = int.Parse(data1.ShuYinChe);
            }
            #endregion

            #region 设置车厢类型
            switch (data1.TrainType.Trim())
            {
                case "动车组":
                    train2.TrainType = EHighTrainType.CRH2A;
                    break;

                case "空调特快":
                    train1.TrainType = ECommTrainType.空调车25T;
                    train1.JiaKuai = EJiaKuai.特快;
                    break;

                case "空调快速":
                    train1.TrainType = ECommTrainType.空调车25K;
                    train1.JiaKuai = EJiaKuai.特快;
                    break;

                case "空调普快":
                    train1.TrainType = ECommTrainType.空调车25K;
                    train1.JiaKuai = EJiaKuai.加快;
                    break;

                case "快速":
                    train1.TrainType = ECommTrainType.绿皮车25B;
                    train1.JiaKuai = EJiaKuai.加快;
                    break;

                case "普列":
                    train1.TrainType = ECommTrainType.绿皮车25B;
                    break;

                default:
                    break;
            }
            #endregion

            if (data1.TrainType.Trim() != "动车组")
            {
                Person = train1.GetTotalPerson();
                Shouru = train1.GetShouRu();
            }
            else
            {
                //需要重新计算
                Shouru =train2.GetShouRu();
                Person = train2.GetTotalPerson();
            }
        }

        /// <summary>
        /// 根据列车的类别得到列车的列表数据
        /// </summary>
        /// <param name="trainTypeName"></param>
        /// <returns></returns>
        public List<NewTrainData> GetTrainListByType(String trainTypeName)
        {
            List<NewTrainData> list1 = new List<NewTrainData>();
            JTable tab1 = new JTable("NEWTRAIN");
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("trainType", trainTypeName));
            DataTable dt1 = tab1.SearchData(condition, -1, "*").Tables[0];
            foreach (DataRow dr1 in dt1.Rows)
            {
                NewTrainData data1 = NewTrainBU.GetTrainInfo(dr1);
                if (data1 != null)
                {
                    list1.Add(data1);
                }
            }
            tab1.Close();
            return list1;
        }

        /// <summary>
        /// 得到类别组的收入和人数
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="Person"></param>
        /// <param name="Shouru"></param>
        public void GetTrainPersonAndMoney(List<NewTrainData> list1,
            out int Person, out double Shouru)
        {
            Person = 0;
            Shouru = 0;
            foreach (NewTrainData data1 in list1)
            {
                int person1 = 0;
                double shouru1 = 0;
                GetTrainPersonAndMoney(data1, out person1, out shouru1);
                Person = Person + person1;
                Shouru = Shouru + shouru1;
            }
        }


        //根据当前数据，得到Train对象
        public static Train GetTrainObject(String TrainName)
        {
            Train train1 = null;
            if (String.IsNullOrEmpty(TrainName) == false)
            {
                NewTrainBU nt = new NewTrainBU();
                NewTrainData data1 = nt.GetTrainInfo(TrainName);

                if (data1.TrainType == "动车组")
                {
                    train1 = new HighTrain();
                    HighTrain ht1 = (HighTrain)train1;
                    ht1.CunZengMoShi = ECunZengMoShi.新人新车;
                    ht1.IsFullChengBen = true;
                    ht1.IsYearFlag = true;

                    //设置其他的值
                    ht1.TrainType = EHighTrainType.CRH2A;
                    ht1.YunXingLiCheng = int.Parse(data1.Mile);
                    ht1.CheDiShu = 1;

                    int yz1 = 0, rz1 = 0;
                    if (String.IsNullOrEmpty(data1.YinZuo) == false)
                    {
                        yz1 = int.Parse(data1.YinZuo);
                    }
                    if (String.IsNullOrEmpty(data1.RuanZuo) == false)
                    {
                        rz1 = int.Parse(data1.RuanZuo);
                    }

                    if (yz1 + rz1 > 8)   //表示是长车
                    {
                        ht1.BianZhu = EHighTrainBianZhu.重联;
                    }
                }
                else
                {
                    train1 = new CommTrain();

                    //设置车的类型
                    CommTrain ct1 = (CommTrain)train1;
                    ct1.IsFullChengBen = true;
                    ct1.IsYearFlag = true;

                    ct1.CunZengMoShi = ECunZengMoShi.新人新车;
                    if (data1.TrainType == "空调特快")
                    {
                        ct1.TrainType = ECommTrainType.空调车25T;
                    }
                    else if (data1.TrainType == "空调快速")
                    {
                        ct1.TrainType = ECommTrainType.空调车25K;
                    }
                    else if (data1.TrainType == "空调普快")
                    {
                        ct1.TrainType = ECommTrainType.绿皮车25B;
                        ct1.KongTiaoFlag = true;
                    }
                    else if (data1.TrainType == "快速")
                    {
                        ct1.TrainType = ECommTrainType.空调车25K;
                        ct1.KongTiaoFlag = false;
                    }
                    else if (data1.TrainType == "普快")
                    {
                        ct1.TrainType = ECommTrainType.绿皮车25B;
                        ct1.KongTiaoFlag = false;
                    }
                    else
                    {
                        ct1.TrainType = ECommTrainType.空调车25T;
                    }

                    //设置车厢编组
                    if (String.IsNullOrEmpty(data1.YinZuo) == false)
                    {
                        ct1.YinZuo = int.Parse(data1.YinZuo);
                    }
                    else
                    {
                        ct1.YinZuo = 0;
                    }

                    if (String.IsNullOrEmpty(data1.RuanZuo) == false)
                    {
                        ct1.RuanZuo = int.Parse(data1.RuanZuo);
                    }
                    else
                    {
                        ct1.RuanZuo = 0;
                    }

                    if (String.IsNullOrEmpty(data1.OpenYinWo) == false)
                    {
                        ct1.OpenYinWo = int.Parse(data1.OpenYinWo);
                    }
                    else
                    {
                        ct1.OpenYinWo = 0;
                    }

                    if (String.IsNullOrEmpty(data1.CloseYinWo) == false)
                    {
                        ct1.CloseYinWo = int.Parse(data1.CloseYinWo);
                    }
                    else
                    {
                        ct1.CloseYinWo = 0;
                    }

                    if (String.IsNullOrEmpty(data1.RuanWo) == false)
                    {
                        ct1.RuanWo = int.Parse(data1.RuanWo);
                    }
                    else
                    {
                        ct1.RuanWo = 0;
                    }

                    if (String.IsNullOrEmpty(data1.AdvanceRuanWo) == false)
                    {
                        ct1.AdvanceRuanWo = int.Parse(data1.AdvanceRuanWo);
                    }
                    else
                    {
                        ct1.AdvanceRuanWo = 0;
                    }

                    if (String.IsNullOrEmpty(data1.CanChe) == false)
                    {
                        ct1.CanChe = int.Parse(data1.CanChe);
                    }
                    else
                    {
                        ct1.CanChe = 0;
                    }

                    if (String.IsNullOrEmpty(data1.FaDianChe) == false)
                    {
                        ct1.FaDianChe = int.Parse(data1.FaDianChe);
                        if (ct1.FaDianChe > 0)
                        {
                            ct1.GongDianType = EGongDianType.非直供电;
                        }
                    }
                    else
                    {
                        ct1.FaDianChe = 0;
                    }

                    if (String.IsNullOrEmpty(data1.ShuYinChe) == false)
                    {
                        ct1.ShuYinChe = int.Parse(data1.ShuYinChe);
                    }
                    if (String.IsNullOrEmpty(data1.YouZhengChe) == false)
                    {
                        //ct1.YouZhengChe =int.Parse(data1.YouZhengChe);
                    }

                    //设置线路里程或线路
                    ct1.YunXingLiCheng = int.Parse(data1.Mile);
                    ct1.CheDiShu = 2;
                    if (String.IsNullOrEmpty(data1.Cdzs) == false)
                    {
                        ct1.CheDiShu = int.Parse(data1.Cdzs);
                    }
                }
            }
            return train1;
        }

        //根据数据行，得到车次对象
        private static Train GetTrainObject(DataRow Traindr)
        {
            Train train1 = null;
            if (Traindr != null
                && Traindr["Line"].ToString().Trim() != String.Empty)
            {
                TrainLine trainLine=null;
                NewTrainBU nt = new NewTrainBU();
                if (Traindr["TrainType"].ToString().Trim() == "动车组")
                {
                    train1 = new HighTrain();
                    HighTrain ht1 = (HighTrain)train1;
                    ht1.CunZengMoShi = ECunZengMoShi.新人新车;
                    ht1.IsFullChengBen = true;
                    ht1.IsYearFlag = true;

                    //设置其他的值
                    ht1.TrainType = EHighTrainType.CRH2A;
                    ht1.YunXingLiCheng = int.Parse(Traindr["Mile"].ToString());
                    ht1.CheDiShu = 1;

                    int yz1 = 0, rz1 = 0;
                    if (String.IsNullOrEmpty(Traindr["YinZuo"].ToString()) == false)
                    {
                        yz1 = int.Parse(Traindr["YinZuo"].ToString());
                    }
                    if (String.IsNullOrEmpty(Traindr["RuanZuo"].ToString()) == false)
                    {
                        rz1 = int.Parse(Traindr["RuanZuo"].ToString());
                    }

                    if (yz1 + rz1 > 8)   //表示是长车
                    {
                        ht1.BianZhu = EHighTrainBianZhu.重联;
                    }

                    if (Traindr["Line"].ToString().Trim() != String.Empty)
                    {
                        String[] nodes1=Traindr["Line"].ToString().Trim().Split('-');
                        trainLine = Line.GetTrainLineByTrainTypeAndLineNoeds(ETrainType.动车CRH2A, false, nodes1);
                        if (trainLine != null)
                        {
                            ht1.Line = trainLine;
                        }
                    }
                }
                else
                {
                    train1 = new CommTrain();

                    //设置车的类型
                    ETrainType type = ETrainType.空调车25T;
                    CommTrain ct1 = (CommTrain)train1;
                    ct1.IsFullChengBen = true;
                    ct1.IsYearFlag = true;
                    bool hasDianche = false;

                    ct1.CunZengMoShi = ECunZengMoShi.新人新车;
                    String traintype1 = Traindr["TrainType"].ToString().Trim();
                    if (traintype1 == "空调特快")
                    {
                        ct1.TrainType = ECommTrainType.空调车25T;
                        type = ETrainType.空调车25T;
                    }
                    else if (traintype1 == "空调快速")
                    {
                        ct1.TrainType = ECommTrainType.空调车25K;
                        type = ETrainType.空调车25K;
                    }
                    else if (traintype1 == "空调普快")
                    {
                        ct1.TrainType = ECommTrainType.绿皮车25B;
                        type = ETrainType.绿皮车25B;
                        ct1.KongTiaoFlag = true;
                    }
                    else if (traintype1 == "快速")
                    {
                        ct1.TrainType = ECommTrainType.绿皮车25B;
                        type = ETrainType.绿皮车25B;
                        ct1.KongTiaoFlag = false;
                        ct1.JiaKuai = EJiaKuai.加快;   //可能是特快K1473  95/110
                    }
                    else if (traintype1 == "普快")
                    {
                        ct1.TrainType = ECommTrainType.绿皮车25B;
                        type = ETrainType.绿皮车25B;
                        ct1.KongTiaoFlag = false;
                    }
                    else
                    {
                        ct1.TrainType = ECommTrainType.空调车25T;
                        type = ETrainType.空调车25T;
                    }

                    //设置车厢编组
                    if (String.IsNullOrEmpty(Traindr["YinZuo"].ToString()) == false)
                    {
                        ct1.YinZuo = int.Parse(Traindr["YinZuo"].ToString());
                    }
                    else
                    {
                        ct1.YinZuo = 0;
                    }

                    if (String.IsNullOrEmpty(Traindr["RuanZuo"].ToString()) == false)
                    {
                        ct1.RuanZuo = int.Parse(Traindr["RuanZuo"].ToString());
                    }
                    else
                    {
                        ct1.RuanZuo = 0;
                    }

                    if (String.IsNullOrEmpty(Traindr["OpenYinWo"].ToString()) == false)
                    {
                        ct1.OpenYinWo = int.Parse(Traindr["OpenYinWo"].ToString());
                    }
                    else
                    {
                        ct1.OpenYinWo = 0;
                    }

                    if (String.IsNullOrEmpty(Traindr["CloseYinWo"].ToString()) == false)
                    {
                        ct1.CloseYinWo = int.Parse(Traindr["CloseYinWo"].ToString());
                    }
                    else
                    {
                        ct1.CloseYinWo = 0;
                    }

                    if (String.IsNullOrEmpty(Traindr["RuanWo"].ToString()) == false)
                    {
                        ct1.RuanWo = int.Parse(Traindr["RuanWo"].ToString());
                    }
                    else
                    {
                        ct1.RuanWo = 0;
                    }

                    if (String.IsNullOrEmpty(Traindr["AdvanceRuanWo"].ToString()) == false)
                    {
                        ct1.AdvanceRuanWo = int.Parse(Traindr["AdvanceRuanWo"].ToString());
                    }
                    else
                    {
                        ct1.AdvanceRuanWo = 0;
                    }

                    if (String.IsNullOrEmpty(Traindr["CanChe"].ToString()) == false)
                    {
                        ct1.CanChe = int.Parse(Traindr["CanChe"].ToString());
                    }
                    else
                    {
                        ct1.CanChe = 0;
                    }

                    if (String.IsNullOrEmpty(Traindr["FaDianChe"].ToString()) == false)
                    {
                        ct1.FaDianChe = int.Parse(Traindr["FaDianChe"].ToString());
                        if (ct1.FaDianChe > 0)
                        {
                            ct1.GongDianType = EGongDianType.非直供电;
                            hasDianche = true;
                        }
                    }
                    else
                    {
                        ct1.FaDianChe = 0;
                    }

                    if (String.IsNullOrEmpty(Traindr["ShuYinChe"].ToString()) == false)
                    {
                        ct1.ShuYinChe = int.Parse(Traindr["ShuYinChe"].ToString());
                    }
                    if (String.IsNullOrEmpty(Traindr["YouZhengChe"].ToString()) == false)
                    {
                        //ct1.YouZhengChe =int.Parse(data1.YouZhengChe);
                    }

                    //设置全程内燃机车的标志
                    if (Traindr["FULLNEIRANG"].ToString().Trim() == "1")
                    {
                        ct1.FullNeiRangChe = true;
                    }

                    //设置线路里程或线路
                    ct1.YunXingLiCheng = int.Parse(Traindr["Mile"].ToString());
                    ct1.CheDiShu = 2;
                    if (String.IsNullOrEmpty(Traindr["Cdzs"].ToString()) == false)
                    {
                        ct1.CheDiShu = int.Parse(Traindr["Cdzs"].ToString());
                    }

                    //设置Line
                    if (Traindr["Line"].ToString().Trim() != String.Empty)
                    {
                        String[] nodes1 = Traindr["Line"].ToString().Trim().Split('-');
                        trainLine = Line.GetTrainLineByTrainTypeAndLineNoeds(type, hasDianche, nodes1);
                        if (trainLine != null)
                        {
                            ct1.Line = trainLine;
                        }
                    }
                }
            }
            return train1;
        }

        #region 计算车次的真实值数据
        //得到真实的收入
        private  static double GetFactShouRu(JTable tab1,String TrainName, int year1, int month1)
        {
            tab1.TableName = "NEWTRAINSHOUROU";
            double result = 0;
            if (String.IsNullOrEmpty(TrainName) == false)
            {
                String trainName1 = TrainName.Replace("/", ",");
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("TrainName", trainName1, SearchOperator.Collection));
                condition.Add(new SearchField("byear", year1 + "", SearchFieldType.NumericType));
                condition.Add(new SearchField("bmonth", month1 + "", SearchFieldType.NumericType));
                DataRow dr1 = tab1.GetFirstDataRow(condition, "sum(shourou1)");
                if (dr1 != null)
                {
                    if (dr1[0].ToString() != String.Empty)
                    {
                        result  = double.Parse(dr1[0].ToString());
                    }
                }
            }
            return result/SRATE ;
        }

        //得到车次的真实运输旅客数和服务费单价标准
        private static void GetFactPCountAndFee(JTable tab1, String TrainName,
            int year1, int month1,out int pcount,out double fee,out double price)
        {
            tab1.TableName = "NEWTRAINSERVERPEOPLE";

            pcount = 0;
            price = 0;
            fee = 0;

            if (String.IsNullOrEmpty(TrainName) == false)
            {
                String trainName1 = TrainName.Replace("/", ",");
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("TrainName", trainName1, SearchOperator.Collection));
                condition.Add(new SearchField("byear", year1 + "", SearchFieldType.NumericType));
                condition.Add(new SearchField("bmonth", month1 + "", SearchFieldType.NumericType));
                DataRow dr1 = tab1.GetFirstDataRow(condition, "sum(pc1+pc2+pc3) pc0,sum(Fee1+Fee2+Fee3) Fee0,Max(Price) price");
                if (dr1 != null)
                {
                    if (dr1[0].ToString() != String.Empty)
                    {
                        pcount  = int.Parse(dr1[0].ToString());
                        fee = double.Parse(dr1[1].ToString())/SRATE;
                        price = double.Parse(dr1[2].ToString());
                    }
                }
            }
        }

        //线路使用费
        private static double GetFactFee1(JTable tab1, String TrainName, int year1, int month1)
        {
            tab1.TableName = "NEWTRAINXIANLUFEE";
            double result = 0;
            if (String.IsNullOrEmpty(TrainName) == false)
            {
                String trainName1 = TrainName.Replace("/", ",");
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("TrainName", trainName1, SearchOperator.Collection));
                condition.Add(new SearchField("byear", year1 + "", SearchFieldType.NumericType));
                condition.Add(new SearchField("bmonth", month1 + "", SearchFieldType.NumericType));
                DataRow dr1 = tab1.GetFirstDataRow(condition, "sum(fee1+fee2+fee3)");
                if (dr1 != null)
                {
                    if (dr1[0].ToString() != String.Empty)
                    {
                        result = double.Parse(dr1[0].ToString());
                    }
                }
            }
            return result/SRATE ;
        }

        //机车牵引费
        private static double GetFactFee2(JTable tab1, String TrainName, int year1, int month1)
        {
            tab1.TableName = "NEWTRAINQIANYINFEE";
            double result = 0;
            if (String.IsNullOrEmpty(TrainName) == false)
            {
                String trainName1 = TrainName.Replace("/", ",");
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("TrainName", trainName1, SearchOperator.Collection));
                condition.Add(new SearchField("byear", year1 + "", SearchFieldType.NumericType));
                condition.Add(new SearchField("bmonth", month1 + "", SearchFieldType.NumericType));
                DataRow dr1 = tab1.GetFirstDataRow(condition, "sum(fee1+fee2+fee3)");
                if (dr1 != null)
                {
                    if (dr1[0].ToString() != String.Empty)
                    {
                        result = double.Parse(dr1[0].ToString());
                    }
                }
            }
            return result/SRATE ;
        }

        //电费
        private static double GetFactFee3(JTable tab1, String TrainName, int year1, int month1)
        {
            tab1.TableName = "NEWTRAINDIANFEE";
            double result = 0;
            if (String.IsNullOrEmpty(TrainName) == false)
            {
                String trainName1 = TrainName.Replace("/", ",");
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("TrainName", trainName1, SearchOperator.Collection));
                condition.Add(new SearchField("byear", year1 + "", SearchFieldType.NumericType));
                condition.Add(new SearchField("bmonth", month1 + "", SearchFieldType.NumericType));
                DataRow dr1 = tab1.GetFirstDataRow(condition, "sum(fee1+fee2+fee3)");
                if (dr1 != null)
                {
                    if (dr1[0].ToString() != String.Empty)
                    {
                        result = double.Parse(dr1[0].ToString());
                    }
                }
            }
            return result/SRATE;
        }
        #endregion

        #region  更改站点名称
        public bool ChangeTrainStationName(string oldName,string newName)
        {
            //对两个表进行关联更改(LINESTATION,NEWTRAIN)
            JTable tab1 = new JTable("LINESTATION");
            JTable tab2 = new JTable("NEWTRAIN");
            JTable tab3 = new JTable("TRAINLINE");
            List<SearchField> condition=new List<SearchField> ();
            SearchField s1=new SearchField ("ASTATION",oldName,WebFrame.SearchOperator.Equal);
            SearchField s2=new SearchField ("BSTATION",oldName,WebFrame.SearchOperator.Equal);
            SearchField s3=new SearchField ("LINE",oldName,WebFrame.SearchOperator.Contains);
            

            Dictionary<string,object> dic1=new Dictionary<string,object> ();
            dic1.Add("ASTATION",newName);
            Dictionary<string, object> dic2 = new Dictionary<string, object>();
            dic2.Add("BSTATION",newName);
            //开启事务
            JConnect conn = JConnect.GetConnect();
            conn.BeginTrans();
            try
            {
                condition.Add(s1);
                tab1.EditData(dic1, condition);
                tab2.EditData(dic1, condition);
                tab3.EditData(dic1, condition);

                condition.Clear();
                condition.Add(s2);
                tab1.EditData(dic2, condition);
                tab2.EditData(dic2, condition);
                tab3.EditData(dic2, condition);

                List<SearchField> condition1 = new List<SearchField>();
                condition1.Add(s3);
                JTable tab4 = new JTable("NEWTRAIN");//注意这里不能共用tab2
                DataTable dt = tab4.SearchData(condition1, -1, "*").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string oldStr = dr["LINE"].ToString();
                        string newStr = oldStr.Replace(oldName, newName);
                        dic1.Clear();
                        dic1.Add("LINE", newStr);
                        tab2.EditData(dic1, condition);
                    }

                }
                conn.CommitTrans();
            }
            catch(Exception ex)
            {
                conn.RollBackTrans();
                return false;
            }

            return true;
        }

        /// <summary>
        /// 查询包含站点的线路
        /// </summary>
        /// <param name="containName"></param>
        /// <returns></returns>
        public DataTable GetLineList(string containName,string direction) 
        {
            JTable tab = new JTable("TrainLine");
            String sql = string.Format("select * from TrainLine where exists (select 1 from LINESTATION where lineid=TrainLine.lineid and  direction={0} and (Astation='{1}' or BStation='{2}' ))",direction,containName,containName);
            tab.CommandText = sql;
             return tab.SearchData(-1).Tables[0];   
        }
        #endregion
        /// <summary>
        /// 查询包含两相连站点的线路
        /// </summary>
        /// <param name="containName"></param>
        /// <returns></returns>
        public DataTable GetNextLineList(string containNameA,string containNameB,string direction) 
        {

            JTable tab = new JTable("TrainLine");
            String sql = string.Format("select * from TrainLine where exists (select 1 from LINESTATION where lineid=TrainLine.lineid and  direction={0} and (Astation='{1}' and BStation='{2}' ))", direction, containNameA, containNameB);
            tab.CommandText = sql;
            return tab.SearchData(-1).Tables[0];  
        }

        #region  删除站点
        /// <summary>
        /// 删除线路中的站点
        /// 说明：根据站点的名称，找到合适的线路（同上）
        /// 根据线路LineStation中Direction=0 和 Direction=1的两种情况分别按num进行排序
        /// 将线路中LINESTATION中 的所有站点数据 按下面的规则进行调整
        /// 如果首站点的 第一条数据的AStation=StationName，则删除该数据，并重新调整编号num
        /// 如果是最后一站，则判断BStation ,如果相等，则直接删除
        /// 如果中间站点 AStation 不等于 StationName，则只比较BStation中的数据是否和StationName相等
        /// 如果相同，则删除该数据，并将下一个数据的AStation改成该条数据的AStation
        /// 对站点处理完成后，要及时退出循环，避免做无用的循环数据
        /// 要分别对Direction=0 和 Direction=1的处理。
        /// NEWTRAIN中的数据调整比较简单，只要把Line中的 武汉- 和 -武汉 替换成空字符串就可以了。
        /// 此操作比较重要，要使用事务处理。
        /// </summary>
        public bool DeleteTrainStation(string StationName)
        {
            //得到lineid集
            DataTable dt = GetLineList(StationName, "0");
            //DataTable dt1 = GetLineList(StationName,"1");//与上面的datatable一样
            JConnect conn = JConnect.GetConnect();
            conn.BeginTrans();
            try
            {
               
                List<SearchField> condition = new List<SearchField>();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                #region direction=0的情况
                if (dt.Rows.Count > 0)
                {                
                    foreach (DataRow dr in dt.Rows)
                    {
                       
                        /////根据lineid得到线路（根据类LineTrainBU类的GetLineStationData（string lineid））DataTable
                        DataTable dtLineStation = GetLineStationByDirection(dr["LineID"].ToString(), "0");
                        DataTable dtLineStation1 = GetLineStationByDirection(dr["LineID"].ToString(), "1");
                        #region  根据dtLineStation表格进行删除
                        //情况1：删除始发站
                        if (dtLineStation.Rows.Count > 0)
                        {
                            if (dtLineStation.Rows[0]["AStation"].ToString() == StationName)
                            {
                                condition.Clear();

                                condition.Add(new SearchField("ID", dtLineStation.Rows[0]["ID"].ToString()));
                                JTable tab = new JTable("LineStation");
                                tab.DeleteData(condition);
                                //更改Num值
                                for (int i = 1; i < dtLineStation.Rows.Count; i++)
                                {
                                    condition.Clear();
                                    dic.Clear();
                                    condition.Add(new SearchField("ID", dtLineStation.Rows[i]["ID"].ToString()));
                                    dic.Add("Num", (Convert.ToInt32(dtLineStation.Rows[i]["Num"]) - 1).ToString());
                                    JTable tab1 = new JTable("LineStation");
                                    tab1.EditData(dic, condition);

                                }
                            }
                            //情况2：删除终点站
                            else if (dtLineStation.Rows[dtLineStation.Rows.Count - 1]["BStation"].ToString() == StationName)
                            {
                                condition.Clear();
                                condition.Add(new SearchField("ID", dtLineStation.Rows[dtLineStation.Rows.Count - 1]["ID"].ToString()));
                                JTable tab2 = new JTable("LineStation");
                                tab2.DeleteData(condition);
                            }
                            //情况3：删除中间值
                            else
                            {
                                
                                condition.Clear();
                                JTable tab3 = new JTable("LineStation");
                                condition.Add(new SearchField("LineID", dr["LineID"].ToString(), SearchFieldType.NumericType));
                                condition.Add(new SearchField("direction", "0"));   //增加方向的控制
                                condition.Add(new SearchField("AStation", StationName));//根据Astation找到中间项武昌
                                tab3.OrderBy = "num";
                                DataRow drItem = tab3.GetFirstDataRow(condition, "*");//得到中间站点为AStation项（武昌——汉口）
                                //删除该站点
                                condition.Clear();
                                condition.Add(new SearchField("ID", drItem["ID"].ToString()));
                                JTable tab4 = new JTable("LineStation");
                                tab4.DeleteData(condition);
                                //更改上一站点（汉阳--武昌）
                                condition.Clear();
                                JTable tab5 = new JTable("LineStation");
                                condition.Add(new SearchField("LineID", dr["LineID"].ToString(), SearchFieldType.NumericType));
                                condition.Add(new SearchField("direction", "0"));   //增加方向的控制
                                condition.Add(new SearchField("BStation", StationName));//根据Astation找到中间项
                                tab5.OrderBy = "num";
                                DataRow drItemHead = tab5.GetFirstDataRow(condition, "*");//得到中间站点为Bstation项（汉阳--武昌）
                                dic.Clear();
                                condition.Clear();
                                dic.Add("BStation", drItem["BStation"].ToString());
                                dic.Add("Miles",(Convert.ToInt32(drItem["Miles"])+Convert.ToInt32(drItemHead["Miles"])).ToString());//将站点里程更改
                                condition.Add(new SearchField("ID", drItemHead["ID"].ToString()));
                                JTable tab6 = new JTable("LineStation");
                                tab6.EditData(dic, condition);//将三个站点删除一个变成两个（汉阳--汉口）
                                //更改Num值
                                condition.Clear();
                                SearchField s1 = new SearchField("AStation", StationName, WebFrame.SearchOperator.NotEqual);
                                SearchField s2 = new SearchField("BStation", StationName, WebFrame.SearchOperator.NotEqual);
                                condition.Add(s1 | s2);
                                condition.Add(new SearchField("LineID", dr["LineID"].ToString(), SearchFieldType.NumericType));
                                condition.Add(new SearchField("direction", "0"));   //增加方向的控制

                                //重新实例化，否则报错
                                JTable tab7 = new JTable("LineStation");
                                DataTable dtTemp = tab7.SearchData(condition, -1, "*").Tables[0];



                                if (dtTemp.Rows.Count > 0)
                                {
                                    foreach (DataRow drTemp in dtTemp.Rows)
                                    {
                                        dic.Clear();
                                        condition.Clear();
                                        condition.Add(new SearchField("ID", drTemp["ID"].ToString()));
                                        dic.Add("Num", (Convert.ToInt32(drTemp["Num"]) - 1).ToString());
                                        JTable tab8 = new JTable("LineStation");
                                        tab8.EditData(dic, condition);
                                    }
                                }
                            }
                        }
                        #endregion
                        #region 根据dtLineStation1表格进行删除
                      
                        //情况1：删除始发站
                        if (dtLineStation1.Rows.Count > 0)
                        {
                            
                            if (dtLineStation1.Rows[0]["AStation"].ToString() == StationName)
                            {
                               
                                condition.Clear();

                                condition.Add(new SearchField("ID", dtLineStation1.Rows[0]["ID"].ToString()));
                                JTable tabNew1 = new JTable("LINESTATION");//重新实例化否则报错
                                tabNew1.DeleteData(condition);
                                //更改Num值
                                for (int i = 1; i < dtLineStation1.Rows.Count; i++)
                                {
                                    condition.Clear();
                                    dic.Clear();
                                    condition.Add(new SearchField("ID", dtLineStation1.Rows[i]["ID"].ToString()));
                                    dic.Add("Num", (Convert.ToInt32(dtLineStation1.Rows[i]["Num"]) - 1).ToString());
                                    JTable tabNew2 = new JTable("LINESTATION");//重新实例化否则报错
                                    tabNew2.EditData(dic, condition);

                                }
                            }
                            //情况2：删除终点站
                            else if (dtLineStation1.Rows[dtLineStation1.Rows.Count - 1]["BStation"].ToString() == StationName)
                            {
                                condition.Clear();
                                condition.Add(new SearchField("ID", dtLineStation1.Rows[dtLineStation1.Rows.Count - 1]["ID"].ToString()));
                                JTable tabNew3 = new JTable("LINESTATION");//重新实例化否则报错
                                tabNew3.DeleteData(condition);
                            }
                            //情况3：删除中间值
                            else
                            {
                                condition.Clear();
                                JTable tabNew4 = new JTable("LINESTATION");//重新实例化否则报错
                                condition.Add(new SearchField("LineID", dr["LineID"].ToString(), SearchFieldType.NumericType));
                                condition.Add(new SearchField("direction", "1"));   //增加方向的控制
                                condition.Add(new SearchField("AStation", StationName));//根据Astation找到中间项武昌
                                tabNew4.OrderBy = "num";
                                DataRow drItem = tabNew4.GetFirstDataRow(condition, "*");//得到中间站点为AStation项（武昌——汉口）
                                //删除该站点
                                condition.Clear();
                                condition.Add(new SearchField("ID", drItem["ID"].ToString()));
                                JTable tabNew5 = new JTable("LINESTATION");//重新实例化否则报错
                                tabNew5.DeleteData(condition);
                                //更改上一站点（汉阳--武昌）
                                condition.Clear();
                                JTable tabNew6 = new JTable("LINESTATION");//重新实例化否则报错
                                condition.Add(new SearchField("LineID", dr["LineID"].ToString(), SearchFieldType.NumericType));
                                condition.Add(new SearchField("direction", "1"));   //增加方向的控制
                                condition.Add(new SearchField("BStation", StationName));//根据Astation找到中间项
                                tabNew6.OrderBy = "num";
                                DataRow drItemHead = tabNew6.GetFirstDataRow(condition, "*");//得到中间站点为Bstation项（汉阳--武昌）
                                dic.Clear();
                                condition.Clear();
                                dic.Add("BStation", drItem["BStation"].ToString());
                                dic.Add("Miles", (Convert.ToInt32(drItem["Miles"]) + Convert.ToInt32(drItemHead["Miles"])).ToString());//将站点里程更改
                                condition.Add(new SearchField("ID", drItemHead["ID"].ToString()));
                                JTable tabNew7 = new JTable("LINESTATION");//重新实例化否则报错
                                tabNew7.EditData(dic, condition);//将三个站点删除一个变成两个（汉阳--汉口）
                                //更改Num值
                                condition.Clear();
                                SearchField s1 = new SearchField("AStation", StationName, WebFrame.SearchOperator.NotEqual);
                                SearchField s2 = new SearchField("BStation", StationName, WebFrame.SearchOperator.NotEqual);
                                condition.Add(s1|s2);
                                condition.Add(new SearchField("LineID", dr["LineID"].ToString(), SearchFieldType.NumericType));
                                condition.Add(new SearchField("direction", "1"));   //增加方向的控制

                                //重新实例化，否则报错
                                JTable tabNew8 = new JTable("LINESTATION");//重新实例化否则报错
                                DataTable dtTemp = tabNew8.SearchData(condition, -1, "*").Tables[0];



                                if (dtTemp.Rows.Count > 0)
                                {
                                    foreach (DataRow drTemp in dtTemp.Rows)
                                    {
                                        dic.Clear();
                                        condition.Clear();
                                        condition.Add(new SearchField("ID", drTemp["ID"].ToString()));
                                        dic.Add("Num", (Convert.ToInt32(drTemp["Num"]) - 1).ToString());
                                        JTable tabNew9 = new JTable("LINESTATION");//重新实例化否则报错
                                        tabNew9.EditData(dic, condition);
                                    }
                                }
                            }
                        }
                     
                        #endregion
                    }
                }
                #endregion
                //更改NEWTRAIN表中的数据
                JTable tabNewTrain = new JTable("NEWTRAIN");
                condition.Clear();
                condition.Add(new SearchField("Line",StationName,WebFrame.SearchOperator.Contains));
                DataTable dtNewTrain = tabNewTrain.SearchData(condition, -1, "*").Tables[0];
                if (dtNewTrain.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtNewTrain.Rows)
                    {
                        string oldStr = dr["LINE"].ToString();
                        string newStr=string.Empty;
                              
                        string model=string.Format("{0}-",StationName);
                        string model1=string.Format("-{0}",StationName);
                        if(oldStr.Contains(model)&&oldStr.StartsWith(model))
                        {
                            newStr=oldStr.Replace(model,string.Empty);
                        }
                        if (oldStr.Contains(model1))
                        {
                            newStr = oldStr.Replace(model1, string.Empty);
                        }
                        dic.Clear();
                        dic.Add("LINE", newStr);
                        condition.Clear();
                        tabNewTrain.EditData(dic, condition);
                    }

                }
               
                conn.CommitTrans();
            }
            catch (Exception ex)
            {
                conn.RollBackTrans();
                return false;
            }
            return true;
        }
        #endregion
        /// <summary>
        /// 根据lineid得到线路（根据类LineTrainBU类的GetLineStationData（string lineid））DataTable
        /// </summary>
        /// <param name="LineID"></param>
        /// <param name="direction"></param>
        /// <returns></returns>

        private DataTable GetLineStationByDirection(string LineID,string direction) 
        {
            DataTable dt1 = new DataTable();
            if (String.IsNullOrEmpty(LineID) == false
               && JValidator.IsInt(LineID))
            {
                JTable tab1 = new JTable("LineStation");
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("LineID", LineID, SearchFieldType.NumericType));
                condition.Add(new SearchField("direction", direction));   //增加方向的控制

                tab1.OrderBy = "num";
                DataSet ds1 = tab1.SearchData(condition, -1, new String[] { "ID", "AStation", "lineid", "BStation", 
                                                                           "Miles", "num","direction","jnflag","dqh","shipflag","Fee1","Fee2","Fee3","GTLLX" });
                dt1 = ds1.Tables[0];


                //假如dt1没数据，则设置初始的数据
                if (dt1.Rows.Count == 0)
                {
                    DataRow dr1 =Line.GetLineData(LineID);
                    if (dr1 != null)
                    {
                        DataRow dr0 = ds1.Tables[0].NewRow();

                        dr0["lineid"] = LineID;
                        dr0["num"] = 1;
                        dr0["astation"] = dr1["astation"];
                        dr0["bstation"] = dr1["bstation"];
                        dr0["miles"] = dr1["miles"];
                        dr0["direction"] = "0";
                        ds1.Tables[0].Rows.Add(dr0);

                        //反方向路线站
                        DataRow dr2 = ds1.Tables[0].NewRow();
                        dr2["lineid"] = LineID;
                        dr2["num"] = 1;
                        dr2["astation"] = dr1["bstation"];
                        dr2["bstation"] = dr1["astation"];
                        dr2["miles"] = dr1["miles"];
                        dr2["direction"] = "1";
                        ds1.Tables[0].Rows.Add(dr2);

                        //更新数据显示
                        tab1.Update(ds1.Tables[0]);
                        dr2.Delete();
                        ds1.Tables[0].AcceptChanges();
                    }
                }
                tab1.Close();
            }
            return dt1;
        }
        #region 获得LineStation(相邻两站) 的lineid
        public DataRow GetDataRowFromLineStation(string astation,string bstation,string direction)
        {
            JTable tab = new JTable("LineStation");
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("Astation",astation));
            condition.Add(new SearchField("Bstation", bstation));
            condition.Add(new SearchField("direction",direction));
            DataRow dr=tab.GetFirstDataRow(condition, "*");
            return dr;
        
        }

        /// <summary>
        /// 更改新站点
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="lineid"></param>
        public void EditLineStation(Dictionary<string,object> dic,string id) 
        {
            JTable tab = new JTable("LineStation");
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("id",id));
            tab.EditData(dic, condition);
        }

        public void EditLineStation(Dictionary<string, object> dic, List<SearchField> condition)
        {
            JTable tab = new JTable("LineStation");
            
            tab.EditData(dic, condition);
        }

        /// <summary>
        /// 插入新站点
        /// </summary>
        /// <param name="dic"></param>
        public void NewLineStation(Dictionary<string,object> dic) 
        {

            JTable tab = new JTable("LineStation");
            tab.InsertData(dic);
        }

        /// <summary>
        /// 得到所有nums
        /// </summary>
        /// <param name="lineid"></param>
        /// <param name="num"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public DataTable GetNumsLineStation(string lineid,string num,string direction) 
        {
            JTable tab = new JTable("LineStation");
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("lineid",lineid));
            condition.Add(new SearchField("direction",direction));
            condition.Add(new SearchField("num",num,WebFrame.SearchOperator.Bigger));
            return  tab.SearchData(condition, -1, "*").Tables[0];
        }

        /// <summary>
        /// 得到始发站
        /// </summary>
        /// <param name="trainname">车名</param>
        /// <returns>始发站名称</returns>
        public static string GetAStation(string trainname)
        {
            JTable tab = new JTable("NEWTRAIN");
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("TRAINNAME", trainname));

            if (!tab.HasData(condition))
            {
                return "";
            }
            else
            {
                return tab.GetFirstDataRow(condition, "ASTATION")["ASTATION"].ToString().Trim();
            }
        }

        /// <summary>
        /// 得到终点站
        /// </summary>
        /// <param name="trainname">车名</param>
        /// <returns>终点站名称</returns>
        public static string GetBStation(string trainname)
        {
            JTable tab = new JTable("NEWTRAIN");
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("TRAINNAME", trainname));

            if (!tab.HasData(condition))
            {
                return "";
            }
            else
            {
                return tab.GetFirstDataRow(condition, "BSTATION")["BSTATION"].ToString().Trim();
            }
        }
        #endregion
    }
}
