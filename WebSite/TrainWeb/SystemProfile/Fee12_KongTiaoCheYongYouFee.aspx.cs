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
    public partial class Fee11_KongTiaoCheYongYouFee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.Fee1.Text = JStrInfoBU.GetStrTextByID("油价标准");
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.button2.Click += new EventHandler(button2_Click);
            base.OnInit(e);
        }

        void button2_Click(object sender, EventArgs e)
        {
            JStrInfoBU bu1 = new JStrInfoBU();
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("StrID", "油价标准"));
            Dictionary<String, object> data1 = new Dictionary<string, object>();
            data1["StrText"] = this.Fee1.Text;
            bu1.UpdateData(condition, data1);

            UpdateOil();
            BusinessRule.TrainProfile.SetData();
            WebFrame.Util.JAjax.Alert("提示：更新数据操作成功！");
        }

        private void UpdateOil()
        {
            WebFrame.Data.JTable tab1 = new WebFrame.Data.JTable("COMMTRAINWEIGHTPROFILE");
            System.Collections.Generic.List<WebFrame.Data.SearchField> condition =
                new System.Collections.Generic.List<WebFrame.Data.SearchField>();
            System.Collections.Generic.Dictionary<String, object> data1 =
                new System.Collections.Generic.Dictionary<string, object>();
            int i = 2;
            String[] arr1 = "Oil".Split(',');
            foreach (RepeaterItem item in this.Repeater2.Items)
            {
                condition.Clear();
                condition.Add(new WebFrame.Data.SearchField("num", i + "", WebFrame.SearchFieldType.NumericType));
                data1.Clear();
                foreach (String m in arr1)
                {
                    TextBox t1 = item.FindControl(m) as TextBox;
                    if (t1 != null)
                    {
                        data1[m] = t1.Text;
                    }
                }
                tab1.EditData(data1, condition);
                i=i+1;
            }
            tab1.Close();
            BusinessRule.CheXianProfile.SetData();
            WebFrame.Util.JAjax.Alert("提示：更新数据操作成功！");
        }
    }
}
