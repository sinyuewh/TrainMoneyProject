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
</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0"
    style="text-align: left; background-image: url(/images/leftBg1.gif); background-repeat: repeat-y;
    margin: 0; padding: 0;">
    <form id="form1" runat="server">
    <div class="menu">
        <div>
            <img src="/images/����ҳ��_20.gif" /></div>
        <!--�г�ά����Ӧ�����˵�����û�У���ɾ��Train/ChangJiaoFeeList.aspx-->
        <div id="secondmenu3">
            <ul>
                <li><a href="ParamSet/GSCorpElecFeeList.aspx" target="dmMain"><span>��ר��˾���</span></a></li>
                <li><a href="ParamSet/GTTrainDragFeeList.aspx" target="dmMain"><span>���˻���ǣ����</span></a></li>
                <li><a href="ParamSet/StationAliasList.aspx" target="dmMain"><span>վ�����</span></a></li>
                <li><a href="ParamSet/BigStationList.aspx" target="dmMain"><span>��վ�б�</span></a></li>
                
                
                <li><a href="ParamSet/StationQuickList.aspx" target="dmMain"><span>վ�����б�</span></a></li>
                <li><a href="ParamSet/ChangeStationName.aspx" target="dmMain"><span>����վ��</span></a></li>
                <li><a href="ParamSet/JoinStation.aspx" target="dmMain"><span>ɾ��վ��</span></a></li>
                <li><a href="ParamSet/InsertNewStation.aspx" target="dmMain"><span>����վ��</span></a></li>
                <li><a href="ParamSet/CorpQuickList.aspx" target="dmMain"><span>��˾����б�</span></a></li>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
