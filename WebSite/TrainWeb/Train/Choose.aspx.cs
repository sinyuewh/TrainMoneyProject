using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessRule;
using System.Data;
using WebFrame.Util;

namespace WebSite.TrainWeb.Train
{
    public partial class Choose : System.Web.UI.Page
    {
        protected override void  OnInit(EventArgs e)
        {
            //this.butSubmit.Click += new EventHandler(butSubmit_Click);
            base.OnInit(e);
        }

        /*
        void butSubmit_Click(object sender, EventArgs e)
        {
            int chooseNum = 0;
            string id = "";
            string name = "";
            foreach (RepeaterItem item in Repeater1.Items)
            {
                CheckBox cbox = item.FindControl("selDocument") as CheckBox;
                if (cbox != null && cbox.Checked)
                {
                    TextBox box = item.FindControl("TextBox1") as TextBox;
                    Label lbllinename = item.FindControl("lblname") as Label;
                    if (box != null)
                    {
                        id = box.Text.Trim();
                    }
                    name = lbllinename.Text.Trim();
                    chooseNum++;
                }
            }
            if (chooseNum == 0)
            {
                JAjax.Alert("请勾选要选择线别！");
            }
            else if (chooseNum > 1)
            {
                JAjax.Alert("只能选择一个线别！");
            }
            else
            {

                this.selectDetialId.Value = id + "&&" + name;
                this.winClose.Value = "1";
            }
        }
        */

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                bind();
            }
        }

        private void bind()
        {
            TrainShouRu1 bu=new TrainShouRu1 ();
            DataTable dt=bu.GetcjlList();
            this.Repeater1.DataSource = dt;
            this.Repeater1.DataBind();
        }
    }
}
