using System;
using System.Collections.Generic;

using System.Text;

namespace BusinessRule
{
    /// <summary>
    /// 计算列车的各项支出
    /// </summary>
    public class TrainFareBack
    {
        #region Private Function
        //判断是否为300公里的动车
        private static bool Is300Train(ETrainType type1)
        {
            if (type1 == ETrainType.动车CRH2C
                || type1 == ETrainType.动车CRH380A
                || type1 == ETrainType.动车CRH380AL
                || type1 == ETrainType.动车CRH380B
                || type1 == ETrainType.动车CRH380BL)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //判断是否为重联的动车
        private static bool IsUnionHighTrain(ETrainType type1)
        {
            if (type1 == ETrainType.动车CRH2B
                    || type1 == ETrainType.动车CRH2E
                    || type1 == ETrainType.动车CRH380AL
                    || type1 ==ETrainType.动车CRH380BL )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //判断是否为普通列车
        private static bool IsCommonTrain(ETrainType type1)
        {
            if (type1 == ETrainType.空调车25G ||
                type1 == ETrainType.空调车25K ||
                type1 == ETrainType.空调车25T ||
                type1 == ETrainType.绿皮车25B)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        //线路使用费
        public static double GetFee1(
            ETrainType type1, 
            bool kongTiao,
            TrainLine line0)
        {
            double fee0 = 0;

            if (line0 != null)
            {
                //普通列车
                if (IsCommonTrain(type1))
                {
                    bool kt = true;
                    if (type1 == ETrainType.绿皮车25B)
                    {
                        kt = kongTiao;
                    }
                    for (int i = 0; i < line0.Nodes.Count; i++)
                    {
                        int index0 = int.Parse(line0.Nodes[i].LineType);
                        double Rate = 0;
                        if (kt == false)
                        {
                            Rate = LineProfile.FeeRate[index0].Fee6;
                        }
                        else
                        {
                            Rate = LineProfile.FeeRate[index0].Fee5;
                        }
                        fee0 = fee0 + Rate * line0.Nodes[i].Miles;
                    }
                }
                else   //动车组
                {
                    EHighTrainBianZhu bianzhu = EHighTrainBianZhu.单组;
                    if (IsUnionHighTrain(type1))
                    {
                        bianzhu = EHighTrainBianZhu.重联;
                    }

                    for (int i = 0; i < line0.Nodes.Count; i++)
                    {
                        int index0 = int.Parse(line0.Nodes[i].LineType);
                        double Rate = 0;
                        if (bianzhu == EHighTrainBianZhu.单组)
                        {
                            if (Is300Train(type1) == false)
                            {
                                Rate = LineProfile.FeeRate[index0].Fee3;
                            }
                            else
                            {
                                Rate = LineProfile.FeeRate[index0].Fee1;
                            }
                        }
                        else
                        {
                            if (Is300Train(type1) == false)
                            {
                                Rate = LineProfile.FeeRate[index0].Fee4;
                            }
                            else
                            {
                                Rate = LineProfile.FeeRate[index0].Fee2;
                            }
                        }
                        fee0 = fee0 + Rate * line0.Nodes[i].Miles;
                    }
                }
            }

            fee0=fee0 * 2 * 365;
            return fee0;
        }

        //线路使用费
        public static double GetFee1(
            ETrainType type1,
            bool kongTiao,
            bool hasDianChe,
            String[] lineStation)
        {
            TrainLine line1 = Line.GetTrainLineByTrainTypeAndLineNoeds(type1,hasDianChe, lineStation);
            if (line1 != null)
            {
                return GetFee1(type1, kongTiao, line1);
            }
            else
            {
                return 0;
            }
        }


        //机车牵引费
        public static double GetFee2(EQianYinType QianYinType,
            EGongDianType GongDianType,
            double TrainWeight, int YunXingLiCheng)
        {
            double Fee = 0;
            if (QianYinType == EQianYinType.内燃机车)
            {
                if (GongDianType == EGongDianType.直供电)
                {
                    Fee = QianYinFeeProfile.Fee01;
                }
                else
                {
                    Fee = QianYinFeeProfile.Fee02;
                }
            }
            else
            {
                if (GongDianType == EGongDianType.直供电)
                {
                    Fee = QianYinFeeProfile.Fee11;
                }
                else
                {
                    Fee = QianYinFeeProfile.Fee12;
                }
            }
            Fee = Fee/10000d * TrainWeight* YunXingLiCheng * 2 * 365; 
            return Fee;
        }
    }
}
