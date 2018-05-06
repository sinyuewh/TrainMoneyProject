<%@ Page Title="" Language="C#" MasterPageFile="~/Designer/HtglMain.Master" AutoEventWireup="true" %>
<%@ Import Namespace="WebFrame.Designer" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Frm_ModelBU.SetModelForListControl(this.modelid, "");
        }
    }
    protected override void OnPreRenderComplete(EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (this.authorityid.Text!=String.Empty)
            {
                this.authorityid.Enabled = false;
                this.roleids.Text = JAuthorityBU.GetAuthorityRoleByAuthorityID(this.authorityid.Text);
            }

            if (Data1.TotalRow == 0)
            {
                this.author.Text = WebFrame.Util.JCookie.GetCookieValue("HTGL_UserName");
                this.createtime.Text = DateTime.Now.ToString("yyyy-MM-dd");
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
                <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="JAuthority">
                    <ParaItems>
                        <jasp:ParameterItem ParaName="AuthorityID" ParaType="RequestQueryString"
                         IsNullValue="true"  />
                    </ParaItems>
                </jasp:JDataSource>
                <table class="InfoTips">
                    <tr>
                        <td>
                            <img src="../images/ld1.gif" />
                            <b>权限功能点</b>
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
                                <jasp:JTextBox ID="num" runat="server" AllowNullValue="false" Caption="序号"  
                                DataType="Integer" />
                            </td>
                            <td class="Caption">
                                权限功能点ID:
                            </td>
                            <td class="Data">
                                <jasp:JTextBox ID="authorityid" runat="server" AllowNullValue="false"
                                 Caption="权限功能点ID" IsUnique="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="Caption">
                                功能点名称:
                            </td>
                            <td class="Data">
                                <jasp:JTextBox ID="authorityname" runat="server" Caption="权限功能点名称" MaxLength="50" 
                                  AllowNullValue="false" IsUnique="true" />
                            </td>
                            <td class="Caption">
                                隶属模块:
                            </td>
                            <td class="Data">
                                <jasp:AppDropDownList ID="modelid" runat="server" Caption="隶属模块" >
                                </jasp:AppDropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="Caption">
                                作者:
                            </td>
                            <td class="Data">
                                <jasp:JTextBox ID="author" runat="server" Caption="作者" MaxLength="50" />
                            </td>
                            <td class="Caption">
                                创建时间:
                            </td>
                            <td class="Data">
                                <jasp:JTextBox ID="createtime" runat="server" Caption="创建时间" 
                                  DataType="DateTime" Format="yyyy-MM-dd" onfocus="calendar()" />
                            </td>
                        </tr>
                        <tr>
                            
                            <td class="Caption">
                                备注:
                            </td>
                            <td class="Data" colspan="3">
                                <jasp:JTextBox ID="remark" runat="server" Caption="备注" MaxLength="200" Width="520" />
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="Caption">
                                角色用户:
                            </td>
                            <td class="Data" colspan="3">
                                <jasp:JTextBox ID="roleids" runat="server" Caption="角色用户" 
                                  TextMode="MultiLine"  Height="150" Width="520" /><br />
                                  填*表示所有角色，角色之间使用逗号分隔
                            </td>
                            
                        </tr>
                    </table>
                </jasp:JDetailView>
                
                <div class="ButtonArea">
                     <jasp:JButton ID="but1" runat="server" Text="更新" JButtonType="BusinessRule" 
                          IsValidatorData="true">
                        <AssemblePara AssembleFile="WebFrame.DLL" ClassLibName="WebFrame.Designer.JAuthorityBU" 
                          MethodName="SaveAuthority" />
                        <ParaItems>
                            <jasp:ParameterItem ParaName="authorityid" ParaType="Control" />
                            <jasp:ParameterItem ParaName="num,author,createtime,authorityid,authorityname,modelid,remark" ParaType="ControlList" />
                            <jasp:ParameterItem ParaName="roleids" ParaType="Control" />
                        </ParaItems>
                        <ExecutePara SuccInfo="提示：更新数据操作成功！" SuccUrl="-1" FailInfo="提示：更新数据操作失败！" />
                    </jasp:JButton> 
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
