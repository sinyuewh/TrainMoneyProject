<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebSite.NewLogin"
    StylesheetTheme="" %>

<html>
<head runat="server">
    <title></title>
    <style type="text/css">
        BODY
        {
            scrollbar-face-color: #d3d3d3;
            scrollbar-shadow-color: #F3F4F4;
            scrollbar-highlight-color: #F3F4F4;
            scrollbar-3dlight-color: #F3F4F4;
            scrollbar-darkshadow-color: #F3F4F4;
            scrollbar-track-color: #F3F4F4;
            scrollbar-arrow-color: #F3F4F4;
        }
    </style>
</head>
<body bgcolor="#FFFFFF" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0"
    style="text-align: center">
    <form id="form1" runat="server">
    <!-- ImageReady Slices (login.psd) -->
    <div style="background-image: url(images/login.jpg); width: 1030px; height: 756px">
        <div style="float: left; width: 570px; height: 756px">
        </div>
        <div style="float: left; width: 460px; height: 313px">
        </div>
        <div style="float: left; width: 460px; height: 441px">
            <table width="460" height="262" border="0">
                <tr>
                    <td width="127" height="107">
                        &nbsp;
                    </td>
                    <td width="323">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td height="28">
                        &nbsp;
                    </td>
                    <td>
                        <jasp:JTextBox ID="UserID" runat="server" Caption="用户名" AllowNullValue="false" Style="border: 0;
                            width: 165px" />
                    </td>
                </tr>
                <tr>
                    <td height="20">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td height="26">
                        &nbsp;
                    </td>
                    <td>
                        <jasp:JTextBox ID="PassWord" runat="server" Caption="登录密码" AllowNullValue="false"
                            TextMode="Password" Style="border: 0; width: 165px" />
                    </td>
                </tr>
                <tr>
                    <td height="69" style="padding-bottom: 21px; text-align: right; font-size: 12px">
                        &nbsp;
                    </td>
                    <td style="padding-bottom: 21px; font-size: 12px; padding-left: 50px">
                        &nbsp;
                    </td>
                </tr>
            </table>

                    <table width="460" height="40" border="0" style="margin-top: -50">
                        <tr>
                            <td width="175" align="right">
                                <jasp:JImageButton ID="button1" runat="server" ImageUrl="~/images/loginBt.gif" IsValidatorData="false">
                                </jasp:JImageButton>
                            </td>
                            <td width="37">
                            </td>
                            <td width="234">
                                <input id="Reset1" type="image" value="reset" src="/images/ResetBt.gif" onclick="javascript:document.forms[0].reset();return false;" />
                            </td>
                        </tr>
                    </table>

        </div>
    </div>
    <!-- End ImageReady es -->
    <script language="javascript" type="text/javascript">
        if (window.parent != window) {
            top.location.href = "Login.aspx";
        }
    </script>
    </form>
</body>
</html>
