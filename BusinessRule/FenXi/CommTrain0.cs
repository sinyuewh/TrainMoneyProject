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
    /// 车厢类型的枚举
    /// </summary>
    public enum ECheXian
    {
        硬座, 软座,
        开放式硬卧, 包房式硬卧,
        软卧, 高级软卧,
        餐车, 供电车, 宿营车,
        双层硬座,双层软座,
        高级软卧19K,高级软卧19T
    }

    /// <summary>
    /// 普通车加快的类型
    /// </summary>
    public enum EJiaKuai
    {
        加快, 特快, 特快附加, 其他
    }

    //供电类型
    public enum EGongDianType
    {
        直供电, 非直供电
    }

    //牵引类型
    public enum EQianYinType
    {
        电力机车, 内燃机车,其他
    }

    /// <summary>
    /// 普通列车的描述
    /// </summary>
    public partial class CommTrain:Train
    {
        #region 构造函数和属性设置
        //购置函数
        public CommTrain()
        {
            this.TrainBigKind = ETrainBigKind.普通列车;
        }

        //普通车的类型
        private ECommTrainType trainType = ECommTrainType.空调车25K;
        public ECommTrainType TrainType
        {
            get { return this.trainType; }
            set 
            { 
                this.trainType = value;
                this.WaterCount = 3;

                if (value == ECommTrainType.空调车25K)
                {
                    this.JiaKuai = EJiaKuai.加快;
                    this.ServerPerson = EServerPerson.二人3车;
                }
                else if (value == ECommTrainType.空调车25T)
                {
                    this.JiaKuai = EJiaKuai.特快;
                    this.WaterCount = 1;
                    this.JnRate = 0.5;
                    this.ServerPerson = EServerPerson.一人2车;
                }
                else if (value == ECommTrainType.空调车25G)
                {
                    this.jiaKuai = EJiaKuai.特快;
                    this.ServerPerson = EServerPerson.二人3车;
                }
                else if (value == ECommTrainType.绿皮车25B)
                {
                    this.KongTiaoFlag = false;
                    this.jiaKuai = EJiaKuai.其他;
                    this.ServerPerson = EServerPerson.一人1车;
                    this.GongDianType = EGongDianType.非直供电;
                }
            }
        }

        //席别增减费
        public int XieBieZhengJiaFee { get; set; }

        //列车加快
        private EJiaKuai jiaKuai = EJiaKuai.其他;
        public EJiaKuai JiaKuai
        {
            get { return this.jiaKuai; }
            set { this.jiaKuai = value; }
        }


        //空调费标志
        private  bool kongTiaoFlag = true;
        public bool KongTiaoFlag
        {
            get { return this.kongTiaoFlag; }
            set { this.kongTiaoFlag = value; }
        }

        #region 车厢编组
        //硬座数量
        private int yinZuo = 8;
        public int YinZuo
        {
            get { return this.yinZuo; }
            set { this.yinZuo = value; }
        }

        //软座数量
        public int RuanZuo { get; set; }               

        //硬卧的数量
        private int openYinWo = 8;
        public int OpenYinWo
        {
            get { return this.openYinWo; }
            set { this.openYinWo = value; }
        }

        //包方式硬卧数量
        public int CloseYinWo { get; set; }             

        //软卧的数量
        private int ruanWo = 1;
        public int RuanWo
        {
            get { return this.ruanWo; }
            set { this.ruanWo = value; }
        }

        //高级软卧数量
        public int AdvanceRuanWo { get; set; }

        public int RuanWo19K { get; set; }
        public int RuanWo19T { get; set; }
        public int SYinZuo { get; set; }
        public int SRuanZuo { get; set; }

        //餐车的数量
        private int canChe = 1;
        public int CanChe
        {
            get { return this.canChe; }
            set { this.canChe = value; }
        }

        //全程内燃机车的标志
        public bool FullNeiRangChe { get; set; }   

        //发电车数量
        public int FaDianChe { get; set; }

        //宿营车数量
        public int ShuYinChe { get; set; }      
       

        //车厢的单价
        public double YinZuoPrice { get; set; }
        public double RuanZuoPrice { get; set; }
        public double OpenYinWoPrice { get; set; }
        public double CloseYinWoPrice { get; set; }
        public double RuanWoPrice { get; set; }
        public double AdvanceRuanWoPrice { get; set; }
        public double CanChePrice { get; set; }
        public double FaDianChePrice { get; set; }
        public double ShuYinChePrice { get; set; } 
        #endregion

        //供电类型
        public EGongDianType GongDianType { get; set; }

        //运行里程
        public override int YunXingLiCheng
        {
            get
            {
                return base.YunXingLiCheng;
            }
            set
            {
                base.YunXingLiCheng = value;
            }
        }
        #endregion

        //得到列车的总收入
        public override double GetShouRu()
        {
            double Fee = 0;
            TrainShouRu1 shour1 = null;
             if (this.trainType == ECommTrainType.绿皮车25B)
            {
                shour1 = new TrainShouRu1(this.YunXingLiCheng,
                    this.KongTiaoFlag, this.JiaKuai);
            }
            else
            {
                shour1 = new TrainShouRu2(this.YunXingLiCheng,this.jiaKuai);
            }

            double price1 = 0;
            if (this.YinZuo > 0)
            {
                price1 = shour1.YinZuoPrice + shour1.GetKongTiaoFee(this.trainType,ECommCheXian.硬座) + shour1.JiaKuaiFee;
                Fee = Fee + this.YinZuo * price1 * ChexianBianZhuData.YinZuo_Pcount;
            }
            if (this.RuanZuo > 0)
            {
                price1 = shour1.RuanZuoPrice + shour1.GetKongTiaoFee(this.trainType, ECommCheXian.软座) + shour1.JiaKuaiFee;
                Fee = Fee + this.RuanZuo * price1 * ChexianBianZhuData.RuanZuo_Pcount;
            }

            if (this.OpenYinWo > 0) //包括上中下
            {
                price1 = shour1.YinZuoPrice + shour1.GetKongTiaoFee(this.trainType, ECommCheXian.开放式硬卧) +
                    shour1.JiaKuaiFee+shour1.YinWoPrice1;
                Fee = Fee + this.openYinWo * price1 * ChexianBianZhuData.YinWo1_Pcount;
                Fee = Fee + this.ShuYinChe * price1 * (TrainProfile.SyCheXianPCount / 3);  //增加宿营车
                

                price1 = shour1.YinZuoPrice + shour1.GetKongTiaoFee(this.trainType, ECommCheXian.开放式硬卧)
                    + shour1.JiaKuaiFee + shour1.YinWoPrice2;
                Fee = Fee + this.openYinWo * price1 * ChexianBianZhuData.YinWo2_Pcount;
                Fee = Fee + this.ShuYinChe * price1 * (TrainProfile.SyCheXianPCount / 3);  //增加宿营车


                price1 = shour1.YinZuoPrice + shour1.GetKongTiaoFee(this.trainType, ECommCheXian.开放式硬卧) 
                    + shour1.JiaKuaiFee + shour1.YinWoPrice3;
                Fee = Fee + this.openYinWo * price1 * ChexianBianZhuData.YinWo3_Pcount;
                Fee = Fee + this.ShuYinChe * price1 * (TrainProfile.SyCheXianPCount / 3);  //增加宿营车
            }

            if (this.RuanWo > 0)   //包括上铺和下铺
            {
                price1 = shour1.RuanZuoPrice
                    + shour1.GetKongTiaoFee(this.trainType, ECommCheXian.软卧) + shour1.JiaKuaiFee + shour1.RuanWoPrice1;
                Fee = Fee + this.RuanWo * price1 * ChexianBianZhuData.RuanWo1_Pcount;

                price1 = shour1.RuanZuoPrice + shour1.GetKongTiaoFee(this.trainType, ECommCheXian.软卧) 
                    + shour1.JiaKuaiFee + shour1.RuanWoPrice2;
                Fee = Fee + this.RuanWo * price1 * ChexianBianZhuData.RuanWo2_Pcount;
            }

            if (this.AdvanceRuanWo > 0)  //包括上铺和下铺
            {
                price1 = shour1.RuanZuoPrice + shour1.GetKongTiaoFee(this.trainType, ECommCheXian.高级软卧)
                    + shour1.JiaKuaiFee + shour1.AdvenceRuanWoPrice1;
                Fee = Fee + this.AdvanceRuanWo * price1 * ChexianBianZhuData.GaoJiRuanWo1_Pcount;

                price1 = shour1.RuanZuoPrice + shour1.GetKongTiaoFee(this.trainType, ECommCheXian.高级软卧) 
                    + shour1.JiaKuaiFee + shour1.AdvanceRuanWoPrice2;
                Fee = Fee + this.AdvanceRuanWo * price1 * ChexianBianZhuData.GaoJiRuanWo2_Pcount;
            }

            //改为按年还是按趟
            double shouru = 0;
            if (this.IsYearFlag)
            {
                shouru = Fee * 2 * 365;
            }
            else
            {
                shouru = Fee * 2 ;
            }

            return JMath.Round1(shouru,this.XiaoShou);
        }

        /// <summary>
        /// 自动计算收入和支出最大化的编组
        /// </summary>
        /// <param name="line">线路</param>
        /// <param name="shouru"></param>
        /// <param name="yz"></param>
        /// <param name="yw"></param>
        /// <param name="rw"></param>
        /// <returns></returns>
        public List<ZhiChuData> GetShouRuAndZhiChuByGoodBianZhu(
            int totalBianZhu,
            out double shouru,
            out int yz, 
            out int yw, 
            out int rw,
            bool hasDianChe,
            String FindCond)
        {
            yz = 0;
            yw = 0;
            rw = 0;

            int yz0 = 0; int yz1 = 0;
            int yw0 = 0; int yw1 = 0;
            int rw0 = 0; int rw1 = 0;
            shouru = 0;
            int minRuanWo = 0;

            double mzc1 = 0, mzc2 = 0, mzc3 = 0, mzc4 = 0, mzc5 = 0, mzc6 = 0,
                mzc7 = 0, mzc8 = 0, mzc9 = 0, mzc10 = 0, mzc11 = 0, mzc12 = 0,
                mzc13 = 0, mzc14 = 0,mzc15=0,mzc16=0;

            if (this.trainType == ECommTrainType.空调车25G)
            {
                yz0 = BZData.Train25G[0]; yz1 = BZData.Train25G[1];
                yw0 = BZData.Train25G[2]; yw1 = BZData.Train25G[3];
                rw0 = BZData.Train25G[4]; rw1 = BZData.Train25G[5];
                minRuanWo = rw0;
            }
            else if (trainType == ECommTrainType.空调车25K)
            {
                yz0 = BZData.Train25K[0]; yz1 = BZData.Train25K[1];
                yw0 = BZData.Train25K[2]; yw1 = BZData.Train25K[3];
                rw0 = BZData.Train25K[4]; rw1 = BZData.Train25K[5];
                minRuanWo = rw0;
            }
            else if (trainType == ECommTrainType.空调车25T)
            {
                yz0 = BZData.Train25T[0]; yz1 = BZData.Train25T[1];
                yw0 = BZData.Train25T[2]; yw1 = BZData.Train25T[3];
                rw0 = BZData.Train25T[4]; rw1 = BZData.Train25T[5];
                minRuanWo = rw0;
            }
            else if (trainType == ECommTrainType.绿皮车25B)
            {
                yz0 = BZData.Train25B[0]; yz1 = BZData.Train25B[1];
                yw0 = BZData.Train25B[2]; yw1 = BZData.Train25B[3];
                rw0 = BZData.Train25B[4]; rw1 = BZData.Train25B[5];
                minRuanWo = rw0;
            }

            //比较的初值
            double good = -999999999999;
           
            //计算不同的支出
            double z1 = this.GetFee1();         //线路使用费
            double z6 = this.GetFee6();         //列车上水费
            int dianchecount = 0;               //表示是否有电车
            if (hasDianChe)
            {
                dianchecount = 1;
                this.FaDianChe = dianchecount;
                this.GongDianType = EGongDianType.非直供电;   //表示有发电车
            }


            int total1 = totalBianZhu - 1;          //表示有一个是餐车

            //编组总数不是18的调整
            if (totalBianZhu < 15)
            {
                yz0 = 1; yz1 = total1 - dianchecount;
                yw0 = 1; minRuanWo = 0;
            }

            for (int i = yz0; i <= yz1; i++)
            {
                int temp = Math.Min(yw1, total1 - i - dianchecount - minRuanWo);
                
                for (int j = yw0; j <= temp; j++)
                {
                    int k = total1 - i - j - dianchecount;
                    if (k < 0){ break; }

                    this.YinZuo = i;
                    this.OpenYinWo = j;
                    this.RuanWo = k;
                    
                    double sr0 = this.GetShouRu();

                    //计算不同的支出
                    double z2 = this.GetFee2();
                    double z3 = this.GetFee3();
                    double z4 = this.GetFee4();
                    double z5 = this.GetFee5();

                    double z7 = this.GetFee7();
                    double z8 = this.GetFee8();
                    double z9 = this.GetFee9();
                    double z10 = this.GetFee10();

                    double z11 = this.GetFee11();
                    double z12 = this.GetFee12();
                    double z13 = this.GetFee13();
                    double z14 = this.GetFee14();
                    double z15 = this.GetFee15();
                    double z16 = this.GetFee16();
                   
                    //double sr0 = 0;
                    double zc0 = z1 + z2 + z3 + z4 + z5 + z6 + z7 + z8 + z9 + z10 + z11 + z12 + z13 + z14+z15+z16;

                    if (sr0 - zc0 >= good)
                    {
                        good = sr0 - zc0;
                        yz = i;
                        yw = j;
                        rw = k;
                        mzc1 = z1; mzc2 = z2; mzc3 = z3; mzc4 = z4; 
                        mzc5 = z5; mzc6 = z6;mzc7 = z7; mzc8 = z8; 
                        mzc9 = z9; mzc10 = z10; mzc11 = z11;
                        mzc12 = z12; mzc13 = z13; mzc14 = z14; mzc15 = z15; mzc16 = z16;

                        shouru = sr0;
                    }
                }
            }

            //返回支出的数据
            List<ZhiChuData> zhichu = new List<ZhiChuData>();

            String[] findcondarray = FindCond.Replace("&&", "&").Split('&');

            if (FindCond.ToString().Trim() == "")
            {
                zhichu.Add(new ZhiChuData(1, ((EFeeKind)1).ToString(), mzc1));
                zhichu.Add(new ZhiChuData(2, ((EFeeKind)2).ToString(), mzc2));
                zhichu.Add(new ZhiChuData(3, ((EFeeKind)3).ToString(), mzc3));
                zhichu.Add(new ZhiChuData(4, ((EFeeKind)4).ToString(), mzc4));
                zhichu.Add(new ZhiChuData(5, ((EFeeKind)5).ToString(), mzc5));
                zhichu.Add(new ZhiChuData(6, ((EFeeKind)6).ToString(), mzc6));
                zhichu.Add(new ZhiChuData(7, ((EFeeKind)7).ToString(), mzc7));
                zhichu.Add(new ZhiChuData(8, ((EFeeKind)8).ToString(), mzc8));
                zhichu.Add(new ZhiChuData(9, ((EFeeKind)9).ToString(), mzc9));
                zhichu.Add(new ZhiChuData(10, ((EFeeKind)10).ToString(), mzc10));
                zhichu.Add(new ZhiChuData(11, ((EFeeKind)11).ToString(), mzc11));
                zhichu.Add(new ZhiChuData(12, ((EFeeKind)12).ToString(), mzc12));
                zhichu.Add(new ZhiChuData(13, ((EFeeKind)13).ToString(), mzc13));
                zhichu.Add(new ZhiChuData(14, ((EFeeKind)14).ToString(), mzc14));
                zhichu.Add(new ZhiChuData(15, ((EFeeKind)15).ToString(), mzc15));
                zhichu.Add(new ZhiChuData(16, ((EFeeKind)16).ToString(), mzc16));
            }
            else
            {
                int i = 0;

                for (String arrayitem = findcondarray[i]; i < findcondarray.Length;++i)
                {
                    arrayitem = findcondarray[i];

                    switch (arrayitem)
                    {
                        case "1":
                            zhichu.Add(new ZhiChuData(1, ((EFeeKind)1).ToString(), mzc1));
                            break;

                        case "2":
                            zhichu.Add(new ZhiChuData(2, ((EFeeKind)2).ToString(), mzc2));
                            break;

                        case "3":
                            zhichu.Add(new ZhiChuData(3, ((EFeeKind)3).ToString(), mzc3));
                            break;

                        case "4":
                            zhichu.Add(new ZhiChuData(4, ((EFeeKind)4).ToString(), mzc4));
                            break;

                        case "5":
                            zhichu.Add(new ZhiChuData(5, ((EFeeKind)5).ToString(), mzc5));
                            break;

                        case "6":
                            zhichu.Add(new ZhiChuData(6, ((EFeeKind)6).ToString(), mzc6));
                            break;

                        case "7":
                            zhichu.Add(new ZhiChuData(7, ((EFeeKind)7).ToString(), mzc7));
                            break;

                        case "8":
                            zhichu.Add(new ZhiChuData(8, ((EFeeKind)8).ToString(), mzc8));
                            break;

                        case "9":
                            zhichu.Add(new ZhiChuData(9, ((EFeeKind)9).ToString(), mzc9));
                            break;

                        case "10":
                            zhichu.Add(new ZhiChuData(10, ((EFeeKind)10).ToString(), mzc10));
                            break;

                        case "11":
                            zhichu.Add(new ZhiChuData(11, ((EFeeKind)11).ToString(), mzc11));
                            break;

                        case "12":
                            zhichu.Add(new ZhiChuData(12, ((EFeeKind)12).ToString(), mzc12));
                            break;

                        case "13":
                            zhichu.Add(new ZhiChuData(13, ((EFeeKind)13).ToString(), mzc13));
                            break;

                        case "14":
                            zhichu.Add(new ZhiChuData(14, ((EFeeKind)14).ToString(), mzc14));
                            break;

                        case "15":
                            zhichu.Add(new ZhiChuData(15, ((EFeeKind)15).ToString(), mzc15));
                            break;

                        case "16":
                            zhichu.Add(new ZhiChuData(16, ((EFeeKind)16).ToString(), mzc16));
                            break;

                        default:
                            break;
                    }
                }
            }

            return zhichu;
        }


        public List<ZhiChuData> GetShouRuAndZhiChu(
            out double shouru,
            int yz,
            int yw,
            int rw,
            int sy,               //宿营车
            int ca,               //餐车
            bool hasDianChe,
            String FindCond)
          {
            
            shouru = 0;
            this.YinZuo = yz;
            this.OpenYinWo = yw;
            this.RuanWo = rw;
            this.canChe = ca;
            this.ShuYinChe = sy;

            if (hasDianChe)
            {
                this.FaDianChe = 1;
                this.GongDianType = EGongDianType.非直供电;  
            }

            shouru = this.GetShouRu();
            double z1 = this.GetFee1();         
            double z2 = this.GetFee2();
            double z3 = this.GetFee3();
            double z4 = this.GetFee4();
            double z5 = this.GetFee5();
            double z6 = this.GetFee6();
            double z7 = this.GetFee7();
            double z8 = this.GetFee8();
            double z9 = this.GetFee9();
            double z10 = this.GetFee10();

            double z11 = this.GetFee11();
            double z12 = this.GetFee12();
            double z13 = this.GetFee13();
            double z14 = this.GetFee14();
            double z15 = this.GetFee15();
            double z16 = this.GetFee16();

            //返回支出的数据
            List<ZhiChuData> zhichu = new List<ZhiChuData>();
            String[] findcondarray = FindCond.Replace("&&", "&").Split('&');

            int i = 0;

            if (FindCond.ToString().Trim() == "")
            {
                zhichu.Add(new ZhiChuData(1, ((EFeeKind)1).ToString(), z1));
                zhichu.Add(new ZhiChuData(2, ((EFeeKind)2).ToString(), z2));
                zhichu.Add(new ZhiChuData(3, ((EFeeKind)3).ToString(), z3));
                zhichu.Add(new ZhiChuData(4, ((EFeeKind)4).ToString(), z4));
                zhichu.Add(new ZhiChuData(5, ((EFeeKind)5).ToString(), z5));
                zhichu.Add(new ZhiChuData(6, ((EFeeKind)6).ToString(), z6));
                zhichu.Add(new ZhiChuData(7, ((EFeeKind)7).ToString(), z7));
                zhichu.Add(new ZhiChuData(8, ((EFeeKind)8).ToString(), z8));
                zhichu.Add(new ZhiChuData(9, ((EFeeKind)9).ToString(), z9));
                zhichu.Add(new ZhiChuData(10, ((EFeeKind)10).ToString(), z10));
                zhichu.Add(new ZhiChuData(11, ((EFeeKind)11).ToString(), z11));
                zhichu.Add(new ZhiChuData(12, ((EFeeKind)12).ToString(), z12));
                zhichu.Add(new ZhiChuData(13, ((EFeeKind)13).ToString(), z13));
                zhichu.Add(new ZhiChuData(14, ((EFeeKind)14).ToString(), z14));
                zhichu.Add(new ZhiChuData(15, ((EFeeKind)15).ToString(), z15));
                zhichu.Add(new ZhiChuData(16, ((EFeeKind)16).ToString(), z16));
            }
            else
            {
                for (String arrayitem = findcondarray[i]; i < findcondarray.Length;++i)
                {
                    arrayitem = findcondarray[i];

                    switch (arrayitem)
                    {
                        case "1":
                            zhichu.Add(new ZhiChuData(1, ((EFeeKind)1).ToString(), z1));
                            break;

                        case "2":
                            zhichu.Add(new ZhiChuData(2, ((EFeeKind)2).ToString(), z2));
                            break;

                        case "3":
                            zhichu.Add(new ZhiChuData(3, ((EFeeKind)3).ToString(), z3));
                            break;

                        case "4":
                            zhichu.Add(new ZhiChuData(4, ((EFeeKind)4).ToString(), z4));
                            break;

                        case "5":
                            zhichu.Add(new ZhiChuData(5, ((EFeeKind)5).ToString(), z5));
                            break;

                        case "6":
                            zhichu.Add(new ZhiChuData(6, ((EFeeKind)6).ToString(), z6));
                            break;

                        case "7":
                            zhichu.Add(new ZhiChuData(7, ((EFeeKind)7).ToString(), z7));
                            break;

                        case "8":
                            zhichu.Add(new ZhiChuData(8, ((EFeeKind)8).ToString(), z8));
                            break;

                        case "9":
                            zhichu.Add(new ZhiChuData(9, ((EFeeKind)9).ToString(), z9));
                            break;

                        case "10":
                            zhichu.Add(new ZhiChuData(10, ((EFeeKind)10).ToString(), z10));
                            break;

                        case "11":
                            zhichu.Add(new ZhiChuData(11, ((EFeeKind)11).ToString(), z11));
                            break;

                        case "12":
                            zhichu.Add(new ZhiChuData(12, ((EFeeKind)12).ToString(), z12));
                            break;

                        case "13":
                            zhichu.Add(new ZhiChuData(13, ((EFeeKind)13).ToString(), z13));
                            break;

                        case "14":
                            zhichu.Add(new ZhiChuData(14, ((EFeeKind)14).ToString(), z14));
                            break;

                        case "15":
                            zhichu.Add(new ZhiChuData(15, ((EFeeKind)15).ToString(), z15));
                            break;

                        case "16":
                            zhichu.Add(new ZhiChuData(16, ((EFeeKind)16).ToString(), z16));
                            break;

                        default:
                            break;
                    }
                }
            }

            return zhichu;
        }


        //得到固定编组的收入和支出
        public List<ZhiChuData> GetShouRuAndZhiChu(
           out double shouru)
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
            double zc15 = this.GetFee15();
            double zc16 = this.GetFee16();

            //返回支出的数据
            List<ZhiChuData> zhichu = new List<ZhiChuData>();
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
            zhichu.Add(new ZhiChuData(15, ((EFeeKind)15).ToString(), zc15));
            zhichu.Add(new ZhiChuData(16, ((EFeeKind)16).ToString(), zc15));
            return zhichu;
        }

        #region 其他方法
        // 得到车厢的满员人数
        private static int GetCheXianTotalPerson(ECheXian type1)
        {
            int d1 = 0;
            switch (type1)
            {
                case ECheXian.包房式硬卧:
                    d1 = ChexianBianZhuData.YinWo4_Pcount 
                        + ChexianBianZhuData.YinWo5_Pcount;
                    break;
                case ECheXian.餐车:
                    d1 = 0;
                    break;
                case ECheXian.高级软卧:
                    d1 = ChexianBianZhuData.GaoJiRuanWo1_Pcount 
                        + ChexianBianZhuData.GaoJiRuanWo2_Pcount;
                    break;
                case ECheXian.供电车:
                    d1 = 0;
                    break;
                case ECheXian.开放式硬卧:
                    d1 = ChexianBianZhuData.YinWo1_Pcount
                        + ChexianBianZhuData.YinWo2_Pcount 
                        + ChexianBianZhuData.YinWo3_Pcount;
                    break;
                case ECheXian.软卧:
                    d1 = ChexianBianZhuData.RuanWo1_Pcount 
                        + ChexianBianZhuData.RuanWo2_Pcount;
                    break;
                case ECheXian.宿营车:
                    d1 = 0;
                    break;
                case ECheXian.硬座:
                    d1 = ChexianBianZhuData.YinZuo_Pcount;
                    break;
                case ECheXian.软座:
                    d1 = ChexianBianZhuData.RuanZuo_Pcount;
                    break;
                default:
                    break;
            }
            return d1;
        }        
        #endregion

    }
}
