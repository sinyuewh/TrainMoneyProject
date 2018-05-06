<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="TrainLineDetail.aspx.cs" Inherits="WebSite.TrainWeb.Train.TrainLineDetail" %>

<%@ Import Namespace="BusinessRule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="/Common/JsLib/PubLib.js" type="text/javascript"></script>
    <script src="/Common/JsLib/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="/Common/JsLib/jquery.autocomplete.js" type="text/javascript"></script>
    
    <script language="javascript" type="text/javascript">
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
                                                    <span class="STYLE3" style="display:none">你当前的位置：</span>财务数据模型-编辑列车线路
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
                            <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="TrainLine" OrderBy="num">
                                <ParaItems>
                                    <jasp:ParameterItem ParaName="LineID" ParaType="RequestQueryString" IsNullValue="true" />
                                </ParaItems>
                            </jasp:JDataSource>
                            
                            <jasp:JDetailView ID="DetailView1" runat="server" DataSourceID="Data1" ControlList="*" >
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
                                            线路代码
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="LineID" runat="server" AllowNullValue="false" Caption="线路代码" DataType="Integer"
                                                IsUnique="true"></jasp:JTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Caption">
                                            线路名称
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="LineName" runat="server" AllowNullValue="false" Caption="线路名称"></jasp:JTextBox>
                                        </td>
                                        <td class="Caption">
                                            起点
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="AStation" runat="server" AllowNullValue="false" Caption="起点"></jasp:JTextBox>
                                            <jasp:JTextBox ID="Astation1" Visible="false" DataField="AStation" runat="server"></jasp:JTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Caption">
                                            讫点
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="BStation" runat="server" AllowNullValue="false" Caption="讫点"></jasp:JTextBox>
                                            <jasp:JTextBox ID="BStation1"  Visible="false" DataField="BStation"  runat="server"></jasp:JTextBox>
                                        </td>
                                        <td class="Caption">
                                            长度(km)
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="MILES" runat="server" AllowNullValue="false" Caption="长度" DataType="Integer"></jasp:JTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Caption">
                                            类别
                                        </td>
                                        <td class="Data">
                                            <jasp:AppDropDownList ID="LineType" runat="server" AllowNullValue="false" Caption="类别">
                                                <asp:ListItem Text="特一类" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="特二类" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="一类上浮" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="一类" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="二类上浮" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="二类" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="三类" Value="7"></asp:ListItem>
                                                <asp:ListItem Text="三类下浮" Value="8"></asp:ListItem>
                                            </jasp:AppDropDownList>
                                        </td>
                                        <td class="Caption">
                                            整条线路是否电气化
                                        </td>
                                        <td class="Data">
                                            <jasp:AppDropDownList ID="dqh" runat="server"  Caption="是否电气化">
                                                <asp:ListItem Text="否" Value=""></asp:ListItem>
                                                <asp:ListItem Text="是" Value="是"></asp:ListItem>
                                            </jasp:AppDropDownList>
                                            <jasp:JTextBox ID="LineClass" runat="server" Text="0" Visible="false"></jasp:JTextBox>
                                        </td>
                                    </tr>
                                    <tr id="row1" runat ="server" visible ="false" >
                                        <td class="Caption">
                                            夏冬切换
                                        </td>
                                        <td class="Data" colspan="3">
                                            <jasp:AppDropDownList ID="SpringWinter" runat="server"  Caption="夏冬切换">
                                                <asp:ListItem Text="不切换" Value=""></asp:ListItem>
                                                <asp:ListItem Text="切换" Value="1"></asp:ListItem>
                                            </jasp:AppDropDownList>（说明：夏季为4-9月 冬季为10、11、12、1、2、3月，冬季时，“切换”标识的线路将自动从“特一类”变成“）
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Caption">
                                            备注
                                        </td>
                                        <td class="Data" colspan="3">
                                            <jasp:JTextBox ID="Remark" runat="server" Width="85%"></jasp:JTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </jasp:JDetailView>
                            <div class="ButtonArea" style="width: 80%">
                                <asp:UpdatePanel ID="update1" runat="server">
                                    <ContentTemplate>
                                        <jasp:JButton ID="button1" runat="server" Text="提 交" JButtonType="SimpleAction" 
                                            IsValidatorData="true" NoControlList="Astation1,BStation1">
                                            <SimpleActionPara TableName="TrainLine" ButtonType="UpdateData" AppendFlag="true" />
                                            <ParaItems>
                                                <jasp:ParameterItem ParaName="LineID" ParaType="RequestQueryString" />
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
