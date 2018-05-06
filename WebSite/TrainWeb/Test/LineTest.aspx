<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LineTest.aspx.cs" Inherits="WebSite.TrainWeb.Test.LineTest" %>

<%@ Import Namespace="BusinessRule" %>
<%@ Import Namespace="System.Collections.Generic" %>

<script runat="server">
    protected override void OnLoad(EventArgs e)
    {
        Line newline = null;
        List<TrainLine> data1 = null;


       // newline = new Line("汉口", "南京南", 20);
        // List<List<LineNode>> list1=newline.GetLineNodesList(
       // data1 = newline.GetLayerLine(null,ETrainType.动车CRH2A,50);
        
        foreach (TrainLine item in data1)
        {
            Response.Write(item);
            Response.Write("(" + item.TotalMiles + "公里)");
            Response.Write("<br>");
        }
        base.OnLoad(e);
    }
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
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
