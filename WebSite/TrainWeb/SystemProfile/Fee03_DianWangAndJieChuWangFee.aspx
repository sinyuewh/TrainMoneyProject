<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="Fee03_DianWangAndJieChuWangFee.aspx.cs" Inherits="WebSite.TrainWeb.SystemProfile.Fee03_DianWangAndJieChuWangFee" %>

<%@ Import Namespace="System.Data" %>

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
                                                    <span class="STYLE3" style="display:none">你当前的位置：</span>系统配置-电费和接触网使用费
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
                            <jasp:JDataSource ID="Data1" runat="server" PageSize="-1" JType="Table" SqlID="TRAINLINEKINDPROFILE"
                                OrderBy="LineID">
                            </jasp:JDataSource>
                            <div style="margin-top: 10">
                                &nbsp;&nbsp;表1：电费和接触网使用费配置表
                            </div>
                            <table class="ListTable" cellspacing="1" cellpadding="0" style="width: 60%; margin-left: 15px;
                                margin-top: 10px">
                                <tr>
                                    <td class="Caption">
                                        线路类型
                                    </td>
                                    <td class="Caption">
                                        接触网使用费
                                    </td>
                                    <td class="Caption">
                                        电费
                                    </td>
                                </tr>
                                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Data1">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <%#Eval("LineName")%>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="JieChuFee" runat="server" Text='<%#Eval("JieChuFee")%>'></jasp:JTextBox>（元/万吨.公里）
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="DianFee" runat="server" Text='<%#Eval("DianFee")%>'></jasp:JTextBox>（元/万吨.公里）
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <div class="ButtonArea" style="width: 60%">
                                <asp:UpdatePanel ID="update1" runat="server">
                                    <ContentTemplate>
                                        <jasp:JButton ID="button1" runat="server" OnClientClick="javascript:return confirm('提示：确定要更新数据吗？');"
                                            AuthorityID="001" Text="提 交">
                                        </jasp:JButton>
                                        &nbsp;
                                        <jasp:JButton ID="JButton1" runat="server" Text="返 回" JButtonType="RedirectButton">
                                            <ExecutePara SuccUrl="-1" />
                                        </jasp:JButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <hr />
                            <jasp:JDataSource ID="Data2" runat="server" PageSize="-1" JType="Table" SqlID="HIGHTRAINPROFILE"
                                OrderBy="id">
                            </jasp:JDataSource>
                            &nbsp;&nbsp;表2：动车车厢重量配置表（单位：吨）
                            <table class="ListTable" cellspacing="1" cellpadding="0" style="width: 60%; margin-left: 15px;
                                margin-top: 10px">
                                <tr>
                                    <td class="Caption">
                                        序号
                                    </td>
                                    <td class="Caption">
                                        车型
                                    </td>
                                    <td class="Caption">
                                        重量(吨)
                                    </td>
                                </tr>
                                <asp:Repeater ID="Repeater2" runat="server" DataSourceID="Data2">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <%#Eval("id") %>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("HIGHTRAINTYPE") %>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Weight" runat="server" Text='<%#Eval("Weight")%>'></jasp:JTextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <div class="ButtonArea" style="width: 80%">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <jasp:JButton ID="JButton2" AuthorityID="001" OnClientClick="javascript:return confirm('提示：确定要更新数据吗？');"
                                            runat="server" Text="提 交">
                                        </jasp:JButton>
                                        &nbsp;
                                        <jasp:JButton ID="JButton3" runat="server" Text="返 回" JButtonType="RedirectButton">
                                            <ExecutePara SuccUrl="-1" />
                                        </jasp:JButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="ButtonArea" style="width: 90%; text-align: left;">
                            </div>
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
