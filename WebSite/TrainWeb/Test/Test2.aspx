<%@ Page Language="C#" AutoEventWireup="true"  %>
<%@ Import  Namespace="BusinessRule" %>

<script runat="server">
    protected override void OnLoad(EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            TrainShouRu1 shour1 = new TrainShouRu1(1345, true, EJiaKuai.特快);
            Response.Write("客车："+shour1 + "<br>");

            TrainShouRu2 shour2 = new TrainShouRu2(1879,EJiaKuai.特快);
            Response.Write("新型空调车："+shour2 + "<br>");
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
