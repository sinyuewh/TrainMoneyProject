<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="Fee10_DiQiJianXiuFee.aspx.cs" Inherits="WebSite.TrainWeb.SystemProfile.Fee10_DiQiJianXiuFee" %>

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
                                                    <span class="STYLE3" style="display:none">你当前的位置：</span>系统配置-车辆定期检修费
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
                            <jasp:JDataSource ID="Data1" runat="server" PageSize="-1" JType="Table" SqlID="COMMTRAINWEIGHTPROFILE"
                                OrderBy="num">
                            </jasp:JDataSource>
                            <div style="margin-top: 10">
                                &nbsp;&nbsp;表1：客车A4修价格表（单位：万元）
                            </div>
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
                                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Data1">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <%#Eval("traintype") %>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="YZA4" runat="server" Text='<%#Eval("YZA4")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="RZA4" runat="server" Text='<%#Eval("RZA4")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="SYZA4" runat="server" Text='<%#Eval("SYZA4")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="SRZA4" runat="server" Text='<%#Eval("SRZA4")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="YWA4" runat="server" Text='<%#Eval("YWA4")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="RWA4" runat="server" Text='<%#Eval("RWA4")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="RW19KA4" runat="server" Text='<%#Eval("RW19KA4")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="RW19TA4" runat="server" Text='<%#Eval("RW19TA4")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="CAA4" runat="server" Text='<%#Eval("CAA4")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="KDA4" runat="server" Text='<%#Eval("KDA4")%>'></jasp:JTextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            
                            <div class="ButtonArea" style="width: 90%">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <jasp:JButton ID="button1" AuthorityID="001" OnClientClick="javascript:return confirm('提示：确定要更新数据吗？');"
                                            runat="server" Text="提 交">
                                        </jasp:JButton>
                                        &nbsp;
                                        <jasp:JButton ID="JButton2" runat="server" Text="返 回" JButtonType="RedirectButton">
                                            <ExecutePara SuccUrl="-1" />
                                        </jasp:JButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <hr />
                            
                            <jasp:JDataSource ID="Data2" runat="server" PageSize="-1" JType="Table" SqlID="A2A3FEE"
                                OrderBy="num">
                            </jasp:JDataSource>
                            <div style="margin-top: 10">
                                &nbsp;&nbsp;表2：客车A2、A3修价格表（单位：万元）
                            </div>
                            <table class="ListTable" cellspacing="1" cellpadding="0" style="width: 65%; margin-left: 15px;
                                margin-top: 10px">
                                <tr>
                                    <td class="Caption">
                                        车型
                                    </td>
                                    <td class="Caption">
                                        A2
                                    </td>
                                    <td class="Caption">
                                        A3
                                    </td>
                                </tr>
                                <asp:Repeater ID="Repeater2" runat="server" DataSourceID="Data2">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <%#Eval("traintype") %>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="A2Fee" runat="server" Text='<%#Eval("A2Fee")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="A3Fee" runat="server" Text='<%#Eval("A3Fee")%>'></jasp:JTextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            
                            <div class="ButtonArea" style="width: 65%">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <jasp:JButton ID="button2" AuthorityID="001" OnClientClick="javascript:return confirm('提示：确定要更新数据吗？');"
                                            runat="server" Text="提 交">
                                        </jasp:JButton>
                                        &nbsp;
                                        <jasp:JButton ID="JButton3" runat="server" Text="返 回" JButtonType="RedirectButton">
                                            <ExecutePara SuccUrl="-1" />
                                        </jasp:JButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            
                            <hr />
                            <jasp:JDataSource ID="Data3" runat="server" PageSize="-1" JType="Table" SqlID="HIGHTRAINPROFILE"
                                OrderBy="id">
                            </jasp:JDataSource>
                            &nbsp;&nbsp;表3：动车定期检修成本标准（单位：万元）
                            <table class="ListTable" cellspacing="1" cellpadding="0" style="width: 65%; margin-left: 15px;
                                margin-top: 10px">
                                <tr>
                                    <td class="Caption">
                                        序号
                                    </td>
                                    <td class="Caption">
                                        车型
                                    </td>
                                    <td class="Caption">
                                        三级检修
                                    </td>
                                    <td class="Caption">
                                        四级检修
                                    </td>
                                    <td class="Caption">
                                        五级检修
                                    </td>
                                </tr>
                                <asp:Repeater ID="Repeater3" runat="server" DataSourceID="Data3">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <%#Eval("id") %>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("hightraintype") %>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Cost2" runat="server" Text='<%#Eval("Cost2")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Cost21" runat="server" Text='<%#Eval("Cost21")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Cost22" runat="server" Text='<%#Eval("Cost22")%>'></jasp:JTextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            
                            <div class="ButtonArea" style="width: 65%">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <jasp:JButton ID="button3" AuthorityID="001" OnClientClick="javascript:return confirm('提示：确定要更新数据吗？');"
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
