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
    public partial class Fee08_CheLiangZheJiuFee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.Fee1.Text = JStrInfoBU.GetStrTextByID("普列折旧费率");
                this.Fee2.Text = JStrInfoBU.GetStrTextByID("动列折旧费率");
            }
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            String[] arr1 = "YzPrice,YwPrice,SYzPrice,SRzPrice,RzPrice,RwPrice,GRw19KPrice,GRw19TPrice,CaPrice,KdPrice".Split(',');
            foreach (RepeaterItem item in this.Repeater2.Items)
            {
                foreach (String m in arr1)
                {
                    TextBox t1 = item.FindControl(m) as TextBox;
                    if (t1 != null)
                    {
                        if (t1.Text.Trim() != String.Empty)
                        {
                            if (double.Parse(t1.Text.Trim()) == 0)
                            {
                                t1.Text = String.Empty;
                            }
                        }
                    }
                }
            }

            String[] arr2 = "Speed,Speed2,Speed3,Price".Split(',');
            foreach (RepeaterItem item in this.Repeater3.Items)
            {
                foreach (String m in arr2)
                {
                    TextBox t1 = item.FindControl(m) as TextBox;
                    if (t1 != null)
                    {
                        if (t1.Text.Trim() != String.Empty)
                        {
                            if (double.Parse(t1.Text.Trim()) == 0)
                            {
                                t1.Text = String.Empty;
                            }
                        }
                    }
                }
            }
            base.OnPreRenderComplete(e);
        }

        protected override void OnInit(EventArgs e)
        {
            this.button1.Click += new EventHandler(button1_Click);
            this.button2.Click += new EventHandler(button2_Click);
            this.button3.Click += new EventHandler(button3_Click);
            base.OnInit(e);
        }

        void button1_Click(object sender, EventArgs e)
        {
            JStrInfoBU bu1 = new JStrInfoBU();
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("StrID", "普列折旧费率"));
            Dictionary<String, object> data1 = new Dictionary<string, object>();
            data1["StrText"] = this.Fee1.Text;
            bu1.UpdateData(condition, data1);

            condition.Clear();
            condition.Add(new SearchField("StrID", "动列折旧费率"));
            data1["StrText"] = this.Fee2.Text;
            bu1.UpdateData(condition, data1);
            BusinessRule.TrainProfile.SetData();
            WebFrame.Util.JAjax.Alert("提示：更新数据操作成功！");
        }

        void button2_Click(object sender, EventArgs e)
        {
            WebFrame.Data.JTable tab1 = new WebFrame.Data.JTable("COMMTRAINWEIGHTPROFILE");
            System.Collections.Generic.List<WebFrame.Data.SearchField> condition =
                new System.Collections.Generic.List<WebFrame.Data.SearchField>();
            System.Collections.Generic.Dictionary<String, object> data1 =
                new System.Collections.Generic.Dictionary<string, object>();
            int i = 1;
            String[] arr1 = "Speed,YzPrice,RzPrice,SYzPrice,SRzPrice,YwPrice,RwPrice,GRw19KPrice,GRw19TPrice,CaPrice,KdPrice".Split(',');
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
                i++;
            }
            tab1.Close();
            BusinessRule.CheXianProfile.SetData();
            WebFrame.Util.JAjax.Alert("提示：更新数据操作成功！");
        }

        void button3_Click(object sender, EventArgs e)
        {
            WebFrame.Data.JTable tab1 = new WebFrame.Data.JTable("HIGHTRAINPROFILE");
            System.Collections.Generic.List<WebFrame.Data.SearchField> condition =
                new System.Collections.Generic.List<WebFrame.Data.SearchField>();
            System.Collections.Generic.Dictionary<String, object> data1 =
                new System.Collections.Generic.Dictionary<string, object>();
            int i = 1;
            String[] arr1 = "Price,Speed2,Speed3,Speed".Split(',');
            foreach (RepeaterItem item in this.Repeater3.Items)
            {
                condition.Clear();
                condition.Add(new WebFrame.Data.SearchField("id", i + "", WebFrame.SearchFieldType.NumericType));
                data1.Clear();
                foreach (String m in arr1)
                {
                    TextBox t1 = item.FindControl(m) as TextBox;
                    if (t1 != null)
                    {
                        if (t1.Text.Trim() != String.Empty)
                        {
                            data1[m] = t1.Text;
                        }
                        else
                        {
                            data1[m] = "0";
                        }
                    }
                }
                tab1.EditData(data1, condition);
                i++;
            }
            tab1.Close();
            BusinessRule.HighTrainProfile.SetData();
            WebFrame.Util.JAjax.Alert("提示：更新数据操作成功！");
        }
    }
}
