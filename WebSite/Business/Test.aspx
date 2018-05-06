<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="WebSite.Business.Test" %>
<%@ Import Namespace="BusinessRule" %>
<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        this.Button1.Click += new EventHandler(Button1_Click);
        this.TrainType.SelectedIndexChanged += new EventHandler(TrainType_SelectedIndexChanged);
        base.OnInit(e);
    }

    void TrainType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.TrainType.SelectedValue == "0")
        {
            this.KongTiao.Checked = false;
        }
        else
        {
            this.KongTiao.Checked = true;
        }
        //this.Cal();
    }

    void Button1_Click(object sender, EventArgs e)
    {
        this.Cal();
    }

    private void Cal()
    {
        
        CommTrainBU bu1 = new CommTrainBU();
        bu1.TrainName = this.TrainName.Text;
        bu1.TrainType = this.TrainType.SelectedValue;
        bu1.YuXingLiCheng = int.Parse(this.YunXingLiCheng.Text);
        double Fee1 = bu1.GetYinZuoFee();


        bu1.YinZuo = int.Parse(this.YinZuo.Text);
        bu1.RuanZuo = int.Parse(this.RuanZuo.Text);
        bu1.OpenYinWo = int.Parse(this.YinWo1.Text);
        bu1.CloseYinWo = int.Parse(this.YinWo2.Text);
        bu1.RuanWo = int.Parse(this.RuanWo1.Text);
        bu1.AdvanceRuanWo = int.Parse(this.RuanWo2.Text);
        bu1.CanChe = int.Parse(this.CanCe.Text);
        bu1.FaDianChe = int.Parse(this.FaDianChe.Text);
        bu1.ShuYinChe = int.Parse(this.ShuYinChe.Text);
        
        if (bu1.CheckBianZhu())
        {

            Fee1 = Fee1 * bu1.GetFeeRate();


            //设置加快
            if (this.JiaKuai.SelectedValue != String.Empty)
            {
                bu1.JiaKuai = (ECommJiaKuai)(int.Parse(this.JiaKuai.SelectedValue));
                Fee1 = Fee1 * bu1.GetJiaKuaiFee();
            }

            //设置空调费
            if (this.KongTiao.Checked)
            {
                Fee1 = Fee1 * bu1.GetKongTiaoFee();
            }

            //设置席别增减费
            if (int.Parse(this.XieBieZhengJia.Text) > 0)
            {
                Fee1 = Fee1 * (1 + int.Parse(this.XieBieZhengJia.Text) / 100d);
            }

            //设置显示的值
            this.SR1.Text = String.Format("{0:C2}", Fee1, 2);
            this.SR2.Text = String.Format("{0:C2}", Fee1 * (1 + 0.02), 2);
            this.SR0.Text = String.Format("{0:C2}", Fee1 * (1 + 0.02) * 365 * 2);

            this.YunXingLiCheng1.Text = bu1.GetQuDuanLiCheng(int.Parse(this.YunXingLiCheng.Text)) + "";
        }
        else
        {
            WebFrame.Util.JAjax.Alert("错误：列车的编组之和应为18！");
        }
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
                <asp:DropDownList ID="TrainType" runat="server" AutoPostBack="true">
                    <asp:ListItem Text="绿皮车25B" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="空调车25K" Value="1"></asp:ListItem>
                    <asp:ListItem Text="空调车25G" Value="2"></asp:ListItem>
                    <asp:ListItem Text="空调车25T" Value="3"></asp:ListItem>
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
            <td>列车编组：<br />（注：合计数应为18）
            </td>
            <td>
                <table style="width:60%">
                    <tr>
                        <td>硬座：</td>
                        <td><asp:TextBox ID="YinZuo" runat="server" Text="0"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>软座：</td>
                        <td><asp:TextBox ID="RuanZuo" runat="server" Text="0"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>开放式硬卧：</td>
                        <td><asp:TextBox ID="YinWo1" runat="server" Text="0"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>包房式硬卧：</td>
                        <td><asp:TextBox ID="YinWo2" runat="server" Text="0"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>软卧：</td>
                        <td><asp:TextBox ID="RuanWo1" runat="server" Text="0"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>高级软卧：</td>
                        <td><asp:TextBox ID="RuanWo2" runat="server" Text="0"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>餐车：</td>
                        <td><asp:TextBox ID="CanCe" runat="server" Text="0"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>发电车：</td>
                        <td><asp:TextBox ID="FaDianChe" runat="server" Text="0"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>宿营车：</td>
                        <td><asp:TextBox ID="ShuYinChe" runat="server" Text="0"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
        </tr>
        
        <tr>
            <td>列车加快：
            </td>
            <td>
                <asp:DropDownList ID="JiaKuai" runat="server">
                    <asp:ListItem Text="无" Value="3" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="加快" Value="0" ></asp:ListItem>
                    <asp:ListItem Text="快速" Value="1"></asp:ListItem>
                    <asp:ListItem Text="特快附加" Value="2"></asp:ListItem>
                </asp:DropDownList>
                <br />
                加快的票价增加比例为20%，快速为40%，特快附加为60%
            </td>
        </tr>
        
        <tr>
            <td>空调：
            </td>
            <td>
                <asp:CheckBox ID="KongTiao" runat="server" Enabled ="false" />（空调的票价增减比例为25%）
            </td>
        </tr>
        
        <tr>
            <td>席别增减系数：
            </td>
            <td><asp:TextBox ID="XieBieZhengJia" runat="server" Text="0"></asp:TextBox>（%）
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
