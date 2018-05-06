<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="Fee02_QianYinFeeProfile.aspx.cs" Inherits="WebSite.TrainWeb.SystemProfile.Fee02_QianYinFeeProfile" %>

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
                                                    <span class="STYLE3" style="display: none">你当前的位置：</span>系统配置-机车牵引费
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
                            <jasp:JDataSource ID="Data1" runat="server" PageSize="-1" JType="Table" SqlID="QianYinFeeProfile"
                                OrderBy="QIANYINTYPE">
                            </jasp:JDataSource>
                            <div style="margin-top: 10">
                                &nbsp;&nbsp;表1：牵引类型配置表
                            </div>
                            <table class="ListTable" cellspacing="1" cellpadding="0" style="width: 65%; margin-left: 15px;
                                margin-top: 10px">
                                <tr>
                                    <td class="Caption">
                                        牵引类型
                                    </td>
                                    <td class="Caption">
                                        非直供电客车
                                    </td>
                                    <td class="Caption">
                                        一站直达
                                    </td>
                                </tr>
                                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Data1">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <%#Eval("QIANYINNAME")%>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Fee2" runat="server" Text='<%#Eval("Fee2")%>'></jasp:JTextBox>（元/万吨）
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Fee3" runat="server" Text='<%#Eval("Fee3")%>'></jasp:JTextBox>（元/万吨）
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <div class="ButtonArea" style="width: 65%">
                                <asp:UpdatePanel ID="update1" runat="server">
                                    <ContentTemplate>
                                        <jasp:JButton ID="button1" AuthorityID="001" OnClientClick="javascript:return confirm('提示：确定要更新数据吗？');"
                                            runat="server" Text="提 交">
                                        </jasp:JButton>
                                        &nbsp;
                                        <jasp:JButton ID="JButton1" runat="server" Text="返 回" JButtonType="RedirectButton">
                                            <ExecutePara SuccUrl="-1" />
                                        </jasp:JButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <hr />
                            <div style="margin-top: 10">
                                &nbsp;&nbsp;表2：机车直供电附加单价表(单位：元/万总重吨公里)
                            </div>
                            <table class="ListTable" cellspacing="1" cellpadding="0" style="width: 65%; margin-left: 15px;
                                margin-top: 10px">
                                <tr>
                                    <td class="Caption">
                                        交路
                                    </td>
                                    <td class="Caption">
                                        机种
                                    </td>
                                    <td class="Caption">
                                        牵引费单价
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data">
                                        各内燃牵引区段
                                    </td>
                                    <td class="Data">
                                        内燃
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Qyfj1" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data">
                                        各电力牵引区段
                                    </td>
                                    <td class="Data">
                                        电力
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Qyfj2" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                            </table>
                            <div class="ButtonArea" style="width: 65%">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <jasp:JButton ID="JButton4" AuthorityID="001" OnClientClick="javascript:return confirm('提示：确定要更新数据吗？');"
                                            runat="server" Text="提 交">
                                        </jasp:JButton>
                                        &nbsp;
                                        <jasp:JButton ID="JButton5" runat="server" Text="返 回" JButtonType="RedirectButton">
                                            <ExecutePara SuccUrl="-1" />
                                        </jasp:JButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <hr />
                            <div style="margin-top: 10">
                                &nbsp;&nbsp;表3：轮渡费单价表(单位：元/辆公里)
                            </div>
                            <table class="ListTable" cellspacing="1" cellpadding="0" style="width: 65%; margin-left: 15px;
                                margin-top: 10px">
                                <tr>
                                    <td class="Caption">
                                        项目
                                    </td>
                                    <td class="Caption">
                                        轮渡费单价
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data">
                                        空调客车
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="ShipFee1" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data">
                                        非空调客车
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="ShipFee2" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                            </table>
                            <div class="ButtonArea" style="width: 65%">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <jasp:JButton ID="JButton6" AuthorityID="001" OnClientClick="javascript:return confirm('提示：确定要更新数据吗？');"
                                            runat="server" Text="提 交">
                                        </jasp:JButton>
                                        &nbsp;
                                        <jasp:JButton ID="JButton7" runat="server" Text="返 回" JButtonType="RedirectButton">
                                            <ExecutePara SuccUrl="-1" />
                                        </jasp:JButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <hr />
                            <jasp:JDataSource ID="Data2" runat="server" PageSize="-1" JType="Table" SqlID="COMMTRAINWEIGHTPROFILE"
                                OrderBy="num">
                            </jasp:JDataSource>
                            &nbsp;&nbsp;表4：车厢重量配置表（单位：吨）
                            <table class="ListTable" cellspacing="1" cellpadding="0" style="width: 90%; margin-left: 15px;
                                margin-top: 10px">
                                <tr>
                                    <td class="Caption">
                                        车型
                                    </td>
                                    <td class="Caption">
                                        硬座
                                    </td>
                                    <td class="Caption">
                                        软座
                                    </td>
                                    <td class="Caption">
                                        双层硬座
                                    </td>
                                    <td class="Caption">
                                        双层软座
                                    </td>
                                    <td class="Caption">
                                        硬卧
                                    </td>
                                    <td class="Caption">
                                        软卧
                                    </td>
                                    <td class="Caption">
                                        19K高软
                                    </td>
                                    <td class="Caption">
                                        19T高软
                                    </td>
                                    <td class="Caption">
                                        餐车
                                    </td>
                                    <td class="Caption">
                                        发电车
                                    </td>
                                </tr>
                                <asp:Repeater ID="Repeater2" runat="server" DataSourceID="Data2">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <%#Eval("traintype") %>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="YzWeight" runat="server" Text='<%#Eval("YzWeight")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Rzweight" runat="server" Text='<%#Eval("RzWeight")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="SYzWeight" runat="server" Text='<%#Eval("SYzWeight")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="SRzWeight" runat="server" Text='<%#Eval("SRzWeight")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="YwWeight" runat="server" Text='<%#Eval("YwWeight")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="RwWeight" runat="server" Text='<%#Eval("RwWeight")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="GRw19KWeight" runat="server" Text='<%#Eval("GRw19KWeight")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="GRw19TWeight" runat="server" Text='<%#Eval("GRw19TWeight")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="CaWeight" runat="server" Text='<%#Eval("CaWeight")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="KdWeight" runat="server" Text='<%#Eval("KdWeight")%>'></jasp:JTextBox>
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
