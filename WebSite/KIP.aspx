<%@ Page Language="C#" MasterPageFile="TrainWeb/TrainWeb.Master" AutoEventWireup="true" CodeBehind="KIP.aspx.cs" Inherits="WebSite.KIP" Title="可访问IP设置" %>


<%@ Register Assembly="WebFrame" Namespace="WebFrame.ExpControl" TagPrefix="cc1" %>
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
                                                    <span class="STYLE3" style="display:none">你当前的位置：</span>可访问IP设置
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
                            <table class="DetailTable" cellspacing="1" cellpadding="0" style="width: 80%; margin-left: 15px">
                            <tr style="height:30px"><td colspan="2"></td></tr>
                             <tr>
                            <tr>
                            <td>
                              <b>可访问IP：</b>
                            </td>
                            <td align="left">
                              <jasp:JTextBox ID="KIPs" Caption="可访问IP" runat="server"  Width="400px"  Height="200px" TextMode="MultiLine"></jasp:JTextBox>
                            </td>
                            </tr>
                            <tr style="height:30px"><td colspan="2"></td></tr>
                             <tr>
                            <td>
                              <b>密码：</b>
                            </td>
                            <td align="left">
                              <jasp:JTextBox ID="Password" Caption="密码" runat="server"  TextMode="Password" Width="400px"></jasp:JTextBox>
                            </td>
                            </tr>
                            </table><br><br>
                             &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        <jasp:JButton ID="but1" runat="server" Text="确 定"  OnClick="but1_Click" Width="80" Height="30" Font-Size="Medium">
                                        </jasp:JButton>
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


