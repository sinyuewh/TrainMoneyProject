<%@ Page Title="输入分段检索的条件" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master"
    AutoEventWireup="true" CodeBehind="SelectMulTrainLine.aspx.cs" Inherits="WebSite.TrainWeb.Fenxi.SelectMulTrainLine" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="/Common/JsLib/PubLib.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function changevalue(c0, c1, n0, n1)
        {
            var b0 = '<%=Request.QueryString["bstation"] %>';
            var b1 = document.getElementById(c1).value;
           
            if (b1 != b0
               && document.getElementById(c0).value != ""
               && document.getElementById(c1).value != "") {
                document.getElementById(n0).value = b1;
                document.getElementById(n1).value = b0;
            }
        }
        
        //选择合适的线路ID
        function selLine(c0) {
            var url1 = "../train/SelectTrainLine.aspx?parent=" + c0;
            myOpenURLWithScroll(url1, 600, 900);
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="update2" runat="server">
        <ContentTemplate>
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
                                                            <span class="STYLE3" style="display: none">你当前的位置：</span>请输入分段检索的条件（最多分10段）
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td width="54%">
                                                <table border="0" align="right" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td width="60">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
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
                                                起点
                                            </td>
                                            <td class="Caption">
                                                终点
                                            </td>
                                            <td class="Caption">
                                                深度
                                            </td>
                                            <td class="Caption">
                                                线路ID
                                            </td>
                                            <td width="15%" class="Caption">
                                                操作
                                            </td>
                                        </tr>
                                        <asp:Repeater ID="repeater1" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="Data">
                                                        <%# Container.DataItem %>
                                                    </td>
                                                    <td class="Data">
                                                        <jasp:JTextBox ID="astation" runat="server"  ToolTip ="只读控件"></jasp:JTextBox>
                                                    </td>
                                                    <td class="Data">
                                                        <jasp:JTextBox ID="bstation" runat="server"></jasp:JTextBox>
                                                    </td>
                                                    <td class="Data">
                                                        <asp:DropDownList ID="sd" runat="server">
                                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                            <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                            <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                            <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                            <asp:ListItem Text="35" Value="35"></asp:ListItem>
                                                            <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                                            <asp:ListItem Text="45" Value="45"></asp:ListItem>
                                                            <asp:ListItem Text="50" Value="50" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="60" Value="60"></asp:ListItem>
                                                            <asp:ListItem Text="70" Value="70"></asp:ListItem>
                                                            <asp:ListItem Text="80" Value="80"></asp:ListItem>
                                                            <asp:ListItem Text="90" Value="90"></asp:ListItem>
                                                            <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="Data">
                                                        <jasp:JTextBox ID="lineid" runat="server"></jasp:JTextBox>
                                                    </td>
                                                    <td class="Data">
                                                        <jasp:JButton ID="button1" runat="server" Text="选择线路"></jasp:JButton>
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
            <br />
            <div style="width: 100%; text-align: center">
                <asp:Button ID="buttonSearch" runat="server" 
                    Text="确定返回"  />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
