﻿<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="Shouru03_HighTrainProfile.aspx.cs" Inherits="WebSite.TrainWeb.SystemProfile.Back.Shouru03_HighTrainProfile" %>

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
                                                    <span class="STYLE3" style="display: none">你当前的位置：</span>系统配置-动车票价和定员
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
                            <jasp:JDataSource ID="Data1" runat="server" PageSize="-1" JType="Table" SqlID="HighTrainProfile"
                                OrderBy="id">
                            </jasp:JDataSource>
                            <table class="ListTable" id="tab1" runat="server" visible="false" cellspacing="1"
                                cellpadding="0" style="width: 98%; margin-left: 15px;">
                                <tr>
                                    <td class="Data" style="text-align: right">
                                        <div style="float: left;">
                                            &nbsp; &nbsp;
                                            <asp:FileUpload ID="FileUpload1" runat="server" Width="285px" />
                                            &nbsp;
                                            <asp:Button ID="BtImport" runat="server" Text="导入Excel数据" OnClick="BtImport_Click" />
                                            &nbsp; &nbsp;
                                            <asp:Button ID="BtExport" runat="server" Text="导出数据到Excel" OnClick="BtExport_Click" /></div>
                                    </td>
                                </tr>
                            </table>
                            <div style="margin-top: 10px">
                                &nbsp;&nbsp;&nbsp;1)动车的票价
                            </div>
                            <table class="ListTable" cellspacing="1" cellpadding="0" style="width: 98%; margin-left: 15px;
                                margin-top: 10px">
                                <tr>
                                    <td class="Caption">
                                        动车类型
                                    </td>
                                    <td class="Caption">
                                        动车类别
                                    </td>
                                    <td class="Caption">
                                        商务座基准票价
                                    </td>
                                    <td class="Caption">
                                        特等座基准票价
                                    </td>
                                    <td class="Caption">
                                        一等座基准票价
                                    </td>
                                    <td class="Caption">
                                        二等座基准票价
                                    </td>
                                    <td class="Caption">
                                        动卧上铺基准票价
                                    </td>
                                    <td class="Caption">
                                        动卧下铺基准票价
                                    </td>
                                </tr>
                                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Data1">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <%#Eval("HIGHTRAINTYPE")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("MILETYPE")%>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Rate4" runat="server" Text='<%#Eval("Rate4")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Rate5" runat="server" Text='<%#Eval("Rate5")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Rate1" runat="server" Text='<%#Eval("Rate1")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Rate2" runat="server" Text='<%#Eval("Rate2")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Rate3" runat="server" Text='<%#Eval("Rate3")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Rate31" runat="server" Text='<%#Eval("Rate31")%>'></jasp:JTextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <div class="ButtonArea" style="width: 98%">
                                <asp:UpdatePanel ID="update1" runat="server">
                                    <ContentTemplate>
                                        <jasp:JButton ID="button1" AuthorityID="001" runat="server" Text="提 交">
                                        </jasp:JButton>
                                        &nbsp;
                                        <jasp:JButton ID="JButton1" runat="server" Text="返 回" JButtonType="RedirectButton">
                                            <ExecutePara SuccUrl="-1" />
                                        </jasp:JButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div style="margin-top: 10px">
                                &nbsp;&nbsp;&nbsp;2)动车的定员
                            </div>
                            <table class="ListTable" cellspacing="1" cellpadding="0" style="width: 98%; margin-left: 15px;
                                margin-top: 10px">
                                <tr>
                                    <td class="Caption">
                                        动车类型
                                    </td>
                                    <td class="Caption">
                                        动车类别
                                    </td>
                                    <td class="Caption">
                                        商务座定员
                                    </td>
                                    <td class="Caption">
                                        特等座定员
                                    </td>
                                    <td class="Caption">
                                        一等座定员
                                    </td>
                                    <td class="Caption">
                                        二等座定员
                                    </td>
                                    <td class="Caption">
                                        动卧定员
                                    </td>
                                </tr>
                                <asp:Repeater ID="Repeater2" runat="server" DataSourceID="Data1">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <%#Eval("HIGHTRAINTYPE")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("MILETYPE")%>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="PCount4" runat="server" Text='<%#Eval("PCount4")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="PCount5" runat="server" Text='<%#Eval("PCount5")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="PCount1" runat="server" Text='<%#Eval("PCount1")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="PCount2" runat="server" Text='<%#Eval("PCount2")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="PCount3" runat="server" Text='<%#Eval("PCount3")%>'></jasp:JTextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <div class="ButtonArea" style="width: 98%">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <jasp:JButton ID="JButton2" AuthorityID="001" runat="server" Text="提 交">
                                        </jasp:JButton>
                                        &nbsp;
                                        <jasp:JButton ID="JButton3" runat="server" Text="返 回" JButtonType="RedirectButton">
                                            <ExecutePara SuccUrl="-1" />
                                        </jasp:JButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div style="text-align: left; display: none">
                                <hr />
                                1) 200公里动车组（硬座）票价收入计算=一等座基准票价*运行里程*定员（100）+二等座票价*运行里程*二等座定员（510）（CRH2A）<br />
                                2) 200公里动车组（卧铺）票价收入计算=二等座基准票价*运行里程*二等座定员（55*2）+ 动卧基准票价*运行里程*动卧定员（13*40）（CRH2E)
                                <br />
                                3) 300公里动车组票价收入计算=一等座基准票价*运行里程*一等座定员（51）+ 二等座票价*运行里程*二等座定员（559）（CRH2C）<br />
                                4) 300公里动车组票价收入计算=一等座基准票价*运行里程*一等座定员（107）+ 二等座票价*运行里程*二等座定员（373）（CRH380A）<br />
                                5) 300公里动车组票价收入计算=一等座基准票价*运行里程*一等座定员（157）+ 二等座票价*运行里程*二等座定员（838）+ 商务座票价*运行里程*特等座定员（22）（CRH380AL）
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
