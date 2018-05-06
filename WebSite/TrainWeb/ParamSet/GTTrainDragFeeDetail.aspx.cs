using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebFrame.Data;
using WebFrame.Util;
using WebFrame;
using BusinessRule;

namespace WebSite.TrainWeb.ParamSet
{
    public partial class GTTrainDragFeeDetail : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            this.button1.Command += new CommandEventHandler(button1_Command);
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        void button1_Command(object sender, CommandEventArgs e)
        {
            if (FrameLib.CheckData(button1))
            {
                button1.ExecutePara.Success = true;

                if (button1.ExecutePara.Success)
                {
                    button1.JButtonType = JButtonType.SimpleAction;
                    FrameLib.ExecuteJButton(button1);
                }
                else
                {
                    FrameLib.ExecuteButtonInfo(button1);
                }
            }
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
            base.OnPreRenderComplete(e);
        }
    }
}
