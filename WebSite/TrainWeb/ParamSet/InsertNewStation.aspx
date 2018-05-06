<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="InsertNewStation.aspx.cs" Inherits="WebSite.TrainWeb.ParamSet.InsertNewStation" %>
<%@ Import Namespace="BusinessRule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="/TrainWeb/JSLib/jquery-1.4.2.min.js" type="text/javascript"></script>
   <script type="text/javascript">
       $(function() {
           $(".trbegin").hide();
           $(".trlast").hide();
           if ($("#<%=txt1.ClientID %>").val().trim() != "") {
               $(".trbegin").show();
               $(".trlast").show();
               $("#<%=lblbegin.ClientID %>").text($("#<%=txt1.ClientID %>").val());
               $("#<%=lblmid.ClientID %>").text($("#<%=newstation.ClientID %>").val());
               $("#<%=lblmiddle.ClientID %>").text($("#<%=newstation.ClientID %>").val());
               $("#<%=ldllast.ClientID %>").text($("#<%=txt2.ClientID %>").val());
           }
           $("#<%=txt1.ClientID %>").focus(function() {
               $(".trbegin").hide();
               $(".trlast").hide();
           });
           $("#<%=txt2.ClientID %>").focus(function() {
               $(".trbegin").hide();
               $(".trlast").hide();
           });
           $("#<%=txtbegin.ClientID %>").blur(function() {
               $(".trbegin").show();
               $(".trlast").show();
           });
           $("#<%=txtlast.ClientID %>").focus(function() {
               $(".trbegin").show();
               $(".trlast").show();
           });
           $("#<%=newstation.ClientID %>").blur(function() {

               if ($("#<%=txt1.ClientID %>").val().trim() == "") { alert("要插入站点路线不能为空"); $("#<%=txt1.ClientID %>").focus(); return false; }
               if ($("#<%=txt2.ClientID %>").val().trim() == "") { alert("要插入站点路线不能为空"); $("#<%=txt2.ClientID %>").focus(); return false; }
               if ($("#<%=newstation.ClientID %>").val().trim() == "") { alert("新站点名称不能为空"); $("#<%=newstation.ClientID %>").focus(); return false; }
               if ($("#<%=newstation.ClientID %>").val().trim() != "" && $("#<%=txt1.ClientID %>").val().trim() != "" && $("#<%=txt2.ClientID %>").val().trim() != "") {
                   $(".trbegin").show();
                   $(".trlast").show();

                   $("#<%=lblbegin.ClientID %>").text($("#<%=txt1.ClientID %>").val());
                   $("#<%=lblmid.ClientID %>").text($("#<%=newstation.ClientID %>").val());
                   $("#<%=lblmiddle.ClientID %>").text($("#<%=newstation.ClientID %>").val());
                   $("#<%=ldllast.ClientID %>").text($("#<%=txt2.ClientID %>").val());


               }
           });
           $("#setMiles").click(function() {

               if ($("#<%=txt1.ClientID %>").val().trim() == "") { alert("要插入站点路线不能为空"); $("#<%=txt1.ClientID %>").focus(); return false; }
               if ($("#<%=txt2.ClientID %>").val().trim() == "") { alert("要插入站点路线不能为空"); $("#<%=txt2.ClientID %>").focus(); return false; }
               if ($("#<%=newstation.ClientID %>").val().trim() == "") { alert("新站点名称不能为空"); $("#<%=newstation.ClientID %>").focus(); return false; }
               if ($("#<%=newstation.ClientID %>").val().trim() != "" && $("#<%=txt1.ClientID %>").val().trim() != "" && $("#<%=txt2.ClientID %>").val().trim() != "") {
                   $(".trbegin").show();
                   $(".trlast").show();
                   $("#<%=lblbegin.ClientID %>").text($("#<%=txt1.ClientID %>").val());
                   $("#<%=lblmid.ClientID %>").text($("#<%=newstation.ClientID %>").val());
                   $("#<%=lblmiddle.ClientID %>").text($("#<%=newstation.ClientID %>").val());
                   $("#<%=ldllast.ClientID %>").text($("#<%=txt2.ClientID %>").val());


               }
           });

       })
   </script>
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
                                                    <span class="STYLE3" style="display: none">你当前的位置：</span>财务数据模型-插入站点
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
                            <table class="DetailTable" cellspacing="1" cellpadding="0" style="width: 60%; margin-left: 15px">
                                <tr>
                                    <td class="Caption">
                                        要插入的站点路线:
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="txt1" runat="server" Width="100px"></jasp:JTextBox>&nbsp;&nbsp;～&nbsp;&nbsp;<jasp:JTextBox ID="txt2" runat="server" Width="100px"></jasp:JTextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <jasp:JButton ID="button1" runat="server" AuthorityID="001" Text="【查询相关线路】">
                                </jasp:JButton>
                                        <input type ="hidden" id="scount" value ="0" runat ="server" />
                                    </td>
                                </tr>
                                <tr>
                                   <td class="Caption">
                                        新站点名称:
                                    </td>
                                    <td class="Data">
                                        <asp:TextBox runat="server" ID="newstation"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input type="button" id="setMiles" value="设置距离"/>
                                    </td>
                                
                                </tr>
                                <tr class="trbegin">
                                    <td class="Caption">
                                        <asp:Label runat="server" ID="lblbegin"></asp:Label>&nbsp;至&nbsp;<asp:Label runat="server" ID="lblmid"></asp:Label>
                                    </td>
                                    <td class="Data">
                                     <asp:TextBox runat="server" ID="txtbegin" AutoPostBack="true" OnTextChanged="txtbegin_TextChanged"></asp:TextBox>&nbsp;&nbsp;公里
                                    </td>
                                </tr>
                                <tr class="trlast">
                                    <td class="Caption">
                                        <asp:Label runat="server" ID="lblmiddle"></asp:Label>&nbsp;至&nbsp;<asp:Label runat="server" ID="ldllast"></asp:Label>
                                    </td>
                                    <td class="Data">
                                     <asp:TextBox runat="server" ID="txtlast"></asp:TextBox>&nbsp;&nbsp;公里
                                    </td>
                                </tr>
                            </table>
                            <div class="ButtonArea" style="width: 80%">
                               
                                &nbsp;&nbsp;
                                <asp:Button runat="server" ID="btnSubmit" Text="提交" />
                               
                            </div>
                            <!--数据列表区-->
                            
                            <div id="info1" runat="server"  style="text-align: left">
                                &nbsp;&nbsp;&nbsp;&nbsp;<b>相关线路预览</b>
                                <!--数据列表区-->
                                <table class="ListTable" cellspacing="1" cellpadding="0" onmouseover="changeto()"
                                    onmouseout="changeback()" style="width: 80%; margin-left: 20px" align="left">
                                    <tr>
                                        <td class="Caption">
                                            序号
                                        </td>
                                        <td class="Caption">
                                            线路代码
                                        </td>
                                        <td class="Caption">
                                            线路名称
                                        </td>
                                        <td class="Caption">
                                            起点
                                        </td>
                                        <td class="Caption">
                                            讫点
                                        </td>
                                        <td class="Caption">
                                            长度(km)
                                        </td>
                                        <td class="Caption">
                                            类别
                                        </td>
                                        <td class="Caption">
                                            线路站点
                                        </td>
                                        <td class="Caption">
                                            电气化
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="Repeater1" runat="server" EnableViewState="false">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="Data">
                                                    <%#Eval("num")%>
                                                </td>
                                                <td class="Data">
                                                    <%#Eval("LineID")%>
                                                </td>
                                                <td class="Data">
                                                    <%#Eval("LineName")%>
                                                </td>
                                                <td class="Data">
                                                    <%#Eval("AStation")%>
                                                </td>
                                                <td class="Data">
                                                    <%#Eval("BStation")%>
                                                </td>
                                                <td class="Data">
                                                    <%#Eval("Miles")%>
                                                </td>
                                                <td class="Data">
                                                    <asp:Label ID="LineType" runat="server" Text='<%#(BusinessRule.ELineType)(int.Parse(Eval("LineType").ToString()))%>'></asp:Label>
                                                    <asp:Label ID="SpringWinter" runat="server" Visible="false" Text='<%#Eval("SpringWinter")%>'></asp:Label>
                                                </td>
                                                <td class="Data">
                                                    <asp:Label ID="lab1" runat="server" Text='<%#String.Format("[{0}]", BusinessRule.Line.GetLineStationCount(Eval("LineID").ToString()))%>'
                                                        ToolTip='<%# BusinessRule.Line.GetLineStationText(Eval("LineID").ToString())%>'></asp:Label>
                                                    <asp:HyperLink ID="hyp1" runat="server" NavigateUrl='<%#String.Format("EditStation.aspx?LineID={0}",Eval("LineID")) %>'
                                                        Text='[查看]' ForeColor="Blue"></asp:HyperLink>
                                                </td>
                                                <td class="Data">
                                                    <%#Eval("dqh") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                            
                            
                        </td>
                    </tr>
                </table>
                &nbsp;
            </td>
            <td width="8" background="/TrainWeb/images/tab_15.gif">
                &nbsp;
            </td>
        </tr>
    </table>
      </td> </tr>
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
