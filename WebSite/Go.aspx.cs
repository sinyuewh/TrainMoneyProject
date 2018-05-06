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

namespace OASystemWeb.SysMng
{
    public partial class Go : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void but1_Click(object sender, EventArgs e)
        {
            //处理系统的升级
            DBUpgrade.GoUpdate();

            //Response.Redirect("MainFrame.aspx", true);
            JAjax.Alert("升级已完成！");
        }
    }
}
