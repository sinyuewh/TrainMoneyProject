<%@ Page Title="" Language="C#" MasterPageFile="~/Designer/HtglMain.Master" AutoEventWireup="true"  %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="WebFrame.Designer" %>
<%@ Import Namespace="WebFrame.Util" %>
<%@ Import Namespace="WebFrame" %>
<%@ Import Namespace="System.Collections.Generic" %>
<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        this.repeater1.ItemCommand += new RepeaterCommandEventHandler(repeater1_ItemCommand);
        this.link1.Click += new EventHandler(link1_Click);
        this.but1.Click += new EventHandler(but1_Click);
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            JItemBU bu1 = new JItemBU();
            DataTable dt1 = bu1.GetDetailTableByItemName(Request.QueryString["ItemName"]);
            //dt1.Columns["num"].DataType = typeof(String);
            JBill.InitData(ViewState, new String[] { "num", "itemtext", "itemvalue" }, dt1);
            this.repeater1.DataSource = dt1;
            this.repeater1.DataBind();

            Frm_ModelBU.SetModelForListControl(this.modelid, "");
        }
    }

    protected override void OnPreRenderComplete(EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Data1.TotalRow == 0)
            {
                this.author.Text = WebFrame.Util.JCookie.GetCookieValue("HTGL_UserName");
                this.createtime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }

            if (this.itemname.Text != String.Empty)
            {
                this.itemname.ReadOnly = true;
                this.itemname.Enabled = false;
            }
        }
        base.OnPreRenderComplete(e);
    }


    #region 事件代码
    //更新数据
    void but1_Click(object sender, EventArgs e)
    {
        if (FrameLib.CheckData(this.but1))
        {
            String error = JBill.CheckData(ViewState,repeater1);
                        
            if (error == string.Empty)
            {
                JItemBU bu1 = new JItemBU();
                Control[] con1 = new Control[] { num, kind, itemname, author, 
                    createtime, modelid, minvalue, maxvalue, intervalue, remark };
                Dictionary<String,object> data1 =JControl.GetControlValuesToDictionary(con1);
                
                error = bu1.SaveItemData(this.itemname.Text, data1, 
                    JBill.GetData(ViewState,repeater1));
                if (error == String.Empty)
                {
                    this.but1.ExecutePara.Success = true;
                }
                FrameLib.ExecuteButtonInfo(this.but1);   
            }
            else
            {
                JAjax.Alert(error);
            }
        }
    }

    //增加Bill明细
    void link1_Click(object sender, EventArgs e)
    {
        JBill.NewData(ViewState,repeater1);
    }

    //删除Bill明细
    void repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int index = int.Parse(e.CommandArgument.ToString());
        JBill.DeleteData(ViewState, repeater1, index);
    }
    #endregion
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:UpdatePanel ID="update1" runat="server">
            <ContentTemplate>
                <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="JItem" OrderBy="num">
                    <ParaItems>
                        <jasp:ParameterItem ParaName="ItemName" ParaType="RequestQueryString" IsNullValue="true"  />
                    </ParaItems>
                </jasp:JDataSource>
                <table class="InfoTips">
                    <tr>
                        <td>
                            <img src="../images/ld1.gif" />
                            <b>数据字典</b>
                        </td>
                        <td style="text-align: right">
                            <a href="javascript:history.back()">
                                <img class="img" src="../images/cs_back.gif">返回</a>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="Spar">
                        </td>
                    </tr>
                </table>
                <jasp:JDetailView ID="DetailView1" runat="server" DataSourceID="Data1" ControlList="*">
                    <table cellpadding="0" cellspacing="1" class="DetailTable">
                        <tr>
                            <td class="Caption">
                                序号:
                            </td>
                            <td class="Data">
                                <jasp:JTextBox ID="num" runat="server" AllowNullValue="false" Caption="序号" MaxLength="10" />
                            </td>
                            <td class="Caption">
                                类别:
                            </td>
                            <td class="Data">
                                <jasp:JDropDownList ID="kind" runat="server">
                                    <asp:ListItem Text="用户条目" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="系统条目" Value="1"></asp:ListItem>
                                </jasp:JDropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="Caption">
                                条目标识:
                            </td>
                            <td class="Data">
                                <jasp:JTextBox ID="itemname" runat="server" AllowNullValue="false" Caption="条目标识"
                                    MaxLength="50" IsUnique="true" />
                            </td>
                            <td class="Caption">
                                作者:
                            </td>
                            <td class="Data">
                                <jasp:JTextBox ID="author" runat="server" Caption="作者" MaxLength="50" />
                            </td>
                        </tr>
                        <tr>
                            <td class="Caption">
                                创建时间:
                            </td>
                            <td class="Data">
                                <jasp:JTextBox ID="createtime" runat="server" Caption="创建时间" 
                                  DataType="DateTime" Format="yyyy-MM-dd" onfocus="calendar()" />
                            </td>
                            <td class="Caption">
                                所属模块:
                            </td>
                            <td class="Data">
                                <jasp:AppDropDownList ID="modelid" runat="server" Caption="隶属模块" >
                                </jasp:AppDropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="Caption">
                                最小值:
                            </td>
                            <td class="Data">
                                <jasp:JTextBox ID="minvalue" runat="server" Caption="最小值" DataType="Integer" />
                            </td>
                            <td class="Caption">
                                最大值:
                            </td>
                            <td class="Data">
                                <jasp:JTextBox ID="maxvalue" runat="server" Caption="最大值" DataType="Integer" />
                            </td>
                        </tr>
                        <tr>
                            <td class="Caption">
                                值间隔:
                            </td>
                            <td class="Data">
                                <jasp:JTextBox ID="intervalue" runat="server" Caption="值间隔" DataType="Integer" />
                            </td>
                            <td class="Caption">
                                备注:
                            </td>
                            <td class="Data">
                                <jasp:JTextBox ID="remark" runat="server" Caption="备注" MaxLength="200" />
                            </td>
                        </tr>
                    </table>
                </jasp:JDetailView>
                                               
                <table cellpadding="0" cellspacing="1" class="ListTable">
                    <tr>
                        <td class="Caption">
                            序号
                        </td>
                        <td class="Caption">
                            条目文本
                        </td>
                        <td class="Caption">
                            条目值
                        </td>
                        <td class="Caption" style="width:15%">
                            操作 <asp:LinkButton ID="link1" runat="server">增加</asp:LinkButton>
                        </td>
                    </tr>
                    <asp:Repeater ID="repeater1" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="Data">
                                    <jasp:JTextBox ID="num" Caption="序号" AllowNullValue="false" 
                                      DataType="Integer" runat="server" Text='<%#Eval("num").ToString()=="" ? "0": Eval("num").ToString() %>' />
                                </td>
                                <td class="Data">
                                    <jasp:JTextBox ID="itemtext" Caption="条目文本"
                                      MaxLength="50" AllowNullValue="false"
                                       runat="server" Text='<%#Eval("itemtext")%>' />
                                </td>
                                <td class="Data">
                                    <jasp:JTextBox ID="itemvalue" Caption="条目值"
                                     MaxLength="50" AllowNullValue="false" 
                                      runat="server" Text='<%#Eval("itemvalue")%>' />
                                </td>
                                 <td class="Data">
                                    <asp:LinkButton ID="link1" runat="server" CommandArgument='<%#Container.ItemIndex %>' OnClientClick="return confirm('提示：确定要删除吗？');">删除</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                
                <div class="ButtonArea">
                    <jasp:JButton ID="but1" runat="server" Text="更新" JButtonType="NoJButton" IsValidatorData="true">
                        <ParaItems>
                            <jasp:ParameterItem ParaName="ItemName" ParaType="RequestQueryString" />
                        </ParaItems>
                        <ExecutePara SuccInfo="提示：更新数据操作成功！" SuccUrl="-1" FailInfo="提示：更新数据操作失败！" />
                    </jasp:JButton>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
