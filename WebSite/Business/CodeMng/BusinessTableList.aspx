<%@ Page Title="业务表管理" Language="C#" MasterPageFile="~/Designer/HtglMain.Master" 
AutoEventWireup="true"  %>
<%@ Import Namespace="WebFrame.Designer" %>
<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        this.butImportTable.Click += new EventHandler(butImportTable_Click);
        this.butChangeModel.Click += new EventHandler(butChangeModel_Click);
        base.OnInit(e);
    }

    void butChangeModel_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(Request.Form["SelDocument"]) == false
            && String.IsNullOrEmpty(this.modelid.SelectedValue) == false)
        {
            Frm_SystableBU.ChangeTableModel(Request.Form["SelDocument"],
                this.modelid.SelectedValue);
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Frm_ModelBU.SetModelForListControl(this.modelid, "不限");
        }
        base.OnLoad(e);
    }
    
    //导入业务数据库的操作
    void butImportTable_Click(object sender, EventArgs e)
    {
        Frm_SystableBU.ImportTable();;
    }
</script>

<%@ Register Src="../Include/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:UpdatePanel ID="update1" runat="server">
            <ContentTemplate>
                <jasp:JDataSource ID="Data2" runat="server" 
                    JType="Business">
                    <AssemblePara AssembleFile="WebFrame.Dll" 
                    ClassLibName="WebFrame.Designer.Frm_SystableBU" 
                    MethodName="GetList" />
                    <ParaItems>
                        <jasp:ParameterItem ParaName="Data2" ParaType="String" />
                        <jasp:ParameterItem ParaName="20" ParaType="String"  />
                    </ParaItems>
                </jasp:JDataSource>
                <table class="InfoTips">
                    <tr>
                        <td>
                            <img src="../images/ld1.gif" />
                            <b>业务表管理</b>
                        </td>
                        <td style="text-align: right; width: 80%">
                            <a href="BusinessTableDetail.aspx">
                                <img class="img" src="../images/cs_edit.gif">新增</a>
                                
                            <jasp:SecurityPanel ID="SecurityPanel2" runat="server"> 
                                &nbsp;<jasp:JLinkButton ID="butImportTable" runat="server" Text="<img class='img' src='../images/cs_set.gif'/>导入" 
                                       OnClientClick="javascript:if(confirm('提示：确定要导入业务表的数据吗？')==false) return false;">
                                  <ExecutePara FailInfo="提示：导入数据操作失败！" SuccInfo="提示：导入数据操作成功！" />
                                </jasp:JLinkButton>
                            </jasp:SecurityPanel>
                            
                            <jasp:SecurityPanel ID="SecurityPanel3" runat="server"> 
                                &nbsp;<jasp:JLinkButton ID="butChangeModel" runat="server" Text="<img class='img' src='../images/53.png'/>调整模块" 
                                       OnClientClick="javascript:if(confirm('提示：确定要调整业务表的模块吗？')==false) return false;">
                                  <ExecutePara FailInfo="提示：调整模块操作失败！" SuccInfo="提示：调整模块操作成功！" />
                                </jasp:JLinkButton>
                            </jasp:SecurityPanel>
                            
                            <jasp:SecurityPanel ID="SecurityPanel1" runat="server">
                                &nbsp;<jasp:JLinkButton ID="link1" runat="server" JButtonType="SimpleAction" Text="<img class='img' src='../images/cs_delete.gif'/>删除"
                                    OnClientClick="javascript:if(executeSelectAllDocument('SelDocument','删除')==false) return false;">
                                <SimpleActionPara TableName="Frm_SysTable" ButtonType="DeleteData" />
                                <ParaItems>
                                    <jasp:ParameterItem ParaName="SelDocument" DataName="tableid" ParaType="RequestForm" />
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
                            所属模块：
                        </td>
                        <td>
                            <jasp:AppDropDownList ID="modelid" runat="server" AutoFillValue="true">
                            </jasp:AppDropDownList>
                        </td>
                        <td style="text-align: right">
                            责任人：
                        </td>
                        <td>
                           <jasp:JTextBox ID="responsible" Width="100" runat="server" AutoFillValue="true" />
                        </td>
                        <td style="text-align: right">
                            表名称：
                        </td>
                        <td>
                            <jasp:JTextBox ID="tablecaption" Width="100" runat="server" AutoFillValue="true" />
                            &nbsp;&nbsp;&nbsp; &nbsp;<jasp:JButton ID="butSearch" Text="查询" JButtonType="SearchButton"
                                runat="server" UseSubmitBehavior="false" DataSourceID="Data2">
                                <SearchPara SearchControlList="modelid,tablecaption,responsible" />
                            </jasp:JButton>
                            <jasp:JButton ID="JButton1" Text="取消" runat="server" JButtonType="SearchButton" DataSourceID="Data2">
                                <SearchPara SearchControlList="modelid,tablecaption,responsible" IsCancelSearch="true" />
                            </jasp:JButton>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="1" class="ListTable">
                    <tr>
                        <td class="Caption" style="width: 8%">
                            序号
                        </td>
                        <td class="Caption">
                            所属模块
                        </td>
                        <td class="Caption">
                            表ID
                        </td>
                        <td class="Caption">
                            <jasp:JLinkButton ID="sortLink" runat="server" Text="表名称" Font-Bold="true" 
                            JButtonType="OrderButton" DataSourceID="Data2">
                                <SortPara SortExpress="tablecaption"  />
                            </jasp:JLinkButton>
                        </td>
                        <td class="Caption">
                           类标识
                        </td>
                        <td class="Caption">
                           责任人
                        </td>
                        <td class="Caption">
                           备注
                        </td>
                        <td class="Caption">
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
                                    <%#Frm_ModelBU.GetModelNameByID(Eval("modelid").ToString()) %>
                                </td>
                                <td class="Data_Left">
                                    <a href='BusinessTableDetail.aspx?id=<%#Eval("tableid") %>'><%#Eval("tableid") %></a>
                                </td>
                                <td class="Data_Left">
                                    <%#Eval("tablecaption") %>
                                </td>
                                <td class="Data_Left">
                                    <%#Eval("classname") %>
                                </td>
                                <td class="Data">
                                    <%#Eval("responsible")%>
                                </td>
                                <td class="Data">
                                    <%#Eval("remark") %>
                                </td>
                                <td class="Data">
                                    <input id="SelDocument" name="SelDocument" type="checkbox" value='<%#Eval("tableid") %>' />
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
