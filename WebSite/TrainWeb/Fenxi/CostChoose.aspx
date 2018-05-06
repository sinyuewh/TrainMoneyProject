<%@ Page Title="选择可变成本" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CostChoose.aspx.cs" Inherits="WebSite.TrainWeb.Fenxi.CostChoose" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <base target ="_self" />
    <script src="../JSLib/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../JSLib/dialog.js" type="text/javascript"></script>
    <script src="../JSLib/PubLib.js" type="text/javascript"></script>
 <script language="javascript" type="text/javascript">
     //设置提交后的代码处理
     function CheckCloseWindow() {
         if (document.getElementById("<%=winClose.ClientID %>").value == "1") {
             var v1 = document.getElementById("<%=selectDetialId.ClientID %>").value;
             window.returnValue = v1;
             window.close();
         }
     }

     function killerrors() {
         return true;
     }

     function CloseWindow() {
         window.close();
     }

     function returnCost(chk1) {
         if (chk1.checked) {
             var v1 = chk1.value;
             window.returnValue = v1;
             window.close();
         }
     }
     
     window.onerror = killerrors;
     window.onload = CheckCloseWindow;
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="winClose" runat="server" Value="0" />
    <asp:HiddenField ID="selectDetialId" runat="server" Value="" />
    <table class="ListTable" cellspacing="1" cellpadding="0" onmouseover="changeto()"
        onmouseout="changeback()" style="width:98%;">
        <asp:Repeater ID="Repeater1" runat="server">
            <HeaderTemplate>
                <tr>
                    <td class="Caption">
                            <asp:CheckBox ID="checkAll" runat="server" CssClass="selAllDocument"  ToolTip="全部选中或者全部不选中"
                            onclick="selectChildChecked('selDocument',this.checked);" Checked="true" />
                    </td>
                    <td class="Caption">
                        序号
                    </td>
                    <td class="Caption">
                        可变成本
                    </td>
                   
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td class="Data">
                         <asp:CheckBox ID="SelCost" runat="server" CssClass="selDocument" value='<%#Eval("NUM") %>' onclick="selectParentChecked('selAllDocument','SelCost');"  Checked="true"/>
                        <asp:TextBox ID="TextBox1" Text='<%#Eval("NUM")%>' runat="server" Visible="false"></asp:TextBox>
                        <asp:label ID="lblname" Text='<%#Eval("PAYNAME")%>' runat="server" Visible="false"></asp:label>
                    </td>
                    <td class="Data">
                        <%#Eval("NUM")%>                        
                    </td>
                    <td class="Data">
                        <%#Eval("PAYNAME")%>
                    </td>
                                 
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <div class="ButtonArea">
        <jasp:JButton ID="butSubmit" runat="server" Text="确 定" />
        &nbsp;
        <jasp:JButton ID="btnClose" runat="server" Text="关 闭" OnClientClick="javascript:window.close();return false;" />
        </div>
</asp:Content>
