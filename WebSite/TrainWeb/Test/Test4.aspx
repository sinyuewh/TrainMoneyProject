<%@ Page Language="C#" AutoEventWireup="true"  %>
<%@ Import  Namespace="BusinessRule" %>
<%@ Import  Namespace="System.Collections.Generic" %>

<script runat="server">
    protected override void OnLoad(EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CommTrain train1 = new CommTrain();
            train1.TrainType = ECommTrainType.空调车25T;
            //train1.GongDianType = EGongDianType.非直供电;
            
            int type1 = (int)train1.TrainType;
            String[] trainNodes="武汉-滠口-武汉北-信阳-漯河-孟庙-郑州".Split('-');
            TrainLine line1 = Line.GetTrainLineByTrainTypeAndLineNoeds((ETrainType)type1, trainNodes);
            
            train1.Line = line1;
           // train1.WaterCount = 1;
            
            train1.YinZuo = 0;
            train1.OpenYinWo = 17;
            train1.RuanWo = 0;
            train1.FaDianChe = 0;

            Response.Write("线路：武汉-滠口-武汉北-信阳-漯河-孟庙-郑州<br>");
            Response.Write("总里程：" + line1.TotalMiles + "<br>");
            Response.Write("车型：" + train1.TrainType.ToString() + "<br>");
            Response.Write("收支明细表：单位（万元）<br>");
            Response.Write("==================================================<br>");
            
            double shouru = 0;
            List<ZhiChuData> zcList= train1.GetShouRuAndZhiChu(out shouru);
            Response.Write("收入：" + WebFrame.Util.JMath.Round1(shouru/10000,0)+"<br>");
            int i = 1;
            double fee = 0;
            foreach (ZhiChuData data1 in zcList)
            {
                double temp = WebFrame.Util.JMath.Round1(data1.ZhiChu / 10000, 0);
                Response.Write(i + ")" + data1.ZhiChuName + "：" + temp + "<br>");
                fee = fee + temp;
                i++;
            }
            Response.Write("总支出：" + fee + "<br>");
            Response.Write("局内线路使用费：" + WebFrame.Util.JMath.Round1(train1.JnFee/10000,0) + "<br>");


            


            CommTrain train2 = new CommTrain();
            train2.TrainType = ECommTrainType.空调车25T;
            train2.CheDiShu = 2.0;
            train2.Line = line1;
            
            //计算四种不同模式的支出
            double ShouRu = 0;
            int yz = 0, yw = 0, rw = 0;
            double JnFee = 0;
            train2.CunZengMoShi = ECunZengMoShi.新人新车;
            List<ZhiChuData> zhichu1 = train2.GetShouRuAndZhiChuByGoodBianZhu(out ShouRu, out yz, out yw, out rw,false);
            JnFee = train1.JnFee;
            
            

            Response.Write("==================================================<br>");
            fee = 0;
            i = 1;
            foreach (ZhiChuData data1 in zhichu1)
            {
                double temp = WebFrame.Util.JMath.Round1(data1.ZhiChu / 10000, 0);
                Response.Write(i + ")" + data1.ZhiChuName + "：" + temp + "<br>");
                fee = fee + temp;
                i++;
            }
            Response.Write("总支出：" + fee + "<br>");
            Response.Write("局内线路使用费：" + WebFrame.Util.JMath.Round1(train1.JnFee / 10000, 0) + "<br>");
            Response.Write(ShouRu + "" + "<br>");
            Response.Write("yz="+yz+"yw="+yw + "" + "<br>");
        }
        base.OnLoad(e);
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
