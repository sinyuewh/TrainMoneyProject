<%@ Page Title="" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master" AutoEventWireup="true" %>
<%@ Import Namespace="BusinessRule" %>
<script runat="server">
    protected override void OnPreRenderComplete(EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CommTrainBU bu1 = CommTrainBU.GetTrainObjectByTrainName(this.TrainName.Text);
            if (bu1 != null)
            {
                Labe0.Text =String.Format("{0:C}",bu1.GetTotalShouru());

                //计算费用
                double t1=bu1.GetLineCost();
                fee1.Text = String.Format("{0:C}",t1);
                double t2 = bu1.GetQianYinCostOrDianFei();
                fee2.Text = String.Format("{0:C}",t2 );

                double t3=bu1.GetSaleCost();
                fee3.Text = String.Format("{0:C}", t3);
                double t4 = bu1.GetServiceCost();
                fee4.Text = String.Format("{0:C}",t4 );

                double t5=bu1.GetWaterCost();
                fee5.Text = String.Format("{0:C}",t5 );
                double t6=bu1.GetPersonCost();
                fee6.Text = String.Format("{0:C}",t6 );

                double t7=bu1.GetOftenFixCost();
                fee7.Text = String.Format("{0:C}",t7 );
                double t8=bu1.GetDingQiFixCost();
                fee8.Text = String.Format("{0:C}", t8);

                double t9=bu1.GetXiaoHaoCost();
                fee9.Text = String.Format("{0:C}",t9);
                double t10=bu1.GetOilCost();
                fee10.Text = String.Format("{0:C}",t10 );

                double t11=bu1.GetPersonOtherCost();
                fee11.Text = String.Format("{0:C}",t11);
                double t12=bu1.GetLiXiCost();
                fee12.Text = String.Format("{0:C}",t12);
                fee0.Text = String.Format("{0:C}",t1+t2+t3+t4+t5+t6+t7+t8+t9+t10+t11+t12);
            }
        }
        base.OnPreRenderComplete(e);
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
                                                    <span class="STYLE3" style="display:none">你当前的位置：</span>财务数据模型-普通列车
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
                            <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="Train" OrderBy="num">
                                <ParaItems>
                                    <jasp:ParameterItem ParaName="TRAINNAME" ParaType="RequestQueryString" IsNullValue="true" />
                                </ParaItems>
                            </jasp:JDataSource>
                            <jasp:JDetailView ID="DetailView1" runat="server" DataSourceID="Data1" ControlList="*">
                                <table class="DetailTable" cellspacing="1" cellpadding="0" style="width: 80%; margin-left: 15px">
                                    <tr>
                                        <td class="Caption">
                                            序号
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="num" runat="server" AllowNullValue="false" Caption="序号" DataType="Integer"
                                                MaxLength="3"></jasp:JTextBox>
                                        </td>
                                        <td class="Caption">
                                            列车名称
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="TrainName" runat="server"></jasp:JTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Caption">
                                            列车类型
                                        </td>
                                        <td class="Data">
                                            <jasp:JDropDownList ID="TrainType" runat="server">
                                                <asp:ListItem Text="绿皮车25B" Value="绿皮车25B"></asp:ListItem>
                                                <asp:ListItem Text="空调车25K" Value="空调车25K"></asp:ListItem>
                                                <asp:ListItem Text="空调车25G" Value="空调车25G"></asp:ListItem>
                                                <asp:ListItem Text="空调车25T" Value="空调车25T"></asp:ListItem>
                                            </jasp:JDropDownList>
                                        </td>
                                        <td class="Caption">
                                            起点终点
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="TrainLine" runat="server"></jasp:JTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Caption">
                                            运行里程
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="YUXINGLICHENG" runat="server"></jasp:JTextBox>
                                        </td>
                                        <td class="Caption">
                                            运行时间
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="RUNHOUR" runat="server"></jasp:JTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Caption">
                                            车厢配置<br />
                                            合计数应为18
                                        </td>
                                        <td class="Data">
                                            <table width="90%">
                                                <tr>
                                                    <td>
                                                        车厢类型
                                                    </td>
                                                    <td>
                                                        节数
                                                    </td>
                                                    <td>
                                                        采购成本/节
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        硬座
                                                    </td>
                                                    <td>
                                                        <jasp:JTextBox ID="YinZuo" runat="server" Width="50"></jasp:JTextBox>
                                                    </td>
                                                    <td>
                                                        <jasp:JTextBox ID="YinZuoPrice" runat="server" Width="50"></jasp:JTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        软座
                                                    </td>
                                                    <td>
                                                        <jasp:JTextBox ID="RUANZUO" runat="server" Width="50"></jasp:JTextBox>
                                                    </td>
                                                    <td>
                                                        <jasp:JTextBox ID="RUANZUOPrice" runat="server" Width="50"></jasp:JTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        开放式硬卧
                                                    </td>
                                                    <td>
                                                        <jasp:JTextBox ID="OPENYINWO" runat="server" Width="50"></jasp:JTextBox>
                                                    </td>
                                                    <td>
                                                        <jasp:JTextBox ID="OPENYINWOPrice" runat="server" Width="50"></jasp:JTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        包房式硬卧
                                                    </td>
                                                    <td>
                                                        <jasp:JTextBox ID="CLOSEYINWO" runat="server" Width="50"></jasp:JTextBox>
                                                    </td>
                                                    <td>
                                                        <jasp:JTextBox ID="CLOSEYINWOPrice" runat="server" Width="50"></jasp:JTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        软卧
                                                    </td>
                                                    <td>
                                                        <jasp:JTextBox ID="RUANWO" runat="server" Width="50"></jasp:JTextBox>
                                                    </td>
                                                    <td>
                                                        <jasp:JTextBox ID="RUANWOPrice" runat="server" Width="50"></jasp:JTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        高级软卧
                                                    </td>
                                                    <td>
                                                        <jasp:JTextBox ID="ADVANCERUANWO" runat="server" Width="50"></jasp:JTextBox>
                                                    </td>
                                                    <td>
                                                        <jasp:JTextBox ID="ADVANCERUANWOPrice" runat="server" Width="50"></jasp:JTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        餐车
                                                    </td>
                                                    <td>
                                                        <jasp:JTextBox ID="CANCHE" runat="server" Width="50"></jasp:JTextBox>
                                                    </td>
                                                    <td>
                                                        <jasp:JTextBox ID="CANCHEPrice" runat="server" Width="50"></jasp:JTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        供电车
                                                    </td>
                                                    <td>
                                                        <jasp:JTextBox ID="FADIANCHE" runat="server" Width="50"></jasp:JTextBox>
                                                    </td>
                                                    <td>
                                                        <jasp:JTextBox ID="FADIANCHEPrice" runat="server" Width="50"></jasp:JTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        宿营车
                                                    </td>
                                                    <td>
                                                        <jasp:JTextBox ID="SHUYINCHE" runat="server" Width="50"></jasp:JTextBox>
                                                    </td>
                                                    <td>
                                                        <jasp:JTextBox ID="SHUYINCHEPrice" runat="server" Width="50"></jasp:JTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="Caption">
                                            线路配置<br />
                                            合计数应为总运行里程
                                        </td>
                                        <td class="Data">
                                            <table width="90%">
                                                <tr>
                                                    <td>
                                                        线路类型
                                                    </td>
                                                    <td>
                                                        公里
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        局内
                                                    </td>
                                                    <td>
                                                        <jasp:JTextBox ID="Line0" runat="server" Width="50"></jasp:JTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        特一类
                                                    </td>
                                                    <td>
                                                        <jasp:JTextBox ID="Line1" runat="server" Width="50"></jasp:JTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        特二类
                                                    </td>
                                                    <td>
                                                        <jasp:JTextBox ID="Line2" runat="server" Width="50"></jasp:JTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        一类上浮
                                                    </td>
                                                    <td>
                                                        <jasp:JTextBox ID="Line3" runat="server" Width="50"></jasp:JTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        一类
                                                    </td>
                                                    <td>
                                                        <jasp:JTextBox ID="Line4" runat="server" Width="50"></jasp:JTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        二类上浮
                                                    </td>
                                                    <td>
                                                        <jasp:JTextBox ID="Line5" runat="server" Width="50"></jasp:JTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        二类
                                                    </td>
                                                    <td>
                                                        <jasp:JTextBox ID="Line6" runat="server" Width="50"></jasp:JTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        三类
                                                    </td>
                                                    <td>
                                                        <jasp:JTextBox ID="Line7" runat="server" Width="50"></jasp:JTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        三类下浮
                                                    </td>
                                                    <td>
                                                        <jasp:JTextBox ID="Line8" runat="server" Width="50"></jasp:JTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Caption">
                                            列车加快
                                        </td>
                                        <td class="Data">
                                            <jasp:JDropDownList ID="JIAKUAI" runat="server">
                                                <asp:ListItem Text="无" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="加快" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="特快" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="特快附加" Value="3"></asp:ListItem>
                                            </jasp:JDropDownList>
                                        </td>
                                        <td class="Caption">
                                            空调费用
                                        </td>
                                        <td class="Data">
                                            <jasp:JDropDownList ID="KONGTIAOFEE" runat="server">
                                                <asp:ListItem Text="无" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="有" Value="1"></asp:ListItem>
                                            </jasp:JDropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Caption">
                                            席别增加费（%）
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="XIEBIEZHENGJIAFEE" runat="server"></jasp:JTextBox>
                                        </td>
                                        <td class="Caption">
                                            牵引类型
                                        </td>
                                        <td class="Data">
                                            <jasp:JDropDownList ID="QIANYINTYPE" runat="server">
                                                <asp:ListItem Text="内燃机车" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="电力机车" Value="1"></asp:ListItem>
                                            </jasp:JDropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Caption">
                                            供电类型
                                        </td>
                                        <td class="Data">
                                            <jasp:JDropDownList ID="GONGDIANTYPE" runat="server">
                                                <asp:ListItem Text="直供电" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="非直供电" Value="1"></asp:ListItem>
                                            </jasp:JDropDownList>
                                        </td>
                                        <td class="Caption">
                                            沿途水站数量
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="WATERCOUNT" runat="server"></jasp:JTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Caption">
                                            服务类型
                                        </td>
                                        <td class="Data">
                                            <jasp:JDropDownList ID="SERVERPERSON" runat="server">
                                                <asp:ListItem Text="一人1车" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="一人2车" Value="1"></asp:ListItem>
                                            </jasp:JDropDownList>
                                        </td>
                                        <td class="Caption">
                                            用车底数
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="YONGCHEDISHU" runat="server"></jasp:JTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Caption">
                                            车底数
                                        </td>
                                        <td class="Data">
                                            <jasp:JTextBox ID="CHEDISHU" runat="server"></jasp:JTextBox>
                                        </td>
                                        <td class="Caption">
                                            存增量模式
                                        </td>
                                        <td class="Data">
                                            <jasp:JDropDownList ID="CUNZENGMOSHI" runat="server">
                                                <asp:ListItem Text="有车有人" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="有车无人" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="新车新人" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="新车有人" Value="2"></asp:ListItem>
                                            </jasp:JDropDownList>
                                            <jasp:JTextBox ID="TRAINBIGKIND" runat="server" Visible="false" Text="0"></jasp:JTextBox>
                                            <jasp:JTextBox ID="HIGHTRAINBIANZHU" runat="server" Visible="false" Text="0"></jasp:JTextBox>
                                            <jasp:JTextBox ID="HIGHTRAINBIGKIND" runat="server" Visible="false" Text="0"></jasp:JTextBox>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            总收入
                                        </td>
                                        <td class="Data" colspan="3">
                                            <jasp:JLabel ID="Labe0" runat="server"  Font-Size="14px" Text="0" ForeColor="Red" />
                                        </td>
                                        
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            线路使用费
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="fee1" runat="server"  Font-Size="14px" Text="0" />
                                        </td>
                                        <td class="Caption">
                                            机车牵引费
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="fee2" runat="server"  Font-Size="14px" Text="0" />
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            售票服务费
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="fee3" runat="server"  Font-Size="14px" Text="0" />
                                        </td>
                                        <td class="Caption">
                                            旅客服务费
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="fee4" runat="server"  Font-Size="14px" Text="0" />
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            列车上水费
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="fee5" runat="server"  Font-Size="14px" Text="0" />
                                        </td>
                                        <td class="Caption">
                                            人员工资及工资附加费
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="fee6" runat="server"  Font-Size="14px" Text="0" />
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            车辆日常检修成本
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="fee7" runat="server"  Font-Size="14px" Text="0" />
                                        </td>
                                        <td class="Caption">
                                            车辆定期检修成本
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="fee8" runat="server"  Font-Size="14px" Text="0" />
                                        </td>
                                    </tr>
                                    
                                     <tr>
                                        <td class="Caption">
                                            客运消耗备用备品
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="fee9" runat="server"  Font-Size="14px" Text="0" />
                                        </td>
                                        <td class="Caption">
                                            空调车用油
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="fee10" runat="server"  Font-Size="14px" Text="0" />
                                        </td>
                                    </tr>
                                    
                                     <tr>
                                        <td class="Caption">
                                            人员其他费用
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="fee11" runat="server"  Font-Size="14px" Text="0" />
                                        </td>
                                        <td class="Caption">
                                            购买车辆利息
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="fee12" runat="server"  Font-Size="14px" Text="0" />
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            总费用
                                        </td>
                                        <td class="Data" colspan="3">
                                            <jasp:JLabel ID="fee0" runat="server"  Font-Size="14px" Text="0" ForeColor="Red" />
                                        </td>
                                        
                                    </tr>
                                </table>
                            </jasp:JDetailView>
                            <div class="ButtonArea" style="width: 80%">
                                <asp:UpdatePanel ID="update1" runat="server">
                                    <ContentTemplate>
                                        <jasp:JButton ID="button1" runat="server" Text="提 交" JButtonType="SimpleAction" IsValidatorData="true">
                                            <SimpleActionPara TableName="Train" ButtonType="UpdateData" AppendFlag="true" />
                                            <ParaItems>
                                                <jasp:ParameterItem ParaName="TRAINNAME" ParaType="RequestQueryString" />
                                            </ParaItems>
                                            <ExecutePara SuccInfo="提示：提交数据操作成功！" SuccUrl="-1" FailInfo="提示：提交数据操作失败！" />
                                        </jasp:JButton>
                                        &nbsp;
                                        <jasp:JButton ID="JButton1" runat="server" Text="返 回" JButtonType="RedirectButton">
                                            <ExecutePara SuccUrl="-1" />
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
