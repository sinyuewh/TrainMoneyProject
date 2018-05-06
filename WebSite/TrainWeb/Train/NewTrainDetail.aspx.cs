using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFrame.Data;
using WebFrame.Util;
using WebFrame;
using BusinessRule;

namespace WebSite.TrainWeb.Train
{
    public partial class NewTrainDetail : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
           this.button1.Command += new CommandEventHandler(button1_Command);
            base.OnInit(e);
        }

        void button1_Command(object sender, CommandEventArgs e)
        {
            if (FrameLib.CheckData(button1))
            {
                button1.ExecutePara.Success = true;
                if (this.Line.Text.Trim() != String.Empty)
                {
                    //检查线路的配置是否正确
                    String traintype = this.TrainType.Text.Trim();
                    ETrainType type1 = ETrainType.空调车25T;

                    if (traintype == "动车组")
                    {
                        type1 = ETrainType.动车CRH2A;
                    }
                    else if (traintype == "空调特快")
                    {
                        type1 = ETrainType.空调车25T;
                    }
                    else if (traintype == "空调快速")
                    {
                        type1 = ETrainType.空调车25G;
                    }
                    else if (traintype == "空调普快")
                    {
                        type1 = ETrainType.空调车25G;
                    }
                    else if (traintype == "快速")
                    {
                        type1 = ETrainType.空调车25K;
                    }
                    else if (traintype == "普快")
                    {
                        type1 = ETrainType.绿皮车25B;
                    }

                    String[] lineNodes = this.Line.Text.Trim().Replace("-", ",").Split(',');
                    TrainLine lineObj = BusinessRule.Line.GetTrainLineByTrainTypeAndLineNoeds(type1, false, lineNodes);
                    if (lineObj != null)
                    {
                        if (lineObj.TotalMiles + "" != this.Mile.Text.Trim())
                        {
                            button1.ExecutePara.Success = false;
                            button1.ExecutePara.FailInfo = "错误：线路的距离设置错误，应为(" + lineObj.TotalMiles + "公里)！";
                        }
                    }
                    else
                    {
                        button1.ExecutePara.Success = false;
                        button1.ExecutePara.FailInfo = "错误：线路的配置不对或线路不存在！";
                    }
                }

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

       
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }


        protected override void OnPreRenderComplete(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (this.num.Text.Trim() == String.Empty)
                {
                    NewTrainBU bu1 = new NewTrainBU();
                    this.num.Text = bu1.GetNexNum() + "";
                }
            }
            base.OnPreRenderComplete(e);
        }
    }
}
