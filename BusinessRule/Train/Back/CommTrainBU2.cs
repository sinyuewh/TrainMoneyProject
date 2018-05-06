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
        /// 得到总收入的计算方法
        /// </summary>
        /// <returns></returns>
        public double GetTotalShouru()
        {
            if (this.TrainBigKind == ETrainBigKind.动车)
            {
                return this.GetTotalShouruForHighTrain();
            }
            else
            {
                return this.GetTotalShouruForCommTrain();
            }
        }

        #region 普通列车的收入计算
        /// <summary>
        /// 根据运行里程得到区段里程
        /// </summary>
        /// <param name="yuxinglicheng"></param>
        /// <returns></returns>
        private int GetQuDuanLiCheng(int yuXingLiCheng)
        {
            int result = 0;
            JTable tab1 = null;
            try
            {
                tab1 = new JTable("LiChengProfile");
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("Pos1", yuXingLiCheng + "", SearchOperator.SmallerAndEqual, SearchFieldType.NumericType));
                condition.Add(new SearchField("(Pos2 >= " + yuXingLiCheng + " or Pos2 is null )", "", SearchOperator.UserDefine));
                tab1.OrderBy = "id";
                DataRow dr1 = tab1.GetFirstDataRow(condition, "*");
                if (dr1 != null)
                {
                    int pos1 = int.Parse(dr1["pos1"].ToString());
                    int posSize = int.Parse(dr1["posSize"].ToString());

                    int y1 = (yuXingLiCheng - pos1 + 1) / posSize;
                    double t = 0;
                    double y2 = (yuXingLiCheng - pos1 + 1 + t) / (posSize + t);
                    double y0 = y2 - y1;

                    if (y0 > 0 && y0 < 0.5)
                    {
                        y0 = 0.5;
                    }
                    else if (y0 >= 0.5)
                    {
                        y0 = 1.0;
                    }
                    else
                    {
                        y0 = 0;
                    }

                    result = (pos1 - 1) + (int)((y1 + y0) * posSize);
                }
            }
            finally
            {
                tab1.Close();
            }
            return result;
        }

        /// <summary>
        /// 根据区段里程计算硬座票
        /// </summary>
        /// <param name="quDuanLiCheng"></param>
        /// <returns></returns>
        private double GetYingZuoFee(int quDuanLiCheng)
        {
            double baseFee = this.BaseFee;
            double Fee = 0;
            JTable tab1 = null;
            try
            {
                tab1 = new JTable("LiChengJianRate");
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("Pos1", quDuanLiCheng + "",
                    SearchOperator.SmallerAndEqual, SearchFieldType.NumericType));
                tab1.OrderBy = "Pos1";
                DataSet ds1 = tab1.SearchData(condition, -1, "*");
                int pos1 = 1;
                int pos2 = -1;
                int rate = 0;
                foreach (DataRow dr1 in ds1.Tables[0].Rows)
                {
                    if (String.IsNullOrEmpty(dr1["pos1"].ToString()) == false)
                    {
                        pos1 = int.Parse(dr1["pos1"].ToString());
                    }
                    else
                    {
                        pos1 = 1;
                    }

                    if (String.IsNullOrEmpty(dr1["pos2"].ToString()) == false)
                    {
                        pos2 = int.Parse(dr1["pos2"].ToString());
                    }
                    else
                    {
                        pos2 = -1;
                    }

                    if (String.IsNullOrEmpty(dr1["JianRate"].ToString()) == false)
                    {
                        rate = int.Parse(dr1["JianRate"].ToString());
                    }
                    else
                    {
                        rate = 0;
                    }

                    ///////////////////////////////////////////////////////
                    if (pos2 != -1)
                    {
                        if (quDuanLiCheng >= pos2)
                        {
                            Fee = Fee + baseFee * (1 - rate / 100d) * (pos2 - pos1 + 1);
                        }
                        else
                        {
                            Fee = Fee + baseFee * (1 - rate / 100d) * (quDuanLiCheng - pos1 + 1);
                        }
                    }
                    else
                    {
                        Fee = Fee + baseFee * (1 - rate / 100d) * (quDuanLiCheng - pos1 + 1);
                    }

                }
            }
            finally
            {
                tab1.Close();
            }
            return Math.Round(Fee, 2);
        }


        /// <summary>
        /// 根据运行里程得到硬座费用
        /// </summary>
        /// <param name="yuXingLiCheng"></param>
        /// <returns></returns>
        private double GetYinZuoFee()
        {
            return this.GetYingZuoFee(this.GetQuDuanLiCheng(this.YuXingLiCheng));
        }

        /// <summary>
        /// 设置不同车厢类型的数量
        /// </summary>
        /// <param name="type">车厢类型</param>
        /// <param name="Count1">车厢数量</param>
        public void SetCheXianCount(ECommCheXian type, int Count1)
        {
            int type1 = (int)type;
            switch (type1)
            {
                case 0:
                    this.YinZuo = Count1;
                    break;
                case 1:
                    this.RuanZuo = Count1;
                    break;
                case 2:
                    this.OpenYinWo = Count1;
                    break;
                case 3:
                    this.CloseYinWo = Count1;
                    break;
                case 4:
                    this.RuanWo = Count1;
                    break;
                case 5:
                    this.AdvanceRuanWo = Count1;
                    break;
                case 6:
                    this.CanChe = Count1;
                    break;
                case 7:
                    this.FaDianChe = Count1;
                    break;
                case 8:
                    this.ShuYinChe = Count1;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 检查编组的数量是否正确
        /// </summary>
        /// <returns></returns>
        public bool CheckBianZhu()
        {
            if ((this.YinZuo + this.RuanZuo
                + this.OpenYinWo + this.CloseYinWo
                + this.RuanWo + this.AdvanceRuanWo
                + this.CanChe + this.FaDianChe + this.ShuYinChe == 18)
                && this.YinZuo >= 0
                && this.RuanZuo >= 0
                && this.OpenYinWo >= 0
                && this.CloseYinWo >= 0
                && this.RuanWo >= 0
                && this.AdvanceRuanWo >= 0
                && this.CanChe >= 0
                && this.FaDianChe >= 0
                && this.ShuYinChe >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到整个车厢的票价率
        /// </summary>
        /// <returns></returns>
        private double GetFeeRate()
        {
            double Fee = 0;
            if (this.YinZuo > 0)
            {
                Fee = Fee + this.YinZuo * GetCheXianFeeTotalRate(ECommCheXian.硬座);
            }
            if (this.RuanZuo > 0)
            {
                Fee = Fee + this.RuanZuo * GetCheXianFeeTotalRate(ECommCheXian.硬座);
            }

            if (this.OpenYinWo > 0)
            {
                Fee = Fee + this.OpenYinWo * GetCheXianFeeTotalRate(ECommCheXian.开放式硬卧);
            }

            if (this.CloseYinWo > 0)
            {
                Fee = Fee + this.CloseYinWo * GetCheXianFeeTotalRate(ECommCheXian.包房式硬卧);
            }

            if (this.RuanWo > 0)
            {
                Fee = Fee + this.RuanWo * GetCheXianFeeTotalRate(ECommCheXian.软卧);
            }

            if (this.AdvanceRuanWo > 0)
            {
                Fee = Fee + this.AdvanceRuanWo * GetCheXianFeeTotalRate(ECommCheXian.高级软卧);
            }
            return Fee;
        }

        /// <summary>
        /// 得到列车的总收入
        /// </summary>
        /// <returns></returns>
        private double GetTotalShouruForCommTrain()
        {
            int quduanlicheng = this.GetQuDuanLiCheng(this.YuXingLiCheng);  //得到区段里程
            double Fee1 = this.GetYingZuoFee(quduanlicheng);                //得到列车的硬座基本费；
            Fee1 = Fee1 * this.GetFeeRate();                                //得到基本的票价收入

            Fee1 = Fee1 * this.GetJiaKuaiFee();                             //加快
            Fee1 = Fee1 * this.KongTiaoFeeRate;                            //空调费用

            //席别增减费 和 保险费用
            Fee1 = Fee1 * (1 + this.XieBieZhengJiaFee / 100d) * (1 + this.BaoXianFee / 100d);

            return Fee1*2*365;
        }

        

        /// <summary>
        /// 得到列车的满员人数
        /// </summary>
        /// <returns></returns>
        public int GetTotalPerson()
        {
            if (this.TrainBigKind == ETrainBigKind.普通列车)
            {
                int result = 0;
                if (this.YinZuo > 0)
                {
                    result = result + this.YinZuo * GetCheXianTotalPerson(ECommCheXian.硬座);
                }
                if (this.RuanZuo > 0)
                {
                    result = result + this.RuanZuo * GetCheXianTotalPerson(ECommCheXian.硬座);
                }

                if (this.OpenYinWo > 0)
                {
                    result = result + this.OpenYinWo * GetCheXianTotalPerson(ECommCheXian.开放式硬卧);
                }

                if (this.CloseYinWo > 0)
                {
                    result = result + this.CloseYinWo * GetCheXianTotalPerson(ECommCheXian.包房式硬卧);
                }

                if (this.RuanWo > 0)
                {
                    result = result + this.RuanWo * GetCheXianTotalPerson(ECommCheXian.软卧);
                }

                if (this.AdvanceRuanWo > 0)
                {
                    result = result + this.AdvanceRuanWo * GetCheXianTotalPerson(ECommCheXian.高级软卧);
                }
                return result;
            }
            else
            {
                return this.GetTotalPersonForHighTrain();   //动车组的满员
            }
        }

        /// <summary>
        /// 计算列车的重量
        /// </summary>
        /// <returns></returns>
        private double GetTrainWeight()
        {
            double result = 0;
            if (this.TrainBigKind == ETrainBigKind.普通列车)
            {
                if (this.YinZuo > 0)
                {
                    result = result + this.YinZuo * GetCheXianWeight(ECommCheXian.硬座);
                }
                if (this.RuanZuo > 0)
                {
                    result = result + this.RuanZuo * GetCheXianWeight(ECommCheXian.硬座);
                }

                if (this.OpenYinWo > 0)
                {
                    result = result + this.OpenYinWo * GetCheXianWeight(ECommCheXian.开放式硬卧);
                }

                if (this.CloseYinWo > 0)
                {
                    result = result + this.CloseYinWo * GetCheXianWeight(ECommCheXian.包房式硬卧);
                }

                if (this.RuanWo > 0)
                {
                    result = result + this.RuanWo * GetCheXianWeight(ECommCheXian.软卧);
                }

                if (this.AdvanceRuanWo > 0)
                {
                    result = result + this.AdvanceRuanWo * GetCheXianWeight(ECommCheXian.高级软卧);
                }

                if (this.CanChe > 0)
                {
                    result = result + this.CanChe * GetCheXianWeight(ECommCheXian.餐车);
                }

                if (this.FaDianChe > 0)
                {
                    result = result + this.FaDianChe * GetCheXianWeight(ECommCheXian.供电车);
                }

                if (this.ShuYinChe > 0)
                {
                    result = result + this.ShuYinChe * GetCheXianWeight(ECommCheXian.宿营车);
                }
            }
            else
            {
                //计算动车组的重量
                double temp = 0;
                if (this.HighTrainBigKind == EHighTrainBigKind.动车200公里)
                {
                    temp = double.Parse(JStrInfoBU.GetStrTextByID("200公里动车车厢重量"));
                }
                else
                {
                    temp = double.Parse(JStrInfoBU.GetStrTextByID("300公里动车车厢重量"));
                }
                temp = temp * 8;
                if (this.HighTrainBianZhu == EHighTrainBianZhu.重联)
                {
                    temp = temp * 2;
                }
                result = temp;
            }
            return result;
        }

        /// <summary>
        /// 得到车厢类型的票价率和满员人数
        /// </summary>
        /// <param name="type1"></param>
        /// <returns></returns>
        private static CommChexianRateAndPersonCount GetCheXianFeeRateAndPersonCount(ECommCheXian type1)
        {
            CommChexianRateAndPersonCount rp1 = new CommChexianRateAndPersonCount();
            JTable tab1 = new JTable("CheXianBianZhu");
            List<SearchField> condition = new List<SearchField>();
            if (type1 == ECommCheXian.硬座)
            {
                condition.Add(new SearchField("kind", "YinZuo"));
            }
            else if (type1 == ECommCheXian.软座)
            {
                condition.Add(new SearchField("kind", "RuanZuo"));
            }
            else if (type1 == ECommCheXian.开放式硬卧)
            {
                condition.Add(new SearchField("(kind='YinWo1' or kind='YinWo2' or kind='YinWo3')", "", SearchOperator.UserDefine));
            }
            else if (type1 == ECommCheXian.包房式硬卧)
            {
                condition.Add(new SearchField("(kind='YinWo4' or kind='YinWo5')", "", SearchOperator.UserDefine));
            }
            else if (type1 == ECommCheXian.软卧)
            {
                condition.Add(new SearchField("(kind='RuanWo1' or kind='RuanWo2')", "", SearchOperator.UserDefine));
            }
            else if (type1 == ECommCheXian.高级软卧)
            {
                condition.Add(new SearchField("(kind='GaoJiRuanWo1' or kind='GaoJiRuanWo2')", "", SearchOperator.UserDefine));
            }
            tab1.OrderBy = "kind";
            DataSet ds1 = tab1.SearchData(condition, -1, "*");
            int i = 0;
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                rp1.SetRate(i, int.Parse(dr1["Rate"].ToString()));
                rp1.SetPerson(i, int.Parse(dr1["PCount"].ToString()));
                i++;
            }
            tab1.Close();
            return rp1;
        }

        /// <summary>
        /// 得到车厢类型的整体票价率
        /// </summary>
        /// <param name="type1"></param>
        /// <returns></returns>
        private static double GetCheXianFeeTotalRate(ECommCheXian type1)
        {
            double d1 = 0;
            CommChexianRateAndPersonCount rp1 = GetCheXianFeeRateAndPersonCount(type1);
            d1 = (rp1.Rate1 / 100d) * rp1.PersonCount1 + (rp1.Rate2 / 100d) * rp1.PersonCount2 + (rp1.Rate3 / 100d) * rp1.PersonCount3;
            return d1;
        }

        /// <summary>
        /// 得到车厢的满员人数
        /// </summary>
        /// <param name="type1"></param>
        /// <returns></returns>
        private static int GetCheXianTotalPerson(ECommCheXian type1)
        {
            int d1 = 0;
            CommChexianRateAndPersonCount rp1 = GetCheXianFeeRateAndPersonCount(type1);
            d1 = rp1.PersonCount1 + rp1.PersonCount2 + rp1.PersonCount3;
            return d1;
        }

        /// <summary>
        /// 得到不同车厢的重量配置
        /// </summary>
        /// <param name="type1"></param>
        /// <returns></returns>
        private static double GetCheXianWeight(ECommCheXian type1)
        {
            double d1 = 0;
            JTable tab1 = new JTable("CHEXIANWEIGHTPROFILE");
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("CHEXIANTYPE", (int)type1 + "", SearchFieldType.NumericType));
            DataRow dr1 = tab1.GetFirstDataRow(condition, "Weight");
            if (dr1 != null)
            {
                d1 = double.Parse(dr1[0].ToString());
            }
            tab1.Close();
            return d1;
        }

        /// <summary>
        /// 得到加快的费率
        /// </summary>
        /// <param name="type1"></param>
        /// <returns></returns>
        private double GetJiaKuaiFeeByKind(ECommJiaKuai type1)
        {
            double fee0 = 0;
            JTable tab1 = new JTable("JiaKuaiProfile");
            List<SearchField> condition = new List<SearchField>();
            if (type1 == ECommJiaKuai.加快)
            {
                condition.Add(new SearchField("JiaKuaiType", "jk1"));
            }
            else if (type1 == ECommJiaKuai.特快)
            {
                condition.Add(new SearchField("JiaKuaiType", "jk2"));
            }
            else if (type1 == ECommJiaKuai.特快附加)
            {
                condition.Add(new SearchField("JiaKuaiType", "jk3"));
            }
            else
            {
                condition.Add(new SearchField("JiaKuaiType", "-1"));
            }
            DataRow dr1 = tab1.GetFirstDataRow(condition, "*");
            if (dr1 != null)
            {
                fee0 = double.Parse(dr1["Fee"].ToString());
            }
            tab1.Close();
            return fee0;
        }

        /// <summary>
        /// 得到加快费率
        /// </summary>
        /// <returns></returns>
        private double GetJiaKuaiFee()
        {
            double fee1 = GetJiaKuaiFeeByKind(this.JiaKuai);
            return (1 + fee1 / 100d);
        }
        #endregion

        #region 动车组收入业务方法
        /// <summary>
        /// 计算动车组的票价总收入
        /// </summary>
        /// <param name="HighTrainType"></param>
        /// <returns></returns>
        private double GetTotalShouruForHighTrain()
        {
            double Fee = 0;
            if (this.TrainBigKind == ETrainBigKind.动车)
            {
                JTable tab1 = new JTable("HighTrainProfile");
                List<SearchField> condition = new List<SearchField>();
                if (String.IsNullOrEmpty(this.TrainType) == false)
                {
                    condition.Add(new SearchField("HighTrainType", TrainType));
                }
                else
                {
                    condition.Add(new SearchField("HighTrainType", "-1"));
                }

                DataRow dr1 = tab1.GetFirstDataRow(condition, "*");
                if (dr1 != null)
                {
                    for (int i = 1; i <= 4; i++)
                    {
                        Fee = Fee + this.YuXingLiCheng
                            * double.Parse(dr1["PCount" + i].ToString())
                            * double.Parse(dr1["Rate" + i].ToString());
                    }
                }

                tab1.Close();
            }
            return Fee;
        }

        /// <summary>
        /// 得到动车的满员人数
        /// </summary>
        /// <returns></returns>
        private int GetTotalPersonForHighTrain()
        {
            int total = 0;
            if (this.TrainBigKind == ETrainBigKind.动车)
            {
                JTable tab1 = new JTable("HighTrainProfile");
                List<SearchField> condition = new List<SearchField>();
                if (String.IsNullOrEmpty(this.TrainType) == false)
                {
                    condition.Add(new SearchField("HighTrainType", TrainType));
                }
                else
                {
                    condition.Add(new SearchField("HighTrainType", "-1"));
                }

                DataRow dr1 = tab1.GetFirstDataRow(condition, "*");
                if (dr1 != null)
                {
                    for (int i = 1; i <= 4; i++)
                    {
                        total = total + int.Parse(dr1["PCount" + i].ToString());
                    }
                }

                tab1.Close();
            }
            return total;
        }
        #endregion
    }
}
