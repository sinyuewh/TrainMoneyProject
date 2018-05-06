<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true"
    CodeBehind="OldCheCiFenxi.aspx.cs" Inherits="WebSite.TrainWeb.Fenxi.OldCheCiFenxi" %>

<%@ Import Namespace="BusinessRule" %>
<%@ Import Namespace="System.Collections.Generic" %>

<script runat="server">
    
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="/Common/JsLib/PubLib.js" type="text/javascript"></script>

    <style type="text/css">
        .left
        {
            padding-left: 5px;
            text-align: left;
        }
        .right
        {
            padding-right: 5px;
            text-align: right;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function ShowZhiChu(zc1) {
            var url1 = "ShowZhiChuMingXi.aspx?zc=" + zc1;
            myOpenURL(url1, 500, 400);
        }

        function HideWindow() {
            var win1 = top.frames["leftFrame"];
            if (win1 != null) {
                win1.document.getElementById("img1").src = "images/main_41_1.gif";
                win1.parent.frame.cols = "18,*";
                win1.document.getElementById("frmTitle").style.display = "none"
            }
        }

        function GoKindFenxi(kind1) {
            var y1 = document.getElementById("<%=this.byear.ClientID %>").value;
            var m1 = document.getElementById("<%=this.bmonth.ClientID %>").value;
            var url1 = "TrainFenXiByKind.aspx?kind=" +escape(kind1) + "&byear=" + y1 + "&bmonth=" + m1;
            location.href = url1;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin: 20 20 20 20; line-height: 250%">
        <table border="0">
            <tr>
                <td align="right">
                    �������£�
                </td>
                <td>
                    <asp:TextBox ID="byear" runat="server" Width="100" Text="2012"></asp:TextBox>
                    ��
                    <asp:TextBox ID="bmonth" runat="server" Width="105" Text="2"></asp:TextBox>
                    ��
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text="��ʼ����" OnClientClick="javascript:HideWindow();" />
                </td>
            </tr>
        </table>
        <div id="SearchInfo" runat="server">
            <table border="1" cellpadding="0" cellspacing="0" width="96%" style="margin-top: 10px">
                <tr>
                    <td align="center" rowspan="2" >
                        <b>���</b>
                    </td>
                    <td align="center" colspan="4">
                        <b>����ֵ</b>
                    </td>
                    <td align="center" colspan="10">
                        <b>ʵ��ֵ</b>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <b>�������� </b>
                    </td>
                    <td class="right">
                        <b>�ۼ�����</b>
                    </td>
                    <td class="right">
                        <b>��������</b>
                    </td>
                    <td class="right">
                        <b>�ۼ�����</b>
                    </td>
                    <td align="center">
                        <b>��������</b>
                    </td>
                    <td class="right">
                        <b>�ۼ�����</b>
                    </td>
                    <td class="right">
                        <b>��������</b>
                    </td>
                    <td class="right">
                        <b>�ۼ�����</b>
                    </td>
                    
                    <td class="right">
                        <b>����֧��</b>
                    </td>
                    <td class="right">
                        <b>�ۼ�֧��</b>
                    </td>
                    <td class="right">
                        <b>����ӯ��</b>
                    </td>
                    <td class="right">
                        <b>�ۼ�ӯ��</b>
                    </td>
                    
                    <td class="right">
                        <b>����������</b>
                    </td>
                    <td class="right">
                        <b>�ۼ�������</b>
                    </td>
                </tr>
                <asp:Repeater ID="repeater1" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <a class="blue" href="javascript:GoKindFenxi('<%#Container.DataItem.ToString() %>');">
                                    <%#Container.DataItem %></a>
                                <asp:Label ID="labtrainType" runat="server" Text="<%#Container.DataItem %>" Visible="false"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="labPerson" runat="server" Text="0"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="labPerson1" runat="server" Text="0"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="labShouRou" runat="server" Text="0"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="labShouRou1" runat="server" Text="0"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="labPerson2" runat="server" Text="0"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="labPerson3" runat="server" Text="0"></asp:Label>
                            </td>
                            
                            <td class="right">
                                <asp:Label ID="labShouRou2" runat="server" Text="0"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="labShouRou3" runat="server" Text="0"></asp:Label>
                            </td>
                            
                            <td class="right">
                                <asp:Label ID="zhichu1" runat="server" Text="0"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="zhichu2" runat="server" Text="0"></asp:Label>
                            </td>
                            
                            <td class="right">
                                <asp:Label ID="yk1" runat="server" Text="0"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="yk2" runat="server" Text="0"></asp:Label>
                            </td>
                            
                            <td class="right">
                                <asp:Label ID="labSzl" runat="server" Text="0"></asp:Label>%
                            </td>
                            <td class="right">
                                <asp:Label ID="labSzl2" runat="server" Text="0"></asp:Label>%
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <table border="0" width="96%">
                <tr>
                    <td>
                        ��ʾ�����ĵ�λΪ��Ԫ�����
                    </td>
                </tr>
            </table>
        </div>
        <br />
    </div>
</asp:Content>
