<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" 
AutoEventWireup="true" CodeBehind="NewTrainShouRuDetail.aspx.cs" 
Inherits="WebSite.TrainWeb.Train.NewTrainShouRuDetail" %>
<%@ Import Namespace="BusinessRule" %>

<script runat="server">
    
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="/Common/JsLib/PubLib.js" type="text/javascript"></script>

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
                                                    <span class="STYLE3" style="display:none">你当前的位置：</span>财务数据模型-列车收入明细
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
                            <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="NEWTRAINSHOUROU" OrderBy="num">
                                <ParaItems>
                                    <jasp:ParameterItem ParaName="num" ParaType="RequestQueryString" IsNullValue="true" />
                                </ParaItems>
                            </jasp:JDataSource>
                            <jasp:JDetailView ID="DetailView1" runat="server" DataSourceID="Data1" ControlList="*">
                                <table class="DetailTable" cellspacing="1" cellpadding="0" style="width: 80%; margin-left: 15px">
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
                                            旅客列车全程客票进款
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="shourou1" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            旅客票价进款
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="shourou2" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                 
                                 <tr>
                                        <td class="Caption">
                                            软票费
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="shourou3" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            卧订费
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="shourou4" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            空调费
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="shourou5" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            应收客票进款
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="shourou6" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            当月列车编组
                                        </td>
                                        <td class="Data" colspan="3">
                                            硬座：<jasp:JTextBox ID="YinZuo"  Width="40" runat="server"></jasp:JTextBox>
                                            软座：<jasp:JTextBox ID="RuanZuo" Width="40" runat="server"></jasp:JTextBox>
                                            硬卧：<jasp:JTextBox ID="OpenYinWo" Width="40" runat="server"></jasp:JTextBox>
                                            软卧：<jasp:JTextBox ID="RuanWo" Width="40" runat="server"></jasp:JTextBox>
                                            餐车：<jasp:JTextBox ID="CanChe" Width="40" runat="server"></jasp:JTextBox>
                                            发电车：<jasp:JTextBox ID="FaDianChe" Width="40" runat="server"></jasp:JTextBox>
                                            行李车：<jasp:JTextBox ID="ShuYinChe" Width="40" runat="server"></jasp:JTextBox>
                                            邮政车：<jasp:JTextBox ID="YouZhengChe" Width="40" runat="server"></jasp:JTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </jasp:JDetailView>
                            <div class="ButtonArea" style="width: 80%">
                                <asp:UpdatePanel ID="update1" runat="server">
                                    <ContentTemplate>
                                        <jasp:JButton ID="button1" runat="server" Text="提 交"  AuthorityID="001"
                                             JButtonType="SimpleAction" IsValidatorData="true">
                                            <SimpleActionPara TableName="NEWTRAINSHOUROU" ButtonType="UpdateData" AppendFlag="true" />
                                            <ParaItems>
                                                <jasp:ParameterItem ParaName="num" ParaType="RequestQueryString" />
                                            </ParaItems>
                                            <ExecutePara SuccInfo="提示：提交数据操作成功！" SuccUrl="-1" FailInfo="提示：提交数据操作失败！" />
                                        </jasp:JButton>
                                        &nbsp;
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
