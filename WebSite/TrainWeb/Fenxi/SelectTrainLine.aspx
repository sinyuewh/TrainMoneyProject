<%@ Page Title="选择合适的线路" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master"
    AutoEventWireup="true" CodeBehind="SelectTrainLine.aspx.cs" Inherits="WebSite.TrainWeb.SelectTrainLine" %>

<%@ Import Namespace="BusinessRule" %>
<%@ Import Namespace="System.Collections.Generic" %>

<script runat="server">
    
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="/Common/JsLib/PubLib.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function returnLine(chk1) {
            if (chk1.checked) {
                var v1 = chk1.value;
                var pos1 = v1.indexOf("(");
                var v2 = v1.substring(0,pos1);
                var parentid = '<%=Request.QueryString["parent"] %>';
                window.opener.document.getElementById(parentid).value = v2;
                window.close();
            }
        }

        function MulDuanSearch() {
            var id1 = '<%=mulLine.ClientID %>';
            var a1 = document.getElementById('<%=AStation.ClientID %>').value;
            var b1 = document.getElementById('<%=BStation.ClientID %>').value;

            var url1 = "SelectMulTrainLine.aspx?parent=" + id1 + "&astation=" + escape(a1) + "&bstation=" + escape(b1);
            myOpenURLWithScroll(url1, 480, 900);
        }
        
        //提示等待信息
        function ShowWaiting() {
            var msgw, msgh, bordercolor;
            msgw = 400; //提示窗口的宽度
            msgh = 100; //提示窗口的高度
            titleheight = 25   //提示窗口标题高度
            bordercolor = "#336699"; //提示窗口的边框颜色
            titlecolor = "#99CCFF"; //提示窗口的标题颜色

            var sWidth, sHeight;
            sWidth = document.body.offsetWidth; //浏览器工作区域内页面宽度
            sHeight = screen.height; //屏幕高度（垂直分辨率）

            //背景层（大小与窗口有效区域相同，即当弹出对话框时，背景显示为放射状透明灰色）
            var bgObj = document.createElement("div"); //创建一个div对象（背景层）

            //定义div属性，即相当于
            // <div   id="bgDiv"   style="position:absolute;   top:0;   background-color:#777;   filter:progid:DXImagesTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=75);   opacity:0.6;   left:0;   width:918px;   height:768px;   z-index:10000;"> </div>
            bgObj.setAttribute("id", "bgDiv");

            //alert("***********")
            bgObj.style.position = "absolute";
            bgObj.style.top = "0";
            bgObj.style.background = "#777";
            bgObj.style.filter = "progid:DXImageTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=75";
            bgObj.style.opacity = "0.6";
            bgObj.style.left = "0";
            bgObj.style.width = sWidth + "px";
            bgObj.style.height = sHeight + "px";
            bgObj.style.zIndex = "10000";
            bgObj.style.cursor = "wait";
            document.body.appendChild(bgObj); //在body内添加该div对象

            var msgObj = document.createElement("div")//创建一个div对象（提示框层）
            //定义div属性，即相当于
            // <div   id="msgDiv"   align="center"   style="background-color:white;   border:1px   solid   #336699;   position:absolute;   left:50%;   top:50%;   font:12px/1.6em   Verdana,Geneva,Arial,Helvetica,sans-serif;   margin-left:-225px;   margin-top:npx;   width:400px;   height:100px;   text-align:center;   line-height:25px;   z-index:100001;"> </div>
            msgObj.setAttribute("id", "msgDiv");
            msgObj.setAttribute("align", "center");
            msgObj.style.background = "white";
            msgObj.style.border = "1px   solid  " + bordercolor;
            msgObj.style.position = "absolute";
            msgObj.style.left = "50%";
            msgObj.style.top = "50%";
            msgObj.style.font = "12px/1.6em   Verdana,   Geneva,   Arial,   Helvetica,   sans-serif";
            msgObj.style.marginLeft = "-225px";
            msgObj.style.marginTop = -75 + document.documentElement.scrollTop + "px";
            msgObj.style.width = msgw + "px";
            msgObj.style.height = msgh + "px";
            msgObj.style.textAlign = "center";
            msgObj.style.lineHeight = "25px";
            msgObj.style.zIndex = "10001";

            //alert("***********")
            var title = document.createElement("h4"); //创建一个h4对象（提示框标题栏）
            //定义h4的属性，即相当于
            // <h4   id="msgTitle"   align="right"   style="margin:0;   padding:3px;   background-color:#336699;   filter:progid:DXImageTransform.Microsoft.Alpha(startX=20,   startY=20,   finishX=100,   finishY=100,style=1,opacity=75,finishOpacity=100);   opacity:0.75;   border:1px   solid   #336699;   height:18px;   font:12px   Verdana,Geneva,Arial,Helvetica,sans-serif;   color:white;   cursor:pointer;"   onclick=""> 关闭 </h4>
            title.setAttribute("id", "msgTitle");
            title.setAttribute("align", "right");
            title.style.margin = "0";
            title.style.padding = "3px";
            title.style.background = bordercolor;
            title.style.filter = "progid:DXImageTransform.Microsoft.Alpha(startX=20,   startY=20,   finishX=100,   finishY=100,style=1,opacity=75,finishOpacity=100);";
            title.style.opacity = "0.75";
            title.style.border = "1px   solid  " + bordercolor;
            title.style.height = "18px";
            title.style.font = "12px   Verdana,   Geneva,   Arial,   Helvetica,   sans-serif";
            title.style.color = "white";
            title.style.cursor = "pointer";
            //title.innerHTML = "关闭";
            // title.onclick=removeObj;

            document.body.appendChild(msgObj); //在body内添加提示框div对象msgObj
            document.getElementById("msgDiv").appendChild(title); //在提示框div中添加标题栏对象title

            var txt = document.createElement("p"); //创建一个p对象（提示框提示信息）
            //定义p的属性，即相当于
            // <p   style="margin:1em   0;"   id="msgTxt"> 测试效果 </p>
            txt.style.margin = "1em"
            txt.setAttribute("id", "msgTxt");
            txt.innerHTML = "正在执行线路搜索（可能需要1-5分钟），请等待。。。"; //来源于函数调用时的参数值
            document.getElementById("msgDiv").appendChild(txt); //在提示框div中添加提示信息对象txt
        }

        function CloseWaiting() {
            // document.getElementById('doing').style.visibility = 'hidden';


        }
        function MyOnload() {
            // document.getElementById('doing').style.visibility = 'hidden';
            var bgObj = document.getElementById("bgDiv");
            if (bgObj != null && bgObj != undefined) {
                document.body.removeChild(bgObj); //删除背景层Div
                // document.getElementById("msgDiv").removeChild(title); //删除提示框的标题栏
                // document.body.removeChild(msgObj); //删除提示框层
                document.body.style.cursor = "default";
            }
        }
        if (window.onload == null) {
            window.onload = MyOnload;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="cursor: default">
        <tr>
            <td height="30" background="../images/tab_05.gif">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="12" height="30">
                            <img src="../images/tab_03.gif" width="12" height="30" />
                        </td>
                        <td>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="46%" valign="middle">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="5%">
                                                    <div align="center">
                                                        <img src="../images/tb.gif" width="16" height="16" /></div>
                                                </td>
                                                <td width="95%" class="STYLE1">
                                                    <span class="STYLE3" style="display: none">你当前的位置：</span>选择合适的线路
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="54%">
                                        <table border="0" align="right" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td width="60">
                                                    <table width="90%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td class="STYLE1">
                                                                <div align="center">
                                                                </div>
                                                            </td>
                                                            <td class="STYLE1">
                                                                <div align="center">
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="16">
                            <img src="../images/tab_07.gif" width="16" height="30" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="8" background="../images/tab_12.gif">
                            &nbsp;
                        </td>
                        <td>
                            <!--数据查询区-->
                            <table class="DetailTable" cellspacing="1" cellpadding="0" style="width: 99%">
                                <tr>
                                    <td class="Caption" style="width: 10%">
                                        起始站：
                                    </td>
                                    <td class="Data" style="width: 15%">
                                        &nbsp;<asp:TextBox ID="AStation" runat="server" Width="100"></asp:TextBox>
                                    </td>
                                    <td class="Caption" style="width: 10%">
                                        终点站：
                                    </td>
                                    <td class="Data" style="width: 20%">
                                        &nbsp;<asp:TextBox ID="BStation" runat="server" Width="105"></asp:TextBox>
                                    </td>
                                    <td class="Caption" style="width: 10%">
                                        中间站：
                                    </td>
                                    <td class="Data" style="width: 33%">
                                        <asp:TextBox ID="middleStation" runat="server" Width="380"></asp:TextBox>
                                        <asp:CheckBoxList ID="trainList" runat="server" Visible="false" RepeatDirection="Horizontal"
                                            RepeatColumns="2" Width="300">
                                        </asp:CheckBoxList>
                                    </td>
                                    <td class="Data" style="width: 10%; text-align: center">
                                        <asp:Button ID="Button1" runat="server" ToolTip="点按钮开始搜索线路" Text="开始搜索"  />
                                    </td>
                                </tr>
                                <tr id="row2" runat="server">
                                    <td class="Caption" style="width: 10%">
                                        分段检索：
                                    </td>
                                    <td colspan="5" class="Data" align="left">
                                        <table style="width: 100%" border="0" align="left">
                                            <tr>
                                                <td style="width: 100%">
                                                    <asp:TextBox ID="mulLine" runat="server" Width="100%"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="Data" style="width: 10%; text-align: center">
                                        <asp:Button ID="Button3" runat="server" ToolTip="分段搜索线路，适合搜索较长的线路" Text="分段检索" OnClientClick="javascript:MulDuanSearch();return false;" />
                                    </td>
                                </tr>
                            </table>
                            <!--数据列表区-->
                            <div id="SearchInfo" runat="server" visible="false">
                                <table class="ListTable" cellspacing="1" id="SearchDataTable" cellpadding="0" style="width: 99%;
                                    margin-top: 10px">
                                    <tr>
                                        <td class="Caption"  style ="width:100">
                                            选择
                                        </td>
                                        <td class="Caption">
                                            线路
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="Repeater1" runat="server" EnableViewState="false">
                                        <ItemTemplate>
                                            <!--显示的数据1-->
                                            <tr>
                                                <td class="Data">
                                                    <input id="selLine" name ="selLine" type="radio" onclick="javascript:returnLine(this);" value ='<%#Container.DataItem %>' /> 
                                                </td>
                                                <td class="Data" align ="left" style ="padding-left:5px; padding-right:5px; text-align:left ">
                                                    <%#Container.DataItem %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                            <!--Report Data-->
                        </td>
                        <td width="8" background="../images/tab_15.gif">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
