<%@ Page Title="客运长交路机车牵引费配置" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master"
    AutoEventWireup="true" CodeBehind="ChangJiaoFeeList.aspx.cs" Inherits="WebSite.TrainWeb.Train.ChangJiaoFeeList" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="BusinessRule" %>
<%@ Register Src="~/Include/PageNavigator1.ascx" TagName="PageNavigator1" TagPrefix="uc1" %>

<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }
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
                                                    <span class="STYLE3" style="display: none">你当前的位置：</span>财务数据模型-客运长交路机车牵引费配置
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
                                                                        <jasp:SecurityPanel ID="pane1" runat="server" AuthorityID="001">
                                                                            <a href="ChangJiaoFeeEdit.aspx?LineID=-1">新增</a></jasp:SecurityPanel>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="52" id="del" runat="server" visible="false">
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
                <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="CHANGJIAOQYFEE"
                    PageSize="15" OrderBy="num">
                </jasp:JDataSource>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="8" background="../images/tab_12.gif">
                            &nbsp;
                        </td>
                        <td>
                            <!--数据查询区-->
                            <table class="ListTable" cellspacing="1" cellpadding="0">
                                <tr>
                                    <td class="Data" style="text-align: right">
                                        &nbsp; &nbsp; 线别：<jasp:JTextBox ID="LineName" runat="server" Width="200" AutoFillValue="true"></jasp:JTextBox>
                                        &nbsp;&nbsp;交路：<jasp:JTextBox ID="JiaoLu" runat="server" Width="200" AutoFillValue="true"></jasp:JTextBox>
                                        &nbsp;&nbsp;<jasp:JButton ID="butSearch" runat="server" Text="过滤" JButtonType="SearchButton"
                                            DataSourceID="Data1">
                                            <SearchPara SearchControlList="LineName,JiaoLu" />
                                        </jasp:JButton>
                                        <jasp:JButton ID="JButton1" runat="server" Text="重置" JButtonType="SearchButton" DataSourceID="Data1">
                                            <SearchPara SearchControlList="LineName,JiaoLu" IsCancelSearch="true" />
                                        </jasp:JButton>
                                        <jasp:JButton ID="JButton2" runat="server" Text="导入数据">
                                        </jasp:JButton>
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
                                    <td class="Caption" style ="width:30%">
                                        线别
                                    </td>
                                    <td class="Caption" style ="width:30%">
                                        交路
                                    </td>
                                    <td class="Caption">
                                        内燃牵引费
                                    </td>
                                    <td class="Caption">
                                        电力牵引费
                                    </td>
                                    <td class="Caption">
                                        已验证
                                    </td>
                                    <td class="Caption">
                                        基本操作
                                    </td>
                                </tr>
                                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Data1">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <%#Eval("num")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("LineName")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("JiaoLu")%>
                                            </td>
                                            <td class="Data">
                                                <%#int.Parse(Eval("Fee1").ToString()) == 0 ? "" : Eval("Fee1").ToString() %>
                                            </td>
                                            <td class="Data">
                                                <%#int.Parse(Eval("Fee2").ToString()) == 0 ? "" : Eval("Fee2").ToString() %>
                                            </td>
                                            <td class="Data">
                                                <%# Eval("checkflag").ToString() == "" ? "×" : "√"%>
                                            </td>
                                            <td class="Data">
                                                <a href='ChangJiaoFeeEdit.aspx?LineID=<%#Eval("LineID")%>'>
                                                    <img src="../images/edt.gif" width="16" height="16" />明细</a> &nbsp;
                                                <jasp:JLinkButton ID="butDel" AuthorityID="001" runat="server" OnClientClick="javascript:return confirm('警告：确定要删除数据吗？');"
                                                    JButtonType="BusinessRule" CommandArgument='<%#Eval("LineID")%>' Text='<img src="../images/del.gif" width="16" height="16"  />删除'>
                                                    
                                                    <AssemblePara AssembleFile="BusinessRule.dll" 
                                                       ClassLibName="BusinessRule.ChangJiaoFeeBU" MethodName="DeleteData" />
                                                    <ParaItems>
                                                        <jasp:ParameterItem ParaName="num" ParaType="CommandArgument" />
                                                    </ParaItems>
                                                    <ExecutePara  SuccUrl="#" />
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
