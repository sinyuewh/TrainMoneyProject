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
            <img src="/images/����ҳ��_20.gif" /></div>
        <!--��ҳ��Ӧ�����˵�����û�У���ɾ��-->
        <div id="secondmenu1">
            <ul id="menu1" runat="server" visible="false">
                <li><a href="#"><span>�˵��˵�1</span></a></li>
                <li><a href="#"><span>�˵�����1</span></a></li>
                <li><a href="#"><span>�˵�����1</span></a></li>
            </ul>
        </div>
        <!--���ݷ��������˵�����û�У���ɾ��-->
        <div id="secondmenu2">
            <ul>
                <li><a href="Fenxi/NewCheCiFenxi.aspx" target="dmMain"><span>�������η���(����)</span></a></li>
                <li><a href="Fenxi/NewCheCiFenxi.aspx?isYearFlag=1" target="dmMain"><span>�������η���(����)</span></a></li>
                <% if (false)
                   { %>
                <li><a href="Fenxi/HistoryFenxiList.aspx" target="dmMain"><span>����ĳ��η���</span></a></li>
                <%} %>
                <li><a href="Fenxi/NewTrainLiLunList.aspx" target="dmMain"><span>���г�����֧��ϸ</span></a></li>
                <% if (false)
                   { %>
                <li><a href="Fenxi/OldCheCiFenxi.aspx" target="dmMain"><span>���г�����֧����</span></a></li>
                <%} %>
                <li><a href="Fenxi/OldCheCiFenxiByKind.aspx" target="dmMain"><span>���г�����֧����</span></a></li>
            </ul>
        </div>
        <!--�г�ά����Ӧ�����˵�����û�У���ɾ��-->
        <div id="secondmenu3">
            <ul>
                <li><a href="Train/NewTrainList.aspx" target="dmMain"><span>�г��б�</span></a></li>
                <li><a href="Train/NewTrainShouRuList.aspx" target="dmMain"><span>����������</span></a></li>
                <li><a href="Train/NewTrainXianLuFeeList.aspx" target="dmMain"><span>��·ʹ�÷�</span></a></li>
                <li><a href="Train/NewTrainQianYinFeeList.aspx" target="dmMain"><span>����ǣ����</span></a></li>
                <li><a href="Train/NewTrainDianFeeList.aspx" target="dmMain"><span>�����ͽӴ�����</span></a></li>
                <li><a href="Train/NewTrainYunShunRsList.aspx" target="dmMain"><span>�г���������</span></a></li>
                <li><a href="Train/TrainLineList.aspx" target="dmMain"><span>�г���·�б�</span></a></li>
                <li><a href="Train/StationAliasList.aspx" target="dmMain"><span>վ�����</span></a></li>
                <li><a href="Train/BigStationList.aspx" target="dmMain"><span>��վ�б�</span></a></li>
            </ul>
        </div>
        <!--ϵͳ���ö�Ӧ�����˵�����û�У���ɾ��-->
        <div id="secondmenu4">
            <ul>
                <li><a href="SystemProfile/Shouru_Profile.aspx" target="dmMain"><span>�����������</span></a></li>
                <li><a href="SystemProfile/Fee_ZhiChuProfile.aspx" target="dmMain"><span>֧���������</span></a></li>
                <% if (false)
                   { %>
                <li><a href="Test/Test2.aspx" target="dmMain"><span>1-�ͳ�Ʊ�۲���</span></a></li>
                <li><a href="Test/Test3.aspx" target="dmMain"><span>2-����Ʊ�۲���</span></a></li>
                <li><a href="Test/Test4.aspx" target="dmMain"><span>3-�ͳ�֧������</span></a></li>
                <li><a href="Test/Test5.aspx" target="dmMain"><span>4-����֧������</span></a></li>
                <li><a href="Test/Test6.aspx" target="dmMain"><span>4-��·����</span></a></li>
                <%} %>
            </ul>
        </div>
        <!--ϵͳ�����Ӧ�����˵�����û�У���ɾ��-->
        <div id="secondmenu5">
            <ul>
                <li><a href="SystemProfile/UserList.aspx" target="dmMain"><span>�û�����</span></a></li>
                <% if (false)
                   { %>
                <li><a href="javascript:void(0);" target="dmMain"><span>������־</span></a></li>
                <li><a href="javascript:void(0);" target="dmMain"><span>���ݿⱸ����ָ�</span></a></li><%} %>
            </ul>
        </div>
        <!--�޸������Ӧ�����˵�����û�У���ɾ��-->
        <div id="secondmenu6">
        </div>
    </div>
    </form>
</body>
</html>
