using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using WebFrame;
using WebFrame.Data;
using WebFrame.Util;
using WebFrame.Designer;
using WebFrame.ExpControl;

namespace BusinessRule
{
    public enum ETrainFareType
    {
        硬座, 软座,
        硬卧上铺, 硬卧中铺, 硬卧下铺,
        包房硬卧上铺, 包房硬卧下铺,
        软卧上铺, 软卧下铺,
        高级软卧上铺, 高级软卧下铺
    }


    /// <summary>
    /// 计算列车的票价（非新型空调列车）
    /// </summary>
    public class TrainShouRu1
    {
        protected int quDuanLiCheng;                          //区段里程
        protected double baseFarePrice;                       //基本的客票价
        protected double baoxianFee;                          //保险费

        protected int yunXinLiCheng;                          //运行里程
        protected bool kongTiaoFlag;                          //空调车标志
        protected EJiaKuai jiakuaiType;                        //加快类型
       

        public TrainShouRu1(
            int yunXinLiCheng, 
            bool kongTiaoFlag, 
            EJiaKuai jiakuaiType)
        {
            this.yunXinLiCheng = yunXinLiCheng;
            this.kongTiaoFlag = kongTiaoFlag;
            this.jiakuaiType = jiakuaiType;

            //设置区段里程和基本的客票价
            this.quDuanLiCheng = GetQuDuanLiCheng(this.yunXinLiCheng);
            this.baseFarePrice = GetKePiaoFee(this.quDuanLiCheng);
            this.baoxianFee = Math.Round(this.baseFarePrice * TrainProfile.BaoXianFee,1); 
        }

        public override string ToString()
        {
            String str1 = "运行里程{0},硬座{1},软座{2},加快{3},硬卧上铺{4},硬卧中铺{5},硬卧下铺{6},软卧上铺{7},软卧下铺{8},空调{9}";
            
            str1 = String.Format(str1, this.yunXinLiCheng, this.YinZuoPrice, this.RuanZuoPrice,
                    this.JiaKuaiFee,this.YinWoPrice1,this.YinWoPrice2,this.YinWoPrice3,
                    this.RuanWoPrice1,  this.RuanWoPrice2,this.KongTiaoFee);
            return str1;
        }

        #region 票价
        //硬座票价
        public virtual double YinZuoPrice
        {
            get
            {
                double fujiaFee = this.GetFuJiaFee(ETrainFareType.硬座);
                double fee = JMath.Round1(this.baseFarePrice,1) + this.baoxianFee + fujiaFee;
                fee= JMath.Round1(fee,0);
                return fee;
            }
        }

        //软座票价
        public virtual double RuanZuoPrice
        {
            get
            {
                double fee = this.baseFarePrice ;
                double rate = ChexianBianZhuData.RuanZuo_Rate /100d;
                double fujiaFee = this.GetFuJiaFee(ETrainFareType.软座);
                fee = fee * rate+this.baoxianFee+fujiaFee;
                return JMath.Round1(fee, 0);
            }
        }

        //加快费
        public virtual double JiaKuaiFee
        {
            get
            {
                double fee = this.baseFarePrice;
                double rate = JiaKuaiProfile.JkFee1;
                fee = JMath.Round1(fee * rate / 100d,0);
                double rate1 = 1;

                if(this.jiakuaiType==EJiaKuai.加快)
                {
                    rate1=1;
                }
                else if(this.jiakuaiType==EJiaKuai.特快)
                {
                    rate1 = JiaKuaiProfile.JkFee2 / JiaKuaiProfile.JkFee1;
                }
                else if(this.jiakuaiType==EJiaKuai.特快附加)
                {
                    rate1 = JiaKuaiProfile.JkFee3 / JiaKuaiProfile.JkFee1;
                }
                
                return rate1*fee;
            }
        }

        //空调费
        public virtual double KongTiaoFee
        {
            get
            {
                double fee = 0;
                if (kongTiaoFlag)
                {
                    double fee1 = this.baseFarePrice;
                    double rate = TrainProfile.KongTiaoFeeRate;
                    fee = fee1 * TrainProfile.KongTiaoFeeRate;
                }
                return JMath.Round1(fee,0);
            }
        }

        //硬卧上铺票价
        public virtual double YinWoPrice1
        {
            get
            {
                double rate1 = ChexianBianZhuData.YinWo1_Rate / 100d;
                return JMath.Round1(rate1 * this.baseFarePrice + TrainProfile.WoPuDingPiaoFee, 0);
            }
        }

        //硬卧中铺票价
        public virtual double YinWoPrice2
        {
            get
            {
                double rate1 = ChexianBianZhuData.YinWo2_Rate / 100d;
                return JMath.Round1(rate1 * this.baseFarePrice + TrainProfile.WoPuDingPiaoFee, 0);
            }
        }

        //硬卧下铺票价
        public virtual double YinWoPrice3
        {
            get
            {
                double rate1 = ChexianBianZhuData.YinWo3_Rate / 100d;
                return JMath.Round1(rate1 * this.baseFarePrice + TrainProfile.WoPuDingPiaoFee, 0);
            }
        }

        //软卧上铺票价
        public virtual double RuanWoPrice1
        {
            get
            {
                double rate1 = ChexianBianZhuData.RuanWo1_Rate / 100d;
                return JMath.Round1(rate1 * this.baseFarePrice + TrainProfile.WoPuDingPiaoFee, 0);
            }
        }

        //软卧下铺票价
        public virtual double RuanWoPrice2
        {
            get
            {
                double rate1 = ChexianBianZhuData.RuanWo2_Rate / 100d;
                return JMath.Round1(rate1 * this.baseFarePrice + TrainProfile.WoPuDingPiaoFee, 0);
            }
        }

        //高级软卧上铺票价
        public virtual double AdvenceRuanWoPrice1
        {
            get
            {
                double rate1 = ChexianBianZhuData.GaoJiRuanWo1_Rate / 100d;
                return JMath.Round1(rate1 * this.baseFarePrice + TrainProfile.WoPuDingPiaoFee, 0);
            }
        }

        //高级软卧下铺票价
        public virtual double AdvanceRuanWoPrice2
        {
            get
            {
                double rate1 = ChexianBianZhuData.GaoJiRuanWo2_Rate / 100d;
                return JMath.Round1(rate1 * this.baseFarePrice + TrainProfile.WoPuDingPiaoFee, 0);
            }
        }
        #endregion

        #region 计算票价的私有方法
        // 根据运行里程得到区段里程
        /*
         计算的方法如下：
         除初始区段不足起码里程按起码里程和最后一个区段按中间里程计算外，
         其余各区段均分别按其区段里程计算，根据各区段的递减票价率求出该区段的
         全程票价和最后一个区段按中间里程求出的票价加总，
        */
        private static int GetQuDuanLiCheng(int yuXingLiCheng)
        {
            int result = 0;
            DataTable dt1 = LiChengProfile.Data;
            DataRow dr1 = null;
            String search = "(Pos1<=" + yuXingLiCheng + ") and (" + "Pos2>=" + yuXingLiCheng + " or Pos2 is null)";
            DataRow[] drs = dt1.Select(search);
            if (drs.Length > 0)
            {
                dr1 = drs[0];
            }

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
                    y0 = 1.0;
                }
                else if (y0 >= 0.5)
                {
                    y0 = 1.0;
                }
                else
                {
                    y0 = 0;
                }
                y0 = y0 - 0.5;
                result = (pos1 - 1) + (int)((y1 + y0) * posSize);
            }
            return result;
        }

        // 根据区段里程计算基本客票价（重要方法)
        private static double GetKePiaoFee(int quDuanLiCheng)
        {
            double baseFee = TrainProfile.BaseFee;
            double Fee = 0;

            DataTable dt1 = LiChengJianRate.Data;
            String search = "Pos1<=" + quDuanLiCheng;
            DataRow[] drs = dt1.Select(search);

            int pos1 = 1;
            int pos2 = -1;
            int rate = 0;
            foreach (DataRow dr1 in drs)
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
            return Fee;
        }


        //得到附加费
        /*
         联合票价(即票面价格)为旅客票价加附加费。
         * 附加费的种类有：客票发展金、候车室空调费、卧铺票订票费。
         * 客票发展金以前叫“软票费”，旅客票价不大于5元时为0.5元，大于5元时为1元；
         * 候车室空调费向乘车超过200km的硬席旅客收取，金额为1元，
         * 软席旅客不收候车室空调费；
         * 卧铺票订票费向购买卧铺票(包括各种等级的软卧、硬卧)的旅客收取，金额为10元
         */
        protected  double GetFuJiaFee(ETrainFareType fareType)
        {
            double fee1 = 0;   //客票发展金
            if (this.baseFarePrice + this.baoxianFee <= 5)
            {
                fee1 = 0.5;
            }
            else
            {
                fee1 = 1;
            }

            double fee2 = 0;   //候车室空调费
            if (fareType  == ETrainFareType.硬座 && quDuanLiCheng > 200)
            {
                fee2 = 1;
            }
            
            
            return fee1 + fee2 ;
        }
        #endregion
    }

    //新型空调车的票价收入计算
    public class TrainShouRu2 : TrainShouRu1
    {
        private const double ShangFuRate= 0.5;      //上浮系数
  
        public TrainShouRu2(int yunXinLiChenge, EJiaKuai jiakuai)
            :base(yunXinLiChenge,true,jiakuai){ ;}

        #region 票价
        //硬座票价
        public override double YinZuoPrice
        {
            get
            {
                double fee1 = base.YinZuoPrice * (1 + ShangFuRate);
                double fujiaFee = this.GetFuJiaFee(ETrainFareType.硬座);
                return JMath.Round1(fee1-fujiaFee*ShangFuRate,0);
            }
        }

        //软座票价
        public override double RuanZuoPrice
        {
            get
            {
                double fee1 = base.RuanZuoPrice * (1 + ShangFuRate);
                double fujiaFee = this.GetFuJiaFee(ETrainFareType.软座);
                return JMath.Round1(fee1 - fujiaFee * ShangFuRate, 0);
            }
        }

        //加快费
        public override double JiaKuaiFee
        {
            get
            {
                double fee1 = base.JiaKuaiFee + JMath.Round1(base.JiaKuaiFee * ShangFuRate, 0);
                double rate1 = 1;

                if (this.jiakuaiType == EJiaKuai.加快)
                {
                    rate1 = 1;
                }
                else if (this.jiakuaiType == EJiaKuai.特快)
                {
                    rate1 = JiaKuaiProfile.JkFee2 / JiaKuaiProfile.JkFee1;
                }
                else if (this.jiakuaiType == EJiaKuai.特快附加)
                {
                    rate1 = JiaKuaiProfile.JkFee3 / JiaKuaiProfile.JkFee1;
                }

                return JMath.Round1(fee1/rate1,0)*rate1;
            }
        }

        //空调费
        public override double KongTiaoFee
        {
            get
            {
                return base.KongTiaoFee + JMath.Round1(base.KongTiaoFee*ShangFuRate, 0);
            }
        }

        //硬卧上铺票价
        public override double YinWoPrice1
        {
            get
            {
                double rate1 = ChexianBianZhuData.YinWo1_Rate / 100d;
                double fee1 = JMath.Round1(rate1 * this.baseFarePrice, 0);
                return fee1+JMath.Round1(fee1 * ShangFuRate, 0) + TrainProfile.WoPuDingPiaoFee;
            }
        }

        //硬卧中铺价格
        public override double YinWoPrice2
        {
            get
            {
                double rate1 = ChexianBianZhuData.YinWo2_Rate / 100d;
                double fee1 = JMath.Round1(rate1 * this.baseFarePrice, 0);
                return fee1 + JMath.Round1(fee1 * ShangFuRate, 0) + TrainProfile.WoPuDingPiaoFee;
            }
        }

        //硬卧下铺价格
        public override double YinWoPrice3
        {
            get
            {
                double rate1 = ChexianBianZhuData.YinWo3_Rate / 100d;
                double fee1 = JMath.Round1(rate1 * this.baseFarePrice, 0);
                return fee1 + JMath.Round1(fee1 * ShangFuRate, 0) + TrainProfile.WoPuDingPiaoFee;
            }
        }

        //软卧上铺价格
        public override double RuanWoPrice1
        {
            get
            {
                double rate1 = ChexianBianZhuData.RuanWo1_Rate / 100d;
                double fee1 = JMath.Round1(rate1 * this.baseFarePrice, 0);
                return fee1 + JMath.Round1(fee1 * ShangFuRate, 0) + TrainProfile.WoPuDingPiaoFee;
            }
        }

        //软卧中铺价格
        public override double RuanWoPrice2
        {
            get
            {
                double rate1 = ChexianBianZhuData.RuanWo2_Rate / 100d;
                double fee1 = JMath.Round1(rate1 * this.baseFarePrice, 0);
                return fee1 + JMath.Round1(fee1 * ShangFuRate, 0) + TrainProfile.WoPuDingPiaoFee;
            }
        }

        //高级软卧上铺
        public override double AdvenceRuanWoPrice1
        {
            get
            {
                double rate1 = ChexianBianZhuData.GaoJiRuanWo1_Rate / 100d;
                double fee1 = JMath.Round1(rate1 * this.baseFarePrice, 0);
                return fee1 + JMath.Round1(fee1 * ShangFuRate, 0) + TrainProfile.WoPuDingPiaoFee;
            }
        }

        //高级软卧下铺
        public override double AdvanceRuanWoPrice2
        {
            get
            {
                double rate1 = ChexianBianZhuData.GaoJiRuanWo2_Rate / 100d;
                double fee1 = JMath.Round1(rate1 * this.baseFarePrice, 0);
                return fee1 + JMath.Round1(fee1 * ShangFuRate, 0) + TrainProfile.WoPuDingPiaoFee;
            }
        }
        #endregion
    }



    //动车的票价类型
    public enum EDTrainFareType
    {
        普动二等软座, 普动一等软座, 普动特等软座,
        高铁二等软座, 高铁一等软座, 动卧上铺, 动卧下铺
    }

    //动车的票价收入
    public class DTrainShouRu
    {
        private static Dictionary<EDTrainFareType ,double> Rate=new Dictionary<EDTrainFareType,double>();
        static DTrainShouRu()
        {
            Rate[EDTrainFareType.普动二等软座] = 0.2805 ;
            Rate[EDTrainFareType.普动一等软座] = 0.3360 ;
            Rate[EDTrainFareType.普动特等软座] = 0.4208 ;
            Rate[EDTrainFareType.高铁二等软座] = 0.4833 ;
            Rate[EDTrainFareType.高铁一等软座] = 0.7733 ;
            Rate[EDTrainFareType.动卧上铺] = 0.3366*1.1*1.6;
            Rate[EDTrainFareType.动卧上铺] = 0.3366 * 1.1 * 1.8;
        }

        //得到动车的价格
        public static double GetTrainPrice(EDTrainFareType type1, int yunXingLiCheng)
        {
            double result = 0;
            result = Rate[type1] * yunXingLiCheng;
            result = JMath.Round1(result + result * TrainProfile.BaoXianFee,0);
            return result;
        }
    }
}
