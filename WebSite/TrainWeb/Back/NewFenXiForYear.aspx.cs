using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessRule;
using System.Reflection;
using System.Data;
using WebFrame.Util;
using WebFrame;
using org.in2bits.MyXls;
using BusinessRule.PubCode;

namespace WebSite.TrainWeb.Fenxi
{
    public partial class NewFenXiForYear : System.Web.UI.Page
    {
        //private double Rate = 0.9676;
        //计算当年的税金
        private double SRate
        {
            get
            {
                if (ViewState["SRate"] == null)
                {
                    double temp1 = SRateProfileBU.GetRate();
                    ViewState["SRate"] = temp1;
                    return temp1;
                }
                else
                {
                    return double.Parse(ViewState["SRate"].ToString());
                }
            }
        }

        Control[] con1 = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            con1=new Control[]{AStation,BStation,middleStation,trainList,sd};
            if (!Page.IsPostBack)
            {
                WebFrame.Util.JControl.SetLisControlByEnum(typeof(ETrainType),this.trainList);
                DataRow dr1=WebFrame.Util.JCookie.GetCookieValues("userFenXiData");
                if (dr1 != null)
                {
                    JControl.SetControlValues(con1, dr1);
                }
                else
                {
                    WebFrame.Util.JControl.SetListControlByValue(this.trainList, "0,1,3,4,7,8");
                }

                this.Button1.Attributes.Add("onclick", "ShowWaiting();");

                
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.Button1.Click += new EventHandler(Button1_Click);
            //this.Repeater2.ItemCommand += new RepeaterCommandEventHandler(Repeater2_ItemCommand);
            base.OnInit(e);
        }

        void Repeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            RepeaterItem item = e.Item ;

            TrainLine line0 = null;                         //表示选中的线路
            
            ETrainType train1 = ETrainType.空调车25T;       //表示列车类型
            double cdsvalue = 1;                            //表示车底数
            String bianZhu = String.Empty;                  //列车的编组

            if (item != null)
            {
                Label lab1 = item.FindControl("traintype") as Label;
                if (lab1 != null)
                {
                    ListControl rad1 = item.FindControl("selLine") as ListControl;
                    if (rad1 != null)
                    {
                        List<TrainLine> list1 = null;
                        if (lab1.Text == "0" || lab1.Text == "1"
                        || lab1.Text == "2" || lab1.Text == "21")                            //普通车
                        {
                            list1 = ViewState["Line4"] as List<TrainLine>;
                        }
                        else if (lab1.Text == "3")                                            //直达车
                        {
                            list1 = ViewState["Line3"] as List<TrainLine>;
                        }
                        else if (lab1.Text == "4" || lab1.Text == "5")                      //200公里高速
                        {
                            list1 = ViewState["Line2"] as List<TrainLine>;
                        }
                        else if (lab1.Text == "6" || lab1.Text == "7" || lab1.Text == "8")  //300公里高速
                        {
                            list1 = ViewState["Line1"] as List<TrainLine>;
                        }

                        line0 = list1[rad1.SelectedIndex];
                    }

                    ListControl cds = item.FindControl("cds") as ListControl;
                    if (cds != null) cdsvalue = double.Parse(cds.SelectedValue);
                    int traintype1 = int.Parse(lab1.Text);
                    if (traintype1 == 21)
                    {
                        traintype1 = 2;
                    }
                    train1 = (ETrainType)(traintype1);

                    Label bianzhu1 = item.FindControl("bianzhu") as Label;
                    if (bianzhu1 != null)
                    {
                        bianZhu = bianzhu1.Text;
                    }
                    
                    //开始计算其中的数值
                    if (line0 != null)
                    {
                        BusinessRule.PubCode.Util.ExportData("trainData1.xls");
                    }
                }
            }
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            /*
            foreach (RepeaterItem item in Repeater2.Items)
            {
                Label lab1 = item.FindControl("traintypeText") as Label;
                if (lab1 != null)
                {
                    if (Rpt1.Text == String.Empty)
                    {
                        Rpt1.Text = lab1.Text;
                    }
                    else
                    {
                        Rpt1.Text = Rpt1.Text + "," + lab1.Text;
                    }
                }
            }*/
            base.OnPreRenderComplete(e);
        }

        /// <summary>
        /// 分析新的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Button1_Click(object sender, EventArgs e)
        {
            //查询结果
            Line newLine = null;
            List<TrainLine> line1 = null;
            List<TrainLine> line2 = null;
            List<TrainLine> line3 = null;
            List<TrainLine> line4 = null;
            
            String A0 = this.AStation.Text.Trim();
            String B0 = this.BStation.Text.Trim();
            if (String.IsNullOrEmpty(A0) == false
                && String.IsNullOrEmpty(B0) == false)
            {

                bool flag = true;
                if (Line.isExistsStation(A0) == false)
                {
                    WebFrame.Util.JAjax.Alert("错误：起始站【" + A0 + "】不存在！");
                    flag = false;
                }

                if (flag)
                {
                    if (Line.isExistsStation(B0) == false)
                    {
                        WebFrame.Util.JAjax.Alert("错误：终点站【" + B0 + "】不存在！");
                        flag = false;
                    }
                }

                if (flag)
                {
                    //将用户当前的选择条件保存到Cookie
                    DataRow dr1 = JControl.GetControlValues(con1);
                    WebFrame.Util.JCookie.SetCookieValues("userFenXiData", dr1, -1);

                    //寻找四类典型的线路
                    String[] middle = null;
                    String middleTemp = String.Empty;
                    if (this.middleStation.Text.Trim() != String.Empty)
                    {
                        middleTemp = this.middleStation.Text.Trim().Replace("，", ",");
                        middle = middleTemp.Split(',');
                    }
                    int shendu = int.Parse(this.sd.SelectedValue);

                    if (ViewState["A0"] != null)
                    {
                        //1--从视图状态中恢复数据
                        if (ViewState["A0"].ToString() == A0
                            && ViewState["B0"].ToString() == B0
                            && ViewState["shendu"].ToString() == shendu.ToString()
                            && ViewState["SelTrainID"].ToString() == this.selTrain.Text)
                        {
                            if (ViewState["middle"].ToString() == middleTemp)
                            {
                                line1 = ViewState["Line1"] as List<TrainLine>;
                                line2 = ViewState["Line2"] as List<TrainLine>;
                                line3 = ViewState["Line3"] as List<TrainLine>;
                                line4 = ViewState["Line4"] as List<TrainLine>;
                            }
                        }
                    }

                    //2--从数据库中恢复数据
                    if (line1 == null && this.CheckBox1.Checked == false)
                    {
                       // SearchObjectBU.RestoreSearchResultFromDb(A0, B0, shendu,String.Empty, out newLine);
                    }

                    //4--执行搜索算法得到数据
                    if ((line1 == null && newLine == null) || this.CheckBox1.Checked)
                    {
                        newLine = new Line(A0, B0, shendu, this.selTrain.Text.Trim(),300,false );
                    }

                    if ((line1 == null && newLine != null) || this.CheckBox1.Checked)
                    {
                        /*------------此处需要修改----------------*/
                        line1 = newLine.GetLayerLine(middle, ETrainType.动车CRH380A,false, 1,false);
                        line2 = newLine.GetLayerLine(middle, ETrainType.动车CRH2A,false, 1,false);
                        line3 = newLine.GetLayerLine(middle, ETrainType.空调车25T, false,3,false);
                        line4 = newLine.GetLayerLine(middle, ETrainType.绿皮车25B,true, 3,false);
                    }

                    //将数据保存到视图状态
                    ViewState["A0"] = A0;
                    ViewState["B0"] = B0;
                    ViewState["shendu"] = shendu;
                    ViewState["SelTrainID"] = this.selTrain.Text;
                    ViewState["middle"] = middleTemp;

                    //ViewState["MyLine"] = newLine;
                    ViewState["Line1"] = line1;    //300公里动车的线路
                    ViewState["Line2"] = line2;    //200公里动车的线路
                    ViewState["Line3"] = line3;    //直达线路
                    ViewState["Line4"] = line4;    //普通车的线路

                    Dictionary<String, String> data1 = CommTrain.GetNewFenXiData(0);
                    this.Repeater2.DataSource = data1;
                    this.Repeater2.DataBind();
                    this.SearchInfo.Visible = true;
                    this.NewBindComplete();

                    //将查询结果保存到数据库中(只保存没有指定线路的站点）
                    if (String.IsNullOrEmpty(this.selTrain.Text.Trim()))
                    {
                      //  bool succ = SearchObjectBU.SaveSearchResultToDb(A0, B0, shendu,newLine, this.CheckBox1.Checked,String.Empty);
                    }
                    this.CheckBox1.Checked = false;
                }
            }
            else
            {
                WebFrame.Util.JAjax.Alert("错误：请输入车次的起点和终点");
            }
        }


        /// <summary>
        /// 绑定Repeater的数据
        /// </summary>
        private void NewBindComplete()
        {
            foreach (RepeaterItem item in this.Repeater2.Items)
            {
                Label lab1 = item.FindControl("traintype") as Label;
                if (lab1 != null)
                {
                    //设置不同的车底数
                    ListControl cds = item.FindControl("cds") as ListControl;
                    if (cds != null)
                    {
                        cds.Items.Clear();
                        if (lab1.Text == "0" || lab1.Text == "1"
                            || lab1.Text == "2" || lab1.Text == "3"
                            || lab1.Text == "21")
                        {
                            for (int i = 1; i <= 6; i++)
                            {
                                ListItem list1 = new ListItem(i + "", i + "");
                                cds.Items.Add(list1);
                            }
                            cds.SelectedValue = "2";
                        }
                        else
                        {
                            cds.Items.Clear();
                            for (double i = 0.3; i <= 2.1; i = i + 0.1)
                            {
                                ListItem list1 = new ListItem(i + "", i + "");
                                cds.Items.Add(list1);
                            }
                            cds.SelectedValue = "1";
                        }
                    }
                    
                    //设置其他的控件
                    ListControl rad1 = item.FindControl("selLine") as ListControl;
                    if (rad1 != null)
                    {
                        List<TrainLine> list1 = null;
                        ETrainType type1 = ETrainType.绿皮车25B;
                        if (lab1.Text == "21")
                        {
                            type1 = ETrainType.空调车25G;
                        }
                        else
                        {
                            type1 = (ETrainType)(int.Parse(lab1.Text));
                        }

                        //===================================================================
                        if (lab1.Text == "0" || lab1.Text == "1"
                            || lab1.Text == "2" || lab1.Text=="21")                         //普通车
                        {
                            list1 = ViewState["Line4"] as List<TrainLine>;
                        }
                        else if (lab1.Text=="3")                                            //直达车
                        {
                            list1 = ViewState["Line3"] as List<TrainLine>;
                        }
                        else if (lab1.Text == "4" || lab1.Text == "5")                      //200公里高速
                        {
                            list1 = ViewState["Line2"] as List<TrainLine>;
                        }
                        else if (lab1.Text == "6" || lab1.Text == "7" || lab1.Text == "8")  //300公里高速
                        {
                            list1 = ViewState["Line1"] as List<TrainLine>;
                        }

                        //调整车底数的默认值
                        if (list1 != null && list1.Count>0)
                        {
                            TrainLine line0 = list1[0];
                            int miles = line0.TotalMiles;
                            String defaultcds = Util.GetDefaultCds(miles, type1)+"";
                            if (cds.Items.FindByValue(defaultcds) != null)
                            {
                                cds.SelectedValue = defaultcds;

                                //设置其他的车底数不能用（仅适用于客车）
                                if (lab1.Text == "0" || lab1.Text == "1"
                                || lab1.Text == "2" || lab1.Text == "3"
                                || lab1.Text == "21")
                                {
                                    for (int i = 0; i < cds.Items.Count; i++)
                                    {
                                        double d1 = double.Parse(cds.Items[i].Value);
                                        if (d1 < double.Parse(defaultcds))
                                        {
                                            cds.Items[i].Enabled = false;
                                        }
                                        else
                                        {
                                            cds.Items[i].Enabled = true;
                                        }
                                    }
                                }
                            }
                        }

                        //设置选中的线路
                        this.SetLineItem(rad1, list1);


                        //计算收入和支持
                        if (rad1.Items.Count > 0)
                        {
                            item.Visible = true;
                            //列车收入和支出
                           this.NewCalShourAndZhiChu(list1[0], item);
                        }
                        else
                        {
                            item.Visible = false;
                        }
                    }

                }
            }
        }

        private void SetLineItem(ListControl list1, List<TrainLine> line)
        {
            list1.Items.Clear();
            int i = 0;
            foreach (TrainLine m in line)
            {
                int total = m.TotalMiles;
                String KeyID = i+"";
                String Text = "<span title='"+m.ToString()+"'>"+m.ToBigString() + " (" + total+"km)"+"</span>";
                //String Text = "<span title='" + m.ToString() + "'>" + m.ToString() + " (" + total + "km)" + "</span>";
                ListItem item1 = new ListItem(Text, KeyID);
                list1.Items.Add(item1);
                i++;
            }

            if (list1.Items.Count > 0)
            {
                list1.SelectedIndex =0;
            }
        }

        protected void selLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListControl obj1 = sender as ListControl;
            if (obj1 != null)
            {
                RepeaterItem item1 = obj1.Parent as RepeaterItem;
                if (item1 != null)
                {
                    int index1 = obj1.SelectedIndex;
                    Label lab1 = item1.FindControl("traintype") as Label;
                    List<TrainLine> list1 = null;

                    if (lab1.Text == "0" || lab1.Text == "1" 
                        || lab1.Text == "2" || lab1.Text=="21" )       //普通车
                    {
                        list1 = ViewState["Line4"] as List<TrainLine>;
                    }
                    else if (lab1.Text == "3")                                            //直达车
                    {
                        list1 = ViewState["Line3"] as List<TrainLine>;
                    }
                    else if (lab1.Text == "4" || lab1.Text == "5")                      //200公里高速
                    {
                        list1 = ViewState["Line2"] as List<TrainLine>;
                    }
                    else if (lab1.Text == "6" || lab1.Text == "7" || lab1.Text == "8")  //300公里高速
                    {
                        list1 = ViewState["Line1"] as List<TrainLine>;
                    }

                    TrainLine line0 = list1[index1];
                    this.NewCalShourAndZhiChu(line0, item1);
                }
            }
        }

        protected void cds_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListControl obj1 = sender as ListControl;
            if (obj1 != null)
            {
                RepeaterItem item1 = obj1.Parent as RepeaterItem;
                if (item1 != null)
                {
                    ListControl selline = item1.FindControl("selLine") as ListControl;
                    int index1 = selline.SelectedIndex;

                    Label lab1 = item1.FindControl("traintype") as Label;
                    List<TrainLine> list1 = null;

                    if (lab1.Text == "0" || lab1.Text == "1" || lab1.Text == "2"
                        || lab1.Text == "21")       //普通车
                    {
                        list1 = ViewState["Line4"] as List<TrainLine>;
                    }
                    else if (lab1.Text == "3")                                            //直达车
                    {
                        list1 = ViewState["Line3"] as List<TrainLine>;
                    }
                    else if (lab1.Text == "4" || lab1.Text == "5")                      //200公里高速
                    {
                        list1 = ViewState["Line2"] as List<TrainLine>;
                    }
                    else if (lab1.Text == "6" || lab1.Text == "7" || lab1.Text == "8")  //300公里高速
                    {
                        list1 = ViewState["Line1"] as List<TrainLine>;
                    }

                    TrainLine line0 = list1[index1];
                    this.NewCalShourAndZhiChu(line0, item1);
                }
            }
        }

        /// <summary>
        /// 计算收入和支出  20140417
        /// </summary>
        /// <param name="line0"></param>
        /// <param name="item"></param>
        private void NewCalShourAndZhiChu(TrainLine line0, RepeaterItem item)
        {
            bool hasDianChe = false;        //表示是否有发电车

            Label lab1 = item.FindControl("traintype") as Label;
            ListControl cds = item.FindControl("cds") as ListControl;

            int traintype1 = int.Parse(lab1.Text);
            if (traintype1 == 21)
            {
                traintype1 = 2;
                hasDianChe = true;
            }

            ETrainType train1 = (ETrainType)(traintype1);
            double shouru1 = 0.0;
            int yz = 0, yw = 0, rw = 0,ca=0,sy1=0;
            int totalPeople = 0;

            List<ZhiChuData>[] arrZc = new List<ZhiChuData>[4];
            arrZc[0] = new List<ZhiChuData>();
            arrZc[1] = new List<ZhiChuData>();
            arrZc[2] = new List<ZhiChuData>();
            arrZc[3] = new List<ZhiChuData>();
            double JnFee = 0;
           // int cbmode = int.Parse(this.cb.selectIndex);

            int totalBianZhu = 18;
            Line.GetLineShouruAndZhiChu(train1, line0, cds.SelectedValue,totalBianZhu,
                out shouru1,ref yz,ref yw,ref rw,ref ca,
                ref sy1,
                out totalPeople,
                out arrZc[0],out arrZc[1],out arrZc[2],out arrZc[3],
                out JnFee,hasDianChe,true,0,"");           

            //列车的收入
            shouru1 = shouru1 / 10000;
            Label labShouRu = item.FindControl("shouru") as Label;
            labShouRu.Text = String.Format("{0:n0}", shouru1);

            //计算列车的运输总人数
            Label labTotalPeople = item.FindControl("totalpeople") as Label;
            labTotalPeople.Text = String.Format("{0:n0}", totalPeople);

            //计算列车的总支出和合适的车厢编组
            for (int i = 1; i <= 4; i++)
            {
                LinkButton labZhiChu = item.FindControl("zhichu"+i) as LinkButton;
                double sum1 = 0;
                String zc1 = String.Empty;
                foreach (ZhiChuData d1 in arrZc[i-1])
                {
                    sum1 = sum1 + d1.ZhiChu;
                    if (zc1 == String.Empty)
                    {
                        zc1 = d1.ZhiChu.ToString();
                    }
                    else
                    {
                        zc1 = zc1 + "," + d1.ZhiChu.ToString();
                    }
                }
                zc1 = zc1 + "," + JnFee;

                sum1 = sum1 / 10000;
                labZhiChu.Text = String.Format("{0:n0}", sum1);
                labZhiChu.OnClientClick = "javascript:ShowZhiChu('" + zc1 + "');return false;";

                //设置收支盈亏
                Label labsz = item.FindControl("sz" + i) as Label;
                labsz.Text = String.Format("{0:n0}", shouru1*SRate-sum1);

                //计算上座率
                Label labSzr = item.FindControl("szr"+i) as Label;
                labSzr.Text=String.Format("{0:n0}%", 100*sum1/(shouru1*SRate) );

            }

            //显示列车的合适编组
            HyperLink link2 = item.FindControl("link2") as HyperLink;
            HyperLink link3 = item.FindControl("link3") as HyperLink;
            if (link2 != null)
            {
                if (int.Parse(lab1.Text) > 3 && lab1.Text != "21")
                {
                    link2.Visible = false;
                    link3.NavigateUrl = "SeeWordFile2.aspx?IsYear=1&TrainType=" + lab1.Text + "&Line=" + Server.UrlEncode(line0.ToString())
                                     + "&cds=" + cds.SelectedValue + "&hasDianChe=0&BianZhu=0,0,0";
                }
                else
                {
                    if (lab1.Text != "21")
                    {
                        link2.NavigateUrl = "BianZhuFenXi.aspx?IsYear=1&TrainType=" + lab1.Text + "&Line=" + Server.UrlEncode(line0.ToString())
                                            + "&cds=" + cds.SelectedValue + "&hasDianChe=0&BianZhu=" + Server.UrlEncode(String.Format("{0},{1},{2}", yz, yw, rw));
                    }
                    else
                    {
                        link2.NavigateUrl = "BianZhuFenXi.aspx?IsYear=1&TrainType=2&Line=" + Server.UrlEncode(line0.ToString())
                                           + "&cds=" + cds.SelectedValue + "&hasDianChe=1&BianZhu=" + Server.UrlEncode(String.Format("{0},{1},{2}", yz, yw, rw));
                    }
                    link2.ToolTip = String.Format("最优的编组配置为：硬座{0}/ 硬卧{1}/ 软卧{2}", yz, yw, rw);
                    link3.NavigateUrl = link2.NavigateUrl.Replace("BianZhuFenXi.aspx", "SeeWordFile.aspx");
                }
            }
            

            Label bianzhu1 = item.FindControl("bianzhu") as Label;
            if (bianzhu1 != null)
            {
                bianzhu1.Text = String.Format("{0}-{1}-{2}", yz, yw, rw);
            }
        }
        /// <summary>
        /// 计算收入和支出  原代码
        /// </summary>
        /// <param name="line0"></param>
        /// <param name="item"></param>
        //private void NewCalShourAndZhiChu(TrainLine line0, RepeaterItem item)
        //{
        //    bool hasDianChe = false;        //表示是否有发电车

        //    Label lab1 = item.FindControl("traintype") as Label;
        //    ListControl cds = item.FindControl("cds") as ListControl;

        //    int traintype1 = int.Parse(lab1.Text);
        //    if (traintype1 == 21)
        //    {
        //        traintype1 = 2;
        //        hasDianChe = true;
        //    }

        //    ETrainType train1 = (ETrainType)(traintype1);
        //    double shouru1 = 0.0;
        //    int yz = 0, yw = 0, rw = 0, ca = 0, sy1 = 0;
        //    int totalPeople = 0;

        //    List<ZhiChuData>[] arrZc = new List<ZhiChuData>[4];
        //    arrZc[0] = new List<ZhiChuData>();
        //    arrZc[1] = new List<ZhiChuData>();
        //    arrZc[2] = new List<ZhiChuData>();
        //    arrZc[3] = new List<ZhiChuData>();
        //    double JnFee = 0;
        //    // int cbmode = int.Parse(this.cb.selectIndex);

        //    int totalBianZhu = 18;
        //    Line.GetLineShouruAndZhiChu(train1, line0, cds.SelectedValue, totalBianZhu,
        //        out shouru1, ref yz, ref yw, ref rw, ref ca,
        //        ref sy1,
        //        out totalPeople,
        //        out arrZc[0], out arrZc[1], out arrZc[2], out arrZc[3],
        //        out JnFee, hasDianChe, true, 0, "");

        //    //列车的收入
        //    shouru1 = shouru1 / 10000;
        //    Label labShouRu = item.FindControl("shouru") as Label;
        //    labShouRu.Text = String.Format("{0:n0}", shouru1);

        //    //计算列车的运输总人数
        //    Label labTotalPeople = item.FindControl("totalpeople") as Label;
        //    labTotalPeople.Text = String.Format("{0:n0}", totalPeople);

        //    //计算列车的总支出和合适的车厢编组
        //    for (int i = 1; i <= 4; i++)
        //    {
        //        LinkButton labZhiChu = item.FindControl("zhichu" + i) as LinkButton;
        //        double sum1 = 0;
        //        String zc1 = String.Empty;
        //        foreach (ZhiChuData d1 in arrZc[i - 1])
        //        {
        //            sum1 = sum1 + d1.ZhiChu;
        //            if (zc1 == String.Empty)
        //            {
        //                zc1 = d1.ZhiChu.ToString();
        //            }
        //            else
        //            {
        //                zc1 = zc1 + "," + d1.ZhiChu.ToString();
        //            }
        //        }
        //        zc1 = zc1 + "," + JnFee;

        //        sum1 = sum1 / 10000;
        //        labZhiChu.Text = String.Format("{0:n0}", sum1);
        //        labZhiChu.OnClientClick = "javascript:ShowZhiChu('" + zc1 + "');return false;";

        //        //设置收支盈亏
        //        Label labsz = item.FindControl("sz" + i) as Label;
        //        labsz.Text = String.Format("{0:n0}", shouru1 * SRate - sum1);

        //        //计算上座率
        //        Label labSzr = item.FindControl("szr" + i) as Label;
        //        labSzr.Text = String.Format("{0:n0}%", 100 * sum1 / (shouru1 * SRate));

        //    }

        //    //显示列车的合适编组
        //    HyperLink link2 = item.FindControl("link2") as HyperLink;
        //    HyperLink link3 = item.FindControl("link3") as HyperLink;
        //    if (link2 != null)
        //    {
        //        if (int.Parse(lab1.Text) > 3 && lab1.Text != "21")
        //        {
        //            link2.Visible = false;
        //            link3.NavigateUrl = "SeeWordFile2.aspx?IsYear=1&TrainType=" + lab1.Text + "&Line=" + Server.UrlEncode(line0.ToString())
        //                             + "&cds=" + cds.SelectedValue + "&hasDianChe=0&BianZhu=0,0,0";
        //        }
        //        else
        //        {
        //            if (lab1.Text != "21")
        //            {
        //                link2.NavigateUrl = "BianZhuFenXi.aspx?IsYear=1&TrainType=" + lab1.Text + "&Line=" + Server.UrlEncode(line0.ToString())
        //                                    + "&cds=" + cds.SelectedValue + "&hasDianChe=0&BianZhu=" + Server.UrlEncode(String.Format("{0},{1},{2}", yz, yw, rw));
        //            }
        //            else
        //            {
        //                link2.NavigateUrl = "BianZhuFenXi.aspx?IsYear=1&TrainType=2&Line=" + Server.UrlEncode(line0.ToString())
        //                                   + "&cds=" + cds.SelectedValue + "&hasDianChe=1&BianZhu=" + Server.UrlEncode(String.Format("{0},{1},{2}", yz, yw, rw));
        //            }
        //            link2.ToolTip = String.Format("最优的编组配置为：硬座{0}/ 硬卧{1}/ 软卧{2}", yz, yw, rw);
        //            link3.NavigateUrl = link2.NavigateUrl.Replace("BianZhuFenXi.aspx", "SeeWordFile.aspx");
        //        }
        //    }


        //    Label bianzhu1 = item.FindControl("bianzhu") as Label;
        //    if (bianzhu1 != null)
        //    {
        //        bianzhu1.Text = String.Format("{0}-{1}-{2}", yz, yw, rw);
        //    }
        //}
    }
}
