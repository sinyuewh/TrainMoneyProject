<%@ Page Title="" Language="C#" MasterPageFile="~/Designer/HtglMain.Master" AutoEventWireup="true" %>

<%@ Register Src="../Include/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:UpdatePanel ID="update1" runat="server">
            <ContentTemplate>
                <jasp:JDataSource ID="Data2" runat="server" JType="Table" 
                SqlID="frm_modelgroup" OrderBy="num">
                </jasp:JDataSource>
                <table class="InfoTips">
                    <tr>
                        <td>
                            <img src="../images/ld1.gif" />
                            <b>模块分组</b>
                        </td>
                        <td style="text-align: right; width: 80%">
                            <a href="ModelKindDetail.aspx">
                                <img class="img" src="../images/cs_edit.gif">新增</a>
                            <jasp:SecurityPanel runat="server">
                                &nbsp;<jasp:JLinkButton ID="link1" runat="server" JButtonType="SimpleAction" Text="<img class='img' src='../images/cs_delete.gif'/>删除"
                                    OnClientClick="javascript:if(executeSelectAllDocument('SelDocument','删除')==false) return false;">
                                <SimpleActionPara TableName="Frm_modelgroup" ButtonType="DeleteData" />
                                <ParaItems>
                                    <jasp:ParameterItem ParaName="SelDocument" DataName="groupname" ParaType="RequestForm" />
                                </ParaItems>
                                <ExecutePara FailInfo="提示：删除数据失败！" SuccInfo="提示：删除数据操作成功！" />
                                </jasp:JLinkButton>
                            </jasp:SecurityPanel>
                            &nbsp;<a href="javascript:history.back()"><img class="img" src="../images/cs_back.gif">返回</a>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="Spar">
                        </td>
                    </tr>
                </table>
                <table id="SearchTable" runat="server" class="InfoSearch">
                    <tr>
                        <td style="text-align: right">
                            模块组名称：
                        </td>
                        <td>
                            <jasp:JTextBox ID="text1" DataField="groupname" runat="server" AutoFillValue="true" />
                        </td>
                        <td style="text-align: right">
                            说明：
                        </td>
                        <td>
                            <jasp:JTextBox ID="text2" DataField="remark" runat="server" AutoFillValue="true" />
                            &nbsp;&nbsp;&nbsp; &nbsp;<jasp:JButton ID="butSearch" Text="查询" JButtonType="SearchButton"
                                runat="server" UseSubmitBehavior="false">
                                <SearchPara SearchControlList="text1,text2" />
                            </jasp:JButton>
                            <jasp:JButton ID="JButton1" Text="取消" runat="server" JButtonType="SearchButton" DataSourceID="Data1"
                                UseSubmitBehavior="false">
                                <SearchPara SearchControlList="text1,text2" IsCancelSearch="true" />
                            </jasp:JButton>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="1" class="ListTable">
                    <tr>
                        <td class="Caption" style="width: 10%">
                            序号
                        </td>
                        <td class="Caption">
                            模块组名称
                        </td>
                        <td class="Caption">
                            模块组图标
                        </td>
                        <td class="Caption">
                            备注
                        </td>
                        <td class="Caption" width="10%">
                            <input id="SelAllDocument" type="checkbox" onclick="selectAllDocument('SelDocument',this.checked);" />
                        </td>
                    </tr>
                    <asp:Repeater ID="repeater1" runat="server" DataSourceID="Data2" EnableViewState="false">
                        <ItemTemplate>
                            <tr>
                                <td class="Data">
                                    <%#Eval("num")%>
                                </td>
                                <td class="Data">
                                    <a href='ModelKindDetail.aspx?id=<%#Eval("groupname")%>'>
                                        <%#Eval("groupname") %></a>
                                </td>
                                <td class="Data">
                                    <%#Eval("groupico").ToString().Trim() == String.Empty ? "" : "<img src='" + Eval("groupico").ToString().Trim()+"'/>" %>
                                </td>
                                <td class="Data">
                                    <%#Eval("remark") %>
                                </td>
                                <td class="Data">
                                    <input id="SelDocument" name="SelDocument" type="checkbox" value='<%#Eval("groupname") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <div class="ButtonArea">
                    <uc1:PageNavigator ID="PageNavigator2" runat="server" DataSourceID="Data2" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
