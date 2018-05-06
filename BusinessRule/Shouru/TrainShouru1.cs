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
                if (this.hasdata == false)
                {
                    double fee1 = base.YinZuoPrice * (1 + ShangFuRate);
                    double fujiaFee = this.GetFuJiaFee(ETrainFareType.硬座);
                    return JMath.Round1(fee1 - fujiaFee * ShangFuRate, 0);
                }
                else
                {
                    return ticket1.YZPrice;
                }
            }
        }

        //软座票价
        public override double RuanZuoPrice
        {
            get
            {
                if (this.hasdata == false)
                {
                    double fee1 = base.RuanZuoPrice * (1 + ShangFuRate);
                    double fujiaFee = this.GetFuJiaFee(ETrainFareType.软座);
                    return JMath.Round1(fee1 - fujiaFee * ShangFuRate, 0);
                }
                else
                {
                    return ticket1.RZPrice;
                }
            }
        }

        //加快费
        public override double JiaKuaiFee
        {
            get
            {
                if (this.hasdata == false)
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

                    return JMath.Round1(fee1 / rate1, 0) * rate1;
                }
                else
                {
                    return this.JkFee;
                }
            }
        }

        //计算空调费
        public override double GetKongTiaoFee(ECommTrainType traintype,
            ECommCheXian chexian1)
        {
            if (this.hasdata == false)
            {
                double fee1 = base.GetKongTiaoFee(traintype,chexian1);
                return  fee1+ JMath.Round1(fee1 * ShangFuRate, 0);
            }
            else
            {
                return this.KtFee;
            }
        }

        

        //硬卧上铺票价
        public override double YinWoPrice1
        {
            get
            {
                if (this.hasdata == false)
                {
                    double rate1 = ChexianBianZhuData.YinWo1_Rate / 100d;
                    double fee1 = JMath.Round1(rate1 * this.baseFarePrice, 0);
                    return fee1 + JMath.Round1(fee1 * ShangFuRate, 0) + TrainProfile.WoPuDingPiaoFee;
                }
                else
                {
                    return ticket1.YWSPrice;
                }
            }
        }

        //硬卧中铺价格
        public override double YinWoPrice2
        {
            get
            {
                if (this.hasdata == false)
                {
                    double rate1 = ChexianBianZhuData.YinWo2_Rate / 100d;
                    double fee1 = JMath.Round1(rate1 * this.baseFarePrice, 0);
                    return fee1 + JMath.Round1(fee1 * ShangFuRate, 0) + TrainProfile.WoPuDingPiaoFee;
                }
                else
                {
                    return ticket1.YWZPrice;
                }
            }
        }

        //硬卧下铺价格
        public override double YinWoPrice3
        {
            get
            {
                if (this.hasdata == false)
                {
                    double rate1 = ChexianBianZhuData.YinWo3_Rate / 100d;
                    double fee1 = JMath.Round1(rate1 * this.baseFarePrice, 0);
                    return fee1 + JMath.Round1(fee1 * ShangFuRate, 0) + TrainProfile.WoPuDingPiaoFee;
                }
                else
                {
                    return ticket1.YWXPrice;
                }
            }
        }

        //软卧上铺价格
        public override double RuanWoPrice1
        {
            get
            {
                if (this.hasdata == false)
                {
                    double rate1 = ChexianBianZhuData.RuanWo1_Rate / 100d;
                    double fee1 = JMath.Round1(rate1 * this.baseFarePrice, 0);
                    return fee1 + JMath.Round1(fee1 * ShangFuRate, 0) + TrainProfile.WoPuDingPiaoFee;
                }
                else
                {
                    return ticket1.RWSPrice;
                }
            }
        }

        //软卧下铺价格
        public override double RuanWoPrice2
        {
            get
            {
                if (this.hasdata == false)
                {
                    double rate1 = ChexianBianZhuData.RuanWo2_Rate / 100d;
                    double fee1 = JMath.Round1(rate1 * this.baseFarePrice, 0);
                    return fee1 + JMath.Round1(fee1 * ShangFuRate, 0) + TrainProfile.WoPuDingPiaoFee;
                }
                else
                {
                    return ticket1.RWXPrice;
                }
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
}
