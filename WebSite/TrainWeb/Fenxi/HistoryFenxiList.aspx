<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="HistoryFenxiList.aspx.cs" Inherits="WebSite.TrainWeb.Fenxi.HistoryFenxiList" %>

<%@ Register Src="~/Include/PageNavigator1.ascx" TagName="PageNavigator1" TagPrefix="uc1" %>

<script runat="server">
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="/Common/JsLib/PubLib.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
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
                                                            <span class="STYLE3" style="display: none">你当前的位置：</span>数据分析-保存的车次分析
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td width="54%">
                                                <table border="0" align="right" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td width="60">
                                                            <jasp:SecurityPanel runat="server" ID="security1" AuthorityID="001">
                                                                <table width="90%" border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td class="STYLE1">
                                                                            <div align="center">
                                                                                <img src="../images/22.gif" width="14" height="14" /></div>
                                                                        </td>
                                                                        <td class="STYLE1">
                                                                            <div align="center">
                                                                                &nbsp;
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </jasp:SecurityPanel>
                                                        </td>
                                                    </tr>
                                                </table>
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
                                    <!--查询区域-->
                                    <table class="DetailTable" cellspacing="1" cellpadding="0" style="width: 100%">
                                        <tr>
                                            <td class="Caption" style="width: 10%">
                                                起点：
                                            </td>
                                            <td class="Data" style="width: 13%">
                                                <jasp:JTextBox ID="astation" runat="server" Width="120" AutoFillValue="true" Caption="起点"></jasp:JTextBox>
                                            </td>
                                            <td class="Caption" style="width: 10%">
                                                终点：
                                            </td>
                                            <td class="Data" style="width: 60%">
                                                <jasp:JTextBox ID="bstation" runat="server" Width="120" AutoFillValue="true" Caption="终点"></jasp:JTextBox>
                                            </td>
                                            <td class="Data" style="width: 21%">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        &nbsp;&nbsp;<jasp:JButton ID="Button1" runat="server" Text="查询" IsValidatorData="true"
                                                            DataSourceID="Data1" JButtonType="SearchButton">
                                                            <SearchPara SearchControlList="astation,bstation" />
                                                        </jasp:JButton>&nbsp;
                                                        <jasp:JButton ID="JButton1" runat="server" Text="重置" ToolTip="重置列表数据" DataSourceID="Data1"
                                                            JButtonType="SearchButton">
                                                            <SearchPara SearchControlList="astation,bstation" IsCancelSearch="true" />
                                                        </jasp:JButton>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                    <!--数据列表区-->
                                    <table class="ListTable" cellspacing="1" cellpadding="0" onmouseover="changeto()"
                                        onmouseout="changeback()">
                                        <tr>
                                            <td class="Caption">
                                                序号
                                            </td>
                                            <td class="Caption">
                                                起点
                                            </td>
                                            <td class="Caption">
                                                终点
                                            </td>
                                            <td class="Caption">
                                                分段搜索
                                            </td>
                                            <td class="Caption">
                                                深度
                                            </td>
                                            <td class="Caption">
                                                更新
                                            </td>
                                            <td width="15%" class="Caption">
                                                操作
                                            </td>
                                        </tr>
                                        <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="SEARCHOBJECTLIST"
                                            PageSize="15" OrderBy="savetime desc">
                                        </jasp:JDataSource>
                                        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Data1">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="Data">
                                                        <%#Container.ItemIndex+1 %>
                                                    </td>
                                                    <td class="Data">
                                                        <%#Eval("astation")%>
                                                    </td>
                                                    <td class="Data">
                                                        <%#Eval("bstation")%>
                                                    </td>
                                                    <td class="Data">
                                                        <%#Eval("FENGDUAN") %>
                                                    </td>
                                                    <td class="Data">
                                                        <%#Eval("shendu")%>
                                                    </td>
                                                    <td class="Data">
                                                        <%#Eval("savetime")%>
                                                    </td>
                                                    <td class="Data">
                                                        <a href='NewCheCiFenxi.aspx?num=<%# Server.UrlEncode(Eval("num").ToString())%>'>
                                                            <img src="../images/edt.gif" width="16" height="16" title="浏览分析结果" />浏览结果</a>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
