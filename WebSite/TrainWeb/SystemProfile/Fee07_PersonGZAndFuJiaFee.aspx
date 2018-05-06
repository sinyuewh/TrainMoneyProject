<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="Fee07_PersonGZAndFuJiaFee.aspx.cs" Inherits="WebSite.TrainWeb.SystemProfile.Fee07_PersonGZAndFuJiaFee" %>

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
                                                    <span class="STYLE3" style="display: none">你当前的位置：</span>系统配置-人员工资及附加费
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
                            <jasp:JDataSource ID="Data1" runat="server" PageSize="-1" JType="Table" SqlID="PersonGZ"
                                OrderBy="num">
                                <ParaItems>
                                    <jasp:ParameterItem ParaName="kind='0'" ParaType="String" DataName="$$empty" />
                                </ParaItems>
                            </jasp:JDataSource>
                            <div style="margin-top: 10">
                                &nbsp;&nbsp;表1：客车工作人员工资及附加费标准
                            </div>
                            <table class="ListTable" cellspacing="1" cellpadding="0" style="width: 90%; margin-left: 15px;
                                margin-top: 10px">
                                <tr>
                                    <td class="Caption">
                                        序号
                                    </td>
                                    <td class="Caption">
                                        岗位
                                    </td>
                                    <td class="Caption">
                                        工资标准(元/年)
                                    </td>
                                    <td class="Caption">
                                        附加费标准(%)
                                    </td>
                                    <td class="Caption">
                                        和车厢关联
                                    </td>
                                </tr>
                                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Data1">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <%#Container.ItemIndex+1%>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Gw" runat="server" Text='<%#Eval("Gw")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Gz" runat="server" Text='<%#Eval("Gz")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Fj" runat="server" Text='<%#Eval("Fj")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JDropDownList ID="Gl" runat="server" SelectedValue='<%#Eval("gl")%>' >
                                                    <asp:ListItem Text="不和车厢关联" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="硬座（含软座）车厢关联" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="硬卧车厢关联" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="软卧（含高级软卧）车厢关联" Value="3"></asp:ListItem>
                                                </jasp:JDropDownList>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <div class="ButtonArea" style="width: 90%">
                                <asp:UpdatePanel ID="update1" runat="server">
                                    <ContentTemplate>
                                        <jasp:JButton ID="button1" runat="server" OnClientClick="javascript:return confirm('提示：确定要更新数据吗？');"
                                            AuthorityID="001" Text="提 交">
                                        </jasp:JButton>
                                        &nbsp;
                                        <jasp:JButton ID="JButton1" runat="server" Text="返 回" JButtonType="RedirectButton">
                                            <ExecutePara SuccUrl="-1" />
                                        </jasp:JButton>
                                        &nbsp;&nbsp;<a href="Fee15_TrainPersonList.aspx" class="blue">[列车工作人员配置]</a>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <hr />
                            <jasp:JDataSource ID="Data2" runat="server" PageSize="-1" JType="Table" SqlID="PersonGZ"
                                OrderBy="num">
                                <ParaItems>
                                    <jasp:ParameterItem ParaName="kind='1'" ParaType="String" DataName="$$empty" />
                                </ParaItems>
                            </jasp:JDataSource>
                            &nbsp;&nbsp;表2：动车工作人员工资及附加费标准
                            <table class="ListTable" cellspacing="1" cellpadding="0" style="width: 90%; margin-left: 15px;
                                margin-top: 10px">
                                <tr>
                                    <td class="Caption">
                                        序号
                                    </td>
                                    <td class="Caption">
                                        岗位
                                    </td>
                                    <td class="Caption">
                                        工资标准(元/年)
                                    </td>
                                    <td class="Caption">
                                        附加费标准(%)
                                    </td>
                                </tr>
                                <asp:Repeater ID="Repeater2" runat="server" DataSourceID="Data2">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <%#Container.ItemIndex+1%>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Gw" runat="server" Text='<%#Eval("Gw")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Gz" runat="server" Text='<%#Eval("Gz")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Fj" runat="server" Text='<%#Eval("Fj")%>'></jasp:JTextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <div class="ButtonArea" style="width: 90%">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <jasp:JButton ID="JButton2" AuthorityID="001" OnClientClick="javascript:return confirm('提示：确定要更新数据吗？');"
                                            runat="server" Text="提 交">
                                        </jasp:JButton>
                                        &nbsp;
                                        <jasp:JButton ID="JButton3" runat="server" Text="返 回" JButtonType="RedirectButton">
                                            <ExecutePara SuccUrl="-1" />
                                        </jasp:JButton>
                                        &nbsp;&nbsp;<a href="Fee15_TrainPersonList.aspx" class="blue">[列车工作人员配置]</a>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
