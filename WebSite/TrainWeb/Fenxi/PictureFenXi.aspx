<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="PictureFenXi.aspx.cs" Inherits="WebSite.TrainWeb.Fenxi.PictureFenXi" %>

<script runat="server">
    protected override void OnLoad(EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            String TrainID = Request.QueryString["TrainID"];
            int byear = int.Parse(Request.QueryString["byear"]);
            double[] data1 = null;
            double[] data2 = null;
            BusinessRule.NewTrainBU.GetTrainMoneyByYear(TrainID, byear, out data1, out data2);
            int kd = (int)(Math.Ceiling(data2[data2.Length - 1] * 1.1)) / 10;

            if (data2 != null && data2.Length > 0 && data2[data2.Length - 1] > 0)
            {

            }
            else
            {
                this.info.Visible = false;
                this.info0.Visible = true;
                this.info2.Visible = false;
            }
        }
        //WebSite.AppCode.JPicture.CreateImage3();
        base.OnLoad(e);
    }
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="margin-top:20; margin-left:20">
        <tr>
            <td>
                <div id="info0" runat="server" visible="false">
                    提示：没有图形数据显示结果，请点按钮返回！&nbsp;
                    <asp:Button ID="Button1" runat="server" Text="返回上一页" OnClientClick="javascript:history.back(); return false;" />
                </div>
                <div id="info" runat="server">
                    <img src='Picture1.aspx?byear=<%=Request.QueryString["byear"]%>&TrainID=<%=Request.QueryString["TrainID"]%>' />
                </div>
            </td>
            <td style="vertical-align: top" width="100%">
                <div id="info2" runat="server">
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="Button2" runat="server" Text="返回上一页" OnClientClick="javascript:history.back(); return false;" />
                </div><br /> <table width="100%">
     <tr><td align="left"><asp:Label ID="FeeTitle" runat="server" ForeColor="Blue" Font-Size="13"></asp:Label></td></tr>
            <tr>
            <td  style="text-align:left">单位（万元）
            </td></tr>
     </table>
    <!--数据列表区-->
    <table class="ListTable" cellspacing="1" cellpadding="0" onmouseover="changeto()"
        onmouseout="changeback()">
        <tr>
            <td width="12%" class="Caption">
                收支类别
            </td>
            <td class="Caption">
                1月
            </td>
            <td class="Caption">
                2月
            </td>
            <td class="Caption">
                3月
            </td>
            <td class="Caption">
                4月
            </td>
             <td class="Caption">
                5月
            </td>
            <td class="Caption">
                6月
            </td>
            <td class="Caption">
                7月
            </td>
             <td class="Caption">
                8月
            </td>
            <td class="Caption">
                9月
            </td>
            <td class="Caption">
                10月
            </td>
            <td class="Caption">
                11月
            </td>
            <td class="Caption">
                12月
            </td>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td class="Data">
                       <asp:Label ID="FeeKind" runat="server" Text='<%#Eval("FeeKind")%>'></asp:Label>
                    </td>
                    <td class="Data">
                       <asp:Label ID="Label1" runat="server" Text='<%#Eval("1月")%>'></asp:Label>
                    </td>
                    <td class="Data">
                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("2月")%>'></asp:Label>
                    </td>
                    <td class="Data">
                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("3月")%>'></asp:Label>
                    </td>
                    <td class="Data">
                       <asp:Label ID="Label4" runat="server" Text='<%#Eval("4月")%>'></asp:Label>
                    </td>
                    <td class="Data">
                        <asp:Label ID="Label5" runat="server" Text='<%#Eval("5月")%>'></asp:Label>
                    </td>
                    <td class="Data">
                       <asp:Label ID="Label6" runat="server" Text='<%#Eval("6月")%>'></asp:Label>
                    </td>
                    <td class="Data">
                        <asp:Label ID="Label7" runat="server" Text='<%#Eval("7月")%>'></asp:Label>
                    </td>
                     <td class="Data">
                        <asp:Label ID="Label8" runat="server" Text='<%#Eval("8月")%>'></asp:Label>
                    </td>
                    <td class="Data">
                       <asp:Label ID="Label9" runat="server" Text='<%#Eval("9月")%>'></asp:Label>
                    </td>
                    <td class="Data">
                       <asp:Label ID="Label10" runat="server" Text='<%#Eval("10月")%>'></asp:Label>
                    </td>
                    <td class="Data">
                       <asp:Label ID="Label11" runat="server" Text='<%#Eval("11月")%>'></asp:Label>
                    </td>
                     <td class="Data">
                       <asp:Label ID="Label12" runat="server"  Text='<%#Eval("12月")%>'></asp:Label>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
            </td>
        </tr>
    </table>
    
</asp:Content>
