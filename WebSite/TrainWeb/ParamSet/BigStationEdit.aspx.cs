using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessRule;
using WebFrame.Data;
using WebFrame;
using WebFrame.Util;

namespace WebSite.TrainWeb.ParamSet
{
    public partial class BigStationEdit : System.Web.UI.Page
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
                BigStationBU.SetBigList(this.num.Text, this.bigname.Text);
            }
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.bigname.Text = BigStationBU.GetBigList(this.num.Text);
            }
            base.OnPreRenderComplete(e);
        }
    }
}
