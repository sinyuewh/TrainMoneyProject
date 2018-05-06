<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" StylesheetTheme="" %>

<html>
<head runat="server">
    <title></title>
    <link href="dtree.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Common/JsLib/dtree.js"></script>

</head>
<body>
    <form id="form1" runat="server" style="height: 100%">
    <table style="width: 100%; height: 100%">
        <tr>
            <td>
                <div class="MenuDiv">
                    <table style="width: 100%">
                        <tr>
                            <td style="height: 20px" class="MenuCaption">
                                功能栏目<br>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="margin-top: 2px; margin-left: 5px">

                                    <script language="javascript" type="text/javascript">
                                        function menu1() {
                                            dht2 = new dTree('dht2');
                                            dht2.add(0, -1, '<span style=\'font-size:11pt;\'>开发平台1.0</span>');

                                            /////////////////////////////////////////////////////////////////////////////
                                            dht2.add(3, 0, '数据管理', '', '');
                                            dht2.add(31, 3, '数据源字符串', '/Designer/DataMng/DataSourceString.aspx', '', 'view');
                                            ///////////////////////////////////////////////////////////////////////////////

                                            dht2.add(4, 0, '代码管理', '', '');
                                            dht2.add(40, 4, '模块分组', '/Designer/CodeMng/ModelKindList.aspx', '', 'view');
                                            dht2.add(41, 4, '系统模块', '/Designer/CodeMng/SysModelList.aspx', '', 'view');
                                            dht2.add(42, 4, '业务表管理', '/Designer/CodeMng/BusinessTableList.aspx', '', 'view');
                                            dht2.add(43, 4, '业务层代码', '/Designer/CodeMng/BusinessLayerCode.aspx', '', 'view');
                                            
                                            //////////////////////////////////////////////////////////////////////////////
                                            dht2.add(10, 0, '系统支撑', '', '');
                                            dht2.add(101, 10, '数据字典', '/Designer/SysMng/ItemList.aspx', '数据字典列表', 'view');
                                            dht2.add(102, 10, 'SQL语句', '/Designer/SysMng/SqlInfoList.aspx', 'SQL语句列表', 'view');
                                             dht2.add(103, 10, '字符串资源', '/Designer/SysMng/StrInfoList.aspx', '字符串资源', 'view');
                                            dht2.add(104, 10, '系统角色', '/Designer/SysMng/RoleList.aspx', '系统角色', 'view');
                                            dht2.add(105, 10, '权限功能点', '/Designer/SysMng/AuthorityList.aspx', '权限功能点', 'view');
                                           
                                            //////////////////////////////////////////////////////////////////////////////

                                            document.write(dht2);
                                            dht2.openAll();

                                        }
                                        menu1();
                                    </script>

                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
