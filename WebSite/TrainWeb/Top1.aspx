<%@ Page Language="C#" AutoEventWireup="true" StylesheetTheme="" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title></title>
    <style type="text/css">
        img
        {
            border: 0;
        }
        .menu
        {
            height: 29px;
            float: left;
        }
        .menu ul
        {
            list-style: none;
            padding: 0;
            margin: 0;
        }
        .menu ul li
        {
            background-image: url(/images/menu1.gif);
            width: 98px;
            height: 29px;
            float: left;
            margin-right: 5px;
            text-align: center;
            line-height: 29px;
            cursor: pointer;
        }
        .menu ul li span
        {
            font-size: 13px;
            font-weight: bolder;
            color: #FFFFFF;
        }
        a
        {
            color: #000;
            text-decoration: none;
            blr: expression(this.onFocus=this.blur());
        }
    </style>
</head>
<body bgcolor="#FFFFFF" leftmargin="0" topmargin="0" marginwidth="0"
    marginheight="0" style="background-image: url(/images/TopBg.gif); background-repeat: repeat-x;
    height: 96px; cursor:wait">
    <form id="form1" runat="server">
    <div>
        <!-- ImageReady Slices (����ҳ��.psd) -->
        <table id="__01" height="96px" border="0" cellpadding="0" cellspacing="0" width="100%"
            style="background-image: url(/images/top.jpg); background-repeat: no-repeat;">
            <tr>
                <td colspan="2" height="55px" width="1024px">
                </td>
            </tr>
            <tr>
                <td style="height: 29px" width="943" valign="top">
                    <table height="29px" border="0" cellpadding="0" cellspacing="0" width="924px">
                        <tr>
                            <td width="180px">
                            </td>
                            <td>
                                <div class="menu">
                                    <ul>
                                        <!--<li id="menu1" onclick="javascript:void(0);"><span>��ҳ</span></li> -->
                                        <li id="menu2" onclick="javascript:void(0);"><span>���ݷ���</span></li>
                                        <li id="menu3" onclick="javascript:void(0);"><span>�г���Ϣ</span></li>
                                        <li id="menu7" onclick="javascript:void(0);"><span>��������</span></li>
                                        <li id="menu4" onclick="javascript:void(0);"><span>ϵͳ����</span></li>
                                        <!--<li id="menu5" onclick="javascript:void(0);"><span>ϵͳ����</span></li> -->
                                        <li id="menu6" onclick="javascript:void(0);"><span>�޸�����</span></li>
                                    </ul>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td height="30px" align="right" valign="middle">
                    <div style="width: 300px; text-align: right; line-height: 30px; padding-right: 10px">
                        <a href="javascript:void(0);" style="color: #FFFFFF; font-size: 12px">
                            <img src="/images/back.gif" style="vertical-align: middle" />����</a> <a href="javascript:void(0);"
                                style="color: #FFFFFF; font-size: 12px">
                                <img src="/images/forward.gif" style="vertical-align: middle" />ǰ��</a> <a href="javascript:void(0);"
                                    style="color: #FFFFFF; font-size: 12px">
                                    <img src="/images/refresh.gif" style="vertical-align: middle" />ˢ��</a>
                        <a href="javascript:void(0);" style="color: #FFFFFF; font-size: 12px">
                            <img src="/images/exit.gif" style="vertical-align: middle" />�˳�ϵͳ</a>
                    </div>
                </td>
            </tr>
        </table>
        <!-- End ImageReady Slices -->
    </div>
    </form>
</body>
</html>
