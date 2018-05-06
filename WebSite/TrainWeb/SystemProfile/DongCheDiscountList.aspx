<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" 
AutoEventWireup="true" CodeBehind="DongCheDiscountList.aspx.cs" Inherits="WebSite.TrainWeb.SystemProfile.DongCheDiscountList" %>
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
                                                    <span class="STYLE3" style="display:none">你当前的位置：</span>系统配置-动车票价折扣配置表
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
                                                                        <a href="DongCheDiscountEdit.aspx?num=-1">新增</a>
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
                                    <td class="Caption">
                                        序号
                                    </td>
                                    <td class="Caption">
                                        里程
                                    </td>
                                    <td class="Caption">
                                        既有线折扣(%)
                                    </td>
                                    <td class="Caption">
                                        200公里线路折扣(%)
                                    </td>
                                    <td class="Caption">
                                        300公里线路折扣(%)
                                    </td>
                                    <td width="15%" class="Caption">
                                        操作
                                    </td>
                                </tr>
                                <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="HIGHTRAINPRICERATE" PageSize="-1"
                                    OrderBy="LICHENG">
                                    
                                </jasp:JDataSource>
                                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Data1">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <%#Eval("num")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("licheng")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("rate1")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("rate2")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("rate3")%>
                                            </td>
                                            <td class="Data">
                                                <a href='DongCheDiscountEdit.aspx?num=<%# Server.UrlEncode(Eval("num").ToString())%>'>
                                                    <img src="../images/edt.gif" width="16" height="16" />明细</a> 
                                                    <jasp:JLinkButton ID="link1" AuthorityID="001" runat="server"
                                                     CommandArgument='<%#Eval("num")%>' JButtonType="SimpleAction"
                                                     OnClientClick="javascript:return confirm('提示：确定要删除数据吗？');" 
                                                    Text='<img src="../images/del.gif"
                                                        width="16" height="16" />删除'>
                                                          <SimpleActionPara TableName="HIGHTRAINPRICERATE" ButtonType="DeleteData" />
                                                          <ParaItems>
                                                             <jasp:ParameterItem ParaName="num" ParaType="CommandArgument" />
                                                          </ParaItems>
                                                          <ExecutePara SuccUrl="#" FailInfo="提示：提交数据操作失败！" />
                                                        </jasp:JLinkButton>
                                                    
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

