<%@ Page Language="C#" AutoEventWireup="true"  %>
<%@ Import  Namespace="BusinessRule" %>

<script runat="server">
    protected override void OnLoad(EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           double price1= DTrainShouRu.GetTrainPrice(EHighTrainType.CRH2A, 
               EDTrainFareType.二等软座, 1205,0);

           Response.Write("CRH2A的二等软座的票价为" + price1);
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
