<%@ Page Title="站点别名" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="StationAliasList.aspx.cs" Inherits="WebSite.TrainWeb.ParamSet.StationAliasList" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="BusinessRule" %>
<%@ Register Src="~/Include/PageNavigator1.ascx" TagName="PageNavigator1" TagPrefix="uc1" %>

<script runat="server">
   
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="/Common/JsLib/PubLib.js" type="text/javascript"></script>

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
                                                    <span class="STYLE3" style="display:none">你当前的位置：</span>财务数据模型-站点别名
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="54%">
                                        <jasp:SecurityPanel ID="sec1" runat="server" AuthorityID="001">
                                            <table border="0" align="right" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="60">
                                                        <table width="90%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="STYLE1">
                                                                    <div align="center">
                                                                        <img src="../images/22.gif" width="14" height="14" /></div>
                                                                </td>
                                                                <td class="STYLE1">
                                                                    <div align="center">
                                                                        <a href="StationAliasDetail.aspx?TRAINNAME=-1">新增</a></div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="52" id="td1" runat="server" visible="false">
                                                        <table width="88%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="STYLE1">
                                                                    <div align="center">
                                                                        <img src="../images/11.gif" width="14" height="14" /></div>
                                                                </td>
                                                                <td class="STYLE1">
                                                                    <div align="center">
                                                                        删除</div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </jasp:SecurityPanel>
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
                            <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="TRAINALIAS" PageSize="15"
                                OrderBy="num">
                            </jasp:JDataSource>
                            <asp:UpdatePanel ID="update1" runat="server">
                                <ContentTemplate>
                                    <table class="ListTable" cellspacing="1" cellpadding="0" onmouseover="changeto()"
                                        onmouseout="changeback()">
                                        <tr>
                                            <td width="3%" class="Caption">
                                                <input type="checkbox" name="checkbox" value="checkbox" onclick="selectAllDocument('selDocumentID',this.checked);" />
                                            </td>
                                            <td width="3%" class="Caption">
                                                序号
                                            </td>
                                            <td class="Caption">
                                                站点别名
                                            </td>
                                            <td class="Caption">
                                                相关站点
                                            </td>
                                            <td class="Caption">
                                                基本操作
                                            </td>
                                        </tr>
                                        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Data1">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="Data">
                                                        <input type="checkbox" name="selDocumentID" id="selDocumentID" value='<%#Eval("TrainName")%>' />
                                                    </td>
                                                    <td class="Data">
                                                        <%#Eval("num")%>
                                                    </td>
                                                    <td class="Data">
                                                        <%#Eval("TrainName")%>
                                                    </td>
                                                    <td class="Data">
                                                        <%#Eval("TRAINALIAS")%>
                                                    </td>
                                                    <td class="Data">
                                                        <a href='StationAliasDetail.aspx?TRAINNAME=<%#Server.UrlEncode(Eval("TRAINNAME").ToString()) %>'>
                                                            <img src="../images/edt.gif" width="16" height="16" />明细</a> &nbsp;
                                                        <jasp:JLinkButton ID="delData"  AuthorityID="001" JButtonType="SimpleAction" CommandArgument='<%#Eval("TRAINNAME")%>'
                                                            OnClientClick="return confirm('警告：确定要删除数据吗？')" runat="server" Text='<img src="../images/del.gif"
                                                        width="16" height="16" />删除'>
                                                            <SimpleActionPara TableName="TRAINALIAS" ButtonType="DeleteData" />
                                                            <ParaItems>
                                                                <jasp:ParameterItem ParaName="TRAINNAME" ParaType="CommandArgument" />
                                                            </ParaItems>
                                                            <ExecutePara SuccUrl="#" />
                                                        </jasp:JLinkButton>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
