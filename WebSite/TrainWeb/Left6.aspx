<%@ Page Language="C#" AutoEventWireup="true" StylesheetTheme="" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                <li><a href="ParamSet/GSCorpElecFeeList.aspx" target="dmMain"><span>客专公司电费</span></a></li>
                <li><a href="ParamSet/GTTrainDragFeeList.aspx" target="dmMain"><span>客运机车牵引费</span></a></li>
                <li><a href="ParamSet/StationAliasList.aspx" target="dmMain"><span>站点别名</span></a></li>
                <li><a href="ParamSet/BigStationList.aspx" target="dmMain"><span>大站列表</span></a></li>
                
                
                <li><a href="ParamSet/StationQuickList.aspx" target="dmMain"><span>站点快捷列表</span></a></li>
                <li><a href="ParamSet/ChangeStationName.aspx" target="dmMain"><span>更改站名</span></a></li>
                <li><a href="ParamSet/JoinStation.aspx" target="dmMain"><span>删除站点</span></a></li>
                <li><a href="ParamSet/InsertNewStation.aspx" target="dmMain"><span>插入站点</span></a></li>
                <li><a href="ParamSet/CorpQuickList.aspx" target="dmMain"><span>公司快捷列表</span></a></li>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
