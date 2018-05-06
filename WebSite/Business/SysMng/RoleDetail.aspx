﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Designer/HtglMain.Master" AutoEventWireup="true" %>
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
            if (this.roleid.Text!=String.Empty)
            {
                this.roleid.Enabled = false;
                this.roleusers.Text = JRoleBU.GetRoleUsersByRoleID(this.roleid.Text);
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
                <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="JRole">
                    <ParaItems>
                        <jasp:ParameterItem ParaName="RoleID" ParaType="RequestQueryString"
                         IsNullValue="true"  />
                    </ParaItems>
                </jasp:JDataSource>
                <table class="InfoTips">
                    <tr>
                        <td>
                            <img src="../images/ld1.gif" />
                            <b>系统角色</b>
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
                                角色ID:
                            </td>
                            <td class="Data">
                                <jasp:JTextBox ID="roleid" runat="server" AllowNullValue="false"
                                 Caption="角色ID" IsUnique="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="Caption">
                                角色名称:
                            </td>
                            <td class="Data">
                                <jasp:JTextBox ID="rolename" runat="server" Caption="角色名称" MaxLength="50" 
                                  AllowNullValue="false" IsUnique="true" />
                            </td>
                            <td class="Caption">
                                部门关联:
                            </td>
                            <td class="Data">
                                <jasp:JCheckBox ID="departflag" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="Caption">
                                隶属模块:
                            </td>
                            <td class="Data">
                                <jasp:AppDropDownList ID="modelid" runat="server" Caption="隶属模块" >
                                </jasp:AppDropDownList>
                            </td>
                            <td class="Caption">
                                备注:
                            </td>
                            <td class="Data">
                                <jasp:JTextBox ID="remark" runat="server" Caption="备注" MaxLength="200" />
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="Caption">
                                角色用户:
                            </td>
                            <td class="Data" colspan="3">
                                <jasp:JTextBox ID="roleusers" runat="server" Caption="角色用户" 
                                  TextMode="MultiLine"  Height="150" Width="520" /><br />
                                  填*表示所有注册用户，部门ID.*表示改部门所有用户，用户之间使用逗号分隔
                            </td>
                            
                        </tr>
                    </table>
                </jasp:JDetailView>
                                               
                
                
                <div class="ButtonArea">
                     <jasp:JButton ID="but1" runat="server" Text="更新" JButtonType="BusinessRule" 
                          IsValidatorData="true">
                        <AssemblePara AssembleFile="WebFrame.DLL" ClassLibName="WebFrame.Designer.JRoleBU" 
                          MethodName="SaveRole" />
                        <ParaItems>
                            <jasp:ParameterItem ParaName="roleid" ParaType="Control" />
                            <jasp:ParameterItem ParaName="num,roleid,rolename,departflag,modelid,remark" ParaType="ControlList" />
                            <jasp:ParameterItem ParaName="roleusers" ParaType="Control" />
                        </ParaItems>
                        <ExecutePara SuccInfo="提示：更新数据操作成功！" SuccUrl="-1" FailInfo="提示：更新数据操作失败！" />
                    </jasp:JButton>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
