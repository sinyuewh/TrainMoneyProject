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
    #region 枚举和数据结构定义
    /// <summary>
    /// 车厢类型的枚举
    /// </summary>
    public enum ECommCheXian
    {
        硬座,软座,
        开放式硬卧, 包房式硬卧,
        软卧,高级软卧,
        餐车,供电车,宿营车
    }

    public enum ECommJiaKuai
    {
        加快,特快,特快附加,其他
    }
    

    
   

    public enum ETrainLine
    {
        普通线路,高速200公里,高速300公里
    }

    /// <summary>
    /// 描述车厢类型的票价率和人数
    /// 1--上铺
    /// 2--中铺
    /// 3--下铺
    /// </summary>
    internal class CommChexianRateAndPersonCount
    {
        public int Rate1 { get; set; }
        public int Rate2 { get; set; }
        public int Rate3 { get; set; }
        public int PersonCount1 { get; set; }
        public int PersonCount2 { get; set; }
        public int PersonCount3 { get; set; }

        public void SetRate(int i,int value1)
        {
            if (i == 0)
            {
                Rate1 = value1;
                
            }
            else if (i == 1)
            {
                Rate2 = value1;
            }
            else if (i == 2)
            {
                Rate3 = value1;
            }
        }

        public void SetPerson(int i, int value1)
        {
            if (i == 0)
            {
                PersonCount1 = value1;
            }
            else if (i == 1)
            {
                PersonCount2 = value1;
            }
            else if (i == 2)
            {
                PersonCount3 = value1;
            }
        }
    }
    #endregion

    /// <summary>
    /// 普通列车类描述
    /// </summary>
    public partial class CommTrainBU
    {
        public CommTrainBU() { ;}

        #region 属性定义
        //列车名称
        private String Num { get; set; }
        public String TrainName { get; set; }
        public String TrainType { get; set; }
        public ETrainBigKind TrainBigKind { get; set; }   //列车的大类（是普通列车还是动车）

        //运行里程
        public int YuXingLiCheng { get; set; }

        //=====车厢编组
        public int YinZuo { get; set; }                 //硬座数量
        public int RuanZuo { get; set; }                //软座数量
        public int OpenYinWo { get; set; }              //开放式硬卧数量
        public int CloseYinWo { get; set; }             //包方式硬卧数量
        public int RuanWo { get; set; }                 //软卧数量
        public int AdvanceRuanWo { get; set; }          //高级软卧数量

        public int CanChe { get; set; }                 //餐车数量
        public int FaDianChe { get; set; }              //发电厂数量
        public int ShuYinChe { get; set; }              //宿营车数量

        //=======车厢编组单价
        public double YinZuoPrice { get; set; }
        public double RuanZuoPrice { get; set; }
        public double OpenYinWoPrice { get; set; }
        public double CloseYinWoPrice { get; set; }
        public double RuanWoPrice { get; set; }
        public double AdvanceRuanWoPrice { get; set; }

        public double CanChePrice { get; set; }
        public double FaDianChePrice { get; set; }
        public double ShuYinChePrice { get; set; }


        //列车加快
        public ECommJiaKuai JiaKuai { get; set; }

        //空调费用
        public bool KongTiaoFee { get; set; }

        //席别增减费
        public int XieBieZhengJiaFee { get; set; }

        //牵引类型
        public EQianYinType QianYinType { get; set; }

        //供电类型
        public EGongDianType GongDianType { get; set; }


        //上水站数量和标准
        public int WaterCount { get; set; }


        //人员工资和工资附加费
        public EServerPerson ServerPerson { get; set; }         //服务方式
        public int RunHour { get; set; }                        //运行时间


        //用车底数
        public double YongCheDiShu { get; set; }


        public double CheDiShu { get; set; }

        //线路描述
        public double Line0 { get; set; }      //局内
        public double Line1 { get; set; }      //特一类
        public double Line2 { get; set; }      //特二类
        public double Line3 { get; set; }      //一类上浮
        public double Line4 { get; set; }      //一类
        public double Line5 { get; set; }      //二类上浮
        public double Line6 { get; set; }      //二类
        public double Line7 { get; set; }      //三类
        public double Line8 { get; set; }      //三类下浮

        //动车组的基本配置
        //车厢编组
        public EHighTrainBianZhu HighTrainBianZhu { get; set; }
        public EHighTrainBigKind HighTrainBigKind { get; set; }

        public ECunZengMoShi CunZengMoShi { get; set; }
        public ETrainLine TrainLine { get; set; }
        #endregion

        #region 系统配置表中的值
        /// <summary>
        /// 得到硬座的基本费率
        /// </summary>
        /// <returns></returns>
        private double BaseFee
        {
            get { return double.Parse(JStrInfoBU.GetStrTextByID("基本硬座费率")); }
        }

        /// <summary>
        /// 得到空调费率
        /// </summary>
        /// <returns></returns>
        public double KongTiaoFeeRate
        {
            get
            {
                if (this.KongTiaoFee)
                {
                    return 1 + double.Parse(JStrInfoBU.GetStrTextByID("空调费率"));
                }
                else
                {
                    return 1;
                }
            }
        }

        //保险费率
        private double BaoXianFee
        {
            get
            {
                return double.Parse(JStrInfoBU.GetStrTextByID("保险费率"));
            }
        }

        //售票服务费的标准
        private double SaleRate
        {
            get
            {
                if (this.TrainBigKind == ETrainBigKind.普通列车)
                {
                    return double.Parse(JStrInfoBU.GetStrTextByID("普通列车售票服务费比例"));
                }
                else
                {
                    return double.Parse(JStrInfoBU.GetStrTextByID("动车售票服务费比例"));
                }
            }
        }

        //旅客服务标准
        private int ServiceRate
        {
            get
            {
                if (this.TrainBigKind == ETrainBigKind.普通列车)
                {
                    return int.Parse(JStrInfoBU.GetStrTextByID("普通列车旅客服务费标准"));
                }
                else
                {
                    return int.Parse(JStrInfoBU.GetStrTextByID("动车旅客服务费标准"));
                }
            }
        }

        private double WaterRate
        {
            get
            {
                if (this.TrainBigKind == ETrainBigKind.普通列车)
                {
                    return double.Parse(JStrInfoBU.GetStrTextByID("普通列车上水站费用标准"));
                }
                else
                {
                    return double.Parse(JStrInfoBU.GetStrTextByID("动车上水站费用标准"));
                }
            }
        }

        private double PersonCost
        {
            get
            {
                return double.Parse(JStrInfoBU.GetStrTextByID("普通列车人年工资成本标准"));
            }
        }

        private int PersonAddRate
        {
            get
            {
                return int.Parse(JStrInfoBU.GetStrTextByID("普通列车人年工资附加费标准"));
            }
        }

        private double PersonOtherCost
        {
            get
            {
                return double.Parse(JStrInfoBU.GetStrTextByID("普通列车人年其他费用标准"));
            }
        }

        //车辆用油成本
        private double YongYouBiaoZhun
        {
            get
            {
                return double.Parse(JStrInfoBU.GetStrTextByID("普通列车用油的定额标准"));
            }
        }

        private double OilUnitCost
        {
            get
            {
                return double.Parse(JStrInfoBU.GetStrTextByID("油价标准"));
            }
        }

        //购买车辆利息
        private double FixLiXi
        {
            get
            {
                return double.Parse(JStrInfoBU.GetStrTextByID("固定利率"));
            }
        }
        #endregion

        #region 其他的方法
        /// <summary>
        /// 得到高铁的类型
        /// </summary>
        /// <returns></returns>
        public static String[] GetHightTrainType()
        {
            String[] result = null;
            JTable tab1 = new JTable("HighTrainProfile");
            tab1.OrderBy = "ID";
            DataSet ds1 = tab1.SearchData(null, -1, "HighTrainType");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                result = new String[ds1.Tables[0].Rows.Count];
                int i = 0;
                foreach (DataRow dr1 in ds1.Tables[0].Rows)
                {
                    result[i] = dr1[0].ToString().Trim();
                    i++;
                }
            }
            tab1.Close();
            return result;
        }

        /// <summary>
        /// 根据列车名称得到列车对象
        /// </summary>
        /// <param name="TrainName"></param>
        /// <returns></returns>
        public static CommTrainBU GetTrainObjectByTrainName(String TrainName)
        {
            CommTrainBU obj1 = null;
            if (String.IsNullOrEmpty(TrainName) == false)
            {
                JTable tab1 = new JTable("Train");
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("TrainName", TrainName));
                DataRow dr1 = tab1.GetFirstDataRow(condition, "*");
                if (dr1 != null)
                {
                    obj1 = new CommTrainBU();
                    obj1.Num = dr1["num"].ToString();
                    obj1.TrainName = dr1["TrainName"].ToString();
                    obj1.TrainBigKind = (ETrainBigKind)(int.Parse(dr1["TrainBigKind"].ToString()));
                    obj1.TrainType = dr1["TrainType"].ToString();
                    obj1.YuXingLiCheng = int.Parse(dr1["YuXingLiCheng"].ToString());

                    obj1.YinZuo = int.Parse(dr1["YinZuo"].ToString());
                    obj1.RuanZuo = int.Parse(dr1["RuanZuo"].ToString());
                    obj1.OpenYinWo = int.Parse(dr1["OpenYinWo"].ToString());
                    obj1.CloseYinWo = int.Parse(dr1["CloseYinWo"].ToString());
                    obj1.RuanWo = int.Parse(dr1["RuanWo"].ToString());
                    obj1.AdvanceRuanWo = int.Parse(dr1["AdvanceRuanWo"].ToString());
                    obj1.CanChe = int.Parse(dr1["CanChe"].ToString());
                    obj1.FaDianChe = int.Parse(dr1["FaDianChe"].ToString());
                    obj1.ShuYinChe = int.Parse(dr1["ShuYinChe"].ToString());

                    //车厢价格
                    obj1.YinZuoPrice = int.Parse(dr1["YinZuoPrice"].ToString());
                    obj1.RuanZuoPrice = int.Parse(dr1["RuanZuoPrice"].ToString());
                    obj1.OpenYinWoPrice = int.Parse(dr1["OpenYinWoPrice"].ToString());
                    obj1.CloseYinWoPrice = int.Parse(dr1["CloseYinWoPrice"].ToString());
                    obj1.RuanWoPrice = int.Parse(dr1["RuanWoPrice"].ToString());
                    obj1.AdvanceRuanWoPrice = int.Parse(dr1["AdvanceRuanWoPrice"].ToString());
                    obj1.CanChePrice = int.Parse(dr1["CanChePrice"].ToString());
                    obj1.FaDianChePrice = int.Parse(dr1["FaDianChePrice"].ToString());
                    obj1.ShuYinChePrice = int.Parse(dr1["ShuYinChePrice"].ToString());

                    obj1.JiaKuai=(ECommJiaKuai)(int.Parse(dr1["JiaKuai"].ToString()));
                    if (dr1["KongTiaoFee"].ToString().Trim() == "1")
                    {
                        obj1.KongTiaoFee = true;
                    }
                    else
                    {
                        obj1.KongTiaoFee = false;
                    }

                    obj1.XieBieZhengJiaFee = int.Parse(dr1["XieBieZhengJiaFee"].ToString());
                    obj1.QianYinType = (EQianYinType)(int.Parse(dr1["QianYinType"].ToString()));
                    obj1.GongDianType = (EGongDianType)(int.Parse(dr1["GongDianType"].ToString()));
                    obj1.WaterCount = int.Parse(dr1["WaterCount"].ToString());
                    obj1.ServerPerson = (EServerPerson)(int.Parse(dr1["ServerPerson"].ToString()));
                    obj1.RunHour = int.Parse(dr1["RunHour"].ToString());

                    obj1.YongCheDiShu = double.Parse(dr1["YongCheDiShu"].ToString());
                    obj1.CheDiShu = double.Parse(dr1["CheDiShu"].ToString());

                    obj1.Line0 = int.Parse(dr1["Line0"].ToString());
                    obj1.Line1 = int.Parse(dr1["Line1"].ToString());
                    obj1.Line2 = int.Parse(dr1["Line2"].ToString());
                    obj1.Line3 = int.Parse(dr1["Line3"].ToString());
                    obj1.Line4 = int.Parse(dr1["Line4"].ToString());
                    obj1.Line5 = int.Parse(dr1["Line5"].ToString());
                    obj1.Line6 = int.Parse(dr1["Line6"].ToString());
                    obj1.Line7 = int.Parse(dr1["Line7"].ToString());
                    obj1.Line8 = int.Parse(dr1["Line8"].ToString());

                    obj1.HighTrainBianZhu = (EHighTrainBianZhu)(int.Parse(dr1["HighTrainBianZhu"].ToString()));
                    obj1.HighTrainBigKind = (EHighTrainBigKind)(int.Parse(dr1["HighTrainBigKind"].ToString()));
                    obj1.CunZengMoShi = (ECunZengMoShi)(int.Parse(dr1["CunZengMoShi"].ToString()));
                }
                tab1.Close();
            }
            return obj1;
        }
        #endregion
    }
}
