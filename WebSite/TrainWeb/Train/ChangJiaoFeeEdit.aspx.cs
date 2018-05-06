using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessRule;
using WebFrame.Data;
using WebFrame;
using WebFrame.Util;

namespace WebSite.TrainWeb.Train
{
    public partial class ChangJiaoFeeEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            this.button1.Click += new EventHandler(button1_Click);
            base.OnInit(e);
        }

        void button1_Click(object sender, EventArgs e)
        {
            if (FrameLib.CheckData(this.button1))
            {
                Dictionary<String, object> data1 = new Dictionary<string, object>();

                data1["num"]=this.num.Text.Trim();
                data1["lineid"]=Request.QueryString["LineID"];
                data1["linename"]=this.LineName.Text.Trim();
                data1["jiaolu"]=this.JiaoLu.Text;

                if (this.fee1.Text.Trim() != String.Empty)
                {
                    data1["fee1"] = int.Parse(this.fee1.Text.Trim());
                }
                else
                {
                    data1["fee1"] = 0;
                }

                if (this.fee2.Text.Trim() != String.Empty)
                {
                    data1["fee2"] = int.Parse(this.fee2.Text.Trim());
                }
                else
                {
                    data1["fee2"] = 0;
                }

               
                ChangJiaoFeeBU bu1 = new ChangJiaoFeeBU();
                bool succ= bu1.UpdateData(data1);
                	
                this.button1.ExecutePara.Success = succ;
                FrameLib.ExecuteButtonInfo(this.button1);
            }
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (this.num.Text.Trim() == String.Empty)
                {
                    this.num.Text = ChangJiaoFeeBU.GetNextNum()+"";
                }

                if (this.fee1.Text.Trim() == "0")
                {
                    this.fee1.Text="";
                }

                if (this.fee2.Text.Trim() == "0")
                {
                    this.fee2.Text = "";
                }
               
            }
            base.OnPreRenderComplete(e);
        }
    }
}
