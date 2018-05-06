<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true" %>

<%@ Import Namespace="System.Data" %>


<script runat="server">
    protected override void OnPreRenderComplete(EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            double price0 = double.Parse(WebFrame.Designer.JStrInfoBU.GetStrTextByID("基本硬座费率"));
            DataTable dt1 = Data1.GetListData();
            int i = 1;
            foreach (DataRow dr in dt1.Rows)
            {
                TextBox t1 = Rate1.Parent.FindControl("Rate" + i) as TextBox;
                if (t1 != null) t1.Text = dr["Rate"].ToString().Trim();

                Label lab1 = Price1.Parent.FindControl("Price" + i) as Label;
                if (lab1 != null)
                {
                    double v1 = Math.Round(price0 * double.Parse(dr["Rate"].ToString().Trim()) / 100, 5);
                    lab1.Text = String.Format("{0:0.00000}", v1);
                }

                TextBox t2 = Person1.Parent.FindControl("Person" + i) as TextBox;
                if (t2 != null) t2.Text = dr["PCount"].ToString().Trim();

                i++;
            }
        }
        base.OnPreRenderComplete(e);
    }

    protected override void OnInit(EventArgs e)
    {
        this.button1.Click += new EventHandler(button1_Click);
        base.OnInit(e);
    }

    //Submit Data
    void button1_Click(object sender, EventArgs e)
    {
        WebFrame.Data.JTable tab1 = new WebFrame.Data.JTable("CHEXIANBIANZHU");
        System.Collections.Generic.List<WebFrame.Data.SearchField> condition =
            new System.Collections.Generic.List<WebFrame.Data.SearchField>();
        System.Collections.Generic.Dictionary<String,object> data1=
            new System.Collections.Generic.Dictionary<string,object>();
        for (int i = 1; i <= 11; i++)
        {
            condition.Clear();
            condition.Add(new WebFrame.Data.SearchField("id",i+"",WebFrame.SearchFieldType.NumericType));
            data1.Clear();

            TextBox t1 = Rate1.Parent.FindControl("Rate" + i) as TextBox;
            if (t1 != null) 
            { 
                data1["Rate" ] = t1.Text;
            }

            
            TextBox t2 = Person1.Parent.FindControl("Person" + i) as TextBox;
            if (t2 != null)
            {
                data1["Person" ] = t2.Text;
            }
            tab1.EditData(data1, condition);
        }
        tab1.Close();
        WebFrame.Util.JAjax.AlertAndGoUrl("提示：更新数据操作成功！", "CommTrainCheXianProfile.aspx");
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
                                                    <span class="STYLE3">你当前的位置</span>：系统配置-普通列车车厢配置
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
                            <jasp:JDataSource ID="Data1" runat="server" PageSize="-1" JType="Table" SqlID="CHEXIANBIANZHU"
                                OrderBy="id">
                            </jasp:JDataSource>
                            <table class="ListTable" cellspacing="1" cellpadding="0" style="width: 90%; margin-left: 15px;
                                margin-top: 10px">
                                <tr>
                                    <td class="Caption" colspan="3">
                                        车厢类型
                                    </td>
                                    <td class="Caption">
                                        票价率(元/(人•千米))
                                    </td>
                                    <td class="Caption">
                                        比例（%）
                                    </td>
                                    <td class="Caption">
                                        满员人数
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data" colspan="3">
                                        硬座
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price1" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate1" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person1" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data" colspan="3">
                                        软座
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price2" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate2" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person2" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data" rowspan="5">
                                        硬卧票
                                    </td>
                                    <td class="Data" rowspan="3">
                                        开放式
                                    </td>
                                    <td class="Data">
                                        上铺
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price3" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate3" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person3" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data">
                                        中铺
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price4" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate4" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person4" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data">
                                        下铺
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price5" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate5" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person5" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data" rowspan="2">
                                        包房式
                                    </td>
                                    <td class="Data">
                                        上铺
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price6" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate6" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person6" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data">
                                        下铺
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price7" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate7" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person7" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data" rowspan="2">
                                        软卧票
                                    </td>
                                    <td class="Data" colspan="2">
                                        上铺
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price8" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate8" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person8" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data" colspan="2">
                                        下铺
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price9" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate9" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person9" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data" rowspan="2">
                                        高级软卧票
                                    </td>
                                    <td class="Data" colspan="2">
                                        上铺
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price10" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate10" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person10" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Data" colspan="2">
                                        下铺
                                    </td>
                                    <td class="Data">
                                        <jasp:JLabel ID="Price11" runat="server"></jasp:JLabel>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Rate11" runat="server"></jasp:JTextBox>
                                    </td>
                                    <td class="Data">
                                        <jasp:JTextBox ID="Person11" runat="server"></jasp:JTextBox>
                                    </td>
                                </tr>
                            </table>
                            <div class="ButtonArea" style="width: 100%">
                                <asp:UpdatePanel ID="update1" runat="server">
                                    <ContentTemplate>
                                        <jasp:JButton ID="button1" AuthorityID="001" runat="server" Text="提 交">
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
