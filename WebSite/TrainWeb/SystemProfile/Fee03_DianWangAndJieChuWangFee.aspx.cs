using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFrame.Data;

namespace WebSite.TrainWeb.SystemProfile
{
    public partial class Fee03_DianWangAndJieChuWangFee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            this.button1.Click += new EventHandler(button1_Click);
            this.JButton2.Click += new EventHandler(JButton2_Click);
            base.OnInit(e);
        }

        void JButton2_Click(object sender, EventArgs e)
        {
            WebFrame.Data.JTable tab1 = new WebFrame.Data.JTable("HIGHTRAINPROFILE");
            System.Collections.Generic.List<WebFrame.Data.SearchField> condition =
                new System.Collections.Generic.List<WebFrame.Data.SearchField>();
            System.Collections.Generic.Dictionary<String, object> data1 =
                new System.Collections.Generic.Dictionary<string, object>();
            int i = 0;
            String[] arr1 = "Weight".Split(',');
            foreach (RepeaterItem item in this.Repeater2.Items)
            {
                condition.Clear();
                condition.Add(new WebFrame.Data.SearchField("id", (i+1) + "", WebFrame.SearchFieldType.NumericType));
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
            BusinessRule.HighTrainProfile.SetData();
            WebFrame.Util.JAjax.Alert("提示：更新数据操作成功！");
        
        }

        void button1_Click(object sender, EventArgs e)
        {
            WebFrame.Data.JTable tab1 = new WebFrame.Data.JTable("TRAINLINEKINDPROFILE");
            System.Collections.Generic.List<WebFrame.Data.SearchField> condition =
                new System.Collections.Generic.List<WebFrame.Data.SearchField>();
            System.Collections.Generic.Dictionary<String, object> data1 =
                new System.Collections.Generic.Dictionary<string, object>();
            int i = 0;
            String[] arr1 = "JieChuFee,DianFee".Split(',');
            foreach (RepeaterItem item in this.Repeater1.Items)
            {
                condition.Clear();
                condition.Add(new WebFrame.Data.SearchField("LineID", i + "", WebFrame.SearchFieldType.NumericType));
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
            BusinessRule.TrainLineKindProfile.SetData();
            WebFrame.Util.JAjax.Alert("提示：更新数据操作成功！");
        }
    }
}
