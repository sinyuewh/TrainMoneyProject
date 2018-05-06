<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="StationQuickList.aspx.cs" Inherits="WebSite.TrainWeb.ParamSet.StationQuickList" %>

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
    <script src="/Common/JsLib/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="/Common/JsLib/jquery.autocomplete.js" type="text/javascript"></script>
    
    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
        $("#<%=STATIONNAME.ClientID %>").autocomplete("/Handler/GetStationNameValue.ashx?AutoKind=GetName",
                {
                    width: $(this).css("width"),
                    selectFirst: false,
                    max: 10,
                    scroll: false,
                    extraParams: { 'txtValue': function() {
                    return $("#<%=STATIONNAME.ClientID %>").val();
                    }
                    }
                });
        });
    </script>
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
                                    <td  valign="middle">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="5%">
                                                    <div align="center">
                                                        <img src="../images/tb.gif" width="16" height="16" /></div>
                                                </td>
                                                <td width="95%" class="STYLE1">
                                                    <span class="STYLE3" style="display:none">你当前的位置：</span>财务数据模型-站点快捷列表
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
                                                                    <a href="StationQuickDetail.aspx?STATIONNAME=-1">新增</a></div>
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
                <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="STATIONINFO" PageSize="15"
                    OrderBy="STATIONNAME">
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
                                    <td class="Data" style="text-align:right">
                                       &nbsp;  &nbsp;  &nbsp;  &nbsp;
                                        车站：<jasp:JTextBox ID="STATIONNAME" runat="server" Width="100" AutoFillValue="true"></jasp:JTextBox>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <jasp:JButton ID="butSearch" runat="server" Text="过滤" JButtonType="SearchButton" DataSourceID="Data1">
                                            <SearchPara SearchControlList="STATIONNAME" />
                                        </jasp:JButton>
                                        &nbsp;&nbsp;<jasp:JButton ID="JButton1" runat="server" Text="重置" JButtonType="SearchButton" DataSourceID="Data1">
                                            <SearchPara SearchControlList="STATIONNAME" IsCancelSearch="true" />
                                        </jasp:JButton>
                                 
                                    </td>
                                </tr>
                            </table>
                       
                            <!--数据列表区-->
                            <table class="ListTable" cellspacing="1" cellpadding="0" onmouseover="changeto()"
                                onmouseout="changeback()">
                                <tr>
                                    <td width="33%" class="Caption">
                                        站名
                                    </td>
                                    <td class="Caption">
                                        站名缩写
                                    </td>
                                    <td class="Caption">
                                        站名全拼
                                    </td>
                                    <td class="Caption">
                                        基本操作
                                    </td>
                                </tr>
                                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Data1">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <%#Eval("STATIONNAME")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("ABBNAME")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("WHOLESPELL")%>
                                            </td>
                                            <td class="Data">
                                                <a href='StationQuickDetail.aspx?STATIONNAME=<%#Eval("STATIONNAME")%>'><img src="../images/edt.gif" width="16" height="16" />明细</a> &nbsp;
                                                <jasp:JLinkButton ID="butDel" AuthorityID="001" runat="server"
                                                       OnClientClick="javascript:return confirm('警告：确定要删除数据吗？');"
                                                       JButtonType="SimpleAction"
                                                       runat="server" CommandArgument='<%#Eval("STATIONNAME")%>' 
                                                       Text='<img src="../images/del.gif"
                                                        width="16" height="16"  />删除'>
                                                    <SimpleActionPara TableName="STATIONINFO" ButtonType="DeleteData" />
                                                    <ParaItems>
                                                        <jasp:ParameterItem ParaName="STATIONNAME" ParaType="CommandArgument" />
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
                            <asp:Button ID="Button1" runat="server" Text="导入数据" Visible="false" />
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
