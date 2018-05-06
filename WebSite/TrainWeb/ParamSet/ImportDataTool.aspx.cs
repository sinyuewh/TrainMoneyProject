using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using WebFrame.Data;

namespace WebSite.TrainWeb.ParamSet
{
    public partial class ImportDataTool : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            this.Button1.Click += new EventHandler(Button1_Click);
            base.OnInit(e);
        }

        //导入数据
        void Button1_Click(object sender, EventArgs e)
        {
            if (this.FileUpload1.HasFile)
            {
                String ext = Path.GetExtension(this.FileUpload1.FileName).ToLower();
                if (ext == ".xls")
                {
                    String NewFileName = WebFrame.Util.JString.GetUnique32ID() + ext;
                    this.FileUpload1.SaveAs(Server.MapPath("~/Attachment/" + NewFileName));
                    String kind = Request.QueryString["kind"];
                    bool succ = false;
                    try
                    {
                        if (kind == "0")      //将文件导入成 “担当车收入”
                        {
                            BusinessRule.PubCode.Util.ImportTrainShouRouDataToSystem(NewFileName,this.CheckBox1.Checked);
                            File.Delete(Server.MapPath("~/Attachment/" + NewFileName));
                            succ = true;

                        }
                        else if (kind == "1")   //线路使用费
                        {
                            BusinessRule.PubCode.Util.ImportTrainXianLuFeeToSystem(NewFileName, this.CheckBox1.Checked);
                            File.Delete(Server.MapPath("~/Attachment/" + NewFileName));
                            succ = true;
                        }
                        else if (kind == "2")   //机车牵引费
                        {
                            BusinessRule.PubCode.Util.ImportTrainQianYinFeeToSystem(NewFileName, this.CheckBox1.Checked);
                            File.Delete(Server.MapPath("~/Attachment/" + NewFileName));
                            succ = true;
                        }
                        else if (kind == "3")  //导入线路站点
                        {
                            BusinessRule.PubCode.Util.importTrainLineDataToSystem(NewFileName);
                            File.Delete(Server.MapPath("~/Attachment/" + NewFileName));
                            succ = true;
                        }
                        else if (kind == "4")  //导入电费和接触网使用费
                        {
                            BusinessRule.PubCode.Util.ImportTrainDianFeeToSystem(NewFileName,this.CheckBox1.Checked);
                            File.Delete(Server.MapPath("~/Attachment/" + NewFileName));
                            succ = true;
                        }
                        else if (kind == "5")  //导入运输人数
                        {
                            BusinessRule.PubCode.Util.ImportTrainPersonCountToSystem(NewFileName, this.CheckBox1.Checked);
                            File.Delete(Server.MapPath("~/Attachment/" + NewFileName));
                            succ = true;
                        }
                        else if (kind == "6")  //导入客专公司电费
                        {
                            BusinessRule.PubCode.Util.ImportGSCorpElecFeeToSystem(NewFileName);
                            File.Delete(Server.MapPath("~/Attachment/" + NewFileName));
                            succ = true;
                        }
                        else if (kind == "7")  //导入客运长交路机车牵引费
                        {
                            BusinessRule.PubCode.Util.ImportGTTrainDragFeeToSystem(NewFileName);
                            File.Delete(Server.MapPath("~/Attachment/" + NewFileName));
                            succ = true;
                        }
                    }
                    catch (Exception err) 
                    {
                        WebFrame.Util.JAjax.Alert("错误：请选择一个合适XLS的数据文件！");
                    }
                    if (succ)
                    {
                        WebFrame.Util.JAjax.Alert("提示：导入数据操作成功，请点【关闭退出】按钮关闭页面，并刷新即可显示数据！");
                    }
                }
                else
                {
                    WebFrame.Util.JAjax.Alert("错误：请选择一个合适XLS的数据文件版本（要求后缀必须是.xls）！");
                }
            }
            else
            {
                WebFrame.Util.JAjax.Alert("错误：请选择一个合适XLS的数据文件！");
            }
            //BusinessRule.PubCode.Util.ImportTrainShouRouDataToSystem("shourou.xls");
        }
    }
}
