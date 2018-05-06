<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="NewCheCiFenXi.aspx.cs" Inherits="WebSite.TrainWeb.Fenxi.NewCheCiFenXi2013" %>

<%@ Import Namespace="BusinessRule" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Register Src="../../Include/WaitResult.ascx" TagName="WaitResult" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="/Common/JsLib/PubLib.js" type="text/javascript"></script>
    <script src="/Common/JsLib/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="/Common/JsLib/jquery.autocomplete.js" type="text/javascript"></script>
    
    <script language="javascript" type="text/javascript">        $(document).ready(function() {
            $("#<%=AStation.ClientID %>").autocomplete("/Handler/GetStationNameValue.ashx?AutoKind=GetName",
                {
                    width: $(this).css("width"),
                    selectFirst: false,
                    max: 10,
                    scroll: false,
                    extraParams: { 'txtValue': function() {
                    return $("#<%=AStation.ClientID %>").val();
                    }
                    }
                });

            $("#<%=BStation.ClientID %>").autocomplete("/Handler/GetStationNameValue.ashx?AutoKind=GetName",
            {
                width: $(this).css("width"),
                selectFirst: false,
                max: 10,
                scroll: false,
                extraParams: { 'txtValue': function() {
                return $("#<%=BStation.ClientID %>").val();
                }
                }
            });

            $("#<%=middleStation.ClientID %>").autocomplete("/Handler/GetStationNameValue.ashx?AutoKind=GetName",
            {
                width: $(this).css("width"),
                selectFirst: false,
                max: 10,
                scroll: false,
                extraParams: { 'txtValue': function() {
                return $("#<%=middleStation.ClientID %>").val();
                }
                }
            });
        });

        
        function ShowZhiChu(zc1) {
            var url1 = "ShowZhiChuMingXi.aspx?zc=" + zc1;
            var height1 = 500;
            var width1 = 400;
            var top1 = (window.screen.availHeight - height1) / 2;
            var left1 = (window.screen.availWidth - width1) / 2;
            window.open(url1, "", "location=no,Status=no,scrollbars=no,resizable=no,left="
                            + left1 + ",top=" + top1 + ",width=" + width1 + ",height=" + height1);
        }

        function HideWindow() {
            var win1 = top.frames["leftFrame"];
            if (win1 != null) {
                win1.document.getElementById("img1").src = "images/main_41_1.gif";
                win1.parent.frame.cols = "18,*";
                win1.document.getElementById("frmTitle").style.display = "none"
            }
        }

        function Select() {
            var v1 = "CostChoose.aspx";
            var u1 = myShowModalDialog(v1, 780, 650);

            if (u1 != undefined && u1 != "") {
            };
            return u1;
        }

        function SelectSpecialTrainLine() {
            var id1 = '<%=selTrain.ClientID %>';
           
            var url1 = "../Train/SelectTrainLine.aspx?parent=" + id1;
            myOpenURLWithScroll(url1, 600, 900);
        }

        function MulDuanSearch() {
            var id1 = '<%=mulLine.ClientID %>';
            var a1 = document.getElementById('<%=AStation.ClientID %>').value;
            var b1 = document.getElementById('<%=BStation.ClientID %>').value;

            var url1 = "SelectMulTrainLine.aspx?parent=" + id1 + "&astation=" + escape(a1) + "&bstation=" + escape(b1);
            myOpenURLWithScroll(url1, 480, 900);
        }

        function DaoChuData() {
            alert("Test");
        }

        function ShowTopWaiting() {
            var win1 = top.frames["top"];
            win1.frames[0].location.href = "/TrainWeb/top1.aspx";
            var win2 = top.frames["menu"];
            win2.location.href = "/TrainWeb/left1.aspx";
        }

        //选择既有线路分析
        function SelectOldLine() {
            var a1 = document.getElementById('<%=AStation.ClientID %>').value;
            var b1 = document.getElementById('<%=BStation.ClientID %>').value;
            var p1 = '<%=mulLine.ClientID %>';
           
            if (a1 != "" && b1 != "") {

                var url1 = "/TrainWeb/Train/SelectOldLines.aspx?parentid=" + p1 + "&astation=" + escape(a1) + "&bstation=" + escape(b1);
                myOpenURLWithScroll(url1, 600, 1300);
            }
            else {
                alert("错误：请输入【起始站】和【终点站】的值（不支持别名）！");
            }

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

            //页面转向，禁止用户点鼠标
            var win1 = top.frames["top"];
            win1.frames[0].location.href = "/TrainWeb/top1.aspx";
            var win2 = top.frames["menu"];
            win2.location.href = "/TrainWeb/left1.aspx";
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
            var win1 = top.frames["top"];
            win1.frames[0].location.href = "/TrainWeb/top.aspx";
            var win2 = top.frames["menu"];
            win2.location.href = "/TrainWeb/left2.aspx";
        }
        else {
            
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
                                                    <span class="STYLE3" style="display: none">你当前的位置：</span><asp:Label ID="titleInfo1"
                                                        runat="server"></asp:Label>
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
                                    <td class="Data" style="width: 23%">
                                        &nbsp;<asp:TextBox ID="AStation" runat="server" Width="100" Text="武汉"></asp:TextBox>
                                    </td>
                                    <td class="Caption" style="width: 10%">
                                        终点站：
                                    </td>
                                    <td class="Data" style="width: 29%">
                                        &nbsp;<asp:TextBox ID="BStation" runat="server" Width="105" Text="广州"></asp:TextBox>
                                    </td>
                                    <td class="Caption" style="width: 8%">
                                        中间站：
                                    </td>
                                    <td class="Data" style="width: 33%">
                                        <asp:TextBox ID="middleStation" runat="server" Width="330"></asp:TextBox>
                                        <asp:CheckBoxList ID="trainList" runat="server" Visible="false" RepeatDirection="Horizontal"
                                            RepeatColumns="2" Width="300">
                                        </asp:CheckBoxList>
                                    </td>
                                    <td class="Data" style="width: 10%; text-align: left">
                                        <asp:Button ID="Button1" runat="server" ToolTip="点按钮开始分析线路" ForeColor="Red" Text="开始分析"
                                         OnClientClick="javascript:HideWindow();"/>
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
                                                    <asp:TextBox ID="selTrain" runat="server" Width="95%" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="mulLine" runat="server" Width="100%" TextMode="MultiLine" Height="40"></asp:TextBox>
                                                    <asp:CheckBox ID="ChkShowFullLine" runat="server" Visible="false" Text="显示完整的线路" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="Data" style="width: 10%; text-align: left; line-height: 150%">
                                        <asp:Button ID="Button3" runat="server" ToolTip="分段搜索线路，适合搜索较长的线路（不保存搜索结果）" Text="分段检索"
                                            OnClientClick="javascript:MulDuanSearch();return false;" />
                                        <br />
                                        <asp:Button ID="Button4" runat="server" Text="既有车次" Style="margin-top: 3px" OnClientClick="javascript:SelectOldLine();return false;"
                                            ToolTip="选择既有车次的线路" />
                                        <asp:Button ID="Button2" runat="server" Text="导出" Visible="false" ToolTip="导出数据到Excel"
                                            OnClientClick="javascript:DaoChuData();return false;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Caption" style="width: 10%">
                                        成本选项：
                                    </td>
                                    <td class="Data" style="width: 23%">
                                        <asp:RadioButtonList ID="cb" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" RepeatLayout="Flow">
                                            <asp:ListItem Text="变动成本" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="全成本" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>&nbsp;&nbsp;&nbsp;&nbsp;
                                         <asp:Button ID="SetSCBtn" runat="server" ToolTip="选择成本选项"  Text="选择成本选项" Width="80"
                                         OnClientClick="javascript:var str = Select(); $(this).next().val(str);"/>
                                         <asp:HiddenField ID="textfind" runat="server" />
                                    </td>
                                    <td class="Caption" style="width: 10%">
                                        编组选项：
                                    </td>
                                    <td class="Data" style="width: 29%">
                                        <asp:RadioButtonList ID="bz" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                            <asp:ListItem Text="自动编组" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="手动编组" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="动车组" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="限高铁线" Value="3"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:CheckBox ID="onlygg" runat="server" Visible="false" />
                                    </td>
                                    <td class="Caption" style="width: 8%">
                                        编组数：
                                    </td>
                                    <td class="Data" style="width: 33%" colspan="2">
                                        <asp:DropDownList ID="bzs0" runat="server" AutoPostBack="true">
                                            <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                            <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                            <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                            <asp:ListItem Text="18" Value="18" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                            <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                            <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                        </asp:DropDownList>
                                        <div id="bzs1" runat="server" visible="false">
                                            硬座：<asp:TextBox ID="yz" runat="server" Width="30" Text="3" AutoPostBack="true"></asp:TextBox>&nbsp;
                                            硬卧：<asp:TextBox ID="yw" runat="server" Width="30" Text="13" AutoPostBack="true"></asp:TextBox>&nbsp;
                                            软卧：<asp:TextBox ID="rw" runat="server" Width="30" Text="1" AutoPostBack="true"></asp:TextBox>&nbsp;
                                            宿营车：<asp:TextBox ID="sy" runat="server" Width="30" Text="0" AutoPostBack="true"></asp:TextBox>&nbsp;
                                            餐车：<asp:TextBox ID="ca" runat="server" Width="30" Text="1" AutoPostBack="true"></asp:TextBox>&nbsp;
                                        </div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                            <!--数据列表区-->
                            <div id="SearchInfo" runat="server" visible="false">
                                <table class="ListTable" cellspacing="1" id="SearchDataTable" cellpadding="0" style="width: 100%;
                                    margin-top: 10px">
                                    <tr>
                                        <td class="Caption">
                                            车类型
                                        </td>
                                        <td class="Caption">
                                            <asp:Label ID="labInfo1" runat="server" Text='往返天数'></asp:Label>
                                        </td>
                                        <td class="Caption">
                                            <b>模式</b>
                                        </td>
                                        <td class="Caption">
                                            <b>
                                                <asp:Label ID="t1" runat="server"></asp:Label>支出</b>
                                        </td>
                                        <td class="Caption">
                                            <b>
                                                <asp:Label ID="t2" runat="server"></asp:Label>盈亏</b>
                                        </td>
                                        <td class="Caption">
                                            <b>
                                                <asp:Label ID="t3" runat="server"></asp:Label>上座率(%)</b>
                                        </td>
                                        
                                        <!--增加的两列-->
                                        <td class="Caption">
                                            <b>
                                                实际上座率(%)</b>
                                        </td>
                                        
                                        <td class="Caption">
                                            <b>
                                                实际盈亏</b>
                                        </td>
                                        
                                        <td class="Caption">
                                            <b>
                                                <asp:Label ID="t4" runat="server"></asp:Label>运输人数</b>
                                        </td>
                                        <td class="Caption">
                                            <b>
                                                <asp:Label ID="t5" runat="server"></asp:Label>收入</b>
                                        </td>
                                        <td class="Caption">
                                            <b>线路选择</b>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="Repeater2" runat="server" EnableViewState="true">
                                        <ItemTemplate>
                                            <!--显示的数据1-->
                                            <tr>
                                                <td rowspan="4" class="Data">
                                                    <asp:DropDownList ID="selTrainType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RepeaterData_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="traintype" Visible ="false" runat="server"  Text='<%#((KeyValuePair<String,String>)(Container.DataItem)).Key %>'></asp:Label>
                                                    <asp:Label ID="traintypeText"  runat="server" Text='<%#((KeyValuePair<String,String>)(Container.DataItem)).Value %>'></asp:Label>
                                                    <br />
                                                    <asp:HyperLink ID="link2" runat="server" ForeColor="Blue" Font-Underline="true" ToolTip="点击查看编组不同的收支情况"
                                                        Text="[编组]"></asp:HyperLink>
                                                    <asp:Label ID="bianzhu" runat="server" Text="" Visible="false"></asp:Label>
                                                    <asp:HyperLink ID="link3" runat="server" ForeColor="Blue" NavigateUrl="SeeWordFile.aspx"
                                                        Font-Underline="true" ToolTip="将数据导出到Excel" Text="[导出]" Target="_blank"></asp:HyperLink>
                                                </td>
                                                <td rowspan="4" class="Data">
                                                    <asp:DropDownList ID="cds" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RepeaterData_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="Data">
                                                    无人无车
                                                </td>
                                                <td class="Data">
                                                    <asp:Label ID="zhichu1" runat="server" ForeColor="Blue" Font-Underline="true"></asp:Label>
                                                </td>
                                                <td class="Data">
                                                    <asp:Label ID="sz1" runat="server"></asp:Label>
                                                </td>
                                                
                                                <td class="Data">
                                                    <asp:Label ID="szr1" runat="server"></asp:Label>
                                                </td>
                                                
                                                <!--增加的列-->
                                                 <td class="Data">
                                                    <asp:TextBox ID="sjszr1" runat ="server"  AutoPostBack="true" OnTextChanged="sjszreTextChange1"
                                                     Width ="60" style="text-align:center"></asp:TextBox>%
                                                </td>
                                                <td class="Data">
                                                    <asp:Label ID="sjyk1" runat="server"></asp:Label>
                                                </td>
                                                
                                                <td ali rowspan="4" class="Data">
                                                    <asp:Label ID="totalpeople" runat="server"></asp:Label>
                                                </td>
                                                <td rowspan="4" class="Data">
                                                    <asp:Label ID="shouru" runat="server"></asp:Label>
                                                </td>
                                                <td rowspan="4" class="Data" style="width: 30%; text-align: left">
                                                    <asp:RadioButtonList ID="selLine" align="left" AutoPostBack="true" runat="server"
                                                        OnSelectedIndexChanged="RepeaterData_SelectedIndexChanged" Font-Size="12px">
                                                    </asp:RadioButtonList>
                                                    <asp:Label ID="labSelLine" runat="server" Visible="false"></asp:Label>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Data">
                                                    无人有车
                                                </td>
                                                <td class="Data">
                                                    <asp:Label ID="zhichu2" runat="server" ForeColor="Blue" Font-Underline="true"></asp:Label>
                                                </td>
                                                <td class="Data">
                                                    <asp:Label ID="sz2" runat="server"></asp:Label>
                                                </td>
                                                <td class="Data">
                                                    <asp:Label ID="szr2" runat="server"></asp:Label>
                                                </td>
                                                
                                                <!--增加的列-->
                                                <td class="Data">
                                                    <asp:TextBox ID="sjszr2" runat ="server" AutoPostBack="true" OnTextChanged="sjszreTextChange2"
                                                     Width ="60" style="text-align:center"></asp:TextBox>%
                                                </td>
                                                <td class="Data">
                                                    <asp:Label ID="sjyk2" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Data">
                                                    有人无车
                                                </td>
                                                <td class="Data">
                                                    <asp:Label ID="zhichu3" runat="server" ForeColor="Blue" Font-Underline="true"></asp:Label>
                                                </td>
                                                <td class="Data">
                                                    <asp:Label ID="sz3" runat="server"></asp:Label>
                                                </td>
                                                <td class="Data">
                                                    <asp:Label ID="szr3" runat="server"></asp:Label>
                                                </td>
                                                
                                                 <td class="Data">
                                                    <asp:TextBox ID="sjszr3" runat ="server" AutoPostBack="true" OnTextChanged="sjszreTextChange3"
                                                     Width ="60" style="text-align:center"></asp:TextBox>%
                                                </td>
                                                <td class="Data">
                                                    <asp:Label ID="sjyk3" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Data">
                                                    有人有车
                                                </td>
                                                <td class="Data">
                                                    <asp:Label ID="zhichu4" runat="server" ForeColor="Blue" Font-Underline="true"></asp:Label>
                                                </td>
                                                <td class="Data">
                                                    <asp:Label ID="sz4" runat="server"></asp:Label>
                                                </td>
                                                <td class="Data">
                                                    <asp:Label ID="szr4" runat="server"></asp:Label>
                                                </td>
                                                
                                                  <td class="Data">
                                                    <asp:TextBox ID="sjszr4" runat ="server" AutoPostBack="true" OnTextChanged="sjszreTextChange4"
                                                     Width ="60" style="text-align:center"></asp:TextBox>%
                                                </td>
                                                <td class="Data">
                                                    <asp:Label ID="sjyk4" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                            <!--Report Data-->
                            <div style="display: none">
                                <asp:TextBox ID="Rpt1" runat="server"></asp:TextBox>
                                <asp:TextBox ID="Rpt2" runat="server"></asp:TextBox>
                                <asp:TextBox ID="Rpt3" runat="server"></asp:TextBox>
                                <asp:TextBox ID="Rpt4" runat="server"></asp:TextBox>
                                <asp:TextBox ID="Rpt5" runat="server"></asp:TextBox>
                                <asp:DropDownList ID="sd" runat="server" Visible="false">
                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                    <asp:ListItem Text="35" Value="35"></asp:ListItem>
                                    <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                    <asp:ListItem Text="45" Value="45"></asp:ListItem>
                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                    <asp:ListItem Text="60" Value="60"></asp:ListItem>
                                    <asp:ListItem Text="70" Value="70"></asp:ListItem>
                                    <asp:ListItem Text="80" Value="80"></asp:ListItem>
                                    <asp:ListItem Text="90" Value="90"></asp:ListItem>
                                    <asp:ListItem Text="100" Value="100" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:CheckBox ID="CheckBox1" runat="server" Visible="false" />
                            </div>
                        </td>
                        <td width="8" background="../images/tab_15.gif">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="isYearPage" Value="0" runat="server" />
</asp:Content>
