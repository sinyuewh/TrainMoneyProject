<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="SelectOldLines.aspx.cs" Inherits="WebSite.TrainWeb.SelectOldLines" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="BusinessRule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="/Common/JsLib/PubLib.js" type="text/javascript"></script>
    <script language="javascript" type ="text/javascript">
        function getLineValue() {
            var result = "";
            var v1 = document.getElementsByName("selDocumentID");
            if (v1.length > 0) {
                for (var i = 0; i < v1.length; i++) {
                    if (v1[i].checked) {
                        var sel = v1[i];
                        result = sel.value;
                        break;
                    }
                }
            }

            if (result != "") {
                var p1 = '<%= Request.QueryString["parentid"] %>';
                window.opener.document.getElementById(p1).value = result;
            }

            window.close();
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
                                    <td valign="middle">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="5%">
                                                    <div align="center">
                                                        <img src="../images/tb.gif" width="16" height="16" /></div>
                                                </td>
                                                <td width="95%" class="STYLE1">
                                                    <span class="STYLE3" style="display: none">你当前的位置：</span>财务数据模型-选择既有线路
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
                            <table class="ListTable" cellspacing="1" cellpadding="0">
                                <tr>
                                    <td class="Data" style ="text-align:left">
                                        <asp:Button ID="but1" Text ="确定返回" runat ="server" OnClientClick="javascript:getLineValue();return false;" />
                                    </td>
                                </tr>
                            </table>
                            <!--数据列表区-->
                            <table class="ListTable" cellspacing="1" cellpadding="0" onmouseover="changeto()"
                                onmouseout="changeback()">
                                <tr>
                                    <td width="3%" class="Caption">
                                        选择
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
                                <asp:Repeater ID="Repeater1" runat="server" >
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <input type="checkbox" name="selDocumentID" id="selDocumentID" value='<%#Eval("linevalue")%>' />
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
                                                <a target="_blank" href='NewTrainDetail.aspx?TRAINNAME=<%#Eval("TRAINNAME")%>'>
                                                    <img src="../images/edt.gif" width="16" height="16" />明细</a> &nbsp;
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
       
    </table>
</asp:Content>
