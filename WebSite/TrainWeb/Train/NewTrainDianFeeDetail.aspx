<%@ Page Title="电费和接触网使用费" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" 
AutoEventWireup="true" CodeBehind="NewTrainDianFeeDetail.aspx.cs" Inherits="WebSite.TrainWeb.Train.NewTrainDianFeeDetail" %>
<%@ Import Namespace="BusinessRule" %>

<script runat="server">
    
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="/Common/JsLib/PubLib.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        //将数据导入到系统
        function ImportDataToSystem() {
            var url1 = "ImportDataTool.aspx?kind=0";
            myOpenURL(url1, 400, 600);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="30" background="/TrainWeb/images/tab_05.gif">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="12" height="30">
                            <img src="/TrainWeb/images/tab_03.gif" width="12" height="30" />
                        </td>
                        <td>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="46%" valign="middle">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="5%">
                                                    <div align="center">
                                                        <img src="/TrainWeb/images/tb.gif" width="16" height="16" /></div>
                                                </td>
                                                <td width="95%" class="STYLE1">
                                                    <span class="STYLE3" style="display:none">你当前的位置：</span>财务数据模型-电费和基础网使用费
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
                            <img src="/TrainWeb/images/tab_07.gif" width="16" height="30" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="8" background="/TrainWeb/images/tab_12.gif">
                            &nbsp;
                        </td>
                        <td style="height: 600px; vertical-align: top">
                            <!--数据源定义-->
                            <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="NewTrainDianFee"
                                OrderBy="num">
                                <ParaItems>
                                    <jasp:ParameterItem ParaName="num" ParaType="RequestQueryString" IsNullValue="true" />
                                </ParaItems>
                            </jasp:JDataSource>
                            <jasp:JDetailView ID="DetailView1" runat="server" DataSourceID="Data1" ControlList="*">
                                <table class="DetailTable" cellspacing="1" cellpadding="0" style="width: 95%; margin-left: 15px">
                                    <tr>
                                        <td class="Caption">
                                            序号
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="num" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            车次
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="trainname" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Caption">
                                            年份
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="byear" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            月份
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="bmonth" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Caption">
                                            始发站
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="astation" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            终到站
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="bstation" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Caption">
                                            本企业万总重吨公里
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="zl1" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            本企业应付余额
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="fee1" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Caption">
                                            外企业(国铁)万总重吨公里
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="zl2" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            外企业(国铁)应付余额
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="fee2" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Caption">
                                            外企业(非国铁)万总重吨公里
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="zl3" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            外企业(非国铁)应付余额
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="fee3" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                </table>
                            </jasp:JDetailView>
                            <div class="ButtonArea" style="width: 80%">
                                <asp:UpdatePanel ID="update1" runat="server">
                                    <ContentTemplate>
                                        <jasp:JButton ID="JButton1" runat="server" Text="返 回" JButtonType="RedirectButton">
                                            <ExecutePara SuccUrl="-1" />
                                        </jasp:JButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            &nbsp;
                        </td>
                        <td width="8" background="/TrainWeb/images/tab_15.gif">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="35" background="/TrainWeb/images/tab_19.gif">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="12" height="35">
                            <img src="/TrainWeb/images/tab_18.gif" width="12" height="35" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td width="16">
                            <img src="/TrainWeb/images/tab_20.gif" width="16" height="35" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

