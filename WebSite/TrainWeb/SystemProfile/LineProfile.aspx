<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true" %>

<%@ Import Namespace="System.Data" %>

<script runat="server">
    protected override void OnPreRenderComplete(EventArgs e)
    {
        foreach (RepeaterItem item in this.Repeater1.Items)
        {
            for (int i = 1; i <= 6; i++)
            {
                TextBox t1 = item.FindControl("Fee" + i) as TextBox;
                if (t1 != null && t1.Text == "") t1.Visible = false;
            }
        }
        base.OnPreRenderComplete(e);
    }

    protected override void OnInit(EventArgs e)
    {
        this.button1.Click += new EventHandler(button1_Click);
        base.OnInit(e);
    }

    void button1_Click(object sender, EventArgs e)
    {
        WebFrame.Data.JTable tab1 = new WebFrame.Data.JTable("LINEPROFILE");
        System.Collections.Generic.List<WebFrame.Data.SearchField> condition =
            new System.Collections.Generic.List<WebFrame.Data.SearchField>();
        System.Collections.Generic.Dictionary<String, object> data1 =
            new System.Collections.Generic.Dictionary<string, object>();
        int i = 1;
        String[] arr1 = "Fee1,Fee2,Fee3,Fee4,Fee5,Fee6".Split(',');
        foreach (RepeaterItem item in this.Repeater1.Items)
        {
            condition.Clear();
            condition.Add(new WebFrame.Data.SearchField("ID", i + "", WebFrame.SearchFieldType.NumericType));
            data1.Clear();
            foreach (String m in arr1)
            {
                TextBox t1 = item.FindControl(m) as TextBox;
                if (t1 != null)
                {
                    data1[m] = t1.Text;
                }
            }
            tab1.EditData(data1, condition);
            i++;
        }


        tab1.Close();
        WebFrame.Util.JAjax.AlertAndGoUrl("提示：更新数据操作成功！", "LineProfile.aspx");
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
                                                    <span class="STYLE3">你当前的位置</span>：系统配置-线路使用费
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
                            <jasp:JDataSource ID="Data1" runat="server" PageSize="-1" JType="Table" SqlID="LineProfile"
                                OrderBy="ID">
                            </jasp:JDataSource>
                            &nbsp; &nbsp;&nbsp;线路使用费标准参考：（元/列车公里）
                            <table class="ListTable" cellspacing="1" cellpadding="0" style="width: 90%; margin-left: 15px;
                                margin-top: 10px">
                                <tr>
                                    <td class="Caption">
                                        列车
                                    </td>
                                    <td class="Caption" colspan="2">
                                        运行时速大于300km/h高速动车组票价列车</td>
                                    <td class="Caption" colspan="2">
                                        其他高速动车组票价列车</td>
                                    <td class="Caption">
                                        空调列车</td>
                                    <td class="Caption">
                                        非空调列车</td>
                                </tr>
                                <tr>
                                    <td class="Caption">
                                        线路
                                    </td>
                                    <td class="Caption">
                                        单组
                                    </td>
                                    <td class="Caption">
                                        重联
                                    </td>
                                    <td class="Caption">
                                        单组
                                    </td>
                                    <td class="Caption">
                                        重联
                                    </td>
                                    <td class="Caption">
                                        &nbsp;
                                    </td>
                                    <td class="Caption">
                                        &nbsp;
                                    </td>
                                </tr>
                                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Data1" >
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <%#Eval("LineName")%>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Fee1" runat="server" Text='<%#Eval("Fee1")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Fee2" runat="server" Text='<%#Eval("Fee2")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Fee3" runat="server" Text='<%#Eval("Fee3")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Fee4" runat="server" Text='<%#Eval("Fee4")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Fee5" runat="server" Text='<%#Eval("Fee5")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Fee6" runat="server" Text='<%#Eval("Fee6")%>'></jasp:JTextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <div class="ButtonArea" style="width: 90%">
                                 <asp:UpdatePanel ID="update1" runat="server">
                                    <ContentTemplate>
                                        <jasp:JButton ID="button1" runat="server" AuthorityID="001" Text="提 交">
                                        </jasp:JButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
