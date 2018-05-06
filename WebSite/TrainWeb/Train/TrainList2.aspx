<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true" %>
<%@ Import  Namespace="System.Data" %>
<%@ Import Namespace="BusinessRule" %>
<%@ Register src="~/Include/PageNavigator1.ascx" tagname="PageNavigator1" tagprefix="uc1" %>
<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        this.Repeater1.ItemDataBound += new RepeaterItemEventHandler(Repeater1_ItemDataBound);
        base.OnInit(e);
    }

    void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRow dr1 = ((e.Item.DataItem as DataRowView) as DataRowView).Row;
        if (dr1 != null)
        {
            String trainname = dr1["trainName"].ToString();
            CommTrainBU bu1 = CommTrainBU.GetTrainObjectByTrainName(trainname);
            if (bu1 != null)
            {
                Label lab1 = e.Item.FindControl("lab0") as Label;
                if (lab1 != null) lab1.Text = String.Format("{0:C}",Math.Round(bu1.GetTotalShouru(), 2));

                Label lab2 = e.Item.FindControl("lab1") as Label;
                if (lab2 != null)
                {
                    double t1 = bu1.GetLineCost();
                    double t2 = bu1.GetQianYinCostOrDianFei();
                    double t3 = bu1.GetSaleCost();
                    double t4 = bu1.GetServiceCost();
                    double t5 = bu1.GetWaterCost();
                    double t6 = bu1.GetPersonCost();
                    double t7 = bu1.GetOftenFixCost();
                    double t8 = bu1.GetDingQiFixCost();
                    double t9 = bu1.GetXiaoHaoCost();
                    double t10 = bu1.GetOilCost();
                    double t11 = bu1.GetPersonOtherCost();
                    double t12 = bu1.GetLiXiCost();
                    lab2.Text = String.Format("{0:C}",Math.Round(t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12, 2));
                }
            }
        }
    }
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" src="/Common/JsLib/PubLib.js" type="text/javascript"></script>
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
                                                    <span class="STYLE3" style="display:none">你当前的位置：</span>财务数据模型-动车
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="54%">
                                       <table border="0" align="right" cellpadding="0" cellspacing="0">
                                            <tr>
                                                
                                                <td width="60">
                                                    <table width="90%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td class="STYLE1">
                                                                <div align="center">
                                                                    <img src="../images/22.gif" width="14" height="14" /></div>
                                                            </td>
                                                            <td class="STYLE1">
                                                                <div align="center">
                                                                    新增</div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                
                                                <td width="52">
                                                    <table width="88%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td class="STYLE1">
                                                                <div align="center">
                                                                    <img src="../images/11.gif" width="14" height="14" /></div>
                                                            </td>
                                                            <td class="STYLE1">
                                                                <div align="center">
                                                                    删除</div>
                                                            </td>
                                                        </tr>
                                                    </table>
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
                                    <td width="3%" class="Caption">
                                        <input type="checkbox" name="checkbox" value="checkbox" onclick="selectAllDocument('selDocumentID',this.checked);" />
                                    </td>
                                    <td width="3%" class="Caption">
                                        序号
                                    </td>
                                    <td class="Caption">
                                        列车名称
                                    </td>
                                    <td class="Caption">
                                        列车类型
                                    </td>
                                    <td class="Caption">
                                        运行路线
                                    </td>
                                    <td class="Caption">
                                        运行里程
                                    </td>
                                    <td class="Caption">
                                        总收入
                                    </td>
                                    <td class="Caption">
                                        总支出
                                    </td>
                                    <td class="Caption">
                                        基本操作
                                    </td>
                                </tr>
                                <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="Train" PageSize="20"
                                    OrderBy="num">
                                    <ParaItems>
                                        <jasp:ParameterItem ParaName="TRAINBIGKIND=1" ParaType="String" DataName="$$EMPTY" />
                                    </ParaItems>
                                </jasp:JDataSource>
                                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Data1" EnableViewState="false">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <input type="checkbox" name="selDocumentID" id="selDocumentID" value='<%#Eval("TrainName")%>' />
                                            </td>
                                            <td class="Data">
                                                <%#Eval("num")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("TrainName")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("TrainType")%>
                                            </td>
          
                                            <td class="Data">
                                                <%#Eval("TrainLine")%>
                                            </td>
                                            
                                            <td class="Data">
                                                <%#Eval("YUXINGLICHENG")%>
                                            </td>
                                            
                                            <td class="Data">
                                                <asp:Label ID="lab0" runat="server"></asp:Label>
                                            </td>
                                            
                                            <td class="Data">
                                                <asp:Label ID="lab1" runat="server"></asp:Label>
                                            </td>
                                            <td class="Data">
                                                <a href='TrainDetail2.aspx?TRAINNAME=<%#Eval("TRAINNAME")%>'><img src="../images/edt.gif" width="16" height="16" />明细</a> 
                                                &nbsp;<img src="../images/del.gif" width="16" height="16" />删除
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
                           <uc1:PageNavigator1 ID="PageNavigator11" runat="server" DataSourceID="Data1" />
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
