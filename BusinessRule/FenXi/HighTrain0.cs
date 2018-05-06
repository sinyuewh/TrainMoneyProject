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
    /// 动车的类型定义
    /// </summary>
    public enum EHighTrainType
    {
        CRH2A = 4, CRH2E, CRH2C, CRH380A, CRH380AL, CRH2B, CRH5A,CRH380B, CRH380BL
    }

    /// <summary>
    /// 动车编组
    /// </summary>
    public enum EHighTrainBianZhu
    {
        单组, 重联
    }

    /// <summary>
    /// 动车的大类
    /// </summary>
    public enum EHighTrainBigKind
    {
        动车200公里=1, 动车300公里
    }

    /// <summary>
    /// 动车的类
    /// </summary>
    public partial class HighTrain:Train
    {
        private const double SFRATE = 1.1;
        private const double Rate300 = 0.9;

        #region 构造函数和属性设置
        public HighTrain()
        {
            this.TrainBigKind = ETrainBigKind.动车;
            this.WaterCount = 1;
            this.CheDiShu = 1;
        }

        public HighTrain(EHighTrainType trainType1)
        {
            this.TrainBigKind = ETrainBigKind.动车;
            this.WaterCount = 1;
            this.TrainType = trainType1;
            this.CheDiShu = 1;
        }

        //动车类型
        private EHighTrainType trainType = EHighTrainType.CRH2A;
        public EHighTrainType TrainType
        {
            get{ return this.trainType;}
            set { 
                this.trainType = value;
                if (value == EHighTrainType.CRH2C
                    || value == EHighTrainType.CRH380A
                    || value == EHighTrainType.CRH380AL
                    || value == EHighTrainType.CRH380B
                    || value == EHighTrainType.CRH380BL)   //增加了动车的车型 CRH380B和CRH380BL
                {
                    this.HighTrainBigKind = EHighTrainBigKind.动车300公里;  //动车的大类
                }
                else
                {
                    this.HighTrainBigKind = EHighTrainBigKind.动车200公里;
                }

                //动卧的默认设置为重联
                if (value == EHighTrainType.CRH2B
                    || value == EHighTrainType.CRH2E
                    || value == EHighTrainType.CRH380AL
                    || value == EHighTrainType.CRH380BL )
                {
                    this.BianZhu = EHighTrainBianZhu.重联;
                    this.isDongWu = true;
                }
            }
        }

        private bool isDongWu = false;   //表示是否为动卧

        //动车票价的折扣
        private double discount = 0;
        public double Discount
        {
            get { return this.discount; }
            set { this.discount = value; }
        }

        //动车编组
        private EHighTrainBianZhu bianzhu = EHighTrainBianZhu.单组;
        public EHighTrainBianZhu BianZhu
        {
            get { return this.bianzhu; }
            set { this.bianzhu = value; }
        }

        //动车大类
        private EHighTrainBigKind highTrainBigKind = EHighTrainBigKind.动车200公里;
        public EHighTrainBigKind HighTrainBigKind
        {
            get { return this.highTrainBigKind; }
            private set { this.highTrainBigKind = value; }
        }
        #endregion

        //计算动车的年收入
        public override double GetShouRu()
        {
            double Fee = 0;
            DataTable dt = HighTrainProfile.Data;
            DataRow[] drs = dt.Select("HighTrainType='" + this.TrainType.ToString() + "'");
            if (drs != null && drs.Length > 0)
            {
                DataRow dr1 = drs[0];
                if (dr1 != null)
                {
                    double price1 = 0;          //一等软座价格
                    double price2 = 0;          //二等软座价格
                    double price3 = 0;          //动卧上铺价格
                    double price4 = 0;          //动卧下铺价格
                    double price5 = 0;          //商务座价格
                    double price6 = 0;          //特等座价格

                    double pc1 = 0;             //一等软座人数
                    double pc2 = 0;             //二等软座人数
                    double pc3 = 0;             //动卧上铺人数
                    double pc4 = 0;             //动卧下铺人数
                    double pc5 = 0;             //商务座人数
                    double pc6 = 0;             //特等座人数

                    double baoxian = 0;         //保险费价格

                    //计算保险费
                    baoxian = JMath.Round1(TrainProfile.BaoXianFee * TrainProfile.BaseFee * this.YunXingLiCheng,1);

                    //计算票价
                    price1 = DTrainShouRu.GetTrainPrice(this.trainType, EDTrainFareType.一等软座, this.Line, this.YunXingLiCheng);
                    price2 = DTrainShouRu.GetTrainPrice(this.trainType, EDTrainFareType.二等软座, this.Line, this.YunXingLiCheng);
                    price3 = DTrainShouRu.GetTrainPrice(this.trainType, EDTrainFareType.动卧上铺, this.Line, this.YunXingLiCheng);
                    price4 = DTrainShouRu.GetTrainPrice(this.trainType, EDTrainFareType.动卧下铺, this.Line, this.YunXingLiCheng);
                    price5 = DTrainShouRu.GetTrainPrice(this.trainType, EDTrainFareType.商务座, this.Line, this.YunXingLiCheng);
                    price6 = DTrainShouRu.GetTrainPrice(this.trainType, EDTrainFareType.特定座,this.Line, this.YunXingLiCheng);
                   
                    
                    //计算人数
                    if (dr1["PCOUNT1"].ToString().Trim() != String.Empty)
                    {
                        pc1 = double.Parse(dr1["PCOUNT1"].ToString());
                    }

                    if (dr1["PCOUNT2"].ToString().Trim() != String.Empty)
                    {
                        pc2 = double.Parse(dr1["PCOUNT2"].ToString());
                    }

                    if (dr1["PCOUNT3"].ToString().Trim() != String.Empty)
                    {
                        pc3 = double.Parse(dr1["PCOUNT3"].ToString()) / 2;
                    }
                    pc4 = pc3;

                    if (dr1["PCOUNT4"].ToString().Trim() != String.Empty)
                    {
                        pc5 = double.Parse(dr1["PCOUNT4"].ToString());
                    }

                    if (dr1["PCOUNT5"].ToString().Trim() != String.Empty)
                    {
                        pc6 = double.Parse(dr1["PCOUNT5"].ToString());
                    }

                    Fee = Fee + price1 * pc1 + price2 * pc2 + price3 * pc3 + price4 * pc4 + price5 * pc5+price6*pc6;
                }
            }

            if (this.BianZhu == EHighTrainBianZhu.重联
                && this.isDongWu == false)
            {
                Fee = Fee * 2;
            }

            //计算票价的收入
            if (this.IsYearFlag)
            {
                Fee = Fee * (1 - this.Discount / 100) * 2 * 365;
            }
            else
            {
                Fee = Fee * (1 - this.Discount / 100) * 2;
            }

            return JMath.Round1(Fee, this.XiaoShou);
        }

        //得到固定编组的收入和支出
        public List<ZhiChuData> GetShouRuAndZhiChu(
           out double shouru,String FindCond)
        {
            shouru = 0;

            //计算收入
            shouru = this.GetShouRu();

            //计算不同的支出
            double zc1 = this.GetFee1();
            double zc2 = this.GetFee2();
            double zc3 = this.GetFee3();
            double zc4 = this.GetFee4();
            double zc5 = this.GetFee5();
            double zc6 = this.GetFee6();

            double zc7 = this.GetFee7();
            double zc8 = this.GetFee8();
            double zc9 = this.GetFee9();
            double zc10 = this.GetFee10();

            double zc11 = this.GetFee11();
            double zc12 = this.GetFee12();
            double zc13 = this.GetFee13();
            double zc14 = this.GetFee14();
            double zc16 = this.GetFee16();

            //返回支出的数据
            List<ZhiChuData> zhichu = new List<ZhiChuData>();
            String[] findcondarray = FindCond.Replace("&&", "&").Split('&');

            int i = 0;

            if (FindCond.ToString().Trim() == "")
            {
                zhichu.Add(new ZhiChuData(1, ((EFeeKind)1).ToString(), zc1));
                zhichu.Add(new ZhiChuData(2, ((EFeeKind)2).ToString(), zc2));
                zhichu.Add(new ZhiChuData(3, ((EFeeKind)3).ToString(), zc3));
                zhichu.Add(new ZhiChuData(4, ((EFeeKind)4).ToString(), zc4));
                zhichu.Add(new ZhiChuData(5, ((EFeeKind)5).ToString(), zc5));
                zhichu.Add(new ZhiChuData(6, ((EFeeKind)6).ToString(), zc6));
                zhichu.Add(new ZhiChuData(7, ((EFeeKind)7).ToString(), zc7));
                zhichu.Add(new ZhiChuData(8, ((EFeeKind)8).ToString(), zc8));
                zhichu.Add(new ZhiChuData(9, ((EFeeKind)9).ToString(), zc9));
                zhichu.Add(new ZhiChuData(10, ((EFeeKind)10).ToString(), zc10));
                zhichu.Add(new ZhiChuData(11, ((EFeeKind)11).ToString(), zc11));
                zhichu.Add(new ZhiChuData(12, ((EFeeKind)12).ToString(), zc12));
                zhichu.Add(new ZhiChuData(13, ((EFeeKind)13).ToString(), zc13));
                zhichu.Add(new ZhiChuData(14, ((EFeeKind)14).ToString(), zc14));
                zhichu.Add(new ZhiChuData(16, ((EFeeKind)16).ToString(), zc16));
            }
            else
            {
                for (String arrayitem = findcondarray[i]; i < findcondarray.Length; ++i)
                {
                    arrayitem = findcondarray[i];

                    switch (arrayitem)
                    {
                        case "1":
                            zhichu.Add(new ZhiChuData(1, ((EFeeKind)1).ToString(), zc1));
                            break;

                        case "2":
                            zhichu.Add(new ZhiChuData(2, ((EFeeKind)2).ToString(), zc2));
                            break;

                        case "3":
                            zhichu.Add(new ZhiChuData(3, ((EFeeKind)3).ToString(), zc3));
                            break;

                        case "4":
                            zhichu.Add(new ZhiChuData(4, ((EFeeKind)4).ToString(), zc4));
                            break;

                        case "5":
                            zhichu.Add(new ZhiChuData(5, ((EFeeKind)5).ToString(), zc5));
                            break;

                        case "6":
                            zhichu.Add(new ZhiChuData(6, ((EFeeKind)6).ToString(), zc6));
                            break;

                        case "7":
                            zhichu.Add(new ZhiChuData(7, ((EFeeKind)7).ToString(), zc7));
                            break;

                        case "8":
                            zhichu.Add(new ZhiChuData(8, ((EFeeKind)8).ToString(), zc8));
                            break;

                        case "9":
                            zhichu.Add(new ZhiChuData(9, ((EFeeKind)9).ToString(), zc9));
                            break;

                        case "10":
                            zhichu.Add(new ZhiChuData(10, ((EFeeKind)10).ToString(), zc10));
                            break;

                        case "11":
                            zhichu.Add(new ZhiChuData(11, ((EFeeKind)11).ToString(), zc11));
                            break;

                        case "12":
                            zhichu.Add(new ZhiChuData(12, ((EFeeKind)12).ToString(), zc12));
                            break;

                        case "13":
                            zhichu.Add(new ZhiChuData(13, ((EFeeKind)13).ToString(), zc13));
                            break;

                        case "14":
                            zhichu.Add(new ZhiChuData(14, ((EFeeKind)14).ToString(), zc14));
                            break;

                        case "16":
                            zhichu.Add(new ZhiChuData(16, ((EFeeKind)16).ToString(), zc16));
                            break;

                        default:
                            break;
                    }
                }
            }
            return zhichu;
        }


        /// <summary>
        /// 得到动车的折扣计算
        /// </summary>
        /// <returns></returns>
        private double GetDiscount112()
        {
            double result = 0;
            String RateName =String.Empty;

            //300公里的动车
            if (this.trainType == EHighTrainType.CRH2C
                || this.trainType == EHighTrainType.CRH380A
                || this.trainType == EHighTrainType.CRH380AL
                || this.trainType == EHighTrainType.CRH380B
                || this.trainType == EHighTrainType.CRH380BL)   //增加了300公里的车型
            {
                RateName = "Rate3";
            }
            else
            {
                RateName = "Rate2";
                if (this.Line != null)
                {
                    foreach (LineNode node1 in this.Line.Nodes)
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
                condition.Add(new SearchField("LICHENG", this.YunXingLiCheng + "", 
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
