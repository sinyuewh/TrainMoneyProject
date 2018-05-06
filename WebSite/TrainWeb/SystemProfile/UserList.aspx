<%@ Page Title="用户列表" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="UserList.aspx.cs" Inherits="WebSite.TrainWeb.SystemProfile.UserList" %>

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
                                                    <span class="STYLE3" style="display:none">你当前的位置：</span>系统配置-用户列表
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
                                                                        <a href="UserEdit.aspx?UserName=-1">新增</a>
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
                            <!--数据列表区-->
                            <table class="ListTable" cellspacing="1" cellpadding="0" onmouseover="changeto()"
                                onmouseout="changeback()">
                                <tr>
                                    <td width="3%" class="Caption">
                                        序号
                                    </td>
                                    <td width="15%" class="Caption">
                                        用户名
                                    </td>
                                    <td width="14%" class="Caption">
                                        管理员
                                    </td>
                                    <td width="18%" class="Caption">
                                        最近登录
                                    </td>
                                    <td width="15%" class="Caption">
                                        基本操作
                                    </td>
                                </tr>
                                <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="MyUserName" PageSize="-1"
                                    OrderBy="num">
                                    <ParaItems>
                                        <jasp:ParameterItem  ParaName="UserName<>'admin'"  ParaType="String" DataName="$$EMPTY" />
                                    </ParaItems>
                                </jasp:JDataSource>
                                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Data1">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <%#Eval("num")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("UserName")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("isAdmin").ToString()=="1" ? "是":"否" %>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("LastLogin")%>
                                            </td>
                                            <td class="Data">
                                                <a href='UserEdit.aspx?UserName=<%# Server.UrlEncode(Eval("UserName").ToString())%>'>
                                                    <img src="../images/edt.gif" width="16" height="16" />明细</a> 
                                                    <jasp:JLinkButton ID="link1" AuthorityID="001" runat="server"
                                                     CommandArgument='<%#Eval("UserName")%>'
                                                     OnClientClick="javascript:return confirm('提示：确定要删除用户吗？');" 
                                                    Text='<img src="../images/del.gif"
                                                        width="16" height="16" />删除'></jasp:JLinkButton>
                                                    
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
                            &nbsp;<uc1:PageNavigator1 ID="PageNavigator11" runat="server" DataSourceID="Data1"
                                Visible="false" />
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
