<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="BianZhuFenXi.aspx.cs" Inherits="WebSite.TrainWeb.Fenxi.BianZhuFenXi" %>

<%@ Import Namespace="BusinessRule" %>

<script runat="server">
    
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="30" background="/TrainWeb/images/tab_05.gif">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="12" height="30">
                            <img src="/TrainWeb/images/tab_03.gif" width="12" height="30" />
                        </td>
                        <td>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="46%" valign="middle">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="5%">
                                                    <div align="center">
                                                        <img src="/TrainWeb/images/tb.gif" width="16" height="16" /></div>
                                                </td>
                                                <td width="95%" class="STYLE1">
                                                    <span class="STYLE3" style="display:none">你当前的位置：</span>财务数据模型-编组比较分析
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="54%">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="16">
                            <img src="/TrainWeb/images/tab_07.gif" width="16" height="30" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="8" background="/TrainWeb/images/tab_12.gif">
                            &nbsp;
                        </td>
                        <td style="height: 600px; vertical-align: top">
                            <table class="DetailTable" cellspacing="1" cellpadding="0" style="width: 99%;">
                                <tr>
                                    <td class="Caption" style="width: 15%">
                                        车类型和线路选择
                                    </td>
                                    <td class="Data" style="width: 85%">
                                        <asp:Label ID="traintype" runat="server"></asp:Label>
                                        <br />
                                        <asp:Label ID="line" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table class="ListTable" cellspacing="1" cellpadding="0" style="width: 99%; margin-top: 10px">
                                <tr>
                                    <td colspan="3" class="Caption">
                                        列车编组
                                    </td>
                                    <td class="Caption" rowspan="2" style="text-align: right">
                                        收入(万元)
                                    </td>
                                    <td class="Caption" rowspan="2" style="text-align: right">
                                        支出(万元)
                                    </td>
                                    <td class="Caption" rowspan="2" style="text-align: right">
                                        收支差(万元)
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Caption">
                                        硬座
                                    </td>
                                    <td class="Caption">
                                        硬卧
                                    </td>
                                    <td class="Caption">
                                        软卧
                                    </td>
                                </tr>
                                <% if (false)
                                   { %>
                                <tr>
                                    <td class="Data">
                                        <asp:TextBox ID="yz1" runat="server" Width="80"></asp:TextBox>
                                    </td>
                                    <td class="Data">
                                        <asp:TextBox ID="yw1" runat="server" Width="80"></asp:TextBox>
                                    </td>
                                    <td class="Data">
                                        <asp:TextBox ID="rw1" runat="server" Width="80"></asp:TextBox>
                                    </td>
                                    <td class="Data" style="text-align: right">
                                        <asp:Label ID="sr1" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="Data" style="text-align: right">
                                        <asp:Label ID="zc1" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="Data" style="text-align: right">
                                        <asp:Label ID="sz1" runat="server" Text="0" Font-Bold="true" ForeColor="Green"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data">
                                        <asp:TextBox ID="yz2" runat="server" Width="80">8</asp:TextBox>
                                    </td>
                                    <td class="Data">
                                        <asp:TextBox ID="yw2" runat="server" Width="80">8</asp:TextBox>
                                    </td>
                                    <td class="Data">
                                        <asp:TextBox ID="rw2" runat="server" Width="80">1</asp:TextBox>
                                    </td>
                                    <td class="Data" style="text-align: right">
                                        <asp:Label ID="sr2" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="Data" style="text-align: right">
                                        <asp:Label ID="zc2" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="Data" style="text-align: right">
                                        <asp:Label ID="sz2" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data">
                                        <asp:TextBox ID="yz3" runat="server" Width="80">6</asp:TextBox>
                                    </td>
                                    <td class="Data">
                                        <asp:TextBox ID="yw3" runat="server" Width="80">8</asp:TextBox>
                                    </td>
                                    <td class="Data">
                                        <asp:TextBox ID="rw3" runat="server" Width="80">3</asp:TextBox>
                                    </td>
                                    <td class="Data" style="text-align: right">
                                        <asp:Label ID="sr3" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="Data" style="text-align: right">
                                        <asp:Label ID="zc3" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="Data" style="text-align: right">
                                        <asp:Label ID="sz3" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data">
                                        <asp:TextBox ID="yz4" runat="server" Width="80">3</asp:TextBox>
                                    </td>
                                    <td class="Data">
                                        <asp:TextBox ID="yw4" runat="server" Width="80">8</asp:TextBox>
                                    </td>
                                    <td class="Data">
                                        <asp:TextBox ID="rw4" runat="server" Width="80">7</asp:TextBox>
                                    </td>
                                    <td class="Data" style="text-align: right">
                                        <asp:Label ID="sr4" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="Data" style="text-align: right">
                                        <asp:Label ID="zc4" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="Data" style="text-align: right">
                                        <asp:Label ID="sz4" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data">
                                        <asp:TextBox ID="yz5" runat="server" Width="80">0</asp:TextBox>
                                    </td>
                                    <td class="Data">
                                        <asp:TextBox ID="yw5" runat="server" Width="80">9</asp:TextBox>
                                    </td>
                                    <td class="Data">
                                        <asp:TextBox ID="rw5" runat="server" Width="80">8</asp:TextBox>
                                    </td>
                                    <td class="Data" style="text-align: right">
                                        <asp:Label ID="sr5" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="Data" style="text-align: right">
                                        <asp:Label ID="zc5" runat="server" Text="0"></asp:Label>
                                    </td>
                                    <td class="Data" style="text-align: right">
                                        <asp:Label ID="sz5" runat="server" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <% } %>
                                <asp:Repeater ID="repeater1" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <asp:TextBox ID="yz1" runat="server" Width="80" Text='<%#Eval("yz")%>'></asp:TextBox>
                                            </td>
                                            <td class="Data">
                                                <asp:TextBox ID="yw1" runat="server" Width="80" Text='<%#Eval("yw")%>'></asp:TextBox>
                                            </td>
                                            <td class="Data">
                                                <asp:TextBox ID="rw1" runat="server" Width="80" Text='<%#Eval("rw")%>'></asp:TextBox>
                                            </td>
                                            <td class="Data" style="text-align: right">
                                                <asp:Label ID="sr1" runat="server" Text='<%#Eval("sr","{0:n2}")%>'></asp:Label>
                                            </td>
                                            <td class="Data" style="text-align: right">
                                                <asp:Label ID="zc1" runat="server" Text='<%#Eval("zc","{0:n2}")%>'></asp:Label>
                                            </td>
                                            <td class="Data" style="text-align: right">
                                                <asp:Label ID="sz1" runat="server" Text='<%#Eval("sz","{0:n2}")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <br />
                            <div style="text-align: center">
                                <input id="Button2" type="button" value="返 回" onclick="history.go(-1);" class="btn" />
                            </div>
                        </td>
                        <td width="8" background="/TrainWeb/images/tab_15.gif">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
