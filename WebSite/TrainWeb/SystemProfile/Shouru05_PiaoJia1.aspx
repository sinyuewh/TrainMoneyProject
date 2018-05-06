<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="Shouru05_PiaoJia1.aspx.cs" Inherits="WebSite.TrainWeb.SystemProfile.Back.Shouru05_PiaoJia1" %>

<%@ Register Src="~/Include/PageNavigator1.ascx" TagName="PageNavigator1" TagPrefix="uc1" %>

<script runat="server">
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="/Common/JsLib/PubLib.js" type="text/javascript"></script>

    <style type="text/css">
        .style1
        {
            height: 18px;
        }
        .Caption
        {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="30" background="../images/tab_05.gif">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="12" height="30">
                            <img src="../images/tab_03.gif" width="12" height="30" />
                        </td>
                        <td>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="46%" valign="middle">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="5%">
                                                    <div align="center">
                                                        <img src="../images/tb.gif" width="16" height="16" /></div>
                                                </td>
                                                <td width="95%" class="STYLE1">
                                                    <span class="STYLE3" style="display: none">你当前的位置：</span>系统配置-普通列车票价参考表
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="54%" style="text-align: right">
                                        <jasp:JButton ID="JButton2" runat="server" Text="成批更新" AuthorityID="001" OnClientClick="return confirm('提示：确定要成批更新票据数据吗？');">
                                        </jasp:JButton>
                                        <jasp:JButton ID="JButton1" runat="server" Text="返 回" JButtonType="RedirectButton">
                                            <ExecutePara SuccUrl="Shouru_Profile.aspx" />
                                        </jasp:JButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="16">
                            <img src="../images/tab_07.gif" width="16" height="30" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="8" background="../images/tab_12.gif">
                            &nbsp;
                        </td>
                        <td>
                            <!--数据列表区-->
                            <table class="ListTable" cellspacing="1" cellpadding="0" onmouseover="changeto()"
                                onmouseout="changeback()">
                                <tr>
                                    <td class="Caption" rowspan="2">
                                        序号
                                    </td>
                                    <td class="Caption" rowspan="2">
                                        里程(公里)
                                    </td>
                                    <td class="Caption" rowspan="2">
                                        硬座
                                    </td>
                                    <td class="Caption" rowspan="2">
                                        软座
                                    </td>
                                    <td class="Caption" colspan="2">
                                        加快
                                    </td>
                                    <td class="Caption" colspan="3">
                                        硬卧
                                    </td>
                                    <td class="Caption" colspan="2">
                                        软卧
                                    </td>
                                    <td class="Caption" rowspan="2">
                                        空调费
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Caption">
                                        普快
                                    </td>
                                    <td class="Caption">
                                        特快
                                    </td>
                                    <td class="Caption">
                                        上铺
                                    </td>
                                    <td class="Caption">
                                        中铺
                                    </td>
                                    <td class="Caption">
                                        下铺
                                    </td>
                                    <td class="Caption">
                                        上铺
                                    </td>
                                    <td class="Caption">
                                        下铺
                                    </td>
                                </tr>
                                <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="ticketprice" PageSize="13"
                                    OrderBy="num">
                                    <ParaItems>
                                        <jasp:ParameterItem ParaName="traintype='1'" ParaType="String" DataName="$$empty" />
                                    </ParaItems>
                                </jasp:JDataSource>
                                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Data1">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <jasp:JLabel ID="NUM" runat="server" Text=' <%#Eval("num") %>'></jasp:JLabel>
                                            </td>
                                            <td class="Data">
                                                <jasp:JLabel ID="StartMile" runat="server" Text=' <%#Eval("StartMile") %>'></jasp:JLabel>
                                                -
                                                <jasp:JLabel ID="EndMile" runat="server" Text=' <%#Eval("EndMile") %>'></jasp:JLabel>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="YZPRICE" runat="server" Text='<%#Eval("YZPRICE","{0:n2}")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="RZPRICE" runat="server" Text='<%#Eval("RZPRICE","{0:n2}")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="PTJKPRICE" runat="server" Text='<%#Eval("PTJKPRICE","{0:n2}")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="KSJKPRICE" runat="server" Text='<%#Eval("KSJKPRICE","{0:n2}")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="YWSPRICE" runat="server" Text='<%#Eval("YWSPRICE","{0:n2}")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="YWZPRICE" runat="server" Text='<%#Eval("YWZPRICE","{0:n2}")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="YWXPRICE" runat="server" Text='<%#Eval("YWXPRICE","{0:n2}")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="RWSPRICE" runat="server" Text='<%#Eval("RWSPRICE","{0:n2}")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="RWXPRICE" runat="server" Text='<%#Eval("RWXPRICE","{0:n2}")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="KDPRICE" runat="server" Text='<%#Eval("KDPRICE","{0:n2}")%>'></jasp:JTextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                        <td width="8" background="../images/tab_15.gif">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="35" background="../images/tab_19.gif">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="12" height="35">
                            <img src="../images/tab_18.gif" width="12" height="35" />
                        </td>
                        <td>
                            <uc1:PageNavigator1 ID="PageNavigator11" runat="server" DataSourceID="Data1" />
                        </td>
                        <td width="16">
                            <img src="../images/tab_20.gif" width="16" height="35" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
