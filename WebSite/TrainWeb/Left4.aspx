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
            background-image: url(/images/二级页面_27.gif);
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
        function HoverLi(n) {
            for (var i = 1; i < 7; i++) {
                getID("secondmenu" + i).style.display = "none";
                if (n == i) {
                    getID("secondmenu" + n).style.display = "block";
                }
            }
        }

        function getID(o) {
            return window.parent.menu.document.getElementById(o);
        }
    </script>

</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0"
    style="text-align: left; background-image: url(/images/leftBg1.gif); background-repeat: repeat-y;
    margin: 0; padding: 0;">
    <form id="form1" runat="server">
    <div class="menu">
        <div>
            <img src="/images/二级页面_20.gif" /></div>
        <!--首页对应二级菜单，如没有，可删除-->
        <div>
            <ul>
                <li><a href="SystemProfile/Shouru_Profile.aspx" target="dmMain"><span>收入相关配置</span></a></li>
                <li><a href="SystemProfile/Fee_ZhiChuProfile.aspx" target="dmMain"><span>支出相关配置</span></a></li>
                 <li><a href="SystemProfile/UserList.aspx" target="dmMain"><span>用户管理</span></a></li>
                <% if (false)
                   { %>
                <li><a href="Test/Test2.aspx" target="dmMain"><span>1-客车票价测试</span></a></li>
                <li><a href="Test/Test3.aspx" target="dmMain"><span>2-动车票价测试</span></a></li>
                <li><a href="Test/Test4.aspx" target="dmMain"><span>3-客车支出测试</span></a></li>
                <li><a href="Test/Test5.aspx" target="dmMain"><span>4-动车支出测试</span></a></li>
                <li><a href="Test/Test6.aspx" target="dmMain"><span>4-线路测试</span></a></li>
                <%} %>
            </ul>
        </div>
       
    </div>
    </form>
</body>
</html>
