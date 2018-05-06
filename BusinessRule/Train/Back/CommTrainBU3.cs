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
    /// 普通列车类描述
    /// </summary>
    public partial class CommTrainBU
    {
        /// <summary>
        /// 得到总支出
        /// </summary>
        /// <returns></returns>
        public double GetTotalZhiChu()
        {
            return GetLineCost() + GetQianYinCostOrDianFei() + GetSaleCost() + GetServiceCost()
                  + GetWaterCost() + GetPersonCost() + GetOftenFixCost() + GetDingQiFixCost() 
                  + GetXiaoHaoCost()+ GetOilCost() + GetPersonOtherCost() + GetLiXiCost();
        }

        #region CB1:线路使用费
        /// <summary>
        /// 计算线路使用费
        /// </summary>
        /// <returns></returns>
        public double GetLineCost()
        {
            double cost1 = 0;
            JTable tab1 = new JTable("LINEPROFILE");
            tab1.OrderBy = "id";
            String fs = "Fee5,linetype";                          //默认的配置
            if (this.TrainBigKind == ETrainBigKind.普通列车)
            {
                if (this.KongTiaoFee == false)
                {
                    fs = "Fee6,linetype";
                }
            }
            else
            {
                if (this.HighTrainBianZhu == EHighTrainBianZhu.单组)
                {
                    if (this.HighTrainBigKind == EHighTrainBigKind.动车200公里)
                    {
                        fs = "Fee3,linetype";
                    }
                    else
                    {
                        fs = "Fee1,linetype";
                    }
                }
                else
                {
                    if (this.HighTrainBigKind == EHighTrainBigKind.动车200公里)
                    {
                        fs = "Fee4,linetype";
                    }
                    else
                    {
                        fs = "Fee2,linetype";
                    }
                }
            }
            DataSet ds1 = tab1.SearchData(null, -1, fs);
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                String LineType = dr1["linetype"].ToString().Trim();
                int index1 = int.Parse(LineType.ToLower().Replace("line", ""));
                double fee = 0;
                if (dr1[0].ToString().Trim() != String.Empty)
                {
                    fee = double.Parse(dr1[0].ToString().Trim());
                }
                cost1 = cost1 + fee * this.GetLineMile(index1);
            }
            tab1.Close();
            return cost1 * 2 * 365;
        }

        /// <summary>
        /// 得到线路的长度
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private double GetLineMile(int i)
        {
            if (i == 0)
            {
                return Line0;
            }
            else if (i == 1)
            {
                return Line1;
            }
            else if (i == 2)
            {
                return Line2;
            }
            else if (i == 3)
            {
                return Line3;
            }
            else if (i == 4)
            {
                return Line4;
            }
            else if (i == 5)
            {
                return Line5;
            }
            else if (i == 6)
            {
                return Line6;
            }
            else if (i == 7)
            {
                return Line7;
            }
            else if (i == 8)
            {
                return Line8;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 设置线路分级里程
        /// </summary>
        /// <param name="i"></param>
        /// <param name="miles"></param>
        public void SetLineMile(int i, double miles)
        {
            if (i == 0)
            {
                Line0 = miles;
            }
            else if (i == 1)
            {
                Line1 = miles;
            }
            else if (i == 2)
            {
                Line2 = miles;
            }
            else if (i == 3)
            {
                Line3 = miles;
            }
            else if (i == 4)
            {
                Line4 = miles;
            }
            else if (i == 5)
            {
                Line5 = miles;
            }
            else if (i == 6)
            {
                Line6 = miles;
            }
            else if (i == 7)
            {
                Line7 = miles;
            }
            else if (i == 8)
            {
                Line8 = miles;
            }
            else
            {
                ;
            }
        }
        #endregion

        #region CB2:机车牵引费或电费
        /// <summary>
        /// 得到牵引费
        /// 计算公式：合适的标准*列车的重量*运用的里程*2*365
        /// </summary>
        /// <returns></returns>
        public double GetQianYinCostOrDianFei()
        {
            double Fee = 0;
            if (this.TrainBigKind == ETrainBigKind.普通列车)
            {
                JTable tab1 = new JTable("QIANYINFEEPROFILE");
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("QIANYINTYPE", (int)this.QianYinType + "", SearchFieldType.NumericType));
                DataRow dr1 = tab1.GetFirstDataRow(condition, "Fee1", "Fee2");
                if (dr1 != null)
                {
                    if (this.GongDianType == EGongDianType.直供电)
                    {
                        Fee = double.Parse(dr1["Fee1"].ToString());
                    }
                    else
                    {
                        Fee = double.Parse(dr1["Fee2"].ToString());
                    }
                }
                tab1.Close();
                Fee = Fee * this.GetTrainWeight() * this.YuXingLiCheng * 2 * 365; //合适的标准*列车的重量*运用的里程*2*365
            }
            else
            {
                int lineid = (int)this.TrainLine;
                JTable tab1 = new JTable("TRAINLINEKINDPROFILE");
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("lineid", lineid + "", SearchFieldType.NumericType));
                DataRow dr1 = tab1.GetFirstDataRow(condition, "*");
                if (dr1 != null)
                {
                    Fee = Fee + (double.Parse(dr1["JIECHUFEE"].ToString()) + double.Parse(dr1["DIANFEE"].ToString()))
                        * this.YuXingLiCheng * this.GetTrainWeight();
                }
                tab1.Close();
            }
            return Fee;   
        }
        #endregion

        #region CB3:售票服务费
        /// <summary>
        /// 得到售票服务费
        /// </summary>
        /// <returns></returns>
        public double GetSaleCost()
        {
            double fee = 0;
            fee = this.GetTotalShouru() * this.SaleRate / 100d;
            return fee;
        }
        #endregion

        #region CB4:旅客服务费
        /// <summary>
        /// 得到旅客服务费
        /// </summary>
        /// <returns></returns>
        public double GetServiceCost()
        {
            double fee = 0;
            fee = this.GetTotalPerson() * this.ServiceRate;
            return fee;
        }
        #endregion

        #region CB5:列车上水费
        /// <summary>
        /// 得到列车上水费
        /// </summary>
        /// <returns></returns>
        public double GetWaterCost()
        {
            double fee = 0;
            fee = this.WaterCount * this.WaterRate * 2 * 365;
            if (this.TrainBigKind == ETrainBigKind.动车
                && this.HighTrainBianZhu == EHighTrainBianZhu.重联)
            {
                fee = fee * 2;
            }
            return fee;
        }
        #endregion

        #region CB6:人员工资和工资附加费
        /// <summary>
        /// 人员工资和工资附加费
        /// </summary>
        /// <returns></returns>
        public double GetPersonCost()
        {
            double fee = 0;
            int personcount = this.GetTotalServicePersonCount();                                        //工作人员的总数
            double bc = Math.Ceiling(this.RunHour * 365 / 2000d) * 2;                                  //计算工作班次
            fee = personcount * bc * this.PersonCost * (1 + this.PersonAddRate / 100d);                 //R*B*人员的年成本（输入）*（1+附加系数）
            return fee;
        }

        /// <summary>
        /// 得到总的服务人员
        /// </summary>
        /// <returns></returns>
        private int GetTotalServicePersonCount()
        {
            int result = 18;
            if (this.TrainBigKind == ETrainBigKind.普通列车)
            {
                if (this.ServerPerson == EServerPerson.一人1车)
                {
                    result = (result - this.CanChe - this.FaDianChe) + 1 + 2 + 2 * this.FaDianChe;
                }
                else if (this.ServerPerson == EServerPerson.一人2车)
                {
                    double t1 = (result - this.CanChe - this.FaDianChe) / 2.0;
                    result = (int)(Math.Ceiling(t1)) + 1 + 2 + 2 * this.FaDianChe;
                }
                else
                {
                }
            }
            else
            {
                //动车，8节动座6人，16节动座10人，16节动卧13人
                result = int.Parse(JStrInfoBU.GetStrTextByID("8节动座服务人员数"));
                if (this.HighTrainBianZhu == EHighTrainBianZhu.重联)
                {
                    result = int.Parse(JStrInfoBU.GetStrTextByID("16节动座服务人员数"));
                }
                if (this.TrainType == "CRH2E")
                {
                    result = result = int.Parse(JStrInfoBU.GetStrTextByID("16节动卧服务人员")); 
                }
            }
            return result;
        }
        #endregion

        #region CB7:车辆日常检修成本
        /// <summary>
        /// 车辆日常检修成本
        /// </summary>
        /// <returns></returns>
        public double GetOftenFixCost()
        {
            double fee = 0;
            if (this.TrainBigKind == ETrainBigKind.普通列车)
            {
                JTable tab1 = new JTable("CHEXIANWEIGHTPROFILE");
                List<SearchField> condition = new List<SearchField>();
                tab1.OrderBy = "CHEXIANTYPE";
                DataSet ds1 = tab1.SearchData(null, -1, "*");
                foreach (DataRow dr1 in ds1.Tables[0].Rows)
                {
                    ECommCheXian type1 = (ECommCheXian)(int.Parse(dr1["CHEXIANTYPE"].ToString()));
                    switch (type1)
                    {
                        case ECommCheXian.硬座:
                            fee = fee + this.YinZuo * double.Parse(dr1["UNITCOST"].ToString());
                            break;
                        case ECommCheXian.软座:
                            fee = fee + this.RuanZuo * double.Parse(dr1["UNITCOST"].ToString());
                            break;
                        case ECommCheXian.开放式硬卧:
                            fee = fee + this.OpenYinWo * double.Parse(dr1["UNITCOST"].ToString());
                            break;
                        case ECommCheXian.包房式硬卧:
                            fee = fee + this.CloseYinWo * double.Parse(dr1["UNITCOST"].ToString());
                            break;
                        case ECommCheXian.软卧:
                            fee = fee + this.RuanWo * double.Parse(dr1["UNITCOST"].ToString());
                            break;
                        case ECommCheXian.高级软卧:
                            fee = fee + this.AdvanceRuanWo * double.Parse(dr1["UNITCOST"].ToString());
                            break;
                        case ECommCheXian.餐车:
                            fee = fee + this.CanChe * double.Parse(dr1["UNITCOST"].ToString());
                            break;
                        case ECommCheXian.供电车:
                            fee = fee + this.FaDianChe * double.Parse(dr1["UNITCOST"].ToString());
                            break;
                        case ECommCheXian.宿营车:
                            fee = fee + this.ShuYinChe * double.Parse(dr1["UNITCOST"].ToString());
                            break;
                    }
                }
                tab1.Close();
                fee = fee * this.YongCheDiShu;          //根据（硬座、软座、硬卧、软卧等）的车厢数*单位维护成本*车底组数计算
            }
            else
            {
                //动车
                double db = 0;
                if (this.HighTrainBigKind == EHighTrainBigKind.动车200公里)
                {
                    db = double.Parse(JStrInfoBU.GetStrTextByID("200公里动车日常检修成本"));
                }
                else
                {
                    db = double.Parse(JStrInfoBU.GetStrTextByID("300公里动车日常检修成本"));
                }
                fee = db * this.CheDiShu * 8;
                if (this.HighTrainBianZhu == EHighTrainBianZhu.重联)
                {
                    fee = fee * 2;
                }
            }
            return fee;
        }
        #endregion

        #region CB8:车辆定期检修成本
        /// <summary>
        /// 车辆定期检修成本
        /// </summary>
        /// <returns></returns>
        public double GetDingQiFixCost()
        {
            double fee = 0;
            if (this.TrainBigKind == ETrainBigKind.普通列车)
            {
                JTable tab1 = new JTable("CHEXIANWEIGHTPROFILE");
                List<SearchField> condition = new List<SearchField>();
                tab1.OrderBy = "CHEXIANTYPE";
                DataSet ds1 = tab1.SearchData(null, -1, "*");
                foreach (DataRow dr1 in ds1.Tables[0].Rows)
                {
                    ECommCheXian type1 = (ECommCheXian)(int.Parse(dr1["CHEXIANTYPE"].ToString()));
                    switch (type1)
                    {
                        case ECommCheXian.硬座:
                            fee = fee + this.YinZuo * double.Parse(dr1["UNITFixCOST"].ToString());
                            break;
                        case ECommCheXian.软座:
                            fee = fee + this.RuanZuo * double.Parse(dr1["UNITFixCOST"].ToString());
                            break;
                        case ECommCheXian.开放式硬卧:
                            fee = fee + this.OpenYinWo * double.Parse(dr1["UNITFixCOST"].ToString());
                            break;
                        case ECommCheXian.包房式硬卧:
                            fee = fee + this.CloseYinWo * double.Parse(dr1["UNITFixCOST"].ToString());
                            break;
                        case ECommCheXian.软卧:
                            fee = fee + this.RuanWo * double.Parse(dr1["UNITFixCOST"].ToString());
                            break;
                        case ECommCheXian.高级软卧:
                            fee = fee + this.AdvanceRuanWo * double.Parse(dr1["UNITFixCOST"].ToString());
                            break;
                        case ECommCheXian.餐车:
                            fee = fee + this.CanChe * double.Parse(dr1["UNITFixCOST"].ToString());
                            break;
                        case ECommCheXian.供电车:
                            fee = fee + this.FaDianChe * double.Parse(dr1["UNITFixCOST"].ToString());
                            break;
                        case ECommCheXian.宿营车:
                            fee = fee + this.ShuYinChe * double.Parse(dr1["UNITFixCOST"].ToString());
                            break;
                    }
                }
                tab1.Close();
                fee = fee * this.YuXingLiCheng * 2 * 365;          //车厢数*单位定检成本标准*运行里程*2*365
            }
            else
            {
                double db = 0;
                if (this.HighTrainBigKind == EHighTrainBigKind.动车200公里)
                {
                    db = double.Parse(JStrInfoBU.GetStrTextByID("200公里车辆定期检修成本"));
                }
                else
                {
                    db = double.Parse(JStrInfoBU.GetStrTextByID("300公里车辆定期检修成本"));
                }
                fee = db * this.YuXingLiCheng * 2;   //（200公里/300公里 标准）*运行里程 *2
            }
            return fee;
        }
        #endregion

        #region CB9:客运消耗备用备品
        /// <summary>
        /// 车辆消耗备用品
        /// </summary>
        /// <returns></returns>
        public double GetXiaoHaoCost()
        {
            double fee = 0;
            if (this.TrainBigKind == ETrainBigKind.普通列车)
            {
                JTable tab1 = new JTable("CHEXIANWEIGHTPROFILE");
                List<SearchField> condition = new List<SearchField>();
                tab1.OrderBy = "CHEXIANTYPE";
                DataSet ds1 = tab1.SearchData(null, -1, "*");
                foreach (DataRow dr1 in ds1.Tables[0].Rows)
                {
                    ECommCheXian type1 = (ECommCheXian)(int.Parse(dr1["CHEXIANTYPE"].ToString()));
                    switch (type1)
                    {
                        case ECommCheXian.硬座:
                            fee = fee + this.YinZuo * double.Parse(dr1["UNITXHCOST"].ToString());
                            break;
                        case ECommCheXian.软座:
                            fee = fee + this.RuanZuo * double.Parse(dr1["UNITXHCOST"].ToString());
                            break;
                        case ECommCheXian.开放式硬卧:
                            fee = fee + this.OpenYinWo * double.Parse(dr1["UNITXHCOST"].ToString());
                            break;
                        case ECommCheXian.包房式硬卧:
                            fee = fee + this.CloseYinWo * double.Parse(dr1["UNITXHCOST"].ToString());
                            break;
                        case ECommCheXian.软卧:
                            fee = fee + this.RuanWo * double.Parse(dr1["UNITXHCOST"].ToString());
                            break;
                        case ECommCheXian.高级软卧:
                            fee = fee + this.AdvanceRuanWo * double.Parse(dr1["UNITXHCOST"].ToString());
                            break;
                        case ECommCheXian.餐车:
                            fee = fee + this.CanChe * double.Parse(dr1["UNITXHCOST"].ToString());
                            break;
                        case ECommCheXian.供电车:
                            fee = fee + this.FaDianChe * double.Parse(dr1["UNITXHCOST"].ToString());
                            break;
                        case ECommCheXian.宿营车:
                            fee = fee + this.ShuYinChe * double.Parse(dr1["UNITXHCOST"].ToString());
                            break;
                    }
                }
                tab1.Close();
                fee = fee * this.YongCheDiShu;
            }
            else
            {
                double db = 0;
                if (this.HighTrainBigKind == EHighTrainBigKind.动车200公里)
                {
                    db = double.Parse(JStrInfoBU.GetStrTextByID("200公里动车客运消耗备用备品成本"));
                }
                else
                {
                    db = double.Parse(JStrInfoBU.GetStrTextByID("300公里动车客运消耗备用备品成本"));
                }
                fee = db;                     //分200公里/300公里 标准2个标准
            }
            return fee;
        }
        #endregion

        #region CB10:空调车用油
        /// <summary>
        /// 用油单价
        /// </summary>
        /// <returns></returns>
        public double GetOilCost()
        {
            double fee = 0;
            if (this.TrainBigKind == ETrainBigKind.普通列车)
            {
                fee = 18 * this.YongYouBiaoZhun * this.YuXingLiCheng * this.OilUnitCost * 2 * 365/1000;
            }
            return fee;
        }
        #endregion

        #region CB11:人员其他
        /// <summary>
        /// 人员的其他费用
        /// </summary>
        /// <returns></returns>
        public double GetPersonOtherCost()
        {
            double fee = 0;
            int personcount = this.GetTotalServicePersonCount();
            fee = personcount * this.PersonOtherCost;
            return fee;
        }
        #endregion

        #region C12:购买车辆利息
        /// <summary>
        /// 购买车辆利息
        /// </summary>
        /// <returns></returns>
        public double GetLiXiCost()
        {
            double fee = 0;
            if (this.TrainBigKind == ETrainBigKind.普通列车)
            {
                fee += YinZuo * YinZuoPrice + RuanZuo * RuanZuoPrice;
                fee += OpenYinWo * OpenYinWoPrice + CloseYinWo * CloseYinWoPrice;
                fee += RuanWo * RuanWoPrice + CanChe * CanChePrice;
                fee += FaDianChe * FaDianChePrice + ShuYinChe * ShuYinChePrice;

                fee = fee * CheDiShu * FixLiXi;
            }
            else
            {
                double db = 0;
                if (this.HighTrainBigKind == EHighTrainBigKind.动车200公里)
                {
                    db = double.Parse(JStrInfoBU.GetStrTextByID("200公里动车购买成本标准"));
                }
                else
                {
                    db = double.Parse(JStrInfoBU.GetStrTextByID("300公里动车购买成本标准"));
                }
                db = db * 8;
                if (this.HighTrainBianZhu == EHighTrainBianZhu.重联)
                {
                    db = db * 2;
                }

                db = db * this.CheDiShu * this.FixLiXi;
                fee = db;
                
            }
            return fee;
        }
        #endregion
    }
}
