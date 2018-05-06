<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="Fee12_KongTiaoCheYongYouFee.aspx.cs" Inherits="WebSite.TrainWeb.SystemProfile.Fee11_KongTiaoCheYongYouFee" %>

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
                                                    <span class="STYLE3" style="display:none">你当前的位置：</span>系统配置-空调车用油
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
                            <jasp:JDataSource ID="Data2" runat="server" PageSize="-1" JType="Table" SqlID="COMMTRAINWEIGHTPROFILE"
                                OrderBy="num">
                                <ParaItems>
                                    <jasp:ParameterItem ParaName="(traintype='25K' or traintype='25G')" ParaType="String"
                                        DataName="$$empty" />
                                </ParaItems>
                            </jasp:JDataSource>
                            <div style="margin-top: 10">
                                &nbsp;&nbsp;表1：空调车用油单耗（单位：公斤/千辆公里）
                            </div>
                            <table class="ListTable" cellspacing="1" cellpadding="0" style="width: 60%; margin-left: 15px;
                                margin-top: 10px">
                                <tr>
                                    <td class="Caption">
                                        车型
                                    </td>
                                    <td class="Caption">
                                        单耗
                                    </td>
                                </tr>
                                <asp:Repeater ID="Repeater2" runat="server" DataSourceID="Data2">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <%#Eval("traintype") %>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Oil" runat="server" Text='<%#Eval("Oil")%>'></jasp:JTextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <jasp:JDataSource ID="Data3" runat="server" PageSize="-1" JType="Table" SqlID="HeightTrainWeightProfile"
                                OrderBy="num">
                            </jasp:JDataSource>
                            <div style="margin-top: 10">
                                &nbsp;&nbsp;表2：列车燃油单价（单位：元/公斤）
                            </div>
                            <table class="ListTable" cellspacing="1" cellpadding="0" style="width: 60%; margin-left: 15px;
                                margin-top: 10px">
                                <tr>
                                    <td class="Caption" style="width: 40%; ">
                                        列车燃油单价
                                    </td>
                                    <td class="Data" >
                                        <jasp:JTextBox ID="Fee1" runat="server" Text=''></jasp:JTextBox>
                                    </td>
                                </tr>
                            </table>
                            <div class="ButtonArea" style="width: 80%">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <jasp:JButton ID="button2" AuthorityID="001" OnClientClick="javascript:return confirm('提示：确定要更新数据吗？');"
                                            runat="server" Text="提 交">
                                        </jasp:JButton>
                                        &nbsp;
                                        <jasp:JButton ID="JButton5" runat="server" Text="返 回" JButtonType="RedirectButton">
                                            <ExecutePara SuccUrl="-1" />
                                        </jasp:JButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="ButtonArea" style="width: 90%; text-align: left;">
                                &nbsp;&nbsp;&nbsp;&nbsp;<strong></strong>
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
