using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;
using WebFrame;
using WebFrame.Data;
using WebFrame.Util;
using WebFrame.ExpControl;
using BusinessRule;

namespace WebSite
{
    public partial class KIP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.KIPs.Text = KIPBU.GetIPs();
            }

            base.OnPreRenderComplete(e);
        }

        protected void but1_Click(object sender, EventArgs e)
        {
            //处理系统的升级
            if (this.Password.Text == "kin90rient")
            {
                bool succ = KIPBU.SetIPs(this.KIPs.Text.ToString().Trim());
                if (succ)
                {
                    JAjax.Alert("设置完成！");
                }
                else
                {
                    JAjax.Alert("设置失败！");
                }
            }
        }
    }
}
