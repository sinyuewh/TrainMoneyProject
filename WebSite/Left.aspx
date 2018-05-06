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
<body onload="HoverLi(<%=(Request.QueryString["menuid"]!=null ? int.Parse(Request.QueryString["menuid"]) : 1) %>);" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0"
    style="text-align: left; background-image: url(/images/leftBg1.gif); background-repeat: repeat-y;
    margin: 0; padding: 0;">
    <form id="form1" runat="server">
    <div class="menu">
        <div>
            <img src="/images/二级页面_20.gif" /></div>
        <!--首页对应二级菜单，如没有，可删除-->
        <div id="secondmenu1">
            <ul id="menu1" runat="server" visible="false">
                <li><a href="#"><span>菜单菜单1</span></a></li>
                <li><a href="#"><span>菜单单菜1</span></a></li>
                <li><a href="#"><span>菜单单菜1</span></a></li>
            </ul>
        </div>
        <!--数据分析二级菜单，如没有，可删除-->
        <div id="secondmenu2">
            <ul>
                <li><a href="Fenxi/NewCheCiFenxi.aspx" target="dmMain"><span>新增车次分析(按趟)</span></a></li>
                <li><a href="Fenxi/NewCheCiFenxi.aspx?isYearFlag=1" target="dmMain"><span>新增车次分析(按年)</span></a></li>
                <% if (false)
                   { %>
                <li><a href="Fenxi/HistoryFenxiList.aspx" target="dmMain"><span>保存的车次分析</span></a></li>
                <%} %>
                <li><a href="Fenxi/NewTrainLiLunList.aspx" target="dmMain"><span>既有车次收支明细</span></a></li>
                <% if (false)
                   { %>
                <li><a href="Fenxi/OldCheCiFenxi.aspx" target="dmMain"><span>既有车次收支分析</span></a></li>
                <%} %>
                <li><a href="Fenxi/OldCheCiFenxiByKind.aspx" target="dmMain"><span>既有车次收支分析</span></a></li>
            </ul>
        </div>
        <!--列车维护对应二级菜单，如没有，可删除-->
        <div id="secondmenu3">
            <ul>
                <li><a href="Train/NewTrainList.aspx" target="dmMain"><span>列车列表</span></a></li>
                <li><a href="Train/NewTrainShouRuList.aspx" target="dmMain"><span>担当车收入</span></a></li>
                <li><a href="Train/NewTrainXianLuFeeList.aspx" target="dmMain"><span>线路使用费</span></a></li>
                <li><a href="Train/NewTrainQianYinFeeList.aspx" target="dmMain"><span>机车牵引费</span></a></li>
                <li><a href="Train/NewTrainDianFeeList.aspx" target="dmMain"><span>电网和接触网费</span></a></li>
                <li><a href="Train/NewTrainYunShunRsList.aspx" target="dmMain"><span>列车运输人数</span></a></li>
                <li><a href="Train/TrainLineList.aspx" target="dmMain"><span>列车线路列表</span></a></li>
                <li><a href="Train/StationAliasList.aspx" target="dmMain"><span>站点别名</span></a></li>
                <li><a href="Train/BigStationList.aspx" target="dmMain"><span>大站列表</span></a></li>
            </ul>
        </div>
        <!--系统配置对应二级菜单，如没有，可删除-->
        <div id="secondmenu4">
            <ul>
                <li><a href="SystemProfile/Shouru_Profile.aspx" target="dmMain"><span>收入相关配置</span></a></li>
                <li><a href="SystemProfile/Fee_ZhiChuProfile.aspx" target="dmMain"><span>支出相关配置</span></a></li>
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
        <!--系统管理对应二级菜单，如没有，可删除-->
        <div id="secondmenu5">
            <ul>
                <li><a href="SystemProfile/UserList.aspx" target="dmMain"><span>用户管理</span></a></li>
                <% if (false)
                   { %>
                <li><a href="javascript:void(0);" target="dmMain"><span>更新日志</span></a></li>
                <li><a href="javascript:void(0);" target="dmMain"><span>数据库备份与恢复</span></a></li><%} %>
            </ul>
        </div>
        <!--修改密码对应二级菜单，如没有，可删除-->
        <div id="secondmenu6">
        </div>
    </div>
    </form>
</body>
</html>
