<%@ Page Title="业务层代码" Language="C#" MasterPageFile="~/Designer/HtglMain.Master" AutoEventWireup="true"  %>
<%@ Import Namespace="WebFrame.Designer" %>

<script runat="server">
    //======业务层项目文件信息=================
    private String ProjectFileDir = System.Configuration.ConfigurationManager.AppSettings["BusinessProjectFileDir"];
    private String ModelFileName = "CommBusiness.txt";

    protected override void OnInit(EventArgs e)
    {
        this.butCreateCode.Click += new EventHandler(butCreateCode_Click);
        this.butChangeResponsible.Click += new EventHandler(butChangeResponsible_Click);
        base.OnInit(e);
    }

    //调整模块的责任人
    void butChangeResponsible_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(Request.Form["SelDocument"]) == false)
        {
            Frm_SystableBU.ChangeTableResponsible(Request.Form["SelDocument"],
                this.responsible.Text);
        }
    }

    //Create Code
    void butCreateCode_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(ProjectFileDir) == false)
        {
            bool succ = JCreateCode.CreateCode(Request.Form["SelDocument"],
                ProjectFileDir,
                Server.MapPath(ModelFileName));
            if (succ)
            {
                WebFrame.Util.JAjax.Alert(" 提示：生成业务层代码操作成功！");
            }
            else
            {
                WebFrame.Util.JAjax.Alert(" 提示：生成业务层代码操作失败，可能的原因是业务层的项目文件路径不对！");
            }
        }
        else
        {
            WebFrame.Util.JAjax.Alert(" 错误提示：没有正确的设置业务层项目文件的位置！");
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
</script>

<%@ Register Src="../Include/PageNavigator.ascx" TagName="PageNavigator" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:UpdatePanel ID="update1" runat="server">
            <ContentTemplate>
                <jasp:JDataSource ID="Data2" runat="server" JType="Table"  PageSize="-1"
                    SqlID="frm_systable">
                    <ParaItems>
                        <jasp:ParameterItem ParaName="modelid<>'-1'" ParaType="String" DataName="$$empty" />
                        <jasp:ParameterItem ParaName="classname is not null" ParaType="String" DataName="$$empty" />
                    </ParaItems>
                </jasp:JDataSource>
                <table class="InfoTips">
                    <tr>
                        <td>
                            <img src="../images/ld1.gif" />
                            <b>业务层代码管理</b>
                        </td>
                        <td style="text-align: right; width: 80%">
                             <jasp:SecurityPanel ID="SecurityPanel3" runat="server"> 
                                &nbsp;<jasp:JLinkButton ID="butChangeResponsible" runat="server" Text="<img class='img' src='../images/53.png'/>调整责任人" 
                                       OnClientClick="javascript:if(confirm('提示：确定要调整业务表的责任人吗？')==false) return false;">
                                  <ExecutePara FailInfo="提示：调整责任人操作失败！" SuccInfo="提示：调整责任人操作成功！" />
                                </jasp:JLinkButton>
                            </jasp:SecurityPanel>
                            
                            <jasp:SecurityPanel ID="SecurityPanel2" runat="server"> 
                                &nbsp;<jasp:JLinkButton ID="butCreateCode" runat="server" Text="<img class='img' src='../images/cs_set.gif'/>生成业务层代码" 
                                       OnClientClick="javascript:if(executeSelectAllDocument('SelDocument','生成业务层代码')==false) return false;">
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
                            <jasp:JButton ID="JButton1" Text="取消" runat="server" JButtonType="SearchButton" 
                            DataSourceID="Data2"
                                UseSubmitBehavior="false">
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
                            表名称
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
               
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
