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
    //动车的票价类型
    public enum EDTrainFareType
    {
        一等软座,二等软座,动卧上铺,动卧下铺,商务座,特定座
    }

    //动车的票价收入
    public class DTrainShouRu
    {
        //private static Dictionary<EDTrainFareType ,double> Rate=new Dictionary<EDTrainFareType,double>();
        static DTrainShouRu()
        {
            /*
            Rate[EDTrainFareType.普动二等软座] = 0.2805 ;
            Rate[EDTrainFareType.普动一等软座] = 0.3360 ;
            Rate[EDTrainFareType.普动特等软座] = 0.4208 ;

            Rate[EDTrainFareType.高铁二等软座] = 0.4833 ;
            Rate[EDTrainFareType.高铁一等软座] = 0.7733 ;
            Rate[EDTrainFareType.动卧上铺] = 0.3366*1.1*1.6;
            Rate[EDTrainFareType.动卧上铺] = 0.3366 * 1.1 * 1.8;*/
        }

        //得到动车的价格
        public static double GetTrainPrice(
            EHighTrainType traintype,
            EDTrainFareType type1, 
            TrainLine Line,
            int yunXingLiCheng)
        {
            double SFRate = 1.0;
            double result = 0;
            String stype1 = "CRH2A";
            String srate1 = "Rate1";

            switch (traintype)
            {
                case EHighTrainType.CRH2A:
                    stype1 = "CRH2A";
                    break;
                case EHighTrainType.CRH2B:
                    stype1 = "CRH2B";
                    break;
                case EHighTrainType.CRH2C:
                    stype1 = "CRH2C";
                    break;
                case EHighTrainType.CRH2E:
                    stype1 = "CRH2E";
                    break;

                case EHighTrainType.CRH380A:
                    stype1 = "CRH380A";
                    break;
                case EHighTrainType.CRH380AL:
                    stype1 = "CRH380AL";
                    break;

                //增加动车车型的影响（2013年3月13日）
                case EHighTrainType.CRH380B:
                    stype1 = "CRH380B";
                    break;
                case EHighTrainType.CRH380BL:
                    stype1 = "CRH380BL";
                    break;

                case EHighTrainType.CRH5A:
                    stype1 = "CRH5A";
                    break;
            }

            switch(type1)
            {
                case EDTrainFareType.一等软座:
                    srate1 = "Rate1";
                    break;
                case EDTrainFareType.二等软座:
                    srate1 = "Rate2";
                    break;
                case EDTrainFareType.动卧上铺:
                    srate1 = "Rate3";
                    break;
                case EDTrainFareType.动卧下铺:
                    srate1 = "Rate31";
                    break;

                case EDTrainFareType.商务座:
                    srate1 = "Rate4";
                    break;

                case EDTrainFareType.特定座:
                    srate1 = "Rate5";
                    break;
            }

            //重新计算票价（300公里高速在非高速线上按200公里的计算）
            //修改日期：2013年
            //Author：Jin ShouJi   
            if (Line != null && Line.Nodes != null)
            {
                foreach (LineNode node1 in Line.Nodes)
                {
                    int miles1 = node1.Miles;
                    String lineType1 = node1.LineType;
                    String stype2 = stype1;
                    if ((stype2 == "CRH380A" ||
                        stype2 == "CRH380AL" ||
                        stype2 == "CRH380B" ||
                        stype2 == "CRH380BL" ||
                        stype2 == "CRH2C") && lineType1 != "1")
                    {
                        stype2 = "CRH2A";
                    }

                    DataRow[] drs = HighTrainProfile.Data.Select("HIGHTRAINTYPE='" + stype2 + "'");
                    if (drs != null && drs.Length > 0)
                    {
                        DataRow dr = drs[0];
                        if (dr[srate1].ToString().Trim() != String.Empty)
                        {
                            double rate0 = double.Parse(dr[srate1].ToString());
                            result = result + miles1 * rate0 * SFRate;
                        }
                    }
                }
            }

            /*
            //老的计算方法
            DataRow[] drs= HighTrainProfile.Data.Select("HIGHTRAINTYPE='" + stype1 + "'");
            if (drs != null && drs.Length > 0)
            {
                DataRow dr = drs[0];
                if (dr[srate1].ToString().Trim() != String.Empty)
                {
                    double rate0 = double.Parse(dr[srate1].ToString());
                    result = yunXingLiCheng * rate0 * SFRate;
                }
            }*/


            //计算保险费
            double baoxian = JMath.Round1(TrainProfile.BaoXianFee * TrainProfile.BaseFee *yunXingLiCheng, 1);
            if (result == 0) baoxian = 0;
            result =JMath.Round1(result + baoxian,0);

            double discount = GetDiscount(traintype,Line,yunXingLiCheng);
            result =JMath.Round1(result * (1 - discount),0);
            return result;
        }

       
        /// <summary>
        /// 得到动车的折扣计算
        /// </summary>
        /// <returns></returns>
        private static  double GetDiscount(EHighTrainType trainType, 
            TrainLine Line, 
            int YunXingLiCheng)
        {
            double result = 0;
            String RateName = String.Empty;

            //300公里的动车
            if (trainType == EHighTrainType.CRH2C
                || trainType == EHighTrainType.CRH380A
                || trainType == EHighTrainType.CRH380AL
                || trainType == EHighTrainType.CRH380B
                || trainType == EHighTrainType.CRH380BL)   //增加了动车车型的CRH380B和CRH380BL的处理
            {
                RateName = "Rate3";
            }
            else
            {
                RateName = "Rate2";
                if (Line != null)
                {
                    foreach (LineNode node1 in Line.Nodes)
                    {
                        int type1 = int.Parse(node1.LineType);
                        if (type1 > 2)
                        {
                            RateName = "Rate1";   //普通线路
                            break;
                        }
                    }
                }
            }

            //计算动车的折扣
            if (String.IsNullOrEmpty(RateName) == false)
            {
                JTable tab1 = new JTable("HIGHTRAINPRICERATE");
                tab1.OrderBy = "LICHENG desc";
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("LICHENG", YunXingLiCheng + "",
                    SearchOperator.SmallerAndEqual, SearchFieldType.NumericType));
                DataRow dr1 = tab1.GetFirstDataRow(condition, RateName);
                if (dr1 != null)
                {
                    result = double.Parse(dr1[0].ToString());
                    result = result / 100d;
                }
                tab1.Close();
            }
            return result;
        }
    }
}
