<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="NewTrainDetail.aspx.cs" Inherits="WebSite.TrainWeb.Train.NewTrainDetail" %>

<%@ Import Namespace="BusinessRule" %>

<script runat="server">
    
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="/Common/JsLib/PubLib.js" type="text/javascript"></script>
    <script src="/Common/JsLib/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="/Common/JsLib/jquery.autocomplete.js" type="text/javascript"></script>
    
    <script language ="javascript" type ="text/javascript">
        function SelectTrainLine() {
            var id1 = '<%=Line.ClientID %>';
            var a1 = document.getElementById('<%=AStation.ClientID %>').value;
            var b1 = document.getElementById('<%=BStation.ClientID %>').value;
            var traintype1 = document.getElementById('<%=TrainType.ClientID %>').value;

            var url1 = "/TrainWeb/Fenxi/SelectTrainLine.aspx?traintype=" + escape(traintype1) + "&parent=" + id1 + "&astation=" + escape(a1) + "&bstation=" + escape(b1);
            myOpenURLWithScroll(url1, 600, 1300);
        }

        $(document).ready(function() {
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
        });
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
                                                    <span class="STYLE3" style="display:none">你当前的位置：</span>财务数据模型-列车明细
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
                        <td style="height: 600px; vertical-align: top">
                            <!--数据源定义-->
                            <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="NEWTRAIN" OrderBy="num">
                                <ParaItems>
                                    <jasp:ParameterItem ParaName="TRAINNAME" ParaType="RequestQueryString" IsNullValue="true" />
                                </ParaItems>
                            </jasp:JDataSource>
                            <jasp:JDetailView ID="DetailView1" runat="server" DataSourceID="Data1" ControlList="*">
                                <table class="DetailTable" cellspacing="1" cellpadding="0" style="width: 80%; margin-left: 15px">
                                    <tr>
                                        <td class="Caption">
                                            序号
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="num" runat="server" AllowNullValue="false" Caption="序号" DataType="Integer"
                                                MaxLength="3"></jasp:JTextBox>
                                        </td>
                                        <td class="Caption">
                                            车次
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="TrainName" runat="server" AllowNullValue="false" Caption="车次"
                                             IsUnique ="true" ></jasp:JTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Caption">
                                            始发站
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="AStation" runat="server" AllowNullValue="false" Caption="始发站"></jasp:JTextBox>
                                        </td>
                                        <td class="Caption">
                                            终到站
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="BStation" runat="server" AllowNullValue="false" Caption="终到站"></jasp:JTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Caption">
                                            列车类型
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="TrainBigKind" runat="server" Text="0" Visible="false"></jasp:JTextBox>
                                            <jasp:JTextBox ID="TrainType" runat="server" AllowNullValue="false" Caption="列车类型"></jasp:JTextBox>
                                            <br />全程内燃机车标志：<jasp:JCheckBox ID="FullNeiRang" runat ="server" />
                                        </td>
                                        <td class="Caption">
                                            单程距离
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="Mile" runat="server" AllowNullValue="false" Caption="单程距离" DataType="Integer"></jasp:JTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Caption">
                                            开行趟数
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="KXTS" runat="server" DataType="Integer" Caption ="开行趟数"></jasp:JTextBox>
                                        </td>
                                        <td class="Caption">
                                            车底组数
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="CDZS" runat="server" DataType="Integer" Caption ="车底组数"></jasp:JTextBox>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            列车编组
                                        </td>
                                        <td class="Data" colspan="3">
                                            硬座：<jasp:JTextBox Caption ="硬座" ID="YinZuo" Width="40" runat="server" DataType="Integer"></jasp:JTextBox>
                                            软座：<jasp:JTextBox Caption ="软座" ID="RuanZuo" Width="40" runat="server" DataType="Integer"></jasp:JTextBox>
                                            硬卧：<jasp:JTextBox Caption ="硬卧" ID="OpenYinWo" Width="40" runat="server" DataType="Integer"></jasp:JTextBox>
                                            软卧：<jasp:JTextBox Caption ="软卧" ID="RuanWo" Width="40" runat="server" DataType="Integer"></jasp:JTextBox>
                                            餐车：<jasp:JTextBox Caption ="餐车" ID="CanChe" Width="40" runat="server" DataType="Integer"></jasp:JTextBox>
                                            发电车：<jasp:JTextBox Caption ="发电车" ID="FaDianChe" Width="40" runat="server" DataType="Integer"></jasp:JTextBox>
                                            行李车：<jasp:JTextBox Caption ="行李车" ID="ShuYinChe" Width="40" runat="server" DataType="Integer"></jasp:JTextBox>
                                            邮政车：<jasp:JTextBox Caption ="邮政车" ID="YouZhengChe" Width="40" runat="server" DataType="Integer"></jasp:JTextBox>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            关联车次
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="GLTRAIN" runat="server" Caption ="关联车次"></jasp:JTextBox>
                                        </td>
                                        <td class="Caption">
                                            关联时间
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="GLYEAR" runat="server" DataType="Integer" Caption ="关联年" Width ="100"></jasp:JTextBox>
                                            年
                                            <jasp:JTextBox ID="GLMONTH" runat="server" DataType="Integer" Caption ="关联月" Width ="100"></jasp:JTextBox>
                                            月
                                        </td>
                                    </tr>
                                    
                                     <tr>
                                        <td class="Caption">
                                            线路
                                        </td>
                                        <td class="Data" colspan="3">
                                            <jasp:JTextBox ID="Line" Width="85%" TextMode="MultiLine" Height ="100" runat="server"></jasp:JTextBox>
                                            &nbsp;
                                            <jasp:JButton ID="butSelectLine" OnClientClick="javascript:SelectTrainLine();return false;" 
                                            runat ="server" AuthorityID="001" Text ="选择"></jasp:JButton>
                                        </td>
                                    </tr>
                                    
                                                                       
                                </table>
                            </jasp:JDetailView>
                            <div class="ButtonArea" style="width: 80%">
                                <asp:UpdatePanel ID="update1" runat="server">
                                    <ContentTemplate>
                                        <jasp:JButton ID="button1" runat="server" AuthorityID="001" Text="提 交" JButtonType="NoJButton" 
                                              IsValidatorData="true">
                                            <SimpleActionPara TableName="NEWTRAIN" ButtonType="UpdateData" AppendFlag="true" />
                                            <ParaItems>
                                                <jasp:ParameterItem ParaName="TRAINNAME" ParaType="RequestQueryString" />
                                            </ParaItems>
                                            <ExecutePara SuccInfo="提示：提交数据操作成功！" SuccUrl="-1" FailInfo="提示：提交数据操作失败！" />
                                        </jasp:JButton>
                                        &nbsp;
                                        <jasp:JButton ID="JButton1" runat="server" Text="返 回" JButtonType="RedirectButton">
                                            <ExecutePara SuccUrl="-1" />
                                        </jasp:JButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            &nbsp;
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
