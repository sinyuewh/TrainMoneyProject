<%@ Page Title="浏览和编辑线路站点" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master"
    AutoEventWireup="true" CodeBehind="EditStation.aspx.cs" Inherits="WebSite.TrainWeb.Train.EditStation" %>

<%@ Import Namespace="BusinessRule" %>

<script runat="server">
    
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script src="/TrainWeb/JSLib/jquery-1.4.2.min.js" type="text/javascript"></script>
<script src="/TrainWeb/JSLib/PubLib.js" type="text/javascript"></script>
<script src="/Common/JsLib/jquery.autocomplete.js" type="text/javascript"></script>
    
<script language="javascript" type="text/javascript">
    function Select() {
        var v1 = "Choose.aspx";
        var u1 = myShowModalDialog(v1, 780, 650);
        
        if (u1 != undefined && u1 != "") {
        };
        return u1;
    }

    function setsamevalue(dpdlkz, indexlbl) {

        try {
            var repeaterId = '<%=Repeater1.ClientID %>'; //Repeater的客户端ID
            var rows = parseInt('<%=Repeater1.Items.Count%>'); //Repeater的行数
            var nextindex = parseInt($("#" + indexlbl).val());

            for (var i = nextindex; i < rows; i++) {

                var ctlid = repeaterId + "_ctl" + getrownumber(i) + "_dpdlkz";
                $("#" + ctlid).find("option[value='" + document.getElementById(dpdlkz).value + "']").attr("selected", true);
            }
        }
        catch (e) {
            alert(e.toString());
        }
    }
    
    function changevalue(dpdlkz, t1, indexlbl) {
           
           try {
                $("#" + dpdlkz).find("option[text='" + $("#" + t1).val() + "']").attr("selected", true);
                
                var repeaterId = '<%=Repeater1.ClientID %>'; //Repeater的客户端ID
                var rows = parseInt('<%=Repeater1.Items.Count%>'); //Repeater的行数
                var nextindex = parseInt($("#" + indexlbl).val());

                for (var i = nextindex; i < rows; i++) {

                    var ctlid = repeaterId + "_ctl" + getrownumber(i) + "_dpdlkz";
                    $("#" + ctlid).find("option[text='" + $("#" + t1).val() + "']").attr("selected", true);
                }
                /*
                var corptext = $("#" + t1).val();
                var count = $("#" + dpdlkz + " option").length;
                var isExist = false;

                for (var i = 0; i < count; i++) {
                    if ($("#" + dpdlkz + " ").get(0).options[i].text == corptext) {
                        isExist = true;

                        break;
                    }
                }

                if (!isExist) {
                    $("#" + dpdlkz).find("option[text='无" + "']").attr("selected", true);
                    return;
                }  */
            }
            catch (e) {
                alert(e.toString());
            }
    }

    $(document).ready(function() {
        try {
            var repeaterId = '<%=Repeater1.ClientID %>'; //Repeater的客户端ID
            var rows = parseInt('<%=Repeater1.Items.Count%>'); //Repeater的行数

            for (var i = 0; i < rows; i++) {

                var ctlid = repeaterId + "_ctl" + getrownumber(i) + "_CORPNAME";

                $("#" + ctlid).autocomplete("/Handler/GetStationNameValue.ashx?AutoKind=GetKzId",
                {
                    width: 205,
                    selectFirst: false,
                    max: 2000,
                    scrollHeight: 100,
                    extraParams: { 'txtValue': function() {
                        return $("#" + ctlid).val();
                    }
                    }
                });

            }
        }
        catch (e) {
            alert(e.toString());
        }
    });
        function getrownumber(i) {
            if (i >= 10) {
                return i;
            }
            else {
                return '0' + i;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="30" background="/TrainWeb/images/tab_05.gif">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="12" height="30">
                            <img src="/TrainWeb/images/tab_03.gif" width="12" height="30" />
                        </td>
                        <td>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="46%" valign="middle">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="5%">
                                                    <div align="center">
                                                        <img src="/TrainWeb/images/tb.gif" width="16" height="16" /></div>
                                                </td>
                                                <td width="95%" class="STYLE1">
                                                    <span class="STYLE3" style="display: none">你当前的位置：</span>财务数据模型-列车线路站点调整
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="54%">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="16">
                            <img src="/TrainWeb/images/tab_07.gif" width="16" height="30" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="8" background="/TrainWeb/images/tab_12.gif">
                            &nbsp;
                        </td>
                        <td style="height: 500px; vertical-align: top;">
                            <div id="div1" style="width: 100%">
                                <!--数据源定义-->
                                <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="TRAINLINE" OrderBy="num">
                                    <ParaItems>
                                        <jasp:ParameterItem ParaName="LineID" ParaType="RequestQueryString" IsNullValue="true" />
                                    </ParaItems>
                                </jasp:JDataSource>
                                <jasp:JDetailView ID="DetailView1" runat="server" DataSourceID="Data1" ControlList="*">
                                    <table class="DetailTable" cellspacing="1" cellpadding="0" style="width: 80%; margin-left: 15px;
                                        display: block">
                                        <tr>
                                            <td class="Caption">
                                                线路ID
                                            </td>
                                            <td class="Data">
                                                <jasp:JLabel ID="lineid" runat="server" />
                                            </td>
                                            <td class="Caption">
                                                线路名称
                                            </td>
                                            <td class="Data">
                                                <jasp:JLabel ID="linename" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Caption">
                                                起点
                                            </td>
                                            <td class="Data">
                                                <jasp:JLabel ID="astation" runat="server" />
                                            </td>
                                            <td class="Caption">
                                                讫点
                                            </td>
                                            <td class="Data">
                                                <jasp:JLabel ID="bstation" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Caption">
                                                长度(km)
                                            </td>
                                            <td class="Data">
                                                <jasp:JLabel ID="miles" runat="server" />
                                            </td>
                                            <td class="Caption">
                                                类别
                                            </td>
                                            <td class="Data">
                                                <jasp:AppDropDownList ID="linetype" Visible="false" KeyItem="lineType" runat="server">
                                                </jasp:AppDropDownList>
                                                <%=this.linetype.SelectedItem.Text %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Caption">
                                                中间站点
                                            </td>
                                            <td class="Data" colspan="3">
                                                <jasp:JTextBox ID="midstation" runat="server" TextMode="MultiLine" Height="200" Width="90%"></jasp:JTextBox>
                                                &nbsp;&nbsp;<jasp:JButton ID="Button1" runat="server" AuthorityID="001" Text="确定" />
                                                <br />
                                                提示：中间站点之间用逗号分隔，不包括起点和讫点。
                                            </td>
                                        </tr>
                                    </table>
                                </jasp:JDetailView>
                            </div>
                            <div id="div2" style="width: 100%; margin-top: 10">
                                
                                   
                                        <table class="ListTable" class="ListTable" cellspacing="1" cellpadding="0" style="width: 80%;
                                            margin-left: 8px;" align="left">
                                            <tr>
                                                <td class="Caption">
                                                    序号
                                                </td>
                                                <td class="Caption">
                                                    始发站
                                                </td>
                                                <td class="Caption">
                                                    到达站
                                                </td>
                                                <td class="Caption">
                                                    长度(km)
                                                </td>
                                                <td class="Caption">
                                                    局内
                                                </td>
                                               <%-- <td class="Caption">
                                                    内燃牵引费
                                                </td>
                                                <td class="Caption">
                                                    电力牵引费
                                                </td>--%>
                                                <td class="Caption">
                                                    公司标识
                                                </td>
                                                <td class="Caption" width="25%" style="display:none">
                                                    长交路标识ID 
                                                </td>
                                                <td class="Caption">
                                                    电气化<asp:CheckBox ID="dqhAll" runat="server" AutoPostBack="true" />
                                                </td>
                                                <td class="Caption">
                                                    轮渡标志
                                                </td>
                                                
                                                 <td class="Caption">
                                                    高铁联络线
                                                </td>
                                            </tr>
                                            <asp:Repeater ID="Repeater1" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="Data">
                                                        <asp:HiddenField ID="IndexLabel" runat="server"  Value='<%#Container.ItemIndex+1 %>' />
                                                       
                                                            <%#Container.ItemIndex+1 %>
                                                        </td>
                                                        <td class="Data">
                                                            <asp:Label ID="AStation" runat="server" Text='<%#Eval("AStation") %>' />
                                                        </td>
                                                        <td class="Data">
                                                            <asp:Label ID="BStation" runat="server" Text='<%#Eval("BStation") %>' />
                                                        </td>
                                                        <td class="Data">
                                                            <asp:TextBox ID="Miles" AutoPostBack="true" Style="text-align: center" Width="100"
                                                                Text='<%#Eval("MILES") %>' runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                                                        </td>
                                                        <td class="Data">
                                                            <asp:CheckBox ID="JnFlag" runat="server" Checked='<%# Eval("jnflag").ToString()=="" ? false : true %>' />
                                                        </td>
                                                        <td class="Data">
                                                            <asp:Label ID="lab1" runat ="server" Visible ="false" Text ='<%#Eval("kzid") %>'></asp:Label>
                                                            <asp:DropDownList runat="server" ID="dpdlkz" Width="192"></asp:DropDownList>
                                                            <asp:TextBox runat="server" ID="CORPNAME" Width="160"></asp:TextBox>
                                                            <%--<asp:Button runat="server" ID="BtnGetKZID" Text="获取客专ID" CommandName="Get"/>--%>
                                                        </td> 
                                                         <td class="Data" style="display:none">
                                                            <asp:TextBox runat="server" ID="lblcjl2"  Text='<%#Eval("CJLID") %>' AutoPostBack="true"  OnTextChanged="lblcjl2_TextChanged">
                                                            </asp:TextBox>
                                                            <asp:HiddenField ID="lblcjl" runat="server" Value="" />
                                                            <asp:Button runat="server" ID="btn" OnClientClick="javascript:var str = Select().split('&&'); $(this).prev().prev().val(str[0]); $(this).next().next().html(str[1]);return false; "  Text="选择"/>
                                                            <br><asp:Label ID="Lab" runat ="server"></asp:Label>
                                                        </td> 
                                                                   
                                                                                                              <%--<td class="Data">
                                                            <asp:TextBox ID="Fee1"  Style="text-align: center" Width="80"
                                                                Text='<%#Eval("Fee1") %>' runat="server" ReadOnly ="true" ToolTip ="只读" ></asp:TextBox>
                                                        </td>
                                                        <td class="Data">
                                                            <asp:TextBox ID="Fee2"  Style="text-align: center" Width="80"
                                                                Text='<%#Eval("Fee2") %>' runat="server" ReadOnly ="true" ToolTip ="只读"></asp:TextBox>
                                                        </td>--%>
                                                        
                                                        <td class="Data">
                                                            <asp:CheckBox ID="dqh" AutoPostBack="true" OnCheckedChanged="MyCheckChange" runat="server"
                                                                Checked='<%# Eval("dqh").ToString()=="" ? false : true %>' />
                                                        </td>
                                                        <td class="Data">
                                                            <asp:CheckBox ID="SHIPFLAG" runat="server" Checked='<%# Eval("SHIPFLAG").ToString()=="" ? false : true %>' />
                                                        </td>
                                                        
                                                        <td class="Data">
                                                            <asp:CheckBox ID="GTLLX" runat="server" Checked='<%# Eval("GTLLX").ToString()=="" ? false : true %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                            </div>
                            <br />
                                    <div style="width: 80%; text-align: center">
                                        <jasp:JButton ID="Button2" AuthorityID="001" runat="server" Text="更新站点里程" OnClientClick="javascript:return confirm('提示：确定要提交站点里程数据吗？');">
                                            <ExecutePara SuccInfo="提示：更新站点里程操作成功！" SuccUrl="-1" FailInfo="提示：更新站点里程操作失败！" />
                                        </jasp:JButton>
                                        &nbsp;
                                        <jasp:JButton ID="JButton1" runat="server" Text="返 回" OnClientClick="javascript:window.navigate('TrainLineList.aspx');return false;"
                                            JButtonType="RedirectButton">
                                        </jasp:JButton>
                                    </div>
                        </td>
                        <td width="8" background="/TrainWeb/images/tab_15.gif">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="35" background="/TrainWeb/images/tab_19.gif">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="12" height="35">
                            <img src="/TrainWeb/images/tab_18.gif" width="12" height="35" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td width="16">
                            <img src="/TrainWeb/images/tab_20.gif" width="16" height="35" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
