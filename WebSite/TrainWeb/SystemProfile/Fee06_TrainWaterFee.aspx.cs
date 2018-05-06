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
    public partial class Fee06_TrainWaterFee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.Fee1.Text = JStrInfoBU.GetStrTextByID("普通列车上水站费用标准");
                this.Fee2.Text = JStrInfoBU.GetStrTextByID("动车上水站费用标准");
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
            condition.Add(new SearchField("StrID", "普通列车上水站费用标准"));
            Dictionary<String, object> data1 = new Dictionary<string, object>();
            data1["StrText"] = this.Fee1.Text;
            bu1.UpdateData(condition, data1);

            condition.Clear();
            condition.Add(new SearchField("StrID", "动车上水站费用标准"));
            data1["StrText"] = this.Fee2.Text;
            bu1.UpdateData(condition, data1);

            BusinessRule.TrainProfile.SetData();
            WebFrame.Util.JAjax.Alert("提示：更新数据操作成功！");

        }
    }
}
