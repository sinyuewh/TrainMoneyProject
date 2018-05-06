using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessRule;
using WebFrame.Designer;
using WebFrame.Data;
using WebFrame.Util;
using WebFrame;

namespace WebSite.TrainWeb.SystemProfile
{
    public partial class Fee17_JianJieFee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.Fee1.Text = JStrInfoBU.GetStrTextByID("间接费用分摊");
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.button1.Click += new EventHandler(button1_Click);
            base.OnInit(e);
        }

        void button1_Click(object sender, EventArgs e)
        {
            JStrInfoBU bu1 = new JStrInfoBU();
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("StrID", "间接费用分摊"));
            Dictionary<String, object> data1 = new Dictionary<string, object>();
            data1["StrText"] = this.Fee1.Text;
            bu1.UpdateData(condition, data1);

            BusinessRule.TrainProfile.SetData();
            WebFrame.Util.JAjax.Alert("提示：更新数据操作成功！");

        }
    }
}
