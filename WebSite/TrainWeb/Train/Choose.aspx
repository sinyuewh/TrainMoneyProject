<%@ Page Title="选择指标名称" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Choose.aspx.cs" Inherits="WebSite.TrainWeb.Train.Choose" %>
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

     function returnLine(chk1) {
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
                        选择
                    </td>
                    <td class="Caption">
                        序号
                    </td>
                    <td class="Caption">
                        指标名称
                    </td>
                   
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td class="Data">
                        <input id="selLine" name ="selLine" type="radio" onclick="javascript:returnLine(this);" value ='<%#Eval("lineId")%>&&<%#Eval("LINENAME")%>' /> 
                        <asp:TextBox ID="TextBox1" Text='<%#Eval("lineId")%>' runat="server" Visible="false"></asp:TextBox>
                        <asp:label ID="lblname" Text='<%#Eval("LINENAME")%>' runat="server" Visible="false"></asp:label>
                    </td>
                    <td class="Data">
                        <%#Eval("LineID")%>                        
                    </td>
                    <td class="Data">
                        <%#Eval("LINENAME")%>
                    </td>
                                 
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <div class="ButtonArea" style="display:none">
        <jasp:JButton ID="butSubmit" runat="server" Text="提 交" />
        &nbsp;
        <jasp:JButton ID="btnClose" runat="server" Text="关 闭" OnClientClick="javascript:window.close();return false;" />
        </div>
</asp:Content>
