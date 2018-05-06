<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="Shouru02_CommTrainCheXianProfile.aspx.cs" Inherits="WebSite.TrainWeb.SystemProfile.Shouru02_CommTrainCheXianProfile" %>

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
                                                    <span class="STYLE3" style="display:none">你当前的位置：</span>系统配置-普通列车车厢配置
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
                            <jasp:JDataSource ID="Data1" runat="server" PageSize="-1" JType="Table" SqlID="CHEXIANBIANZHU"
                                OrderBy="id">
                            </jasp:JDataSource>
                            <table class="ListTable" cellspacing="1" cellpadding="0" style="width: 90%; margin-left: 15px;
                                margin-top: 10px">
                                <tr>
                                    <td class="Caption" colspan="3">
                                        车厢类型
                                    </td>
                                    <td class="Caption">
                                        票价率(元/(人•千米))
                                    </td>
                                    <td class="Caption">
                                        比例（%）
                                    </td>
                                    <td class="Caption">
                                        满员人数
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data" colspan="3">
                                        硬座
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price1" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate1" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person1" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td class="Data" colspan="3">
                                        双层硬座
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price12" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate12" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person12" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td class="Data" colspan="3">
                                        软座
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price2" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate2" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person2" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td class="Data" colspan="3">
                                        双层软座
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price13" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate13" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person13" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td class="Data" rowspan="5">
                                        硬卧票
                                    </td>
                                    <td class="Data" rowspan="3">
                                        开放式
                                    </td>
                                    <td class="Data">
                                        上铺
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price3" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate3" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person3" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data">
                                        中铺
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price4" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate4" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person4" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data">
                                        下铺
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price5" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate5" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person5" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data" rowspan="2">
                                        包房式
                                    </td>
                                    <td class="Data">
                                        上铺
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price6" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate6" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person6" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data">
                                        下铺
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price7" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate7" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person7" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data" rowspan="2">
                                        软卧票
                                    </td>
                                    <td class="Data" colspan="2">
                                        上铺
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price8" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate8" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person8" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data" colspan="2">
                                        下铺
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price9" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate9" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person9" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td class="Data" rowspan="2">
                                        高级软卧票
                                    </td>
                                    <td class="Data" colspan="2">
                                        上铺
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price10" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate10" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person10" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data" colspan="2">
                                        下铺
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price11" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate11" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person11" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                
                                 <tr>
                                    <td class="Data" rowspan="2">
                                        19K高级软卧票
                                    </td>
                                    <td class="Data" colspan="2">
                                        上铺
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price14" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate14" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person14" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data" colspan="2">
                                        下铺
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price15" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate15" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person15" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                
                                 <tr>
                                    <td class="Data" rowspan="2">
                                        19T高级软卧票
                                    </td>
                                    <td class="Data" colspan="2">
                                        上铺
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price16" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate16" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person16" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data" colspan="2">
                                        下铺
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price17" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate17" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person17" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                            </table>
                            <div class="ButtonArea" style="width: 100%">
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
