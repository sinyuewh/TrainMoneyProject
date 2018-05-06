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

namespace WebSite.TrainWeb.Fenxi
{
    public partial class NewCheCiFenxi : System.Web.UI.Page
    {
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
        protected void Page_Load(object sender, EventArgs e)
        {
            con1=new Control[]{AStation,BStation,middleStation,trainList,sd};
            if (!Page.IsPostBack)
            {
                //this.mulLine.Attributes["ReadOnly"] = "true";
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

        protected override void OnInit(EventArgs e)
        {
            this.Button1.Click += new EventHandler(Button1_Click);
            this.bz.SelectedIndexChanged += new EventHandler(bz_SelectedIndexChanged);
            this.cb.SelectedIndexChanged += new EventHandler(cb_SelectedIndexChanged);
            this.bzs0.SelectedIndexChanged += new EventHandler(bzs0_SelectedIndexChanged);

            this.yw.TextChanged += new EventHandler(yw_TextChanged);
            this.yz.TextChanged+=new EventHandler(yw_TextChanged);
            this.rw.TextChanged+=new EventHandler(yw_TextChanged);
            this.ca.TextChanged+=new EventHandler(yw_TextChanged);
            this.sy.TextChanged+=new EventHandler(yw_TextChanged);
            base.OnInit(e);
        }

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
                            + int.Parse(this.ca.Text)+int.Parse(this.sy.Text);

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

        void bzs0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ViewState["Line1"] != null)
            {
                this.SearchLine();
            }
        }

        void Button1_Click(object sender, EventArgs e)
        {
            this.SearchLine();
        }

        void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ViewState["Line1"] != null)
            {
                this.SearchLine();
            }
        }

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


            if (index1 == 0 || index1 ==2)
            {
                bzs0.Visible = true;
                bzs1.Visible = false;

                if (index1 == 2)
                {
                    bzs0.Visible = false;
                }
                else
                {
                    bzs0.Visible = true;
                }
            }
            else
            {
                bzs0.Visible = false;
                bzs1.Visible = true;
            }

            if (ViewState["Line1"] != null)
            {
                this.SearchLine();
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

                    /*
                    //以前的版本（25K和25B的条件相同）      
                    if (lab1.Text == "0" || lab1.Text == "1" 
                        || lab1.Text == "2" || lab1.Text=="21" )       //普通车
                    {
                        list1 = ViewState["Line4"] as List<TrainLine>;
                    }
                    else if (lab1.Text == "3")                                            //直达车
                    {
                        list1 = ViewState["Line3"] as List<TrainLine>;
                    }
                    else if (lab1.Text == "4" || lab1.Text == "5" || lab1.Text == "9" || lab1.Text == "10")                      //200公里高速
                    {
                        list1 = ViewState["Line2"] as List<TrainLine>;
                    }
                    else if (lab1.Text == "6" || lab1.Text == "7" || lab1.Text == "8" 
                        || lab1.Text =="11" || lab1.Text =="12" )  //300公里高速
                    {
                        list1 = ViewState["Line1"] as List<TrainLine>;
                    }
                    */

                    if (lab1.Text == "0" || lab1.Text == "21")                               //普通车
                    {
                        list1 = ViewState["Line4"] as List<TrainLine>;
                    }
                    else if (lab1.Text == "3" || lab1.Text == "2" || lab1.Text == "1")           //直达车
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

                    /*
                    //重新调整了线路是选择条件
                    if (lab1.Text == "0" || lab1.Text == "1" || lab1.Text == "2"
                        || lab1.Text == "21")       //普通车
                    {
                        list1 = ViewState["Line4"] as List<TrainLine>;
                    }
                    else if (lab1.Text == "3")                                            //直达车
                    {
                        list1 = ViewState["Line3"] as List<TrainLine>;
                    }
                    else if (lab1.Text == "4" || lab1.Text == "5" || lab1.Text == "9" || lab1.Text == "10")                      //200公里高速
                    {
                        list1 = ViewState["Line2"] as List<TrainLine>;
                    }
                    else if (lab1.Text == "6" || lab1.Text == "7" || lab1.Text == "8"
                        || lab1.Text =="11" || lab1.Text =="12" )  //300公里高速
                    {
                        list1 = ViewState["Line1"] as List<TrainLine>;
                    }
                    */

                    if (lab1.Text == "0" || lab1.Text == "21")                               //普通车
                    {
                        list1 = ViewState["Line4"] as List<TrainLine>;
                    }
                    else if (lab1.Text == "3" || lab1.Text == "2" || lab1.Text == "1")           //直达车
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


                    TrainLine line0 = list1[index1];
                    this.NewCalShourAndZhiChu(line0, item1);
                }
            }
        }

        protected void selTrainType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListControl obj1 = sender as ListControl;
            if (obj1 != null)
            {
                RepeaterItem item1 = obj1.Parent as RepeaterItem;
                if (item1 != null)
                {
                    Label lab1 = item1.FindControl("traintype") as Label;
                    Label lab2 = item1.FindControl("traintypeText") as Label;
                    if (lab1.Text != null)
                    {
                        lab1.Text = obj1.SelectedValue;
                        lab2.Text = obj1.SelectedItem.Text;
                    }

                    ListControl selline = item1.FindControl("selLine") as ListControl;
                    int index1 = selline.SelectedIndex;

                    List<TrainLine> list1 = null;

                    /*
                     //重新调整线路的限制
                    if (lab1.Text == "0" || lab1.Text == "1" || lab1.Text == "2"
                        || lab1.Text == "21")       //普通车
                    {
                        list1 = ViewState["Line4"] as List<TrainLine>;
                    }
                    else if (lab1.Text == "3")                                            //直达车
                    {
                        list1 = ViewState["Line3"] as List<TrainLine>;
                    }
                    else if (lab1.Text == "4" || lab1.Text == "5" || lab1.Text == "9" || lab1.Text == "10")                      //200公里高速
                    {
                        list1 = ViewState["Line2"] as List<TrainLine>;
                    }
                    else if (lab1.Text == "6" || lab1.Text == "7" || lab1.Text == "8"
                        || lab1.Text =="11" || lab1.Text =="12" )  //300公里高速
                    {
                        list1 = ViewState["Line1"] as List<TrainLine>;
                    }
                    */

                    if (lab1.Text == "0" || lab1.Text == "21")                               //普通车
                    {
                        list1 = ViewState["Line4"] as List<TrainLine>;
                    }
                    else if (lab1.Text == "3" || lab1.Text == "2" || lab1.Text == "1")           //直达车
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


                    TrainLine line0 = list1[index1];
                    this.NewCalShourAndZhiChu(line0, item1);
                }
            }
        }

        #region Privae 方法
        // 绑定Repeater的数据
        private void NewBindComplete()
        {
            foreach (RepeaterItem item in this.Repeater2.Items)
            {
                Label lab1 = item.FindControl("traintype") as Label;
                Label lab2 = item.FindControl("traintypeText") as Label;

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

                            /*
                            for (int i = 1; i <= 2; i++)
                            {
                                ListItem list1 = new ListItem(i + "", i + "");
                                cds.Items.Add(list1);
                            }*/
                            cds.SelectedValue = "1.0";
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
                        /*
                        //重新调整车型的条件
                        if (lab1.Text == "0" || lab1.Text == "1"
                            || lab1.Text == "21")                               //普通车
                        {
                            list1 = ViewState["Line4"] as List<TrainLine>;
                        }
                        else if (lab1.Text == "3" || lab1.Text == "2")           //直达车
                        {
                            list1 = ViewState["Line3"] as List<TrainLine>;
                        }
                        else if (lab1.Text == "4" || lab1.Text == "5" || lab1.Text == "9" || lab1.Text == "10")           //200公里高速
                        {
                            list1 = ViewState["Line2"] as List<TrainLine>;
                        }
                        else if (lab1.Text == "6" || lab1.Text == "7" || lab1.Text == "8"
                            || lab1.Text =="11" || lab1.Text =="12")  //300公里高速
                        {
                            list1 = ViewState["Line1"] as List<TrainLine>;
                        }*/

                        if (lab1.Text == "0" || lab1.Text == "21")                               //普通车
                        {
                            list1 = ViewState["Line4"] as List<TrainLine>;
                        }
                        else if (lab1.Text == "3" || lab1.Text == "2" || lab1.Text == "1")           //直达车
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

                        //调整车底数的默认值
                        if (list1 != null && list1.Count > 0)
                        {
                            TrainLine line0 = list1[0];
                            int miles = line0.TotalMiles;
                            // String defaultcds = Util.GetDefaultCds(miles, type1)+"";
                            String defaultcds = Util.GetDefaultCds1(miles, type1) + "";

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

                        //设置不同的下拉框
                        DropDownList drop1 = item.FindControl("selTrainType") as DropDownList;
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
                            else
                            {
                                drop1.Visible = false;
                            }

                            if (drop1.Items.FindByValue(lab1.Text) != null)
                            {
                                drop1.SelectedValue = lab1.Text;
                            }
                        }
                    }

                }
            }
        }
        // 计算收入和支出  20140417
        private void NewCalShourAndZhiChu(TrainLine line0, RepeaterItem item)
        {
            bool hasDianChe = false;                 //表示是否有发电车
            bool isYearFlag = false;                //表示是否是年分析的模式
            if (this.isYearPage.Value == "1")
            {
                isYearFlag = true;
            }

            Label lab1 = item.FindControl("traintype") as Label;
            ListControl cds = item.FindControl("cds") as ListControl;

            int traintype1 = int.Parse(lab1.Text);
            if (traintype1 == 21)
            {
                traintype1 = 2;
                hasDianChe = true;
            }

            //25B和25K有电车（金寿吉新增）
            if (traintype1 == 0 || traintype1 == 1)
            {
                hasDianChe = true;
            }

            ETrainType train1 = (ETrainType)(traintype1);
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

            Line.GetLineShouruAndZhiChu(train1, line0,
                cds.SelectedValue, totalBianZhu,
                out shouru1, ref yz1, ref yw1, ref rw1, ref ca1, ref sy1,
                out totalPeople,
                out arrZc[0], out arrZc[1], out arrZc[2], out arrZc[3],
                out JnFee,
                hasDianChe, isYearFlag, cb.SelectedIndex,"");

            //列车的收入
            shouru1 = shouru1 / 10000;
            Label labShouRu = item.FindControl("shouru") as Label;
            if (isYearFlag == false)
            {
                labShouRu.Text = String.Format("{0:n2}", shouru1);
                //labShouRu.Text = shouru1 + "";
            }
            else
            {
                labShouRu.Text = String.Format("{0:n0}", shouru1);
            }

            //计算列车的运输总人数
            Label labTotalPeople = item.FindControl("totalpeople") as Label;
            labTotalPeople.Text = String.Format("{0}", totalPeople);

            //计算列车的总支出和合适的车厢编组
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
                if (int.Parse(lab1.Text) > 3 && lab1.Text != "21")
                {
                    link2.Visible = false;
                    link3.NavigateUrl = "SeeWordFile2.aspx?IsYear=" + this.isYearPage.Value + "&TrainType=" + lab1.Text + "&Line=" + Server.UrlEncode(line0.ToString())
                                     + "&cds=" + cds.SelectedValue + "&hasDianChe=0&BianZhu=0,0,0,0,0";
                }
                else
                {
                    if (lab1.Text != "21")
                    {
                        if (lab1.Text == "0" || lab1.Text == "1")  //表示 25B和25K
                        {
                            link2.NavigateUrl = "BianZhuFenXi.aspx?IsYear=" + this.isYearPage.Value + "&TrainType=" + lab1.Text + "&Line=" + Server.UrlEncode(line0.ToString())
                                                + "&cds=" + cds.SelectedValue + "&hasDianChe=1&BianZhu=" + Server.UrlEncode(String.Format("{0},{1},{2},{3},{4}", yz1, yw1, rw1, ca1, sy1));
                        }
                        else
                        {
                            link2.NavigateUrl = "BianZhuFenXi.aspx?IsYear=" + this.isYearPage.Value + "&TrainType=" + lab1.Text + "&Line=" + Server.UrlEncode(line0.ToString())
                                                + "&cds=" + cds.SelectedValue + "&hasDianChe=0&BianZhu=" + Server.UrlEncode(String.Format("{0},{1},{2},{3},{4}", yz1, yw1, rw1, ca1, sy1));
                        }
                    }
                    else   //表示非直供电25G
                    {
                        link2.NavigateUrl = "BianZhuFenXi.aspx?IsYear=" + this.isYearPage.Value + "&TrainType=2&Line=" + Server.UrlEncode(line0.ToString())
                                           + "&cds=" + cds.SelectedValue + "&hasDianChe=1&BianZhu=" + Server.UrlEncode(String.Format("{0},{1},{2},{3},{4}", yz1, yw1, rw1, ca1, sy1));
                    }
                    link2.ToolTip = String.Format("最优的编组配置为：硬座{0}/ 硬卧{1}/ 软卧{2}/ 餐车{3} /宿营车{4}", yz1, yw1, rw1, ca1, sy1);
                    link3.NavigateUrl = link2.NavigateUrl.Replace("BianZhuFenXi.aspx", "SeeWordFile.aspx");

                    if (this.bz.SelectedIndex == 1)
                    {
                        link2.NavigateUrl = "javascript:alert('提示：此功能不适合手动编组模式！');";
                        link2.ToolTip = String.Format("手动编组：硬座{0}/ 硬卧{1}/ 软卧{2}/ 餐车{3}/宿营车{4}", yz1, yw1, rw1, ca1, sy1);
                    }
                }
            }


            Label bianzhu1 = item.FindControl("bianzhu") as Label;
            if (bianzhu1 != null)
            {
                bianzhu1.Text = String.Format("{0}-{1}-{2}-{3}-{4}", yz1, yw1, rw1, ca1, sy1);
            }
        }
        //// 计算收入和支出  原代码
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
        //    int yz1 = 0, yw1 = 0, rw1 = 0,ca1=0,sy1=0;
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

        //    Line.GetLineShouruAndZhiChu(train1, line0, 
        //        cds.SelectedValue,totalBianZhu,
        //        out shouru1,ref yz1,ref yw1,ref rw1,ref ca1,ref sy1,
        //        out totalPeople,
        //        out arrZc[0],out arrZc[1],out arrZc[2],out arrZc[3],
        //        out JnFee,
        //        hasDianChe,isYearFlag,cb.SelectedIndex,"");           

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
        //        Label labZhiChu = item.FindControl("zhichu"+i) as Label;
        //        double sum1 = 0;
        //        String zc1 = String.Empty;
        //        foreach (ZhiChuData d1 in arrZc[i-1])
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

        //         if (isYearFlag == false)
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
        //        Label labSzr = item.FindControl("szr"+i) as Label;
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
        //            link3.NavigateUrl = "SeeWordFile2.aspx?IsYear="+this.isYearPage.Value+"&TrainType=" + lab1.Text + "&Line=" + Server.UrlEncode(line0.ToString())
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
        //                link2.NavigateUrl = "BianZhuFenXi.aspx?IsYear="+this.isYearPage.Value+"&TrainType=2&Line=" + Server.UrlEncode(line0.ToString())
        //                                   + "&cds=" + cds.SelectedValue + "&hasDianChe=1&BianZhu=" + Server.UrlEncode(String.Format("{0},{1},{2},{3},{4}", yz1, yw1, rw1,ca1,sy1));
        //            }
        //            link2.ToolTip = String.Format("最优的编组配置为：硬座{0}/ 硬卧{1}/ 软卧{2}/ 餐车{3} /宿营车{4}", yz1, yw1, rw1,ca1,sy1);
        //            link3.NavigateUrl = link2.NavigateUrl.Replace("BianZhuFenXi.aspx", "SeeWordFile.aspx");

        //            if (this.bz.SelectedIndex == 1)
        //            {
        //                link2.NavigateUrl = "javascript:alert('提示：此功能不适合手动编组模式！');";
        //                link2.ToolTip = String.Format("手动编组：硬座{0}/ 硬卧{1}/ 软卧{2}/ 餐车{3}/宿营车{4}", yz1, yw1, rw1, ca1,sy1);
        //            }
        //        }
        //    }
            

        //    Label bianzhu1 = item.FindControl("bianzhu") as Label;
        //    if (bianzhu1 != null)
        //    {
        //        bianzhu1.Text = String.Format("{0}-{1}-{2}-{3}-{4}", yz1, yw1, rw1,ca1,sy1);
        //    }
        //}

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
                                newLine = Line.GetLine(A0, B0, 100,this.selTrain.Text.Trim(),this.onlygg.Checked);
                            }
                            else
                            {
                                newLine = Line.GetLineByFengDuanSearch(this.mulLine.Text.Trim(), this.onlygg.Checked);
                            }
                        }

                        if (newLine != null)
                        {
                            line1 = newLine.GetLayerLine(middle, ETrainType.动车CRH380A, false, 1,this.onlygg.Checked);
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
                    SearchObjectBU.SaveSearchResultToDb(A0, B0, newLine,this.mulLine.Text.Trim());


                    //=========设置车型的选择========================
                    /*
                     data1.Add("3", "直达25T");
                     data1.Add("2", "空调25G(直供电)");
                     data1.Add("21", "空调25G(非直供电)");
                     data1.Add("1", "空调25K");
                     data1.Add("0", "绿皮车25B");
                     */
                    int index1 = bz.SelectedIndex;
                    if (index1 == 3) index1 = 2;
                    Dictionary<String, String> data1 = CommTrain.GetNewFenXiData(index1);
                    this.Repeater2.DataSource = data1;
                    this.Repeater2.DataBind();
                    this.SearchInfo.Visible = true;
                    this.NewBindComplete();

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

        // 设置线路显示
        private void SetLineItem(ListControl list1, List<TrainLine> line)
        {
            list1.Items.Clear();
            int i = 0;
            if (line != null && line.Count > 0)
            {
                foreach (TrainLine m in line)
                {
                    int total = m.TotalMiles;
                    String KeyID = i + "";
                    String Text = "<span title='" + m.ToString() + "'>" + m.ToBigString() + " (" + total + "km)" + "</span>";
                    if (this.ChkShowFullLine.Checked)
                    {
                        Text = "<span title='" + m.ToString() + "'>" + m.ToString() + " (" + total + "km)" + "</span>";
                    }
                    ListItem item1 = new ListItem(Text, KeyID);
                    list1.Items.Add(item1);
                    i++;
                }
            }

            if (list1.Items.Count > 0)
            {
                list1.SelectedIndex = 0;
            }
        }

        #endregion

    }
}
