using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using WebFrame.Data;
using WebFrame;
using System.Web;
using System.IO;
using WebFrame.Util;
using System.Configuration;

namespace BusinessRule
{
    //分段检索条件
    [Serializable]
    public class MulDuanData
    {
        public String AStation{get;set;}
        public String BStation{get;set;}
        public int MaxLayer{get;set;}
        public String trainID { get; set; }
    }
    /// <summary>
    /// 线路类型定义
    /// </summary>
    public enum ELineType
    {
        特一类 = 1, 特二类,
        一类上浮, 一类, 二类上浮,
        二类, 三类, 三类下浮
    }

    //线路节点定义
    [Serializable]
    public class LineNode
    {
        public String AStation { get; set; }
        public String BStation { get; set; }
        public int Miles { get; set; }
        public String LineID { get; set; }
        public String LineType { get; set; }
        public String JnFlag{ get; set; }
        public String DqhFlag { get; set; }

        public String ShipFlag { get; set; }
        public String Gtllx { get; set; }       //高铁联络线

        public int Fee1 { get; set; }           //内燃牵引费
        public int Fee2 { get; set; }           //电力牵引费 

        public bool BigA { get; set; }
        public bool BigB { get; set; }

        //节点的牵引类型
        private  EQianYinType qianyinType = EQianYinType.电力机车;
        public EQianYinType QianYinType
        {
            get
            {
                return this.qianyinType;
            }
            set
            {
                this.qianyinType = value;
            }
        }

        public List<String> passedstation = new List<string>();
        public List<String> PassedStation
        {
            get
            {
                return this.passedstation;
            }
        }

        public String PassedStationString
        {
            get
            {
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < this.PassedStation.Count; i++)
                {
                    if (i == 0)
                    {
                        result.Append(this.PassedStation[i]);
                    }
                    else
                    {
                        result.Append(",").Append(this.PassedStation[i]);
                    }
                }
                return result.ToString();
            }
        }

        public void SetGoneStation(String gonestation)
        {
            String[] arr1 = gonestation.Split(',');
            foreach (String m in arr1)
            {
                if (this.PassedStation.Contains(m) == false)
                {
                    this.PassedStation.Add(m);
                }
            }
        }

        private List<String> parent = new List<String>();
        public List<String> Parent
        {
            get { return this.parent; }
        }

        
        public String ID { get; set; }
        public bool Used { get; set; }
        public bool Selected { get; set; }

        public override string ToString()
        {
            return String.Format("{0}-{1}/{2}-{3}", this.AStation, this.BStation, this.Parent, this.ID);
        }
    }

    //火车线路定义
    [Serializable]
    public class TrainLine : IComparable
    {
        public bool compleQianYinFeeCal = false;

        private List<LineNode> trainLine = new List<LineNode>();

        //线点类型
        public bool HighLine { get; set; }

        //线路是否选择的标志位
        private bool select = true;
        public bool Select
        {
            get { return this.select; }
            set { this.select = value; }
        }

        //线路中的站点列表
        public List<LineNode> Nodes
        {
            get
            {
                return this.trainLine;
            }
        }

        /// <summary>
        /// 返回总距离
        /// </summary>
        public int TotalMiles
        {
            get
            {
                int result = 0;
                foreach (LineNode node1 in this.Nodes)
                {
                    result = result + node1.Miles;
                }
                return result;
            }
        }

        /// <summary>
        /// 得到所有站点的数组
        /// </summary>
        public String[] ArrStation
        {
            get
            {
                return this.ToString().Split('-');
            }
        }


        public override string ToString()
        {
            StringBuilder str1 = new StringBuilder();
            for (int i = 0; i < this.Nodes.Count; i++)
            {
                if (str1.Length == 0)
                {
                    str1.Append(this.Nodes[i].AStation);
                }
                else
                {
                    str1.Append("-").Append(this.Nodes[i].AStation);
                }

                if (i == this.Nodes.Count - 1)
                {
                    str1.Append("-").Append(this.Nodes[i].BStation);
                }
            }
            return str1.ToString();
        }

        public string ToBigString()
        {
            StringBuilder str1 = new StringBuilder();
            for (int i = 0; i < this.Nodes.Count; i++)
            {
                if (str1.Length == 0)
                {
                    str1.Append(this.Nodes[i].AStation);
                }
                else
                {
                    if (this.Nodes[i].BigA)
                    {
                        str1.Append("-").Append(this.Nodes[i].AStation);
                    }
                }

                if (i == this.Nodes.Count - 1)
                {
                    str1.Append("-").Append(this.Nodes[i].BStation);
                }
            }
            return str1.ToString();
        }

        #region IComparable 成员
        public int CompareTo(object obj)
        {
            if (obj is TrainLine)
            {
                TrainLine lineTemp = obj as TrainLine;
                return this.TotalMiles.CompareTo(lineTemp.TotalMiles);
            }
            else
            {
                throw new NotImplementedException("系统异常错误，比较的对象不正确！");
            }
        }
        #endregion

        //线路进行夏冬切换
        public static void ExchangeSpringAndWinter()
        {
            int month1 = DateTime.Now.Month;
            String sql = String.Empty;
            if (month1 >= 4 && month1 <= 9)   //夏季
            {
                sql = "update trainline set linetype=1 where linetype=2 and SpringWinter is not null  ";
            }
            else                              //冬季
            {
                sql = "update trainline set linetype=2 where linetype=1 and SpringWinter is not null";        
            }
            JCommand comm1 = new JCommand();
            comm1.CommandText = sql;
            comm1.ExecuteNoQuery();
            comm1.Close();
        }

        public static TrainLine operator +(TrainLine line1, TrainLine line2)
        {
            List<LineNode> lineNodes1 = line1.Nodes;
            List<LineNode> lineNodes2 = line2.Nodes;
            TrainLine line = new TrainLine();
            foreach (LineNode node1 in lineNodes1)
            {
                line.Nodes.Add(node1);
            }

            foreach (LineNode node2 in lineNodes2)
            {
                line.Nodes.Add(node2);
            }
            return line;
        }

        //设置线路牵引费
        public void SetQianYinFee(ECommTrainType type1,EGongDianType gongdian1)
        {
            if (this.compleQianYinFeeCal == false)
            {
                String firstA = String.Empty;
                String firstB = String.Empty;
                int pos1 = -1;
                int pos2 = -1;

                JTable tab1 = new JTable("bigstationview");
                for (int i = 0; i < this.Nodes.Count; i++)
                {
                    if (this.Nodes[i].BigA)
                    {
                        firstA = this.Nodes[i].AStation;
                        pos1 = i;
                    }

                    if (this.Nodes[i].BigB)
                    {
                        firstB = this.Nodes[i].BStation;
                        pos2 = i;
                    }

                    if (pos1 != -1 && pos2 != -1)
                    {
                        String first = String.Empty;

                        this.SetQianYinFee(tab1, firstB, pos1, pos2);
                        firstA = String.Empty;
                        firstB = String.Empty;
                        pos1 = -1;
                        pos2 = -1;
                    }
                }

                if (pos1 > 0 && pos2 == -1 && String.IsNullOrEmpty(firstA) == false)
                {
                    this.SetQianYinFee(tab1, firstA, pos1, this.Nodes.Count - 1);
                }
                tab1.Close();

                //设置线路的牵引类型和默认的牵引费
                this.SetQianYinType();

                //完善所有的牵引费
                this.SetDefaultQianYinFee(type1, gongdian1);
                this.compleQianYinFeeCal = true;
            }
        }

        //设置线路的牵引费等系列（利用大站点来设置）
        private void  SetQianYinFee(JTable tab,String BStation, int posA, int PosB)
        {
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("name1", BStation));
            DataRow dr1 = tab.GetFirstDataRow(condition, "*");
            if (dr1 != null)
            {
                for (int i = posA; i <= PosB; i++)
                {
                    if (String.IsNullOrEmpty(dr1["fee1"].ToString()) == false
                        && int.Parse(dr1["fee1"].ToString()) > 0)
                    {
                        if (this.Nodes[i].Fee1 == 0)
                        {
                            this.Nodes[i].Fee1 = int.Parse(dr1["fee1"].ToString());
                        }
                    }

                    if (String.IsNullOrEmpty(dr1["fee2"].ToString()) == false
                        && int.Parse(dr1["fee2"].ToString()) > 0)
                    {
                        if (this.Nodes[i].Fee2 == 0)
                        {
                            this.Nodes[i].Fee2 = int.Parse(dr1["fee2"].ToString());
                        }
                    }
                }
            }
        }

        //设置线路的默认牵引类型
        private void SetQianYinType()
        {
            int aPos = 0;
            int bPos = 0;

            if (this.Nodes.Count > 0)
            {
                while (aPos < this.Nodes.Count)
                {
                    bPos = -1;
                    for (int j = aPos + 1; j < this.Nodes.Count; j++)
                    {
                        if (this.Nodes[j].BigA)
                        {
                            bPos = j;
                            break;
                        }
                    }
                    if (bPos == -1) { bPos = this.Nodes.Count; }


                    EQianYinType qy1 = EQianYinType.电力机车;
                    for (int k = aPos; k < bPos; k++)
                    {
                        if (this.Nodes[k].DqhFlag.Trim() == String.Empty)
                        {
                            qy1 = EQianYinType.内燃机车;
                            break;
                        }
                    }

                    for (int k = aPos; k < bPos; k++)
                    {
                        this.Nodes[k].QianYinType = qy1;
                    }

                    aPos = bPos;
                }
            }
        }

        //设置线路的默认牵引费
        private void SetDefaultQianYinFee(ECommTrainType traintype, EGongDianType gongdian)
        {
            foreach (LineNode node1 in this.Nodes)
            {
                if (traintype == ECommTrainType.空调车25T)   //一站直达的机车牵引
                {
                    node1.Fee1 = (int)QianYinFeeProfile.Fee13 + TrainProfile.QianYinFjFee1;
                    node1.Fee2 = (int)QianYinFeeProfile.Fee03 + TrainProfile.QianYinFjFee2; 
                }
                else
                {
                    if (node1.Fee1 == 0)
                    {
                        node1.Fee1 = (int)QianYinFeeProfile.Fee12;
                    }
                    if (node1.Fee2 == 0)
                    {
                        node1.Fee2 = (int)QianYinFeeProfile.Fee02;
                    }

                    if (gongdian == EGongDianType.直供电)   //增加直供电附加费
                    {
                        node1.Fee1 = node1.Fee1 + TrainProfile.QianYinFjFee1;
                        node1.Fee2 = node1.Fee2 + TrainProfile.QianYinFjFee2;
                    }
                }
            }
        }
    }

  
    /// <summary>
    /// 列车线路模型
    /// </summary>
    [Serializable]
    public class Line
    {
        #region 属性和构造函数
        private int MaxLayer = 35;
        private String AStation = null;
        private String BStation = null;
        private String SelTrainLineID=String.Empty;
        private List<LineNode>[] LineLayer = null;                                      //表示所有的节点
        private List<String>[] LineAllStation = null;                                   //表示层经过的站点
        private bool OnlyGG = false;                                                    //表示只选择高铁线
        
        private int MAXLINECOUNT = 300;                                                 //最大的搜索线路条数
        private List<TrainLine> usedLine = new List<TrainLine>();
        
        public Line(String AStation, String BStation,int MaxLayer,String trainID,int maxLineCount,bool onlyGG)
        {
            if (String.IsNullOrEmpty(AStation) == false
                && String.IsNullOrEmpty(BStation) == false)
            {

                this.AStation = AStation;
                this.BStation = BStation;
                this.MaxLayer = MaxLayer;
                this.SelTrainLineID = trainID;
                this.OnlyGG = onlyGG;
                
                if (String.IsNullOrEmpty(trainID) == false)
                {
                    this.MaxLayer = 100;
                }
                this.MAXLINECOUNT = maxLineCount;

                //设置线路中的节点
                SetLayerNodesInit();
            }
        }

        /*
        public Line(String AStation, String BStation, int MaxLayer, String trainID) 
            : this(AStation, BStation, MaxLayer, trainID, 300) { }

        public Line(String AStation, String BStation):this(AStation, BStation,35,String.Empty){} 

         */

        private Line() { ;}

        #endregion
        #region 线路的业务方法
        //得到选择的线路的数组（分段检索）
        public static String[] GetLineArrByFengDuanSearch(
            String AStation,String BStation,
            String SearchCondition,
            ETrainType trainType,String MiddleStation,bool onlygg)
        {
            String[] result = null;
            Line line1 = null;

            SearchObjectBU.RestoreSearchResultFromDb(AStation,BStation,SearchCondition,out line1);
            if(line1 ==null)
            {
                if (String.IsNullOrEmpty(SearchCondition) == false)
                {
                    String[] arr1 = SearchCondition.Replace("，", ",").Split(',');
                    MulDuanData[] search1 = new MulDuanData[arr1.Length];
                    for (int i = 0; i < arr1.Length; i++)
                    {
                        search1[i] = new MulDuanData();
                        String[] t2 = arr1[i].Split('-');
                        search1[i].AStation = t2[0];
                        String[] t3 = t2[1].Split('(');
                        search1[i].BStation = t3[0];
                        String[] t4 = t3[1].Replace("(", "").Replace(")", "").Split('#');
                        search1[i].trainID = t4[0];
                        if (search1[i].trainID == "0") search1[i].trainID = String.Empty;
                        search1[i].MaxLayer = int.Parse(t4[1]);
                    }

                    if (search1 != null && search1.Length > 0)
                    {
                        line1 = GetLineByFengDuanSearch(search1,onlygg);
                    }
                    if (line1 != null)
                    {
                        SearchObjectBU.SaveSearchResultToDb(AStation, BStation,line1,SearchCondition);
                    }
                }

                if (line1 != null)
                {
                    line1.usedLine.Sort();
                    String[] middle = null;
                    if (String.IsNullOrEmpty(MiddleStation) == false)
                    {
                        String middleTemp = MiddleStation.Trim().Replace("，", ",");
                        middle = middleTemp.Split(',');
                    }

                    List<TrainLine> list1 = line1.GetLayerLine(middle, trainType, false, 300,onlygg);
                    if (list1 != null && list1.Count > 0)
                    {
                        result = new String[list1.Count];
                        for (int i = 0; i < list1.Count; i++)
                        {
                            result[i] = list1[i].ToString() + "(" + list1[i].TotalMiles + "公里)";
                        }
                    }
                }
            }
            return result;
        }

        // 得到选择线路的数组
        public static String[] GetLineArr(
            String AStation, String BStation,
            ETrainType trainType,String MiddleStation,bool onlygg)
        {
            String[] result = null;
            String[] middle = null;
            if (String.IsNullOrEmpty(MiddleStation)==false)
            {
                String middleTemp = MiddleStation.Trim().Replace("，", ",");
                middle = middleTemp.Split(',');
            }

            Line line1 = null;
            SearchObjectBU.RestoreSearchResultFromDb(AStation , BStation, "", out line1);
            if (line1 == null)
            {
                line1 = new Line(AStation, BStation, 100, "", 300,onlygg);
                if (line1 != null)
                {
                    SearchObjectBU.SaveSearchResultToDb(AStation, BStation, line1, "");
                }
            }

           
            List<TrainLine> list1 = line1.GetLayerLine(middle, trainType,false, 300,onlygg);
            if (list1 != null && list1.Count > 0)
            {
                result = new String[list1.Count];
                for (int i = 0; i < list1.Count; i++)
                {
                    result[i] = list1[i].ToString()+"("+list1[i].TotalMiles+"公里)";
                }
            }
            return result;
        }

        public static Line GetLine(String AStation, String BStation, int MaxLayer,
            String trainID,bool onlyGG)
        {
            return GetLine(AStation, BStation, MaxLayer, trainID,300,onlyGG);
        }
        /// <summary>
        /// 得到起点是别名的站点
        /// </summary>
        /// <param name="AStation"></param>
        /// <param name="BStation"></param>
        /// <param name="MaxLayer"></param>
        /// <param name="trainID"></param>
        /// <returns></returns>
        public static Line GetLine(String AStation, String BStation, int MaxLayer, 
            String trainID, int maxLineCount,bool onlyGG)
        {
            Line line0=new Line();
            TrainAliasBU t1 = new TrainAliasBU();
            
            List<String> All1=t1.GetAlias(AStation);
           // List<String> B1 = t1.GetAlias(BStation);
            foreach (String m in All1)
            {
                /*
                foreach(String m2 in B1)
                {
                    if (m2 != m)
                    {
                        Line line1 = new Line(m, m2, MaxLayer, trainID, maxLineCount);
                        foreach (TrainLine l1 in line1.usedLine)
                        {
                            line0.usedLine.Add(l1);
                        }
                    }
                }*/

                if (BStation != m)
                {
                    Line line1 = new Line(m, BStation, MaxLayer, trainID, maxLineCount,onlyGG);
                    foreach (TrainLine l1 in line1.usedLine)
                    {
                        line0.usedLine.Add(l1);
                    }
                }
            }
            line0.usedLine.Sort();
            return line0;
        }

        /// <summary>
        /// 得到分段的搜索
        /// </summary>
        /// <param name="SearchCondition"></param>
        /// <returns></returns>
        public static Line GetLineByFengDuanSearch(String SearchCondition,bool onlyGG)
        {
            Line line1 = null;
            if (String.IsNullOrEmpty(SearchCondition) == false)
            {
                if (SearchCondition.IndexOf(",") < 0)
                {
                    String temp = String.Empty;
                    bool first = true;
                    String[] arr2 = SearchCondition.Split('-');
                    for (int i = 0; i < arr2.Length - 1; i++)
                    {
                        if (first)
                        {
                            temp = arr2[i] + "-" + arr2[i + 1] + "(0#0)";
                            first = false;
                        }
                        else
                        {
                            temp = temp + "," + arr2[i] + "-" + arr2[i + 1] + "(0#0)";
                        }
                    }
                    SearchCondition = temp;
                }

                //得到条件
                String[] arr1 = SearchCondition.Replace("，", ",").Split(',');
                MulDuanData[] search1=new MulDuanData[arr1.Length];
                for (int i = 0; i < arr1.Length; i++)
                {
                    search1[i] = new MulDuanData();
                    String[] t2=arr1[i].Split('-');
                    search1[i].AStation = t2[0];
                    String[] t3 = t2[1].Split('(');
                    search1[i].BStation = t3[0];
                    String[] t4 = t3[1].Replace("(","").Replace(")","").Split('#');
                    search1[i].trainID = t4[0];
                    if (search1[i].trainID == "0") search1[i].trainID = String.Empty;
                    search1[i].MaxLayer = int.Parse(t4[1]);
                }

                if (search1 != null && search1.Length > 0)
                {
                    line1 = GetLineByFengDuanSearch(search1,onlyGG);
                }
            }
            return line1;
        }

        /// <summary>
        /// 得到分段的搜索
        /// </summary>
        /// <param name="SearchCondition"></param>
        /// <returns></returns>
        public static Line GetLineByFengDuanSearch(MulDuanData[] SearchCondition,bool onlyGG)
        {
            Line line1 = null;
            Line[] mulLine = null;
            List<TrainLine>[] LineList = null;

            if (SearchCondition != null && SearchCondition.Length > 0)
            {
                mulLine = new Line[SearchCondition.Length];
                LineList = new List<TrainLine>[SearchCondition.Length];
            }

            for (int i = 0; i < SearchCondition.Length; i++)
            {
                mulLine[i] = Line.GetLine(SearchCondition[i].AStation, SearchCondition[i].BStation
                                    , SearchCondition[i].MaxLayer, SearchCondition[i].trainID, 3,onlyGG);
                LineList[i] = mulLine[i].usedLine;
            }

            //修正算法
            for (int i = 0; i < SearchCondition.Length; i++)
            {
                String a1=SearchCondition[i].AStation;
                String b1=SearchCondition[i].BStation;
                for (int j = LineList[i].Count - 1; j >= 0;j-- )
                {
                    TrainLine line0 = LineList[i][j];
                    LineNode node1 = line0.Nodes[0];
                    LineNode node2 = line0.Nodes[line0.Nodes.Count - 1];
                    if (node1.AStation != a1 || node2.BStation != b1)
                    {
                        LineList[i].Remove(line0);
                    }
                }
            }

            bool hasLine = true;
            for (int i = 0; i < LineList.Length; i++)
            {
                if (LineList[i] == null || LineList[i].Count == 0)
                {
                    hasLine = false;
                    break;
                }
            }

            //得到拼接后的线路
            if (hasLine)
            {
                line1 = new Line();
                List<TrainLine> NewList = new List<TrainLine>();
                for(int i=0;i<LineList.Length;i++)
                {
                    GetListLine(ref NewList, LineList[i]);
                }

                foreach (TrainLine line0 in NewList)
                {
                    line1.usedLine.Add(line0);
                }

            }
            if (line1 != null)
            {
                line1.usedLine.Sort();
            }
            return line1;
        }

        /// <summary>
        /// 分段搜索拼接的私有方法
        /// </summary>
        /// <param name="NewList"></param>
        /// <param name="oldList"></param>
        private static void GetListLine(ref List<TrainLine> NewList,List<TrainLine> oldList)
        {
            if (NewList.Count  == 0)
            {
                foreach (TrainLine l1 in oldList)
                {
                    NewList.Add(l1);
                }
            }
            else
            {
                List<TrainLine> NewList1 = new List<TrainLine>();
                foreach (TrainLine l1 in NewList)
                {
                    foreach (TrainLine l2 in oldList)
                    {
                        TrainLine l0 = l1 + l2;
                        NewList1.Add(l0);
                    }
                }
                NewList = NewList1;
            }
        }

        public static void GetMaxNumAndLineID(out int MaxNum,out int MaxLineID)
        {
            MaxNum = 1;
            MaxLineID = 1;

            JTable tab1 = new JTable("linestationview");
            DataRow dr1=tab1.GetFirstDataRow(null, "max(num)","max(lineid)");
            if (dr1 != null)
            {
                if (String.IsNullOrEmpty(dr1[0].ToString().Trim()) == false)
                {
                    MaxNum = int.Parse(dr1[0].ToString().Trim()) + 1;
                    if (MaxNum < 1) MaxNum = 1;
                }

                if (String.IsNullOrEmpty(dr1[1].ToString().Trim()) == false)
                {
                    MaxLineID = int.Parse(dr1[1].ToString().Trim()) + 1;
                    if (MaxLineID < 1) MaxLineID = 1;
                }
            }
            tab1.Close();
        }

        //判断站点是否存在
        public static bool isExistsStation(String StationName)
        {
            bool result = false;
            JTable tab1 = new JTable("linestationview");
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("Astation", StationName));
            result = tab1.HasData(condition);
            tab1.Close();
            return result;
        }
        /// <summary>
        /// 根据车型和线路计算票据收入和票据费用(含客专费用时的电费和接地网费)  20140417
        /// </summary>
        /// <param name="traintype"></param>
        /// <param name="line1"></param>
        /// <param name="cdsvalue"></param>
        /// <param name="totalBianZhu"></param>
        /// <param name="ShouRu"></param>
        /// <param name="yz"></param>
        /// <param name="yw"></param>
        /// <param name="rw"></param>
        /// <param name="ca"></param>
        /// <param name="sy"></param>
        /// <param name="totalPeople"></param>
        /// <param name="zhichu1"></param>
        /// <param name="zhichu2"></param>
        /// <param name="zhichu3"></param>
        /// <param name="zhichu4"></param>
        /// <param name="JnFee"></param>
        /// <param name="hasDianChe"></param>
        /// <param name="isYearFlag"></param>
        /// <param name="cbmoshi"></param>
        /// <param name="FindCond"></param>
        public static void GetLineShouruAndZhiChu(
         ETrainType traintype,
         TrainLine line1,
         String cdsvalue,
         int totalBianZhu,               //表示总编组数
         out double ShouRu,
         ref int yz,
         ref int yw,
         ref int rw,
         ref int ca,
         ref int sy,
         out int totalPeople,
         out List<ZhiChuData> zhichu1,
         out List<ZhiChuData> zhichu2,
         out List<ZhiChuData> zhichu3,
         out List<ZhiChuData> zhichu4,
         out double JnFee,
         bool hasDianChe,
         bool isYearFlag,
         int cbmoshi,                             //成本模式
         String FindCond,
            List<string[]> lineInfos//线路信息
         )
        {
            ShouRu = 0;
            line1.compleQianYinFeeCal = false;
            totalPeople = 0;            //年总运输人数

            //四种模式下的收支分析
            zhichu1 = null;
            zhichu2 = null;
            zhichu3 = null;
            zhichu4 = null;
            JnFee = 0;


            Train train1 = null;
            int type1 = (int)traintype;
            if (type1 >= 4)
            {
                train1 = new HighTrain();
                if (cbmoshi > 0) train1.IsFullChengBen = true;

                train1.IsYearFlag = isYearFlag;
                HighTrain train2 = (HighTrain)train1;
                if (String.IsNullOrEmpty(cdsvalue) == false)
                {
                    train2.CheDiShu = double.Parse(cdsvalue);
                }
                train2.Line = line1;
                EHighTrainType hightype = (EHighTrainType)type1;
                ((HighTrain)train1).TrainType = hightype;

                //计算高铁的收入和支出
                train1.YunXingLiCheng = line1.TotalMiles;
                ShouRu = train1.GetShouRu();
                if (isYearFlag)
                {
                    totalPeople = (int)(train1.GetTotalPerson() * 365 * 2);
                }
                else
                {
                    totalPeople = (int)(train1.GetTotalPerson() * 2);
                }

                //计算四种不同模式的支出
                train1.CunZengMoShi = ECunZengMoShi.新人新车;
                zhichu1 = train1.GetZhiChu(FindCond, lineInfos);
                JnFee = train1.JnFee;


                train1.CunZengMoShi = ECunZengMoShi.新人有车;
                zhichu2 = train1.GetZhiChu(FindCond, lineInfos);
                JnFee = train1.JnFee;

                train1.CunZengMoShi = ECunZengMoShi.有人新车;
                zhichu3 = train1.GetZhiChu(FindCond, lineInfos);

                train1.CunZengMoShi = ECunZengMoShi.有人有车;
                zhichu4 = train1.GetZhiChu(FindCond, lineInfos);
            }
            else
            {
                //计算普通列车的收入和支出
                train1 = new CommTrain();
                if (cbmoshi > 0) train1.IsFullChengBen = true;
                train1.IsYearFlag = isYearFlag;
                CommTrain train2 = (CommTrain)train1;
                if (String.IsNullOrEmpty(cdsvalue) == false)
                {
                    train2.CheDiShu = double.Parse(cdsvalue);
                }

                train2.Line = line1;
                ECommTrainType commtype = (ECommTrainType)type1;
                ((CommTrain)train1).TrainType = commtype;
                train1.YunXingLiCheng = line1.TotalMiles;
                train1.Line = line1;


                //计算四种不同模式的支出
                train2.CunZengMoShi = ECunZengMoShi.新人新车;
                String traintypeName = traintype.ToString();
                if (hasDianChe) traintypeName = traintypeName + "(非直供电)";
                //TrainMaxCheXianBU.GetMaxCheXian(traintypeName, out yz, out yw, out rw);
                if (yz == 0 && yw == 0 && rw == 0)
                {
                    zhichu1 = train2.GetShouRuAndZhiChuByGoodBianZhu(totalBianZhu, out ShouRu, out yz, out yw, out rw, hasDianChe, FindCond);
                    ca = 1;
                }
                else
                {
                    zhichu1 = train2.GetShouRuAndZhiChu(out ShouRu, yz, yw, rw, sy, ca, hasDianChe, FindCond);
                }
                JnFee = train1.JnFee;

                //临时增加的测试信息
                String txt1 = String.Empty;
                for (int i = 0; i < zhichu1.Count; i++)
                {
                    txt1 = txt1 + "\n" + zhichu1[i].ZhiChuName + "=" + zhichu1[i].ZhiChu;
                }


                train2.CunZengMoShi = ECunZengMoShi.新人有车;
                zhichu2 = train2.GetShouRuAndZhiChu(out ShouRu, yz, yw, rw, sy, ca, hasDianChe, FindCond);

                train2.CunZengMoShi = ECunZengMoShi.有人新车;
                zhichu3 = train2.GetShouRuAndZhiChu(out ShouRu, yz, yw, rw, sy, ca, hasDianChe, FindCond); ;

                train2.CunZengMoShi = ECunZengMoShi.有人有车;
                zhichu4 = train2.GetShouRuAndZhiChu(out ShouRu, yz, yw, rw, sy, ca, hasDianChe, FindCond); ;


                //重新调整车厢编组并计算
                train2.YinZuo = yz;
                train2.OpenYinWo = yw;
                train2.RuanWo = rw;
                TrainMaxCheXianBU.SaveMaxCheXian(traintypeName, yz, yw, rw);

                if (isYearFlag)
                {
                    totalPeople = (int)(train1.GetTotalPerson() * 365 * 2);
                }
                else
                {
                    totalPeople = (int)(train1.GetTotalPerson() * 2);
                }
            }
        }
        /// <summary>
        /// 根据车型和线路计算票据收入和票据费用(原方法)
        /// </summary>
        /// <param name="traintype">列车类型</param>
        /// <param name="line1">线路</param>
        /// <param name="ShouRu">计算出的收入</param>
        /// <returns>返回支出List</returns>
        public static void GetLineShouruAndZhiChu(
            ETrainType traintype,
            TrainLine line1,
            String cdsvalue,
            int totalBianZhu,               //表示总编组数
            out double ShouRu,
            ref int yz,
            ref int yw,
            ref int rw,
            ref int ca,
            ref int sy,
            out int totalPeople,
            out List<ZhiChuData> zhichu1,
            out List<ZhiChuData> zhichu2,
            out List<ZhiChuData> zhichu3,
            out List<ZhiChuData> zhichu4,
            out double JnFee,
            bool hasDianChe,
            bool isYearFlag,
            int cbmoshi,                             //成本模式
            String FindCond
            )
        {
            ShouRu = 0;
            line1.compleQianYinFeeCal = false;
            totalPeople = 0;            //年总运输人数

            //四种模式下的收支分析
            zhichu1 = null;
            zhichu2 = null;
            zhichu3 = null;
            zhichu4 = null;
            JnFee = 0;


            Train train1 = null;
            int type1 = (int)traintype;
            if (type1 >= 4)
            {
                train1 = new HighTrain();
                if (cbmoshi > 0) train1.IsFullChengBen = true;

                train1.IsYearFlag = isYearFlag;
                HighTrain train2 = (HighTrain)train1;
                if (String.IsNullOrEmpty(cdsvalue) == false)
                {
                    train2.CheDiShu = double.Parse(cdsvalue);
                }
                train2.Line = line1;
                EHighTrainType hightype = (EHighTrainType)type1;
                ((HighTrain)train1).TrainType = hightype;

                //计算高铁的收入和支出
                train1.YunXingLiCheng = line1.TotalMiles;
                ShouRu = train1.GetShouRu();
                if (isYearFlag)
                {
                    totalPeople = (int)(train1.GetTotalPerson() * 365 * 2);
                }
                else
                {
                    totalPeople = (int)(train1.GetTotalPerson() * 2);
                }

                //计算四种不同模式的支出
                train1.CunZengMoShi = ECunZengMoShi.新人新车;
                zhichu1 = train1.GetZhiChu(FindCond);
                JnFee = train1.JnFee;


                train1.CunZengMoShi = ECunZengMoShi.新人有车;
                zhichu2 = train1.GetZhiChu(FindCond);
                JnFee = train1.JnFee;

                train1.CunZengMoShi = ECunZengMoShi.有人新车;
                zhichu3 = train1.GetZhiChu(FindCond);

                train1.CunZengMoShi = ECunZengMoShi.有人有车;
                zhichu4 = train1.GetZhiChu(FindCond);
            }
            else
            {
                //计算普通列车的收入和支出
                train1 = new CommTrain();
                if (cbmoshi > 0) train1.IsFullChengBen = true;
                train1.IsYearFlag = isYearFlag;
                CommTrain train2 = (CommTrain)train1;
                if (String.IsNullOrEmpty(cdsvalue) == false)
                {
                    train2.CheDiShu = double.Parse(cdsvalue);
                }

                train2.Line = line1;
                ECommTrainType commtype = (ECommTrainType)type1;
                ((CommTrain)train1).TrainType = commtype;
                train1.YunXingLiCheng = line1.TotalMiles;
                train1.Line = line1;


                //计算四种不同模式的支出
                train2.CunZengMoShi = ECunZengMoShi.新人新车;
                String traintypeName = traintype.ToString();
                if (hasDianChe) traintypeName = traintypeName + "(非直供电)";
                //TrainMaxCheXianBU.GetMaxCheXian(traintypeName, out yz, out yw, out rw);
                if (yz == 0 && yw == 0 && rw == 0)
                {
                    zhichu1 = train2.GetShouRuAndZhiChuByGoodBianZhu(totalBianZhu, out ShouRu, out yz, out yw, out rw, hasDianChe, FindCond);
                    ca = 1;
                }
                else
                {
                    zhichu1 = train2.GetShouRuAndZhiChu(out ShouRu, yz, yw, rw, sy, ca, hasDianChe, FindCond);
                }
                JnFee = train1.JnFee;

                //临时增加的测试信息
                String txt1 = String.Empty;
                for (int i = 0; i < zhichu1.Count; i++)
                {
                    txt1 = txt1 + "\n" + zhichu1[i].ZhiChuName + "=" + zhichu1[i].ZhiChu;
                }


                train2.CunZengMoShi = ECunZengMoShi.新人有车;
                zhichu2 = train2.GetShouRuAndZhiChu(out ShouRu, yz, yw, rw, sy, ca, hasDianChe, FindCond);

                train2.CunZengMoShi = ECunZengMoShi.有人新车;
                zhichu3 = train2.GetShouRuAndZhiChu(out ShouRu, yz, yw, rw, sy, ca, hasDianChe, FindCond); ;

                train2.CunZengMoShi = ECunZengMoShi.有人有车;
                zhichu4 = train2.GetShouRuAndZhiChu(out ShouRu, yz, yw, rw, sy, ca, hasDianChe, FindCond); ;


                //重新调整车厢编组并计算
                train2.YinZuo = yz;
                train2.OpenYinWo = yw;
                train2.RuanWo = rw;
                TrainMaxCheXianBU.SaveMaxCheXian(traintypeName, yz, yw, rw);

                if (isYearFlag)
                {
                    totalPeople = (int)(train1.GetTotalPerson() * 365 * 2);
                }
                else
                {
                    totalPeople = (int)(train1.GetTotalPerson() * 2);
                }
            }
        }

        /// <summary>
        /// 设置中间站点的数据
        /// </summary>
        /// <param name="LineID"></param>
        /// <param name="dt1"></param>
        /// <returns></returns>
        public static String SetMiddleStation(String LineID, DataTable dt1)
        {
            String error = String.Empty;
            if (String.IsNullOrEmpty(LineID) == false
                && JValidator.IsInt(LineID))
            {
                JTable tab1 = new JTable("LINESTATION");
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    List<SearchField> condition = new List<SearchField>();
                    condition.Add(new SearchField("LineID", LineID, SearchFieldType.NumericType));
                    tab1.DeleteData(condition);

                    DataSet ds0 = tab1.SearchData(condition, -1, "*");
                    if (ds0 != null)
                    {
                        int index = 1;
                        int index2 = dt1.Rows.Count ;

                        DataTable dt0 = ds0.Tables[0];
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            DataRow dr0 = dt0.NewRow();
                            dr0["num"] = index;
                            dr0["lineid"] = LineID;
                            dr0["Astation"] = dr1["Astation"];
                            dr0["BStation"] = dr1["BStation"];
                            dr0["miles"] = dr1["miles"];
                            dr0["direction"] = "0";

                            dr0["jnflag"] = dr1["jnflag"];
                            dr0["dqh"] = dr1["dqh"];
                            dr0["shipflag"] = dr1["shipflag"];
                            dr0["gtllx"] = dr1["gtllx"];
                            dr0["KZID"] = dr1["KZID"];
                            dr0["CJLID"] = dr1["CJLID"];

                            dt0.Rows.Add(dr0);

                            DataRow dr01 = dt0.NewRow();
                            dr01["num"] = index2;
                            dr01["lineid"] = LineID;
                            dr01["Astation"] = dr1["BStation"];
                            dr01["BStation"] = dr1["Astation"];
                            dr01["miles"] = dr1["miles"];
                            dr01["direction"] = "1";

                            dr01["jnflag"] = dr1["jnflag"];
                            dr01["dqh"] = dr1["dqh"];
                            dr01["shipflag"] = dr1["shipflag"];
                            dr01["gtllx"] = dr1["gtllx"];
                            dr01["KZID"] = dr1["KZID"];
                            dr01["CJLID"] = dr1["CJLID"];
                          

                            dt0.Rows.Add(dr01);
                            index++;
                            index2--;
                        }
                        tab1.Update(dt0);   //更新数据
                    }
                }
                tab1.Close();
            }
            return error;
        }

        /// <summary>
        /// 设置中间站点
        /// </summary>
        /// <param name="lineID"></param>
        /// <param name="MiddleStation"></param>
        /// <returns></returns>
        public static DataTable GetMiddleStation(String LineID, String MiddleStation)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("LineID", typeof(int));
            dt.Columns.Add("num", typeof(int));
            dt.Columns.Add("Astation");
            dt.Columns.Add("Bstation");
            dt.Columns.Add("Miles", typeof(int));
            dt.Columns.Add("dqh");
            dt.Columns.Add("jnflag");
            dt.Columns.Add("shipflag");
            dt.Columns.Add("fee1", typeof(int));
            dt.Columns.Add("fee2", typeof(int));
            dt.Columns.Add("GTLLX");
            dt.Columns.Add("KZID");
            dt.Columns.Add("CJLID");
            DataRow tempRow = null;

            if (String.IsNullOrEmpty(LineID) == false
                && JValidator.IsInt(LineID))
            {
                DataRow dr = GetLineData(LineID);
                if (dr != null)
                {
                    String temp1 = dr["astation"].ToString();
                    if (String.IsNullOrEmpty(MiddleStation) == false)
                    {
                        temp1 = temp1 + "," + MiddleStation;
                    }

                    temp1 = temp1 + "," + dr["bstation"].ToString();
                    temp1 = temp1.Replace("，", ",");
                    string[] t1 = temp1.Split(',');
                    int index = 0;
                    for (int i = 0; i < t1.Length - 1; i++)
                    {
                        DataRow dr0 = dt.NewRow();
                        dr0["lineid"] = LineID;
                        dr0["num"] = index;
                        dr0["astation"] = t1[i];
                        dr0["bstation"] = t1[i + 1];
                        dr0["miles"] = GetStationMiles(LineID, t1[i], t1[i + 1]);

                        tempRow = GetStationInfo(LineID, t1[i], t1[i + 1], "*");

                        if (dr["dqh"].ToString().Trim() != String.Empty)
                        {
                            dr0["dqh"] = "1";
                        }

                        //设置其他的信息
                        if (tempRow != null)
                        {
                            dr0["jnflag"]=tempRow["jnflag"];
                            dr0["shipflag"]=tempRow["shipflag"];
                            dr0["fee1"]=tempRow["fee1"];
                            dr0["fee2"] = tempRow["fee2"];
                            dr0["gtllx"]=tempRow["gtllx"];
                            dr0["KZID"] = tempRow["KZID"];
                            dr0["CJLID"] = tempRow["CJLID"];
                        }

                        dt.Rows.Add(dr0);

                        index++;
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// 得到线路的里程数
        /// </summary>
        /// <param name="LineID"></param>
        /// <param name="AStation"></param>
        /// <param name="BStation"></param>
        /// <returns></returns>
        private static int GetStationMiles(String LineID, String AStation, String BStation)
        {
            int result = 0;
            if (String.IsNullOrEmpty(LineID) == false
                && JValidator.IsInt(LineID))
            {
                JTable tab1 = new JTable("LINESTATION");
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("lineid", LineID, SearchFieldType.NumericType));
                condition.Add(new SearchField("astation", AStation));
                condition.Add(new SearchField("bstation", BStation));
                DataRow dr1 = tab1.GetFirstDataRow(condition, "miles");
                if (dr1 != null)
                {
                    result = int.Parse(dr1[0].ToString());
                }
                tab1.Close();

            }
            return result;
        }

        /// <summary>
        /// 得到两个站之间的信息
        /// </summary>
        /// <param name="LineID"></param>
        /// <param name="AStation"></param>
        /// <param name="BStation"></param>
        /// <returns></returns>
        private static DataRow  GetStationInfo(String LineID, String AStation, 
            String BStation,String fileds)
        {
            DataRow dr1 = null;
            if (String.IsNullOrEmpty(LineID) == false
                && JValidator.IsInt(LineID))
            {
                JTable tab1 = new JTable("LINESTATION");
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("lineid", LineID, SearchFieldType.NumericType));
                condition.Add(new SearchField("astation", AStation));
                condition.Add(new SearchField("bstation", BStation));
                dr1 = tab1.GetFirstDataRow(condition, fileds);
                tab1.Close();

            }
            return dr1;
        }

        /// <summary>
        /// 得到某条线上的数据
        /// </summary>
        /// <param name="LineID"></param>
        /// <returns></returns>
        public static DataRow GetLineData(String LineID)
        {
            DataRow dr1 = null;
            if (String.IsNullOrEmpty(LineID) == false
                && JValidator.IsInt(LineID))
            {
                JTable tab1 = new JTable("TRAINLINE");
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("LineID", LineID, SearchFieldType.NumericType));
                dr1 = tab1.GetFirstDataRow(condition, "*");
                tab1.Close();
            }
            return dr1;
        }

        /// <summary>
        /// 得到列车中间站点的数量
        /// </summary>
        /// <param name="LineID"></param>
        /// <returns></returns>
        public static int GetLineStationCount(String LineID)
        {
            int result = 0;
            if (String.IsNullOrEmpty(LineID) == false
                && JValidator.IsInt(LineID))
            {
                JTable tab1 = new JTable("LineStation");
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("LineID", LineID, SearchFieldType.NumericType));
                condition.Add(new SearchField("direction", "0"));   //增加站点的方向控制

                DataSet ds1 = tab1.SearchData(condition, -1, new String[] { "distinct Astation", "BStation" });
                if (ds1 != null)
                {
                    result = ds1.Tables[0].Rows.Count;
                    if (result != 0)
                    {
                        result = result + 1;
                    }
                }
                tab1.Close();
            }
            return result;
        }

        /// <summary>
        /// 得到列车中间站点的文本
        /// </summary>
        /// <param name="LineID"></param>
        /// <returns></returns>
        public static String GetLineStationText(String LineID)
        {
            String result = String.Empty;
            if (String.IsNullOrEmpty(LineID) == false
                && JValidator.IsInt(LineID))
            {
                JTable tab1 = new JTable("LineStation");
                tab1.OrderBy = "num";
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("LineID", LineID, SearchFieldType.NumericType));
                condition.Add(new SearchField("direction", "0"));   //增加站点的方向控制
                DataSet ds1 = tab1.SearchData(condition, -1, new String[] { "Astation", "BStation" });
                if (ds1 != null)
                {
                    DataTable dt1 = ds1.Tables[0];
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        if (result == String.Empty)
                        {
                            result = dt1.Rows[i][0].ToString();
                        }
                        else
                        {
                            result = result + "," + dt1.Rows[i][0].ToString();
                        }

                        if (i == dt1.Rows.Count - 1)
                        {
                            result = result + "," + dt1.Rows[i][1].ToString();
                        }
                    }
                }
                tab1.Close();
            }
            return result;
        }

        /// <summary>
        /// 得到列车的中间站点数据
        /// </summary>
        /// <param name="LineID"></param>
        /// <returns></returns>
        public static String GetLineMiddleStation(String LineID)
        {
            String result = String.Empty;
            if (String.IsNullOrEmpty(LineID) == false
              && JValidator.IsInt(LineID))
            {
                String[] str1 = GetLineStationText(LineID).Split(',');
                if (str1.Length > 2)
                {
                    for (int i = 1; i < str1.Length - 1; i++)
                    {
                        if (result == String.Empty)
                        {
                            result = str1[i];
                        }
                        else
                        {
                            result = result + "," + str1[i];
                        }
                    }
                }
            }
            return result;
        }

        
        /// <summary>
        /// 根据列车的ID，得到列车的数据
        /// </summary>
        /// <param name="LineID"></param>
        /// <returns></returns>
        public static DataTable GetLineStationData(String LineID)
        {
            DataTable dt1 = new DataTable();
            if (String.IsNullOrEmpty(LineID) == false
               && JValidator.IsInt(LineID))
            {
                JTable tab1 = new JTable("LineStation");
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("LineID", LineID, SearchFieldType.NumericType));
                condition.Add(new SearchField("direction", "0"));   //增加方向的控制

                tab1.OrderBy = "num";
                DataSet ds1 = tab1.SearchData(condition, -1, new String[] { "ID", "AStation", "lineid", "BStation", 
                                                                           "Miles", "num","direction","jnflag","dqh","shipflag","Fee1","Fee2","Fee3","GTLLX","KZID","CJLID"});
                dt1 = ds1.Tables[0];


                //假如dt1没数据，则设置初始的数据
                if (dt1.Rows.Count == 0)
                {
                    DataRow dr1 = GetLineData(LineID);
                    if (dr1 != null)
                    {
                        DataRow dr0 = ds1.Tables[0].NewRow();

                        dr0["lineid"] = LineID;
                        dr0["num"] = 1;
                        dr0["astation"] = dr1["astation"];
                        dr0["bstation"] = dr1["bstation"];
                        dr0["miles"] = dr1["miles"];
                        dr0["direction"] = "0";
                        ds1.Tables[0].Rows.Add(dr0);

                        //反方向路线站
                        DataRow dr2 = ds1.Tables[0].NewRow();
                        dr2["lineid"] = LineID;
                        dr2["num"] = 1;
                        dr2["astation"] = dr1["bstation"];
                        dr2["bstation"] = dr1["astation"];
                        dr2["miles"] = dr1["miles"];
                        dr2["direction"] = "1";
                        ds1.Tables[0].Rows.Add(dr2);

                        //更新数据显示
                        tab1.Update(ds1.Tables[0]);
                        dr2.Delete();
                        ds1.Tables[0].AcceptChanges();
                    }
                }
                tab1.Close();
            }
            return dt1;
        }

        /// <summary>
        /// 删除线路和线路所有的站点信息
        /// </summary>
        /// <param name="LineID"></param>
        public static void DeleteLine(int LineID)
        {
            JConnect conn1 = JConnect.GetConnect();
             conn1.BeginTrans();
            JTable tab1 = null;
            try
            {
                tab1 = new JTable("TrainLine");
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("LineID", LineID + "", SearchFieldType.NumericType));
                tab1.DeleteData(condition);

                tab1.TableName = "LINESTATION";
                tab1.DeleteData(condition);
                conn1.CommitTrans();
            }
            catch (Exception err)
            {
                conn1.RollBackTrans();
            }
            finally
            {
                if (tab1 != null) tab1.Close();
            }
        }

        /// <summary>
        /// 更换线路的夏冬切换状态
        /// </summary>
        /// <param name="LineID"></param>
        public static void ChangeSpringAndWinterStatus(int LineID)
        {
            JConnect conn1 = JConnect.GetConnect();
            conn1.BeginTrans();
            JTable tab1 = null;
            try
            {
                tab1 = new JTable("TrainLine");
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("LineID", LineID + "", SearchFieldType.NumericType));
                DataSet ds1 = tab1.SearchData(condition, -1, "*");
                if (ds1 != null)
                {
                    DataRow dr1 = ds1.Tables[0].Rows[0];
                    if (dr1["SpringWinter"].ToString() == "1")
                    {
                        dr1["SpringWinter"] = DBNull.Value;
                    }
                    else
                    {
                        dr1["SpringWinter"] = "1";
                    }
                    tab1.Update(ds1.Tables[0]);
                }

                conn1.CommitTrans();
            }
            catch (Exception err)
            {
                conn1.RollBackTrans();
            }
            finally
            {
                if (tab1 != null) tab1.Close();
            }
        }

        /// <summary>
        /// 从XLS中导入线路数据
        /// </summary>
        /// <param name="FileName"></param>
        public static void ImportLineData(String FileName)
        {
            Dictionary<String, int> LineClass = PubCode.Util.GetLineClass();
            String[] fs = new String[] { "线路代码","线路名称","起点","讫点",
                                         "长度(km)","类别","产权","产权归属","备注"
                                        };

            String[] fs1 = new String[] {"LineID","LineName","AStation","BStation",
                                         "MILES","LineType","ChanQuan","ChanQuanGuiShou","Remark" };

            JTable tab1 = new JTable("TrainLine");
            Dictionary<String, object> data1 = new Dictionary<string, object>();
            int num = 1;

            String FileName1 = HttpContext.Current.Server.MapPath("~/Attachment/" + FileName);
            if (File.Exists(FileName1))
            {
                DataSet ds1 = PubCode.Util.xsldata(FileName1, "线路等级");
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr1 in ds1.Tables[0].Rows)
                    {
                        data1.Clear();
                        for (int i = 0; i < fs.Length; i++)
                        {
                            object obj1 = dr1[fs[i]];
                            if (fs[i] == "类别")
                            {
                                if (obj1.ToString() == "特1")
                                {
                                    obj1 = 1;
                                }
                                else if (obj1.ToString() == "特2")
                                {
                                    obj1 = 2;
                                }
                                else if (obj1.ToString() == "1+")
                                {
                                    obj1 = 3;
                                }
                                else if (obj1.ToString() == "1")
                                {
                                    obj1 = 4;
                                }
                                else if (obj1.ToString() == "2+")
                                {
                                    obj1 = 5;
                                }
                                else if (obj1.ToString() == "2")
                                {
                                    obj1 = 6;
                                }
                                else if (obj1.ToString() == "3")
                                {
                                    obj1 = 7;
                                }
                                else
                                {
                                    obj1 = 8;
                                }
                            }
                            data1.Add(fs1[i], obj1);
                        }
                        data1.Add("num", num);
                        data1.Add("lineclass", 0);
                        num++;
                        tab1.InsertData(data1);
                    }
                }
            }
            tab1.Close();
        }
        #endregion

        #region 线路的分析方法
        /// <summary>
        /// 返回所有的线路列表数据
        /// </summary>
        /// <returns></returns>
        public static List<String> GetTrainLindName()
        {
            List<String> list1 = new List<string>();
            DataTable dt1 = null;
            JTable tab1 = new JTable("linestationview");
            tab1.OrderBy = "lineid,linename";
            dt1 = tab1.SearchData(null, -1, "distinct lineid,linename").Tables[0];
            tab1.Close();
            foreach (DataRow dr1 in dt1.Rows)
            {
                list1.Add(dr1[1].ToString());
            }
            return list1;
        }

        /// <summary>
        /// 得到每个线路的数据
        /// </summary>
        /// <param name="tab1"></param>
        /// <param name="linename"></param>
        /// <returns></returns>
        public static DataTable GetTrainLindData(JTable tab1,String linename)
        {
            DataTable dt1 = null;
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("linename", linename));
            condition.Add(new SearchField("direction", "0"));
            tab1.OrderBy = "lineid,num";
            dt1 = tab1.SearchData(condition, -1, "*").Tables[0];
            return dt1;
        }


        //根据线路的节点得到Line对象
        public static TrainLine GetTrainLineByTrainTypeAndLineNoeds(
            ETrainType trainType,
            bool hasDianChe,
            String[] lineNodes)
        {
            TrainLine line1 = new TrainLine();
            JTable tab1 = new JTable("LINESTATIONVIEW");
            //tab1.OrderBy = "AStation,BStation,Miles desc";

            tab1.OrderBy = "AStation,BStation,Miles ";   //2013年4月10日修改（该成升序，取第一个）
            List<SearchField> condition = new List<SearchField>();

            //设置线路的条件
            #region 线路限制条件
            switch (trainType)
            {
                case ETrainType.绿皮车25B:
                case ETrainType.空调车25K:
                    condition.Add(new SearchField("LineType<>'1' and LineType<>'2'", "", SearchOperator.UserDefine));
                    break;

                case ETrainType.空调车25G:
                    if (hasDianChe == true)
                    {
                        condition.Add(new SearchField("LineType<>'1' and LineType<>'2'", "", SearchOperator.UserDefine));
                    }
                    else
                    {
                        condition.Add(new SearchField("LineType<>'1'", "", SearchOperator.UserDefine));
                    }
                    break;

                case ETrainType.空调车25T:
                    condition.Add(new SearchField("LineType<>'1'", "", SearchOperator.UserDefine));
                    break;

                case ETrainType.动车CRH2A:
                case ETrainType.动车CRH2E:
                case ETrainType.动车CRH2B:
                case ETrainType.动车CRH5A:
                    condition.Add(new SearchField("dqh is not null", "", SearchOperator.UserDefine));
                    break;

                //修改动车的过滤条件
                case ETrainType.动车CRH2C:
                case ETrainType.动车CRH380A:
                case ETrainType.动车CRH380AL:
                case ETrainType.动车CRH380B:
                case ETrainType.动车CRH380BL:
                    //说明：取消300高速车对特1线的要求限制
                    condition.Add(new SearchField("(LineType='1' or gtllx is not null)", "", SearchOperator.UserDefine));
                    condition.Add(new SearchField("dqh is not null", "", SearchOperator.UserDefine));
                    break;
                default:
                    break;
            }
            #endregion

            String parent = "0";
            for (int i = 0; i < lineNodes.Length - 1; i++)
            {
                String A0 = lineNodes[i];
                String B0 = lineNodes[i + 1];

                //设置线路站点
                SearchField AStation = new SearchField("Astation", A0);
                SearchField BStation = new SearchField("BStation", B0);
                condition.Add(AStation);
                condition.Add(BStation);

                DataRow dr1 = tab1.GetFirstDataRow(condition, "*");
                if (dr1 != null)
                {
                    LineNode data1 = new LineNode();
                    data1.AStation = dr1["AStation"].ToString();
                    data1.BStation = dr1["BStation"].ToString();

                    data1.LineID = dr1["LineID"].ToString();
                    data1.Miles = int.Parse(dr1["Miles"].ToString());
                    data1.LineType = dr1["LineType"].ToString();
                    data1.Parent.Add(parent);
                    data1.ID = dr1["ID"].ToString();
                    data1.JnFlag = dr1["JnFlag"].ToString();
                    data1.DqhFlag = dr1["dqh"].ToString();
                    data1.ShipFlag = dr1["ShipFlag"].ToString();
                    data1.Gtllx = dr1["Gtllx"].ToString();          //增加了高铁联络线

                    if (dr1["Aname1"].ToString().Trim() != String.Empty)
                    {
                        data1.BigA = true;
                    }

                    if (dr1["Bname1"].ToString().Trim() != String.Empty)
                    {
                        data1.BigB = true;
                    }

                    //增加Fee1、Fee2和Fee3的费用说明
                    if (dr1["Fee1"].ToString().Trim() != String.Empty)
                    {
                        data1.Fee1 = int.Parse(dr1["Fee1"].ToString().Trim());
                    }

                    if (dr1["Fee2"].ToString().Trim() != String.Empty)
                    {
                        data1.Fee2 = int.Parse(dr1["Fee2"].ToString().Trim());
                    }


                    line1.Nodes.Add(data1);
                    parent = data1.ID;
                }
                else
                {
                    line1 = null;
                    break;
                }

                //移除站点条件
                condition.Remove(AStation);
                condition.Remove(BStation);
            }
            tab1.Close();
            return line1;
        }

        //根据线路节点，得到线路
        private void GetLine()
        {
            List<List<LineNode>> findLine = new List<List<LineNode>>();
                        
            for (int i =this.MaxLayer; i >= 0; i--)
            {
                foreach (LineNode node1 in this.LineLayer[i])
                {
                    if (this.BStation==node1.BStation)
                    {
                       SearchLineAddNodes(node1, i,findLine);
                    }
                }
            }
            
            //将线路排序
            foreach (List<LineNode> line in findLine)
            {
                TrainLine line1 = new TrainLine();
                for (int i = line.Count - 1; i >= 0; i--)
                {
                    line1.Nodes.Add(line[i]);
                }
                this.usedLine.Add(line1);
            }
            this.usedLine.Sort();   //将线路按站点距离排序

            //去掉多余的线路
            try
            {
                if (this.usedLine.Count > MAXLINECOUNT)
                {
                    this.usedLine.RemoveRange(MAXLINECOUNT, this.usedLine.Count - MAXLINECOUNT);
                }
            }
            catch (Exception err) { ;}
        }

        //将节点加入线
        private void SearchLineAddNodes(LineNode node1, int layer, 
            List<List<LineNode>> findLine)
        {
            List<List<LineNode>> listTemp = new List<List<LineNode>>();
            List<LineNode> line1 = new List<LineNode>();
            line1.Add(node1);
            listTemp.Add(line1);

            for (int i = layer - 1; i >= 0; i--)
            {
                for (int j = 0; j < this.LineLayer[i].Count; j++)
                {
                    LineNode parentNode = this.LineLayer[i][j];
                    parentNode.Selected = false;
                }
            

                for (int j = 0; j < this.LineLayer[i].Count; j++)
                {
                    LineNode parentNode = this.LineLayer[i][j];
                    for (int k = 0; k < listTemp.Count; k++)
                    {
                        List<LineNode> lineTemp = listTemp[k];
                        if (lineTemp[lineTemp.Count - 1].Parent.Contains(parentNode.ID))
                        {
                            lineTemp.Add(parentNode);
                        }
                    }

                    //次端增加线
                    for (int k = 0; k < listTemp.Count; k++)
                    {
                        List<LineNode> lineTemp = listTemp[k];
                        if (lineTemp.Count > 1)
                        {
                            if (lineTemp[lineTemp.Count - 2].Parent.Contains(parentNode.ID)
                                && lineTemp[lineTemp.Count - 1].ID != parentNode.ID
                                && parentNode.Selected==false)
                            {
                                List<LineNode> newLine = new List<LineNode>();
                                for (int l = 0; l < lineTemp.Count - 1; l++)
                                {
                                    newLine.Add(lineTemp[l]);
                                }
                                newLine.Add(parentNode);
                                listTemp.Add(newLine);

                                parentNode.Selected = true;
                            }
                        }
                    }
                }
            }

            foreach (List<LineNode> l1 in listTemp)
            {
                findLine.Add(l1);
            }
            
        }


        //得到线路（importance function)
        public List<TrainLine> GetLayerLine(String[] KeyStation,
            ETrainType trainType,bool hasDianChe,int PageSize,bool onlygg)
        {
            /*
            List<TrainLine> resultLine = new List<TrainLine>();
            foreach (List<LineNode> line in this.findLine)
            {
                TrainLine line1 = new TrainLine();
                for (int i = line.Count - 1; i >= 0; i--)
                {
                    line1.Nodes.Add(line[i]);
                }
                resultLine.Add(line1);
            }
            resultLine.Sort();   //将线路按站点距离排序*/


            //根据线路必须经过的中间站点和站点类型条件输出
            List<TrainLine> resultLine1 = new List<TrainLine>();
            int index = 0;
                        
            foreach (TrainLine line in this.usedLine)
            {
                if (CheckLineByMiddleStation(line, KeyStation,trainType,hasDianChe,onlygg))
                {
                    resultLine1.Add(line);
                    index++;
                    if (index >= PageSize)
                    {
                        break;
                    }
                }
            }
            return resultLine1;
        }


        //得到线路
        public List<TrainLine> GetLayerLine(String[] KeyStation, ETrainType trainType,bool hasDianChe,bool onlygg)
        {
            return this.GetLayerLine(KeyStation, trainType, hasDianChe,2,onlygg);
        }
       
        #region Private Function
        //设置线路站点中的数据
        private void SetLayerNodesInit()
        {
            this.LineLayer = new List<LineNode>[this.MaxLayer+1];
            this.LineAllStation = new List<string>[this.MaxLayer + 1];

            for (int i = 0; i < this.LineLayer.Length; i++)
            {
                this.LineLayer[i] = new List<LineNode>();
                this.LineAllStation[i] = new List<string>();
            }


            //设置层节点数据
            JTable tab1 = new JTable("LINESTATIONVIEW");
            tab1.OrderBy = "AStation,BStation,Miles desc";
            DataTable InitTable = tab1.SearchData(null, -1, "*").Tables[0];
            tab1.Close();

            List<SearchField> condition = new List<SearchField>();
            if (String.IsNullOrEmpty(this.SelTrainLineID))
            {
                condition.Add(new SearchField("BStation <> '" + this.AStation + "'", "", SearchOperator.UserDefine));
                condition.Add(new SearchField("AStation <> '" + this.BStation + "'", "", SearchOperator.UserDefine));
            }
            else
            {
                condition.Add(new SearchField("BStation <> '" + this.AStation + "'", "", SearchOperator.UserDefine));
                condition.Add(new SearchField("( AStation <> '" + this.BStation + "' and lineid in ("+this.SelTrainLineID+") )", "", SearchOperator.UserDefine));
            }

            //增加高铁标识条件（5月7日修改）
            if (this.OnlyGG)
            {
                string str1 = "LineType = '1' or LineType = '2' or Gtllx is not null";
                condition.Add(new SearchField(str1, "", SearchOperator.UserDefine));
            }
            else
            {
                string str1 = "1=1";
                condition.Add(new SearchField(str1, "", SearchOperator.UserDefine));
            }
            
            for (int i = 0; i < this.LineLayer.Length; i++)
            {
                if (condition.Count != 3)
                {
                    condition.RemoveAt(4);
                    condition.RemoveAt(3);
                }
                SetLayerNodes(InitTable, i, condition);
            }

            //剔除不用的站点
            for (int i = this.LineLayer.Length - 1; i >= 0; i--)
            {
                if (this.LineLayer[i].Count > 0)
                {
                    this.MaxLayer = i;
                    break;
                }
            }

            //释放表数据资源
            InitTable.Dispose();

            //清除无用的表站点
            ClearNoUsedNodes();

            //设置可用的线路
            GetLine();
            
        }

        //在层中寻找站点
        private LineNode FindNode(int lay, String parent)
        {
            LineNode find = null;
            foreach (LineNode node1 in this.LineLayer[lay])
            {
                if (node1.ID == parent)
                {
                    find = node1;
                    break;
                }
            }
            return find;
        }
        
        //设置线路层次站点（重要方法）
        private void SetLayerNodesOLD(DataTable tab1, int lay,
           List<SearchField> condition)
        {
            if (lay == 0)
            {
                SearchField AStation = new SearchField("Astation = '" + this.AStation + "'", "", SearchOperator.UserDefine);
                SearchField BStation = new SearchField("BStation <> '" + this.AStation + "'", "", SearchOperator.UserDefine);
                condition.Add(AStation);
                condition.Add(BStation);

                String filter = SearchField.GetSearchCondition(condition);
                
                DataRow[] drs = tab1.Select(filter);
                foreach (DataRow dr1 in drs)
                {
                    LineNode data1 = new LineNode();
                    data1.AStation = dr1["AStation"].ToString();
                    data1.BStation = dr1["BStation"].ToString();

                    data1.LineID = dr1["LineID"].ToString();
                    data1.Miles = int.Parse(dr1["Miles"].ToString());
                    data1.LineType = dr1["LineType"].ToString();
                    data1.JnFlag = dr1["JnFlag"].ToString();
                    data1.DqhFlag = dr1["dqh"].ToString();
                    data1.ShipFlag = dr1["ShipFlag"].ToString();

                    //设置大站标志
                    if (dr1["Aname1"].ToString().Trim() != String.Empty)
                    {
                        data1.BigA = true;
                    }

                    if (dr1["Bname1"].ToString().Trim() != String.Empty)
                    {
                        data1.BigB = true;
                    }

                    //增加Fee1、Fee2和Fee3的费用说明
                    if (dr1["Fee1"].ToString().Trim() != String.Empty)
                    {
                        data1.Fee1 = int.Parse(dr1["Fee1"].ToString().Trim());
                    }

                    if (dr1["Fee2"].ToString().Trim() != String.Empty)
                    {
                        data1.Fee2 = int.Parse(dr1["Fee2"].ToString().Trim());
                    }

                    
                    data1.Parent.Add("0");
                    data1.ID = lay+"_"+dr1["ID"].ToString();
                    data1.PassedStation.Add("'" + data1.AStation + "'");

                    LineNode n1 = IsHasNodes(lay, data1);
                    if (n1 == null)
                    {
                        this.LineLayer[lay].Add(data1);     //将站点加入到层
                    }
                    else
                    {
                        n1.Parent.Add("0");
                    }
                }
                condition.Remove(AStation);
                condition.Remove(BStation);
            }
            else
            {
                SearchField Astation = null;
                SearchField BStation = null;
                for (int i = 0; i < this.LineLayer[lay - 1].Count; i++)
                {
                    LineNode node1 = this.LineLayer[lay - 1][i];
                    if (node1.BStation != this.BStation)
                    {
                        if (Astation != null) condition.Remove(Astation);
                        if (BStation != null) condition.Remove(BStation);

                        Astation = new SearchField("Astation", node1.BStation);
                        String go = node1.PassedStationString;
                        if (String.IsNullOrEmpty(go) == false)
                        {
                            BStation = new SearchField("BStation <>'" + node1.AStation + "' and BStation not in (" + go + ")", "", SearchOperator.UserDefine);
                        }
                        else
                        {
                            BStation = new SearchField("BStation", node1.AStation, SearchOperator.NotEqual);
                        }

                        condition.Add(Astation);
                        condition.Add(BStation);

                        String filter = SearchField.GetSearchCondition(condition);                       
                        DataRow[] drs = tab1.Select(filter);
                        foreach (DataRow dr1 in drs)
                        {
                            LineNode data1 = new LineNode();
                            data1.AStation = dr1["AStation"].ToString();
                            data1.BStation = dr1["BStation"].ToString();
                            data1.ID = lay+"_"+ dr1["ID"].ToString();

                            data1.LineID = dr1["LineID"].ToString();
                            data1.Miles = int.Parse(dr1["Miles"].ToString());
                            data1.LineType = dr1["LineType"].ToString();
                            data1.JnFlag = dr1["JnFlag"].ToString();
                            data1.ShipFlag = dr1["ShipFlag"].ToString();
                            data1.DqhFlag = dr1["dqh"].ToString();

                            //设置大站标志
                            if (dr1["Aname1"].ToString().Trim() != String.Empty)
                            {
                                data1.BigA = true;
                            }

                            if (dr1["Bname1"].ToString().Trim() != String.Empty)
                            {
                                data1.BigB = true;
                            }

                            //增加Fee1、Fee2和Fee3的费用说明
                            if (dr1["Fee1"].ToString().Trim() != String.Empty)
                            {
                                data1.Fee1 = int.Parse(dr1["Fee1"].ToString().Trim());
                            }

                            if (dr1["Fee2"].ToString().Trim() != String.Empty)
                            {
                                data1.Fee2 = int.Parse(dr1["Fee2"].ToString().Trim());
                            }

                           
                            String newStation = "'" + data1.AStation + "'";
                            LineNode n1 = IsHasNodes(lay, data1);
                            if (n1==null)
                            {
                                data1.SetGoneStation(go);
                                if (data1.PassedStation.Contains(newStation) == false)
                                {
                                    data1.PassedStation.Add(newStation);
                                }
                                
                                data1.Parent.Add(node1.ID);
                                this.LineLayer[lay].Add(data1);     //将站点加入到层
                            }
                            else
                            {
                                n1.SetGoneStation(go);
                                if (n1.PassedStation.Contains(newStation) == false)
                                {
                                    n1.PassedStation.Add(newStation);
                                }
                                n1.Parent.Add(node1.ID);
                            }
                        }
                    }
                }

            }
        }


        //设置线路层次站点（重要方法）
        private void SetLayerNodes(DataTable tab1,
           int lay,
           List<SearchField> condition)
        {
            if (lay == 0)
            {
                SearchField AStation = new SearchField("Astation = '" + this.AStation + "'", "", SearchOperator.UserDefine);
                SearchField BStation = new SearchField("BStation <> '" + this.AStation + "'", "", SearchOperator.UserDefine);
                condition.Add(AStation);
                condition.Add(BStation);

                String filter = SearchField.GetSearchCondition(condition);

                DataRow[] drs = tab1.Select(filter);
                foreach (DataRow dr1 in drs)
                {
                    LineNode data1 = new LineNode();
                    data1.AStation = dr1["AStation"].ToString();
                    data1.BStation = dr1["BStation"].ToString();

                    data1.LineID = dr1["LineID"].ToString();
                    data1.Miles = int.Parse(dr1["Miles"].ToString());
                    data1.LineType = dr1["LineType"].ToString();
                    data1.JnFlag = dr1["JnFlag"].ToString();
                    data1.DqhFlag = dr1["dqh"].ToString();
                    data1.ShipFlag = dr1["ShipFlag"].ToString();
                    if (tab1.Columns.Contains("Gtllx"))
                    {
                        data1.Gtllx = dr1["Gtllx"].ToString();
                    }

                    //设置牵引费
                    if (dr1["Fee1"].ToString().Trim() != String.Empty)
                    {
                        data1.Fee1 = int.Parse(dr1["Fee1"].ToString().Trim());
                    }

                    if (dr1["Fee2"].ToString().Trim() != String.Empty)
                    {
                        data1.Fee2 = int.Parse(dr1["Fee2"].ToString().Trim());
                    }

                    //设置大站标志
                    if (dr1["Aname1"].ToString().Trim() != String.Empty)
                    {
                        data1.BigA = true;
                    }

                    if (dr1["Bname1"].ToString().Trim() != String.Empty)
                    {
                        data1.BigB = true;
                    }

                    data1.Parent.Add("0");
                    data1.ID = lay + "_" + dr1["ID"].ToString();
                    data1.PassedStation.Add("'" + data1.AStation + "'");

                    LineNode n1 = IsHasNodes(lay, data1);
                    if (n1 == null)
                    {
                        this.LineLayer[lay].Add(data1);     //将站点加入到层
                        if (this.LineAllStation[lay].Contains(data1.AStation) == false)
                        {
                            this.LineAllStation[lay].Add(data1.AStation);
                        }
                    }
                    else
                    {
                        n1.Parent.Add("0");
                    }
                }
                condition.Remove(AStation);
                condition.Remove(BStation);
            }
            else
            {
                //将上一层节点经过的数据全部Copy到本层
                foreach (String m in this.LineAllStation[lay - 1])
                {
                    this.LineAllStation[lay].Add(m);
                }
                
                //得到下一层的查询条件
                StringBuilder filter1 = new StringBuilder();
                for (int i = 0; i < this.LineLayer[lay - 1].Count; i++)
                {
                    LineNode node1 = this.LineLayer[lay - 1][i];
                    if (i == 0)
                    {
                        filter1.Append(node1.BStation);
                    }
                    else
                    {
                        filter1.Append(",").Append(node1.BStation);
                    }
                }

                //根据条件查询下一层的子节点
                String condition1 = filter1.ToString();
                if (String.IsNullOrEmpty(condition1) == false)
                {
                    SearchField Astation = new SearchField("Astation", condition1,SearchOperator.Collection);
                    SearchField BStation = new SearchField("Astation", this.BStation, SearchOperator.NotEqual);
                    condition.Add(Astation);
                    condition.Add(BStation);
                    String filter = SearchField.GetSearchCondition(condition);
                    DataRow[] drs = tab1.Select(filter);

                    //将查询到的数据增加到节点
                    foreach (DataRow dr1 in drs)
                    {
                        LineNode data1 = new LineNode();
                        data1.AStation = dr1["AStation"].ToString();
                        data1.BStation = dr1["BStation"].ToString();
                        data1.ID = lay + "_" + dr1["ID"].ToString();

                        data1.LineID = dr1["LineID"].ToString();
                        data1.Miles = int.Parse(dr1["Miles"].ToString());
                        data1.LineType = dr1["LineType"].ToString();
                        data1.JnFlag = dr1["JnFlag"].ToString();
                        data1.DqhFlag = dr1["dqh"].ToString();
                        data1.ShipFlag = dr1["ShipFlag"].ToString();
                        data1.Gtllx = dr1["Gtllx"].ToString();

                        //设置大站标志
                        if (dr1["Aname1"].ToString().Trim() != String.Empty)
                        {
                            data1.BigA = true;
                        }

                        if (dr1["Bname1"].ToString().Trim() != String.Empty)
                        {
                            data1.BigB = true;
                        }

                        //设置站点的父节点
                        bool goodStation = false;
                        bool check = true;
                        if (lay - 10 >= 0)
                        {
                            if(this.LineAllStation[lay-10].Contains(data1.BStation))
                            {
                                check =false;
                            }
                        }

                        if (check)
                        {
                            for (int i = 0; i < this.LineLayer[lay - 1].Count; i++)
                            {
                                LineNode parentNode = this.LineLayer[lay - 1][i];
                                if (parentNode.BStation == data1.AStation)
                                {
                                    if (parentNode.passedstation.Contains(data1.BStation) == false)
                                    {
                                        if (goodStation == false)
                                        {
                                            goodStation = true;
                                        }
                                        if (data1.Parent.Contains(parentNode.ID) == false)
                                        {
                                            data1.Parent.Add(parentNode.ID);
                                        }
                                        foreach (String m1 in parentNode.passedstation)
                                        {
                                            if (data1.passedstation.Contains(m1) == false)
                                            {
                                                data1.passedstation.Add(m1);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        //判断站点是否符合查询条件
                        if (goodStation)
                        {
                            data1.passedstation.Add(data1.AStation);
                            this.LineLayer[lay].Add(data1);              //将站点加入到层

                            if (this.LineAllStation[lay].Contains(data1.AStation) == false)
                            {
                                this.LineAllStation[lay].Add(data1.AStation);
                            }
                        }
                        
                    }
                }
            }
        }

        //清除无用的站点
        private void ClearNoUsedNodes()
        {
            int usedLayer = 0;
            bool flag = true;
            for (int i = this.MaxLayer; i >= 0; i--)
            {
                foreach (LineNode node1 in this.LineLayer[i])
                {
                    if (node1.BStation == this.BStation)
                    {
                        usedLayer = i;
                        flag = false;
                        break;
                    }
                }
                if (flag == false) break;
            }
            for (int i = usedLayer + 1; i < this.MaxLayer; i++)
            {
                this.LineLayer[i].Clear();   
            }
            this.MaxLayer = usedLayer;
            

            for (int j = this.MaxLayer; j > 0; j--)
            {
                for (int k = this.LineLayer[j].Count - 1; k >= 0; k--)
                {
                    LineNode node1 = this.LineLayer[j][k];
                    if (node1.BStation == this.BStation
                        || node1.Used)
                    {
                        for (int m = this.LineLayer[j - 1].Count-1; m >= 0; m--)
                        {
                            LineNode prevNode = this.LineLayer[j - 1][m];
                            if (node1.Parent.Contains(prevNode.ID))
                            {
                                prevNode.Used = true;
                            }
                        }
                    }
                    else
                    {
                        this.LineLayer[j].RemoveAt(k);
                    }
                }
            }
        }

        // 判断某层是否已存在该节点，如果存在，则返回该节点，否则返回空
        private LineNode IsHasNodes(int Layer, LineNode node1)
        {
            LineNode parent1=null;
            foreach (LineNode n1 in this.LineLayer[Layer])
            {
                if (node1.AStation == n1.AStation
                    && node1.BStation == n1.BStation
                    && node1.LineType== n1.LineType
                    && node1.Gtllx==n1.Gtllx)
                {
                    parent1=n1;
                    break;
                }
            }
            return parent1;
        }

        //判断线路是否通过中间站点
        private static bool CheckLineByMiddleStation(TrainLine line1, 
            String[] station,ETrainType trainType,bool hasDianChe,bool onlygg)
        {
            bool succ = true;
            if (station != null)
            {
                int pass1 = station.Length;
                int pass2 = 0;
                foreach (String m in station)
                {
                    succ = false;
                    TrainAliasBU bu1 = new TrainAliasBU();
                    if (CheckLineByMiddleStation(line1, m, bu1, trainType,hasDianChe,onlygg))
                    {
                        pass2++;
                        if (pass2 >= pass1)
                        {
                            succ = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                return CheckLineByMiddleStation(line1, null, null, trainType,hasDianChe,onlygg);
            }
            return succ;
        }

        // 判断线路是否通过某个中间站点（单一的站点，支持别名)
        private static bool CheckLineByMiddleStation
            (TrainLine line1,
             String station, 
             TrainAliasBU bu1,
             ETrainType trainType,
             bool hasDianChe,
             bool onlygg)
        {
            bool succ = false; 
            bool succ1=true;

            #region 线路限制条件
            switch (trainType)
            {
                case ETrainType.绿皮车25B: 
                    foreach (LineNode node1 in line1.Nodes)
                    {
                        if (node1.LineType == "1" || node1.LineType == "2")
                        {
                            succ1 = false;
                            break;
                        }

                        //增加了只输出高铁线的条件（2013年3月22日修改）
                        if (onlygg)
                        {
                            if (!(node1.LineType == "1" || node1.LineType == "2" || String.IsNullOrEmpty(node1.Gtllx) == false))
                            {
                                succ1 = false;
                                break;
                            }
                        }
                    }
                    break;

                case ETrainType.空调车25G:
                    foreach (LineNode node1 in line1.Nodes)
                    {
                        if (hasDianChe == false)   //直供电的25G等同25T
                        {
                            if (node1.LineType == "1" )
                            {
                                succ1 = false;
                                break;
                            }
                        }
                        else
                        {
                            if (node1.LineType == "1" || node1.LineType == "2")
                            {
                                succ1 = false;
                                break;
                            }
                        }

                        //增加了只输出高铁线的条件（2013年3月22日修改）
                        if (onlygg)
                        {
                            if (!(node1.LineType == "1" || node1.LineType == "2" || String.IsNullOrEmpty(node1.Gtllx) == false))
                            {
                                succ1 = false;
                                break;
                            }
                        }
                    }
                    break;

                case ETrainType.空调车25T:
                case ETrainType.空调车25K:     //重新调整了25K的选择线路的条件 25K的选择条件等同25T
                    foreach (LineNode node1 in line1.Nodes)
                    {
                        if (node1.LineType == "1")
                        {
                            succ1 = false;
                            break;
                        }

                        //增加了只输出高铁线的条件（2013年3月22日修改）
                        if (onlygg)
                        {
                            if (!(node1.LineType == "1" || node1.LineType == "2" || String.IsNullOrEmpty(node1.Gtllx) == false))
                            {
                                succ1 = false;
                                break;
                            }
                        }
                    }
                    break;

                case ETrainType.动车CRH2A:
                case ETrainType.动车CRH2E:
                case ETrainType.动车CRH2B:
                case ETrainType.动车CRH5A:

                    //增加电气化的限制条件
                    foreach (LineNode node1 in line1.Nodes)
                    {
                        if (String.IsNullOrEmpty(node1.DqhFlag))
                        {
                            succ1 = false;
                            break;
                        }

                        //增加了只输出高铁线的条件（2013年3月22日修改）
                        if (onlygg)
                        {
                            if (!(node1.LineType == "1" || node1.LineType == "2" || String.IsNullOrEmpty(node1.Gtllx) == false))
                            {
                                succ1 = false;
                                break;
                            }
                        }
                    }
                    break;

                //修改动车的限制条件
                case ETrainType.动车CRH2C:
                case ETrainType.动车CRH380A:
                case ETrainType.动车CRH380AL:
                case ETrainType.动车CRH380B:
                case ETrainType.动车CRH380BL:
                    foreach (LineNode node1 in line1.Nodes)
                    {
                        //说明：300公里的动车去掉特一线的限制或联络线的条件
                        if (!(node1.LineType == "1" 
                            || String.IsNullOrEmpty(node1.Gtllx)==false ))
                        {
                            succ1 = false;
                            break;
                        }

                        //增加电气化的条件
                        if (String.IsNullOrEmpty(node1.DqhFlag))
                        {
                            succ1 = false;
                            break;
                        }

                        //增加了只输出高铁线的条件（2013年3月22日修改）
                        if (onlygg)
                        {
                            if (!(node1.LineType == "1" || node1.LineType == "2" || String.IsNullOrEmpty(node1.Gtllx) == false))
                            {
                                succ1 = false;
                                break;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
            #endregion

            if (succ1 == true)
            {
                if (String.IsNullOrEmpty(station) == false)
                {
                    String m = station;
                    if (m.Trim() == String.Empty || Array.IndexOf(line1.ArrStation, m) >= 0)
                    {
                        succ = true;
                    }

                    /* List<String> list1 = bu1.GetAlias(station);
                    foreach (String m in list1)
                    {
                        if (m.Trim() == String.Empty || Array.IndexOf(line1.ArrStation, m) >= 0)
                        {
                            succ = true;
                            break;
                        }
                    }*/
                }
                else
                {
                    succ = true;
                }
            }
            return succ;
        }
        #endregion

        #endregion
    }
}
