<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" 
AutoEventWireup="true" CodeBehind="NewFenXiForYear.aspx.cs" Inherits="WebSite.TrainWeb.Fenxi.NewFenXiForYear" %>


<%@ Import Namespace="BusinessRule" %>
<%@ Import Namespace="System.Collections.Generic" %>

<script runat="server">
    
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="/Common/JsLib/PubLib.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
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

        function SelectSpecialTrainLine() {
            var id1 = '<%=selTrain.ClientID %>';
            var url1 = "../Train/SelectTrainLine.aspx?parent=" + id1;
            myOpenURLWithScroll(url1, 600, 900);
        }

        function DaoChuData() {
            alert("Test");
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
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
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
                                                    <span class="STYLE3" style="display:none">你当前的位置：</span>财务数据模型-新增车次 <b>按年</b> 分析
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
                                    <td class="Data" style="width: 13%">
                                        <asp:TextBox ID="AStation" runat="server" Width="100" Text="武汉"></asp:TextBox>
                                    </td>
                                    <td class="Caption" style="width: 10%">
                                        终点站：
                                    </td>
                                    <td class="Data" style="width: 13%">
                                        <asp:TextBox ID="BStation" runat="server" Width="105" Text="广州"></asp:TextBox>
                                    </td>
                                    <td class="Caption" style="width: 10%">
                                        中间站：
                                    </td>
                                    <td class="Data" style="width: 33%">
                                        <asp:TextBox ID="middleStation" runat="server" Width="350"></asp:TextBox>
                                        <asp:CheckBoxList ID="trainList" runat="server" Visible="false" RepeatDirection="Horizontal"
                                            RepeatColumns="2" Width="300">
                                        </asp:CheckBoxList>
                                    </td>
                                    <td class="Caption" style="width: 10%">
                                        深度：
                                    </td>
                                    <td class="Data" style="width: 13%">
                                        <asp:DropDownList ID="sd" runat="server">
                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                            <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                            <asp:ListItem Text="30" Value="30" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="35" Value="35"></asp:ListItem>
                                            <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                            <asp:ListItem Text="45" Value="45"></asp:ListItem>
                                            <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                            <asp:ListItem Text="60" Value="60"></asp:ListItem>
                                            <asp:ListItem Text="70" Value="70"></asp:ListItem>
                                            <asp:ListItem Text="80" Value="80"></asp:ListItem>
                                            <asp:ListItem Text="90" Value="90"></asp:ListItem>
                                            <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="Data" style="width: 10%; text-align: center">
                                        <asp:Button ID="Button1" runat="server"  ToolTip="点按钮开始分析线路" Text="开始分析"
                                            OnClientClick="javascript:HideWindow();" />
                                    </td>
                                </tr>
                                <tr id="row2" runat="server">
                                    <td class="Caption" style="width: 10%">
                                        指定线路：
                                    </td>
                                    <td colspan="7" class="Data">
                                        <asp:TextBox ID="selTrain" runat="server" Width="85%"></asp:TextBox>&nbsp;&nbsp;
                                        <asp:CheckBox ID="CheckBox1" runat="server" onclick="javascript:if(this.checked){alert('提示：重新分析线路仅适合线路站点定义发生了改变的情况，对于线路未改变，不需要选中此项（选中此项，系统将不会利用上次分析的结果，一般分析需较长时间）!');}" /> 重新分析线路
                                    </td>
                                    <td class="Data" style="width: 10%; text-align: center">
                                        <asp:Button ID="Button3" runat="server" ToolTip="指定搜索的线路" Text="指定线路" 
                                        OnClientClick="javascript:SelectSpecialTrainLine();return false;" />
                                        <asp:Button ID="Button2" runat="server" Text="导出" Visible="false" ToolTip="导出数据到Excel" OnClientClick="javascript:DaoChuData();return false;" />
                                    </td>
                                </tr>
                            </table>
                            <div style="margin-top: 5px; margin-bottom: 5px; color:Blue ">
                                提示：深度表示经过的站点数，深度越深，分析越耗时，请根据经验，选择合适的分析深度(一般请控制在50以内)。
                            </div>
                            <!--数据列表区-->
                            <div id="SearchInfo" runat="server" visible="false">
                                <table class="ListTable" cellspacing="1" id="SearchDataTable" cellpadding="0" style="width: 100%;
                                    margin-top: 10px">
                                    <tr>
                                        <td class="Caption">
                                            车类型
                                        </td>
                                        <td class="Caption">
                                            车底数
                                        </td>
                                        <td class="Caption">
                                            <b>模式</b>
                                        </td>
                                        <td class="Caption">
                                            <b>年总支出(万)</b>
                                        </td>
                                        <td class="Caption">
                                            <b>盈亏(万)</b>
                                        </td>
                                        <td class="Caption">
                                            <b>上座率(%)</b>
                                        </td>
                                        <td class="Caption">
                                            <b>年运输人数
                                                <br />
                                                (万人)</b>
                                        </td>
                                        <td class="Caption">
                                            <b>年总收入(万)</b>
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
                                                    <asp:Label ID="traintype" runat="server" Visible="false" Text='<%#((KeyValuePair<String,String>)(Container.DataItem)).Key %>'></asp:Label>
                                                    <asp:Label ID="traintypeText" runat="server" Text='<%#((KeyValuePair<String,String>)(Container.DataItem)).Value %>'></asp:Label>
                                                    &nbsp;
                                                    <asp:HyperLink ID="link2" runat="server" ForeColor="Blue" Font-Underline="true" ToolTip="点击查看编组不同的收支情况"
                                                        Text="[编组]"></asp:HyperLink>
                                                    <asp:Label ID="bianzhu" runat="server" Text="" Visible="false"></asp:Label>
                                                    <asp:HyperLink ID="link3" runat="server" ForeColor="Blue" NavigateUrl="SeeWordFile.aspx"
                                                        Font-Underline="true" ToolTip="将数据导出到Excel" Text="[导出]" Target="_blank"></asp:HyperLink>
                                                </td>
                                                <td rowspan="4" class="Data">
                                                    <asp:DropDownList ID="cds" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cds_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="Data">
                                                    无人无车
                                                </td>
                                                <td class="Data">
                                                    <asp:LinkButton ID="zhichu1" runat="server" Text="0" ForeColor="Blue" Font-Underline="true"
                                                        ToolTip="点击查看费用的详细情况"></asp:LinkButton>
                                                </td>
                                                <td class="Data">
                                                    <asp:Label ID="sz1" runat="server"></asp:Label>
                                                </td>
                                                <td class="Data">
                                                    <asp:Label ID="szr1" runat="server"></asp:Label>
                                                </td>
                                                <td ali rowspan="4" class="Data">
                                                    <asp:Label ID="totalpeople" runat="server"></asp:Label>
                                                </td>
                                                <td rowspan="4" class="Data">
                                                    <asp:Label ID="shouru" runat="server"></asp:Label>
                                                </td>
                                                <td rowspan="4" class="Data" style="width: 30%; text-align: left">
                                                    <asp:RadioButtonList ID="selLine" align="left" AutoPostBack="true" runat="server"
                                                        OnSelectedIndexChanged="selLine_SelectedIndexChanged" Font-Size="12px">
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
                                                    <asp:LinkButton ID="zhichu2" runat="server" Text="0" ForeColor="Blue" Font-Underline="true"
                                                        ToolTip="点击查看费用的详细情况"></asp:LinkButton>
                                                </td>
                                                <td class="Data">
                                                    <asp:Label ID="sz2" runat="server"></asp:Label>
                                                </td>
                                                <td class="Data">
                                                    <asp:Label ID="szr2" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Data">
                                                    有人无车
                                                </td>
                                                <td class="Data">
                                                    <asp:LinkButton ID="zhichu3" runat="server" Text="0" ForeColor="Blue" Font-Underline="true"
                                                        ToolTip="点击查看费用的详细情况"></asp:LinkButton>
                                                </td>
                                                <td class="Data">
                                                    <asp:Label ID="sz3" runat="server"></asp:Label>
                                                </td>
                                                <td class="Data">
                                                    <asp:Label ID="szr3" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Data">
                                                    有人有车
                                                </td>
                                                <td class="Data">
                                                    <asp:LinkButton ID="zhichu4" runat="server" Text="0" ForeColor="Blue" Font-Underline="true"
                                                        ToolTip="点击查看费用的详细情况"></asp:LinkButton>
                                                </td>
                                                <td class="Data">
                                                    <asp:Label ID="sz4" runat="server"></asp:Label>
                                                </td>
                                                <td class="Data">
                                                    <asp:Label ID="szr4" runat="server"></asp:Label>
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
    <asp:HiddenField ID="isYearPage" Value="1" runat="server" />
</asp:Content>

