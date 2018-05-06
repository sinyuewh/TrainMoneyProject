﻿<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="GTTrainDragFeeDetail.aspx.cs" Inherits="WebSite.TrainWeb.ParamSet.GTTrainDragFeeDetail" %>

<%@ Import Namespace="BusinessRule" %>

<script runat="server">
    
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="/Common/JsLib/PubLib.js" type="text/javascript"></script>
    
    <script language ="javascript" type ="text/javascript">
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
                                                    <span class="STYLE3" style="display:none">你当前的位置：</span>财务数据模型-客运机车牵引费明细
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
                            <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="GTTRAINDRAGFEE" OrderBy="NUM">
                                <ParaItems>
                                    <jasp:ParameterItem ParaName="NUM" ParaType="RequestQueryString" IsNullValue="true" />
                                </ParaItems>
                            </jasp:JDataSource>
                            <jasp:JDetailView ID="DetailView1" runat="server" DataSourceID="Data1" ControlList="*">
                                <table class="DetailTable" cellspacing="1" cellpadding="0" style="width: 95%; margin-left: 15px">
                                    <tr>
                                        <td class="Caption">
                                            线别
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="LINETYPE" runat="server" Caption="线别" AllowNullValue="false" Width="98%"></jasp:JTextBox>
                                        </td>
                                        <td class="Caption">
                                            交路
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="CROSSROAD" runat="server" AllowNullValue="false" Caption="交路" Width="98%"></jasp:JTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Caption">
                                            机种
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="MACTYPE" runat="server" Caption="机种" AllowNullValue="false" Width="98%"></jasp:JTextBox>
                                        </td>
                                        <td class="Caption">
                                            牵引费单价
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="DRAGFEE" runat="server" AllowNullValue="false" Caption="牵引费单价" DataType="Integer" Width="98%"></jasp:JTextBox>
                                        </td>
                                    </tr>    
                                    <tr>
                                        <td class="Caption">
                                            接触网（含电费）单价
                                        </td>
                                        <td class="Data"  colspan="3">
                                            <jasp:JTextBox ID="NETFEE" runat="server" Caption="接触网（含电费）单价" DataType="Integer" Width="99.3%"></jasp:JTextBox>
                                        </td>
                                    </tr>             
                                </table>
                            </jasp:JDetailView>
                            <div class="ButtonArea" style="width: 80%">
                                <asp:UpdatePanel ID="update1" runat="server">
                                    <ContentTemplate>
                                        <jasp:JButton ID="button1" runat="server" AuthorityID="001" Text="提 交" JButtonType="NoJButton" 
                                              IsValidatorData="true">
                                            <SimpleActionPara TableName="GTTRAINDRAGFEE" ButtonType="UpdateData" AppendFlag="true" />
                                            <ParaItems>
                                                <jasp:ParameterItem ParaName="NUM" ParaType="RequestQueryString" />
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
