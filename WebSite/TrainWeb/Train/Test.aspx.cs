using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSite.TrainWeb.Train
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.but1.Visible = false;
            }
            base.OnPreRenderComplete(e);
        }
    }
}
