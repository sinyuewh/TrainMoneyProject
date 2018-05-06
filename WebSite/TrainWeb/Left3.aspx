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
</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0"
    style="text-align: left; background-image: url(/images/leftBg1.gif); background-repeat: repeat-y;
    margin: 0; padding: 0;">
    <form id="form1" runat="server">
    <div class="menu">
        <div>
            <img src="/images/二级页面_20.gif" /></div>
        <!--列车维护对应二级菜单，如没有，可删除Train/ChangJiaoFeeList.aspx-->
        <div id="secondmenu3">
            <ul>
                <li><a href="Train/NewTrainList.aspx" target="dmMain"><span>列车列表</span></a></li>
                <li><a href="Train/NewTrainShouRuList.aspx" target="dmMain"><span>担当车收入</span></a></li>
                <li><a href="Train/NewTrainXianLuFeeList.aspx" target="dmMain"><span>线路使用费</span></a></li>
                <li><a href="Train/NewTrainQianYinFeeList.aspx" target="dmMain"><span>机车牵引费</span></a></li>
                <li><a href="Train/NewTrainDianFeeList.aspx" target="dmMain"><span>电网和接触网费</span></a></li>
                <li><a href="Train/NewTrainYunShunRsList.aspx" target="dmMain"><span>列车运输人数</span></a></li>
                <li><a href="Train/TrainLineList.aspx" target="dmMain"><span>列车线路列表</span></a></li>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
