<%@ Page Title="系统模块" Language="C#" MasterPageFile="~/Designer/HtglMain.Master" AutoEventWireup="true" %>
<%@ Import Namespace="WebFrame.Designer" %>
<script runat="server">
    protected override void OnLoad(EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Frm_ModelGroupBU.SetModelGroupForListControl(this.groupname, "请选择");
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
                <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="frm_model">
                    <ParaItems>
                        <jasp:ParameterItem ParaName="id" DataName="modelid" ParaType="RequestQueryString" IsNullValue="true"  />
                    </ParaItems>
                </jasp:JDataSource>
                <table class="InfoTips">
                    <tr>
                        <td>
                            <img src="../images/ld1.gif" />
                            <b>系统模块</b>
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
                                <jasp:JTextBox ID="num" runat="server" AllowNullValue="false" 
                                  Caption="序号"  DataType="Integer" MaxLength="10" />
                            </td>
                            <td class="Caption">
                                所属模块组:
                            </td>
                            <td class="Data">
                                <jasp:JDropDownList ID="groupname" runat="server" Caption="模块组名称">
                                </jasp:JDropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="Caption">
                                模块标识:
                            </td>
                            <td class="Data">
                                <jasp:JTextBox ID="modelid" runat="server" AllowNullValue="false" 
                                Caption="模块ID" MaxLength="50" IsUnique="true" />
                            </td>
                            <td class="Caption">
                                模块名称:
                            </td>
                            <td class="Data">
                                <jasp:JTextBox ID="modelname" runat="server" AllowNullValue="false" 
                                Caption="模块名称" MaxLength="50" IsUnique="true" />
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="Caption" >
                                模块图标:
                            </td>
                            <td class="Data" colspan="3">
                                <jasp:JTextBox ID="modelico" runat="server"  
                                Caption="模块ICO" MaxLength="200" Width="520"  />
                            </td>
                            
                        </tr>
                        
                        <tr>
                            <td class="Caption" >
                                模块URL:
                            </td>
                            <td class="Data" colspan="3">
                                <jasp:JTextBox ID="modelurl" runat="server"  
                                Caption="模块URL" MaxLength="200" Width="520"  />
                            </td>
                            
                        </tr>
                        
                        <tr>
                            <td class="Caption" >
                                备注:
                            </td>
                            <td class="Data" colspan="3">
                                <jasp:JTextBox ID="remark" runat="server"  
                                Caption="备注"  TextMode="MultiLine" Height="60"
                                 MaxLength="200" Width="518"  />
                            </td>
                            
                        </tr>
                    </table>
                </jasp:JDetailView>
                                               
                <div class="ButtonArea">
                    <jasp:JButton ID="but1" runat="server" Text="更新" JButtonType="SimpleAction" IsValidatorData="true">
                        <SimpleActionPara TableName="frm_model" ButtonType="UpdateData" AppendFlag="true" />
                        <ParaItems>
                            <jasp:ParameterItem ParaName="id" DataName="modelid" ParaType="RequestQueryString" />
                        </ParaItems>
                        <ExecutePara SuccInfo="提示：更新数据操作成功！" SuccUrl="-1" FailInfo="提示：更新数据操作失败！" />
                    </jasp:JButton>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
