<%@ Page Title="列车经济效益排行榜" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master"
    AutoEventWireup="true" CodeBehind="OldCheCiFenxiPaiHang.aspx.cs" Inherits="WebSite.TrainWeb.Fenxi.OldCheCiFenxiPaiHang" %>

<%@ Import Namespace="BusinessRule" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Register Src="../../Include/WaitResult.ascx" TagName="WaitResult" TagPrefix="uc1" %>

<script runat="server">
    
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="/Common/JsLib/PubLib.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function GoKindFenxi(kind1) {
            var y1 = document.getElementById("<%=this.byear.ClientID %>").value;
            var m1 = document.getElementById("<%=this.bmonth.ClientID %>").value;
            var url1 = "OldChengChiFenXiByMingXi.aspx?kind=" + escape(kind1) + "&byear=" + y1 + "&bmonth=" + m1;
            location.href = url1;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="cursor: default">
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
                                                    你当前的位置：既有车次排行榜
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="54%">
                                        <table border="0" align="right" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td width="60">
                                                    <table width="90%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td class="STYLE1">
                                                                <div align="center">
                                                                </div>
                                                            </td>
                                                            <td class="STYLE1">
                                                                <div align="center">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
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
                            <!--数据查询区-->
                            <table class="DetailTable" cellspacing="1" cellpadding="0" style="width: 98%">
                                <tr>
                                    <td class="Caption" style="width: 10%">
                                        年份：
                                    </td>
                                    <td class="Data" style="width: 10%">
                                        <asp:TextBox ID="byear" runat="server" Width="100"></asp:TextBox>
                                    </td>
                                    <td class="Caption" style="width: 10%">
                                        月份：
                                    </td>
                                    <td class="Data" style="width: 10%">
                                        <asp:DropDownList ID="bmonth" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="Caption" style="width: 10%">
                                        排行指标：
                                    </td>
                                    <td class="Data" style="width: 10%">
                                        <asp:DropDownList ID="paihangkind" runat="server">
                                            <asp:ListItem Text="收入" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="支出" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="收支差" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="上座率" Value="3"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="Caption" style="width: 10%">
                                        方向：
                                    </td>
                                    <td class="Data" style="width: 10%">
                                        <asp:DropDownList ID="direction" runat="server">
                                            <asp:ListItem Text="最好" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="最差" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="Caption" style="width: 10%">
                                        数量：
                                    </td>
                                    <td class="Data" style="width: 10%">
                                        <asp:TextBox ID="pcount" runat="server" Width="100" Text="15"></asp:TextBox>
                                    </td>
                                    <td class="Data" style="width: 10%; text-align: center">
                                        <asp:Button ID="Button1" runat="server" ToolTip="点按钮开始排行" Text="开始排行" />
                                    </td>
                                </tr>
                            </table>
                            <!--数据列表区-->
                            <div id="SearchInfo" runat="server" visible="false">
                                <table class="ListTable" cellspacing="1" id="SearchDataTable" cellpadding="0" style="width: 98%;
                                    margin-top: 10px">
                                    <tr>
                                        <td class="Caption">
                                            排名
                                        </td>
                                        <td class="Caption">
                                            类别
                                        </td>
                                        <td class="Caption">
                                            车次
                                        </td>
                                        <td class="Caption">
                                            <asp:Label ID="zhibiao" runat ="server" Text =""></asp:Label>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="Repeater1" runat="server" EnableViewState="true">
                                        <ItemTemplate>
                                            <!--显示的数据1-->
                                            <tr>
                                                <td class="Data">
                                                    <%#Container.ItemIndex+1 %>
                                                </td>
                                                <td class="Data">
                                                    <%#Eval("traintype") %>
                                                </td>
                                                <td class="Data">
                                                     <%#Eval("trainname") %>
                                                </td>
                                                <td class="Data">
                                                     <%#Eval("zhibiaovalue") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                            <!--Report Data-->
                        </td>
                        <td width="8" background="../images/tab_15.gif">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
