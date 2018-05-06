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
/// 动车的类
/// </summary>
public partial class HighTrain:Train
{
    #region 列车支出的各项费用
    //线路使用费--OK
    internal override double GetFee1()
    {
        double fee0 = 0;
        double jnfee = 0;

       
        ETrainType type1 =(ETrainType)((int)this.TrainType);
        /*
        EHighTrainBianZhu bianzhu = EHighTrainBianZhu.单组;
        if (IsUnionHighTrain(type1))
        {
            bianzhu = EHighTrainBianZhu.重联;
        }*/

        if (this.Line != null && this.Line.Nodes.Count > 0)
        {
            for (int i = 0; i < this.Line.Nodes.Count; i++)
            {
                int index0 = int.Parse(this.Line.Nodes[i].LineType);
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

        if (this.IsYearFlag)
        {
            fee0 = fee0 * 2 * 365/UnitRate ;
            jnfee = jnfee * 2 * 365/UnitRate ;
            this.JnFee = JMath.Round1(jnfee, this.XiaoShou);
        }
        else
        {
            fee0 = fee0 * 2/UnitRate  ;
            jnfee = jnfee * 2 /UnitRate;
            this.JnFee = JMath.Round1(jnfee,this.XiaoShou);
        }

        //增加全成本和部分成本
        if (this.IsFullChengBen == false)
        {
            return JMath.Round1(fee0, this.XiaoShou);
        }
        else
        {
            return JMath.Round1(fee0 , this.XiaoShou)
                +JMath.Round1(jnfee,this.XiaoShou);
        }
    }

    //机车牵引费--OK
    internal override double GetFee2()
    {
        return 0;
    }


    //电费和接触网使用费--OK
    internal override double GetFee3()
    {
        TrainLine line0 = this.Line;
        double Fee = 0;
        if (line0 != null)
        {
            //根据线路的里程和线路的级别计算电费和接触网使用费用
            for (int i = 0; i < line0.Nodes.Count; i++)
            {
                String lineType = line0.Nodes[i].LineType.Trim();
                double JieChuFeeRate = TrainLineKindProfile.JieChuFee0;
                double DianFeeRate = TrainLineKindProfile.DianFee0;
                if (lineType == "1")
                {
                    JieChuFeeRate = TrainLineKindProfile.JieChuFee2;
                    DianFeeRate = TrainLineKindProfile.DianFee2;
                }
                else if (lineType == "2")
                {
                    JieChuFeeRate = TrainLineKindProfile.JieChuFee1;
                    DianFeeRate = TrainLineKindProfile.DianFee1;
                }

                Fee = Fee + (JieChuFeeRate + DianFeeRate) * line0.Nodes[i].Miles * this.GetTrainWeight();
            }
        }

        if (this.IsYearFlag)
        {
            Fee = Fee * 2 * 365;
        }
        else
        {
            Fee = Fee * 2 ;
        }

        Fee = Fee / UnitRate;
        return JMath.Round1(Fee / 10000, this.XiaoShou);
    }
    //20140409  编写
    /// <summary>
    /// 求得客运专线的电费地和接触费
    /// </summary>
    /// <param name="busType"></param>
    /// <returns></returns>
    internal override double GetFee3(List<string[]> lineInfos)
    {
        double Fee = 0;
        JTable tab1 = new JTable("gscorpelecfee");
        for (int i = 0; i < lineInfos.Count; i++)
        {
            double JieChuFeeRate = 0.0d;
            double DianFeeRate = 0.0d;
            if (string.IsNullOrEmpty(lineInfos[i][0]))//判断客专id值
            {
                string linetype = lineInfos[i][2];//获得线路类型
                if (linetype == "1")
                {
                    JieChuFeeRate = TrainLineKindProfile.JieChuFee2;
                    DianFeeRate = TrainLineKindProfile.DianFee2;
                }
                else if (linetype == "2")
                {
                    JieChuFeeRate = TrainLineKindProfile.JieChuFee1;
                    DianFeeRate = TrainLineKindProfile.DianFee1;
                }
            }
            else 
            {
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("Num", lineInfos[i][0]));
                JieChuFeeRate = Convert.ToDouble(tab1.SearchScalar(condition, "NETFEE"));
                DianFeeRate = Convert.ToDouble(tab1.SearchScalar(condition, "ELECFEE"));
            
            }

            Fee = Fee + (JieChuFeeRate + DianFeeRate) * Convert.ToInt32(lineInfos[i][1]) * this.GetTrainWeight();//获得里程数
          
        }
        if (this.IsYearFlag)
        {
            Fee = Fee * 2 * 365;
        }
        else
        {
            Fee = Fee * 2;
        }
            
#region 注释信息
        //TrainLine line0 = this.Line;
        //double Fee = 0;
        //if (line0 != null)
        //{
        //    //根据线路的里程和线路的级别计算电费和接触网使用费用
        //    for (int i = 0; i < line0.Nodes.Count; i++)
        //    {
        //        string  lineID = line0.Nodes[i].LineID;
        //        string astation = line0.Nodes[i].AStation;
        //        string bstation = line0.Nodes[i].BStation;
                
        //        condition.Clear();
        //        condition.Add(new SearchField("lineID",lineID));
        //        condition.Add(new SearchField("AStation",astation));
        //        condition.Add(new SearchField("BStation",bstation));
        //        //得到相应客专主键id
        //        string corpid = tab1.SearchScalar(condition, "KZID").ToString() ;
        //        //得到相应的标准
        //        condition.Clear();
        //        condition.Add(new SearchField("Num",corpid));
        //        double JieChuFeeRate=0.0d;
        //        double DianFeeRate =0.0d;
        //        //不是默认的计算方法，bugsType表示选中的客专路线
        //        if (corpid != "0")
        //        {
        //          JieChuFeeRate = Convert.ToDouble(tab2.SearchScalar(condition, "NETFEE"));
        //          DianFeeRate = Convert.ToDouble(tab2.SearchScalar(condition, "ELECFEE"));
        //        }    
        //        else
        //        {
        //            String lineType = line0.Nodes[i].LineType.Trim();
        //            if (lineType == "1")
        //            {
        //                JieChuFeeRate = TrainLineKindProfile.JieChuFee2;
        //                 DianFeeRate = TrainLineKindProfile.DianFee2;
        //            }
        //            else if (lineType == "2")
        //             {
        //                JieChuFeeRate = TrainLineKindProfile.JieChuFee1;
        //                DianFeeRate = TrainLineKindProfile.DianFee1;
        //             }
        //        }
        //        Fee = Fee + (JieChuFeeRate + DianFeeRate) * line0.Nodes[i].Miles * this.GetTrainWeight();
        //    }
        //}
        //if (this.IsYearFlag)
        //{
        //    Fee = Fee * 2 * 365;
        //}
        //else
        //{
        //    Fee = Fee * 2;
        //}
#endregion
        Fee = Fee / UnitRate;
        return JMath.Round1(Fee / 10000, this.XiaoShou);
    }
        
    
    //Fee4:售票服务费---OK
    internal override double GetFee4()
    {
        return base.GetFee4();
    }

    //Fee5：旅客服务费---OK
    internal override double GetFee5()
    {
        return base.GetFee5();
    }

    //列车上水费用---OK
    internal override double GetFee6()
    {
        double fee = base.GetFee6();
        if (this.BianZhu == EHighTrainBianZhu.重联)
        {
            fee = fee * 2;
        }
        return fee;
    }

    //人员工资和附加费--OK
    internal override double GetFee7()
    {
        if (this.CunZengMoShi == ECunZengMoShi.新人新车
            || this.CunZengMoShi == ECunZengMoShi.新人有车)
        {
            double fee = 0;

            #region 老的算法
            double sjcount = 0;         //司机数量
            double czcount = 0;         //列车长数量
            double cwcount = 0;         //乘务员数量
            double jxcount = 0;         //机械师数量

            if (this.bianzhu == EHighTrainBianZhu.单组)
            {
                cwcount = 2;
                czcount = 1;
                jxcount = 1;
                sjcount = 2;
            }
            else
            {
                if (this.isDongWu == false)
                {
                    cwcount = 4;
                    czcount = 2;
                    jxcount = 2;
                    sjcount = 2;
                }
                else
                {
                    cwcount = 8;
                    czcount = 1;
                    jxcount = 2;
                    sjcount = 2;
                }
            }

            DataRow[] drs = PersonGZProfile.Data.Select("kind='1'");
            foreach (DataRow dr in drs)
            {
                String gw = dr["gw"].ToString();
                double fj = 0;
                if(dr["FJ"].ToString().Trim()!=String.Empty)
                {
                    fj = double.Parse(dr["FJ"].ToString());
                }

                double gz1 = 0;
                if (dr["gz"].ToString().Trim() != String.Empty)
                {
                    gz1 = double.Parse(dr["gz"].ToString());
                }

                double t1 = gz1 * (1 + fj / 100d);

                if (gw == "司机")
                {
                    fee = fee + sjcount * t1;  //2个司机
                }
                else if (gw == "列车长")
                {
                    fee = fee + czcount * t1;  //1个列车长
                }
                else if (gw == "乘务员")
                {
                    fee = fee + cwcount * t1;
                }
                else if (gw == "车检")
                {
                    fee = fee + jxcount * t1;
                }
            }
            #endregion

            ETrainType type1 = (ETrainType)((int)this.trainType);
            fee = TrainPersonBU.GetPersonGzAndFjFee(type1,false,0,0,0);

            //计算班次
            double banci = 1;
            double hour1 = this.GetRunHour();
            if (hour1 > 0)
            {
                /*
                if (this.IsYearFlag)
                {
                    banci = hour1 * 365 * 2 / 2000;
                }
                else
                {
                    banci = hour1 * 2 / 2000;
                }*/

                /*
                if (this.IsYearFlag)
                {
                    fee = fee * Math.Ceiling(hour1 * 365 / 2000) * 2;
                }
                else
                {
                    fee = Math.Ceiling(fee / 2000) * Math.Ceiling(hour1) * 2;
                }
                 */
                 //改用车底数计算班次
                double cds1 = this.CheDiShu;
                if (cds1 < 1)
                {
                    //cds1 = 1.0;   //7月5日修改（汉宜线数据过大）
                }
                fee = fee * cds1 * 2;
                if (this.IsYearFlag == false)
                {
                    fee = fee * hour1 * 2 / 2000;
                }
            }

            
            fee = fee / UnitRate;
            return JMath.Round1(fee * banci,this.XiaoShou);
        }
        else
        {
            return 0;
        }
    }

    //车辆折旧费(车底数相关)--OK
    internal override double GetFee8()
    {
        double fee = 0;
        if (this.CunZengMoShi == ECunZengMoShi.新人新车
            || this.CunZengMoShi == ECunZengMoShi.有人新车)
        {
            DataRow[] drs = HighTrainProfile.Data.Select("HIGHTRAINTYPE='" + this.TrainType.ToString() + "'");
            if (drs != null && drs.Length > 0)
            {
                DataRow dr = drs[0];
                fee = double.Parse(dr["price"].ToString());
            }
            if (this.BianZhu == EHighTrainBianZhu.重联 && this.isDongWu == false)
            {
                fee = fee * 2;
            }
            fee = fee * TrainProfile.HighZheJiuRate / 100d;

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
                fee = fee * this.CheDiShu / 365;  //4月23日调整
            }

            //考虑检备率
            if (this.IsYearFlag)
            {
                fee = fee * (1 + TrainProfile.JianBeiLv2 / 100d);
            }

            fee = fee / UnitRate;
            return JMath.Round1(fee * 10000,this.XiaoShou);
        }
        else
        {
            return fee;
        }
    }

    //日常检修成本（车底数相关）
    internal override double GetFee9()
    {
        double fee = 0;
        DataRow[] drs = HighTrainProfile.Data.Select("HIGHTRAINTYPE='" + this.TrainType.ToString() + "'");
        if (drs != null && drs.Length > 0)
        {
            DataRow dr = drs[0];
            fee = double.Parse(dr["cost1"].ToString());
        }
        if (this.BianZhu == EHighTrainBianZhu.重联 && this.isDongWu == false)
        {
            fee = fee * 2;
        }

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
            fee = fee / 365;    //4月23日调整
        }

        //考虑检备率
        if (this.IsYearFlag)
        {
            fee = fee * (1 + TrainProfile.JianBeiLv2 / 100d);
        }

        fee = fee / UnitRate;
       //非新车返回50%
       if (this.CunZengMoShi == ECunZengMoShi.新人新车
           || this.CunZengMoShi == ECunZengMoShi.有人新车)
       {
           return JMath.Round1(fee,this.XiaoShou);
       }
       else
       {
           return JMath.Round1(fee * TrainProfile.JianXiuFeeRate ,this.XiaoShou);
       }
    }


    //定期检修成本
    internal override double GetFee10()
    {
        double fee = 0;
        double A2 = 0;
        double A3 = 0;
        double A4 = 0;
        double iA2 = 2, iA3 = 1, iA4 = 1;

        DataRow[] drs = HighTrainProfile.Data.Select("HIGHTRAINTYPE='" + this.TrainType.ToString() + "'");
        if (drs != null && drs.Length > 0)
        {
            DataRow dr = drs[0];
            if (String.IsNullOrEmpty(dr["cost2"].ToString()) == false)
            {
                A2 = double.Parse(dr["cost2"].ToString());
            }

            if (String.IsNullOrEmpty(dr["cost21"].ToString()) == false)
            {
                A3 = double.Parse(dr["cost21"].ToString());
            }

            if (String.IsNullOrEmpty(dr["cost22"].ToString()) == false)
            {
                A4 = double.Parse(dr["cost22"].ToString());
            }
            fee = iA2 * A2 + iA3 * A3 + iA4 * A4;
            if (this.TrainType == EHighTrainType.CRH5A)
            {
                fee = fee / 480;
            }
            else
            {
                fee = fee / 240;
            }

        }
        if (this.BianZhu == EHighTrainBianZhu.重联 && this.isDongWu == false)
        {
            fee = fee * 2;
        }

      
        //年成本和趟成本
        if (this.IsYearFlag)
        {
            fee = fee * this.YunXingLiCheng * 2 * 365;
        }
        else
        {
            fee = fee * this.YunXingLiCheng * 2;
        }

        //非新车返回50%
        fee = fee / UnitRate;
        if (this.CunZengMoShi == ECunZengMoShi.新人新车
            || this.CunZengMoShi == ECunZengMoShi.有人新车)
        {
            return JMath.Round1(fee,this.XiaoShou);
        }
        else
        {
            //return JMath.Round1(fee * 0.5,this.XiaoShou);
            return JMath.Round1(fee * TrainProfile.JianXiuFeeRate, this.XiaoShou);
        }
    }

    //车辆备用品消耗（车底数相关）
    internal override double GetFee11()
    {
        double fee = 0;
        DataRow[] drs = HighTrainProfile.Data.Select("HIGHTRAINTYPE='" + this.TrainType.ToString() + "'");
        if (drs != null && drs.Length > 0)
        {
            DataRow dr = drs[0];
            fee = double.Parse(dr["cost3"].ToString());
        }
        if (this.BianZhu == EHighTrainBianZhu.重联 && this.isDongWu == false)
        {
            fee = fee * 2;
        }

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
            fee = fee * cishu;*/
            fee = fee / 365;   //4月23日调整

        }

        fee = fee / UnitRate;
        return JMath.Round1(fee,this.XiaoShou);
    }

    //空调车用油--OK
    internal override double GetFee12()
    {
        return 0;
    }

    //人员其他费用
    internal override double GetFee13()
    {
        if (this.CunZengMoShi == ECunZengMoShi.新人新车
            || this.CunZengMoShi == ECunZengMoShi.新人有车)
        {
            double fee = 0;

            #region 以前的算法
            double sjcount = 0;         //司机数量
            double czcount = 0;         //列车长数量
            double cwcount = 0;         //乘务员数量
            double jxcount = 0;         //机械师数量

            if (this.bianzhu == EHighTrainBianZhu.单组)
            {
                cwcount = 2;
                czcount = 1;
                jxcount = 1;
                sjcount = 2;
            }
            else
            {
                if (this.isDongWu == false)
                {
                    cwcount = 4;
                    czcount = 2;
                    jxcount = 2;
                    sjcount = 2;
                }
                else
                {
                    cwcount = 8;
                    czcount = 1;
                    jxcount = 2;
                    sjcount = 2;
                }
            }

            DataRow[] drs = PersonGZProfile.Data.Select("kind='1'");
            foreach (DataRow dr in drs)
            {
                String gw = dr["gw"].ToString();
                double t1 = 0;
                if (dr["qtfy"].ToString().Trim() != String.Empty)
                {
                    t1 = double.Parse(dr["qtfy"].ToString());
                }

                if (gw == "司机")
                {
                    fee = fee + sjcount * t1;  //2个司机
                }
                else if (gw == "列车长")
                {
                    fee = fee + czcount * t1;  //1个列车长
                }
                else if (gw == "乘务员")
                {
                    fee = fee + cwcount * t1;
                }
                else if (gw == "车检")
                {
                    fee = fee + jxcount * t1;
                }
            }
            #endregion

            ETrainType type1 = (ETrainType)((int)this.trainType);
            fee = TrainPersonBU.GetPersonQtFee(type1,false,0,0,0);

            //计算班次
            double banci = 1;
            double hour1 = this.GetRunHour();
            if (hour1 > 0)
            {
                /*
                if (this.IsYearFlag)
                {
                    banci = hour1 * 365 * 2 / 2000;
                }
                else
                {
                    banci = hour1 * 2 / 2000;
                }*/

                /*
                if (this.IsYearFlag)
                {
                    fee = fee * Math.Ceiling(hour1 * 365 / 2000) * 2;
                }
                else
                {
                    fee = Math.Ceiling(fee / 2000) * Math.Ceiling(hour1) * 2;
                }*/

                double cds1 = this.CheDiShu;
                if (cds1 < 1)
                {
                   // cds1 = 1.0;     //7月5日修改，汉宜线过大。
                }


                //班次的算法
                fee = fee * cds1 * 2;
                if (this.IsYearFlag == false)
                {
                    fee = fee * hour1 * 2 / 2000;
                }
            }

            fee = fee / UnitRate;
            return JMath.Round1(fee * banci, this.XiaoShou);
        }
        else
        {
            return 0;
        }
    }


    //购买车辆利息(车底数相关）
    internal override double GetFee14()
    {
        if (this.CunZengMoShi == ECunZengMoShi.新人新车
            || this.CunZengMoShi == ECunZengMoShi.有人新车)
        {
            double fee = 0;
            DataRow[] drs = HighTrainProfile.Data.Select("HIGHTRAINTYPE='" + this.TrainType.ToString() + "'");
            if (drs != null && drs.Length > 0)
            {
                DataRow dr = drs[0];
                fee = double.Parse(dr["price"].ToString());
            }
            if (this.BianZhu == EHighTrainBianZhu.重联 && this.isDongWu == false)
            {
                fee = fee * 2;
            }
            fee = fee * TrainProfile.FixLiXi / 100d;

            //考虑检备率
            if (this.IsYearFlag)
            {
                fee = fee * (1 + TrainProfile.JianBeiLv2 / 100d);
            }

            if (this.IsYearFlag == false)
            {
                fee = fee / 365;
            }

            fee = fee / UnitRate;
            return JMath.Round1(fee * this.CheDiShu * 10000,this.XiaoShou);
        }
        else
        {
            return 0;
        }
    }

    //轮渡费
    public override double GetFee15()
    {
        return 0;
    }

    //间接费用分摊
    public override double GetFee16()
    {
        double Fee = 0;
        double CheXianCount = 8;
        int temp1=(int)this.trainType;
        ETrainType train1 = (ETrainType)temp1;
        if (IsUnionHighTrain(train1))
        {
            CheXianCount =16;
        }

        Fee = TrainProfile.JianJieFee * this.YunXingLiCheng * CheXianCount ;

        //趟乘2，年乘2乘365
        if (this.IsYearFlag)
        {
            Fee = Fee * 2 * 365 / UnitRate;
        }
        else
        {
            Fee = Fee * 2 / UnitRate;
        }
        Fee = JMath.Round1(Fee, this.XiaoShou);
        return Fee;
    }
    #endregion

    #region 其他方法
    //得到动车的满员人数
    public override int GetTotalPerson()
    {
        int total = 0;
        DataTable dt = HighTrainProfile.Data;
        DataRow[] drs = dt.Select("HighTrainType='" + this.TrainType.ToString() + "'");
        if (drs != null && drs.Length > 0)
        {
            DataRow dr1 = drs[0];
            if (dr1 != null)
            {
                for (int i = 1; i <= 4; i++)
                {
                    total = total + int.Parse(dr1["PCount" + i].ToString());
                }
            }
        }
        return total;
    }
    
    //计算列车的重量
    private double GetTrainWeight()
    {
        double fee = 0;
        DataRow[] drs = HighTrainProfile.Data.Select("HIGHTRAINTYPE='" + this.TrainType.ToString() + "'");
        if (drs != null && drs.Length > 0)
        {
            DataRow dr = drs[0];
            fee = double.Parse(dr["WEIGHT"].ToString());
        }
        if (this.BianZhu == EHighTrainBianZhu.重联 && this.isDongWu == false)
        {
            fee = fee * 2;
        }
        return fee;
    }

    //判断是否为重联的动车
    private static bool IsUnionHighTrain(ETrainType type1)
    {
        if (type1 == ETrainType.动车CRH2B
                || type1 == ETrainType.动车CRH2E
                || type1 == ETrainType.动车CRH380AL
                || type1 == ETrainType.动车CRH380BL)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private static bool Is300Train(ETrainType type1)
    {
        if (type1 == ETrainType.动车CRH2C
            || type1 == ETrainType.动车CRH380A
            || type1 == ETrainType.动车CRH380AL
            || type1 ==ETrainType.动车CRH380B
            || type1 ==ETrainType.动车CRH380BL )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //得到运行是时间（4月24日重新调整了算法）
    public override double GetRunHour()
    {
        double hour1 = 0;
        DataRow[] drs1 = HighTrainProfile.Data.Select("HIGHTRAINTYPE='" + this.TrainType.ToString() + "'");
        if (drs1 != null && drs1.Length > 0)
        {
            DataRow dr1 = drs1[0];
            double speed = 0;
            double speed2 = 0;
            double speed3 = 0;

            if (String.IsNullOrEmpty(dr1["SPEED"].ToString()) == false)
            {
                speed = double.Parse(dr1["SPEED"].ToString());
                speed2 = double.Parse(dr1["SPEED2"].ToString());
                speed3 = double.Parse(dr1["SPEED3"].ToString());
            }

            if (this.trainType == EHighTrainType.CRH2A
                || this.trainType == EHighTrainType.CRH2E
                || this.trainType == EHighTrainType.CRH2B
                || this.trainType == EHighTrainType.CRH5A)
            {
                double hour0 = 0;
                if (this.Line != null && this.Line.Nodes.Count > 0)
                {
                    foreach (LineNode node1 in this.Line.Nodes)
                    {
                        double sp = 0;
                        if (node1.LineType == "1")
                        {
                            sp = speed3;
                        }
                        else if (node1.LineType == "2")
                        {
                            sp = speed2;
                        }
                        else
                        {
                            sp = speed;
                        }
                        if (speed > 0)
                        {
                            hour0 = hour0 + node1.Miles / sp;
                        }
                    }
                }
                hour1 = hour0;
            }
            else  //300公里的高速车
            {
                if (speed3 > 0)
                {
                    hour1 = this.YunXingLiCheng / speed3;
                }
            }
            
        }
        return hour1;
    }
    #endregion
}
}
