<%@ Page Title="列车收支明细" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master"
    AutoEventWireup="true" CodeBehind="NewTrainLiLunDetail.aspx.cs" Inherits="WebSite.TrainWeb.Train.NewTrainLiLunDetail" %>

<%@ Import Namespace="BusinessRule" %>

<script runat="server">
    
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
                                                    <span class="STYLE3" style="display: none">你当前的位置：</span>财务数据模型-列车收入明细
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
                            <jasp:JDataSource ID="Data1" runat="server" JType="Table" SqlID="NEWTRAINLILUNZHI"
                                OrderBy="num">
                                <ParaItems>
                                    <jasp:ParameterItem ParaName="num" ParaType="RequestQueryString" IsNullValue="true" />
                                </ParaItems>
                            </jasp:JDataSource>
                            <jasp:JDetailView ID="DetailView1" runat="server" DataSourceID="Data1" ControlList="*">
                                <table class="DetailTable" cellspacing="1" cellpadding="0" style="width: 95%; margin-left: 15px">
                                    <tr>
                                        <td class="Caption">
                                            序号
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="num" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            车次
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="trainname" runat="server"></jasp:JLabel>
                                            &nbsp;&nbsp;类型：<jasp:JLabel ID="traintype" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Caption">
                                            年份
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="byear" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            月份
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="bmonth" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            收入理论值(万)
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="shouru" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            收入实际值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="sshouru" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            运输旅客人次理论值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="pcount" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            运输旅客实际值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="spcount" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan ="4" class ="caption" style ="text-align:center">
                                            费 用 对 比
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Caption">
                                            线路使用费理论值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="Fee1" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            线路使用费实际值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="SFee1" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            机车牵引费理论值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="Fee2" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            机车牵引费实际值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="SFee2" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            电费和接触网使用费理论值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="Fee3" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            电费和接触网使用费实际值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="SFee3" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            售票服务费理论值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="Fee4" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            售票服务费实际值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="SFee4" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            旅客服务费理论值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="Fee5" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            旅客服务费实际值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="SFee5" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            列车上水费理论值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="Fee6" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            列车上水费实际值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="SFee6" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            人员和工资附加费理论值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="Fee7" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            人员和工资附加费实际值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="SFee7" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            车辆折旧费理论值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="Fee8" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            车辆折旧费实际值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="SFee8" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            日常检修成本费理论值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="Fee9" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            日常检修成本费实际值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="SFee9" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            定期检修成本理论值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="Fee10" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            定期检修成本实际值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="SFee10" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            车辆备用品消耗理论值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="Fee11" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            车辆备用品消耗实际值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="SFee11" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            空调车用油理论值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="Fee12" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            空调车用油实际值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="SFee12" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            人员其他费理论值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="Fee13" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            人员其他费实际值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="SFee13" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            车辆购置利息理论值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="Fee14" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            车辆购置利息实际值
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="SFee14" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="Caption">
                                            车辆轮渡费
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="Fee15" runat="server"></jasp:JLabel>
                                        </td>
                                        <td class="Caption">
                                            车辆轮渡费
                                        </td>
                                        <td class="Data">
                                            <jasp:JLabel ID="SFee15" runat="server"></jasp:JLabel>
                                        </td>
                                    </tr>
                                </table>
                            </jasp:JDetailView>
                            <div class="ButtonArea" style="width: 95%">
                                <asp:UpdatePanel ID="update1" runat="server">
                                    <ContentTemplate>
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
