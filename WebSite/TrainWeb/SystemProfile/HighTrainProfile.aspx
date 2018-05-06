<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true" %>

<%@ Import Namespace="System.Data" %>

<script runat="server">
    
    protected override void OnPreRenderComplete(EventArgs e)
    {
        foreach (RepeaterItem item in this.Repeater1.Items)
        {
            for (int i = 1; i <= 4; i++)
            {
                TextBox t1 = item.FindControl("Rate" + i) as TextBox;
                if (t1 != null && t1.Text == "0") t1.Visible = false;

                TextBox t2 = item.FindControl("PCount" + i) as TextBox;
                if (t2 != null && t1.Text == "0") t2.Visible = false;
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
        WebFrame.Data.JTable tab1 = new WebFrame.Data.JTable("HIGHTRAINPROFILE");
        System.Collections.Generic.List<WebFrame.Data.SearchField> condition =
            new System.Collections.Generic.List<WebFrame.Data.SearchField>();
        System.Collections.Generic.Dictionary<String, object> data1 =
            new System.Collections.Generic.Dictionary<string, object>();
        int i = 1;
        String[] arr1 = "Rate1,PCount1,Rate2,PCount2,Rate3,PCount3,Rate4,PCount4".Split(',');
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
        WebFrame.Util.JAjax.AlertAndGoUrl("提示：更新数据操作成功！", "HighTrainProfile.aspx");
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
                                                    <span class="STYLE3">你当前的位置</span>：系统配置-动车票价和定员
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
                            <jasp:JDataSource ID="Data1" runat="server" PageSize="-1" JType="Table" SqlID="HighTrainProfile"
                                OrderBy="id">
                            </jasp:JDataSource>
                            <table class="ListTable" cellspacing="1" cellpadding="0" style="width: 90%; margin-left: 15px;
                                margin-top: 10px">
                                <tr>
                                    <td class="Caption">
                                        动车类型
                                    </td>
                                    <td class="Caption">
                                        动车类别
                                    </td>
                                    <td class="Caption">
                                        一等座基准票价
                                    </td>
                                    <td class="Caption">
                                        一等座定员
                                    </td>
                                    <td class="Caption">
                                        二等座基准票价
                                    </td>
                                    <td class="Caption">
                                        二等座定员
                                    </td>
                                    <td class="Caption">
                                        动卧基准票价
                                    </td>
                                    <td class="Caption">
                                        动卧定员
                                    </td>
                                    <td class="Caption">
                                        商务座基准票价
                                    </td>
                                    <td class="Caption">
                                        商务座定员
                                    </td>
                                </tr>
                                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="Data1">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="Data">
                                                <%#Eval("HIGHTRAINTYPE")%>
                                            </td>
                                            <td class="Data">
                                                <%#Eval("MILETYPE")%>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Rate1" runat="server" Text='<%#Eval("Rate1")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="PCount1" runat="server" Text='<%#Eval("PCount1")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Rate2" runat="server" Text='<%#Eval("Rate2")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="PCount2" runat="server" Text='<%#Eval("PCount2")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Rate3" runat="server" Text='<%#Eval("Rate3")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="PCount3" runat="server" Text='<%#Eval("PCount3")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="Rate4" runat="server" Text='<%#Eval("Rate4")%>'></jasp:JTextBox>
                                            </td>
                                            <td class="Data">
                                                <jasp:JTextBox ID="PCount4" runat="server" Text='<%#Eval("PCount4")%>'></jasp:JTextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <div class="ButtonArea" style="width: 90%">
                                <asp:UpdatePanel ID="update1" runat="server">
                                    <ContentTemplate>
                                        <jasp:JButton ID="button1" AuthorityID="001" runat="server" Text="提 交">
                                        </jasp:JButton>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div style="text-align: left">
                                <hr />
                                1) 200公里动车组（硬座）票价收入计算=一等座基准票价*运行里程*定员（100）+二等座票价*运行里程*二等座定员（510）（CRH2A）<br />
                                2) 200公里动车组（卧铺）票价收入计算=二等座基准票价*运行里程*二等座定员（55*2）+
                                动卧基准票价*运行里程*动卧定员（13*40）（CRH2E) <br />
                                3) 300公里动车组票价收入计算=一等座基准票价*运行里程*一等座定员（51）+ 二等座票价*运行里程*二等座定员（559）（CRH2C）<br />
                                4) 300公里动车组票价收入计算=一等座基准票价*运行里程*一等座定员（107）+ 二等座票价*运行里程*二等座定员（373）（CRH380A）<br />
                                5) 300公里动车组票价收入计算=一等座基准票价*运行里程*一等座定员（157）+
                                二等座票价*运行里程*二等座定员（838）+ 商务座票价*运行里程*特等座定员（22）（CRH380AL）
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
