<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeeWordFile.aspx.cs" Inherits="WebSite.TrainWeb.Fenxi.SeeWordFile" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            margin: 0 0 0 0;
            overflow: hidden;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function myLoad() {
            var url = document.getElementById("wordUrl").value;
            var type1 = document.getElementById("wordExt").value;
            document.getElementById("WebOffice1").OptionFlag = 0x0400;
            flag = document.getElementById("WebOffice1").LoadOriginalFile(url, type1);
            if (flag == 0) {
                alert("启动Office失败，请检查 Microsoft Office 是否正确安装后重试！");
            }
            else {
                document.getElementById("WebOffice1").ShowToolBar = 0;
                doc1 = new Object(document.getElementById("WebOffice1").GetDocumentObject());

                //1--设置收入列表
                var sheet1 = doc1.Worksheets(1);
                //设置抬头
                sheet1.Cells(1, 1) = document.getElementById("as1").value + "-" +
                                     document.getElementById("bs1").value + "列车开行收入一览表";

                //设置车型和里程
                sheet1.Cells(2, 2) = document.getElementById("traintype").value;
                sheet1.Cells(2, 4) = document.getElementById("totalmiles").value + "(公里)";

                //设置编组
                sheet1.Cells(4, 2) = document.getElementById("yz").value;
                sheet1.Cells(5, 2) = document.getElementById("yw").value;
                sheet1.Cells(8, 2) = document.getElementById("rw").value;
                sheet1.Cells(10, 2) = document.getElementById("sy").value;

                //设置票价
                sheet1.Cells(4, 5) = document.getElementById("p1").value;
                
                sheet1.Cells(5, 5) = document.getElementById("p2").value;
                sheet1.Cells(6, 5) = document.getElementById("p3").value;
                sheet1.Cells(7, 5) = document.getElementById("p4").value;
                
                sheet1.Cells(8, 5) = document.getElementById("p5").value;
                sheet1.Cells(9, 5) = document.getElementById("p6").value;
                
                sheet1.Cells(10, 5) = document.getElementById("p2").value;
                sheet1.Cells(11, 5) = document.getElementById("p3").value;
                sheet1.Cells(12, 5) = document.getElementById("p4").value;
                
                

                //设置定员
                sheet1.Cells(4, 3) = document.getElementById("m1").value;
                sheet1.Cells(5, 3) = document.getElementById("m2").value;
                sheet1.Cells(8, 3) = document.getElementById("m3").value;
                sheet1.Cells(10, 3) = document.getElementById("m4").value;

                //2--设置支出
                var sheet2 = doc1.Worksheets(2);
                //设置抬头
                sheet2.Cells(1, 1) = document.getElementById("as1").value + "-" +
                                     document.getElementById("bs1").value + "列车开行支出一览表";
                //设置车型和里程
                sheet2.Cells(2, 2) = document.getElementById("traintype").value;
                sheet2.Cells(2, 4) = document.getElementById("totalmiles").value + "(公里)";

                for (var i = 3; i <= 6; i++) {
                    for (var j = 3; j <= 15; j++) {
                        sheet2.Cells(j + 2, i) = document.getElementById("f" + i + "_" + j).value;
                    }
                }

                //设置局内线路使用费、局内旅客服务费、局内售票服务费
                sheet2.Cells(21, 3) = document.getElementById("jfee1").value;
                sheet2.Cells(22, 3) = document.getElementById("jfee2").value;
                sheet2.Cells(23, 3) = document.getElementById("jfee3").value;

                sheet2.Cells(21, 4) = document.getElementById("jfee4").value;
                sheet2.Cells(22, 4) = document.getElementById("jfee5").value;
                sheet2.Cells(23, 4) = document.getElementById("jfee6").value;

                sheet2.Cells(21, 5) = document.getElementById("jfee7").value;
                sheet2.Cells(22, 5) = document.getElementById("jfee8").value;
                sheet2.Cells(23, 5) = document.getElementById("jfee9").value;

                sheet2.Cells(21, 6) = document.getElementById("jfee10").value;
                sheet2.Cells(22, 6) = document.getElementById("jfee11").value;
                sheet2.Cells(23, 6) = document.getElementById("jfee12").value;

                //设置轮渡费
                sheet2.Cells(18, 3) = document.getElementById("shipfee").value;
                sheet2.Cells(18, 4) = document.getElementById("shipfee").value;
                sheet2.Cells(18, 5) = document.getElementById("shipfee").value;
                sheet2.Cells(18, 6) = document.getElementById("shipfee").value;

                //设置间接分摊费
                sheet2.Cells(19, 3) = document.getElementById("jianjiefee").value;
                sheet2.Cells(19, 4) = document.getElementById("jianjiefee").value;
                sheet2.Cells(19, 5) = document.getElementById("jianjiefee").value;
                sheet2.Cells(19, 6) = document.getElementById("jianjiefee").value;
            }
        }

        //屏蔽Word的菜单和按钮
        function HiddenMenuAndBar() {
            //屏蔽标准工具栏的前几个按钮
            document.getElementById("WebOffice1").SetToolBarButton2("Standard", 3, 1);
            document.getElementById("WebOffice1").SetKeyCtrl(595, -1, 0);
        }

        //恢复菜单和Bar
        function RestoreMenuAndBar() {
            //恢复被屏蔽的菜单项和快捷键
            document.getElementById("WebOffice1").SetToolBarButton2("Standard", 1, 3);
            document.getElementById("WebOffice1").SetToolBarButton2("Standard", 2, 3);
            document.getElementById("WebOffice1").SetToolBarButton2("Standard", 3, 3);
            document.getElementById("WebOffice1").SetToolBarButton2("Standard", 6, 3);

            //恢复文件菜单项
            document.getElementById("WebOffice1").SetToolBarButton2("Menu Bar", 1, 4);
            //恢复 保存快捷键(Ctrl+S) 
            document.getElementById("WebOffice1").SetKeyCtrl(595, 0, 0);
            //恢复 打印快捷键(Ctrl+P) 
            document.getElementById("WebOffice1").SetKeyCtrl(592, 0, 0);

            //恢复文件保存
            document.getElementById("WebOffice1").SetSecurity(0x02 + 0x8000);
        }

        //关闭窗口
        function myUnload() {
            RestoreMenuAndBar();
            document.getElementById("WebOffice1").CloseDoc(0);
            document.getElementById("WebOffice1").close();
        }

        function SaveDocument() {
            document.getElementById("WebOffice1").ShowDialog(86);
        }
    </script>

</head>
<body onload="javascript:myLoad();" onunload="javascript:myUnload();">
    <form id="form1" runat="server">
    <div style="text-align: center;">
        <table width="100%" align="center" cellpadding="1" cellspacing="1" height="100%"
            border="0">
            <tr bgcolor="#f0f8ff" height="96%">
                <td align="center">

                    <script src="loadweboffice.js" type="text/javascript"></script>

                </td>
            </tr>
        </table>
        <br />
        <br />
        <input id="wordUrl" type="hidden" name="wordUrl" runat="server" />
        <input id="wordExt" type="hidden" name="wordExt" runat="server" />
        <input id="filename" type="hidden" name="filename" runat="server" />
        
        <input id="yz" type="hidden" name="yz" runat="server" />
        <input id="yw" type="hidden" name="yw" runat="server" />
        <input id="rw" type="hidden" name="rw" runat="server" />
        <input id="ca" type="hidden" name="ca" runat="server" />
        <input id="sy" type="hidden" name="sy" runat="server" />
        
        <input id="traintype" type="hidden" name="traintype" runat="server" />
        <input id="totalmiles" type="hidden" name="totalmiles" runat="server" />
        
        
        <input id="p1" type="hidden" runat="server" />
        <input id="p2" type="hidden" runat="server" />
        <input id="p3" type="hidden" runat="server" />
        <input id="p4" type="hidden" runat="server" />
        <input id="p5" type="hidden" runat="server" />
        <input id="p6" type="hidden" runat="server" />
        <input id="m1" type="hidden" runat="server" value="0" />
        <input id="m2" type="hidden" runat="server" value="0" />
        <input id="m3" type="hidden" runat="server" value="0" />
        <input id="m4" type="hidden" runat="server" value="0" />
        
        <input id="f3_3" type="hidden" runat="server" value="0" />
        <input id="f3_4" type="hidden" runat="server" value="0" />
        <input id="f3_5" type="hidden" runat="server" value="0" />
        <input id="f3_6" type="hidden" runat="server" value="0" />
        <input id="f3_7" type="hidden" runat="server" value="0" />
        <input id="f3_8" type="hidden" runat="server" value="0" />
        <input id="f3_9" type="hidden" runat="server" value="0" />
        <input id="f3_10" type="hidden" runat="server" value="0" />
        <input id="f3_11" type="hidden" runat="server" value="0" />
        <input id="f3_12" type="hidden" runat="server" value="0" />
        <input id="f3_13" type="hidden" runat="server" value="0" />
        <input id="f3_14" type="hidden" runat="server" value="0" />
        <input id="f3_15" type="hidden" runat="server" value="0" />
        <input id="f3_16" type="hidden" runat="server" value="0" />
        <input id="f4_3" type="hidden" runat="server" value="0" />
        <input id="f4_4" type="hidden" runat="server" value="0" />
        <input id="f4_5" type="hidden" runat="server" value="0" />
        <input id="f4_6" type="hidden" runat="server" value="0" />
        <input id="f4_7" type="hidden" runat="server" value="0" />
        <input id="f4_8" type="hidden" runat="server" value="0" />
        <input id="f4_9" type="hidden" runat="server" value="0" />
        <input id="f4_10" type="hidden" runat="server" value="0" />
        <input id="f4_11" type="hidden" runat="server" value="0" />
        <input id="f4_12" type="hidden" runat="server" value="0" />
        <input id="f4_13" type="hidden" runat="server" value="0" />
        <input id="f4_14" type="hidden" runat="server" value="0" />
        <input id="f4_15" type="hidden" runat="server" value="0" />
        <input id="f4_16" type="hidden" runat="server" value="0" />
        <input id="f5_3" type="hidden" runat="server" value="0" />
        <input id="f5_4" type="hidden" runat="server" value="0" />
        <input id="f5_5" type="hidden" runat="server" value="0" />
        <input id="f5_6" type="hidden" runat="server" value="0" />
        <input id="f5_7" type="hidden" runat="server" value="0" />
        <input id="f5_8" type="hidden" runat="server" value="0" />
        <input id="f5_9" type="hidden" runat="server" value="0" />
        <input id="f5_10" type="hidden" runat="server" value="0" />
        <input id="f5_11" type="hidden" runat="server" value="0" />
        <input id="f5_12" type="hidden" runat="server" value="0" />
        <input id="f5_13" type="hidden" runat="server" value="0" />
        <input id="f5_14" type="hidden" runat="server" value="0" />
        <input id="f5_15" type="hidden" runat="server" value="0" />
        <input id="f5_16" type="hidden" runat="server" value="0" />
        <input id="f6_3" type="hidden" runat="server" value="0" />
        <input id="f6_4" type="hidden" runat="server" value="0" />
        <input id="f6_5" type="hidden" runat="server" value="0" />
        <input id="f6_6" type="hidden" runat="server" value="0" />
        <input id="f6_7" type="hidden" runat="server" value="0" />
        <input id="f6_8" type="hidden" runat="server" value="0" />
        <input id="f6_9" type="hidden" runat="server" value="0" />
        <input id="f6_10" type="hidden" runat="server" value="0" />
        <input id="f6_11" type="hidden" runat="server" value="0" />
        <input id="f6_12" type="hidden" runat="server" value="0" />
        <input id="f6_13" type="hidden" runat="server" value="0" />
        <input id="f6_14" type="hidden" runat="server" value="0" />
        <input id="f6_15" type="hidden" runat="server" value="0" />
        <input id="f6_16" type="hidden" runat="server" value="0" />
        
        <input id="as1" type="hidden" runat="server" value="起始站" />
        <input id="bs1" type="hidden" runat="server" value="终点站" />
        
        <input id="jfee1" type="hidden" runat="server" value="0" />
        <input id="jfee2" type="hidden" runat="server" value="0" />
        <input id="jfee3" type="hidden" runat="server" value="0" />
        
        <input id="jfee4" type="hidden" runat="server" value="0" />
        <input id="jfee5" type="hidden" runat="server" value="0" />
        <input id="jfee6" type="hidden" runat="server" value="0" />

        <input id="jfee7" type="hidden" runat="server" value="0" />
        <input id="jfee8" type="hidden" runat="server" value="0" />
        <input id="jfee9" type="hidden" runat="server" value="0" />

        <input id="jfee10" type="hidden" runat="server" value="0" />
        <input id="jfee11" type="hidden" runat="server" value="0" />
        <input id="jfee12" type="hidden" runat="server" value="0" />
        
        <input id="shipfee" type="hidden" runat="server" value="0" />
        <input id="jianjiefee" type="hidden" runat="server" value="0" />
        
        <br />
        <br />
    </div>
    </form>
</body>
</html>
