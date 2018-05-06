<%@ Control Language="C#" AutoEventWireup="true" Inherits="WebFrame.ExpControl.PageNavigatorUrl" %>
<span id="NavSpanForHaveData" runat="server" style="vertical-align: top">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td class="STYLE4">
                &nbsp;&nbsp;共有
                <%=this.TotalRow %>
                条记录，当前第
                <%=this.CurPage %>/<%=this.TotalPage %>
                页
            </td>
            <td>
                <table border="0" align="right" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="40">
                            <asp:HyperLink ID="HypFirst" runat="server">
                            <img src="/TrainWeb/images/first.gif" width="37" height="15" /></asp:HyperLink>
                        </td>
                        <td width="45">
                            <asp:HyperLink ID="HypPrev" runat="server">
                            <img src="/TrainWeb/images/back.gif" width="43" height="15" />
                            </asp:HyperLink>
                        </td>
                        <td width="45">
                            <asp:HyperLink ID="HypNext" runat="server">
                            <img src="/TrainWeb/images/next.gif" width="43" height="15" />
                            </asp:HyperLink>
                        </td>
                        <td width="40">
                            <asp:HyperLink ID="HypLast" runat="server">
                            <img src="/TrainWeb/images/last.gif" width="37" height="15" /></asp:HyperLink>
                        </td>
                        <td width="100">
                            <div align="center">
                                <span class="STYLE1">转到第
                                    <jasp:JTextBox ID="txtgoPage" runat="server" Width="15" Style="height: 18px; width: 20px;
                                        border: 1px solid #999999; text-align: center; vertical-align: middle" />
                                    页 </span>
                            </div>
                        </td>
                        <td width="40">
                            <asp:UpdatePanel ID="update1" runat="server">
                                <ContentTemplate>
                                    <asp:ImageButton ID="butGoPageImage" runat="server" ImageUrl="/TrainWeb/images/go.gif"
                                        Width="37" />
                                    <asp:Button ID="butGoPage" runat="server" UseSubmitBehavior="false" Visible="false">
                                    </asp:Button>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</span><span id="NoSpanForNoData" runat="server">提示：没有满足条件的数据！ </span>