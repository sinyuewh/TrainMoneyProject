<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="NewTrainList.aspx.cs" Inherits="WebSite.TrainWeb.Train.NewTrainList" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="BusinessRule" %>
<%@ Register Src="~/Include/PageNavigator1.ascx" TagName="PageNavigator1" TagPrefix="uc1" %>

<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        this.Button1.Click += new EventHandler(Button1_Click);
        base.OnInit(e);
    }

    //导入数据
    void Button1_Click(object sender, EventArgs e)
    {
        BusinessRule.PubCode.Util.ImportTrainDataToSystem("train1.xls");
    }
</script>

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
            <td height="30" background="../images/tab_05.gif">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="12" height="30">
                            <img src="../images/tab_03.gif" width="12" height="30" />
                        </td>
                        <td>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td  valign="middle">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="5%">
                                                    <div align="center">
                                                        <img src="../images/tb.gif" width="16" height="16" /></div>
                                                </td>
                                                <td width="95%" class="STYLE1">
                                                    <span class="STYLE3" style="display:none">你当前的位置：</span>财务数据模型-列车列表
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="54%">
                                        <jasp:SecurityPanel ID="sec1" runat="server" AuthorityID="001">
                                        <table border="0" align="right" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td width="60"> 
                                                    <table width="90%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td class="STYLE1">
                                                                <div align="center">
                                                                    <img src="../images/22.gif" width="14" height="14" /></div>
                                                            </td>
                                                            <td class="STYLE1">
                                                                <div align="center">
                                                                    <a href="NewTrainDetail.aspx?TRAINNAME=-1">新增</a></div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="75" id="del" runat="server" >
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td class="STYLE1">
                                                                <div align="center">
                                                                    <img src="../images/22.gif" width="14" height="14" /></div>
                                                            </td>
                                                            <td class="STYLE1">
                                                                <jasp:JLinkButton ID="LinkCheckLine" runat ="server"  OnClientClick="return confirm('提示：线路检测可能需要花费较长的时间，确定要进行线路检测操作吗？');"
                                                                ToolTip="检测线路的配置是否正确，适用于线路发生修改的情况"  Text ="线路检测"></jasp:JLinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        </jasp:SecurityPanel>
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
                <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="NEWTRAIN" PageSize="15"
                    OrderBy="num">
                </jasp:JDataSource>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="8" background="../images/tab_12.gif">
                            &nbsp;
                        </td>
                        <td>
                            <!--数据查询区-->
                            
                            <table class="ListTable" cellspacing="1" cellpadding="0">
                                <tr>
                                    <td class="Data" style="text-align:right">
                                    <jasp:SecurityPanel ID="security1" runat="server" AuthorityID="001">
                                    <div style=" float:left">
                                    &nbsp;  &nbsp;
                                        <asp:FileUpload ID="FileUpload1" runat="server" />  &nbsp;
                                        <asp:Button ID="Button3" runat="server" Text="导入Excel" 
                                            onclick="Button3_Click" /> &nbsp;  &nbsp; 
                                       <asp:Button ID="Button2" runat="server" Text="导出Excel"  onclick="Button2_Click" /></div>
                                       </jasp:SecurityPanel>
                                       &nbsp;  &nbsp;  &nbsp;  &nbsp;
                                        车次：<jasp:JTextBox ID="TrainName" runat="server" Width="100" AutoFillValue="true"></jasp:JTextBox>
                                        &nbsp;&nbsp;始发站：<jasp:JTextBox ID="AStation" runat="server" Width="100" AutoFillValue="true"></jasp:JTextBox>
                                         &nbsp;&nbsp;终到站：<jasp:JTextBox ID="BStation" runat="server" Width="100" AutoFillValue="true"></jasp:JTextBox>
                                        &nbsp;&nbsp;<jasp:JButton ID="butSearch" runat="server" Text="过滤" JButtonType="SearchButton" DataSourceID="Data1">
                                            <SearchPara SearchControlList="TrainName,AStation,BStation" />
                                        </jasp:JButton>
                                        &nbsp;&nbsp;<jasp:JButton ID="JButton1" runat="server" Text="重置" JButtonType="SearchButton" DataSourceID="Data1">
                                            <SearchPara SearchControlList="TrainName,AStation,BStation" IsCancelSearch="true" />
                                        </jasp:JButton>
                                 
                                    </td>
                                </tr>
                            </table>
                       
                            <!--数据列表区-->
                            <table class="ListTable" cellspacing="1" cellpadding="0" onmouseover="changeto()"
                                onmouseout="changeback()">
                                <tr>
                                    <td width="3%" class="Caption">
                                        <input type="checkbox" name="checkbox" value="checkbox" onclick="selectAllDocument('selDocumentID',this.checked);" />
                                    </td>
                                    <td width="3%" class="Caption">
                                        序号
                                    </td>
                                    <td class="Caption">
                                        车次
                                    </td>
                                    <td class="Caption">
                                        始发站
                                    </td>
                                    <td class="Caption">
                                        终到站
                                    </td>
                                    <td class="Caption">
                                        列车类型
                                    </td>
                                    <td class="Caption">
                                        单程距离
                                    </td>
                                    <td class="Caption">
                                        开行趟数
                                    </td>
                                    <td class="Caption">
                                        车底组数
                                    </td>
                                    <td class="Caption">
                                        线路配置
                                    </td>
                                    <td class="Caption">
                                        基本操作
                                    </td>
                                </tr>
                                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Data1">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <input type="checkbox" name="selDocumentID" id="selDocumentID" value='<%#Eval("TrainName")%>' />
                                            </td>
                                            <td class="Data">
                                                <%#Eval("num")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("TrainName")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("AStation")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("BStation")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("TrainType")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("Mile")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("KXTS")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("CDZS")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("Line").ToString().Trim()==String.Empty ? "":"已配置"%>
                                                <%#Eval("CheckLine").ToString().Trim()==String.Empty ? "":"<font color='red'>×</font>"%>
                                            </td>
                                            <td class="Data">
                                                <a href='NewTrainDetail.aspx?TRAINNAME=<%#Eval("TRAINNAME")%>'><img src="../images/edt.gif" width="16" height="16" />明细</a> &nbsp;
                                                <jasp:JLinkButton ID="butDel" AuthorityID="001" runat="server"
                                                       OnClientClick="javascript:return confirm('警告：确定要删除数据吗？');"
                                                       JButtonType="SimpleAction"
                                                       runat="server" CommandArgument='<%#Eval("TRAINNAME")%>' 
                                                       Text='<img src="../images/del.gif"
                                                        width="16" height="16"  />删除'>
                                                    <SimpleActionPara TableName="NEWTRAIN" ButtonType="DeleteData" />
                                                    <ParaItems>
                                                        <jasp:ParameterItem ParaName="TRAINNAME" ParaType="CommandArgument" />
                                                    </ParaItems>
                                                    <ExecutePara  SuccUrl="#" />
                                                </jasp:JLinkButton>
                                                    
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                        <td width="8" background="../images/tab_15.gif">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="35" background="../images/tab_19.gif">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="12" height="35">
                            <img src="../images/tab_18.gif" width="12" height="35" />
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="导入数据" Visible="false" />
                            <uc1:PageNavigator1 ID="PageNavigator11" runat="server" DataSourceID="Data1" />
                        </td>
                        <td width="16">
                            <img src="../images/tab_20.gif" width="16" height="35" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
