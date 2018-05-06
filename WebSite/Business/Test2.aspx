<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test2.aspx.cs" Inherits="WebSite.Business.Test2" %>
<%@ Import Namespace="BusinessRule" %>
<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.Button1.Click += new EventHandler(Button1_Click);
    }

    void Button1_Click(object sender, EventArgs e)
    {
        this.Cal();
    }

    protected override void OnLoad(EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            String[] str1=HighTrainBU.GetTrainType();
            this.TrainType.DataSource = str1;
            this.TrainType.DataBind();
        }
        base.OnLoad(e);
    }

    private void Cal()
    {
        HighTrainBU bu1 = new HighTrainBU();
        bu1.TrainType = this.TrainType.SelectedValue;
        bu1.YuXingLiCheng = int.Parse(this.YunXingLiCheng.Text);
        double Fee0=bu1.GetTotalFee();

        this.SR1.Text = String.Format("{0:C2}", Fee0, 2);
        this.SR2.Text = String.Format("{0:C2}", Fee0, 2);
        this.SR0.Text = String.Format("{0:C2}", Fee0, 2);
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
      <table style="width:80%; margin-top:10px; margin-left:10px" border="1">
        <tr>
            <td>列车名称：
            </td>
            <td><asp:TextBox ID="TrainName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>车种类型：
            </td>
            <td>
                <asp:DropDownList ID="TrainType" runat="server">
                    
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>运行里程：
            </td>
            <td><asp:TextBox ID="YunXingLiCheng" runat="server" Text="0"></asp:TextBox>（公里）&nbsp;区段里程：<asp:Label
                    ID="YunXingLiCheng1" runat="server" Text="0" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        
        <tr>
            <td>单次收入（不含保险）：
            </td>
            <td>
                <asp:Label ID="SR1" runat="server" Text="0"></asp:Label>（元）
            </td>
        </tr>
        
        <tr>
            <td>单次收入（含保险）：
            </td>
            <td>
                <asp:Label ID="SR2" runat="server" Text="0"></asp:Label>（元）<br />
                保险费的比例为2%
            </td>
        </tr>
        
        <tr>
            <td>年累计收入：
            </td>
            <td>
                <asp:Label ID="SR0" runat="server" Text="0"></asp:Label>（元）
            </td>
        </tr>
      </table>
<br />
        <asp:Button ID="Button1" runat="server" Text="开始计算"  style="margin-left:10px" />
    </div>
    </form>
</body>
</html>
