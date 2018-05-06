<%@ Page Title="" Language="C#" MasterPageFile="~/Designer/HtglMain.Master" AutoEventWireup="true" %>
<script runat="server">
    
</script>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:UpdatePanel ID="update1" runat="server">
            <ContentTemplate>
                <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="frm_modelgroup">
                    <ParaItems>
                        <jasp:ParameterItem ParaName="id" DataName="groupname" ParaType="RequestQueryString" IsNullValue="true"  />
                    </ParaItems>
                </jasp:JDataSource>
                <table class="InfoTips">
                    <tr>
                        <td>
                            <img src="../images/ld1.gif" />
                            <b>模块分组</b>
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
                                模块组名称:
                            </td>
                            <td class="Data">
                                <jasp:JTextBox ID="groupname" runat="server" AllowNullValue="false" 
                                Caption="模块组名称" MaxLength="50" IsUnique="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="Caption">
                                模块组图标:
                            </td>
                            <td class="Data" colspan="3">
                                <jasp:JTextBox ID="groupico" runat="server" Caption="模块组图标"
                                 MaxLength="50" Width="520"  />
                            </td>
                            
                        </tr>
                        <tr>
                            <td class="Caption">
                                备注:
                            </td>
                            <td class="Data" colspan="3">
                                <jasp:JTextBox ID="remark" runat="server" Caption="备注" 
                                MaxLength="200" TextMode="MultiLine" Height="60" Width="520" />
                            </td>
                        </tr>
                    </table>
                </jasp:JDetailView>
                                               
                <div class="ButtonArea">
                    <jasp:JButton ID="but1" runat="server" Text="更新" JButtonType="SimpleAction" IsValidatorData="true">
                        <SimpleActionPara TableName="frm_modelgroup" ButtonType="UpdateData" AppendFlag="true" />
                        <ParaItems>
                            <jasp:ParameterItem ParaName="id" DataName="groupname" ParaType="RequestQueryString" />
                        </ParaItems>
                        <ExecutePara SuccInfo="提示：更新数据操作成功！" SuccUrl="-1" FailInfo="提示：更新数据操作失败！" />
                    </jasp:JButton>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
