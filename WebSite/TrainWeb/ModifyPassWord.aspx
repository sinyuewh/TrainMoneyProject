<%@ Page Language="C#" MasterPageFile="TrainWeb.Master" AutoEventWireup="true" CodeBehind="ModifyPassWord.aspx.cs"
    Inherits="OASystemWeb.SysMng.ModifyPassWord" Title="修改用户登录密码" %>

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
                                                    <span class="STYLE3" style="display:none">你当前的位置：</span>修改登录密码
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
                                <tr>
                                    <td class="Caption">
                                        用户老密码
                                    </td>
                                    <td class="Data">
                                        <cc1:JTextBox ID="oldPass" runat="server" Width="200px" Caption="用户老密码" AllowNullValue="false"
                                            TextMode="Password"></cc1:JTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Caption">
                                        用户新密码
                                    </td>
                                    <td class="Data">
                                        <cc1:JTextBox ID="password1" runat="server" Width="200px" Caption="用户新密码" AllowNullValue="false"
                                            TextMode="Password"></cc1:JTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Caption">
                                        重复新密码
                                    </td>
                                    <td class="Data">
                                        <cc1:JTextBox ID="password2" runat="server" Width="200px" Caption="重复新密码" AllowNullValue="false"
                                            TextMode="Password" EqualControl="password1"></cc1:JTextBox>
                                    </td>
                                </tr>
                            </table>
                            <div class="ButtonArea" style="width: 80%">
                                <asp:UpdatePanel ID="update1" runat="server">
                                    <ContentTemplate>
                                        <cc1:JButton ID="but1" runat="server" Text="提交" IsValidatorData="true" OnClick="but1_Click">
                                        </cc1:JButton>
                                        &nbsp; &nbsp; &nbsp; &nbsp;
                                        <cc1:JButton ID="JButton1" runat="server" Text="返回" OnClientClick="history.back();return false;">
                                        </cc1:JButton>
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
