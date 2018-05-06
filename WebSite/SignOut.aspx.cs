using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSite
{
    public partial class SignOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                System.Web.Security.FormsAuthentication.SignOut();
                //WebFrame.Util.JCookie.ClearAllCookie();
                Response.Redirect("Login.aspx", true);
            }
        }
    }
}
