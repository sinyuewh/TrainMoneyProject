using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFrame.Data;

namespace WebSite.TrainWeb.SystemProfile
{
    public partial class LineProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnPreRenderComplete(EventArgs e)
        {
            foreach (RepeaterItem item in this.Repeater1.Items)
            {
                for (int i = 1; i <= 6; i++)
                {
                    TextBox t1 = item.FindControl("Fee" + i) as TextBox;
                   // if (t1 != null && t1.Text == "") t1.Visible = false;
                }
            }
            base.OnPreRenderComplete(e);
        }

        protected override void OnInit(EventArgs e)
        {
            this.button1.Click += new EventHandler(button1_Click);
            base.OnInit(e);
        }

        void button1_Click(object sender, EventArgs e)
        {
            JTable tab1 = new JTable("LINEPROFILE");
            List<SearchField> condition =new List<SearchField>();
            Dictionary<String, object> data1 = new Dictionary<string, object>();
            int i = 1;
            String[] arr1 = "Fee1,Fee2,Fee3,Fee4,Fee5,Fee6".Split(',');
            foreach (RepeaterItem item in this.Repeater1.Items)
            {
                condition.Clear();
                condition.Add(new WebFrame.Data.SearchField("ID", i + "", WebFrame.SearchFieldType.NumericType));
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
                i++;
            }
            tab1.Close();
            BusinessRule.LineProfile.SetData();
            WebFrame.Util.JAjax.Alert("提示：更新数据操作成功！");
        }
    }
}
