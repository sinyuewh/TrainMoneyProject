<%@ Page Title="选择线路ID" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" 
AutoEventWireup="true" CodeBehind="SelectTrainLine.aspx.cs" 
Inherits="WebSite.TrainWeb.Train.SelectTrainLine" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="BusinessRule" %>
<%@ Register Src="~/Include/PageNavigator1.ascx" TagName="PageNavigator1" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language ="javascript" type="text/javascript">
        function getSelectLineID() {
            var sel = "";
            var v1 = document.getElementsByName("seldocument");
            for (i = 0; i < v1.length; i++) {
                var item = v1[i];
                if (item.checked) {
                    if (sel == "") {
                        sel = item.value;
                    }
                    else {
                        sel = sel + "," + item.value;
                    }
                }
            }

            var parentid = '<%=Request.QueryString["parent"] %>';
            if (parentid != "" && window.opener != null) {
                var oldValue=window.opener.document.getElementById(parentid).value;
                if (oldValue == "") {
                    window.opener.document.getElementById(parentid).value = sel;
                }
                else {
                    window.opener.document.getElementById(parentid).value = sel+","+oldValue
                }
            }
            window.close();
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
                                                    <span class="STYLE3" style="display:none">你当前的位置：</span>选择指定的线路
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="54%">
                                       
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
                                    <td class="Caption" style="width: 15%">
                                        线路代码：
                                    </td>
                                    <td class="Data" style="width: 15%">
                                        <jasp:JTextBox ID="LineID" runat="server" Width="100" AutoFillValue="true" SearchExpress="LineID={0}"
                                            DataType="Integer" Caption="线路代码"></jasp:JTextBox>
                                    </td>
                                    <td class="Caption" style="width: 15%">
                                        线路名称：
                                    </td>
                                    <td class="Data" style="width: 15%">
                                        <jasp:JTextBox ID="LineName" AutoFillValue="true" Width="100" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Caption" style="width: 10%">
                                        起点：
                                    </td>
                                    <td class="Data" style="width: 15%">
                                        <jasp:JTextBox ID="AStation" runat="server" Width="100" AutoFillValue="true"></jasp:JTextBox>
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
                                                    <SearchPara SearchControlList="LineID,LineName,AStation,BStation" />
                                                </jasp:JButton>&nbsp;
                                                <jasp:JButton ID="JButton1" runat="server" Text="重置" ToolTip="重置列表数据" DataSourceID="Data1"
                                                    JButtonType="SearchButton">
                                                    <SearchPara SearchControlList="LineID,LineName,AStation,BStation" IsCancelSearch="true" />
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
                                        选中
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
                                        备注
                                    </td>
                                    
                                </tr>
                                <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="TRAINLINE" PageSize="15"
                                    OrderBy="num">
                                </jasp:JDataSource>
                                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Data1" EnableViewState="false">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <input id="seldocument" name="seldocument"  
                                                type="checkbox" value='<%#Eval("LineID")%>' />
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
                                                <%#(BusinessRule.ELineType)(int.Parse(Eval("LineType").ToString()))%>
                                            </td>
                                            
                                            <td class="Data">
                                                <%#Eval("Remark") %>
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
        <tr>
            <td height="35" style="text-align:center">
                <asp:Button ID="Button2" runat="server" Text="确定返回"  OnClientClick="javascript:getSelectLineID();return false;"  />
            </td>
        </tr>
    </table>
</asp:Content>
