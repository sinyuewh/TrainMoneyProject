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
    public partial class Shouru01_BaseDataList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.Fee1.Text = JStrInfoBU.GetStrTextByID("基本硬座费率");
                this.Fee2.Text = JStrInfoBU.GetStrTextByID("空调费率");
                this.Fee3.Text = JStrInfoBU.GetStrTextByID("保险费率");
                this.Fee4.Text = JStrInfoBU.GetStrTextByID("卧铺订票费");
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
            condition.Add(new SearchField("StrID", "基本硬座费率"));
            Dictionary<String, object> data1 = new Dictionary<string, object>();
            data1["StrText"] = this.Fee1.Text;
            bu1.UpdateData(condition, data1);

            condition.Clear();
            condition.Add(new SearchField("StrID", "空调费率"));
            data1["StrText"] = this.Fee2.Text;
            bu1.UpdateData(condition, data1);

            condition.Clear();
            condition.Add(new SearchField("StrID", "保险费率"));
            data1["StrText"] = this.Fee3.Text;
            bu1.UpdateData(condition, data1);

            condition.Clear();
            condition.Add(new SearchField("StrID", "卧铺订票费"));
            data1["StrText"] = this.Fee4.Text;
            bu1.UpdateData(condition, data1);
            TrainProfile.SetData();

            WebFrame.Util.JAjax.Alert("提示：更新数据操作成功！");
         

        }
    }
}
