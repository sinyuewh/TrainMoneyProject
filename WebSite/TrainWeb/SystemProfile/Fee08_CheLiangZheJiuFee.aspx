<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="Fee08_CheLiangZheJiuFee.aspx.cs" Inherits="WebSite.TrainWeb.SystemProfile.Fee08_CheLiangZheJiuFee" %>

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
                                                    <span class="STYLE3" style="display:none">你当前的位置：</span>系统配置-车辆采购价格表
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
                            <div style="display: none">
                                <div style="margin-top: 10">
                                    &nbsp;&nbsp;表1：车辆折旧费率
                                </div>
                                <table class="ListTable" cellspacing="1" cellpadding="0" style="width: 60%; margin-left: 15px;
                                    margin-top: 10px">
                                    <tr>
                                        <td class="Caption">
                                            序号
                                        </td>
                                        <td class="Caption">
                                            列车类型
                                        </td>
                                        <td class="Caption">
                                            折旧费率(%)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Data">
                                            1
                                        </td>
                                        <td class="Data">
                                            客车折旧费率
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="Fee1" runat="server" Text=''></jasp:JTextBox>(%)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Data">
                                            2
                                        </td>
                                        <td class="Data">
                                            动列折旧费率
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="Fee2" runat="server" Text=''></jasp:JTextBox>(%)
                                        </td>
                                    </tr>
                                </table>
                                <div class="ButtonArea" style="width: 60%">
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
                            </div>
                            <jasp:JDataSource ID="Data2" runat="server" PageSize="-1" JType="Table" SqlID="COMMTRAINWEIGHTPROFILE"
                                OrderBy="num">
                            </jasp:JDataSource>
                            <div style="margin-top: 10">
                                &nbsp;&nbsp;表1：客车采购价格表（单位：万元/车厢）
                            </div>
                            <table class="ListTable" cellspacing="1" cellpadding="0" style="width: 95%; margin-left: 15px;
                                margin-top: 10px">
                                <tr>
                                    <td class="Caption">
                                        车型
                                    </td>
                                    <td class="Caption">
                                        车速(公里/小时)
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
                                                <jasp:JTextBox ID="Speed" runat="server" Text='<%#Eval("Speed")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="YzPrice" runat="server" Text='<%#Eval("YzPrice")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="RzPrice" runat="server" Text='<%#Eval("RzPrice")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="SYzPrice" runat="server" Text='<%#Eval("SYzPrice")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="SRzPrice" runat="server" Text='<%#Eval("SRzPrice")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="YwPrice" runat="server" Text='<%#Eval("YwPrice")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="RwPrice" runat="server" Text='<%#Eval("RwPrice")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="GRw19KPrice" runat="server" Text='<%#Eval("GRw19KPrice")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="GRw19TPrice" runat="server" Text='<%#Eval("GRw19TPrice")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="CaPrice" runat="server" Text='<%#Eval("CaPrice")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="KdPrice" runat="server" Text='<%#Eval("KdPrice")%>'></jasp:JTextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <div class="ButtonArea" style="width: 80%">
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
                            &nbsp;&nbsp;表2：动车采购价格表（单位：万元）
                            <table class="ListTable" cellspacing="1" cellpadding="0" style="width: 95%; margin-left: 15px;
                                margin-top: 10px">
                                <tr>
                                    <td class="Caption">
                                        序号
                                    </td>
                                    <td class="Caption">
                                        车型
                                    </td>
                                    
                                    <td class="Caption">
                                        采购价格(万元)
                                    </td>
                                    
                                    <td class="Caption">
                                        既有线车速(公里/小时)
                                    </td>
                                    
                                    <td class="Caption">
                                        200公里客转(公里/小时)
                                    </td>
                                    
                                    <td class="Caption">
                                        300公里客转(公里/小时)
                                    </td>
                                </tr>
                                <asp:Repeater ID="Repeater3" runat="server" DataSourceID="Data3">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <%#Eval("id") %>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("HIGHTRAINTYPE")%>
                                            </td>
                                            
                                            <td class="Data">
                                                <jasp:JTextBox ID="Price" runat="server" Text='<%#Eval("Price")%>'></jasp:JTextBox>
                                            </td>
                                            
                                            <td class="Data">
                                                <jasp:JTextBox ID="Speed" runat="server" Text='<%#Eval("Speed")%>'></jasp:JTextBox>
                                            </td>
                                            
                                            <td class="Data">
                                                <jasp:JTextBox ID="Speed2" runat="server" Text='<%#Eval("Speed2")%>'></jasp:JTextBox>
                                            </td>
                                            
                                            <td class="Data">
                                                <jasp:JTextBox ID="Speed3" runat="server" Text='<%#Eval("Speed3")%>'></jasp:JTextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <div class="ButtonArea" style="width: 95%">
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
