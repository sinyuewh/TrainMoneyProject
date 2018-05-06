<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="Fee15_TrainPersonList.aspx.cs" Inherits="WebSite.TrainWeb.SystemProfile.Fee15_TrainPersonList" %>

<%@ Register Src="~/Include/PageNavigator1.ascx" TagName="PageNavigator1" TagPrefix="uc1" %>

<script runat="server">
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="/Common/JsLib/PubLib.js" type="text/javascript"></script>

    <style type="text/css">
        .style1
        {
            height: 18px;
        }
        .Caption
        {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="30" background="../images/tab_05.gif">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="12" height="30">
                            <img src="../images/tab_03.gif" width="12" height="30" />
                        </td>
                        <td>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="46%" valign="middle">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="5%">
                                                    <div align="center">
                                                        <img src="../images/tb.gif" width="16" height="16" /></div>
                                                </td>
                                                <td width="95%" class="STYLE1">
                                                    <span class="STYLE3" style="display: none">你当前的位置：</span>系统配置-列车工作人员配置
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="54%" style="text-align: right">
                                        <jasp:SecurityPanel ID="sec1" runat="server" AuthorityID="001">
                                            <table width="80%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="STYLE1">
                                                        <div align="center">
                                                        </div>
                                                    </td>
                                                    <td class="STYLE1">
                                                        <div align="right" id="div1" runat ="server"  visible ="false" >
                                                            <img src="../images/22.gif" width="14" height="14" />
                                                            <asp:LinkButton ID="link1" runat="server" Text="人员编制初始化" OnClientClick="javascript:return confirm('提示：确定要进行【人员编制数据初始化】吗？');"></asp:LinkButton>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </jasp:SecurityPanel>
                                    </td>
                                    <td>
                                        <jasp:JButton ID="JButton2" runat="server" Text="提交更新" AuthorityID="001" OnClientClick="javascript:return confirm('提示：确定要【提交更新】操作吗？');">
                                        </jasp:JButton>
                                        &nbsp;<jasp:JButton ID="JButton1" runat="server" Text="返 回" JButtonType="RedirectButton">
                                            <ExecutePara SuccUrl="Fee_ZhiChuProfile.aspx" />
                                        </jasp:JButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="16">
                            <img src="../images/tab_07.gif" width="16" height="30" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="8" background="../images/tab_12.gif">
                            &nbsp;
                        </td>
                        <td style ="padding-top:10px">
                            <!--数据列表区-->
                            1) 客车工作人员编制表<br />
                            <asp:Table ID="tab1" runat="server" class="ListTable" cellspacing="1" cellpadding="0"
                                onmouseover="changeto()" onmouseout="changeback()" style="width: 60%">
                            </asp:Table>
                            
                           
                            
                            <br /><br />
                            2) 动车工作人员编制表<br />
                            <asp:Table ID="tab2" runat="server" class="ListTable" cellspacing="1" cellpadding="0"
                                onmouseover="changeto()" onmouseout="changeback()" style="width: 60%">
                            </asp:Table>
                            
                            <br />
                        </td>
                        <td width="8" background="../images/tab_15.gif">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="35" background="../images/tab_19.gif">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
