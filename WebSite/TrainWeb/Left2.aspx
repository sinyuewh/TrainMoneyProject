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
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" style="text-align: left;
    background-image: url(/images/leftBg1.gif); background-repeat: repeat-y; margin: 0;
    padding: 0;">
    <form id="form1" runat="server">
    <div class="menu">
        <div>
            <img src="/images/二级页面_20.gif" /></div>
        <div>
            <ul>
                <li><a href="Fenxi/NewCheCiFenxi.aspx" target="dmMain"><span>新增车次分析(按趟)</span></a></li>
                <li><a href="Fenxi/NewCheCiFenxi.aspx?isYearFlag=1" target="dmMain"><span>新增车次分析(按年)</span></a></li>
                <li><a href="Fenxi/NewTrainLiLunList.aspx" target="dmMain"><span>既有车次收支明细</span></a></li>
                <li><a href="Fenxi/OldCheCiFenxiByKind.aspx" target="dmMain"><span>既有车次收支分析</span></a></li>
                <% if (false)
                   { %>
                <li><a href="Fenxi/OldCheCiFenxiPaiHang.aspx" target="dmMain"><span>既有车次排行榜</span></a></li>
                <%} %>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
