<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="Content.aspx.cs" Inherits="WebSite.TrainWeb.Content" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function HoverLi(n) {
            for (var i = 1; i < 7; i++) {
                getID("secondmenu" + i).style.display = "none";
                if (n == i) {
                    getID("secondmenu" + n).style.display = "block";
                }
            }

        }

        function getID(o) {
            return window.parent.menu.document.getElementById(o);
            //document.getElementById(o);
        }

        var win1 = top.frames["top"];
        win1.frames[0].location.href = "/TrainWeb/top.aspx";
        var win2 = top.frames["menu"];
        win2.location.href = "/TrainWeb/left.aspx";
        
    </script>

    <style type="text/css">
        body
        {
            background-color: White;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 100%; height: 28px; background-image: url(/images/title.gif);
        line-height: 28px">
        <img src="/images/tubiao.gif" style="float: left; width: 28px; height: 28px" />
        <span style="color: #07487F; font-weight: bolder; font-size: 12px">我的位置：</span>
    </div>
    <div style="clear: both">
    </div>
    <div style="width: 100%; background-color: #FFFFFF">
        <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0" style="margin-top: -40">
            <tr>
            <td width="20"  >
                    <img src="/images/line.gif"  />
                </td>
                <td align="center" valign="middle" width="600">
                    <img src="/images/centerBg.gif" usemap="#mp" />
                </td>
                
                <td width="240" valign="top" align="right" style="border: 0px solid black">
                    <div style="margin-top: 45px">
                        <asp:UpdatePanel ID="update1" runat="server">
                            <ContentTemplate>
                                <div style="width: 219px; height: 230px;">
                                    <asp:Calendar ID="Calendar1" runat="server" BackColor="#FFFFCC" BorderColor="#FFCC66"
                                        BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                        ForeColor="#663399" Height="200px" ShowGridLines="True" Width="220px">
                                        <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                                        <SelectorStyle BackColor="#FFCC66" />
                                        <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                                        <OtherMonthDayStyle ForeColor="#CC9966" />
                                        <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                        <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                                        <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                                    </asp:Calendar>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <table class="tab" width="100%" border="0" cellpadding="0" cellspacing="0" style="display:none">
                        <tr>
                            <td width="20%" rowspan="4" valign="top">
                                <img src="/images/icon1.gif" />
                            </td>
                            <td width="80%">
                                <span>数据分析</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span><a href="Fenxi/NewCheCiFenxi.aspx">新增车次分析</a></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span><a href="Fenxi/OldCheCiFenxi.aspx">既有车次分析</a></span>
                            </td>
                        </tr>
                    </table>
                    <table class="tab" width="100%" border="0" cellpadding="0" cellspacing="0" style="display:none">
                        <tr>
                            <td width="20%" rowspan="4" valign="top">
                                <img src="/images/icon2.gif" />
                            </td>
                            <td width="80%">
                                <span>列车信息</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span><a href="Train/NewTrainShouRuList.aspx">担当车收入</a></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span><a href="Train/NewTrainXianLuFeeList.aspx">线路使用费</a></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span><a href="Train/NewTrainQianYinFeeList.aspx">机车牵引费</a></span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <!-- <map name="mp">-->
        <!-- 数据分析超链接-->
        <!-- <area shape="circle" coords="164,191,62" onclick="HoverLi(2)" href="/TrainWeb/Fenxi/NewCheCiFenxi.aspx" />-->
        <!-- 列车信息超链接-->
        <!-- <area shape="circle" coords="441,220,63" onclick="HoverLi(3)" href="/TrainWeb/Train/NewTrainList.aspx" />-->
        <!--基础配置超链接-->
        <!-- <area shape="circle" coords="162,434,62" onclick="HoverLi(4)" href="/TrainWeb/SystemProfile/Shouru_Profile.aspx" />-->
        <!-- 系统管理超链接-->
        <!-- <area shape="circle" coords="432,429,62" onclick="HoverLi(5)" href="#" />-->
    <!--</map>-->
</asp:Content>
