<%@ Page Language="C#" AutoEventWireup="true" StylesheetTheme="" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title></title>
    <style type="text/css">
        .menu
        {
            text-align: center;
            width: 179px;
            height: 100%;
            float: left;
            padding-right: 5px;
        }
        .menu ul
        {
            list-style: none;
            padding: 0;
            margin: 0;
        }
        .menu ul li
        {
            background-image: url(/images/����ҳ��_27.gif);
            height: 28px;
            text-align: left;
            line-height: 28px;
        }
        .menu ul li span
        {
            font-size: 14px;
            font-weight: bolder;
            color: #FFFFFF;
        }
        a
        {
            color: #000;
            text-decoration: none;
            blr: expression(this.onFocus=this.blur());
            margin-left: 30px;
        }
    </style>

    <script language="javascript" type="text/javascript">
        
    </script>

</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" style="text-align: left;
    background-image: url(/images/leftBg1.gif); background-repeat: repeat-y; margin: 0;
    padding: 0;">
    <form id="form1" runat="server">
    <div class="menu">
        <div>
            <img src="/images/����ҳ��_20.gif" /></div>
        <div id="secondmenu2">
            <ul>
                <li><a href="javascript:void(0);"><span>�������η���(����)</span></a></li>
                <li><a href="javascript:void(0);"><span>�������η���(����)</span></a></li>
                <li><a href="javascript:void(0);"><span>���г�����֧��ϸ</span></a></li>
                <li><a href="javascript:void(0);"><span>���г�����֧����</span></a></li>
                <% if(false){ %>
                <li><a href="javascript:void(0);"><span>���г������а�</span></a></li>
                <%} %>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
