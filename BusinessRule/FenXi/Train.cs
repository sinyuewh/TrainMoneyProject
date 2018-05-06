using System;
using System.Collections.Generic;

using System.Text;
using System.Data;

using WebFrame;
using WebFrame.Data;
using WebFrame.Util;
using WebFrame.Designer;
using WebFrame.ExpControl;
using System.Web.UI.WebControls;

namespace BusinessRule
{
    /// <summary>
    /// 新开车模式
    /// </summary>
    public enum ECunZengMoShi
    {
        新人新车,新人有车,有人新车,有人有车
    }

    /// <summary>
    /// 所有车的类型定义
    /// </summary>
    public enum ETrainType
    {
        绿皮车25B, 空调车25K, 空调车25G, 空调车25T,
        动车CRH2A, 动车CRH2E, 动车CRH2C, 动车CRH380A, 动车CRH380AL,
        动车CRH2B, 动车CRH5A, 动车CRH380B, 动车CRH380BL
    }

    /// <summary>
    /// 普通车类型定义
    /// </summary>
    public enum ECommTrainType
    {
        绿皮车25B, 空调车25K, 空调车25G, 空调车25T
    }

    /// <summary>
    /// 列车大类
    /// </summary>
    public enum ETrainBigKind
    {
        普通列车, 动车
    }

    /// <summary>
    /// 服务方式
    /// </summary>
    public enum EServerPerson
    {
        一人1车, 一人2车, 二人3车
    }

    /// <summary>
    /// 费用的枚举值
    /// </summary>
    public enum EFeeKind
    {
        局外线路使用费 = 1, 机车牵引费, 电网和接触网使用费, 售票服务费, 旅客服务费, 列车上水费,
        人员工资和工资附加费, 车辆折旧费,列车日常检修成本, 定期检修成本, 车辆消耗备用品, 空调车用油,
        人员其他费用, 购买车辆利息, 轮渡费, 间接费用分摊
    }


    /// <summary>
    /// 支出的数据结构
    /// </summary>
    [Serializable]
    public class ZhiChuData:IComparable
    {
        public int ID { get; set; }
        public String ZhiChuName { get; set; }
        public double ZhiChu { get; set; }

        public ZhiChuData(int id, String zhichuName, double zhichu)
        {
            this.ID = id;
            this.ZhiChuName = zhichuName;
            this.ZhiChu = zhichu;
        }

        #region IComparable 成员
        public int CompareTo(object obj)
        {
            if (obj is ZhiChuData)
            {
                ZhiChuData data2 = obj as ZhiChuData;
                return this.ZhiChu.CompareTo(data2.ZhiChu);
            }
            else
            {
                throw new NotImplementedException("系统异常错误，比较的对象不正确！");
            }
        }
        #endregion
    }

    /// <summary>
    /// 列车的抽象类
    /// </summary>
    public abstract class Train
    {
        #region 属性设置
        protected double JnRate = 0.55;                 //局内分配比例(用户可调整）
        protected const double UnitRate = 10000;        //数据单位
        public double JnSaleFee
        {
            get;
            private set;
        }

        //表示是按年还是按趟的处理方式
        protected  int XiaoShou = 0;       //保留的小数位
        private bool isYearFlag = true;
        public bool IsYearFlag
        {
            get
            {
                return this.isYearFlag;
            }
            set
            {
                this.isYearFlag = value;
                if (this.isYearFlag == false)
                {
                    this.XiaoShou = 2;
                }
                else
                {
                    this.XiaoShou = 0;
                }
            }
        }

        //表示是全成本还是部分成本
        public bool IsFullChengBen
        {
            get;
            set;
        }

        public double JnServerFee
        {
            get;
            private set;
        }

        //新开车的模式
        private ECunZengMoShi cunZengMoShi = ECunZengMoShi.新人新车;
        public ECunZengMoShi CunZengMoShi
        {
            get { return this.cunZengMoShi; }
            set { this.cunZengMoShi = value; }
        }

        //运行里程
        private int yunXingLiCheng = 0;
        public virtual  int YunXingLiCheng
        {
            get { return this.yunXingLiCheng; }
            set {
                if (this.line0 == null)
                {
                    this.yunXingLiCheng = value;
                }
            }
        }
 
        private TrainLine line0 = null;
        public virtual TrainLine Line
        {
            set
            {
                if (value != null)
                {
                    this.line0 = value;
                    this.yunXingLiCheng = value.TotalMiles;
                    if (value.Nodes.Count > 0)
                    {
                        LineNode node1 = value.Nodes[0];
                        if (node1.JnFlag.Trim() != String.Empty)
                        {
                            this.JnFlag = true;
                        }
                        else
                        {
                            this.JnFlag = false;
                        }

                        //设置线路的牵引费类型
                        SetLineQianYin(this.line0);
                    }
                }
            }
            get
            {
                return this.line0;
            }
        }

        //局内线路标志
        private bool jnFlag = false;
        public bool JnFlag
        {
            get
            {
                return this.jnFlag;
            }
            protected set
            {
                this.jnFlag = value;
            }
        }

        //局内线路使用费
        private double jnFee;
        public double JnFee
        {
            get{
                return this.jnFee;
            }
            protected set
            {
                this.jnFee = value;
            }
        }

        //上水站数量和标准
        private int waterCount = 2;
        public int WaterCount
        {
            get
            {
                return this.waterCount;
            }
            set
            {
                this.waterCount = value;
            }
        }
        

        //列车的大类（是普通列车还是动车）
        protected ETrainBigKind TrainBigKind { get; set; }

        //服务方式
        private EServerPerson serverPerson = EServerPerson.一人2车;
        public EServerPerson ServerPerson
        {
            get { return this.serverPerson; }
            set { this.serverPerson = value; }
        }

       
        //车底数
        private double cheDiShu = 2;
        public double CheDiShu 
        {
            get { return this.cheDiShu; }
            set { this.cheDiShu = value; }
        }
        #endregion

        #region 列车的收入
        //计算收入
        public abstract double GetShouRu();
        #endregion

        #region 列车支出的各项费用

        //计算支出  原来计算公式
        public virtual List<ZhiChuData> GetZhiChu(String FindCond)
        {
            String[] findcondarray = FindCond.Replace("&&", "&").Split('&');

            List<ZhiChuData> list1 = new List<ZhiChuData>();

            int i = 0;

            if (FindCond.ToString().Trim() == "")
            {
                list1.Add(new ZhiChuData(1, ((EFeeKind)1).ToString(), GetFee1()));
                list1.Add(new ZhiChuData(2, ((EFeeKind)2).ToString(), GetFee2()));
                list1.Add(new ZhiChuData(3, ((EFeeKind)3).ToString(), GetFee3()));//电网和接地费用
                list1.Add(new ZhiChuData(4, ((EFeeKind)4).ToString(), GetFee4()));
                list1.Add(new ZhiChuData(5, ((EFeeKind)5).ToString(), GetFee5()));
                list1.Add(new ZhiChuData(6, ((EFeeKind)6).ToString(), GetFee6()));
                list1.Add(new ZhiChuData(7, ((EFeeKind)7).ToString(), GetFee7()));
                list1.Add(new ZhiChuData(8, ((EFeeKind)8).ToString(), GetFee8()));
                list1.Add(new ZhiChuData(9, ((EFeeKind)9).ToString(), GetFee9()));
                list1.Add(new ZhiChuData(10, ((EFeeKind)10).ToString(), GetFee10()));
                list1.Add(new ZhiChuData(11, ((EFeeKind)11).ToString(), GetFee11()));
                list1.Add(new ZhiChuData(12, ((EFeeKind)12).ToString(), GetFee12()));
                list1.Add(new ZhiChuData(13, ((EFeeKind)13).ToString(), GetFee13()));
                list1.Add(new ZhiChuData(14, ((EFeeKind)14).ToString(), GetFee14()));
                list1.Add(new ZhiChuData(15, ((EFeeKind)15).ToString(), GetFee15()));
                list1.Add(new ZhiChuData(16, ((EFeeKind)16).ToString(), GetFee16()));
            }
            else
            {
                for (String arrayitem = findcondarray[i]; i < findcondarray.Length; ++i)
                {
                    arrayitem = findcondarray[i];

                    switch (arrayitem)
                    {
                        case "1":
                            list1.Add(new ZhiChuData(1, ((EFeeKind)1).ToString(), GetFee1()));
                            break;

                        case "2":
                            list1.Add(new ZhiChuData(2, ((EFeeKind)2).ToString(), GetFee2()));
                            break;

                        case "3":
                            list1.Add(new ZhiChuData(3, ((EFeeKind)3).ToString(), GetFee3()));
                            break;

                        case "4":
                            list1.Add(new ZhiChuData(4, ((EFeeKind)4).ToString(), GetFee4()));
                            break;

                        case "5":
                            list1.Add(new ZhiChuData(5, ((EFeeKind)5).ToString(), GetFee5()));
                            break;

                        case "6":
                            list1.Add(new ZhiChuData(6, ((EFeeKind)6).ToString(), GetFee6()));
                            break;

                        case "7":
                            list1.Add(new ZhiChuData(7, ((EFeeKind)7).ToString(), GetFee7()));
                            break;

                        case "8":
                            list1.Add(new ZhiChuData(8, ((EFeeKind)8).ToString(), GetFee8()));
                            break;

                        case "9":
                            list1.Add(new ZhiChuData(9, ((EFeeKind)9).ToString(), GetFee9()));
                            break;

                        case "10":
                            list1.Add(new ZhiChuData(10, ((EFeeKind)10).ToString(), GetFee10()));
                            break;

                        case "11":
                            list1.Add(new ZhiChuData(11, ((EFeeKind)11).ToString(), GetFee11()));
                            break;

                        case "12":
                            list1.Add(new ZhiChuData(12, ((EFeeKind)12).ToString(), GetFee12()));
                            break;

                        case "13":
                            list1.Add(new ZhiChuData(13, ((EFeeKind)13).ToString(), GetFee13()));
                            break;

                        case "14":
                            list1.Add(new ZhiChuData(14, ((EFeeKind)14).ToString(), GetFee14()));
                            break;

                        case "15":
                            list1.Add(new ZhiChuData(15, ((EFeeKind)15).ToString(), GetFee15()));
                            break;

                        case "16":
                            list1.Add(new ZhiChuData(16, ((EFeeKind)16).ToString(), GetFee16()));
                            break;

                        default:
                            break;
                    }
                }
            }

            return list1;
        }

        /// <summary>
        /// 2014 04 17 计算含有客专路线（非默认）的支出
        /// </summary>
        /// <param name="FindCond"></param>
        /// <param name="lineid"></param>
        /// <returns></returns>

        public virtual List<ZhiChuData> GetZhiChu(String FindCond, List<string[]> lineInfos)
        {
            String[] findcondarray = FindCond.Replace("&&", "&").Split('&');

            List<ZhiChuData> list1 = new List<ZhiChuData>();

            int i = 0;

            if (FindCond.ToString().Trim() == "")
            {
                list1.Add(new ZhiChuData(1, ((EFeeKind)1).ToString(), GetFee1()));
                list1.Add(new ZhiChuData(2, ((EFeeKind)2).ToString(), GetFee2()));
                list1.Add(new ZhiChuData(3, ((EFeeKind)3).ToString(), GetFee3(lineInfos)));//电网和接地费用
                list1.Add(new ZhiChuData(4, ((EFeeKind)4).ToString(), GetFee4()));
                list1.Add(new ZhiChuData(5, ((EFeeKind)5).ToString(), GetFee5()));
                list1.Add(new ZhiChuData(6, ((EFeeKind)6).ToString(), GetFee6()));
                list1.Add(new ZhiChuData(7, ((EFeeKind)7).ToString(), GetFee7()));
                list1.Add(new ZhiChuData(8, ((EFeeKind)8).ToString(), GetFee8()));
                list1.Add(new ZhiChuData(9, ((EFeeKind)9).ToString(), GetFee9()));
                list1.Add(new ZhiChuData(10, ((EFeeKind)10).ToString(), GetFee10()));
                list1.Add(new ZhiChuData(11, ((EFeeKind)11).ToString(), GetFee11()));
                list1.Add(new ZhiChuData(12, ((EFeeKind)12).ToString(), GetFee12()));
                list1.Add(new ZhiChuData(13, ((EFeeKind)13).ToString(), GetFee13()));
                list1.Add(new ZhiChuData(14, ((EFeeKind)14).ToString(), GetFee14()));
                list1.Add(new ZhiChuData(15, ((EFeeKind)15).ToString(), GetFee15()));
                list1.Add(new ZhiChuData(16, ((EFeeKind)16).ToString(), GetFee16()));
            }
            else
            {
                for (String arrayitem = findcondarray[i]; i < findcondarray.Length; ++i)
                {
                    arrayitem = findcondarray[i];

                    switch (arrayitem)
                    {
                        case "1":
                            list1.Add(new ZhiChuData(1, ((EFeeKind)1).ToString(), GetFee1()));
                            break;

                        case "2":
                            list1.Add(new ZhiChuData(2, ((EFeeKind)2).ToString(), GetFee2()));
                            break;

                        case "3":
                            list1.Add(new ZhiChuData(3, ((EFeeKind)3).ToString(), GetFee3(lineInfos)));
                            break;

                        case "4":
                            list1.Add(new ZhiChuData(4, ((EFeeKind)4).ToString(), GetFee4()));
                            break;

                        case "5":
                            list1.Add(new ZhiChuData(5, ((EFeeKind)5).ToString(), GetFee5()));
                            break;

                        case "6":
                            list1.Add(new ZhiChuData(6, ((EFeeKind)6).ToString(), GetFee6()));
                            break;

                        case "7":
                            list1.Add(new ZhiChuData(7, ((EFeeKind)7).ToString(), GetFee7()));
                            break;

                        case "8":
                            list1.Add(new ZhiChuData(8, ((EFeeKind)8).ToString(), GetFee8()));
                            break;

                        case "9":
                            list1.Add(new ZhiChuData(9, ((EFeeKind)9).ToString(), GetFee9()));
                            break;

                        case "10":
                            list1.Add(new ZhiChuData(10, ((EFeeKind)10).ToString(), GetFee10()));
                            break;

                        case "11":
                            list1.Add(new ZhiChuData(11, ((EFeeKind)11).ToString(), GetFee11()));
                            break;

                        case "12":
                            list1.Add(new ZhiChuData(12, ((EFeeKind)12).ToString(), GetFee12()));
                            break;

                        case "13":
                            list1.Add(new ZhiChuData(13, ((EFeeKind)13).ToString(), GetFee13()));
                            break;

                        case "14":
                            list1.Add(new ZhiChuData(14, ((EFeeKind)14).ToString(), GetFee14()));
                            break;

                        case "15":
                            list1.Add(new ZhiChuData(15, ((EFeeKind)15).ToString(), GetFee15()));
                            break;

                        case "16":
                            list1.Add(new ZhiChuData(16, ((EFeeKind)16).ToString(), GetFee16()));
                            break;

                        default:
                            break;
                    }
                }
            }

            return list1;
        }

        //Fee1:线路使用费--OK
        internal abstract double GetFee1();

        //Fee2:机车牵引费---OK
        internal abstract double GetFee2();

        //Fee3:电网和接触网使用费---OK
        internal abstract double GetFee3();

        //2014 04 17 客专电网和接地网使用费
        internal abstract double GetFee3(List<string[]> lineinfos);

        //Fee4:售票服务费---OK
        internal virtual double GetFee4()
        {
            double fee = 0;
            fee = this.GetShouRu() * this.SaleRate / 100d;
            fee = fee / UnitRate;
           
            if (this.JnFlag == false)
            {
                return JMath.Round1(fee,this.XiaoShou);
            }
            else
            {
                if (this.Line != null && this.Line.Nodes.Count > 0)
                {
                    //起点和终点全部是局内的
                    if (String.IsNullOrEmpty(this.Line.Nodes[this.Line.Nodes.Count - 1].JnFlag) == false)
                    {
                        this.JnSaleFee = JMath.Round1(fee, this.XiaoShou);

                        //区分是变动成本还是全成本
                        if (this.IsFullChengBen == false)
                        {
                            return 0;
                        }
                        else
                        {
                            return this.JnSaleFee;
                        }
                    }
                    else
                    {
                        this.JnSaleFee = JMath.Round1(fee * (1 - JnRate), this.XiaoShou);

                        //区分是变动成本还是全成本（5月14日修改）
                        if (this.IsFullChengBen == false)
                        {
                            return JMath.Round1(fee * JnRate, this.XiaoShou);
                        }
                        else
                        {
                            return JMath.Round1(fee * JnRate, this.XiaoShou)
                                + JMath.Round1(fee * (1 - JnRate), this.XiaoShou);
                        }
                    }
                }
                else
                {
                    return JMath.Round1(fee, this.XiaoShou);
                }
            }
        }

        //Fee5：旅客服务费---OK
        internal virtual double GetFee5()
        {
            double fee = 0;
            double p1 = 0;
            if (this.isYearFlag)
            {
                p1 = this.GetTotalPerson() * 2 * 365;
            }
            else
            {
                p1 = this.GetTotalPerson() * 2;
            }

            fee=p1*this.ServiceRate/UnitRate ;
            if (this.JnFlag == false)
            {
                return JMath.Round1(fee,this.XiaoShou);
            }
            else
            {
                if (this.Line != null && this.Line.Nodes.Count > 0)
                {
                    //起点和终点全部是局内的
                    if (String.IsNullOrEmpty(this.Line.Nodes[this.Line.Nodes.Count - 1].JnFlag) == false)
                    {
                        this.JnServerFee = JMath.Round1(fee, this.XiaoShou);
                        //区分是变动成本还是全成本（5月14日修改）
                        if (this.IsFullChengBen == false)
                        {
                            return 0;
                        }
                        else
                        {
                            return this.JnServerFee;
                        }
                    }
                    else
                    {
                        this.JnServerFee = JMath.Round1(fee * (1 - JnRate), this.XiaoShou);
                        //区分是变动成本还是全成本（5月14日修改）
                        if (this.IsFullChengBen == false)
                        {
                            return JMath.Round1(fee * JnRate, this.XiaoShou);
                        }
                        else
                        {
                            return JMath.Round1(fee * JnRate, this.XiaoShou)
                                + JMath.Round1(fee * (1 - JnRate), this.XiaoShou);
                        }
                    }
                }
                else
                {
                    return JMath.Round1(fee, this.XiaoShou);
                }
            }
        }

        //Fee6：列车上水费---OK
        internal virtual double GetFee6()
        {
            double fee = 0;
            if (this.IsYearFlag)
            {
                fee = this.WaterCount * this.WaterRate * 2 * 365;
            }
            else
            {
                fee = this.WaterCount * this.WaterRate * 2;
            }
            fee = fee / UnitRate;
            return JMath.Round1(fee,0);
        }

        //Fee7：人员工资和工资附加费--OK
        internal abstract double GetFee7();
        
        //Fee8:车辆折旧费--OK
        internal abstract double GetFee8();

        //Fee9：列车日常检修成本--OK
        internal abstract double GetFee9();

        //Fee10：定期检修成本---OK
        internal abstract double GetFee10();

        //Fee11：车辆消耗备用品---OK
        internal abstract double GetFee11();

        //Fee12：空调车用油---OK
        internal abstract double GetFee12();

        //Fee13：人员其他费用---OK
        internal abstract double GetFee13();
        
        //Fee14：购买车辆利息---OK
        internal abstract double GetFee14();

        //Fee15：轮渡费
        public abstract double GetFee15();

        //Fee15：间接费用分摊
        public abstract double GetFee16();
        #endregion

        #region 其他方法
        /// <summary>
        /// 得到新车的分析数据
        /// </summary>
        /// <returns></returns>
        public static Dictionary<String, String> GetNewFenXiData(int Flag)
        {
            /*---此处需要修改-------*/
            Dictionary<String, String> data1 = new Dictionary<string, string>();
            if (Flag == 0 || Flag == 2)
            {
                data1.Add("6", "高铁CRH2C");
                data1.Add("4", "动车CRH2A");
                //data1.Add("7", "高铁CRH380A");
            }

            if (Flag ==0 || Flag==1)
            {
                data1.Add("3", "直达25T");
                data1.Add("2", "空调25G(直供电)");
                data1.Add("21", "空调25G(非直供电)");
                data1.Add("1", "空调25K");
                data1.Add("0", "绿皮车25B");
            }
            return data1;
        }


        /// <summary>
        /// 得到新车的分析数据
        /// </summary>
        /// <returns></returns>
        public static Dictionary<String, String> GetNewFenXiData2013(int Flag)
        {
            /*---此处需要修改-------*/
            Dictionary<String, String> data1 = new Dictionary<string, string>();
            if (Flag == 0 || Flag == 2)
            {
                data1.Add("6", "高铁CRH2C");
                data1.Add("4", "动车CRH2A");
                //data1.Add("7", "高铁CRH380A");
            }

            if (Flag == 0 || Flag == 1)
            {
                data1.Add("3", "直达25T");
                data1.Add("2", "空调25G(直供电)");
                data1.Add("21", "空调25G(非直供电)");
                //data1.Add("1", "空调25K");
                data1.Add("0", "绿皮车25B");
            }
            return data1;
        }

        //设置动车的类型
        /// <summary>
        /// 设置动车的类型
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="flag"></param>
        public static void SetDongCheListControl(ListControl list1, int flag)
        {
            String[] name1=null;
            String[] values1=null;
            if (flag == 1)
            {
                /*----------车型的调整此处需要修改------------*/
                name1 = new String[] { "动车CRH2C", "动车CRH380A", "动车CRH380AL", "动车CRH380B", "动车CRH380BL" };
                values1 = new String[] { "6", "7", "8","11","12" };
            }
            else
            {
                name1 = new String[] { "动车CRH2A", "动车CRH2E", "动车CRH2B", "动车CRH5A" };
                values1 = new String[] { "4", "5", "9","10" };
            }
            for (int i = 0; i < name1.Length; i++)
            {
                ListItem item1 = new ListItem(name1[i], values1[i]);
                list1.Items.Add(item1);
            }
        }

        public static void SetCommCheListControl(ListControl list1, int flag)
        {
            String[] name1 = null;
            String[] values1 = null;
            if (flag == 1)
            {
                /*----------车型的调整此处需要修改------------*/
                name1 = new String[] { "空调25G(直供电)", "空调25K(直供电)"};
                values1 = new String[] { "2", "1" };
            }
            else
            {
                name1 = new String[] { "空调25G(非直供电)", "空调25K(非直供电)" };
                values1 = new String[] { "2", "1" };
            }

            for (int i = 0; i < name1.Length; i++)
            {
                ListItem item1 = new ListItem(name1[i], values1[i]);
                list1.Items.Add(item1);
            }
        }

        //得到列车的满员人数
        public abstract int GetTotalPerson();


        //售票服务费的标准
        protected double SaleRate
        {
            get
            {
                if (this.TrainBigKind == ETrainBigKind.普通列车)
                {
                    return TrainProfile.SaleRateForComm;
                }
                else
                {
                    return TrainProfile.SaleRateForHigh;
                }
            }
        }

        //旅客服务标准
        private int ServiceRate
        {
            get
            {
                if (this.TrainBigKind == ETrainBigKind.普通列车)
                {
                    return TrainProfile.ServiceRateForComm;
                }
                else
                {
                    return TrainProfile.ServiceRateForHigh;
                }
            }
        }

        //水站服务费标准
        private double WaterRate
        {
            get
            {
                if (this.TrainBigKind == ETrainBigKind.普通列车)
                {
                    return TrainProfile.WaterRateForComm;
                }
                else
                {
                    return TrainProfile.WaterRateForHigh;
                }
            }
        }

        //设置线路的牵引费类型
        private static void SetLineQianYin(TrainLine line)
        {
            for (int i = 0; i < line.Nodes.Count; i++)
            {
                line.Nodes[i].QianYinType = EQianYinType.其他;      //设置为初值
            }

            int pos0 = 0;
            int pos2 = line.Nodes.Count - 1;
            while (pos0 < pos2)
            {
                int pos1 = -1;
                for (int i = pos0+1; i <= pos2; i++)
                {
                    if (line.Nodes[i].BigB)
                    {
                        pos1 = i;
                        break;
                    }
                }

                if (pos1 == -1) pos1 = pos2;

                //其中有一段时内燃机车，则使用内燃机车
                EQianYinType type1 = EQianYinType.电力机车;
                for (int i = pos0; i <= pos1; i++)
                {
                    if (String.IsNullOrEmpty(line.Nodes[i].DqhFlag))
                    {
                        type1 = EQianYinType.内燃机车;
                        break;
                    }
                }
                for (int i = pos0; i <= pos1; i++)
                {
                    line.Nodes[i].QianYinType = type1;
                }

                //重新设置Pos1的值
                pos0 = pos1;  
            }

            for (int i = 0; i < line.Nodes.Count; i++)
            {
                if (line.Nodes[i].QianYinType == EQianYinType.其他)
                {
                    line.Nodes[i].QianYinType = EQianYinType.电力机车;
                }
            }
        }

        //得到运行时间
        public abstract double GetRunHour();
        #endregion
    }
}
