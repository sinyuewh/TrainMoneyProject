<%@ Page Title="税金年配置列表" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master"
    AutoEventWireup="true" CodeBehind="Shouru08_SRateList.aspx.cs" Inherits="WebSite.TrainWeb.SystemProfile.Shouru08_SRateList" %>

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
    
    <script language ="javascript" type ="text/javascript">
        function AddNewData() {
            var succ = true;
            var d1 = '<%=DateTime.Now.ToString("yyyy")%>';
            var msg = window.prompt("请确定要税金的年份（如2012）", d1);
            if (msg == null || msg == undefined) {
                succ = false;
            }
            else {
                document.getElementById('<%=byear.ClientID %>').value = msg;
            }
            return succ;
        }
    </script>
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
                                                    <span class="STYLE3" style="display: none">你当前的位置：</span>系统配置-税金年配置表
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="54%" style="text-align: right">
                                        <asp:HiddenField ID="byear" runat ="server" />
                                        <jasp:JButton ID="JButton2" runat="server" Text="成批更新" AuthorityID="001" OnClientClick="return confirm('提示：确定要成批更新数据吗？');">
                                        </jasp:JButton>
                                        <jasp:JButton ID="JButton1" runat="server" Text="新 增" AuthorityID="001" OnClientClick="return AddNewData();">
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
                        <td>
                            <!--数据列表区-->
                            <table class="ListTable" cellspacing="1" cellpadding="0" onmouseover="changeto()"
                                onmouseout="changeback()">
                                <tr>
                                    <td class="Caption">
                                        序号
                                    </td>
                                    <td class="Caption">
                                        年份
                                    </td>
                                    <td class="Caption">
                                        税金
                                    </td>
                                </tr>
                                <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="SRATEPROFILE" PageSize="-1"
                                    OrderBy="byear desc">
                                </jasp:JDataSource>
                                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Data1">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <%#Container.ItemIndex+1 %>
                                            </td>
                                            <td class="Data">
                                                <jasp:JLabel ID="byear" runat="server" Text=' <%#Eval("byear") %>'></jasp:JLabel>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Srate" runat="server" Text='<%#Eval("Srate")%>'></jasp:JTextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
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
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="12" height="35">
                            <img src="../images/tab_18.gif" width="12" height="35" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td width="16">
                            <img src="../images/tab_20.gif" width="16" height="35" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
