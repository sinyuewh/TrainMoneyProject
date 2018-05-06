<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test3.aspx.cs" Inherits="WebSite.Business.Test3" %>

<%@ Import Namespace="BusinessRule" %>

<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        this.Button1.Click += new EventHandler(Button1_Click);
        base.OnInit(e);
    }

    void Button1_Click(object sender, EventArgs e)
    {
        Cal();
    }

    private void Cal()
    {
        CommTrainBU bu1 = new CommTrainBU();
        foreach (RepeaterItem item1 in this.Repeater1.Items)
        {
            Label lab1 = item1.FindControl("LineType") as Label;
            TextBox txt1 = item1.FindControl("LineMile") as TextBox;
            if (lab1 != null)
            {
                int index1 = int.Parse(lab1.Text.ToLower().Replace("line", ""));
                bu1.SetLineMile(index1, int.Parse(txt1.Text));
            }
        }

        if (this.TrainType.SelectedValue == "0")
        {
            bu1.KongTiaoFee = false;
        }
        else
        {
            bu1.KongTiaoFee = true;
        }

        cost1.Text = String.Format("{0:C}", bu1.GetLineCost());

        //计算列车牵引费
        int index = 0;
        foreach (RepeaterItem item1 in this.Repeater2.Items)
        {
            TextBox txt1 = item1.FindControl("ZuoWei") as TextBox;
            if (txt1 != null)
            {
                bu1.SetCheXianCount((ECommCheXian)index,int.Parse(txt1.Text));
                index++;
            }
        }
        bu1.QianYinType = (EQianYinType)int.Parse(this.QianYinType.SelectedValue);
        bu1.GongDianType = (EGongDianType)int.Parse(this.GongDianType.SelectedValue);
        bu1.YuXingLiCheng = int.Parse(this.TrainLiCheng.Text);
        double Fee2=bu1.GetQianYinCost();
        cost2.Text = String.Format("{0:C}", Fee2);

        //计算售票服务费
        
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
        <table style="width: 80%; margin-top: 10px; margin-left: 10px" border="1">
            <tr>
                <td>
                    列车名称：
                </td>
                <td>
                    <asp:TextBox ID="TrainName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    运行里程：
                </td>
                <td>
                    <asp:TextBox ID="TrainLiCheng" runat="server" Text="0"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    车种类型：
                </td>
                <td>
                    <asp:DropDownList ID="TrainType" runat="server">
                        <asp:ListItem Text="绿皮车25B" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="空调车25K" Value="1"></asp:ListItem>
                        <asp:ListItem Text="空调车25G" Value="2"></asp:ListItem>
                        <asp:ListItem Text="空调车25T" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    线路分级（公里）：<br />
                </td>
                <td>
                    <jasp:JDataSource ID="Data1" JType="Table" runat="server" SqlID="LineProfile" OrderBy="ID"
                        PageSize="-1">
                    </jasp:JDataSource>
                    <table style="width: 60%">
                        <tr>
                            <td>
                                局内：
                            </td>
                            <td>
                                <asp:TextBox ID="Line0" runat="server" Text="0"></asp:TextBox>
                            </td>
                        </tr>
                        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Data1">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#Eval("LineName")%>：<asp:Label ID="LineType" runat="server" Visible="false" Text='<%#Eval("LineType")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="LineMile" runat="server" Text="0"></asp:TextBox>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    线路使用费：<asp:Label ID="cost1" runat="server" Text="0" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    牵引类型：
                </td>
                <td>
                    <asp:DropDownList ID="QianYinType" runat="server">
                        <asp:ListItem Text="内燃机车" Value="0"></asp:ListItem>
                        <asp:ListItem Text="电力机车" Value="1" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    供电类型：
                </td>
                <td>
                    <asp:DropDownList ID="GongDianType" runat="server">
                        <asp:ListItem Text="直供电" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="非直供电" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    列车编组：<br />
                    （注：合计数应为18）
                </td>
                <td>
                    <jasp:JDataSource ID="Data2" runat="server" JType="Table" OrderBy="CHEXIANTYPE" PageSize="-1"
                        SqlID="CHEXIANWEIGHTPROFILE">
                    </jasp:JDataSource>
                    <table style="width: 60%">
                        <asp:Repeater ID="Repeater2" runat="server" DataSourceID="Data2">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#Eval("CHEXIANNAME")%>：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ZuoWei" runat="server" Text="0"></asp:TextBox>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        
                    </table>
                    列车牵引费用：<asp:Label ID="cost2" runat="server" Text="0" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            
            <tr>
                <td>
                    售票服务费比例：
                </td>
                <td>
                    <asp:TextBox ID="SaleRate" runat="server" Text="1"></asp:TextBox>(%)
                </td>
            </tr>
        </table>
        <br />
        <asp:Button ID="Button1" runat="server" Text="开始计算" Style="margin-left: 10px" />
    </div>
    </form>
</body>
</html>
