using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSite.TrainWeb.Train
{
    public partial class NewTrainYunShunRs : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            this.linkMerge.Click += new EventHandler(linkMerge_Click);
            base.OnInit(e);
        }

        void linkMerge_Click(object sender, EventArgs e)
        {
            int count1 = BusinessRule.PubCode.Util.MergeData(4);
            this.Repeater1.DataBind();
            WebFrame.Util.JAjax.Alert("提示：成功的合并了" + count1 + "条数据！");
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
