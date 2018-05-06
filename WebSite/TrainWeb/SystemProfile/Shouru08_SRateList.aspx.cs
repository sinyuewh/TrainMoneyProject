using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSite.TrainWeb.SystemProfile
{
    public partial class Shouru08_SRateList : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            this.JButton1.Click += new EventHandler(JButton1_Click);
            this.JButton2.Click += new EventHandler(JButton2_Click);
            base.OnInit(e);
        }

        //成批更新税金数据
        void JButton2_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in this.Repeater1.Items)
            {
                int byear = int.Parse((item.FindControl("byear") as Label).Text);
                double srate= double.Parse((item.FindControl("Srate") as TextBox).Text);
                BusinessRule.SRateProfileBU.UpdateData(byear,srate);
            }
            WebFrame.Util.JAjax.Alert("提示：成批更新数据操作成功！");
        }

        //新增税金数据
        void JButton1_Click(object sender, EventArgs e)
        {
            int year1 = int.Parse(this.byear.Value);
            BusinessRule.SRateProfileBU.AddRate(year1);
            this.Repeater1.DataBind();
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
