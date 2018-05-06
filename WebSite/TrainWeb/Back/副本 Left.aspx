<%@ Page Language="C#" AutoEventWireup="true" StylesheetTheme="" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title></title>
    <style type="text/css">
        body
        {
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
        }
        .navPoint
        {
            color: white;
            cursor: hand;
            font-family: Webdings;
            font-size: 12px;
        }
        
        a:active {
	        font-size:12px;
	        line-height: 20px;
	        font-weight: normal;
	        color: black;
	        text-decoration:none;
        }
        a:link {
	        font-size:12px;
	        line-height: 20px;
	        font-weight: normal;
	        color: black;
	        text-decoration:none;
        }
        a:visited {
	        font-size:12px;
	        line-height: 20px;
	        font-weight: normal;
	        text-decoration:none;
	        color: black;
        }
        a:hover {
	        font-size:12px;
	        line-height: 20px;
	        font-weight: normal;
	        color: black;
	        text-decoration:underline;
        }
        
        img {
	        border-width:0;
        }
    </style>

    <script type="text/javascript">
        function switchSysBar() {

            var locate = location.href.replace('left.aspx', '');
            var ssrc = document.getElementById("img1").src.replace(locate, '');
            if (ssrc == "images/main_41.gif") {
                document.getElementById("img1").src = "images/main_41_1.gif";
                parent.frame.cols = "18,*";
                document.getElementById("frmTitle").style.display = "none"
            }
            else {
                document.getElementById("img1").src = "images/main_41.gif"
                parent.frame.cols = "188,*";
                document.getElementById("frmTitle").style.display = ""
            }
        }
        function closewin() {
            if (opener != null && !opener.closed) {
                opener.window.newwin = null;
                opener.openbutton.disabled = false;
                opener.closebutton.disabled = true;
            }
        }


        var count = 0; //做计数器
        var limit = new Array(); //用于记录当前显示的哪几个菜单
        var countlimit = 1; //同时打开菜单数目，可自定义
        function expandIt(el) {
            obj = document.getElementById("tab" + el);
            if (obj.style.display == "none") {
                obj.style.display = "block"; //显示子菜单
                if (count < countlimit) {//限制2个
                    limit[count] = el; //录入数组
                    count++;
                }
                else {
                    eval("tab" + limit[0]).style.display = "none";
                    for (i = 0; i < limit.length - 1; i++) { limit[i] = limit[i + 1]; } //数组去掉头一位，后面的往前挪一位
                    limit[limit.length - 1] = el;
                }
            }
//            else {
//                obj.style.display = "none";
//                var j;
//                for (i = 0; i < limit.length; i++) { if (limit[i] == el) j = i; } //获取当前点击的菜单在limit数组中的位置
//                for (i = j; i < limit.length - 1; i++) { limit[i] = limit[i + 1]; } //j以后的数组全部往前挪一位
//                limit[limit.length - 1] = null; //删除数组最后一位
//                count--;
//            }
        }
    </script>
    
    <link href="dtree.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Common/JsLib/dtree.js"></script>
</head>
<body onunload="closewin()" onload="expandIt('1')">
    <form id="form1" runat="server">
    <div>
     <table height="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#e5f4fd">
        <tr>
            <td valign="top" id="frmTitle" nowrap name="fmTitle" width="171">
                <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0" style="table-layout: fixed;">
                    <tr height="100%">
                        <td style="width: 3px; background: #0a5c8e;">
                            &nbsp;
                        </td>
                        <td valign="top">
                            <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0" style="table-layout: fixed;">
                                <tr>
                                    <td height="5" style="line-height: 5px; background: #0a5c8e;">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr onclick="expandIt('1')">
                                    <td height="23" background="images/main_29.gif">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="40%">
                                                    &nbsp;
                                                </td>
                                                <td width="42%">
                                                    <font style="height: 1; font-size: 12px; color: #bfdbeb; filter: glow(color=#1070a3,strength=1)">
                                                        系统导航</font>
                                                </td>
                                                <td width="18%">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                
                                <tr id="tab1"  style="display:none; height:100%;">
                                    <td bgcolor="#e5f4fd"  valign="top">
                                       
                                        <div style="margin-top: 5px; margin-left: 5px">

                                    <script language="javascript" type="text/javascript">
                                        function menu1() {
                                            dht2 = new dTree('dht2');
                                            dht2.add(0, -1, '<span style=\'font-size:12px;\'>财务模型系统</span>');

                                            ///////////////////////////////////////////////////////////////////////////////

                                            dht2.add(4, 0, '财务数据模型', '', '');
                                            dht2.add(41, 4, '列车列表', 'Train/NewTrainList.aspx', '', 'view');
                                            dht2.add(42, 4, '担当车收入', 'Train/NewTrainShouRuList.aspx', '', 'view');
                                            dht2.add(43, 4, '线路使用费', 'Train/NewTrainXianLuFeeList.aspx', '', 'view');
                                            dht2.add(44, 4, '机车牵引费', 'Train/NewTrainQianYinFeeList.aspx', '', 'view');
                                            
                                            //dht2.add(42, 4, '普通列车1', 'Train/TrainList1.aspx', '', 'view');
                                            //dht2.add(43, 4, '动车列表2', 'Train/TrainList2.aspx', '', 'view');
                                            
                                            dht2.add(45, 4, '列车线路', 'Train/TrainLineList.aspx', '', 'view');
                                            dht2.add(46, 4, '站点别名', 'Train/StationAliasList.aspx', '', 'view');
                                            dht2.add(47, 4, '沿途水站', 'Train/StationAliasList.aspx', '', 'view');
                                            dht2.add(48, 4, '局内站点', 'Train/StationAliasList.aspx', '', 'view');
                                            dht2.add(49, 4, '新增车次分析', 'Fenxi/NewCheCiFenxi.aspx', '', 'view');
                                            dht2.add(50, 4, '既有车次分析', 'Fenxi/OldCheCiFenxi.aspx', '', 'view');
                                            
                                            /////////////////////////////////////////////////////////////////////////////
                                            dht2.add(3, 0, '系统配置', '', '');
                                            dht2.add(31, 3, '基础数据', 'SystemProfile/BaseDataList.aspx', '', 'view');
                                            dht2.add(32, 3, '普通列车车厢配置', 'SystemProfile/CommTrainCheXianProfile.aspx', '', 'view');
                                            dht2.add(33, 3, '里程区段对应表', 'SystemProfile/LiChengQuDuanProfile.aspx', '', 'view');
                                            dht2.add(34, 3, '区段票价递减表', 'SystemProfile/QuDuanPiaoJiDiJian.aspx', '', 'view');       
                                            dht2.add(35, 3, '列车加快费率', 'SystemProfile/JiaKuaiProfile.aspx', '', 'view');

                                            dht2.add(37, 3, '车厢重量和成本', 'SystemProfile/CheXianWeightProfile.aspx', '', 'view');
                                            dht2.add(36, 3, '机车牵引费', 'SystemProfile/QianYinFeeProfile.aspx', '', 'view');
                                            dht2.add(38, 3, '线路使用费', 'SystemProfile/LineProfile.aspx', '', 'view');
                                            
                                            dht2.add(39, 3, '动车票价和定员', 'SystemProfile/HighTrainProfile.aspx', '', 'view');
                                            dht2.add(390, 3, '电费和接触网费', 'SystemProfile/TrainLineKindProfile.aspx', '', 'view');

                                            /////////////////////////////////////////////////////////////////////////////
                                            dht2.add(5, 0, '系统管理', '', '');
                                            dht2.add(51, 5, '用户管理', 'javascript:void(0);', '', 'view');
                                            dht2.add(52, 5, '更新日志　', 'javascript:void(0);', '', 'view');
                                            dht2.add(53, 5, '数据库备份与恢复', 'javascript:void(0);', '', 'view');
                                            
                                            
                                            
                                            //////////////////////////////////////////////////////////////////////////////
                                            //dht2.add(10, 0, '系统管理', '', '');
                                            //dht2.add(101, 10, '系统用户', '#', '数据字典列表', 'view');
                                            //dht2.add(102, 10, '系统角色', '#', '系统角色', 'view');
                                            //dht2.add(103, 10, '系统权限', '#', '系统权限', 'view');
                           
                                            //////////////////////////////////////////////////////////////////////////////

                                            document.write(dht2);
                                            dht2.openAll();

                                        }
                                        menu1();
                                    </script>

                                </div>
                                    </td>
                                </tr>
                                
                                <%if (false)
                                  { %>
                                <tr onclick="expandIt('2')">
                                    <td height="25" background="images/main_43.gif">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="37%">
                                                    &nbsp;
                                                </td>
                                                <td width="45%">
                                                    <font style="height: 1; font-size: 12px; color: #bfdbeb; filter: glow(color=#1070a3,strength=1)">
                                                        方法浏览</font>
                                                </td>
                                                <td width="18%">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="tab2"  style="display: none">
                                    <td bgcolor="#e5f4fd">
                                        <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td valign="top">
                                                    <div align="center">
                                                        <img src="images/tree.gif" width="117" height="189"></div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>                                
                                <tr onclick="expandIt('3')">
                                    <td height="25" background="images/main_43.gif">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="37%">
                                                    &nbsp;
                                                </td>
                                                <td width="45%">
                                                    <font style="height: 1; font-size: 12px; color: #bfdbeb; filter: glow(color=#1070a3,strength=1)">
                                                        其他菜单</font>
                                                </td>
                                                <td width="18%">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="tab3" style="display: none">
                                    <td bgcolor="#e5f4fd">
                                        <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td valign="top">
                                                    <div align="center">
                                                    <ul style=" font-size:15px">
                                                    <li>其他菜单</li>
                                                    <li>其他菜单</li>
                                                    <li>其他菜单</li>
                                                    </ul>
                                                        </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr><%} %>
                            </table>
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="width: 3px; background: #0a5c8e;">
                            &nbsp;
                        </td>
                        <td>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" style="table-layout: fixed;">
                                <tr>
                                    <td height="23" background="images/main_45.gif">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="18%">
                                                    &nbsp;
                                                </td>
                                                <td width="64%">
                                                    <div align="center">
                                                        <font style="height: 1; font-size: 12px; color: #bfdbeb; filter: glow(color=#1070a3,strength=1)">
                                                            版本2011 V1.0 </font>
                                                    </div>
                                                </td>
                                                <td width="18%">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="9" style="line-height: 9px; background: #0a5c8e;">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="9" valign="middle" bgcolor="#0a5c8e" onclick="switchSysBar()">
                <span class="navPoint" id="switchPoint" title="关闭/打开左栏">
                    <img src="images/main_41.gif" name="img1" width="9" height="52" id="img1" /></span>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
