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
        WebFrame.Data.JTable tab1 = new WebFrame.Data.JTable("CHEXIANWEIGHTPROFILE");
        System.Collections.Generic.List<WebFrame.Data.SearchField> condition =
            new System.Collections.Generic.List<WebFrame.Data.SearchField>();
        System.Collections.Generic.Dictionary<String, object> data1 =
            new System.Collections.Generic.Dictionary<string, object>();
        int i = 0;
        String[] arr1 = "weight,unitcost,unitfixcost,unitxhcost".Split(',');
        foreach (RepeaterItem item in this.Repeater1.Items)
        {
            condition.Clear();
            condition.Add(new WebFrame.Data.SearchField("CHEXIANTYPE", i + "", WebFrame.SearchFieldType.NumericType));
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
        WebFrame.Util.JAjax.AlertAndGoUrl("提示：更新数据操作成功！", "CheXianWeightProfile.aspx");
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
                                                    <span class="STYLE3">你当前的位置</span>：系统配置-客车厢重量和成本
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
                            <jasp:JDataSource ID="Data1" runat="server" PageSize="-1" JType="Table" SqlID="CHEXIANWEIGHTPROFILE"
                                OrderBy="CHEXIANTYPE">
                            </jasp:JDataSource>
                            <table class="ListTable" cellspacing="1" cellpadding="0" style="width: 90%; margin-left: 15px;
                                margin-top: 10px">
                                <tr>
                                    <td class="Caption">
                                        车厢类型
                                    </td>
                                    <td class="Caption">
                                        重量（吨）/每节
                                    </td>
                                    <td class="Caption">
                                        日常检修成本（元）
                                    </td>
                                    <td class="Caption">
                                        定期检修成本（元）
                                    </td>
                                    <td class="Caption">
                                        客运消耗、备用备品（元）
                                    </td>
                                </tr>
                                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Data1" >
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <%#Eval("CHEXIANNAME")%>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="weight" runat="server" Text='<%#Eval("weight")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="unitcost" runat="server" Text='<%#Eval("unitcost")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="unitfixcost" runat="server" Text='<%#Eval("unitfixcost")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="unitxhcost" runat="server" Text='<%#Eval("unitxhcost")%>'></jasp:JTextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <div class="ButtonArea" style="width: 100%">
                                <asp:UpdatePanel ID="update1" runat="server">
                                    <ContentTemplate>
                                        <jasp:JButton ID="button1" runat="server" AuthorityID="001" Text="提 交">
                                        </jasp:JButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <hr />
                                <div style="text-align: left">
                                    1）客车根据（硬座、软座、硬卧、软卧）的车辆数*单位维护成本*车底组数计算；<br />
                                    2）电车和餐车的维护成本单独计算（同上）；<br />
                                    3）用车底数（用户输入）*（1+2的成本和）为车辆的日常维护成本；<br />
                                </div>
                                <hr />
                                <div style="text-align: left">
                                    1）客车根据（硬座、软座、硬卧、软卧）的车厢数*单位定检成本计算*运行里程*2*365*标准
                                    <br />
                                    2）发电车和餐车的定检成本（运行里程*2*365*标准）<br />
                                    3）1+2的成本和
                                </div>
                                <hr />
                                <div style="text-align: left">
                                    1）客车根据（硬座、软座、硬卧、软卧）的车厢数*单位消耗备用备品成本；<br />
                                    2）电车的单位消耗备用备品成本；<br />
                                    3）用车底数（用户输入）*（1+2的成本和）<br />
                                </div>
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
