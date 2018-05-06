<%@ Page Title="" Language="C#" MasterPageFile="~/Designer/HtglMain.Master" 
AutoEventWireup="true" %>

<script runat="server">
    protected override void OnLoad(EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.connString.Text = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        }
        base.OnLoad(e);
    }

    protected override void OnInit(EventArgs e)
    {
        but1.Click += new EventHandler(but1_Click);
        but2.Click += new EventHandler(but2_Click);
        base.OnInit(e);
    }

    //解密
    void but2_Click(object sender, EventArgs e)
    {
        this.connString.Text = WebFrame.Util.JString.EnString(WebFrame.Util.JString.GetHexToString(this.connString.Text));
    }

    //加密
    void but1_Click(object sender, EventArgs e)
    {
        this.connString.Text = WebFrame.Util.JString.GetStringToHex(WebFrame.Util.JString.EnString(this.connString.Text));
    }
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:UpdatePanel ID="update1" runat="server">
            <ContentTemplate>
                <table class="InfoTips">
                    <tr>
                        <td>
                            <img src="../images/ld1.gif" />
                            <b>连接字符串</b>
                        </td>
                        <td style="text-align: right">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="Spar">
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="1" class="DetailTable">
                    <tr>
                        <td class="Caption">
                            连接字符串:
                        </td>
                        <td class="Data"  style="width:580px">
                            <jasp:JTextBox ID="connString" runat="server" TextMode="MultiLine" Height="250" Width="500" />
                        </td>
                    </tr>
                </table>
                <div class="ButtonArea">
                    <jasp:JButton ID="but1" runat="server" Text="加密" JButtonType="NoJButton" IsValidatorData="true">
                        <ExecutePara SuccInfo="提示：操作成功！" FailInfo="提示：操作失败！" />
                    </jasp:JButton>
                    &nbsp;&nbsp;
                    <jasp:JButton ID="but2" runat="server" Text="解密" JButtonType="NoJButton" IsValidatorData="true">
                        <ExecutePara SuccInfo="提示：操作成功！" FailInfo="提示：操作失败！" />
                    </jasp:JButton>
                </div>
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
