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
    /// 普通列车的描述
    /// </summary>
    public partial class CommTrain:Train
    {
        #region 列车支出的各项费用
        //线路使用费 Fee1--OK
        internal override double GetFee1()
        {
            double fee0 = 0;
            double jnfee = 0;
           
            bool kt = true;
            if (this.trainType ==ECommTrainType.绿皮车25B)
            {
                kt = this.kongTiaoFlag;
            }

            if (this.Line != null && this.Line.Nodes.Count > 0)
            {
                for (int i = 0; i < this.Line.Nodes.Count; i++)
                {
                    int index0 = int.Parse(this.Line.Nodes[i].LineType);
                    double Rate = 0;
                    if (kt == false)
                    {
                        Rate = LineProfile.FeeRate[index0].Fee6;
                    }
                    else
                    {
                        Rate = LineProfile.FeeRate[index0].Fee5;
                    }

                    //区分局内费用和非局内费用
                    if (String.IsNullOrEmpty(this.Line.Nodes[i].JnFlag))
                    {
                        fee0 = fee0 + Rate * this.Line.Nodes[i].Miles;
                    }
                    else
                    {
                        jnfee = jnfee + Rate * this.Line.Nodes[i].Miles;
                    }
                }
            }

            //趟乘2，年乘2乘365
            if (this.IsYearFlag)
            {
                fee0 = fee0 * 2 * 365/UnitRate ;
                jnfee = jnfee * 2 * 365/UnitRate ;
            }
            else
            {
                fee0 = fee0 * 2 /UnitRate ;
                jnfee = jnfee * 2/UnitRate  ;
            }


            //数据的显示输出
            this.JnFee = JMath.Round1(jnfee, this.XiaoShou);
            //增加全成本和部分成本
            if (this.IsFullChengBen == false)
            {
                return JMath.Round1(fee0, this.XiaoShou);
            }
            else
            {
                return JMath.Round1(fee0 , this.XiaoShou)
                    +JMath.Round1(this.JnFee,this.XiaoShou);
            }
        }

        //机车牵引费 Fee2--OK
        internal double GetFee2OLD()
        {
            double Fee = 0;
            double Fee0 = 0;

            int aPos = 0;
            int bPos = 0;

            if (this.Line != null && this.Line.Nodes.Count > 0)
            {
                while (aPos < this.Line.Nodes.Count)
                {
                    bPos = -1;
                    for (int j = aPos + 1; j < this.Line.Nodes.Count; j++)
                    {
                        if (this.Line.Nodes[j].BigA)
                        {
                            bPos = j;
                            break;
                        }
                    }
                    if (bPos == -1) { bPos = this.Line.Nodes.Count; }


                    EQianYinType qy1 = EQianYinType.电力机车;
                    for (int k = aPos; k < bPos; k++)
                    {
                        if (this.Line.Nodes[k].DqhFlag.Trim() == String.Empty)
                        {
                            qy1 = EQianYinType.内燃机车;
                            break;
                        }
                    }

                    for (int k = aPos; k < bPos; k++)
                    {
                        this.Line.Nodes[k].QianYinType = qy1;
                    }

                    aPos = bPos;
                }


                foreach (LineNode node1 in this.Line.Nodes)
                {
                    EQianYinType qytype1 = node1.QianYinType;
                    if (FullNeiRangChe)  //表示全程为内燃机车
                    {
                        qytype1 = EQianYinType.内燃机车;
                    }

                    if (qytype1 == EQianYinType.电力机车)
                    {
                        if (this.trainType != ECommTrainType.空调车25T)
                        {
                            Fee = QianYinFeeProfile.Fee02;
                        }
                        else
                        {
                            Fee = QianYinFeeProfile.Fee03;
                        }

                        //增加直供电的附加费计算
                        if (this.GongDianType == EGongDianType.直供电)
                        {
                            Fee = Fee + TrainProfile.QianYinFjFee2;
                        }
                    }
                    else
                    {
                        if (this.trainType != ECommTrainType.空调车25T)
                        {
                            Fee = QianYinFeeProfile.Fee12;
                        }
                        else
                        {
                           Fee = QianYinFeeProfile.Fee13;
                        }

                        //增加直供电的附加费计算
                        if (this.GongDianType == EGongDianType.直供电)
                        {
                            Fee = Fee + TrainProfile.QianYinFjFee1;
                        }
                    }

                   
                    if (this.IsYearFlag)
                    {
                        Fee0 = Fee0 + Fee / 10000d * this.GetTrainWeight() *
                          node1.Miles * 2 * 365;
                    }
                    else
                    {
                        Fee0 = Fee0 + Fee / 10000d * this.GetTrainWeight() *
                         node1.Miles * 2;
                    }
                }
            }

            Fee0 = Fee0 / UnitRate;
            return JMath.Round1(Fee0, this.XiaoShou);

        }

        //机车牵引费 Fee2--OK
        internal override double GetFee2()
        {
            double Fee = 0;
            double Fee0 = 0;

            if (this.Line != null && this.Line.Nodes.Count > 0)
            {
                //设置线路牵引费
                this.Line.SetQianYinFee(trainType, this.GongDianType);
                foreach (LineNode node1 in this.Line.Nodes)
                {
                    EQianYinType qytype1 = node1.QianYinType;
                    if (FullNeiRangChe)  //表示全程为内燃机车
                    {
                        qytype1 = EQianYinType.内燃机车;
                    }

                    if (qytype1 == EQianYinType.电力机车)
                    {
                        Fee = node1.Fee2;
                    }
                    else
                    {
                        Fee = node1.Fee1;
                    }

                    if (this.IsYearFlag)
                    {
                        Fee0 = Fee0 + Fee / 10000d * this.GetTrainWeight() *
                          node1.Miles * 2 * 365;
                    }
                    else
                    {
                        Fee0 = Fee0 + Fee / 10000d * this.GetTrainWeight() *
                         node1.Miles * 2;
                    }
                }
            }

            Fee0 = Fee0 / UnitRate;
            return JMath.Round1(Fee0, this.XiaoShou);

        }

        //电网和接触网使用费--OK
        internal override double GetFee3()
        {
           return 0;
        }
        //电网和接触网使用费--2014 04 17
        internal override double GetFee3(List<string[]> ListInfos)
        {
            return 0;
        }
        //Fee4:售票服务费---OK
        internal override double GetFee4()
        {
            return base.GetFee4();
        }

        //Fee5：旅客服务费----OK
        internal override double GetFee5()
        {
            return base.GetFee5();
        }

        //Fee6：列车上水费----OK
        internal override double GetFee6()
        {
            return base.GetFee6();
        }

        //Fee7：人员和工资附加费--OK
        internal override double GetFee7()
        {
            double fee = 0;
            if (this.CunZengMoShi == ECunZengMoShi.新人新车 
                || this.CunZengMoShi == ECunZengMoShi.新人有车)
            {
                #region 以前的算法
                /*
                DataRow[] drs = PersonGZProfile.Data.Select("kind='0'");
                foreach (DataRow dr in drs)
                {
                    String gw = dr["gw"].ToString();
                    double fj = 0;
                    if (dr["FJ"].ToString().Trim() != String.Empty)
                    {
                        fj = double.Parse(dr["FJ"].ToString());
                    }

                    double gz1 = 0;
                    if (dr["FJ"].ToString().Trim() != String.Empty)
                    {
                        gz1 = double.Parse(dr["FJ"].ToString());
                    }


                    double t1 = gz1 * (1 + fj / 100d);

                    if (gw == "司机")
                    {
                        fee = fee + 2 * t1;  //2个司机
                    }
                    else if (gw == "列车长")
                    {
                        fee = fee + t1;  //1个列车长
                    }
                    else if (gw == "乘务员")
                    {
                        double rs = (this.YinZuo + this.RuanZuo +
                            this.OpenYinWo + this.RuanWo + this.AdvanceRuanWo);

                        if (this.ServerPerson == EServerPerson.一人2车)
                        {
                            rs = Math.Ceiling(rs / 2.0);
                        }
                        else if (this.ServerPerson == EServerPerson.二人3车)
                        {
                            rs = Math.Ceiling(rs * 2 / 3.0);
                        }

                        fee = fee + rs * t1;
                    }
                    else if (gw == "车检")
                    {
                        fee = fee + 2 * t1;

                        if (this.GongDianType == EGongDianType.非直供电)
                        {
                            fee = fee + 2*t1;    //非直供电，增加2人维修发电车
                        }
                    }
                }*/
                #endregion

                bool hasDianChe = false;
                if (this.FaDianChe > 0) hasDianChe = true;

                ETrainType type1 = (ETrainType)((int)this.trainType);

                //设置不同车厢的数量
                int yz = this.yinZuo + this.RuanZuo;
                int yw = this.openYinWo + this.CloseYinWo+this.ShuYinChe ;
                int rw = this.ruanWo + this.AdvanceRuanWo;

                fee = TrainPersonBU.GetPersonGzAndFjFee(type1,hasDianChe,yz,yw,rw);

                //计算班次

                //double banci = 1;
                //调整为按车底数计算
                double hour1 = this.GetRunHour();
                /*
                if (hour1 > 0)
                {
                    if (this.IsYearFlag)
                    {
                        fee = fee * Math.Ceiling( hour1 * 365 / 2000) * 2;
                    }
                    else
                    {
                        fee =Math.Ceiling(fee /2000) * Math.Ceiling(hour1) * 2 ;
                    }
                }*/

                fee = fee * this.CheDiShu * 2;
                if (this.IsYearFlag == false)
                {
                    fee = fee * hour1*2 / 2000;
                }

                //增加系数(空调车和单趟不考虑）
                double PRate = 1;
                if (this.trainType != ECommTrainType.空调车25T
                    && this.IsYearFlag )
                {
                    if (hour1 > 12 && hour1 <= 18)
                    {
                        PRate = 1.5;
                    }

                    if (hour1 > 18)
                    {
                        PRate = 2.0;
                    }
                }
                fee = fee * PRate;


                //计算
                fee = fee / UnitRate;
                return JMath.Round1(fee ,this.XiaoShou);
            }
            else
            {
                fee = fee * UnitRate;
                return JMath.Round1(fee,this.XiaoShou);
            }
        }

        //车辆折旧费--OK（车底数相关）
        internal override double GetFee8()
        {
            if (this.CunZengMoShi == ECunZengMoShi.新人新车 
                || this.CunZengMoShi == ECunZengMoShi.有人新车)
            {
                double fee = this.GetTrainBuyPrice();
                fee = fee * TrainProfile.TrainZheJiuRate / 100d;

                //全年的折旧率
                if (this.IsYearFlag)
                {
                    fee = fee * this.CheDiShu;
                }
                else
                {
                    /*
                    double hour1 = this.GetRunHour();
                    fee = fee * Math.Ceiling(hour1 * 2 / 24.0) / 365;*/
                    fee = (fee / 365) * this.CheDiShu;  //4月23日调整
                }

                //考虑检备率
                if (this.IsYearFlag)
                {
                    fee = fee * (1 + TrainProfile.JianBeiLv1 / 100d);
                }

                fee = fee * 10000;
                fee = fee / UnitRate;
                return JMath.Round1(fee,this.XiaoShou);
            }
            else
            {
                return 0;
            }
        }

        //日常检修成本--OK（车底数相关）
        internal override double GetFee9()
        { 
            double fee = 0;
            DataTable dt = RiChangFeeProfile.Data;
            if (dt.Rows.Count > 0)
            {
                double chexiangCount = this.GetTotalCheXianShu();
                DataRow dr = null;
                DataRow[] drs = null;
                if (this.TrainType == ECommTrainType.绿皮车25B)
                {
                    drs = dt.Select("TRAINTYPE='25B'");
                }
                else if(this.trainType==ECommTrainType.空调车25G)
                {
                    if (this.GongDianType == EGongDianType.直供电)
                    {
                        drs = dt.Select("TRAINTYPE='25G'");
                    }
                    else
                    {
                        drs = dt.Select("TRAINTYPE='25G(直供电)'");
                    }
                }
                else if (this.trainType == ECommTrainType.空调车25K)
                {
                    if (this.GongDianType == EGongDianType.直供电)
                    {
                        drs = dt.Select("TRAINTYPE='25K'");
                    }
                    else
                    {
                        drs = dt.Select("TRAINTYPE='25K(直供电)'");
                    }
                }
                else if (this.TrainType == ECommTrainType.空调车25T)
                {
                    drs = dt.Select("TRAINTYPE='25T'");
                }
                if (drs != null && drs.Length > 0)
                {
                    dr = drs[0];
                    if (String.IsNullOrEmpty(dr["rcfee1"].ToString()) == false)
                    {
                        fee = fee + chexiangCount * double.Parse(dr["rcfee1"].ToString());
                    }
                }

                //计算发电车的检修费用
                if (this.FaDianChe > 0)
                {
                    drs = dt.Select("TRAINTYPE='发电车'");
                    if (drs != null && drs.Length > 0)
                    {
                        dr = drs[0];
                        if (String.IsNullOrEmpty(dr["rcfee1"].ToString()) == false)
                        {
                            fee = fee + this.FaDianChe * double.Parse(dr["rcfee1"].ToString());
                        }
                    }
                }
            }

            //考虑检备率
            if (this.IsYearFlag)
            {
                fee = fee * (1 + TrainProfile.JianBeiLv1 / 100d);
            }

            fee = fee / UnitRate;
            //年成本和趟成本
            if (this.IsYearFlag)
            {
                fee = fee * this.CheDiShu * 10000;
            }
            else
            {
                /* fee = fee * this.CheDiShu * 10000;
                double hour1 = this.GetRunHour();
                double cishu= Math.Ceiling(hour1 * 2 / 24.0) / 365;
                fee = fee * cishu; */
                fee = (fee * 10000 / 365) * this.CheDiShu;   //4月23日调整
            }

            //非新车返回50%
            if (this.CunZengMoShi == ECunZengMoShi.新人新车
                || this.CunZengMoShi == ECunZengMoShi.有人新车)
            {
                return JMath.Round1(fee,this.XiaoShou);
            }
            else
            {
                return JMath.Round1(fee*TrainProfile.JianXiuFeeRate,this.XiaoShou);   //0.5参数可维护
            }
        }
        
        //定期检修成本--OK
        internal override double GetFee10()
        {
            double fee = 0;
            double A2 = 0;
            double A3 = 0;
            double A4 = 0;
            double Fd = 0;

            int nA2 = 0;
            int nA3 = 0;
            int nA4 = 0;
            int nFd = 0;

            //计算A2A3A4的费用
            String key1 = String.Empty;
            if (this.TrainType == ECommTrainType.绿皮车25B)
            {
                key1 = "25B";
                if (this.SYinZuo > 0 || this.SRuanZuo > 0)
                {
                    key1 = "25BS";
                }
            }
            else if (this.TrainType == ECommTrainType.空调车25G)
            {
                key1 = "25G";
                if (this.GongDianType == EGongDianType.直供电)
                {
                    key1 = "25G(直供电)";
                }
            }
            else if (this.TrainType == ECommTrainType.空调车25K)
            {
                key1 = "25K";
                if (this.GongDianType == EGongDianType.直供电)
                {
                    key1 = "25K(直供电)";
                }
                if (this.RuanWo19K > 0)
                {
                    key1 = "19K";
                }
            }
            else if (this.TrainType == ECommTrainType.空调车25T)
            {
                key1 = "25T";
                if (this.RuanWo19T > 0)
                {
                    key1 = "19T ";
                }
            }

            //计算A2和A3的费用
            DataTable dt = A2A3FeeProfile.Data;
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] drs = dt.Select("traintype='" + key1 + "'");
                DataRow dr = null;
                if (drs != null && drs.Length > 0) dr = drs[0];
                if (dr != null)
                {
                    if(String.IsNullOrEmpty(dr["A2Fee"].ToString())==false)
                    {
                        A2 = double.Parse(dr["A2Fee"].ToString());
                    }

                    if (String.IsNullOrEmpty(dr["A3Fee"].ToString()) == false)
                    {
                        A3 = double.Parse(dr["A3Fee"].ToString());
                    }

                    if (A2 > 0)
                    {
                        nA2 = 3; nA3 = 2; nA4 = 1;
                    }
                    else
                    {
                        nA2 = 0; nA3 = 3; nA4 = 1;
                    }
                }
            }

            //计算A4的费用
            A4 = A4 + this.YinZuo * double.Parse(CheXianProfile.Data[this.trainType].YZA4);
            A4 = A4 + this.RuanZuo * double.Parse(CheXianProfile.Data[this.trainType].RZA4);

            //增加了对宿营车的处理
            A4 = A4 + (this.OpenYinWo +this.ShuYinChe) * double.Parse(CheXianProfile.Data[this.trainType].YWA4);

            A4 = A4 + this.RuanWo * double.Parse(CheXianProfile.Data[this.trainType].RWA4);
            A4 = A4 + this.CanChe * double.Parse(CheXianProfile.Data[this.trainType].CAA4);
            A4 = A4 + this.FaDianChe * double.Parse(CheXianProfile.Data[this.trainType].KDA4);

            A4 = A4 + this.SYinZuo * double.Parse(CheXianProfile.Data[this.trainType].SYZA4);
            A4 = A4 + this.SRuanZuo * double.Parse(CheXianProfile.Data[this.trainType].SRZA4);
            A4 = A4 + this.RuanWo19K * double.Parse(CheXianProfile.Data[this.trainType].RW19KA4);
            A4 = A4 + this.RuanWo19T * double.Parse(CheXianProfile.Data[this.trainType].RW19TA4);
            int chexianCount = this.GetTotalCheXianShu();

            fee = (nA2 * A2*chexianCount + nA3 * A3*chexianCount + nA4 * A4)/(240);   //增加车厢数相乘

            //发电车机组的费用（4月21日）
            if (this.GongDianType == EGongDianType.非直供电
                && this.KongTiaoFlag)
            {
                key1 = "发电机组(MTU183)";
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow[] drs = dt.Select("traintype='" + key1 + "'");
                    DataRow dr = null;
                    if (drs != null && drs.Length > 0) dr = drs[0];
                    if (dr != null)
                    {
                        Fd = double.Parse(dr["A3Fee"].ToString());
                    }
                }
            }


            fee = fee / UnitRate;
            //年成本和趟成本
            if (this.IsYearFlag)
            {
                fee = fee * this.YunXingLiCheng * 2 * 365+Fd*365/(365*1.5);
            }
            else
            {
                fee = fee * this.YunXingLiCheng * 2+Fd/(365*1.5)*this.CheDiShu;
            }

            //非新车返回50%
            if (this.CunZengMoShi == ECunZengMoShi.新人新车
                || this.CunZengMoShi == ECunZengMoShi.有人新车)
            {
                return JMath.Round1(fee,this.XiaoShou);
            }
            else
            {
                //return JMath.Round1(fee * 0.5,this.XiaoShou);
                return JMath.Round1(fee*TrainProfile.JianXiuFeeRate , this.XiaoShou);
            }
        }

        //车辆备用品消耗--OK（车底数相关）
        internal override double GetFee11()
        {
            double fee = 0;
            if (String.IsNullOrEmpty(CheXianProfile.Data[this.trainType].YZCOST3) == false)
            {
                fee = fee + this.YinZuo * double.Parse(CheXianProfile.Data[this.trainType].YZCOST3);
            }

            if (String.IsNullOrEmpty(CheXianProfile.Data[this.trainType].RZCOST3) == false)
            {
                fee = fee + this.RuanZuo * double.Parse(CheXianProfile.Data[this.trainType].RZCOST3);
            }

            //增加了对宿营车的处理
            if (String.IsNullOrEmpty(CheXianProfile.Data[this.trainType].YWCOST3) == false)
            {
                fee = fee + (this.OpenYinWo+this.ShuYinChe)
                    * double.Parse(CheXianProfile.Data[this.trainType].YWCOST3);
            }

            if (String.IsNullOrEmpty(CheXianProfile.Data[this.trainType].RWCOST3) == false)
            {
                fee = fee + this.RuanWo * double.Parse(CheXianProfile.Data[this.trainType].RWCOST3);
            }

            if (String.IsNullOrEmpty(CheXianProfile.Data[this.trainType].GRWCOST3) == false)
            {
                fee = fee + this.AdvanceRuanWo * double.Parse(CheXianProfile.Data[this.trainType].GRWCOST3);
            }

            if (String.IsNullOrEmpty(CheXianProfile.Data[this.trainType].CACOST3) == false)
            {
                fee = fee + this.CanChe * double.Parse(CheXianProfile.Data[this.trainType].CACOST3);
            }

            if (String.IsNullOrEmpty(CheXianProfile.Data[this.trainType].KDCOST3) == false)
            {
                fee = fee + this.FaDianChe * double.Parse(CheXianProfile.Data[this.trainType].KDCOST3);
            }

            if (String.IsNullOrEmpty(CheXianProfile.Data[this.trainType].SYCOST3) == false)
            {
               // fee = fee + this.ShuYinChe * double.Parse(CheXianProfile.Data[this.trainType].SYCOST3);
            }

            fee = fee / UnitRate;
            //年成本和趟成本
            if (this.IsYearFlag)
            {
                fee = fee * this.CheDiShu * 10000;
            }
            else
            {
                fee = fee * this.CheDiShu * 10000;
                /*
                double hour1 = this.GetRunHour();
                double cishu = Math.Ceiling(hour1 * 2 / 24.0) / 365;
                fee = fee * cishu; */
                fee = (fee / 365) ;

            }
            return JMath.Round1(fee,this.XiaoShou);
        }

        //空调车用油--OK
        internal override double GetFee12()
        {
            double fee = 0;
            if (this.FaDianChe > 0)
            {
                fee = double.Parse(CheXianProfile.Data[this.trainType].OIL) * this.YunXingLiCheng;
                fee = fee * this.GetTotalCheXianShu() / 1000;
                if (this.IsYearFlag)
                {
                    fee = fee * TrainProfile.OilUnitCost * 2 * 365;
                }
                else
                {
                    fee = fee * TrainProfile.OilUnitCost * 2;
                }
            }

            fee = fee / UnitRate;
            return JMath.Round1(fee,this.XiaoShou);
        }

        //人员其他的费用--OK
        internal override double GetFee13()
        {
            double fee = 0;
            if (this.CunZengMoShi == ECunZengMoShi.新人新车
                || this.CunZengMoShi == ECunZengMoShi.新人有车)
            {
                #region 以前的算法
                DataRow[] drs = PersonGZProfile.Data.Select("kind='0'");
                foreach (DataRow dr in drs)
                {
                    String gw = dr["gw"].ToString();
                    double qtfy = 0;
                    if (dr["QTFY"].ToString().Trim() != String.Empty)
                    {
                        qtfy = double.Parse(dr["QTFY"].ToString());
                    }
                    double t1 = qtfy;

                    if (gw == "司机")
                    {
                        fee = fee + 2 * t1;  //2个司机
                    }
                    else if (gw == "列车长")
                    {
                        fee = fee + t1;  //1个列车长
                    }
                    else if (gw == "乘务员")
                    {
                        double rs = (this.YinZuo + this.RuanZuo
                            + this.OpenYinWo + this.RuanWo
                            + this.AdvanceRuanWo);
                        if (this.ServerPerson == EServerPerson.一人2车)
                        {
                            rs = Math.Ceiling(rs / 2.0);
                        }
                        else if (this.ServerPerson == EServerPerson.二人3车)
                        {
                            rs = Math.Ceiling(rs * 2 / 3.0);
                        }
                        fee = fee + rs * t1;
                    }
                    else if (gw == "车检")
                    {
                        fee = fee + 2 * t1;
                        if (this.GongDianType == EGongDianType.非直供电)
                        {
                            fee = fee + 2 * t1;    //非直供电，增加2人维修发电车
                        }
                    }
                }
                #endregion

                bool hasDianChe = false;
                if (this.FaDianChe > 0) hasDianChe = true;
                ETrainType type1 = (ETrainType)((int)this.trainType);

                int yz = this.yinZuo + this.RuanZuo;
                int yw = this.openYinWo + this.CloseYinWo+this.ShuYinChe;
                int rw = this.ruanWo + this.AdvanceRuanWo;
                fee = TrainPersonBU.GetPersonQtFee(type1,hasDianChe,yz,yw,rw );

                //计算班次
                //double banci = 1;
                double hour1 = this.GetRunHour();
                if (hour1 > 0)
                {
                    /*
                    if (this.IsYearFlag)
                    {
                        fee = fee * Math.Ceiling(hour1 * 365 / 2000) * 2;
                    }
                    else
                    {
                        fee = Math.Ceiling(fee / 2000) * Math.Ceiling(hour1) * 2;
                    }*/

                    //直接使用车底数
                    fee = fee * this.CheDiShu * 2;
                    if (this.IsYearFlag == false)
                    {
                        fee = fee * hour1*2 / 2000;
                    }

                    /*
                    if (this.IsYearFlag)
                    {
                        banci = Math.Ceiling(hour1 * 365 * 2 / 2000);
                    }
                    else
                    {
                        banci = hour1 * 2 / 2000;
                    }*/
                }

                //增加系数(不考虑空调车和单趟）
                double PRate = 1;
                if (this.trainType != ECommTrainType.空调车25T
                    && this.IsYearFlag )
                {
                    if (hour1 > 12 && hour1 <= 18)
                    {
                        PRate = 1.5;
                    }

                    if (hour1 > 18)
                    {
                        PRate = 2.0;
                    }
                }
                fee = fee * PRate;

                //费率计算
                fee = fee / UnitRate;
                return JMath.Round1(fee , this.XiaoShou);
            }
            else
            {
                fee = fee / UnitRate;
                return JMath.Round1(fee, this.XiaoShou);
            }
        }

        //车辆购置利息（车底数相关）
        internal override double GetFee14()
        {
            if (this.CunZengMoShi == ECunZengMoShi.新人新车
                || this.CunZengMoShi == ECunZengMoShi.有人新车)
            {
                double fee = this.GetTrainBuyPrice();
                fee = fee * TrainProfile.FixLiXi / 100d;
                fee = fee * this.CheDiShu;

                //考虑检备率
                if (this.IsYearFlag)
                {
                    fee = fee * (1 + TrainProfile.JianBeiLv1 / 100d);
                }

                if (this.IsYearFlag == false)
                {
                    fee = fee / 365;
                }
                fee = fee / UnitRate;
                return JMath.Round1(fee * 10000,this.XiaoShou);
            }
            else
            {
                return 0;
            }
        }

        //轮渡费
        public override double GetFee15()
        {
            double Fee = 0;
            double Fee0 = 0;

            if (this.Line != null && this.Line.Nodes.Count > 0)
            {
                //设置线路牵引费
                foreach (LineNode node1 in this.Line.Nodes)
                {
                    if (String.IsNullOrEmpty(node1.ShipFlag)==false)
                    {
                        if (this.KongTiaoFlag)
                        {
                            Fee = Fee + this.GetTotalCheXianShu() * node1.Miles * TrainProfile.ShipFee1;
                        }
                        else
                        {
                            Fee = Fee + this.GetTotalCheXianShu() * node1.Miles * TrainProfile.ShipFee2;
                        }
                    }
                }
            }

            //趟乘2，年乘2乘365
            if (this.IsYearFlag)
            {
                Fee0 = Fee * 2 * 365 / UnitRate;
            }
            else
            {
                Fee0 = Fee * 2  / UnitRate; 
            }
            return JMath.Round1(Fee0, this.XiaoShou);
        }

        //间接费用分摊
        public override double GetFee16()
        {
            double Fee=0;
            Fee = TrainProfile.JianJieFee * this.YunXingLiCheng * GetTotalCheXianShu();
            //趟乘2，年乘2乘365
            if (this.IsYearFlag)
            {
                Fee = Fee * 2 * 365 / UnitRate;
            }
            else
            {
                Fee = Fee * 2 / UnitRate;
            }
            Fee= JMath.Round1(Fee, this.XiaoShou);
            return Fee;
        }
        #endregion

        #region 其他方法
        //得到普通列车的满员人数-
        public override int GetTotalPerson()
        {
            int result = 0;
            if (this.YinZuo > 0)
            {
                result = result + this.YinZuo * GetCheXianTotalPerson(ECheXian.硬座);
            }
            if (this.RuanZuo > 0)
            {
                result = result + this.RuanZuo * GetCheXianTotalPerson(ECheXian.硬座);
            }

            if (this.OpenYinWo > 0)
            {
                result = result + this.OpenYinWo * GetCheXianTotalPerson(ECheXian.开放式硬卧);
            }

            if (this.CloseYinWo > 0)
            {
                result = result + this.CloseYinWo * GetCheXianTotalPerson(ECheXian.包房式硬卧);
            }

            if (this.RuanWo > 0)
            {
                result = result + this.RuanWo * GetCheXianTotalPerson(ECheXian.软卧);
            }

            if (this.AdvanceRuanWo > 0)
            {
                result = result + this.AdvanceRuanWo * GetCheXianTotalPerson(ECheXian.高级软卧);
            }

            //增加宿营车的人员处理
            if (this.ShuYinChe > 0)
            {
                result = result + this.ShuYinChe * TrainProfile.SyCheXianPCount;
            }
            return result;
        }

        //计算列车的重量
        private double GetTrainWeight()
        {
            double result = 0;
            if (this.YinZuo > 0)
            {
                result = result + this.YinZuo * double.Parse(CheXianProfile.Data[this.trainType].YZWEIGHT);
            }
            if (this.RuanZuo > 0)
            {
                result = result + this.RuanZuo * double.Parse(CheXianProfile.Data[this.trainType].RZWEIGHT);
            }

            if (this.OpenYinWo > 0)
            {
                result = result + this.OpenYinWo * double.Parse(CheXianProfile.Data[this.trainType].YWWEIGHT);
            }

            if (this.CloseYinWo > 0)
            {
                //result = result + this.CloseYinWo * CheXianProfile.YinWo2_Weight;
            }

            if (this.RuanWo > 0)
            {
                result = result + this.RuanWo * double.Parse(CheXianProfile.Data[this.trainType].RWWEIGHT);
            }

            if (this.AdvanceRuanWo > 0)
            {
                result = result + this.AdvanceRuanWo * double.Parse(CheXianProfile.Data[this.trainType].GRWWEIGHT);
            }

            if (this.CanChe > 0)
            {
                result = result + this.CanChe * double.Parse(CheXianProfile.Data[this.trainType].CAWEIGHT);
            }

            if (this.FaDianChe > 0)
            {
                result = result + this.FaDianChe * double.Parse(CheXianProfile.Data[this.trainType].KDWEIGHT);
            }

            //宿营车的重量和硬卧车厢的重量是一样的（5月17日修改）
            if (this.ShuYinChe > 0)
            {
                result = result + this.ShuYinChe * double.Parse(CheXianProfile.Data[this.trainType].YWWEIGHT);
            }
            return result;
        }

        //得到列车的采购价格
        private double GetTrainBuyPrice()
        {
            double fee = 0;
            if (String.IsNullOrEmpty(CheXianProfile.Data[this.trainType].YZPRICE) == false)
            {
                fee = fee + this.YinZuo * double.Parse(CheXianProfile.Data[this.trainType].YZPRICE);
            }

            if (String.IsNullOrEmpty(CheXianProfile.Data[this.trainType].RZPRICE) == false)
            {
                fee = fee + this.RuanZuo * double.Parse(CheXianProfile.Data[this.trainType].RZPRICE);
            }

            //宿营车的价格和硬卧相同
            if (String.IsNullOrEmpty(CheXianProfile.Data[this.trainType].YWPRICE) == false)
            {
                fee = fee + (this.OpenYinWo +this.ShuYinChe)
                    * double.Parse(CheXianProfile.Data[this.trainType].YWPRICE);
            }

            if (String.IsNullOrEmpty(CheXianProfile.Data[this.trainType].RWPRICE) == false)
            {
                fee = fee + this.RuanWo * double.Parse(CheXianProfile.Data[this.trainType].RWPRICE);
            }

            if (String.IsNullOrEmpty(CheXianProfile.Data[this.trainType].GRWPRICE) == false)
            {
                fee = fee + this.AdvanceRuanWo * double.Parse(CheXianProfile.Data[this.trainType].GRWPRICE);
            }

            if (String.IsNullOrEmpty(CheXianProfile.Data[this.trainType].CAPRICE) == false)
            {
                fee = fee + this.CanChe * double.Parse(CheXianProfile.Data[this.trainType].CAPRICE);
            }

            if (String.IsNullOrEmpty(CheXianProfile.Data[this.trainType].KDPRICE) == false)
            {
                fee = fee + this.FaDianChe * double.Parse(CheXianProfile.Data[this.trainType].KDPRICE);
            }

            if (String.IsNullOrEmpty(CheXianProfile.Data[this.trainType].SYPRICE) == false)
            {
               // fee = fee + this.ShuYinChe * double.Parse(CheXianProfile.Data[this.trainType].SYPRICE);
            }
            return fee;
        }

        //得到总的车厢数
        public int GetTotalCheXianShu()
        {
            int result = 0;
            result = result + this.YinZuo +this.RuanZuo + this.OpenYinWo 
                + this.CloseYinWo + this.RuanWo + this.AdvanceRuanWo
                + this.CanChe + this.FaDianChe+this.ShuYinChe;
            return result;
        }

        //得到运行时间
        public override double GetRunHour()
        {
            double speed = 0;
            double hour1 = -1;
            if (String.IsNullOrEmpty(CheXianProfile.Data[this.TrainType].SPEED) == false)
            {
                speed = double.Parse(CheXianProfile.Data[this.TrainType].SPEED);
                hour1 = this.YunXingLiCheng / speed;
            }
            return hour1;
        }
        #endregion

    }
}
