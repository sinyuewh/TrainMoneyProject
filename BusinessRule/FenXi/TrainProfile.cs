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
    /// 编组的条件数据
    /// </summary>
    [Serializable]
    public class BZData
    {
        public static int[] Train25T = new int[] { 0, 3, 3, 16, 1, 17 };
        public static int[] Train25G = new int[] { 5, 13, 3, 11, 1, 17 };
        public static int[] Train25K = new int[] { 5, 13, 3, 11, 1, 17 };
        public static int[] Train25B = new int[] { 5, 13, 3, 11, 1, 17 };
    }
    


    /// <summary>
    /// 车厢编组的数据
    /// </summary>
    [Serializable]
    public class ChexianBianZhuData
    {
        private ChexianBianZhuData() { ;} 
     
        public static int YinZuo_Rate{ get;private set;}
        public static int YinZuo_Pcount { get; private set; }

        public static int RuanZuo_Rate { get; private set; }
        public static int RuanZuo_Pcount { get; private set; }

        public static int YinWo1_Rate { get; private set; }
        public static int YinWo1_Pcount { get; private set; }

        public static int YinWo2_Rate { get; private set; }
        public static int YinWo2_Pcount { get; private set; }

        public static int YinWo3_Rate { get; private set; }
        public static int YinWo3_Pcount { get; private set; }

        public static int YinWo4_Rate { get; private set; }
        public static int YinWo4_Pcount { get; private set; }

        public static int YinWo5_Rate { get; private set; }
        public static int YinWo5_Pcount { get; private set; }

        public static int RuanWo1_Rate { get; private set; }
        public static int RuanWo1_Pcount { get; private set; }

        public static int RuanWo2_Rate { get; private set; }
        public static int RuanWo2_Pcount { get; private set; }

        public static int GaoJiRuanWo1_Rate { get; private set; }
        public static int GaoJiRuanWo1_Pcount { get; private set; }

        public static int GaoJiRuanWo2_Rate { get; private set; }
        public static int GaoJiRuanWo2_Pcount { get; private set; }

        /// <summary>
        /// 静态的构造函数
        /// </summary>
        static ChexianBianZhuData()
        {
            
        }

        public static void Init()
        {
            SetData();
        }

        public static void SetData()
        {
            JTable tab1 = new JTable("CHEXIANBIANZHU");
            tab1.OrderBy = "id";
            DataTable dt = tab1.SearchData(null, -1, "*").Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                String kind1 = dr["kind"].ToString();
                switch (kind1)
                {
                    case "YinZuo":
                        YinZuo_Pcount = int.Parse(dr["Pcount"].ToString());
                        YinZuo_Rate = int.Parse(dr["Rate"].ToString());
                        break;

                    case "RuanZuo":
                        RuanZuo_Pcount = int.Parse(dr["Pcount"].ToString());
                        RuanZuo_Rate = int.Parse(dr["Rate"].ToString());
                        break;

                    case "YinWo1":
                        YinWo1_Pcount = int.Parse(dr["Pcount"].ToString());
                        YinWo1_Rate = int.Parse(dr["Rate"].ToString());
                        break;

                    case "YinWo2":
                        YinWo2_Pcount = int.Parse(dr["Pcount"].ToString());
                        YinWo2_Rate = int.Parse(dr["Rate"].ToString());
                        break;

                    case "YinWo3":
                        YinWo3_Pcount = int.Parse(dr["Pcount"].ToString());
                        YinWo3_Rate = int.Parse(dr["Rate"].ToString());
                        break;

                    case "YinWo4":
                        YinWo4_Pcount = int.Parse(dr["Pcount"].ToString());
                        YinWo4_Rate = int.Parse(dr["Rate"].ToString());
                        break;

                    case "YinWo5":
                        YinWo5_Pcount = int.Parse(dr["Pcount"].ToString());
                        YinWo5_Rate = int.Parse(dr["Rate"].ToString());
                        break;

                    case "RuanWo1":
                        RuanWo1_Pcount = int.Parse(dr["Pcount"].ToString());
                        RuanWo1_Rate = int.Parse(dr["Rate"].ToString());
                        break;

                    case "RuanWo2":
                        RuanWo2_Pcount = int.Parse(dr["Pcount"].ToString());
                        RuanWo2_Rate = int.Parse(dr["Rate"].ToString());
                        break;

                    case "GaoJiRuanWo1":
                        GaoJiRuanWo1_Pcount = int.Parse(dr["Pcount"].ToString());
                        GaoJiRuanWo1_Rate = int.Parse(dr["Rate"].ToString());
                        break;

                    case "GaoJiRuanWo2":
                        GaoJiRuanWo2_Pcount = int.Parse(dr["Pcount"].ToString());
                        GaoJiRuanWo2_Rate = int.Parse(dr["Rate"].ToString());
                        break;

                    default:
                        break;
                }
            }
            tab1.Close();
        }

    }

    /// <summary>
    /// 里程数据配置表
    /// </summary>
    [Serializable]
    public class LiChengProfile
    {
        private LiChengProfile() { ; } 
        private static DataTable data = null;
        public static DataTable Data
        {
            get
            {
                return data;
            }
            private set
            {
                data = value;
            }
        }

        static LiChengProfile()
        {
            SetData();
        }
        public static void Init()
        {
        }

        public static void SetData()
        {
            JTable tab1 = new JTable("LICHENGPROFILE");
            tab1.OrderBy = "id";
            LiChengProfile.Data = tab1.SearchData(null, -1, "*").Tables[0];
            tab1.Close();
        }
    }

    /// <summary>
    /// 里程价格递减表
    /// </summary>
    [Serializable]
    public class LiChengJianRate
    {
        private LiChengJianRate() { ;}
        private static DataTable data = null;
        public static DataTable Data
        {
            get
            {
                return data;
            }
            private set
            {
                data = value;
            }
        }

        static LiChengJianRate()
        {
            SetData();
        }

        public static void Init()
        {
        }

        public static void SetData()
        {
            JTable tab1 = new JTable("LICHENGJIANRATE");
            tab1.OrderBy = "id";
            LiChengJianRate.Data = tab1.SearchData(null, -1, "*").Tables[0];
            tab1.Close();
        }
    }

    /// <summary>
    /// 客车日常检修成本
    /// </summary>
    [Serializable]
    public class RiChangFeeProfile
    {
        private RiChangFeeProfile() { ;}
        private static DataTable data = null;
        public static DataTable Data
        {
            get
            {
                return data;
            }
            private set
            {
                data = value;
            }
        }

        static RiChangFeeProfile()
        {
            SetData();
        }

        public static void Init()
        {
        }

        public static void SetData()
        {
            JTable tab1 = new JTable("COMMONRICHANGFEE");
            tab1.OrderBy = "num";
            RiChangFeeProfile.Data = tab1.SearchData(null, -1, "*").Tables[0];
            tab1.Close();
        }
    }

    /// <summary>
    /// 加快配置表数据
    /// </summary>
    [Serializable]
    public class JiaKuaiProfile
    {
        private JiaKuaiProfile() { ;}
        public static double JkFee1 { get; private set; }
        public static double JkFee2 { get; private set; }
        public static double JkFee3 { get; private set; }

        static JiaKuaiProfile()
        {
            SetData();   
        }

        public static void Init()
        {
        }

        public static void SetData()
        {
            JTable tab1 = new JTable("JiaKuaiProfile");
            tab1.OrderBy = "id";
            DataTable dt = tab1.SearchData(null, -1, "*").Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                String kind1 = dr["JIAKUAITYPE"].ToString().Trim();
                if (kind1 == "jk1")
                {
                    JkFee1 = double.Parse(dr["Fee"].ToString());
                }
                else if (kind1 == "jk2")
                {
                    JkFee2 = double.Parse(dr["Fee"].ToString());
                }
                else if (kind1 == "jk3")
                {
                    JkFee3 = double.Parse(dr["Fee"].ToString());
                }
                else
                {
                    ;
                }
            }
            tab1.Close();
        }
    }

    /// <summary>
    /// 线路成本数据
    /// </summary>
    [Serializable]
    public class LineCostData
    {
        public double Fee1 { get; set; }
        public double Fee2 { get; set; }
        public double Fee3 { get; set; }
        public double Fee4 { get; set; }
        public double Fee5 { get; set; }
        public double Fee6 { get; set; }
    }


    /// <summary>
    /// 线路费用配置表
    /// </summary>
    [Serializable]
    public class LineProfile
    {
        private LineProfile() { ;}
        private static DataTable data = null;
        public static DataTable Data
        {
            get
            {
                return data;
            }
            private set
            {
                data = value;
            }
        }
        public static Dictionary<int, LineCostData> FeeRate = null;

        static LineProfile()
        {
            SetData();
        }
        public static void Init()
        {
        }

        public static void SetData()
        {
            JTable tab1 = new JTable("LINEPROFILE");
            tab1.OrderBy = "id";
            LineProfile.Data = tab1.SearchData(null, -1, "*").Tables[0];
            tab1.Close();

            FeeRate = new Dictionary<int, LineCostData>();
            foreach (DataRow dr1 in LineProfile.Data.Rows)
            {
                String LineType = dr1["linetype"].ToString().Trim();
                int index1 = int.Parse(LineType.ToLower().Replace("line", ""));

                LineCostData data1 = new LineCostData();
                if (dr1["Fee1"].ToString().Trim() != String.Empty)
                {
                    data1.Fee1 = double.Parse(dr1["Fee1"].ToString().Trim());
                }
                if (dr1["Fee2"].ToString().Trim() != String.Empty)
                {
                    data1.Fee2 = double.Parse(dr1["Fee2"].ToString().Trim());
                }
                if (dr1["Fee3"].ToString().Trim() != String.Empty)
                {
                    data1.Fee3 = double.Parse(dr1["Fee3"].ToString().Trim());
                }
                if (dr1["Fee4"].ToString().Trim() != String.Empty)
                {
                    data1.Fee4 = double.Parse(dr1["Fee4"].ToString().Trim());
                }
                if (dr1["Fee5"].ToString().Trim() != String.Empty)
                {
                    data1.Fee5 = double.Parse(dr1["Fee5"].ToString().Trim());
                }
                if (dr1["Fee6"].ToString().Trim() != String.Empty)
                {
                    data1.Fee6 = double.Parse(dr1["Fee6"].ToString().Trim());
                }
                FeeRate.Add(index1, data1);
            }
        }
    }

    /// <summary>
    /// 机车牵引费配置表
    /// </summary>
    [Serializable]
    public class QianYinFeeProfile
    {
        private QianYinFeeProfile() { ;}
        public static double Fee01 { get; private set; }
        public static double Fee02 { get; private set; }
        public static double Fee03 { get; private set; }

        public static double Fee11 { get; private set; }
        public static double Fee12 { get; private set; }
        public static double Fee13 { get; private set; }

        static QianYinFeeProfile()
        {
            SetData();
        }
        public static void Init()
        {
        }

        public static void SetData()
        {
            JTable tab1 = new JTable("QIANYINFEEPROFILE");
            DataTable dt1 = tab1.SearchData(null, -1, "*").Tables[0];
            foreach (DataRow dr1 in dt1.Rows)
            {
                String kind = dr1["QIANYINTYPE"].ToString().Trim();
                if (kind == "0")
                {
                    Fee01 = double.Parse(dr1["Fee1"].ToString());
                    Fee02 = double.Parse(dr1["Fee2"].ToString());
                    Fee03 = double.Parse(dr1["Fee3"].ToString());
                }
                else
                {
                    Fee11 = double.Parse(dr1["Fee1"].ToString());
                    Fee12 = double.Parse(dr1["Fee2"].ToString());
                    Fee13 = double.Parse(dr1["Fee3"].ToString());
                }
            }
            tab1.Close();
        }
    }

    //普通车的数据结构
    [Serializable]
    public class CommonTrainData
    {
        public String TRAINTYPE	    {get;set;}			
        public String YZWEIGHT	    {get;set;}			
        public String YWWEIGHT	    {get;set;}			
        public String RWWEIGHT	    {get;set;}			
        public String GRWWEIGHT	    {get;set;}			
        public String CAWEIGHT	    {get;set;}			
        public String KDWEIGHT	    {get;set;}			
        public String RZWEIGHT	    {get;set;}			
        public String SYWEIGHT	    {get;set;}			
        public String YZPRICE	    {get;set;}			
        public String YWPRICE	    {get;set;}			
        public String RWPRICE	    {get;set;}			
        public String GRWPRICE	    {get;set;}			
        public String CAPRICE	    {get;set;}			
        public String KDPRICE	    {get;set;}			
        public String RZPRICE	    {get;set;}			
        public String SYPRICE	    {get;set;}			
        public String YZCOST1	    {get;set;}			
        public String YWCOST1	    {get;set;}			
        public String RWCOST1	    {get;set;}			
        public String GRWCOST1	    {get;set;}			
        public String CACOST1	    {get;set;}			
        public String KDCOST1	    {get;set;}			
        public String RZCOST1	    {get;set;}			
        public String SYCOST1	    {get;set;}			
        public String YZCOST2	    {get;set;}			
        public String YWCOST2	    {get;set;}			
        public String RWCOST2	    {get;set;}			
        public String GRWCOST2	    {get;set;}			
        public String CACOST2	    {get;set;}			
        public String KDCOST2	    {get;set;}			
        public String RZCOST2	    {get;set;}			
        public String SYCOST2	    {get;set;}			
        public String YZCOST3	    {get;set;}			
        public String YWCOST3	    {get;set;}			
        public String RWCOST3	    {get;set;}			
        public String GRWCOST3	    {get;set;}			
        public String CACOST3	    {get;set;}			
        public String KDCOST3	    {get;set;}			
        public String RZCOST3	    {get;set;}			
        public String SYCOST3	    {get;set;}			
        public String OIL	        {get;set;}
        public String SPEED         { get; set; }
        public String A2COST        { get; set; }
        public String A3COST        { get; set; }
        public String A4COST        { get; set; }
        public String A5COST        { get; set; }

        //A4的费用价格
        public String YZA4        { get; set; }
        public String RZA4        { get; set; }
        public String SYZA4        { get; set; }
        public String SRZA4        { get; set; }
        public String YWA4        { get; set; }
        public String RWA4        { get; set; }
        public String RW19KA4        { get; set; }
        public String RW19TA4        { get; set; }
        public String CAA4        { get; set; }
        public String KDA4          { get; set; }
    }

    /// <summary>
    /// 车厢重量、日常维修成本、固定维修成本和消耗成本
    /// </summary>
    [Serializable]
    public class CheXianProfile
    {
        private CheXianProfile() { ;}

        public static Dictionary<ECommTrainType, CommonTrainData> Data =
            new Dictionary<ECommTrainType, CommonTrainData>();

    

        static CheXianProfile()
        {
            SetData();
        }

        public static void Init()
        {
        }

        public static void SetData()
        {
            JTable tab1 = new JTable("COMMTRAINWEIGHTPROFILE");
            tab1.OrderBy = "TRAINTYPE";
            DataSet ds1 = tab1.SearchData(null, -1, "*");
            Data.Clear();
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                CommonTrainData data1 = new CommonTrainData();
                JObject.FillObjectByDataRow(data1, dr1);
                String type1 = dr1["TRAINTYPE"].ToString();

                if (type1 == "25B")
                {
                    Data.Add(ECommTrainType.绿皮车25B, data1);
                }
                else if (type1 == "25G")
                {
                    Data.Add(ECommTrainType.空调车25G, data1);
                }
                else if (type1 == "25T")
                {
                    Data.Add(ECommTrainType.空调车25T, data1);
                }
                else if (type1 == "25K")
                {
                    Data.Add(ECommTrainType.空调车25K, data1);
                }
                else
                {
                }
            }
            tab1.Close();
        }
    }

    /// <summary>
    /// 计算A2和A3的费用
    /// </summary>
    [Serializable]
    public class A2A3FeeProfile
    {
        private A2A3FeeProfile() { ; } 
        private static DataTable data = null;
        public static DataTable Data
        {
            get
            {
                return data;
            }
            private set
            {
                data = value;
            }
        }

        static A2A3FeeProfile()
        {
            SetData();
        }
        public static void Init()
        {
        }
        public static void SetData()
        {
            JTable tab1 = new JTable("A2A3Fee");
            tab1.OrderBy = "num";
            A2A3FeeProfile.Data = tab1.SearchData(null, -1, "*").Tables[0];
            tab1.Close();
        }
    }

    //人员工资、附加费和其他费
    [Serializable]
    public class PersonGZProfile
    {
        private PersonGZProfile() { ; } 
        private static DataTable data = null;
        public static DataTable Data
        {
            get
            {
                return data;
            }
            private set
            {
                data = value;
            }
        }

        static PersonGZProfile()
        {
            SetData();
        }
        public static void Init()
        {
        }
        public static void SetData()
        {
            JTable tab1 = new JTable("PERSONGZ");
            tab1.OrderBy = "GW";
            PersonGZProfile.Data = tab1.SearchData(null, -1, "*").Tables[0];
            tab1.Close();
        }
    }

    /// <summary>
    /// 动车配置基本表
    /// </summary>
    [Serializable]
    public class HighTrainProfile
    {
        private HighTrainProfile() { ;}
        private static DataTable data = null;
        public static DataTable Data
        {
            get
            {
                return data;
            }
            private set
            {
                data = value;
            }
        }
        static HighTrainProfile()
        {
            SetData();
        }
        public static void Init()
        {
        }

        public static void SetData()
        {
            JTable tab1 = new JTable("HIGHTRAINPROFILE");
            tab1.OrderBy = "id";
            HighTrainProfile.Data = tab1.SearchData(null, -1, "*").Tables[0];
            tab1.Close();
        }
    }

    /// <summary>
    /// 动车线路电费和接触网使用费用
    /// </summary>
    [Serializable]
    public class TrainLineKindProfile
    {
        private TrainLineKindProfile() { ;}
        public static double JieChuFee0 { get; private set; }
        public static double JieChuFee1 { get; private set; }
        public static double JieChuFee2 { get; private set; }

        public static double DianFee0 { get; private set; }
        public static double DianFee1 { get; private set; }
        public static double DianFee2 { get; private set; }

       
        static TrainLineKindProfile()
        {
            SetData();
        }
        public static void Init()
        {
        }

        public static void SetData()
        {
            JTable tab1 = new JTable("TRAINLINEKINDPROFILE");
            tab1.OrderBy = "lineid";
            DataTable dt1 = tab1.SearchData(null, -1, "*").Tables[0];
            if (dt1 != null)
            {
                foreach (DataRow dr1 in dt1.Rows)
                {
                    String kind1 = dr1["lineID"].ToString();
                    if (kind1 == "0")
                    {
                        JieChuFee0 = double.Parse(dr1["JIECHUFEE"].ToString());
                        DianFee0 = double.Parse(dr1["DIANFEE"].ToString());
                    }
                    else if (kind1 == "1")
                    {
                        JieChuFee1 = double.Parse(dr1["JIECHUFEE"].ToString());
                        DianFee1 = double.Parse(dr1["DIANFEE"].ToString());
                    }
                    else if (kind1 == "2")
                    {
                        JieChuFee2 = double.Parse(dr1["JIECHUFEE"].ToString());
                        DianFee2 = double.Parse(dr1["DIANFEE"].ToString());
                    }
                }
            }
            tab1.Close();
        }

    }

    /// <summary>
    /// 列车的基本配置表
    /// </summary>
    [Serializable]
    public class TrainProfile
    {
        private TrainProfile() { ;}
        public static double BaseFee { get; private set; }                  // 得到硬座的基本费率
        public static double BaoXianFee{get;private set;}                   //保险费率
        public static double KongTiaoFeeRate { get; private set; }          //得到空调费率
        public static double WoPuDingPiaoFee { get; private set; }          //得到卧铺订票费

        public static double SaleRateForComm { get; private set; }          //普通车的售票服务比例
        public static double SaleRateForHigh { get; private set; }          //动车的售票服务比例

        public static int ServiceRateForComm { get; private set; }          
        public static int ServiceRateForHigh { get; private set; }

        public static double WaterRateForComm { get; private set; }
        public static double WaterRateForHigh { get; private set; }

        public static double PersonCost { get; private set; }
        public static int PersonAddRate { get; private set; }

        public static double PersonOtherCost { get; private set; }
        public static double YongYouBiaoZhun { get; private set; }
        public static double OilUnitCost { get; private set; }
        public static double FixLiXi { get; private set; }
        /*---------------------------------------------------------*/
        public static double HighTrainWeight200 { get; private set; }
        public static double HighTrainWeight300 { get; private set; }

        public static double HighTrainCost200 { get; private set; }
        public static double HighTrainCost300 { get; private set; }

        public static double HighTrainFixCost200 { get; private set; }
        public static double HighTrainFixCost300 { get; private set; }

        public static double HighTrainXhCost200 { get; private set; }
        public static double HighTrainXhCost300 { get; private set; }

        public static double HighTrainLiXiCost200 { get; private set; }
        public static double HighTrainLiXiCost300 { get; private set; }

        public static int HighTrainServerCount1 { get; private set; }
        public static int HighTrainServerCount2 { get; private set; }
        public static int HighTrainServerCount3 { get; private set; }

        public static double TrainZheJiuRate { get; private set; }
        public static double HighZheJiuRate { get; private set; }

        /*---------------------------------------------------------------*/
        public static int ShipFee1 { get; private set; }
        public static int ShipFee2 { get; private set; }
        public static int QianYinFjFee1 { get; private set; }
        public static int QianYinFjFee2 { get; private set; }

        /*---------------------------------------------------------------*/

        public static double JianBeiLv1 { get; private set; }
        public static double JianBeiLv2 { get; private set; }

        public static double JianJieFee { get; private set; }
        public static double JianXiuFeeRate { get; private set; }

        //宿营车厢的人员配置
        public static int SyCheXianPCount { get; private set; }

        static TrainProfile()
        {
            SetData();
        }

        public static void Init()
        {
        }

        public static void SetData()
        {
            
            BaseFee = double.Parse(JStrInfoBU.GetStrTextByID("基本硬座费率"));
            BaoXianFee = double.Parse(JStrInfoBU.GetStrTextByID("保险费率"));
            KongTiaoFeeRate = double.Parse(JStrInfoBU.GetStrTextByID("空调费率"));

            SaleRateForComm = double.Parse(JStrInfoBU.GetStrTextByID("普通列车售票服务费比例"));
            SaleRateForHigh = double.Parse(JStrInfoBU.GetStrTextByID("动车售票服务费比例"));

            ServiceRateForComm = int.Parse(JStrInfoBU.GetStrTextByID("普通列车旅客服务费标准"));
            ServiceRateForHigh = int.Parse(JStrInfoBU.GetStrTextByID("动车旅客服务费标准"));

            WaterRateForComm = double.Parse(JStrInfoBU.GetStrTextByID("普通列车上水站费用标准"));
            WaterRateForHigh = double.Parse(JStrInfoBU.GetStrTextByID("动车上水站费用标准"));

            PersonCost = double.Parse(JStrInfoBU.GetStrTextByID("普通列车人年工资成本标准"));
            PersonAddRate = int.Parse(JStrInfoBU.GetStrTextByID("普通列车人年工资附加费标准"));

            PersonOtherCost = double.Parse(JStrInfoBU.GetStrTextByID("普通列车人年其他费用标准"));
            YongYouBiaoZhun = double.Parse(JStrInfoBU.GetStrTextByID("普通列车用油的定额标准"));
            OilUnitCost = double.Parse(JStrInfoBU.GetStrTextByID("油价标准"));
            FixLiXi = double.Parse(JStrInfoBU.GetStrTextByID("固定利率"));

            HighTrainWeight200 = double.Parse(JStrInfoBU.GetStrTextByID("200公里动车车厢重量"));
            HighTrainWeight300 = double.Parse(JStrInfoBU.GetStrTextByID("300公里动车车厢重量"));

            HighTrainCost200 = double.Parse(JStrInfoBU.GetStrTextByID("200公里动车日常检修成本"));
            HighTrainCost300 = double.Parse(JStrInfoBU.GetStrTextByID("300公里动车日常检修成本"));

            HighTrainFixCost200 = double.Parse(JStrInfoBU.GetStrTextByID("200公里车辆定期检修成本"));
            HighTrainFixCost300 = double.Parse(JStrInfoBU.GetStrTextByID("300公里车辆定期检修成本"));

            HighTrainXhCost200 = double.Parse(JStrInfoBU.GetStrTextByID("200公里动车客运消耗备用备品成本"));
            HighTrainXhCost300 = double.Parse(JStrInfoBU.GetStrTextByID("300公里动车客运消耗备用备品成本"));

            HighTrainLiXiCost200 = double.Parse(JStrInfoBU.GetStrTextByID("200公里动车购买成本标准"));
            HighTrainLiXiCost300 = double.Parse(JStrInfoBU.GetStrTextByID("300公里动车购买成本标准"));

            HighTrainServerCount1 = int.Parse(JStrInfoBU.GetStrTextByID("8节动座服务人员数"));
            HighTrainServerCount2 = int.Parse(JStrInfoBU.GetStrTextByID("16节动座服务人员数"));
            HighTrainServerCount3 = int.Parse(JStrInfoBU.GetStrTextByID("16节动卧服务人员"));

            WoPuDingPiaoFee = double.Parse(JStrInfoBU.GetStrTextByID("卧铺订票费"));        //卧铺订票费

            TrainZheJiuRate = double.Parse(JStrInfoBU.GetStrTextByID("普列折旧费率"));
            HighZheJiuRate = double.Parse(JStrInfoBU.GetStrTextByID("动列折旧费率"));

            JianBeiLv1 = double.Parse(JStrInfoBU.GetStrTextByID("列车检备率"));
            JianBeiLv2 = double.Parse(JStrInfoBU.GetStrTextByID("动车检备率"));


            //宿营车厢的定员配置
            if (JStrInfoBU.GetStrTextByID("宿营车定员").Trim() != String.Empty)
            {
                SyCheXianPCount = int.Parse(JStrInfoBU.GetStrTextByID("宿营车定员"));
            }
            else
            {
                SyCheXianPCount = 42;
            }

            //轮渡费单价表配置
            if (JStrInfoBU.GetStrTextByID("空调客车轮渡费").Trim() != String.Empty)
            {
                ShipFee1 = int.Parse(JStrInfoBU.GetStrTextByID("空调客车轮渡费").Trim());
            }
            else
            {
                ShipFee1 = 100;
            }

            if (JStrInfoBU.GetStrTextByID("非空调客车轮渡费").Trim() != String.Empty)
            {
                ShipFee2 = int.Parse(JStrInfoBU.GetStrTextByID("非空调客车轮渡费").Trim());
            }
            else
            {
                ShipFee2 = 70;
            }

            //直供电牵引付加费
            if (JStrInfoBU.GetStrTextByID("直供电内燃牵引附加费").Trim() != String.Empty)
            {
                QianYinFjFee1 = int.Parse(JStrInfoBU.GetStrTextByID("直供电内燃牵引附加费").Trim());
            }
            else
            {
                QianYinFjFee1 = 50;
            }

            if (JStrInfoBU.GetStrTextByID("直供电电力牵引附加费").Trim() != String.Empty)
            {
                QianYinFjFee2 = int.Parse(JStrInfoBU.GetStrTextByID("直供电电力牵引附加费").Trim());
            }
            else
            {
                QianYinFjFee2 = 25;
            }


            if (JStrInfoBU.GetStrTextByID("间接费用分摊").Trim() != String.Empty)
            {
               JianJieFee  = double.Parse(JStrInfoBU.GetStrTextByID("间接费用分摊").Trim());
            }
            else
            {
                JianJieFee =0.2822;
            }


            if (JStrInfoBU.GetStrTextByID("检修费率系数").Trim() != String.Empty)
            {
                JianXiuFeeRate = double.Parse(JStrInfoBU.GetStrTextByID("检修费率系数").Trim());
            }
            else
            {
                JianJieFee = 1.0;
            }
            /*----------------------------------------------------------*/

        }
    }
}
