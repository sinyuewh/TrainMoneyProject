<%@ Control Language="C#" AutoEventWireup="true" Inherits="WebFrame.ExpControl.PageNavigatorUrl" %>
<span style="text-align: center; width: 98%; text-decoration: none" id="pagerStuInfo"
    textbeforeinputbox="跳转到" textafterinputbox="页" showinputbox="Always"><span id="NavSpanForHaveData"
        runat="server" style="vertical-align: top">共有<b><%=this.TotalRow %></b>条记录,当前第<b><%=this.CurPage %></b>/<b><%=this.TotalPage %></b>页
        &nbsp;&nbsp;&nbsp;<asp:HyperLink ID="HypFirst" runat="server">首页</asp:HyperLink>
        |
        <asp:HyperLink ID="HypPrev" runat="server">上一页</asp:HyperLink>
        |
        <asp:HyperLink ID="HypNext" runat="server">下一页</asp:HyperLink>
        |
        <asp:HyperLink ID="HypLast" runat="server">尾页</asp:HyperLink>
        &nbsp;&nbsp;<jasp:JTextBox ID="txtgoPage" runat="server" Width="30" Style="text-align: center" />
        &nbsp;<asp:Button ID="butGoPage" runat="server" Text="Go" UseSubmitBehavior="false"
            CssClass="btnBack1" />
    </span><span id="NoSpanForNoData" runat="server">提示：没有满足条件的数据！ </span></span>
