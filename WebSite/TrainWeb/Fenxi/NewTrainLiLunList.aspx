<%@ Page Title="列车收支理论值列表" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master"
    AutoEventWireup="true" CodeBehind="NewTrainLiLunList.aspx.cs" Inherits="WebSite.TrainWeb.Train.NewTrainLiLunList" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="BusinessRule" %>
<%@ Register Src="~/Include/PageNavigator1.ascx" TagName="PageNavigator1" TagPrefix="uc1" %>

<script runat="server">
    
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="/Common/JsLib/PubLib.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function checkData() {
            var succ = confirm("计算车次的收支需要事先导入收支情况的电子表格（如担当车收入 、线路使用费等），确定吗？");
            var d1='<%=DateTime.Now.ToString("yyyy-M")%>';
            var msg = window.prompt("请确定要计算的年和月，中间用-分隔", d1);
            if (msg == null || msg == undefined) {
                succ = false;
            }
            else {
                document.getElementById('<%=msg1.ClientID %>').value = msg;
            }
            return succ;
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
                                                    <span class="STYLE3" style="display: none">你当前的位置：</span>财务数据模型-既有车次收支明细
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="55%">
                                        <jasp:SecurityPanel ID="sec1" runat="server" AuthorityID="001">
                                            <table border="0" align="right" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="100" style="text-align: right">
                                                        <table width="90%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="STYLE1">
                                                                    <div align="center">
                                                                        <img src="../images/22.gif" width="14" height="14" /></div>
                                                                </td>
                                                                <td class="STYLE1" style ="width:200">
                                                                    <div align="center">
                                                                        <asp:LinkButton ID="link1" runat="server" OnClientClick="javascript:return checkData();"
                                                                            Text="计算收支"></asp:LinkButton>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="52" id="tdDel" runat="server" visible="false">
                                                        <table width="88%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="STYLE1">
                                                                    <div align="center">
                                                                        <img src="../images/11.gif" width="14" height="14" /></div>
                                                                </td>
                                                                <td class="STYLE1">
                                                                    <div align="center">
                                                                        删除</div>
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
                <jasp:JDataSource ID="Data1" runat="server" JType="Sql" 
                   SqlID="select NEWTRAINLILUNZHI.*,spcount-pcount spcount_pcount,sshouru-shouru sshouru_shouru,fee1+fee2+fee3+fee4+fee5+fee6+fee7+fee8+fee9+fee10+fee11+fee12+fee13+fee14 fee0,
                          sfee1+sfee2+sfee3+sfee4+sfee5+sfee6+sfee7+sfee8+sfee9+sfee10+sfee11+sfee12+sfee13+sfee14 sfee0,
                           (sfee1+sfee2+sfee3+sfee4+sfee5+sfee6+sfee7+sfee8+sfee9+sfee10+sfee11+sfee12+sfee13+sfee14)-
                           (fee1+fee2+fee3+fee4+fee5+fee6+fee7+fee8+fee9+fee10+fee11+fee12+fee13+fee14) sfee0_fee0
                           from NEWTRAINLILUNZHI"
                    PageSize="20" OrderBy="byear desc,bmonth desc,num">
                </jasp:JDataSource>
                <asp:HiddenField ID="msg1" runat="server" />
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="8" background="../images/tab_12.gif">
                            &nbsp;
                        </td>
                        <td>
                            <!--数据查询区-->
                            <table class="ListTable" cellspacing="1" cellpadding="0">
                                <tr>
                                    <td class="Data" style="text-align: right">
                                        车次：<jasp:JTextBox ID="TrainName" runat="server" Width="100" AutoFillValue="true"></jasp:JTextBox>
                                        &nbsp;&nbsp;年份：<jasp:JTextBox ID="byear" runat="server" Width="100" SearchExpress="byear='{0}'"
                                            AutoFillValue="true"></jasp:JTextBox>
                                        &nbsp;&nbsp;月份：<jasp:JTextBox ID="bmonth" runat="server" Width="100" SearchExpress="bmonth='{0}'"
                                            AutoFillValue="true"></jasp:JTextBox>
                                        &nbsp;&nbsp;<jasp:JButton ID="butSearch" runat="server" Text="过滤" JButtonType="SearchButton"
                                            DataSourceID="Data1">
                                            <SearchPara SearchControlList="TrainName,byear,bmonth" />
                                        </jasp:JButton>
                                        &nbsp;&nbsp;<jasp:JButton ID="JButton1" runat="server" Text="重置" JButtonType="SearchButton"
                                            DataSourceID="Data1">
                                            <SearchPara SearchControlList="TrainName,byear,bmonth" IsCancelSearch="true" />
                                        </jasp:JButton>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <!--数据列表区-->
                            <table class="ListTable" cellspacing="1" cellpadding="0" onmouseover="changeto()"
                                onmouseout="changeback()">
                                <tr>
                                    <td width="3%" class="Caption" rowspan="2">
                                        <input type="checkbox" name="checkbox" value="checkbox" onclick="selectAllDocument('selDocumentID',this.checked);" />
                                    </td>
                                    <td class="Caption" rowspan="2">
                                        年月
                                    </td>
                                    <td class="Caption" rowspan="2">
                                        车次
                                    </td>
                                    <td class="Caption" rowspan="2">
                                        类型
                                    </td>
                                    <td class="Caption" colspan="3">
                                        运输人数
                                    </td>
                                    <td class="Caption" colspan="4">
                                        票价收入(万)
                                    </td>
                                    <td class="Caption" colspan="3">
                                        总支出(万)
                                    </td>
                                    <td class="Caption" rowspan="2">
                                        基本操作
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Caption">
                                        理论值
                                    </td>
                                    <td class="Caption">
                                        实际值
                                    </td>
                                     <td class="Caption">
                                        差额
                                    </td>
                                    <td class="Caption">
                                        理论值
                                    </td>
                                    <td class="Caption">
                                        实际值
                                    </td>
                                    <td class="Caption">
                                        差额
                                    </td>
                                    <td class="Caption">
                                        上座率
                                    </td>
                                    <td class="Caption">
                                        理论值
                                    </td>
                                    <td class="Caption">
                                        实际值
                                    </td>
                                    <td class="Caption">
                                        差额
                                    </td>
                                </tr>
                                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Data1" EnableViewState="false">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <input type="checkbox" name="selDocumentID" id="selDocumentID" value='<%#Eval("num")%>' />
                                            </td>
                                            <td class="Data">
                                                <%#Eval("byear")%>-<%#Eval("bmonth")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("TrainName")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("TrainType")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("pcount", "{0:n0}")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("spcount", "{0:n0}")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("spcount_pcount", "{0:n0}")%>
                                            </td>
                                            <td class="Data" style="text-align: right; padding-left: 5px">
                                                <%#Eval("shouru","{0:n2}")%>
                                            </td>
                                            <td class="Data" style="text-align: right; padding-left: 5px">
                                                <%#Eval("sshouru","{0:n2}")%>
                                                
                                            </td>
                                            <td class="Data" style="text-align: right; padding-left: 5px">
                                                <%#Eval("sshouru_shouru","{0:n2}")%>
                                            </td>
                                            <td class="Data">
                                                <asp:Label ID="szr" runat="server" Text="Label"></asp:Label> %
                                            </td>
                                            <td class="Data" style="text-align: right; padding-left: 5px">
                                                <%#Eval("fee0","{0:n2}")%>
                                            </td>
                                            <td class="Data" style="text-align: right; padding-left: 5px">
                                                <%#Eval("sfee0","{0:n2}")%>
                                            </td>
                                            <td class="Data" style="text-align: right; padding-left: 5px">
                                                <%#Eval("sfee0_fee0","{0:n2}")%>
                                            </td>
                                            <td class="Data">
                                                <a href='NewTrainLiLunDetail.aspx?num=<%#Eval("num")%>'>
                                                    <img src="../images/edt.gif" width="16" height="16" />明细</a>
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
