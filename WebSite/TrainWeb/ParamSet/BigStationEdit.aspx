﻿<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="BigStationEdit.aspx.cs" Inherits="WebSite.TrainWeb.ParamSet.BigStationEdit" %>

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
                                                    <span class="STYLE3" style="display:none">你当前的位置：</span>财务数据模型-大站列表
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
                            <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="BIGSTATION" OrderBy="num">
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
                                            <jasp:JTextBox ID="num" runat="server" Caption="序号" AllowNullValue="false" IsUnique="true"></jasp:JTextBox>
                                        </td>
                                        <td class="Caption">
                                            铁路局
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="name1" Caption="铁路局" AllowNullValue="false" runat="server"></jasp:JTextBox>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td class="Caption">
                                            内燃牵引费
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="fee1" runat="server" Caption="内燃机车牵引费" DataType="Integer"></jasp:JTextBox>
                                        </td>
                                        <td class="Caption">
                                            电力牵引费
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="fee2" runat="server" Caption="电力机车牵引费" DataType="Integer"></jasp:JTextBox>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            电力机车网电费
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="fee3" runat="server" Caption=" 电力机车网电费" DataType="Integer"></jasp:JTextBox>
                                        </td>
                                        <td class="Caption">
                                            动车组网电费
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="fee4" runat="server" Caption="动车组网电费" DataType="Integer"></jasp:JTextBox>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            大站列表
                                        </td>
                                        <td class="Data" colspan="3">
                                            <jasp:JTextBox ID="bigname" runat="server" Caption="大站列表" AllowNullValue="false"
                                                Width="635" TextMode="MultiLine" Height="80"></jasp:JTextBox>
                                        </td>
                                    </tr>
                                </table>
                                <br />&nbsp;&nbsp;&nbsp;&nbsp;费用单位：元/万总重吨公里
                            </jasp:JDetailView>
                            <div class="ButtonArea" style="width: 80%">
                                <asp:UpdatePanel ID="update1" runat="server">
                                    <ContentTemplate>
                                        <jasp:JButton ID="button1" runat="server" Text="提 交" AuthorityID="001" JButtonType="SimpleAction"
                                            IsValidatorData="true">
                                            <SimpleActionPara TableName="BIGSTATION" ButtonType="UpdateData" AppendFlag="true" />
                                            <ParaItems>
                                                <jasp:ParameterItem ParaName="num" ParaType="RequestQueryString" />
                                            </ParaItems>
                                            <ExecutePara SuccInfo="提示：操作成功！" SuccUrl="-1" FailInfo="提示：操作失败！" />
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
