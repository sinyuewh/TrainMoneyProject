<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CalFareAndZhiChu.aspx.cs" Inherits="WebSite.TrainWeb.Test.CalFareAndZhiChu" %>
<%@ Import  Namespace="BusinessRule" %>
<script runat="server">
    protected override void OnLoad(EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int[] li = new int[] { 1182, 1234, 1265, 1345, 1387, 1445, 1487, 1523, 1567, 1654, 1680, 1745, 1799, 1867, 1923, 1987, 2056, 2098, 2166, 2250, 2287, 2376, 2467, 2499, 2574 };
            //int[] li = new int[] { 1069 };
            for (int i = 0; i < li.Length; i++)
            {
                int licheng = li[i];
                TrainShouRu1 shour1 = new TrainShouRu1(licheng, true, EJiaKuai.特快);
                Response.Write(shour1 + "<br>");

                //TrainShouRu2 shour2 = new TrainShouRu2(licheng,EJiaKuai.特快);
                //Response.Write(shour2 + "<br>");

                //double pj=DTrainShouRu.GetTrainPrice(EDTrainFareType.动车一等软座, licheng);
                //Response.Write(pj+ "<br>");
            }
        }
        base.OnLoad(e);
    }
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
