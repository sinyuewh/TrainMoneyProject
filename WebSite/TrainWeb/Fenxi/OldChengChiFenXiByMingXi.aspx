<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true" CodeBehind="OldChengChiFenXiByMingXi.aspx.cs" Inherits="WebSite.TrainWeb.Fenxi.OldChengChiFenXiByMingXi" %>
<%@ Import Namespace="BusinessRule" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Register Src="../../Include/WaitResult.ascx" TagName="WaitResult" TagPrefix="uc1" %>

<script runat="server">
    
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="/Common/JsLib/PubLib.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function GoKindFenxi(train1) {
            var y1 = document.getElementById("<%=this.byear.ClientID %>").value;
            var m1 = document.getElementById("<%=this.bmonth.ClientID %>").value;
            var url1 = "PictureFenXi.aspx?TrainID=" + escape(train1) + "&byear=" + y1 + "&bmonth=" + m1;
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
                                                    你当前的位置：既有车次分类明细统计
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
                                    <td class="Data" style="width: 20%">
                                        &nbsp;<asp:TextBox ID="byear" runat="server" Width="100"></asp:TextBox>
                                    </td>
                                    <td class="Caption" style="width: 10%">
                                        月份：
                                    </td>
                                    <td class="Data" style="width: 25%">
                                        &nbsp;<asp:TextBox ID="bmonth" runat="server" Width="105"></asp:TextBox>
                                    </td>
                                    <td class="Caption" style="width: 10%">
                                        类别：
                                    </td>
                                    <td class="Data" style="width: 25%">
                                        &nbsp;<asp:DropDownList ID="traintype" runat ="server">
                                            
                                        </asp:DropDownList>
                                    </td>
                                    <td class="Data" style="width: 10%; text-align: center">
                                        <asp:Button ID="Button1" runat="server" ToolTip="点按钮开始分析" Text="开始分析"  />
                                    </td>
                                </tr>
                            </table>
                            <!--数据列表区-->
                            <div id="SearchInfo" runat="server" visible="false">
                                <div style="margin-top: 10px">
                                    1)收入明细表(单位为万)</div>
                                <table class="ListTable" cellspacing="1" id="SearchDataTable" cellpadding="0" style="width: 98%;
                                    margin-top: 10px">
                                    <tr>
                                        <td class="Caption" rowspan="2">
                                            车次
                                        </td>
                                        <td class="Caption" colspan="5">
                                            理论值
                                        </td>
                                        <td class="Caption" colspan="7">
                                            实际值
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Caption">
                                            当月人数
                                        </td>
                                        <td class="Caption">
                                            累计人数
                                        </td>
                                        <td class="Caption">
                                            当月收入
                                        </td>
                                        <td class="Caption">
                                            当月支出
                                        </td>
                                        <td class="Caption">
                                            累计收入
                                        </td>
                                        <td class="Caption">
                                            当月人数
                                        </td>
                                        <td class="Caption">
                                            累计人数
                                        </td>
                                        <td class="Caption">
                                            当月收入
                                        </td>
                                        <td class="Caption">
                                            累计收入
                                        </td>
                                        <td class="Caption">
                                            当月上座率
                                        </td>
                                        <td class="Caption">
                                            累计上座率
                                        </td>
                                        <td class="Caption">
                                            <b style="color:Red">理论盈亏上座率</b>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="Repeater1" runat="server" EnableViewState="true">
                                        <ItemTemplate>
                                            <!--显示的数据1-->
                                            <tr>
                                                <td class="Data">
                                                    <a class="blue" href="javascript:GoKindFenxi('<%#Eval("trainname") %>');">
                                                    <%#Eval("trainname") %> <asp:Label ID="lblAB" runat="server" Text= '<%# "(" + BusinessRule.NewTrainBU.GetAStation(Eval("trainname").ToString()) + " ~ "+ BusinessRule.NewTrainBU.GetBStation(Eval("trainname").ToString())+")"%>'>
                                                    </asp:Label>
                                                </td>
                                                
                                                <td class="Data">
                                                    <%#Eval("pcount0") %>
                                                </td>
                                                <td class="Data">
                                                    <%#Eval("pcount1") %>
                                                </td>
                                                <td class="Data">
                                                    <%#Eval("shouru0","{0:n0}") %>
                                                </td>
                                                 <td class="Data">
                                                     <%#Eval("fee0","{0:n0}") %>
                                                </td>
                                                <td class="Data">
                                                    <%#Eval("shouru1", "{0:n0}")%>
                                                </td>
                                                
                                                <td class="Data">
                                                    <%#Eval("spcount0") %>
                                                </td>
                                                <td class="Data">
                                                    <%#Eval("spcount1") %>
                                                </td>
                                                <td class="Data">
                                                    <%#Eval("sshouru0", "{0:n0}")%>
                                                </td>
                                                <td class="Data">
                                                    <%#Eval("sshouru1", "{0:n0}")%>
                                                </td>
                                                
                                                <td class="Data">
                                                    <%#Eval("szr0","{0:n2}") %>%
                                                </td>
                                                <td class="Data">
                                                     <%#Eval("szr1","{0:n2}") %>%
                                                </td>
                                                 <td class="Data">
                                                     <b style="color:Red"><%#Eval("szr2","{0:n2}") %>%</b>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                                <div style="margin-top: 10px">
                                    2)支出明细表 (单位为万) </div>
                                <table class="ListTable" cellspacing="1" id="Table1" cellpadding="0" style="width: 98%;
                                    margin-top: 10px">
                                    <tr>
                                        <td class="Caption" rowspan="2">
                                            类别
                                        </td>
                                        <td class="Caption" colspan="2">
                                            理论值
                                        </td>
                                        <td class="Caption" colspan="6">
                                            实际值
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Caption">
                                            当月支出
                                        </td>
                                        <td class="Caption">
                                            累计支出
                                        </td>
                                        <td class="Caption">
                                            当月收入
                                        </td>
                                        <td class="Caption">
                                            累计收入
                                        </td>
                                        <td class="Caption">
                                            当月支出
                                        </td>
                                        <td class="Caption">
                                            累计支出
                                        </td>
                                        <td class="Caption">
                                            当月盈亏
                                        </td>
                                        <td class="Caption">
                                            累计盈亏
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="Repeater2" runat="server" EnableViewState="true">
                                        <ItemTemplate>
                                            <!--显示的数据1-->
                                            <tr>
                                                <td class="Data">
                                                    <a class="blue" href="javascript:GoKindFenxi('<%#Eval("trainname") %>');">
                                                    <%#Eval("trainname") %>
                                                </td>
                                                <td class="Data">
                                                    <%#Eval("fee0","{0:n0}") %>
                                                </td>
                                                <td class="Data">
                                                    <%#Eval("fee1","{0:n0}")%>
                                                </td>
                                                <td class="Data">
                                                    <%#Eval("sshouru0","{0:n0}") %>
                                                </td>
                                                <td class="Data">
                                                    <%#Eval("sshouru1","{0:n0}") %>
                                                </td>
                                                <td class="Data">
                                                    <%#Eval("sfee0","{0:n0}") %>
                                                </td>
                                                <td class="Data">
                                                    <%#Eval("sfee1","{0:n0}") %>
                                                </td>
                                                <td class="Data">
                                                    <%#Eval("yk0","{0:n0}") %>
                                                </td>
                                                <td class="Data">
                                                    <%#Eval("yk1","{0:n0}") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                                <div style="margin-top: 10px">
                                    提示：如当月没有显示数据，请先点 <a class ="blue" href="NewTrainLiLunList.aspx">既有车次收支明细</a> 计算需要的数据。</div>
                                 
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

