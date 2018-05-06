<%@ Page Title="" Language="C#" MasterPageFile="~/Designer/HtglMain.Master" AutoEventWireup="true"  %>
<%@ Import Namespace="WebFrame.Designer" %>

<%@ Register Src="../Include/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<script runat="server">
    protected override void OnLoad(EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Frm_ModelBU.SetModelForListControl(this.modelid, "不限");
        }
        base.OnLoad(e);
    }
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:UpdatePanel ID="update1" runat="server">
            <ContentTemplate>
                <jasp:JDataSource ID="Data2" runat="server" JType="Table" 
                    SqlID="JAUTHORITY" PageSize="15"
                    OrderBy="num">
                </jasp:JDataSource>
                <table class="InfoTips">
                    <tr>
                        <td>
                            <img src="../images/ld1.gif" />
                            <b>权限功能点</b>
                        </td>
                        <td style="text-align: right">
                            <a href="AuthorityDetail.aspx">
                                <img class="img" src="../images/5.gif">新增</a>
                            <jasp:SecurityPanel ID="SecurityPanel1" runat="server">
                                &nbsp;<jasp:JLinkButton ID="link1" runat="server" JButtonType="BusinessRule" Text="<img class='img' src='../images/cs_delete.gif'/>删除"
                                    OnClientClick="javascript:if(executeSelectAllDocument('SelDocument','删除')==false) return false;">
                                <AssemblePara AssembleFile="WebFrame.DLL" ClassLibName="WebFrame.Designer.JAuthorityBU" MethodName="DeleteAuthority"  />
                                <ParaItems>
                                    <jasp:ParameterItem ParaName="SelDocument" DataName="AuthorityID" ParaType="RequestForm" />
                                </ParaItems>
                                <ExecutePara FailInfo="提示：删除数据失败！" SuccInfo="提示：删除数据操作成功！" />
                                </jasp:JLinkButton>
                            </jasp:SecurityPanel>
                            <a href="javascript:history.back()">
                                <img class="img" src="../images/cs_back.gif">返回</a>
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
                            所属模块：
                        </td>
                        <td>
                            <jasp:AppDropDownList ID="modelid" runat="server" AutoFillValue="true">
                            </jasp:AppDropDownList>
                        </td>
                        <td style="text-align: right">
                            功能点名称：
                        </td>
                        <td>
                            <jasp:JTextBox ID="authorityname" runat="server" AutoFillValue="true" Width="100" />
                        </td>
                        <td style="text-align: right">
                            备注：
                        </td>
                        <td>
                            <jasp:JTextBox ID="remark" runat="server" AutoFillValue="true" Width="100" />
                            &nbsp;&nbsp;&nbsp; &nbsp;<jasp:JButton ID="butSearch" Text="查询" JButtonType="SearchButton"
                                runat="server" UseSubmitBehavior="false">
                                <SearchPara SearchControlList="authorityname,remark,modelid" />
                            </jasp:JButton>
                            &nbsp;
                            <jasp:JButton ID="JButton1" Text="取消" runat="server" JButtonType="SearchButton" DataSourceID="Data1"
                                UseSubmitBehavior="false">
                                <SearchPara SearchControlList="authorityname,remark,modelid" IsCancelSearch="true" />
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
                            功能点ID
                        </td>
                        <td class="Caption">
                            功能点名称
                        </td>
                        <td class="Caption">
                            所属模块
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
                                    <a href='AuthorityDetail.aspx?AuthorityID=<%# Eval("authorityid")%>'>
                                        <%#Eval("authorityid")%></a>
                                </td>
                                <td class="Data">
                                    <%#Eval("authorityname")%>
                                </td>
                                 <td class="Data">
                                    <%#Frm_ModelBU.GetModelNameByID(Eval("modelid").ToString()) %>
                                </td>
                                <td class="Data">
                                    <%#Eval("remark") %>
                                </td>
                                <td class="Data">
                                    <input id="SelDocument" name="SelDocument" type="checkbox" value='<%#Eval("authorityid") %>' />
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

