<%@ Page Title="浏览支出项明细" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master"
    AutoEventWireup="true" CodeBehind="ShowZhiChuMingXi.aspx.cs" 
    Inherits="WebSite.TrainWeb.Fenxi.ShowZhiChuMingXi" %>

<%@ Import Namespace="BusinessRule" %>
<%@ Import Namespace="System.Collections.Generic" %>

<script runat="server">
    protected override void OnLoad(EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            List<ZhiChuData> data1 = new List<ZhiChuData>();
            String[] zc1 = Request.QueryString["zc"].Split(',');
            int i = 1;
            double sum = 0;
            foreach (String m in zc1)
            {
                double d1 = double.Parse(m);
                data1.Add(new ZhiChuData(i, "", d1 / 10000));
                if (i != zc1.Length)
                {
                    sum = sum + d1;
                }
                i++;
            }

            this.Repeater1.DataSource = data1;
            this.Repeater1.DataBind();

            this.labSum.Text = String.Format("{0:n2}", sum / 10000);
        }
        base.OnLoad(e);
    }
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin: 20 20 20 20; line-height: 250%">
        <table class="ListTable" cellspacing="1" cellpadding="0" style="width: 90%">
            <tr>
                <td class="Caption" style="text-align: center; width: 50">
                    序号
                </td>
                <td class="Caption">
                    支出项
                </td>
                <td class="Caption" style="text-align: right">
                    支出金额(万)
                </td>
            </tr>
            <asp:Repeater ID="Repeater1" runat="server" EnableViewState="false">
                <ItemTemplate>
                    <tr>
                        <td class="Data" style="text-align: center">
                            <%# Container.ItemIndex+1 %>
                        </td>
                        <td class="Data">
                            <%# (EFeeKind)(((ZhiChuData)(Container.DataItem)).ID)%>
                        </td>
                        <td class="Data" style="text-align: right">
                            <%# String.Format("{0:n2}", 
                                (((ZhiChuData)(Container.DataItem)).ZhiChu))%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td class="Data" style="text-align: center">
                </td>
                <td class="Data">
                    <b>合计</b>(不含局内线路使用费)
                </td>
                <td class="Data" style="text-align: right">
                    <asp:Label ID="labSum" runat="server" Text="0"></asp:Label>
                </td>
            </tr>
        </table>
        <div style="text-align: center; margin-top: 10px">
            <asp:Button ID="button1" Text="关 闭" runat="server" OnClientClick="javascript:window.close(); return false;" />
        </div>
    </div>
</asp:Content>
