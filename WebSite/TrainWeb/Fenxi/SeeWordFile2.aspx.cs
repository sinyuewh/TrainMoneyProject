using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using BusinessRule;
using System.Web.UI.HtmlControls;
using WebFrame.Util;
using System.Data;

namespace WebSite.TrainWeb.Fenxi
{
    public partial class SeeWord2 : System.Web.UI.Page
    {
        private const int XiShu = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int pos1 = Request.Url.AbsoluteUri.LastIndexOf("/");
                String url1 = Request.Url.AbsoluteUri.Substring(0, pos1);

                this.wordUrl.Value = url1 + "/trainData2.xls";
                String ext = Path.GetExtension("trainData2.xls").Replace(".", "");
                this.wordExt.Value = ext;
                this.filename.Value = "trainData2.xls";

                if (String.IsNullOrEmpty(Request.QueryString["IsYear"]) == false)
                {
                    if (Request.QueryString["IsYear"] == "1")
                    {
                        this.wordUrl.Value = url1 + "/trainDataYear2.xls";
                        ext = Path.GetExtension("trainDataYear2.xls").Replace(".", "");
                        this.wordExt.Value = ext;
                        this.filename.Value = "trainDataYear2.xls";
                    }
                }

                TrainLine lineObj= this.SetData();
                this.SetPriceAndPerson(lineObj);

            }
        }

        private void SetSubData(int col, ZhiChuData zc1, int XiaoShou,HighTrain  train2)
        {
            switch (zc1.ZhiChuName)
            {
                case "局外线路使用费":
                    HtmlInputHidden hid1 = this.f3_3.Parent.FindControl("f" + col + "_3") as HtmlInputHidden;
                    if (hid1 != null) hid1.Value = JMath.Round1(zc1.ZhiChu / XiShu, XiaoShou) + "";
                    break;

                case "电网和接触网使用费":
                    HtmlInputHidden hid2 = this.f3_3.Parent.FindControl("f" + col + "_4") as HtmlInputHidden;
                    if (hid2 != null) hid2.Value = JMath.Round1(zc1.ZhiChu / XiShu, XiaoShou) + "";
                    break;

                case "售票服务费":
                    HtmlInputHidden hid3 = this.f3_3.Parent.FindControl("f" + col + "_5") as HtmlInputHidden;
                    if (hid3 != null) hid3.Value = JMath.Round1(zc1.ZhiChu / XiShu, XiaoShou) + "";
                    break;

                case "旅客服务费":
                    HtmlInputHidden hid4 = this.f3_3.Parent.FindControl("f" + col + "_6") as HtmlInputHidden;
                    if (hid4 != null) hid4.Value = JMath.Round1(zc1.ZhiChu / XiShu, XiaoShou) + "";
                    break;

                case "列车上水费":
                    HtmlInputHidden hid5 = this.f3_3.Parent.FindControl("f" + col + "_7") as HtmlInputHidden;
                    if (hid5 != null) hid5.Value = JMath.Round1(zc1.ZhiChu / XiShu, XiaoShou) + "";
                    break;

                case "人员工资和工资附加费":
                    HtmlInputHidden hid6 = this.f3_3.Parent.FindControl("f" + col + "_8") as HtmlInputHidden;
                    if (hid6 != null) hid6.Value = JMath.Round1(zc1.ZhiChu / XiShu, XiaoShou) + "";
                    break;

                case "车辆折旧费":
                    HtmlInputHidden hid7 = this.f3_3.Parent.FindControl("f" + col + "_9") as HtmlInputHidden;
                    if (hid7 != null) hid7.Value = JMath.Round1(zc1.ZhiChu / XiShu, XiaoShou) + "";
                    break;

                case "列车日常检修成本":
                    HtmlInputHidden hid8 = this.f3_3.Parent.FindControl("f" + col + "_10") as HtmlInputHidden;
                    if (hid8 != null) hid8.Value = JMath.Round1(zc1.ZhiChu / XiShu, XiaoShou) + "";
                    break;

                case "定期检修成本":
                    HtmlInputHidden hid9 = this.f3_3.Parent.FindControl("f" + col + "_11") as HtmlInputHidden;
                    if (hid9 != null) hid9.Value = JMath.Round1(zc1.ZhiChu / XiShu, XiaoShou) + "";
                    break;

                case "车辆消耗备用品":
                    HtmlInputHidden hid10 = this.f3_3.Parent.FindControl("f" + col + "_12") as HtmlInputHidden;
                    if (hid10 != null) hid10.Value = JMath.Round1(zc1.ZhiChu / XiShu, XiaoShou) + "";
                    break;

                case "空调车用油":
                    HtmlInputHidden hid11 = this.f3_3.Parent.FindControl("f" + col + "_13") as HtmlInputHidden;
                    if (hid11 != null) hid11.Value = JMath.Round1(zc1.ZhiChu / XiShu, XiaoShou) + "";
                    break;

                case "人员其他费用":
                    HtmlInputHidden hid12 = this.f3_3.Parent.FindControl("f" + col + "_14") as HtmlInputHidden;
                    if (hid12 != null) hid12.Value = JMath.Round1(zc1.ZhiChu / XiShu, XiaoShou) + "";
                    break;

                case "购买车辆利息":
                    HtmlInputHidden hid13 = this.f3_3.Parent.FindControl("f" + col + "_15") as HtmlInputHidden;
                    if (hid13 != null) hid13.Value = JMath.Round1(zc1.ZhiChu / XiShu, XiaoShou) + "";
                    break;

                case "间接费用分摊":
                    this.jianjiefee.Value = train2.GetFee16() + "";
                    break;

                default:
                    break;
            }
        }

        private TrainLine SetData()
        {
            String line1 = Request.QueryString["Line"];
            int traintype1 = int.Parse(Request.QueryString["TrainType"]);
            int dianche = int.Parse(Request.QueryString["hasDianChe"]);
            bool hasDianChe = false;

            bool isYearFlag = true;
            int XiaoShou = 0;
            if (String.IsNullOrEmpty(Request.QueryString["IsYear"]) == false)
            {
                if (Request.QueryString["IsYear"] == "0")
                {
                    isYearFlag = false;
                    XiaoShou = 2;
                }
            }
          
            String traintypename = ((ETrainType)traintype1).ToString();
            if ((ETrainType)traintype1 == ETrainType.空调车25G)
            {
                if (dianche > 0)
                {
                    traintypename = traintypename + "(非直供电)";
                    hasDianChe = true;
                }
                else
                {
                    traintypename = traintypename + "(直供电)";
                    hasDianChe = false;
                }
            }

            String[] lineNodes = line1.Replace("-", ",").Split(',');
            TrainLine lineObj = Line.GetTrainLineByTrainTypeAndLineNoeds((ETrainType)traintype1,hasDianChe,lineNodes);
            this.traintype.Value = traintypename;
            this.totalmiles.Value = lineObj.TotalMiles + "";

            //设置起始站点
            this.as1.Value = lineObj.Nodes[0].AStation;
            this.bs1.Value = lineObj.Nodes[lineObj.Nodes.Count - 1].BStation;

            
            //计算支出
            HighTrain  train2 = new HighTrain();
            if (String.IsNullOrEmpty(Request.QueryString["cds"]) == false)
            {
                train2.CheDiShu = double.Parse(Request.QueryString["cds"]);
            }

            train2.Line = lineObj;
            train2.IsYearFlag = isYearFlag;
            EHighTrainType commtype = (EHighTrainType)traintype1;
            train2.TrainType = commtype;
            train2.YunXingLiCheng = lineObj.TotalMiles;
            double JnFee = 0;
            double ShouRu = 0;

            String findcond;
            if (Session["FindCond"] == null)
            {
                findcond = "";
            }
            else
            {
                findcond = Session["FindCond"].ToString().Trim();
            }
            //1计算四种不同模式的支出
            train2.CunZengMoShi = ECunZengMoShi.新人新车;
            List<ZhiChuData> zhichu1 = train2.GetShouRuAndZhiChu(out ShouRu, findcond);
            JnFee = train2.JnFee;
            int col = 3;
            int row = 3;
            foreach (ZhiChuData zc1 in zhichu1)
            {
                if (zc1.ZhiChuName != "机车牵引费")
                {
                    //HtmlInputHidden hid1 = this.f3_3.Parent.FindControl("f" + col + "_" + row) as HtmlInputHidden;
                    //if (hid1 != null) hid1.Value = JMath.Round1(zc1.ZhiChu / XiShu, XiaoShou) + "";
                    //row++;
                    SetSubData(col, zc1, XiaoShou, train2);
                }
            }
            f3_16.Value = JMath.Round1(JnFee / XiShu, XiaoShou) + "";
            this.jfee1.Value = JMath.Round1(train2.JnFee / XiShu, XiaoShou) + "";
            this.jfee2.Value = JMath.Round1(train2.JnSaleFee / XiShu, XiaoShou) + "";
            this.jfee3.Value = JMath.Round1(train2.JnServerFee / XiShu, XiaoShou) + "";


            //2
            train2.CunZengMoShi = ECunZengMoShi.新人有车;
            List<ZhiChuData> zhichu2 = train2.GetShouRuAndZhiChu(out ShouRu, findcond);
            JnFee = train2.JnFee;
            col = 4;
            row = 3;
            foreach (ZhiChuData zc1 in zhichu2)
            {
                if (zc1.ZhiChuName != "机车牵引费")
                {
                    SetSubData(col, zc1, XiaoShou, train2);
                }
            }
            f4_16.Value = JMath.Round1(JnFee / XiShu, XiaoShou) + "";
            this.jfee4.Value = JMath.Round1(train2.JnFee / XiShu, XiaoShou) + "";
            this.jfee5.Value = JMath.Round1(train2.JnSaleFee / XiShu, XiaoShou) + "";
            this.jfee6.Value = JMath.Round1(train2.JnServerFee / XiShu, XiaoShou) + "";

            //3
            train2.CunZengMoShi = ECunZengMoShi.有人新车;
            List<ZhiChuData> zhichu3 = train2.GetShouRuAndZhiChu(out ShouRu, findcond);
            JnFee = train2.JnFee;
            col = 5;
            row = 3;
            foreach (ZhiChuData zc1 in zhichu3)
            {
                if (zc1.ZhiChuName != "机车牵引费")
                {
                    SetSubData(col, zc1, XiaoShou, train2);
                }
            }
            f5_16.Value = JMath.Round1(JnFee / XiShu, 0) + "";
            this.jfee7.Value = JMath.Round1(train2.JnFee / XiShu, XiaoShou) + "";
            this.jfee8.Value = JMath.Round1(train2.JnSaleFee / XiShu, XiaoShou) + "";
            this.jfee9.Value = JMath.Round1(train2.JnServerFee / XiShu, XiaoShou) + "";

            //4
            train2.CunZengMoShi = ECunZengMoShi.有人有车;
            List<ZhiChuData> zhichu4 = train2.GetShouRuAndZhiChu(out ShouRu, findcond);
            JnFee = train2.JnFee;
            col = 6;
            row = 3;
            foreach (ZhiChuData zc1 in zhichu4)
            {
                if (zc1.ZhiChuName != "机车牵引费")
                {
                    SetSubData(col, zc1, XiaoShou, train2);
                }
            }
            f6_16.Value = JMath.Round1(JnFee / XiShu, XiaoShou) + "";
            this.jfee10.Value = JMath.Round1(train2.JnFee / XiShu, XiaoShou) + "";
            this.jfee11.Value = JMath.Round1(train2.JnSaleFee / XiShu, XiaoShou) + "";
            this.jfee12.Value = JMath.Round1(train2.JnServerFee / XiShu, XiaoShou) + "";

            //设置间接费用分摊
            //this.jianjiefee.Value = train2.GetFee16() + "";

            return lineObj;
        }


        //设置票价和满员
        private void SetPriceAndPerson(TrainLine line)
        {
            DataTable dt = HighTrainProfile.Data;
            int traintype1 = int.Parse(Request.QueryString["TrainType"]);
            EHighTrainType type1 = (EHighTrainType)traintype1;
            
            DataRow[] drs = dt.Select("HIGHTRAINTYPE='"+type1.ToString()+"'");
            if (drs != null && drs.Length > 0)
            {
                DataRow dr = drs[0];
                this.m1.Value = dr["pcount1"].ToString();
                this.m2.Value = dr["pcount2"].ToString();
                this.m3.Value = dr["pcount4"].ToString();
                this.m4.Value = dr["pcount3"].ToString();
                this.m5.Value = dr["pcount5"].ToString();
            }
            //设置票价
            int licheng = int.Parse(this.totalmiles.Value);
            this.p1.Value=DTrainShouRu.GetTrainPrice(type1, EDTrainFareType.一等软座,line, licheng)+"";
            this.p2.Value = DTrainShouRu.GetTrainPrice(type1, EDTrainFareType.二等软座,line, licheng) + "";
            this.p3.Value = DTrainShouRu.GetTrainPrice(type1, EDTrainFareType.商务座,line,licheng) + "";

            this.p4.Value = DTrainShouRu.GetTrainPrice(type1, EDTrainFareType.动卧上铺,line, licheng) + "";
            this.p5.Value = DTrainShouRu.GetTrainPrice(type1, EDTrainFareType.动卧下铺,line,licheng) + "";

            this.p6.Value = DTrainShouRu.GetTrainPrice(type1, EDTrainFareType.特定座, line, licheng) + "";
        }
    }
}
