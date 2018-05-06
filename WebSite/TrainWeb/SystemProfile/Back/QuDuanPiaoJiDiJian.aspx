<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true" %>

<%@ Import Namespace="System.Data" %>

<script runat="server">
    private double s1
    {
        get
        {
            if (ViewState["sum1"] == null)
            {
                return 0;
            }
            else
            {
                return (double)ViewState["sum1"];
            }
        }
        set
        {
            ViewState["sum1"] = value;
        }
    }
    protected override void OnInit(EventArgs e)
    {
        this.Repeater1.ItemDataBound += new RepeaterItemEventHandler(Repeater1_ItemDataBound);
        base.OnInit(e);
    }

    void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        double price0 = double.Parse(WebFrame.Designer.JStrInfoBU.GetStrTextByID("基本硬座费率"));
        DataRow dr = (e.Item.DataItem as DataRowView).Row;
        if (dr != null)
        {
            Label lab1 = e.Item.FindControl("lab1") as Label;
            if (lab1 != null)
            {
                double d1=Math.Round(price0*(1-double.Parse(dr["jianrate"].ToString())/100),6);
                lab1.Text = String.Format("{0:0.000000}",d1);

                Label lab2 = e.Item.FindControl("lab2") as Label;
                if (dr["pos2"].ToString().Trim() != String.Empty)
                {
                    double pos2=double.Parse(dr["pos2"].ToString());
                    double pos1=double.Parse(dr["pos1"].ToString());
                    double t1=Math.Round((pos2-pos1+1)*d1,4);
                    lab2.Text = String.Format("{0:0.0000}", t1);

                    s1 = s1 + t1;
                    if (s1 != t1)
                    {
                        Label lab3 = e.Item.FindControl("lab3") as Label;
                        lab3.Text = String.Format("{0:0.0000}", s1);
                    }
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
                                                    <span class="STYLE3">你当前的位置</span>：系统配置-区段票价递减表
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
                            <!--数据源定义-->
                            <jasp:JDataSource ID="Data1" runat="server" PageSize="-1" JType="Table" SqlID="LICHENGJIANRATE"
                                OrderBy="id">
                            </jasp:JDataSource>
                            <table class="ListTable" cellspacing="1" cellpadding="0" style="width: 90%; margin-left: 15px;
                                margin-top: 10px">
                                <tr>
                                    <td class="Caption">
                                        区段（千米）
                                    </td>
                                    <td class="Caption">
                                        递减率（%）
                                    </td>
                                    <td class="Caption">
                                        票价率[元/(人•千米)]
                                    </td>
                                    <td class="Caption">
                                        各区段全程票价(元)
                                    </td>
                                    <td class="Caption">
                                        区段累计票价(元)
                                    </td>
                                </tr>
                                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Data1" EnableViewState="false">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <%#Eval("pos1")%>～<%#Eval("pos2").ToString().Trim()!=String.Empty ? Eval("pos2") : "以上" %>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("jianrate")%>
                                            </td>
                                            <td class="Data">
                                                <asp:Label ID="lab1" runat="server"></asp:Label>
                                            </td>
                                            <td class="Data">
                                               <asp:Label ID="lab2" runat="server"></asp:Label>
                                            </td>
                                            <td class="Data">
                                                <asp:Label ID="lab3" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <div class="ButtonArea" style="width: 80%">
                            </div>
                            &nbsp;
                        </td>
                        <td width="8" background="/TrainWeb/images/tab_15.gif">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
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
