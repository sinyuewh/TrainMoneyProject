using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessRule;
using WebFrame.Util;
using org.in2bits.MyXls;
using System.Data;

namespace WebSite.TrainWeb.Fenxi
{
    public partial class BianZhuFenXi : System.Web.UI.Page
    {
        static Random rand = new Random();
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

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                String line1 = Request.QueryString["Line"];
                int traintype1 = int.Parse(Request.QueryString["TrainType"]);
                this.traintype.Text = ((ETrainType)traintype1).ToString();
                bool hasDianche = true;

                if ((ETrainType)traintype1 == ETrainType.空调车25G)
                {
                    int dianche = int.Parse(Request.QueryString["hasDianChe"]);
                    if (dianche > 0)
                    {
                        this.traintype.Text = this.traintype.Text + "(非直供电)";
                        hasDianche = true;
                    }
                    else
                    {
                        this.traintype.Text = this.traintype.Text + "(直供电)";
                        hasDianche = false;
                    }
                }
                
                ETrainType traintype2 = (ETrainType)traintype1;
                String[] lineNodes = line1.Replace("-", ",").Split(',');
                String[] bianzhu = Request.QueryString["BianZhu"].Split(',');

                this.yz1.Text = bianzhu[0];
                this.yw1.Text = bianzhu[1];
                this.rw1.Text = bianzhu[2];

                //保存总的编组数
                int totalBianZhu = int.Parse(bianzhu[0]) + int.Parse(bianzhu[1]) + int.Parse(bianzhu[2]);


                //设置其他的默认编组
                TrainLine lineObj = Line.GetTrainLineByTrainTypeAndLineNoeds(traintype2,hasDianche,lineNodes);
                ViewState["Line"] = lineObj;
                this.line.Text = line1+"("+lineObj.TotalMiles+"公里)";

                //设置其他的编组的数据
                ECommTrainType traintype3 = (ECommTrainType)traintype1;
                this.SetBianZhu(traintype3,totalBianZhu);
                this.CalShouruAndZhiChu();

                DataTable dt1 = ViewState["BianZhuData"] as DataTable;
                if (dt1 != null)
                {
                    DataView dv = dt1.DefaultView;
                    dv.Sort = "sz desc";
                    this.repeater1.DataSource = dv;
                    this.repeater1.DataBind();
                }
            }
            base.OnLoad(e);
        }


        /// <summary>
        /// 设置其他的编组数据
        /// </summary>
        /// <param name="trainType"></param>
        private void SetBianZhu(ECommTrainType trainType,int totalBianZhu)
        {
            int yz0 = 0; int yz1 = 0;
            int yw0 = 0; int yw1 = 0;
            int rw0 = 0; int rw1 = 0;

            if (trainType == ECommTrainType.空调车25G)
            {
                yz0 = BZData.Train25G[0]; yz1 = BZData.Train25G[1];
                yw0 = BZData.Train25G[2]; yw1 = BZData.Train25G[3];
                rw0 = BZData.Train25G[4]; rw1 = BZData.Train25G[5];
            }
            else if (trainType == ECommTrainType.空调车25K)
            {
                yz0 = BZData.Train25K[0]; yz1 = BZData.Train25K[1];
                yw0 = BZData.Train25K[2]; yw1 = BZData.Train25K[3];
                rw0 = BZData.Train25K[4]; rw1 = BZData.Train25K[5];
            }
            else if (trainType == ECommTrainType.空调车25T)
            {
                yz0 = BZData.Train25T[0]; yz1 = BZData.Train25T[1];
                yw0 = BZData.Train25T[2]; yw1 = BZData.Train25T[3];
                rw0 = BZData.Train25T[4]; rw1 = BZData.Train25T[5];
            }
            else if (trainType == ECommTrainType.绿皮车25B)
            {
                yz0 = BZData.Train25B[0]; yz1 = BZData.Train25B[1];
                yw0 = BZData.Train25B[2]; yw1 = BZData.Train25B[3];
                rw0 = BZData.Train25B[4]; rw1 = BZData.Train25B[5];
            }

            //设置数据的值
            int yz = 0, yw = 0, rw = 0;
            int oldyz = int.Parse(this.yz1.Text);
            int oldyw = int.Parse(this.yw1.Text);
            int dianche = int.Parse(Request.QueryString["hasDianChe"]);

            for (int i = 2; i <= 5; i++)
            {
                this.GetRandom(totalBianZhu, yz0, yz1, yw0, yw1, rw0, rw1,
                    oldyz,oldyw,out yz, out yw);

                rw = totalBianZhu -dianche  - yz - yw;

                TextBox t1 = this.yz1.Parent.FindControl("yz" + i) as TextBox;
                TextBox t2 = this.yz1.Parent.FindControl("yw" + i) as TextBox;
                TextBox t3 = this.yz1.Parent.FindControl("rw" + i) as TextBox;
                t1.Text = yz + "";
                t2.Text = yw + "";
                t3.Text = rw + "";
            }
            
        }

        private void GetRandom(
            int totalBianZhu,
            int yz0, int yz1, 
            int yw0, int yw1,
            int rw0,int rw1,
            int oldyz,int oldyw,

            out int yz, out int yw)
        {
            yz = 0;
            yw = 0;
            int dianche = int.Parse(Request.QueryString["hasDianChe"]);

            while (true)
            {
                
                yz = rand.Next(yz0, yz1);

                //Random r2 = new Random();
                yw = rand.Next(yw0, yw1);

                if (yz + yw <= totalBianZhu  - rw0-dianche 
                    && yz + yw >= totalBianZhu  - rw1-dianche
                    && (yz!=oldyz || yw!=oldyw))
                {
                    break;
                }
            }
        }
        /// <summary>
        /// 原方法
        /// </summary>
        //private void CalShouruAndZhiChu()
        //{
        //    DataTable dt1 = new DataTable();
        //    dt1.Columns.Add("yz", typeof(int));
        //    dt1.Columns.Add("yw", typeof(int));
        //    dt1.Columns.Add("rw", typeof(int));
        //    dt1.Columns.Add("sr", typeof(double));
        //    dt1.Columns.Add("zc", typeof(double));
        //    dt1.Columns.Add("sz",typeof(double));


        //    CommTrain train1 = new CommTrain();
        //    TrainLine lineObj = ViewState["Line"] as TrainLine;
        //    int traintype1 = int.Parse(Request.QueryString["TrainType"]);
        //    train1.TrainType = (ECommTrainType)traintype1;
        //    train1.YunXingLiCheng = lineObj.TotalMiles;
        //    int dianche = int.Parse(Request.QueryString["hasDianChe"]);

        //    bool isYearFlag = true;
        //    if (String.IsNullOrEmpty(Request.QueryString["IsYear"]) == false)
        //    {
        //        if (Request.QueryString["IsYear"] == "0") isYearFlag = false;
        //    }
        //    train1.IsYearFlag = isYearFlag;

        //    for (int i = 1; i <= 5; i++)
        //    {
        //        DataRow dr1 = dt1.NewRow();
        //        TextBox t1 = this.yz1.Parent.FindControl("yz" + i) as TextBox;
        //        TextBox t2 = this.yz1.Parent.FindControl("yw" + i) as TextBox;
        //        TextBox t3 = this.yz1.Parent.FindControl("rw" + i) as TextBox;

        //        //设置车厢的编组
        //        train1.YinZuo = int.Parse(t1.Text);

        //        train1.OpenYinWo = int.Parse(t2.Text);
        //        train1.RuanWo = int.Parse(t3.Text);
        //        train1.CheDiShu = int.Parse(Request.QueryString["cds"]);

        //        //将数据保存到Table
        //        dr1["yz"] = int.Parse(t1.Text);
        //        dr1["yw"] = int.Parse(t2.Text);
        //        dr1["rw"] = int.Parse(t3.Text);

        //        if (dianche > 0)
        //        {
        //            train1.FaDianChe = 1;
        //            train1.GongDianType = EGongDianType.非直供电;
        //        }
        //        train1.Line = lineObj;

        //        //计算费用
        //        Label lab1 = this.yz1.Parent.FindControl("sr" + i) as Label;
        //        double s = train1.GetShouRu() / 10000;
        //        if (isYearFlag)
        //        {
        //            lab1.Text = String.Format("{0:n0}", s);
        //        }
        //        else
        //        {
        //            lab1.Text = String.Format("{0:n2}", s);
        //        }
        //        dr1["sr"] = double.Parse(lab1.Text);

        //        String findcond = "";
        //        List<ZhiChuData> zlist = train1.GetZhiChu(findcond);
        //        double z = 0;
        //        foreach (ZhiChuData data1 in zlist)
        //        {
        //            z = z + data1.ZhiChu;
        //        }
        //        //z = z / 10000;

        //        Label lab2 = this.yw1.Parent.FindControl("zc" + i) as Label;
        //        if (isYearFlag)
        //        {
        //            lab2.Text = String.Format("{0:n0}", z);
        //        }
        //        else
        //        {
        //            lab2.Text = String.Format("{0:n2}", z);
        //        }
        //        dr1["zc"] = double.Parse(lab2.Text);


        //        Label lab3 = this.yw1.Parent.FindControl("sz" + i) as Label;
        //        if (isYearFlag)
        //        {
        //            lab3.Text = String.Format("{0:n0}", s * SRate - z);
        //        }
        //        else
        //        {
        //            lab3.Text = String.Format("{0:n2}", s * SRate - z);
        //        }
        //        if (s - z < 0)
        //        { lab3.ForeColor = System.Drawing.Color.Red; }
        //        else
        //        {
        //            lab3.ForeColor = System.Drawing.Color.Empty;
        //        }
        //        dr1["sz"] = double.Parse(lab3.Text);

        //        dt1.Rows.Add(dr1);
        //    }

        //    ViewState["BianZhuData"] = dt1;
        //}
        /// <summary>
        /// 2014 0417
        /// </summary>
        private void CalShouruAndZhiChu()
        {
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("yz", typeof(int));
            dt1.Columns.Add("yw", typeof(int));
            dt1.Columns.Add("rw", typeof(int));
            dt1.Columns.Add("sr", typeof(double));
            dt1.Columns.Add("zc", typeof(double));
            dt1.Columns.Add("sz", typeof(double));


            CommTrain train1 = new CommTrain();
            TrainLine lineObj = ViewState["Line"] as TrainLine;
            int traintype1 = int.Parse(Request.QueryString["TrainType"]);
            train1.TrainType = (ECommTrainType)traintype1;
            train1.YunXingLiCheng = lineObj.TotalMiles;
            int dianche = int.Parse(Request.QueryString["hasDianChe"]);

            bool isYearFlag = true;
            if (String.IsNullOrEmpty(Request.QueryString["IsYear"]) == false)
            {
                if (Request.QueryString["IsYear"] == "0") isYearFlag = false;
            }
            train1.IsYearFlag = isYearFlag;

            for (int i = 1; i <= 5; i++)
            {
                DataRow dr1 = dt1.NewRow();
                TextBox t1 = this.yz1.Parent.FindControl("yz" + i) as TextBox;
                TextBox t2 = this.yz1.Parent.FindControl("yw" + i) as TextBox;
                TextBox t3 = this.yz1.Parent.FindControl("rw" + i) as TextBox;

                //设置车厢的编组
                train1.YinZuo = int.Parse(t1.Text);

                train1.OpenYinWo = int.Parse(t2.Text);
                train1.RuanWo = int.Parse(t3.Text);
                train1.CheDiShu = int.Parse(Request.QueryString["cds"]);

                //将数据保存到Table
                dr1["yz"] = int.Parse(t1.Text);
                dr1["yw"] = int.Parse(t2.Text);
                dr1["rw"] = int.Parse(t3.Text);

                if (dianche > 0)
                {
                    train1.FaDianChe = 1;
                    train1.GongDianType = EGongDianType.非直供电;
                }
                train1.Line = lineObj;

                //计算费用
                Label lab1 = this.yz1.Parent.FindControl("sr" + i) as Label;
                double s = train1.GetShouRu() / 10000;
                if (isYearFlag)
                {
                    lab1.Text = String.Format("{0:n0}", s);
                }
                else
                {
                    lab1.Text = String.Format("{0:n2}", s);
                }
                dr1["sr"] = double.Parse(lab1.Text);

                String findcond = "";
                List<ZhiChuData> zlist = train1.GetZhiChu(findcond);
                double z = 0;
                foreach (ZhiChuData data1 in zlist)
                {
                    z = z + data1.ZhiChu;
                }
                //z = z / 10000;

                Label lab2 = this.yw1.Parent.FindControl("zc" + i) as Label;
                if (isYearFlag)
                {
                    lab2.Text = String.Format("{0:n0}", z);
                }
                else
                {
                    lab2.Text = String.Format("{0:n2}", z);
                }
                dr1["zc"] = double.Parse(lab2.Text);


                Label lab3 = this.yw1.Parent.FindControl("sz" + i) as Label;
                if (isYearFlag)
                {
                    lab3.Text = String.Format("{0:n0}", s * SRate - z);
                }
                else
                {
                    lab3.Text = String.Format("{0:n2}", s * SRate - z);
                }
                if (s - z < 0)
                { lab3.ForeColor = System.Drawing.Color.Red; }
                else
                {
                    lab3.ForeColor = System.Drawing.Color.Empty;
                }
                dr1["sz"] = double.Parse(lab3.Text);

                dt1.Rows.Add(dr1);
            }

            ViewState["BianZhuData"] = dt1;
        }
    }
}
