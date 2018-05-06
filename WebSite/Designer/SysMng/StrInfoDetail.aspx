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
                <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="JStrInfo">
                    <ParaItems>
                        <jasp:ParameterItem ParaName="StrID" ParaType="RequestQueryString" IsNullValue="true"  />
                    </ParaItems>
                </jasp:JDataSource>
                <table class="InfoTips">
                    <tr>
                        <td>
                            <img src="../images/ld1.gif" />
                            <b>字符串资源</b>
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
                                <jasp:JTextBox ID="num" runat="server" AllowNullValue="false" Caption="序号"  DataType="Integer" />
                            </td>
                            <td class="Caption">
                                字符串ID:
                            </td>
                            <td class="Data">
                                <jasp:JTextBox ID="strid" runat="server" AllowNullValue="false" Caption="字符串ID" IsUnique="true" />
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
                                字符串资源:
                            </td>
                            <td class="Data" colspan="3">
                                <jasp:JFCKeditor ID="strtext" runat="server" Height="350" Width="520" 
                                  Caption="字符串资源"  AllowNullValue="false"  />
                            </td>
                            
                        </tr>
                    </table>
                </jasp:JDetailView>
                                               
                
                
                <div class="ButtonArea">
                     <jasp:JButton ID="but1" runat="server" Text="更新" JButtonType="SimpleAction" IsValidatorData="true">
                        <SimpleActionPara TableName="jstrinfo" ButtonType="UpdateData" AppendFlag="true" />
                        <ParaItems>
                            <jasp:ParameterItem ParaName="StrID" ParaType="RequestQueryString" />
                        </ParaItems>
                        <ExecutePara SuccInfo="提示：更新数据操作成功！" SuccUrl="-1" FailInfo="提示：更新数据操作失败！" />
                    </jasp:JButton>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
