using System;
using System.Collections.Generic;

using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using BusinessRule;

namespace WebSite
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception err1 = Server.GetLastError();
            UTool.WriteErrorLog(err1);
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            //WebFrame.Data.JConnect.CloseConnect();
        }
    }
}