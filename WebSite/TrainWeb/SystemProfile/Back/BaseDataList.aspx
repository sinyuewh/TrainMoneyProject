<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true" %>

<%@ Register Src="~/Include/PageNavigator1.ascx" TagName="PageNavigator1" TagPrefix="uc1" %>

<script runat="server">
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="/Common/JsLib/PubLib.js" type="text/javascript"></script>

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
                                                    <span class="STYLE3">你当前的位置</span>：系统配置-基础数据
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="54%">
                                        <% if (false)
                                           { %>
                                        <table border="0" align="right" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td width="60">
                                                    <table width="87%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td class="STYLE1">
                                                                <div align="center">
                                                                    <input type="checkbox" name="checkbox62" value="checkbox" />
                                                                </div>
                                                            </td>
                                                            <td class="STYLE1">
                                                                <div align="center">
                                                                    全选</div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="60">
                                                    <table width="90%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td class="STYLE1">
                                                                <div align="center">
                                                                    <img src="../images/22.gif" width="14" height="14" /></div>
                                                            </td>
                                                            <td class="STYLE1">
                                                                <div align="center">
                                                                    新增</div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="60">
                                                    <table width="90%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td class="STYLE1">
                                                                <div align="center">
                                                                    <img src="../images/33.gif" width="14" height="14" /></div>
                                                            </td>
                                                            <td class="STYLE1">
                                                                <div align="center">
                                                                    修改</div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="52">
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
                                        <%} %>
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
                                    <td width="15%" class="Caption">
                                        配置项
                                    </td>
                                    <td width="14%" class="Caption">
                                        数据值
                                    </td>
                                    <td width="18%" class="Caption">
                                        数据说明
                                    </td>
                                    <td width="15%" class="Caption">
                                        基本操作
                                    </td>
                                </tr>
                                <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="JSTRINFO" PageSize="-1"
                                    OrderBy="num">
                                    <ParaItems>
                                        <jasp:ParameterItem ParaName="syskind is null" ParaType="String" DataName="$$empty" />
                                    </ParaItems>
                                </jasp:JDataSource>
                                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Data1" EnableViewState="false">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <input type="checkbox" name="selDocumentID" id="selDocumentID" value="checkbox" />
                                            </td>
                                            <td class="Data">
                                                <%#Container.ItemIndex+1 %>
                                            </td>
                                            <td class="Data_Left">
                                                <a href='BaseDataEdit.aspx?keyid=<%# Server.UrlEncode(Eval("StrID").ToString())%>'>
                                                    <%#Eval("StrID")%></a>
                                            </td>
                                            <td class="Data_Right" style="padding-right: 5px">
                                                <%#Eval("StrText")%>
                                            </td>
                                            <td class="Data_Left" style="padding-left: 15px">
                                                <%#Eval("remark")%>
                                            </td>
                                            <td class="Data">
                                                <a href='BaseDataEdit.aspx?keyid=<%# Server.UrlEncode(Eval("StrID").ToString())%>'>
                                                    <img src="../images/edt.gif" width="16" height="16" />明细</a>
                                                <%if (false)
                                                  {%>&nbsp;<img src="../images/del.gif" width="16" height="16" />删除<%} %>
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
                            &nbsp;<uc1:PageNavigator1 ID="PageNavigator11" runat="server" DataSourceID="Data1"
                                Visible="false" />
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
