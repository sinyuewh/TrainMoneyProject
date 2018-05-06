using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFrame.Util;
using BusinessRule;

namespace WebSite.TrainWeb.ParamSet
{
    public partial class ChangeStationName : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            this.button1.Click += new EventHandler(button1_Click);
            base.OnInit(e);
        }

        //成批更新站点的名称
        void button1_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txt1.Text.Trim())==false
                && String.IsNullOrEmpty(txt2.Text.Trim())==false)
            {
                NewTrainBU bu = new NewTrainBU();
                bool flag= bu.ChangeTrainStationName(this.txt1.Text.Trim(), this.txt2.Text.Trim());
                if (flag)
                {
                    JAjax.Alert("更改成功！");
                }
                else 
                {
                    JAjax.Alert("更改失败，请重试！");
                }
            }
            else
            {
                JAjax.Alert("错误：请输入站点的名称！");
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
