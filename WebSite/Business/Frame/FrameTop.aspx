<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" StylesheetTheme="" %>
<html>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<head>
<title></title>
<base target="view" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link href="css.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">
    //刷新页面
    function MyRefresh() {
        var win1 = window.top.frames["view"];
        win1.location.reload();
    }

    //转登录页面
    function Relogin() {
        window.onunload = "";
        window.onbeforeunload = "";
        top.location.href = "../Login.aspx?Relogin=1"
    }

    //退出系统
    function ExitApp(flag) {
        var exit1 = true;
        if (flag == 1) {
            exit1 = confirm("提示：确定退出OA系统吗？");
        }
        if (exit1) {
            top.location.href = "signOut.aspx";
        }
    }

    //检查页面是否在框架
    function checkWin() {
        if (parent == window) {
            top.location.href = "Login.aspx";
        }
    }

    function Myreload() {
        window.onbeforeunload = "";
        window.location.reload();
        var win1 = window.top.frames["LeftFrm"];
        win1.location.reload();
    }
</script>
</head>
<body style="background-color: #1E3957;">
<form runat="server" id="Form1">
<table width="989" height="54" border="0"  cellpadding="0" cellspacing="0" background="../images/qsbj.jpg">
  <tr>
    <td height="16" colspan="3" align="center" valign="top"><img src="../images/q1.jpg" width="989" height="16" /></td>
  </tr>
  <tr>
    <td width="7" height="38" align="center" valign="top"></td>
    <td width="975" align="center" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0" background="../images/bjlan.jpg">
      <tr>
        <td width="66%" align="left" valign="top"><img src="../images/log.jpg" width="643" height="81" /></td>
        <td width="32%" align="right" valign="top"><img src="../images/logt11.jpg" alt="" width="153" height="81" /></td>
        <td width="2%" align="left" valign="top">&nbsp;</td>
      </tr>
      <tr>
        <td colspan="3" align="center" valign="top" background="../images/ulbj.jpg">
        <table width="952" height="29" border="0" align="center" cellpadding="0" cellspacing="0" background="../images/lt.jpg" class="bhbi">
          <tr>
            <td height="29" class="hong" width="200">&nbsp;欢迎您,&nbsp;<a href="javascript:Relogin();" target="_self" title="点用户名，重新登录" class="linkhei"><font color="red"><%=WebFrame.Util.JCookie.GetCookieValue("HTGL_UserName")%></font></a>
            </td>
            <td align="left">
                &nbsp; <img src="../images/icons/MyDesk.png" />&nbsp;<a href="MainPage.aspx" class="linkhei">我的桌面</a>
            </td>
            <td align="right">
                        
             <img src="../images/icons/Refresh.png" /><a href="javascript:MyRefresh();" class="linkhei" title="刷新主窗口" target="_self" >刷新</a>
             &nbsp;
            </td>
          </tr>
        </table></td>
      </tr>
    </table></td>
    <td width="7" align="center" valign="top"></td>
  </tr>
</table>
</form>
</body>
</html>