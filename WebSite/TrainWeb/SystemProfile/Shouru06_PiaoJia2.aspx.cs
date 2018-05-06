using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSite.TrainWeb.SystemProfile
{
    public partial class Shouru06_PiaoJia2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            this.JButton2.Click += new EventHandler(JButton2_Click);
            base.OnInit(e);
        }

        void JButton2_Click(object sender, EventArgs e)
        {
            String[] arr1 = "YZPRICE,RZPRICE,PTJKPRICE,KSJKPRICE,YWSPRICE,YWZPRICE,YWXPRICE,RWSPRICE,RWXPRICE,KDPRICE".Split(',');
            Dictionary<string, object> data1 = new Dictionary<string, object>();
            foreach (RepeaterItem item in this.Repeater1.Items)
            {
                int StartMile = int.Parse((item.FindControl("StartMile") as Label).Text);
                int EndMile = int.Parse((item.FindControl("EndMile") as Label).Text);
                data1.Clear();
                foreach (String m in arr1)
                {
                    data1[m] = double.Parse((item.FindControl(m) as TextBox).Text);
                }
                BusinessRule.TicketPrice.UpdatePriceData(2, StartMile, EndMile, data1);
            }
            WebFrame.Util.JAjax.Alert("提示：成批更新数据操作成功！");
        }
    }
}
