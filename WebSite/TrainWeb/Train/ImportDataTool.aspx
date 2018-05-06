<%@ Page Title="将XLS文件的数据导入到系统" Language="C#" MasterPageFile="~/TrainWeb/TrainWeb.Master"
    AutoEventWireup="true" CodeBehind="ImportDataTool.aspx.cs" Inherits="WebSite.TrainWeb.Train.ImportDataTool" %>
<%@ Import Namespace="System.IO" %>

<script runat="server">
    
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language ="javascript" type ="text/javascript">
        function CheckData() {
            var flag = true;
            var file1 = document.getElementById('<%=FileUpload1.ClientID %>').value;
            if (file1 == "") {
                alert("错误：请选择你要导入的数据文件（格式为.xls）!");
                flag = false;
            }
            else{
                var kind1 = '<%=Request.QueryString["kind"] %>';
                var check1 = document.getElementById('<%=CheckBox1.ClientID %>').checked;
                if (kind1 != "3" && check1 == false) {
                    flag = confirm('警告：导入新数据，将自动删除掉同年月或同一线路的站点的相同数据，确定要导入新数据吗？');
                }
            }
            return flag;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding: 20 20 20 20; line-height: 150%">
        请选择要导入数据的XLS文件：<asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:CheckBox ID="CheckBox1" runat="server" Checked ="true"  /> 追加（合并）数据选项
        
        &nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text="导入数据" OnClientClick="javascript:return CheckData();" />
        <br />
        <hr size="1" />
        上传数据之前请仔细检查数据的格式，数据格式的不正确，可能导致数据导入失败(或部分失败)。
        <br />
        1) 担当车收入格式模板：<a href="/Attachment/shourou.xls" target="_blank" class="blue">shourou.xls</a><br />
        2) 线路使用费格式模板：<a href="/Attachment/xianlufee.xls" target="_blank" class="blue">xianlufee.xls</a><br />
        3) 机车牵引费格式模板：<a href="/Attachment/QianYinFee.xls" target="_blank" class="blue">QianYinFee.xls</a><br />
        4) 线路站点格式模板：<a href="/Attachment/kylc2.xls" target="_blank" class="blue">kylc2.xls</a><br />
        
        5) 电费和接触网使用费格式模板：<a href="/Attachment/DianFee.xls" target="_blank" class="blue">DianFee.xls</a><br />
        6) 列车运输人次的格式模板：<a href="/Attachment/PCount.xls" target="_blank" class="blue">PCount.xls</a><br />
        7) 客专公司电费的格式模板：<a href="/Attachment/ElecFee.xls" target="_blank" class="blue">ElecFee.xls</a><br />
        8) 客运长交路机车牵引费的格式模板：<a href="/Attachment/DragFee.xls" target="_blank" class="blue">DragFee.xls</a><br />
        <br />
        <div style="text-align:center">
            <asp:Button ID="Button2" runat="server" Text="关闭退出" OnClientClick="javascript:window.close();return false;" />
        </div>
    </div>
</asp:Content>
