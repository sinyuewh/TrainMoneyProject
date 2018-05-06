<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true" %>

<%@ Import Namespace="System.Data" %>

<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        this.button1.Click += new EventHandler(button1_Click);
        base.OnInit(e);
    }

    void button1_Click(object sender, EventArgs e)
    {
        WebFrame.Data.JTable tab1 = new WebFrame.Data.JTable("QIANYINFEEPROFILE");
        System.Collections.Generic.List<WebFrame.Data.SearchField> condition =
            new System.Collections.Generic.List<WebFrame.Data.SearchField>();
        System.Collections.Generic.Dictionary<String, object> data1 =
            new System.Collections.Generic.Dictionary<string, object>();
        int i = 0;
        String[] arr1 = "Fee1,Fee2".Split(',');
        foreach (RepeaterItem item in this.Repeater1.Items)
        {
            condition.Clear();
            condition.Add(new WebFrame.Data.SearchField("QIANYINTYPE", i + "", WebFrame.SearchFieldType.NumericType));
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
        WebFrame.Util.JAjax.AlertAndGoUrl("提示：更新数据操作成功！", "QianYinFeeProfile.aspx");
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
                                                    <span class="STYLE3">你当前的位置</span>：系统配置-机车牵引费
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
                            <jasp:JDataSource ID="Data1" runat="server" PageSize="-1" JType="Table" SqlID="QianYinFeeProfile"
                                OrderBy="QIANYINTYPE">
                            </jasp:JDataSource>
                            <table class="ListTable" cellspacing="1" cellpadding="0" style="width: 50%; margin-left: 15px;
                                margin-top: 10px">
                                <tr>
                                    <td class="Caption">
                                        牵引类型
                                    </td>
                                    <td class="Caption">
                                        直供电客车
                                    </td>
                                    <td class="Caption">
                                        非直供电客车
                                    </td>
                                </tr>
                                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Data1" >
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <%#Eval("QIANYINNAME")%>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Fee1" runat="server" Text='<%#Eval("Fee1")%>'></jasp:JTextBox>（元/吨）
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Fee2" runat="server" Text='<%#Eval("Fee2")%>'></jasp:JTextBox>（元/吨）
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <div class="ButtonArea" style="width: 50%">
                                <asp:UpdatePanel ID="update1" runat="server">
                                    <ContentTemplate>
                                        <jasp:JButton ID="button1" AuthorityID="001" runat="server" Text="提 交">
                                        </jasp:JButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <hr />
                            <div style="text-align: left">
                                1））牵引费按上面的标准*列车的重量*运用的里程*2*365；<br />
                            </div> &nbsp;
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
