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
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using WebFrame.Data;

namespace WebSite.TrainWeb.Fenxi
{
    public partial class NewCheCiFenXi2013 : System.Web.UI.Page
    {
        #region 相关属性
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

        public String PageTitle { get; set; }
        //private double Rate = 0.9676;

        Control[] con1 = null;
        #endregion

        protected override void OnInit(EventArgs e)
        {
            this.Button1.Click += new EventHandler(Button1_Click);
            this.SetSCBtn.Click += new EventHandler(SetSCBtn_Click);
            this.Repeater2.ItemDataBound += new RepeaterItemEventHandler(Repeater2_ItemDataBound);

            this.bz.SelectedIndexChanged += new EventHandler(bz_SelectedIndexChanged);
            this.cb.SelectedIndexChanged += new EventHandler(cb_SelectedIndexChanged);
            this.bzs0.SelectedIndexChanged += new EventHandler(bzs0_SelectedIndexChanged);

            this.yw.TextChanged += new EventHandler(yw_TextChanged);
            this.yz.TextChanged += new EventHandler(yw_TextChanged);
            this.rw.TextChanged += new EventHandler(yw_TextChanged);
            this.ca.TextChanged += new EventHandler(yw_TextChanged);
            this.sy.TextChanged += new EventHandler(yw_TextChanged);
            base.OnInit(e);
        }

        void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;
            Label lab1 = item.FindControl("traintype") as Label;
            Label lab2 = item.FindControl("traintypeText") as Label;

            //设置分析的车型
            this.SetFenXiTrainType(item);

            //设置可用的线路
            this.SetCanUseLind(item);

            //设置不同的车底数
            this.SetCds(item);

            //设置默认的车底数
            this.SetDefaultCds(item);

            //计算收入和支出
            //this.CalShourAndZhiChu(item);//原来代码
            ListControl rad1 = item.FindControl("selLine") as ListControl;
            List<SearchField> condition = new List<SearchField>();
            JTable tab1 = new JTable("LINESTATION");
            JTable tab2 = new JTable("trainline");
            List<string[]> lineInfos = new List<string[]>();
            if (rad1 != null)
            {
                if (rad1.Items.Count > 0)
                {
                    int i = rad1.SelectedIndex;
                    //获得所在的linestation 的主键id  根据线路具体信息
                    List<TrainLine> list1 = this.GetRowLine(item);
                    string lineRemark = list1[i].ToString();//线路完整长度
                    if (!string.IsNullOrEmpty(lineRemark))
                    {
                        string[] linepoint = lineRemark.Split('-');
                        for (int j = 0; j < linepoint.Length - 1; j++)
                        {
                            condition.Clear();
                            condition.Add(new SearchField("astation", linepoint[j]));
                            condition.Add(new SearchField("bstation", linepoint[j + 1]));
                            string[] lineinfo = new string[3];
                            lineinfo[0] = tab1.SearchScalar(condition, "KZID").ToString();//获得客专id编号
                            lineinfo[1] = tab1.SearchScalar(condition, "miles").ToString();//获得里程
                            string lineID = tab1.SearchScalar(condition, "lineid").ToString();
                            condition.Clear();
                            condition.Add(new SearchField("lineid", lineID));
                            lineinfo[2] = tab2.SearchScalar(condition, "linetype") == null ? "" : tab2.SearchScalar(condition, "linetype").ToString();//获得线路类型
                            lineInfos.Add(lineinfo);

                        }

                    }


                }

            }
            this.CalShourAndZhiChu(item, lineInfos);//2014 04 17

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            con1 = new Control[] { AStation, BStation, middleStation, trainList, sd };
            if (!Page.IsPostBack)
            {
                //this.mulLine.Attributes["ReadOnly"] = "true";
                WebFrame.Util.JControl.SetLisControlByEnum(typeof(ETrainType), this.trainList);
                DataRow dr1 = WebFrame.Util.JCookie.GetCookieValues("userFenXiData");
                if (dr1 != null)
                {
                    JControl.SetControlValues(con1, dr1);

                }
                else
                {
                    WebFrame.Util.JControl.SetListControlByValue(this.trainList, "0,1,3,4,7,8");
                }

                this.Button1.Attributes.Add("onclick", "ShowWaiting();");

                //显示是分趟标志还是分年标志
                if (String.IsNullOrEmpty(Request.QueryString["isYearFlag"]) == false)
                {
                    this.isYearPage.Value = "1";
                }

                if (this.isYearPage.Value == "0")
                {
                    this.titleInfo1.Text = "财务数据模型-新增车次 <b>按趟</b> 分析";
                    this.t1.Text = "趟";
                    this.t2.Text = "趟";
                    this.t3.Text = "趟";
                    this.t4.Text = "趟";
                    this.t5.Text = "趟";
                }
                else
                {
                    this.titleInfo1.Text = "财务数据模型-新增车次 <b>按年</b> 分析";
                    this.t1.Text = "年";
                    this.t2.Text = "年";
                    this.t3.Text = "年";
                    this.t4.Text = "年";
                    this.t5.Text = "年";
                    this.labInfo1.Text = "车底数";
                }

               
            }
        }

        //搜索按钮
        void Button1_Click(object sender, EventArgs e)
        {
            this.SearchLine();
        }

        //设置查询条件按钮
        void SetSCBtn_Click(object sender, EventArgs e)
        {
            //string ss = this.textfind.Text.ToString().Trim();
            Session["FindCond"] = this.textfind.Value.ToString().Trim();
        }

        #region Private Function
        // 分析线路
        private void SearchLine()
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

                    if (ViewState["A0"] != null)
                    {
                        //1--从视图状态中恢复数据
                        if (ViewState["A0"].ToString() == A0
                            && ViewState["B0"].ToString() == B0
                            && ViewState["middle"].ToString() == middleTemp
                            && ViewState["fengduanCondition"].ToString() == this.mulLine.Text
                            && ViewState["onlygg"].ToString() == this.onlygg.Checked.ToString())
                        {
                            line1 = ViewState["Line1"] as List<TrainLine>;
                            line2 = ViewState["Line2"] as List<TrainLine>;
                            line3 = ViewState["Line3"] as List<TrainLine>;
                            line4 = ViewState["Line4"] as List<TrainLine>;
                        }
                    }

                    //2--从数据库中恢复数据
                    if (line1 == null)
                    {
                        SearchObjectBU.RestoreSearchResultFromDb(A0, B0, this.mulLine.Text.Trim(), out newLine);
                        if (newLine == null)
                        {
                            if (this.mulLine.Text.Trim() == String.Empty)
                            {
                                newLine = Line.GetLine(A0, B0, 100, this.selTrain.Text.Trim(), this.onlygg.Checked);
                            }
                            else
                            {
                                newLine = Line.GetLineByFengDuanSearch(this.mulLine.Text.Trim(), this.onlygg.Checked);
                            }
                        }

                        if (newLine != null)
                        {
                            line1 = newLine.GetLayerLine(middle, ETrainType.动车CRH380A, false, 1, this.onlygg.Checked);
                            line2 = newLine.GetLayerLine(middle, ETrainType.动车CRH2A, false, 1, this.onlygg.Checked);
                            line3 = newLine.GetLayerLine(middle, ETrainType.空调车25T, false, 3, this.onlygg.Checked);
                            line4 = newLine.GetLayerLine(middle, ETrainType.绿皮车25B, true, 3, this.onlygg.Checked);

                            //重新调整算法
                            if (line1.Count == 0 || line2.Count == 0)
                            {
                                Line dcnewLine = null;
                                if (this.mulLine.Text.Trim() == String.Empty)
                                {
                                    dcnewLine = Line.GetLine(A0, B0, 100, this.selTrain.Text.Trim(), true);
                                }
                                else
                                {
                                    dcnewLine = Line.GetLineByFengDuanSearch(this.mulLine.Text.Trim(), true);
                                }

                                if (dcnewLine != null)
                                {
                                    line1 = dcnewLine.GetLayerLine(middle, ETrainType.动车CRH380A, false, 1, true);
                                }

                                if (line2.Count == 0 && newLine != null)
                                {
                                    line2 = dcnewLine.GetLayerLine(middle, ETrainType.动车CRH2A, false, 1, true);
                                }
                            }
                        }
                    }

                    //3--将数据保存到视图状态
                    ViewState["A0"] = A0;
                    ViewState["B0"] = B0;
                    ViewState["middle"] = middleTemp;
                    ViewState["fengduanCondition"] = this.mulLine.Text;
                    ViewState["onlygg"] = this.onlygg.Checked.ToString();

                    //ViewState["MyLine"] = newLine;
                    ViewState["Line1"] = line1;    //300公里动车的线路
                    ViewState["Line2"] = line2;    //200公里动车的线路
                    ViewState["Line3"] = line3;    //直达线路
                    ViewState["Line4"] = line4;    //普通车的线路

                    //4--将查询对象保存到数据库
                    SearchObjectBU.SaveSearchResultToDb(A0, B0, newLine, this.mulLine.Text.Trim());


                    //=========设置车型的选择========================
                    int index1 = bz.SelectedIndex;
                    if (index1 == 3) index1 = 2;
                    Dictionary<String, String> data1 = CommTrain.GetNewFenXiData2013(index1);
                    this.Repeater2.DataSource = data1;
                    this.Repeater2.DataBind();
                    this.SearchInfo.Visible = true;
                    //this.NewBindComplete();

                    String fullChengBen = String.Empty;
                    if (this.cb.SelectedIndex == 1)
                    {
                        fullChengBen = "全成本";
                    }

                    if (this.isYearPage.Value == "0")
                    {
                        this.t1.Text = fullChengBen + "趟";
                        this.t2.Text = fullChengBen + "趟";
                        this.t3.Text = fullChengBen + "趟";
                        this.t4.Text = fullChengBen + "趟";
                        this.t5.Text = fullChengBen + "趟";
                    }
                    else
                    {
                        this.t1.Text = fullChengBen + "年";
                        this.t2.Text = fullChengBen + "年";
                        this.t3.Text = fullChengBen + "年";
                        this.t4.Text = fullChengBen + "年";
                        this.t5.Text = fullChengBen + "年";
                    }
                }
            }
            else
            {
                WebFrame.Util.JAjax.Alert("错误：请输入车次的起点和终点");
            }
        }

        //设置方法（1）==设置分析的车型
        private void SetFenXiTrainType(RepeaterItem item)
        {
            //设置车型的下拉框
            DropDownList drop1 = item.FindControl("selTrainType") as DropDownList;
            Label lab1 = item.FindControl("traintype") as Label;
            Label lab2 = item.FindControl("traintypeText") as Label;
            if (drop1 != null)
            {
                //300公里的动车
                if (lab1.Text == "6" || lab1.Text == "7" || lab1.Text == "8" || lab1.Text == "11" || lab1.Text == "12")
                {
                    CommTrain.SetDongCheListControl(drop1, 1);
                    lab2.Visible = false;
                }
                else if (lab1.Text == "4" || lab1.Text == "5" || lab1.Text == "9" || lab1.Text == "10") //200公里动车
                {
                    CommTrain.SetDongCheListControl(drop1, 0);
                    lab2.Visible = false;
                }
                else if (lab1.Text == "2")   //直供电25G和25K
                {
                    CommTrain.SetCommCheListControl(drop1, 1);
                    lab2.Visible = false;
                }
                else if (lab1.Text == "21")  //非直供电25G和25K
                {
                    CommTrain.SetCommCheListControl(drop1, 0);
                    lab2.Visible = false;
                }
                else
                {
                    drop1.Visible = false;
                }

                if (drop1.Items.FindByValue(lab1.Text) != null)
                {
                    drop1.SelectedValue = lab1.Text;
                }
                else
                {
                    drop1.SelectedIndex = 0;
                }
            }
        }

        //设置方法（2）==设置可用的线路
        private void SetCanUseLind(RepeaterItem item)
        {
            //设置可用的线路
            ListControl rad1 = item.FindControl("selLine") as ListControl;
            Label lab1 = item.FindControl("traintype") as Label;
            if (rad1 != null)
            {
                List<TrainLine> list1 = this.GetRowLine(item);
                rad1.Items.Clear();
                int i = 0;
                if (list1 != null && list1.Count > 0)
                {
                    foreach (TrainLine m in list1)
                    {
                        int total = m.TotalMiles;
                        String KeyID = i + "";
                        String Text = "<span title='" + m.ToString() + "'>" + m.ToBigString() + " (" + total + "km)" + "</span>";
                        if (this.ChkShowFullLine.Checked)
                        {
                            Text = "<span title='" + m.ToString() + "'>" + m.ToString() + " (" + total + "km)" + "</span>";
                        }
                        ListItem item1 = new ListItem(Text, KeyID);
                        rad1.Items.Add(item1);
                        i++;
                    }
                }

                if (rad1.Items.Count > 0)
                {
                    rad1.SelectedIndex = 0;
                }
                else
                {
                    item.Visible = false;  //没有数据，则隐藏当前行
                }
            }
        }

        //设置方法（3）==设置车底数
        private void SetCds(RepeaterItem item)
        {
            ListControl cds = item.FindControl("cds") as ListControl;
            Label lab1 = item.FindControl("traintype") as Label;
            Label lab2 = item.FindControl("traintypeText") as Label;

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
                    cds.SelectedValue = "1.0";
                }
            }
            
        }

        //设置默认的车底数
        void SetDefaultCds(RepeaterItem item)
        {
            List<TrainLine> linelist1 = this.GetRowLine(item);
            Label lab1 = item.FindControl("traintype") as Label;
            Label lab2 = item.FindControl("traintypeText") as Label;
            if (linelist1 != null && linelist1.Count > 0)
            {
                TrainLine line0 = linelist1[0];
                int miles = line0.TotalMiles;

                ETrainType type1 = ETrainType.绿皮车25B;
                if (lab1.Text == "21")
                {
                    type1 = ETrainType.空调车25G;
                }
                else
                {
                    type1 = (ETrainType)(int.Parse(lab1.Text));
                }

                String defaultcds = Util.GetDefaultCds1(miles, type1) + "";
                ListControl cds = item.FindControl("cds") as ListControl;
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
        }

        //得到行的默认选择的线路
        private List<TrainLine> GetRowLine(RepeaterItem item)
        {
            List<TrainLine> list1 = null;
            Label lab1 = item.FindControl("traintype") as Label;
            if (lab1.Text == "0")                                                                            //绿皮车
            {
                list1 = ViewState["Line4"] as List<TrainLine>;
            }
            else if (lab1.Text == "3" || lab1.Text == "2" || lab1.Text == "21" || lab1.Text == "1")            //25T、25G和25K
            {
                list1 = ViewState["Line3"] as List<TrainLine>;
            }
            else if (lab1.Text == "4" || lab1.Text == "5" || lab1.Text == "9" || lab1.Text == "10")           //200公里高速
            {
                list1 = ViewState["Line2"] as List<TrainLine>;
            }
            else if (lab1.Text == "6" || lab1.Text == "7" || lab1.Text == "8"
                || lab1.Text == "11" || lab1.Text == "12")  //300公里高速
            {
                list1 = ViewState["Line1"] as List<TrainLine>;
            }
            return list1;
        }

        //计算收入和支出  原来的方法
        //private void CalShourAndZhiChu(RepeaterItem item)
        //{
        //    bool hasDianChe = false;                 //表示是否有发电车
        //    bool isYearFlag = false;                //表示是否是年分析的模式
        //    if (this.isYearPage.Value == "1")
        //    {
        //        isYearFlag = true;
        //    }

        //    Label lab1 = item.FindControl("traintype") as Label;
        //    ListControl cds = item.FindControl("cds") as ListControl;
        //    ListControl seltrain = item.FindControl("selTrainType") as ListControl;
        //    RadioButtonList rad1 = item.FindControl("selLine") as RadioButtonList;

        //    //得到车的类型
        //    int traintype1 = int.Parse(lab1.Text);
        //    if (traintype1 == 21||traintype1 ==0)  //25B总是非直供电（需要自带电车）
        //    {
        //       hasDianChe = true;
        //    }

        //    if (seltrain.Items.Count>0)
        //    {
        //        traintype1 = int.Parse(seltrain.SelectedValue);
        //    }
        //    ETrainType train1 = (ETrainType)(traintype1);


        //    //设置基本变量
        //    double shouru1 = 0.0;
        //    int yz1 = 0, yw1 = 0, rw1 = 0, ca1 = 0, sy1 = 0;
        //    int totalPeople = 0;

        //    List<ZhiChuData>[] arrZc = new List<ZhiChuData>[4];
        //    arrZc[0] = new List<ZhiChuData>();
        //    arrZc[1] = new List<ZhiChuData>();
        //    arrZc[2] = new List<ZhiChuData>();
        //    arrZc[3] = new List<ZhiChuData>();
        //    double JnFee = 0;

        //    int totalBianZhu = 18;
        //    if (bz.SelectedIndex == 0)   //表示是自动编组方式
        //    {
        //        totalBianZhu = int.Parse(this.bzs0.SelectedValue);
        //    }

        //    //编组的选项目
        //    if (this.bz.SelectedIndex == 1)
        //    {
        //        yz1 = int.Parse(yz.Text);
        //        yw1 = int.Parse(yw.Text);
        //        rw1 = int.Parse(rw.Text);
        //        ca1 = int.Parse(ca.Text);
        //        sy1 = int.Parse(sy.Text);
        //    }

        //    //计算数据
        //    List<TrainLine> linelist = this.GetRowLine(item);
        //    if (linelist.Count > 0)
        //    {
        //        TrainLine line0 = linelist[rad1.SelectedIndex];
        //        foreach (LineNode node1 in line0.Nodes)
        //        {
        //            node1.Fee1 = 0;
        //            node1.Fee2 = 0;
        //        }

        //        String findcond;
        //        if (Session["FindCond"]==null)
        //        {
        //            findcond = "";
        //        }
        //        else
        //        {
        //            findcond = Session["FindCond"].ToString().Trim();
        //        }
        //        //需要在这里传入客专路线的id值
        //        Line.GetLineShouruAndZhiChu(train1, line0,
        //           cds.SelectedValue, totalBianZhu,
        //           out shouru1, ref yz1, ref yw1, ref rw1, ref ca1, ref sy1,
        //           out totalPeople,
        //           out arrZc[0], out arrZc[1], out arrZc[2], out arrZc[3],
        //           out JnFee,
        //           hasDianChe, isYearFlag, cb.SelectedIndex, findcond);


        //        //列车的收入
        //        shouru1 = shouru1 / 10000;
        //        Label labShouRu = item.FindControl("shouru") as Label;
        //        if (isYearFlag == false)
        //        {
        //            labShouRu.Text = String.Format("{0:n2}", shouru1);
        //        }
        //        else
        //        {
        //            labShouRu.Text = String.Format("{0:n0}", shouru1);
        //        }

        //        //计算列车的运输总人数
        //        Label labTotalPeople = item.FindControl("totalpeople") as Label;
        //        labTotalPeople.Text = String.Format("{0}", totalPeople);

        //        //计算列车的总支出和合适的车厢编组



        //        //这里需要进行判断和计算 2014 04 17
        //        for (int i = 1; i <= 4; i++)
        //        {
        //            Label labZhiChu = item.FindControl("zhichu" + i) as Label;
        //            double sum1 = 0;
        //            String zc1 = String.Empty;
        //            foreach (ZhiChuData d1 in arrZc[i - 1])
        //            {
        //                double t1 = d1.ZhiChu;

        //                sum1 = sum1 + t1;
        //                if (zc1 == String.Empty)
        //                {
        //                    zc1 = d1.ZhiChu.ToString();
        //                }
        //                else
        //                {
        //                    zc1 = zc1 + "," + d1.ZhiChu.ToString();
        //                }
        //            }
        //            zc1 = zc1 + "," + JnFee;

        //            if (isYearFlag == false)
        //            {
        //                labZhiChu.Text = String.Format("{0:n2}", sum1);
        //            }
        //            else
        //            {
        //                labZhiChu.Text = String.Format("{0:n0}", sum1);
        //            }
        //            //labZhiChu.OnClientClick = "javascript:ShowZhiChu('" + zc1 + "');return false;";

        //            //设置收支盈亏
        //            Label labsz = item.FindControl("sz" + i) as Label;
        //            if (isYearFlag == false)
        //            {
        //                labsz.Text = String.Format("{0:n2}", shouru1 * SRate - sum1);
        //            }
        //            else
        //            {
        //                labsz.Text = String.Format("{0:n0}", shouru1 * SRate - sum1);
        //            }

        //            //计算上座率
        //            Label labSzr = item.FindControl("szr" + i) as Label;
        //            if (isYearFlag == false)
        //            {
        //                labSzr.Text = String.Format("{0:n2}%", 100 * sum1 / (shouru1 * SRate));
        //            }
        //            else
        //            {
        //                labSzr.Text = String.Format("{0:n0}%", 100 * sum1 / (shouru1 * SRate));
        //            }
        //        }


        //        //显示列车的合适编组
        //        HyperLink link2 = item.FindControl("link2") as HyperLink;
        //        HyperLink link3 = item.FindControl("link3") as HyperLink;
        //        if (link2 != null)
        //        {
        //            String type1 = traintype1+"";   //表示列车的类型
        //            String hasdianche = "0";        //默认电车的数量为0
        //            if (hasDianChe)
        //            { 
        //                hasdianche = "1"; 
        //            }

        //            if (int.Parse(type1) > 3 ) //表示是高铁类型
        //            {
        //                link2.Visible = false;
        //                link3.NavigateUrl = "SeeWordFile2.aspx?IsYear=" + this.isYearPage.Value + "&TrainType=" + type1 + "&Line=" + Server.UrlEncode(line0.ToString())
        //                                 + "&cds=" + cds.SelectedValue + "&hasDianChe=0&BianZhu=0,0,0,0,0";
        //            }
        //            else
        //            {
        //                link2.NavigateUrl = "BianZhuFenXi.aspx?IsYear=" + this.isYearPage.Value + "&TrainType=" + type1 + "&Line=" + Server.UrlEncode(line0.ToString())
        //                                            + "&cds=" + cds.SelectedValue + "&hasDianChe="+hasdianche+"&BianZhu=" + Server.UrlEncode(String.Format("{0},{1},{2},{3},{4}", yz1, yw1, rw1, ca1, sy1));

        //                link2.ToolTip = String.Format("最优的编组配置为：硬座{0}/ 硬卧{1}/ 软卧{2}/ 餐车{3} /宿营车{4}", yz1, yw1, rw1, ca1, sy1);
        //                link3.NavigateUrl = link2.NavigateUrl.Replace("BianZhuFenXi.aspx", "SeeWordFile.aspx");

        //                if (this.bz.SelectedIndex == 1)
        //                {
        //                    link2.NavigateUrl = "javascript:alert('提示：此功能不适合手动编组模式！');";
        //                    link2.ToolTip = String.Format("手动编组：硬座{0}/ 硬卧{1}/ 软卧{2}/ 餐车{3}/宿营车{4}", yz1, yw1, rw1, ca1, sy1);
        //                }
        //            }
        //        }
        //    }
        //}
        /// <summary>
        /// 2014 04 17 含客专路线时计算收入和支出
        /// </summary>
        /// <param name="item"></param>
        private void CalShourAndZhiChu(RepeaterItem item,List<string[]> lineInfos)
        {
            bool hasDianChe = false;                 //表示是否有发电车
            bool isYearFlag = false;                //表示是否是年分析的模式
            if (this.isYearPage.Value == "1")
            {
                isYearFlag = true;
            }

            Label lab1 = item.FindControl("traintype") as Label;
            ListControl cds = item.FindControl("cds") as ListControl;
            ListControl seltrain = item.FindControl("selTrainType") as ListControl;
            RadioButtonList rad1 = item.FindControl("selLine") as RadioButtonList;

            //得到车的类型
            int traintype1 = int.Parse(lab1.Text);
            if (traintype1 == 21 || traintype1 == 0)  //25B总是非直供电（需要自带电车）
            {
                hasDianChe = true;
            }

            if (seltrain.Items.Count > 0)
            {
                traintype1 = int.Parse(seltrain.SelectedValue);
            }
            ETrainType train1 = (ETrainType)(traintype1);


            //设置基本变量
            double shouru1 = 0.0;
            int yz1 = 0, yw1 = 0, rw1 = 0, ca1 = 0, sy1 = 0;
            int totalPeople = 0;

            List<ZhiChuData>[] arrZc = new List<ZhiChuData>[4];
            arrZc[0] = new List<ZhiChuData>();
            arrZc[1] = new List<ZhiChuData>();
            arrZc[2] = new List<ZhiChuData>();
            arrZc[3] = new List<ZhiChuData>();
            double JnFee = 0;

            int totalBianZhu = 18;
            if (bz.SelectedIndex == 0)   //表示是自动编组方式
            {
                totalBianZhu = int.Parse(this.bzs0.SelectedValue);
            }

            //编组的选项目
            if (this.bz.SelectedIndex == 1)
            {
                yz1 = int.Parse(yz.Text);
                yw1 = int.Parse(yw.Text);
                rw1 = int.Parse(rw.Text);
                ca1 = int.Parse(ca.Text);
                sy1 = int.Parse(sy.Text);
            }

            //计算数据
            List<TrainLine> linelist = this.GetRowLine(item);
            if (linelist != null)
            {
                if (linelist.Count > 0)
                {
                    TrainLine line0 = linelist[rad1.SelectedIndex];
                    foreach (LineNode node1 in line0.Nodes)
                    {
                        node1.Fee1 = 0;
                        node1.Fee2 = 0;
                    }

                    String findcond;
                    if (Session["FindCond"] == null)
                    {
                        findcond = "";
                    }
                    else
                    {
                        findcond = Session["FindCond"].ToString().Trim();
                    }
                    //需要在这里传入客专路线的id值
                    Line.GetLineShouruAndZhiChu(train1, line0,
                       cds.SelectedValue, totalBianZhu,
                       out shouru1, ref yz1, ref yw1, ref rw1, ref ca1, ref sy1,
                       out totalPeople,
                       out arrZc[0], out arrZc[1], out arrZc[2], out arrZc[3],
                       out JnFee,
                       hasDianChe, isYearFlag, cb.SelectedIndex, findcond, lineInfos);


                    //列车的收入
                    shouru1 = shouru1 / 10000;
                    Label labShouRu = item.FindControl("shouru") as Label;
                    if (isYearFlag == false)
                    {
                        labShouRu.Text = String.Format("{0:n2}", shouru1);
                    }
                    else
                    {
                        labShouRu.Text = String.Format("{0:n0}", shouru1);
                    }

                    //计算列车的运输总人数
                    Label labTotalPeople = item.FindControl("totalpeople") as Label;
                    labTotalPeople.Text = String.Format("{0}", totalPeople);

                    //计算列车的总支出和合适的车厢编组



                    //这里需要进行判断和计算 2014 04 17
                    for (int i = 1; i <= 4; i++)
                    {
                        Label labZhiChu = item.FindControl("zhichu" + i) as Label;
                        double sum1 = 0;
                        String zc1 = String.Empty;
                        foreach (ZhiChuData d1 in arrZc[i - 1])
                        {
                            double t1 = d1.ZhiChu;

                            sum1 = sum1 + t1;
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

                        if (isYearFlag == false)
                        {
                            labZhiChu.Text = String.Format("{0:n2}", sum1);
                        }
                        else
                        {
                            labZhiChu.Text = String.Format("{0:n0}", sum1);
                        }
                        //labZhiChu.OnClientClick = "javascript:ShowZhiChu('" + zc1 + "');return false;";

                        //设置收支盈亏
                        Label labsz = item.FindControl("sz" + i) as Label;
                        if (isYearFlag == false)
                        {
                            labsz.Text = String.Format("{0:n2}", shouru1 * SRate - sum1);
                        }
                        else
                        {
                            labsz.Text = String.Format("{0:n0}", shouru1 * SRate - sum1);
                        }

                        //计算上座率
                        Label labSzr = item.FindControl("szr" + i) as Label;
                        if (isYearFlag == false)
                        {
                            labSzr.Text = String.Format("{0:n2}%", 100 * sum1 / (shouru1 * SRate));
                        }
                        else
                        {
                            labSzr.Text = String.Format("{0:n0}%", 100 * sum1 / (shouru1 * SRate));
                        }
                    }


                    //显示列车的合适编组
                    HyperLink link2 = item.FindControl("link2") as HyperLink;
                    HyperLink link3 = item.FindControl("link3") as HyperLink;
                    if (link2 != null)
                    {
                        String type1 = traintype1 + "";   //表示列车的类型
                        String hasdianche = "0";        //默认电车的数量为0
                        if (hasDianChe)
                        {
                            hasdianche = "1";
                        }

                        if (int.Parse(type1) > 3) //表示是高铁类型
                        {
                            link2.Visible = false;
                            link3.NavigateUrl = "SeeWordFile2.aspx?IsYear=" + this.isYearPage.Value + "&TrainType=" + type1 + "&Line=" + Server.UrlEncode(line0.ToString())
                                             + "&cds=" + cds.SelectedValue + "&hasDianChe=0&BianZhu=0,0,0,0,0";
                        }
                        else
                        {
                            link2.NavigateUrl = "BianZhuFenXi.aspx?IsYear=" + this.isYearPage.Value + "&TrainType=" + type1 + "&Line=" + Server.UrlEncode(line0.ToString())
                                                        + "&cds=" + cds.SelectedValue + "&hasDianChe=" + hasdianche + "&BianZhu=" + Server.UrlEncode(String.Format("{0},{1},{2},{3},{4}", yz1, yw1, rw1, ca1, sy1));

                            link2.ToolTip = String.Format("最优的编组配置为：硬座{0}/ 硬卧{1}/ 软卧{2}/ 餐车{3} /宿营车{4}", yz1, yw1, rw1, ca1, sy1);
                            link3.NavigateUrl = link2.NavigateUrl.Replace("BianZhuFenXi.aspx", "SeeWordFile.aspx");

                            if (this.bz.SelectedIndex == 1)
                            {
                                link2.NavigateUrl = "javascript:alert('提示：此功能不适合手动编组模式！');";
                                link2.ToolTip = String.Format("手动编组：硬座{0}/ 硬卧{1}/ 软卧{2}/ 餐车{3}/宿营车{4}", yz1, yw1, rw1, ca1, sy1);
                            }
                        }
                    }
                }
            }
            else
            {
                if (bz.SelectedItem.Value.ToString() == "3")
                {
                    WebFrame.Util.JAjax.Alert("错误：系统中不存在该高铁线！");
                }
                else
                {
                    WebFrame.Util.JAjax.Alert("错误：系统中不存在该" + bz.SelectedItem.Text.ToString() + "！");
                }
            }
        }
        #endregion

        #region 选择条件的改变（Repeater控件外的改变）
        //设置编组方式的改变
        void bz_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index1 = bz.SelectedIndex;
            if (index1 == 3)
            {
                index1 = 2;
                onlygg.Checked = true;
            }
            else
            {
                onlygg.Checked = false;
            }


            if (index1 == 0)   //表示选中的是自动编组
            {
                bzs0.Visible = true;
                bzs1.Visible = false;
            }
            else if (index1 == 1) //表示选中的是手动编组
            {
                bzs0.Visible = false;
                bzs1.Visible = true;
            }
            else if (index1 == 2)  //表示选中的是动车组
            {
                bzs0.Visible = false;
                bzs1.Visible = false;
            }

            if (ViewState["Line1"] != null)
            {
                this.SearchLine();
            }
        }

        //设置不同成本的方式选择改变
        void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ViewState["Line1"] != null)
            {
                this.SearchLine();
            }
        }

        //设置自动编组编组数的变化
        void bzs0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ViewState["Line1"] != null)
            {
                this.SearchLine();
            }
        }

        //设置手动编组编组数的变化
        void yw_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int cat = int.Parse(this.ca.Text);
                if (cat > 1)
                {
                    JAjax.Alert("错误：餐车的数值只能为0或1");
                }
                else
                {
                    int syc = int.Parse(this.sy.Text);
                    if (syc > 1)
                    {
                        JAjax.Alert("错误：宿营车的数值只能为0或1");
                    }
                    else
                    {
                        int t1 = int.Parse(this.yz.Text) + int.Parse(this.yw.Text) + int.Parse(this.rw.Text)
                            + int.Parse(this.ca.Text) + int.Parse(this.sy.Text);

                        if (t1 >= 10 && t1 <= 21)
                        {
                            if (ViewState["Line1"] != null)
                            {
                                this.SearchLine();
                            }
                        }
                        else
                        {
                            JAjax.Alert("错误：列车编组数值的和必须在【10-21】之间！");
                        }
                    }
                }
            }
            catch (Exception err)
            {
                JAjax.Alert("错误：请输入数值，0不能用空代替！");
            }
        }


        //改变实际的上座率
        public void sjszreTextChange1(object sender,EventArgs e)
        {
            TextBox t1 = sender as TextBox;
            if (t1 != null)
            {
                //访问收入的值
                Label lab1 = t1.Parent.FindControl("shouru") as Label;
                Label lab2 = t1.Parent.FindControl("zhichu1") as Label;
                Label lab3 = t1.Parent.FindControl("sjyk1") as Label;
                double d = (Convert.ToDouble(t1.Text.ToString().Trim())/100.0) * Convert.ToDouble(lab1.Text.ToString().Trim()) * SRate - Convert.ToDouble(lab2.Text.ToString().Trim());

                lab3.Text = String.Format("{0:n2}", d);
            }
        }

        public void sjszreTextChange2(object sender, EventArgs e)
        {
            TextBox t1 = sender as TextBox;
            if (t1 != null)
            {
                //访问收入的值
                Label lab1 = t1.Parent.FindControl("shouru") as Label;
                Label lab2 = t1.Parent.FindControl("zhichu2") as Label;
                Label lab3 = t1.Parent.FindControl("sjyk2") as Label;
                double d = (Convert.ToDouble(t1.Text.ToString().Trim()) / 100.0) * Convert.ToDouble(lab1.Text.ToString().Trim()) * SRate - Convert.ToDouble(lab2.Text.ToString().Trim());

                lab3.Text = String.Format("{0:n2}", d);
            }
        }

        public void sjszreTextChange3(object sender, EventArgs e)
        {
            TextBox t1 = sender as TextBox;
            if (t1 != null)
            {
                //访问收入的值
                Label lab1 = t1.Parent.FindControl("shouru") as Label;
                Label lab2 = t1.Parent.FindControl("zhichu3") as Label;
                Label lab3 = t1.Parent.FindControl("sjyk3") as Label;
                double d = (Convert.ToDouble(t1.Text.ToString().Trim()) / 100.0) * Convert.ToDouble(lab1.Text.ToString().Trim()) * SRate - Convert.ToDouble(lab2.Text.ToString().Trim());

                lab3.Text = String.Format("{0:n2}", d);
            }
        }

        public void sjszreTextChange4(object sender, EventArgs e)
        {
            TextBox t1 = sender as TextBox;
            if (t1 != null)
            {
                //访问收入的值
                Label lab1 = t1.Parent.FindControl("shouru") as Label;
                Label lab2 = t1.Parent.FindControl("zhichu4") as Label;
                Label lab3 = t1.Parent.FindControl("sjyk4") as Label;
                double d = (Convert.ToDouble(t1.Text.ToString().Trim()) / 100.0) * Convert.ToDouble(lab1.Text.ToString().Trim()) * SRate - Convert.ToDouble(lab2.Text.ToString().Trim());

                lab3.Text = String.Format("{0:n2}", d);
            }
        }
        #endregion

        #region 数据改变的事件（Repeater 控件内的改变）
        //车型的改变 （含客专路线 2014 04 17）
        protected void RepeaterData_SelectedIndexChanged(object sender, EventArgs e)
        {
            Control con1 = sender as Control;
            RepeaterItem item1 = con1.Parent as RepeaterItem;
            //找到选中的路线
            ListControl rad1 = item1.FindControl("selLine") as ListControl;
            List<SearchField> condition = new List<SearchField>();
            JTable tab1 = new JTable("LINESTATION");
            JTable tab2= new JTable("trainline");
            List<string[]> lineInfos = new List<string[]>();
            if (rad1 != null)
            {
                if (rad1.Items.Count > 0)
                {
                    int i=rad1.SelectedIndex;
                    //获得所在的linestation 的主键id  根据线路具体信息
                    List<TrainLine> list1 = this.GetRowLine(item1);
                    string lineRemark=list1[i].ToString();//线路完整长度
                    if (!string.IsNullOrEmpty(lineRemark))
                    {
                        string[] linepoint = lineRemark.Split('-');
                        for(int j=0;j<linepoint.Length-1;j++)
                        {
                            condition.Clear();
                            condition.Add(new SearchField("astation",linepoint[j]));
                            condition.Add(new SearchField("bstation", linepoint[j+1]));
                            string[] lineinfo=new string[3];
                            lineinfo[0]=tab1.SearchScalar(condition, "KZID").ToString();//获得客专id编号
                            lineinfo[1] = tab1.SearchScalar(condition, "miles").ToString();//获得里程
                            string lineID= tab1.SearchScalar(condition,"lineid").ToString();
                            condition.Clear();
                            condition.Add(new SearchField("lineid",lineID));
                            lineinfo[2] = tab2.SearchScalar(condition, "linetype") == null ? "" : tab2.SearchScalar(condition, "linetype").ToString();//获得线路类型
                            lineInfos.Add(lineinfo);

                        }

                    }
                    
                    
                }
               
            }

            this.CalShourAndZhiChu(item1,lineInfos);
        }

        //车型的改变 原来的方法
        //protected void RepeaterData_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Control con1 = sender as Control;
        //    RepeaterItem item1 = con1.Parent as RepeaterItem;
        //    this.CalShourAndZhiChu(item1);
        //}
        #endregion

        #region 暂时不用的代码
        //// 计算收入和支出
        //private void NewCalShourAndZhiChu(TrainLine line0, RepeaterItem item)
        //{
        //    bool hasDianChe = false;                 //表示是否有发电车
        //    bool isYearFlag = false;                //表示是否是年分析的模式
        //    if (this.isYearPage.Value == "1")
        //    {
        //        isYearFlag = true;
        //    }

        //    Label lab1 = item.FindControl("traintype") as Label;
        //    ListControl cds = item.FindControl("cds") as ListControl;

        //    int traintype1 = int.Parse(lab1.Text);
        //    if (traintype1 == 21)
        //    {
        //        traintype1 = 2;
        //        hasDianChe = true;
        //    }

        //    //25B和25K有电车（金寿吉新增）
        //    if (traintype1 == 0 || traintype1 == 1)
        //    {
        //        hasDianChe = true;
        //    }

        //    ETrainType train1 = (ETrainType)(traintype1);
        //    double shouru1 = 0.0;
        //    int yz1 = 0, yw1 = 0, rw1 = 0, ca1 = 0, sy1 = 0;
        //    int totalPeople = 0;

        //    List<ZhiChuData>[] arrZc = new List<ZhiChuData>[4];
        //    arrZc[0] = new List<ZhiChuData>();
        //    arrZc[1] = new List<ZhiChuData>();
        //    arrZc[2] = new List<ZhiChuData>();
        //    arrZc[3] = new List<ZhiChuData>();
        //    double JnFee = 0;


        //    int totalBianZhu = 18;
        //    if (bz.SelectedIndex == 0)   //表示是自动编组方式
        //    {
        //        totalBianZhu = int.Parse(this.bzs0.SelectedValue);
        //    }

        //    //编组的选项目
        //    if (this.bz.SelectedIndex == 1)
        //    {
        //        yz1 = int.Parse(yz.Text);
        //        yw1 = int.Parse(yw.Text);
        //        rw1 = int.Parse(rw.Text);
        //        ca1 = int.Parse(ca.Text);
        //        sy1 = int.Parse(sy.Text);
        //    }

        //    String findcond;
        //    if (Session["FindCond"] == null)
        //    {
        //        findcond = "";
        //    }
        //    else
        //    {
        //        findcond = Session["FindCond"].ToString().Trim();
        //    }

        //    Line.GetLineShouruAndZhiChu(train1, line0,
        //        cds.SelectedValue, totalBianZhu,
        //        out shouru1, ref yz1, ref yw1, ref rw1, ref ca1, ref sy1,
        //        out totalPeople,
        //        out arrZc[0], out arrZc[1], out arrZc[2], out arrZc[3],
        //        out JnFee,
        //        hasDianChe, isYearFlag, cb.SelectedIndex, findcond);

        //    //列车的收入
        //    shouru1 = shouru1 / 10000;
        //    Label labShouRu = item.FindControl("shouru") as Label;
        //    if (isYearFlag == false)
        //    {
        //        labShouRu.Text = String.Format("{0:n2}", shouru1);
        //        //labShouRu.Text = shouru1 + "";
        //    }
        //    else
        //    {
        //        labShouRu.Text = String.Format("{0:n0}", shouru1);
        //    }

        //    //计算列车的运输总人数
        //    Label labTotalPeople = item.FindControl("totalpeople") as Label;
        //    labTotalPeople.Text = String.Format("{0}", totalPeople);

        //    //计算列车的总支出和合适的车厢编组
        //    for (int i = 1; i <= 4; i++)
        //    {
        //        Label labZhiChu = item.FindControl("zhichu" + i) as Label;
        //        double sum1 = 0;
        //        String zc1 = String.Empty;
        //        foreach (ZhiChuData d1 in arrZc[i - 1])
        //        {
        //            double t1 = d1.ZhiChu;

        //            sum1 = sum1 + t1;
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

        //        if (isYearFlag == false)
        //        {
        //            labZhiChu.Text = String.Format("{0:n2}", sum1);
        //        }
        //        else
        //        {
        //            labZhiChu.Text = String.Format("{0:n0}", sum1);
        //        }
        //        //labZhiChu.OnClientClick = "javascript:ShowZhiChu('" + zc1 + "');return false;";

        //        //设置收支盈亏
        //        Label labsz = item.FindControl("sz" + i) as Label;
        //        if (isYearFlag == false)
        //        {
        //            labsz.Text = String.Format("{0:n2}", shouru1 * SRate - sum1);
        //        }
        //        else
        //        {
        //            labsz.Text = String.Format("{0:n0}", shouru1 * SRate - sum1);
        //        }

        //        //计算上座率
        //        Label labSzr = item.FindControl("szr" + i) as Label;
        //        if (isYearFlag == false)
        //        {
        //            labSzr.Text = String.Format("{0:n2}%", 100 * sum1 / (shouru1 * SRate));
        //        }
        //        else
        //        {
        //            labSzr.Text = String.Format("{0:n0}%", 100 * sum1 / (shouru1 * SRate));
        //        }
        //    }

        //    //显示列车的合适编组
        //    HyperLink link2 = item.FindControl("link2") as HyperLink;
        //    HyperLink link3 = item.FindControl("link3") as HyperLink;
        //    if (link2 != null)
        //    {
        //        if (int.Parse(lab1.Text) > 3 && lab1.Text != "21")
        //        {
        //            link2.Visible = false;
        //            link3.NavigateUrl = "SeeWordFile2.aspx?IsYear=" + this.isYearPage.Value + "&TrainType=" + lab1.Text + "&Line=" + Server.UrlEncode(line0.ToString())
        //                             + "&cds=" + cds.SelectedValue + "&hasDianChe=0&BianZhu=0,0,0,0,0";
        //        }
        //        else
        //        {
        //            if (lab1.Text != "21")
        //            {
        //                if (lab1.Text == "0" || lab1.Text == "1")  //表示 25B和25K
        //                {
        //                    link2.NavigateUrl = "BianZhuFenXi.aspx?IsYear=" + this.isYearPage.Value + "&TrainType=" + lab1.Text + "&Line=" + Server.UrlEncode(line0.ToString())
        //                                        + "&cds=" + cds.SelectedValue + "&hasDianChe=1&BianZhu=" + Server.UrlEncode(String.Format("{0},{1},{2},{3},{4}", yz1, yw1, rw1, ca1, sy1));
        //                }
        //                else
        //                {
        //                    link2.NavigateUrl = "BianZhuFenXi.aspx?IsYear=" + this.isYearPage.Value + "&TrainType=" + lab1.Text + "&Line=" + Server.UrlEncode(line0.ToString())
        //                                        + "&cds=" + cds.SelectedValue + "&hasDianChe=0&BianZhu=" + Server.UrlEncode(String.Format("{0},{1},{2},{3},{4}", yz1, yw1, rw1, ca1, sy1));
        //                }
        //            }
        //            else   //表示非直供电25G
        //            {
        //                link2.NavigateUrl = "BianZhuFenXi.aspx?IsYear=" + this.isYearPage.Value + "&TrainType=2&Line=" + Server.UrlEncode(line0.ToString())
        //                                   + "&cds=" + cds.SelectedValue + "&hasDianChe=1&BianZhu=" + Server.UrlEncode(String.Format("{0},{1},{2},{3},{4}", yz1, yw1, rw1, ca1, sy1));
        //            }
        //            link2.ToolTip = String.Format("最优的编组配置为：硬座{0}/ 硬卧{1}/ 软卧{2}/ 餐车{3} /宿营车{4}", yz1, yw1, rw1, ca1, sy1);
        //            link3.NavigateUrl = link2.NavigateUrl.Replace("BianZhuFenXi.aspx", "SeeWordFile.aspx");

        //            if (this.bz.SelectedIndex == 1)
        //            {
        //                link2.NavigateUrl = "javascript:alert('提示：此功能不适合手动编组模式！');";
        //                link2.ToolTip = String.Format("手动编组：硬座{0}/ 硬卧{1}/ 软卧{2}/ 餐车{3}/宿营车{4}", yz1, yw1, rw1, ca1, sy1);
        //            }
        //        }
        //    }

        //    Label bianzhu1 = item.FindControl("bianzhu") as Label;
        //    if (bianzhu1 != null)
        //    {
        //        bianzhu1.Text = String.Format("{0}-{1}-{2}-{3}-{4}", yz1, yw1, rw1, ca1, sy1);
        //    }
        //}
        #endregion
    }
}
