using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using WebFrame.Data;
using WebFrame;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using WebFrame.Util;
using System.Data.OleDb;

namespace BusinessRule
{
    public class TrainPersonBU
    {
        #region private 方法
        //得到列车类型
        private static String[] GetTrainType(bool dongche)
        {
            String[] arr1 = null;
            JTable tab1 = new JTable();
            String fs=String.Empty;
            if (dongche)
            {
                tab1.TableName = "hightrainprofile";
                tab1.OrderBy = "id";
                fs="hightraintype";
            }
            else
            {
                tab1.TableName = "commtrainweightprofile";
                tab1.OrderBy = "num";
                fs="traintype";
            }
            DataSet ds1 = tab1.SearchData(null, -1, fs);
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                arr1=new String[ds1.Tables[0].Rows.Count];
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    DataRow dr1 = ds1.Tables[0].Rows[i];
                    String text1 = dr1[0].ToString();
                    arr1[i] = text1;
                }
            }
            tab1.Close();
            return arr1;
        }

        //得到列车的岗位
        public static void GetTrainGw(out String[] gw1,out String[] gw2)
        {
            gw1= null;
            gw2 = null;
            JTable tab1 = new JTable();
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("gw is not null", "", SearchOperator.UserDefine));

            tab1.TableName = "persongz";
            tab1.OrderBy = "kind,num";
            DataSet ds1 = tab1.SearchData(condition, -1, "*");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                DataRow[] drs1 = ds1.Tables[0].Select("kind='0'");
                DataRow[] drs2 = ds1.Tables[0].Select("kind='1'");
                if (drs1 != null && drs1.Length > 0)
                {
                    gw1 = new String[drs1.Length];
                    for (int i = 0; i < drs1.Length; i++)
                    {
                        gw1[i] = drs1[i]["gw"].ToString();
                    }
                }


                gw2 = new String[drs2.Length];
                if (drs2 != null && drs2.Length > 0)
                {
                    gw2 = new String[drs2.Length];
                    for (int i = 0; i < drs2.Length; i++)
                    {
                        gw2[i] = drs2[i]["gw"].ToString();
                    }
                }
            }
            tab1.Close();
        }

        private static void InsertData(String traintype, String gw,String kind, JTable tab1)
        {
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("kind", kind));
            condition.Add(new SearchField("traintype", traintype));
            condition.Add(new SearchField("gw", gw));
            DataSet ds1 = tab1.SearchData(condition, -1, "*");
            if (ds1.Tables[0].Rows.Count == 0)
            {
                DataRow dr1 = ds1.Tables[0].NewRow();
                dr1["kind"] = kind;
                dr1["traintype"] = traintype;
                dr1["gw"] = gw;
                dr1["pcount"] = GetDefaultPCount(kind, traintype, gw);
                ds1.Tables[0].Rows.Add(dr1);
                tab1.Update(ds1.Tables[0]);
            }
        }

        private static int GetDefaultPCount(String kind, String traintype, String gw)
        {
            int result = 1;
            if (kind == "0")
            {
                if (gw == "列车长" || gw == "车长")
                {
                    result = 1;
                }
                else if (gw == "客运乘务员" || gw == "乘务员")
                {
                    result = 8;
                }
                else if (gw == "车辆乘务员" || gw == "机械师" || gw=="车检")
                {
                    result = 2;
                }
                else
                {
                    result = 1;
                }
            }
            else
            {
                if (gw == "随车机械师" || gw == "车检" || gw == "机械师")
                {
                    result = 2;
                }
                else if (gw == "司机" || gw == "动车司机")
                {
                    result = 2;
                }
                else if (gw == "客车乘务员" || gw == "乘务员")
                {
                    result = 4;
                    if (traintype == "CRH380AL" || traintype =="CRH380BL")          //增加了CRH380B和CRH380BL的处理
                    {
                        result = 8;
                    }
                }
                else if (gw == "列车长" || gw == "车长")
                {
                    result = 1;
                    if (traintype == "CRH380AL" || traintype == "CRH380BL")          //增加了CRH380B和CRH380BL的处理
                    {
                        result = 2;
                    }
                }
                else
                {
                    result = 1;
                }
            }
            return result;
        }

        //得到车的类型字符串和类别
        private static void GetTrainTypeForGz(ETrainType type1,
            out String traintype1, out String kind,bool hadDianche)
        {
            traintype1 = String.Empty;
            kind = String.Empty;

            switch (type1)
            {
                //============客车================
                case ETrainType.绿皮车25B:
                    traintype1 = "25B";
                    kind = "0";
                    break;
                case ETrainType.空调车25T:
                    traintype1 = "25T";
                    kind = "0";
                    break;

                case ETrainType.空调车25K:
                    if (hadDianche)
                    {
                        traintype1 = "25G(非直)";
                    }
                    else
                    {
                        traintype1 = "25G(直)";
                    }
                    kind = "0";
                    break;
                case ETrainType.空调车25G:
                    if (hadDianche)
                    {
                        traintype1 = "25G(非直)";
                    }
                    else
                    {
                        traintype1 = "25G(直)";
                    }
                    kind = "0";
                    break;


                //==========动车===============
                case ETrainType.动车CRH2E:
                    traintype1 = "CRH动卧";
                    kind = "1";
                    break;

                case ETrainType.动车CRH380AL:
                    traintype1 = "CRH动坐(16)";
                    kind = "1";
                    break;

                //---------修改的地方 13年3月13日------------
                case ETrainType.动车CRH380BL:
                    traintype1 = "CRH动坐(16)";
                    kind = "1";
                    break;
                //--------------------------------------------

                case ETrainType.动车CRH5A:
                    traintype1 = "CRH动坐(8)";
                    kind = "1";
                    break;
                
                case ETrainType.动车CRH380A:
                    traintype1 = "CRH动坐(8)";
                    kind = "1";
                    break;

                //---------修改的地方 13年3月13日------------
                case ETrainType.动车CRH380B:
                    traintype1 = "CRH动坐(8)";
                    kind = "1";
                    break;
                //--------------------------------------------
               
                case ETrainType.动车CRH2C:
                    traintype1 = "CRH动坐(8)";
                    kind = "1";
                    break;
                case ETrainType.动车CRH2B:
                    traintype1 = "CRH动坐(8)";
                    kind = "1";
                    break;
                case ETrainType.动车CRH2A:
                    traintype1 = "CRH动坐(8)";
                    kind = "1";
                    break;

                default:
                    break;
            }
        }

        #endregion 

        //人员配置初始化
        public static void PersonInit()
        {
            //得到动车和客车的类型数组
            String[] trainType1 = GetTrainType(false);
            String[] trainType2 = GetTrainType(true);

            String[] gw1 = null;
            String[] gw2 = null;

            JTable tab1 = new JTable("TrainPerson");
            GetTrainGw(out gw1, out gw2);
            if (trainType1 != null && trainType1.Length > 0 && gw1 != null && gw1.Length > 0)
            {
                foreach (String type1 in trainType1)
                {
                    foreach (String gw in gw1)
                    {
                        InsertData(type1, gw, "0", tab1);
                    }
                }
            }


            //设置动车组
            if (trainType2 != null && trainType2.Length > 0 && gw2 != null && gw2.Length > 0)
            {
                foreach (String type1 in trainType2)
                {
                    foreach (String gw in gw2)
                    {
                        InsertData(type1, gw, "1", tab1);
                    }
                }
            }
            tab1.Close();
        }

        //得到列表数据
        public static DataSet GetPersonGwList()
        {
            DataSet ds1 = null;
            JTable tab1 = new JTable("TrainPerson");
            tab1.OrderBy = "kind,traintype,num";
            ds1 = tab1.SearchData(null, -1, "*");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                String first = ds1.Tables[0].Rows[0]["traintype"].ToString();
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    DataRow dr1=ds1.Tables[0].Rows[i];
                    String Second = dr1["traintype"].ToString();
                    if (Second == first && i != 0)
                    {
                        dr1["traintype"] = "";
                    }
                    else
                    {
                        first = Second;
                    }
                }
            }
            tab1.Close();
            return ds1;
        }

        //更新列表数据
        public static void UpdateListData(DataTable dt1)
        {
            JTable tab1 = new JTable("TrainPerson");
            tab1.MyConnect.BeginTrans();
            try
            {
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("1=1","",SearchOperator.UserDefine));
                tab1.DeleteData(condition);

                //插入新的数据
                Dictionary<String, object> data1 = new Dictionary<string, object>();
                foreach (DataRow dr1 in dt1.Rows)
                {
                    int pcount = int.Parse(dr1["pcount"].ToString());
                    if (pcount > 0)
                    {
                        data1.Clear();
                        data1["traintype"] = dr1["traintype"];
                        data1["kind"] = dr1["kind"];
                        data1["gw"] = dr1["gw"];
                        data1["pcount"] = dr1["pcount"];
                        tab1.InsertData(data1);
                    }
                }
                tab1.MyConnect.CommitTrans();
            }
            catch
            {
                tab1.MyConnect.RollBackTrans();
            }
            tab1.Close();
        }

        //得到人员的工资和附加费
        public static double GetPersonGzAndFjFee(ETrainType traintype1,bool hasDianChe,
            int yz,int yw,int rz)
        {
            double fee1 = 0;
            String kind = "1";
            String traintype="";
            GetTrainTypeForGz(traintype1, out traintype, out kind,hasDianChe);

            JTable tab1 = new JTable("trainpersongzview");
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("traintype", traintype));
            DataSet ds1 = tab1.SearchData(condition, -1, "*");
            
            //记录服务员的值
            double total = 0;
            double fwygz = 0;
            double fwyfj = 0;

            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                //根据车型和岗位，计算相应的资费标准
                double fj = 0;
                if (dr1["FJ"].ToString().Trim() != String.Empty)
                {
                    fj = double.Parse(dr1["FJ"].ToString());
                }

                double gz1 = 0;
                if (dr1["GZ"].ToString().Trim() != String.Empty)
                {
                    gz1 = double.Parse(dr1["gz"].ToString());
                }

                double pcount1 = double.Parse(dr1["pcount"].ToString().Trim());
                String gl1 = dr1["gl"].ToString().Trim();
                if (gl1 != String.Empty)
                {
                    if (gl1 == "0")
                    {
                        pcount1 = pcount1 * yz;
                    }
                    else if (gl1 == "2")
                    {
                        pcount1 = pcount1 * yw;
                    }
                    else if (gl1 == "3")
                    {
                        pcount1 = pcount1 * rz;
                    }
                    total = total + pcount1;
                    if (fwygz == 0)
                    {
                        fwygz = gz1;
                        fwyfj = fj;
                    }
                }

                double t1 = gz1 * (1 + fj / 100d);
                fee1 = fee1 + t1 * pcount1;

                #region OLD Function
                /*
                DataRow[] drs = PersonGZProfile.Data.Select("kind='"+kind+"' and gw='"+gw1+"'");
                if (drs != null && drs.Length > 0)
                {
                    DataRow dr = drs[0];
                    double fj = 0;
                    if (dr["FJ"].ToString().Trim() != String.Empty)
                    {
                        fj = double.Parse(dr["FJ"].ToString());
                    }

                    double gz1 = 0;
                    if (dr["GZ"].ToString().Trim() != String.Empty)
                    {
                        gz1 = double.Parse(dr["gz"].ToString());
                    }
                    double t1 = gz1 * (1 + fj / 100d);
                    fee1 = fee1 + t1*pcount1; 
                }*/
                #endregion
            }

            //对人数取整的补偿处理
            if (total > 0)
            {
                double t2 = fwygz * (1 + fwyfj / 100d);
                fee1 = fee1 + t2 * (Math.Ceiling(total) - total);
            }
            tab1.Close();
            return fee1;
        }

        //得到人员的其他费用
        public static double GetPersonQtFee(ETrainType traintype1, 
            bool hasDianChe, int yz, int yw, int rz)
        {
            double fee1 = 0;
            String kind = "1";
            String traintype = "";
            GetTrainTypeForGz(traintype1, out traintype, out kind,hasDianChe);

            JTable tab1 = new JTable("trainpersongzview");
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("traintype", traintype));
            DataSet ds1 = tab1.SearchData(condition, -1, "*");

            //记录服务员的值
            double total = 0;
            double fwyqtfy = 0;
           
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                double qtfy = 0;
                if (dr1["QTFY"].ToString().Trim() != String.Empty)
                {
                    qtfy = double.Parse(dr1["QTFY"].ToString());
                }

                double pcount1 = double.Parse(dr1["pcount"].ToString().Trim());
                String gl1 = dr1["gl"].ToString().Trim();
                if (gl1 != String.Empty)
                {
                    if (gl1 == "0")
                    {
                        pcount1 = pcount1 * yz;
                    }
                    else if (gl1 == "2")
                    {
                        pcount1 = pcount1 * yw;
                    }
                    else if (gl1 == "3")
                    {
                        pcount1 = pcount1 * rz;
                    }
                    total = total + pcount1;
                    if (fwyqtfy  == 0)
                    {
                        fwyqtfy = qtfy;
                    }
                }

                double t1 = qtfy;
                fee1 = fee1 + t1 * pcount1;

                /*
                //根据车型和岗位，计算相应的资费标准
                DataRow[] drs = PersonGZProfile.Data.Select("kind='" + kind + "' and gw='" + gw1 + "'");
                if (drs != null && drs.Length > 0)
                {
                    DataRow dr = drs[0];
                    double qtfy = 0;
                    if (dr["QTFY"].ToString().Trim() != String.Empty)
                    {
                        qtfy  = double.Parse(dr["QTFY"].ToString());
                    }

                    double t1 = qtfy;
                    fee1 = fee1 + t1 * pcount1;
                }*/
            }

            //对人数取整的补偿处理
            if (total > 0)
            {
               fee1 = fee1 + fwyqtfy * (Math.Ceiling(total) - total);
            }
            tab1.Close();
            return fee1;
        }
    }
}
