using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using BusinessRule;
using System.Web.UI.HtmlControls;
using WebFrame.Util;

namespace WebSite.TrainWeb.Fenxi
{
    public partial class SeeWordFile : System.Web.UI.Page
    {
        private const int XiShu = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int pos1 = Request.Url.AbsoluteUri.LastIndexOf("/");
                String url1 = Request.Url.AbsoluteUri.Substring(0, pos1);

                this.wordUrl.Value = url1 + "/trainData1.xls";
                String ext = Path.GetExtension("trainData1.xls").Replace(".", "");
                this.wordExt.Value = ext;
                this.filename.Value = "trainData1.xls";

                if (String.IsNullOrEmpty(Request.QueryString["IsYear"]) == false)
                {
                    if (Request.QueryString["IsYear"] == "1")
                    {
                        this.wordUrl.Value = url1 + "/trainDataYear1.xls";
                        ext = Path.GetExtension("trainDataYear1.xls").Replace(".", "");
                        this.wordExt.Value = ext;
                        this.filename.Value = "trainDataYear1.xls";
                    }
                }

                this.SetData();
                this.SetPriceAndPerson();

            }
        }

        private void SetSubData(int col, ZhiChuData zc1, int XiaoShou, CommTrain train2)
        {
            switch (zc1.ZhiChuName)
            {
                case "局外线路使用费":
                    HtmlInputHidden hid1 = this.f3_3.Parent.FindControl("f" + col + "_3") as HtmlInputHidden;
                    if (hid1 != null) hid1.Value = JMath.Round1(zc1.ZhiChu / XiShu, XiaoShou) + "";
                    break;

                case "机车牵引费":
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

                case "轮渡费":
                    this.shipfee.Value = train2.GetFee15()+"";
                    break;

                case "间接费用分摊":
                    this.jianjiefee.Value = train2.GetFee16() + "";
                    break;
                    
                default:
                    break;
            }
        }

        private void SetData()
        {
            String line1 = Request.QueryString["Line"];
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

            int traintype1 = int.Parse(Request.QueryString["TrainType"]);
            int dianche = int.Parse(Request.QueryString["hasDianChe"]);
            String[] bianzhu = Request.QueryString["BianZhu"].Split(',');
            bool hasDianChe = false;
            if (dianche > 0) hasDianChe = true;

            this.yz.Value = bianzhu[0];
            this.yw.Value = bianzhu[1];
            this.rw.Value = bianzhu[2];
            this.ca.Value = "1";
            this.sy.Value = "0";

            if (bianzhu.Length >= 4)
            {
                this.ca.Value = bianzhu[3];
            }
            if (bianzhu.Length >= 5)
            {
                this.sy.Value = bianzhu[4];
            }

            String traintypename = ((ETrainType)traintype1).ToString();
            if (traintype1 == 2 || traintype1 == 1)
            {
                if (dianche > 0)
                {
                    traintypename = traintypename + "(非直供电)";
                }
                else
                {
                    traintypename = traintypename + "(直供电)";
                }
            }

            String[] lineNodes = line1.Replace("-", ",").Split(',');
            TrainLine lineObj = Line.GetTrainLineByTrainTypeAndLineNoeds((ETrainType)traintype1,hasDianChe, lineNodes);
            this.traintype.Value = traintypename;
            this.totalmiles.Value = lineObj.TotalMiles+"";

            //设置起始站点
            this.as1.Value = lineObj.Nodes[0].AStation;
            this.bs1.Value = lineObj.Nodes[lineObj.Nodes.Count - 1].BStation;

            //计算支出
            CommTrain train2 = new CommTrain();
            train2.IsYearFlag = isYearFlag;

            if (String.IsNullOrEmpty(Request.QueryString["cds"]) == false)
            {
                train2.CheDiShu = double.Parse(Request.QueryString["cds"]);
            }

            train2.Line = lineObj;
            ECommTrainType commtype = (ECommTrainType)traintype1;
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
            int yz1 = int.Parse(this.yz.Value);
            int yw1 = int.Parse(this.yw.Value);
            int rw1 = int.Parse(this.rw.Value);
            int ca1 = int.Parse(this.ca.Value);
            int sy1 = int.Parse(this.sy.Value);

            train2.CunZengMoShi = ECunZengMoShi.新人新车;
            List<ZhiChuData> zhichu1 = train2.GetShouRuAndZhiChu(out ShouRu, yz1, yw1, rw1, sy1, ca1, hasDianChe, findcond);
            JnFee = train2.JnFee;
            int col=3;
            int row=3;
            foreach(ZhiChuData zc1 in zhichu1)
            {
                if (zc1.ZhiChuName != "电网和接触网使用费")
                {
                    SetSubData(col, zc1, XiaoShou, train2);
                }
            }
            f3_16.Value = JMath.Round1(JnFee / XiShu, XiaoShou) + "";
            this.jfee1.Value = JMath.Round1(train2.JnFee / XiShu, XiaoShou) + "";
            this.jfee2.Value = JMath.Round1(train2.JnSaleFee / XiShu, XiaoShou) + "";
            this.jfee3.Value = JMath.Round1(train2.JnServerFee / XiShu, XiaoShou) + "";

            //2
            train2.CunZengMoShi = ECunZengMoShi.新人有车;
            List<ZhiChuData> zhichu2 = train2.GetShouRuAndZhiChu(out ShouRu, yz1, yw1, rw1, sy1, ca1, hasDianChe, findcond);
            JnFee = train2.JnFee;
            col = 4;
            row = 3;
            foreach (ZhiChuData zc1 in zhichu2)
            {
                if (zc1.ZhiChuName != "电网和接触网使用费")
                {
                    //HtmlInputHidden hid1 = this.f3_3.Parent.FindControl("f" + col + "_" + row) as HtmlInputHidden;
                    //if (hid1 != null) hid1.Value = JMath.Round1(zc1.ZhiChu / XiShu, XiaoShou) + "";
                   // row++;
                    SetSubData(col, zc1, XiaoShou, train2);
                }
            }
            f4_16.Value = JMath.Round1(JnFee / XiShu, XiaoShou) + "";
            this.jfee4.Value = JMath.Round1(train2.JnFee / XiShu, XiaoShou) + "";
            this.jfee5.Value = JMath.Round1(train2.JnSaleFee / XiShu, XiaoShou) + "";
            this.jfee6.Value = JMath.Round1(train2.JnServerFee / XiShu, XiaoShou) + "";

            //3
            train2.CunZengMoShi = ECunZengMoShi.有人新车;
            List<ZhiChuData> zhichu3 = train2.GetShouRuAndZhiChu(out ShouRu, yz1, yw1, rw1, sy1, ca1, hasDianChe, findcond);
            JnFee = train2.JnFee;
            col = 5;
            row = 3;
            foreach (ZhiChuData zc1 in zhichu3)
            {
                if (zc1.ZhiChuName != "电网和接触网使用费")
                {
                    SetSubData(col, zc1, XiaoShou, train2);
                }
            }
            f5_16.Value = JMath.Round1(JnFee / XiShu, XiaoShou) + "";
            this.jfee7.Value = JMath.Round1(train2.JnFee / XiShu, XiaoShou) + "";
            this.jfee8.Value = JMath.Round1(train2.JnSaleFee / XiShu, XiaoShou) + "";
            this.jfee9.Value = JMath.Round1(train2.JnServerFee / XiShu, XiaoShou) + "";

            //4
            train2.CunZengMoShi = ECunZengMoShi.有人有车;
            List<ZhiChuData> zhichu4 = train2.GetShouRuAndZhiChu(out ShouRu, yz1, yw1, rw1, sy1, ca1, hasDianChe, findcond);
            JnFee = train2.JnFee;
            col = 6;
            row = 3;
            foreach (ZhiChuData zc1 in zhichu4)
            {
                if (zc1.ZhiChuName != "电网和接触网使用费")
                {
                    SetSubData(col, zc1, XiaoShou, train2);
                }
            }
            f6_16.Value = JMath.Round1(JnFee / XiShu, XiaoShou) + "";
            this.jfee10.Value = JMath.Round1(train2.JnFee / XiShu, XiaoShou) + "";
            this.jfee11.Value = JMath.Round1(train2.JnSaleFee / XiShu, XiaoShou) + "";
            this.jfee12.Value = JMath.Round1(train2.JnServerFee / XiShu, XiaoShou) + "";

            //计算轮渡费和间接分摊费
            //this.shipfee.Value = train2.GetFee15()+"";
            //this.jianjiefee.Value = train2.GetFee16() + "";
        }

        //设置票价和满员
        private void SetPriceAndPerson()
        {
            TicketPrice ticket = new TicketPrice();
            int traintype1 = int.Parse(Request.QueryString["TrainType"]);
            ETrainType traintype = (ETrainType)traintype1;
            ECommTrainType type1 = (ECommTrainType)((int)traintype);

            int yunxingLiCheng=int.Parse(this.totalmiles.Value);

            TrainShouRu1 shour1 = null;
            if (traintype == ETrainType.绿皮车25B)
            {
                shour1 = new TrainShouRu1(yunxingLiCheng,false,EJiaKuai.其他);
            }
            else
            {
                shour1 = new TrainShouRu2(yunxingLiCheng, EJiaKuai.特快);
            }

            
            //设置票价
            this.p1.Value = shour1.YinZuoPrice + shour1.GetKongTiaoFee(type1, ECommCheXian.硬座) + shour1.JiaKuaiFee + "";

            this.p2.Value = shour1.YinZuoPrice + shour1.GetKongTiaoFee(type1, ECommCheXian.硬座) +
                    shour1.JiaKuaiFee + shour1.YinWoPrice1+"";
            this.p3.Value = shour1.YinZuoPrice + shour1.GetKongTiaoFee(type1, ECommCheXian.硬座)
                    + shour1.JiaKuaiFee + shour1.YinWoPrice2+"";
            this.p4.Value = shour1.YinZuoPrice + shour1.GetKongTiaoFee(type1, ECommCheXian.硬座)
                    + shour1.JiaKuaiFee + shour1.YinWoPrice3+"";

            this.p5.Value=shour1.RuanZuoPrice
                    + shour1.GetKongTiaoFee(type1, ECommCheXian.软卧) + shour1.JiaKuaiFee + shour1.RuanWoPrice1 + "";
            this.p6.Value = shour1.RuanZuoPrice + shour1.GetKongTiaoFee(type1, ECommCheXian.软卧) 
                    + shour1.JiaKuaiFee + shour1.RuanWoPrice2+"";
            
            //设置定员
            this.m1.Value = ChexianBianZhuData.YinZuo_Pcount + "";
            this.m2.Value=(ChexianBianZhuData.YinWo1_Pcount+
                           ChexianBianZhuData.YinWo2_Pcount+
                           ChexianBianZhuData.YinWo3_Pcount)+"";
            this.m3.Value = (ChexianBianZhuData.RuanWo1_Pcount
                          + ChexianBianZhuData.RuanWo2_Pcount) + "";
            this.m4.Value = TrainProfile.SyCheXianPCount + "";
        }
    }
}
