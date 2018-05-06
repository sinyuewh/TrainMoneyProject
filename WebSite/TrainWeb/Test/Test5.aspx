<%@ Page Language="C#" AutoEventWireup="true"  %>
<%@ Import  Namespace="BusinessRule" %>
<%@ Import  Namespace="System.Collections.Generic" %>

<script runat="server">
    protected override void OnLoad(EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            HighTrain train1 = new HighTrain();
            train1.TrainType = EHighTrainType.CRH2A;

            int type1 = (int)train1.TrainType;
            String[] trainNodes = "武汉-滠口-武汉北-信阳-漯河-孟庙-郑州".Split('-');
            TrainLine line1 = Line.GetTrainLineByTrainTypeAndLineNoeds((ETrainType)type1, trainNodes);

            train1.Line = line1;
            train1.WaterCount = 1;

            Response.Write("线路：武汉-滠口-武汉北-信阳-漯河-孟庙-郑州<br>");
            Response.Write("总里程：" + line1.TotalMiles + "<br>");
            Response.Write("车型：" + train1.TrainType.ToString() + "<br>");
            Response.Write("收支明细表：单位（万元）<br>");
            Response.Write("==================================================<br>");
            
            double shouru = 0;
            List<ZhiChuData> zcList = train1.GetShouRuAndZhiChu(out shouru);
            Response.Write("收入：" + WebFrame.Util.JMath.Round1(shouru / 10000, 0) + "<br>");
            int i = 1;
            double fee = 0;
            foreach (ZhiChuData data1 in zcList)
            {
                double temp = WebFrame.Util.JMath.Round1(data1.ZhiChu / 10000, 0);
                Response.Write(i + ")" + data1.ZhiChuName + "：" + temp + "<br>");
                fee = fee + temp;
                i++;
            }
            Response.Write("总支出：" + fee  + "<br>");
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
