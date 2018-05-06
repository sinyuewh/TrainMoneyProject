<%@ Page Title="登陆" Language="C#" MasterPageFile="~/Designer/HtglMain.Master" 
    AutoEventWireup="true" StylesheetTheme=""%>
    
<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        ImageButton1.Click += new ImageClickEventHandler(ImageButton1_Click);
        base.OnInit(e);
    }

    void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if(WebFrame.FrameLib.CheckData(ImageButton1))
        {
            String curname1 = WebFrame.Util.JCookie.GetCookieValue("HTGL_UserName");
            String userName = this.UserName.Text.Trim();
            if (curname1 != userName)
            {
                WebFrame.Util.JCookie.SetCookieValue("HTGL_UserName", userName, 15 * 24);
            }
            Response.Redirect("Frame/HtFrame.aspx", true);
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           this.UserName.Text = WebFrame.Util.JCookie.GetCookieValue("HTGL_UserName");
        }
        base.OnLoad(e);
    }
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        body
        {
            background-color: #233C50;
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
            font-size:12px;
        }
        .style1
        {
            height: 358px;
        }
    </style>
    <link href="Frame/css.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/jscript">
        function go() {
            if (parent != window) {
                top.location.href = "Login.aspx";
            }
            dWidth = 800;
            dHeight = 600;
            resizeTo(dWidth, dHeight);
            moveTo((screen.availWidth - dWidth) / 2, (screen.availHeight - dHeight) / 2);
        }
        go();
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" height="778" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="389" align="center" valign="bottom" background="images/ubj.jpg"><table width="522"  border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td height="3" align="center" valign="top"><img src="images/u1.jpg" width="521" height="3" /></td>
      </tr>
      <tr>
        <td height="35" align="center" valign="top" background="images/u2.jpg"><table width="516" height="100%" border="0" cellpadding="0" cellspacing="0" background="images/fjh.jpg">
            <tr>
              <td height="69" align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="ls">铁路局列车收支模型</span></td>
            </tr>
            
        </table></td>
      </tr>
      <tr>
        <td height="3" align="center" valign="top" background="images/u2.jpg"><table width="516" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td align="center" valign="middle"><img src="images/u3.jpg" width="521" height="3" /></td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td  align="center" valign="top" background="images/u2.jpg"></td>
      </tr>
      <tr>
        <td height="19"  align="center" valign="top" background="images/u2.jpg"><table width="516" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td width="231" align="left" valign="top"><img src="images/z1.jpg" width="231" height="171" /></td>
              <td width="2" align="left" valign="top" bgcolor="#4C6C81"></td>
              <td width="226" align="center" valign="middle" background="images/cbj.jpg"><table width="98%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td width="31%" height="32" align="left" valign="middle" class="l">&nbsp;&nbsp;&nbsp;用户名：</td>
                    <td width="69%" align="left" valign="middle">&nbsp;<jasp:JTextBox ID="UserName" runat="server" Width="100" AllowNullValue="false" Caption="用户名" />                    </td>
                  </tr>
                  <tr>
                    <td align="left" valign="middle" class="l">&nbsp;&nbsp;&nbsp;密&nbsp;码&nbsp;：</td>
                    <td align="left" valign="middle">&nbsp;<jasp:JTextBox ID="PassWord" runat="server" Width="100" TextMode="Password"  Caption="密码" /></td>
                  </tr>
                  <tr>
                    <td colspan="2" height="49" align="left" valign="middle" style=" padding-left:50px"><asp:UpdatePanel ID="update1" runat="server"><ContentTemplate><jasp:JImageButton ID="ImageButton1" runat="server" ImageUrl="images/ng.jpg" IsValidatorData="true" ControlList="UserName,PassWord" JButtonType="NoJButton"
         /></ContentTemplate></asp:UpdatePanel></td>
                  </tr>
              </table></td>
              <td width="3" align="left" valign="top" bgcolor="#4C6C81"></td>
              <td width="54" align="left" valign="top"><img src="images/ybyu.jpg" width="54" height="171" /></td>
            </tr>
            <tr>
              <td height="25" colspan="5" align="center" valign="top"  background="images/dw.jpg" ></td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td height="3"  align="center" valign="top"><img src="images/u4.jpg" width="521" height="2" /></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td height="389" align="center" valign="top" background="images/bj1.jpg">&nbsp;</td>
  </tr>
</table>
</asp:Content>
