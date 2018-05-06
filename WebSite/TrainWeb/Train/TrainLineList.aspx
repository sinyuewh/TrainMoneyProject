<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="TrainLineList.aspx.cs" Inherits="TrainWebSite.TrainLineList" %>

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
        function DeleteData(lineid1) {
            if (confirm("警告：确定要删除列车线路的数据吗？")) {
                TrainWebSite.TrainLineList.DeleteLine(lineid1 + "");
                document.location.reload();
            }
        }

        function EditStation(lineid1) {
            var url1 = "EditStation.aspx?LineID=" + lineid1;
            location.href = url1;
        }

        function ChangeStatus(lineid1) {
            if (confirm("警告：确定要更换“夏冬线路切换标识” 吗?")) {
                TrainWebSite.TrainLineList.ChangeSpringAndWinterStatus(lineid1 + "");
                document.location.reload();
            }
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

    <script language="javascript" type="text/javascript">
        //将数据导入到系统
        function ImportDataToSystem() {
            var url1 = "ImportDataTool.aspx?kind=3";
            myOpenURL(url1, 400, 600);
        }

        function ExportDataToExl() {
            alert(1);
        }
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
                                                    <span class="STYLE3" style="display: none">你当前的位置：</span>财务数据模型-列车线路
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
                                                                        <a href="TrainLineDetail.aspx?LineID=-1" title="新增列车线路数据">新增</a></div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="100">
                                                        <table width="90%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="STYLE1">
                                                                    <div align="center">
                                                                        <img src="../images/22.gif" width="14" height="14" /></div>
                                                                </td>
                                                                <td class="STYLE1">
                                                                    <div align="right">
                                                                        <a href="javascript:ImportDataToSystem();" title="导入线路的站点数据">导入线路站点</a></div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="100">
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="STYLE1">
                                                                    <div align="center">
                                                                        <img src="../images/xls.png" width="14" height="14" /></div>
                                                                </td>
                                                                <td class="STYLE1" style="text-align: right">
                                                                    <asp:LinkButton ID="butExportData" runat="server" ToolTip="导出线路的站点数据" Text="导出线路站点"></asp:LinkButton>
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
                            <table class="DetailTable" cellspacing="1" cellpadding="0" style="width: 100%">
                                <tr>
                                    <td class="Caption" style="width: 10%">
                                        线路代码：
                                    </td>
                                    <td class="Data" style="width: 13%">
                                        <jasp:JTextBox ID="LineID" runat="server" Width="120" AutoFillValue="true" SearchExpress="LineID={0}"
                                            DataType="Integer" Caption="线路代码"></jasp:JTextBox>
                                    </td>
                                    <td class="Caption" style="width: 10%">
                                        线路名称：
                                    </td>
                                    <td class="Data" style="width: 13%">
                                        <jasp:JTextBox ID="LineName" AutoFillValue="true" Width="120" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Caption" style="width: 10%">
                                        类别：
                                    </td>
                                    <td class="Data" style="width: 13%">
                                        <jasp:AppDropDownList ID="LineType" runat="server" AutoFillValue="true">
                                            <asp:ListItem Text="不限" Value=""></asp:ListItem>
                                            <asp:ListItem Text="特一类" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="特二类" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="一类上浮" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="一类" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="二类上浮" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="二类" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="三类" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="三类下浮" Value="8"></asp:ListItem>
                                        </jasp:AppDropDownList>
                                    </td>
                                    <td class="Caption" style="width: 10%">
                                        起点：
                                    </td>
                                    <td class="Data" style="width: 13%">
                                        <jasp:JTextBox ID="AStation" runat="server" Width="120" AutoFillValue="true"></jasp:JTextBox>
                                    </td>
                                    <td class="Caption" style="width: 10%">
                                        讫点：
                                    </td>
                                    <td class="Data" style="width: 21%">
                                        <asp:UpdatePanel ID="update1" runat="server">
                                            <ContentTemplate>
                                                <jasp:JTextBox ID="BStation" runat="server" AutoFillValue="true" Width="120"></jasp:JTextBox>
                                                &nbsp;&nbsp;<jasp:JButton ID="Button1" runat="server" Text="查询" IsValidatorData="true"
                                                    DataSourceID="Data1" ControlList="LineID" JButtonType="SearchButton">
                                                    <SearchPara SearchControlList="LineID,LineName,AStation,BStation,LineType" />
                                                </jasp:JButton>&nbsp;
                                                <jasp:JButton ID="JButton1" runat="server" Text="重置" ToolTip="重置列表数据" DataSourceID="Data1"
                                                    JButtonType="SearchButton">
                                                    <SearchPara SearchControlList="LineID,LineName,AStation,BStation,LineType" IsCancelSearch="true" />
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
                                        <jasp:JLinkButton ID="JLinkButton4" runat="server" Text="序号" DataSourceID="Data1"
                                            JButtonType="OrderButton" Font-Bold="true" ToolTip="按序号排序">
                                            <SortPara  SortExpress="num" />
                                        </jasp:JLinkButton>
                                    </td>
                                    <td class="Caption">
                                        <jasp:JLinkButton ID="link1" runat="server" Text="线路代码" DataSourceID="Data1" JButtonType="OrderButton"
                                            Font-Bold="true" ToolTip="按线路代码排序">
                                            <SortPara  SortExpress="LineID" />
                                        </jasp:JLinkButton>
                                    </td>
                                    <td class="Caption">
                                        <jasp:JLinkButton ID="JLinkButton1" runat="server" Text="线路名称" DataSourceID="Data1"
                                            JButtonType="OrderButton" Font-Bold="true" ToolTip="按线路名称排序">
                                            <SortPara  SortExpress="LineName" />
                                        </jasp:JLinkButton>
                                    </td>
                                    <td class="Caption">
                                        <jasp:JLinkButton ID="JLinkButton2" runat="server" Text="起点" DataSourceID="Data1"
                                            JButtonType="OrderButton" Font-Bold="true" ToolTip="按起点排序">
                                            <SortPara  SortExpress="AStation" />
                                        </jasp:JLinkButton>
                                    </td>
                                    <td class="Caption">
                                        <jasp:JLinkButton ID="JLinkButton3" runat="server" Text="讫点" DataSourceID="Data1"
                                            JButtonType="OrderButton" Font-Bold="true" ToolTip="按讫点排序">
                                            <SortPara  SortExpress="BSTATION" />
                                        </jasp:JLinkButton>
                                    </td>
                                    <td class="Caption">
                                        长度(km)
                                    </td>
                                    <td class="Caption">
                                        类别
                                    </td>
                                    <td class="Caption">
                                        线路站点
                                    </td>
                                    <td class="Caption">
                                        电气化
                                    </td>
                                    <td class="Caption" style ="width:170px">
                                        基本操作
                                    </td>
                                </tr>
                                <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="TRAINLINE" PageSize="15"
                                    OrderBy="num">
                                </jasp:JDataSource>
                                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Data1" EnableViewState="false">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <%#Eval("num")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("LineID")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("LineName")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("AStation")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("BStation")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("Miles")%>
                                            </td>
                                            <td class="Data">
                                                <asp:Label ID="LineType" runat="server" Text='<%#(BusinessRule.ELineType)(int.Parse(Eval("LineType").ToString()))%>'></asp:Label>
                                                <asp:Label ID="SpringWinter" runat="server" Visible ="false" Text='<%#Eval("SpringWinter")%>'></asp:Label>
                                            </td>
                                            <td class="Data">
                                                <asp:Label ID="lab1" runat="server" Text='<%#String.Format("[{0}]", BusinessRule.Line.GetLineStationCount(Eval("LineID").ToString()))%>'
                                                    ToolTip='<%# BusinessRule.Line.GetLineStationText(Eval("LineID").ToString())%>'></asp:Label>
                                                <asp:HyperLink ID="hyp1" runat="server" NavigateUrl='<%#String.Format("EditStation.aspx?LineID={0}",Eval("LineID")) %>'
                                                    Text='[设置]' ForeColor="Blue"></asp:HyperLink>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("dqh") %>
                                            </td>
                                            <td class="Data" style ="width:170px">
                                                <a href='TrainLineDetail.aspx?LineID=<%#Eval("LineID")%>' title="更新线路基本信息">
                                                    <img src="../images/edt.gif" width="16" height="16" />明细</a>&nbsp;
                                                <jasp:JLinkButton ID="link1" runat="server" AuthorityID="001" OnClientClick='<%#String.Format("javascript:DeleteData({0});return false;",Eval("lineID"))%>'
                                                    Text='<img src="../images/del.gif" width="16" height="16" />删除'>
                                                </jasp:JLinkButton>
                                                <jasp:JLinkButton ID="butExchange" runat="server"  Visible="false" OnClientClick='<%#String.Format("javascript:ChangeStatus({0});return false;",Eval("lineID"))%>'
                                                    AuthorityID="001" Text='<img src="../images/22.gif" width="16" height="16" />切换'>
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
                            <asp:Button ID="butImport" runat="server" Text="导入数据" Visible="false" />
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
