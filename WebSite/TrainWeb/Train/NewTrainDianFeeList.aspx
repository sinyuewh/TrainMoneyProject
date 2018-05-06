<%@ Page Title="电网和接触网使用费" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master"
  AutoEventWireup="true" CodeBehind="NewTrainDianFeeList.aspx.cs" Inherits="WebSite.TrainWeb.Train.NewTrainDianFeeList" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="BusinessRule" %>
<%@ Register Src="~/Include/PageNavigator1.ascx" TagName="PageNavigator1" TagPrefix="uc1" %>

<script runat="server">
    
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="/Common/JsLib/PubLib.js" type="text/javascript"></script>
    <script src="/Common/JsLib/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="/Common/JsLib/jquery.autocomplete.js" type="text/javascript"></script>
    
    <script language="javascript" type="text/javascript">
        //将数据导入到系统
        function ImportDataToSystem() {
            var url1 = "ImportDataTool.aspx?kind=4";
            myOpenURL(url1, 400, 600);
        }

        $(document).ready(function() {
            $("#<%=AStation.ClientID %>").autocomplete("/Handler/GetStationNameValue.ashx?AutoKind=GetName",
                {
                    width: $(this).css("width"),
                    selectFirst: false,
                    max: 10,
                    scroll: false,
                    extraParams: { 'txtValue': function() {
                        return $("#<%=AStation.ClientID %>").val();
                    }
                    }
                });

            $("#<%=BStation.ClientID %>").autocomplete("/Handler/GetStationNameValue.ashx?AutoKind=GetName",
            {
                width: $(this).css("width"),
                selectFirst: false,
                max: 10,
                scroll: false,
                extraParams: { 'txtValue': function() {
                    return $("#<%=BStation.ClientID %>").val();
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
                                    <td width="46%" valign="middle">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="5%">
                                                    <div align="center">
                                                        <img src="../images/tb.gif" width="16" height="16" /></div>
                                                </td>
                                                <td width="95%" class="STYLE1">
                                                    <span class="STYLE3" style="display:none">你当前的位置：</span>财务数据模型-电费和接触网使用费
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="54%">
                                        <jasp:SecurityPanel ID="sec1" runat="server" AuthorityID="001">
                                            <table border="0" align="right" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="200" style="text-align: right">
                                                        <table width="90%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="STYLE1">
                                                                    <div align="center">
                                                                        <img src="../images/22.gif" width="14" height="14" /></div>
                                                                </td>
                                                                <td class="STYLE1">
                                                                    <div align="center">
                                                                        <a href="javascript:ImportDataToSystem();">导入数据</a></div>
                                                                </td>
                                                                <td class="STYLE1">
                                                                    <div align="center">
                                                                        <img src="../images/22.gif" width="14" height="14" /></div>
                                                                </td>
                                                                <td class="STYLE1">
                                                                    <div align="center">
                                                                        <asp:LinkButton ID="linkMerge" runat="server" Text="合并数据" OnClientClick="javascript:return confirm('提示：合并数据可能要花费较多的时间，确定执行吗？');"></asp:LinkButton>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="52" id="tdDel" runat="server" visible="false">
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
                <jasp:JDataSource ID="Data1" runat="server" JType="Sql" SqlID="select NewTrainDianFee.*,Fee1+Fee2+Fee3 Fee0,zl1+zl2+zl3 zl0 from NewTrainDianFee"
                    PageSize="15" OrderBy="byear desc,bmonth desc,num">
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
                                        年份：<jasp:JTextBox ID="byear" runat="server" Width="100" AutoFillValue="true" SearchExpress="byear={0}"></jasp:JTextBox>
                                        &nbsp;&nbsp;
                                        车次：<jasp:JTextBox ID="TrainName" runat="server" Width="100" AutoFillValue="true"></jasp:JTextBox>
                                        &nbsp;&nbsp;始发站：<jasp:JTextBox ID="AStation" runat="server" Width="100" AutoFillValue="true"></jasp:JTextBox>
                                        &nbsp;&nbsp;终到站：<jasp:JTextBox ID="BStation" runat="server" Width="100" AutoFillValue="true"></jasp:JTextBox>
                                        &nbsp;&nbsp;<jasp:JButton ID="butSearch" runat="server" Text="过滤" JButtonType="SearchButton"
                                            DataSourceID="Data1">
                                            <SearchPara SearchControlList="byear,TrainName,AStation,BStation" />
                                        </jasp:JButton>
                                        &nbsp;&nbsp;<jasp:JButton ID="JButton1" runat="server" Text="重置" JButtonType="SearchButton"
                                            DataSourceID="Data1">
                                            <SearchPara SearchControlList="byear,TrainName,AStation,BStation" IsCancelSearch="true" />
                                        </jasp:JButton>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <!--数据列表区-->
                            <table class="ListTable" cellspacing="1" cellpadding="0" onmouseover="changeto()"
                                onmouseout="changeback()">
                                <tr>
                                    <td width="3%" class="Caption">
                                        <input type="checkbox" name="checkbox" value="checkbox" onclick="selectAllDocument('selDocumentID',this.checked);" />
                                    </td>
                                    <td class="Caption">
                                        年月
                                    </td>
                                    <td class="Caption">
                                        车次
                                    </td>
                                    <td class="Caption">
                                        始发站
                                    </td>
                                    <td class="Caption">
                                        终到站
                                    </td>
                                    <td class="Caption" style="text-align: right; padding-left: 5px">
                                        万总重吨公里
                                    </td>
                                    <td class="Caption" style="text-align: right; padding-left: 5px">
                                        电费和接触网使用费
                                    </td>
                                    <td class="Caption">
                                        基本操作
                                    </td>
                                </tr>
                                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Data1" EnableViewState="false">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <input type="checkbox" name="selDocumentID" id="selDocumentID" value='<%#Eval("num")%>' />
                                            </td>
                                            <td class="Data">
                                                <%#Eval("byear")%>-<%#Eval("bmonth")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("TrainName")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("AStation")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("BStation")%>
                                            </td>
                                            <td class="Data" style="text-align: right; padding-left: 5px">
                                                <%#Eval("zl0")%>
                                            </td>
                                            <td class="Data" style="text-align: right; padding-left: 5px">
                                                <%#Eval("Fee0","{0:n2}")%>
                                            </td>
                                            <td class="Data">
                                                <a href='NewTrainDianFeeDetail.aspx?num=<%#Eval("num")%>'>
                                                    <img src="../images/edt.gif" width="16" height="16" />明细</a>
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
