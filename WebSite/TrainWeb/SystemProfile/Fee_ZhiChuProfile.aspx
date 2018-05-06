<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="Fee_ZhiChuProfile.aspx.cs" Inherits="WebSite.TrainWeb.SystemProfile.ZhiChuProfile" %>

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
                                                    <span class="STYLE3" style="display:none">你当前的位置：</span>系统配置-支出相关配置
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
                                        <input type="checkbox" name="checkbox" value="checkbox" onclick="selectAllDocument('selDocumentID',this.checked);" />
                                    </td>
                                    <td width="3%" class="Caption">
                                        序号
                                    </td>
                                    <td width="15%" class="Caption">
                                        配置说明
                                    </td>
                                    
                                    <td width="15%" class="Caption">
                                        相关操作
                                    </td>
                                </tr>
                                <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="PROFILEGROUP" PageSize="-1"
                                    OrderBy="num">
                                    <ParaItems>
                                        <jasp:ParameterItem ParaName="kind='1'"  ParaType="String"  DataName="$$empty" />
                                    </ParaItems>
                                </jasp:JDataSource>
                                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Data1" EnableViewState="false">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <input type="checkbox" name="selDocumentID" id="selDocumentID" value="checkbox" />
                                            </td>
                                            <td class="Data">
                                                <%#Container.ItemIndex+1 %>
                                            </td>
                                            <td class="Data_Left">
                                                <%#Eval("remark")%>
                                            </td>
                                            
                                            <td class="Data">
                                                <a href='<%#Eval("url")%>'>
                                                    <img src="../images/edt.gif" width="16" height="16" />配置</a>
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
</asp:Content>
