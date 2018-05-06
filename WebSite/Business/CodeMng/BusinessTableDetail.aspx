<%@ Page Title="业务表管理" Language="C#" MasterPageFile="~/Designer/HtglMain.Master"
 AutoEventWireup="true" %>
 <%@ Import Namespace="WebFrame.Designer" %>
 <script runat="server">
    protected override void OnInit(EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Frm_ModelBU.SetModelForListControl(this.modelid, "");
        }
        base.OnInit(e);
    }

    protected override void OnPreRenderComplete(EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (this.tableid.Text != String.Empty)
            {
                this.tableid.Enabled = false;
            }
        }
        base.OnPreRenderComplete(e);
    }
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:UpdatePanel ID="update1" runat="server">
            <ContentTemplate>
                <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="frm_systable">
                    <ParaItems>
                        <jasp:ParameterItem ParaName="id" DataName="tableid" 
                           ParaType="RequestQueryString" IsNullValue="true"  />
                    </ParaItems>
                </jasp:JDataSource>
                <table class="InfoTips">
                    <tr>
                        <td>
                            <img src="../images/ld1.gif" />
                            <b>业务表管理</b>
                        </td>
                        <td style="text-align: right">
                            <a href="javascript:history.back()">
                                <img class="img" src="../images/cs_back.gif">返回</a>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="Spar">
                        </td>
                    </tr>
                </table>
                <jasp:JDetailView ID="DetailView1" runat="server" DataSourceID="Data1" ControlList="*">
                    <table cellpadding="0" cellspacing="1" class="DetailTable">
                        <tr>
                            <td class="Caption">
                                序号:
                            </td>
                            <td class="Data">
                                <jasp:JTextBox ID="num" runat="server" DataType="Integer"
                                     AllowNullValue="false" Caption="序号" MaxLength="10" />
                            </td>
                            <td class="Caption">
                                所属模块:
                            </td>
                            <td class="Data">
                                <jasp:AppDropDownList ID="modelid" runat="server" Caption="模块ID" >
                                </jasp:AppDropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="Caption">
                                表标识:
                            </td>
                            <td class="Data">
                                <jasp:JTextBox ID="tableid" IsUnique="true" runat="server" 
                                  AllowNullValue="false" Caption="表标识" MaxLength="50" />
                            </td>
                            <td class="Caption">
                                表名称:
                            </td>
                            <td class="Data">
                                <jasp:JTextBox ID="tablecaption" runat="server" AllowNullValue="false"
                                 Caption="表名称" MaxLength="50" />
                            </td>
                        </tr>
                        <tr>
                            <td class="Caption">
                                业务类名称:
                            </td>
                            <td class="Data">
                                <jasp:JTextBox ID="classname" runat="server" 
                                 Caption="业务类名称" MaxLength="50" />
                            </td>
                            <td class="Caption">
                                责任人:
                            </td>
                            <td class="Data">
                                <jasp:JTextBox ID="responsible" runat="server" Caption="责任人" 
                                MaxLength="50" />
                            </td>
                        </tr>
                         <tr>
                            <td class="Caption">
                                备注:
                            </td>
                            <td class="Data" colspan="3">
                                <jasp:JTextBox ID="remark" runat="server" Caption="备注" 
                                MaxLength="200" Width="518" TextMode="MultiLine" Height="60" />
                            </td>
                            
                        </tr>
                    </table>
                </jasp:JDetailView>
                                               
                <div class="ButtonArea">
                    <jasp:JButton ID="but1" runat="server" Text="更新" JButtonType="SimpleAction" IsValidatorData="true">
                        <SimpleActionPara TableName="frm_systable" ButtonType="UpdateData" AppendFlag="true" />
                        <ParaItems>
                            <jasp:ParameterItem ParaName="id" DataName="tableid" ParaType="RequestQueryString" />
                        </ParaItems>
                        <ExecutePara SuccInfo="提示：更新数据操作成功！" SuccUrl="-1" FailInfo="提示：更新数据操作失败！" />
                    </jasp:JButton>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
