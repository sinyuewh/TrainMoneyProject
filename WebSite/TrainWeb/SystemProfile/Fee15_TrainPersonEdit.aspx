<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="Fee15_TrainPersonEdit.aspx.cs" Inherits="WebSite.TrainWeb.SystemProfile.Fee15_TrainPersonEdit" %>

<script runat="server">
    
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="/Common/JsLib/PubLib.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="30" background="/TrainWeb/images/tab_05.gif">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="12" height="30">
                            <img src="/TrainWeb/images/tab_03.gif" width="12" height="30" />
                        </td>
                        <td>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="46%" valign="middle">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="5%">
                                                    <div align="center">
                                                        <img src="/TrainWeb/images/tb.gif" width="16" height="16" /></div>
                                                </td>
                                                <td width="95%" class="STYLE1">
                                                    <span class="STYLE3" style="display: none">你当前的位置：</span>系统配置-列车工作人员配置
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="54%">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="16">
                            <img src="/TrainWeb/images/tab_07.gif" width="16" height="30" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="8" background="/TrainWeb/images/tab_12.gif">
                            &nbsp;
                        </td>
                        <td style="height: 600px; vertical-align: top">
                            <!--数据源定义-->
                            <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="TRAINPERSON" OrderBy="num">
                                <ParaItems>
                                    <jasp:ParameterItem ParaName="num" ParaType="RequestQueryString" IsNullValue="true" />
                                </ParaItems>
                            </jasp:JDataSource>
                            <jasp:JDetailView ID="DetailView1" runat="server" DataSourceID="Data1" ControlList="*">
                                <table class="DetailTable" cellspacing="1" cellpadding="0" style="width: 60%; margin-left: 15px">
                                    <tr>
                                        <td class="Caption">
                                            序号
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="num" runat="server" AllowNullValue="false" Caption="序号"
                                              DataType="Integer" IsUnique="true"></jasp:JTextBox>
                                        </td>
                                        <td class="Caption">
                                            类别
                                        </td>
                                        <td class="Data">
                                            <jasp:JDropDownList ID="kind" runat="server">
                                                <asp:ListItem Text="普通客车" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="动车" Value="1"></asp:ListItem>
                                            </jasp:JDropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Caption">
                                            车型
                                        </td>
                                        <td class="Data">
                                            <jasp:JDropDownList ID="traintype" runat="server">
                                                <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                                            </jasp:JDropDownList>
                                        </td>
                                        <td class="Caption">
                                            人员岗位
                                        </td>
                                        <td class="Data">
                                            <jasp:JDropDownList ID="gw" runat="server">
                                                <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                                            </jasp:JDropDownList>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            人员编制
                                        </td>
                                        <td class="Data" colspan="3">
                                           <jasp:JTextBox ID="pcount" runat="server" AllowNullValue="false" Caption="人员编制"
                                              DataType="Integer" Width="100" ></jasp:JTextBox>
                                        </td>
                                        
                                    </tr>
                                </table>
                            </jasp:JDetailView>
                            <div class="ButtonArea" style="width: 60%">
                                <asp:UpdatePanel ID="update1" runat="server">
                                    <ContentTemplate>
                                        <jasp:JButton ID="button1" AuthorityID="001" runat="server" Text="提 交" JButtonType="SimpleAction"
                                            IsValidatorData="true">
                                            <ParaItems>
                                                <jasp:ParameterItem ParaName="num" ParaType="RequestQueryString" IsNullValue="true" />
                                            </ParaItems>
                                            <SimpleActionPara TableName="TRAINPERSON" ButtonType="UpdateData" AppendFlag="true" />
                                            <ExecutePara SuccInfo="提示：提交数据操作成功！" SuccUrl="-1" FailInfo="提示：提交数据操作失败！" />
                                        </jasp:JButton>
                                        &nbsp;
                                        <jasp:JButton ID="JButton1" runat="server" Text="返 回" JButtonType="RedirectButton">
                                            <ExecutePara SuccUrl="-1" />
                                        </jasp:JButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            &nbsp;
                        </td>
                        <td width="8" background="/TrainWeb/images/tab_15.gif">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="35" background="/TrainWeb/images/tab_19.gif">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="12" height="35">
                            <img src="/TrainWeb/images/tab_18.gif" width="12" height="35" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td width="16">
                            <img src="/TrainWeb/images/tab_20.gif" width="16" height="35" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
