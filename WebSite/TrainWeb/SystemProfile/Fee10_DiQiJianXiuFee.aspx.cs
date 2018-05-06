using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSite.TrainWeb.SystemProfile
{
    public partial class Fee10_DiQiJianXiuFee : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            this.button1.Click += new EventHandler(button1_Click);
            this.button2.Click += new EventHandler(button2_Click);
            this.button3.Click += new EventHandler(button3_Click);
            base.OnInit(e);
        }

        

        protected override void OnPreRenderComplete(EventArgs e)
        {
            String[] arr1 = "YZA4,RZA4,SYZA4,SRZA4,YWA4,RWA4,RW19KA4,RW19TA4,CAA4,KDA4".Split(',');
            foreach (RepeaterItem item in this.Repeater1.Items)
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


            String[] arr2 = "A2Fee,A3Fee".Split(',');
            foreach (RepeaterItem item in this.Repeater2.Items)
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

            String[] arr3 = "Cost2,Cost21,Cost22".Split(',');
            foreach (RepeaterItem item in this.Repeater3.Items)
            {
                foreach (String m in arr3)
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

        void button1_Click(object sender, EventArgs e)
        {
            WebFrame.Data.JTable tab1 = new WebFrame.Data.JTable("COMMTRAINWEIGHTPROFILE");
            System.Collections.Generic.List<WebFrame.Data.SearchField> condition =
                new System.Collections.Generic.List<WebFrame.Data.SearchField>();
            System.Collections.Generic.Dictionary<String, object> data1 =
                new System.Collections.Generic.Dictionary<string, object>();
            int i = 1;
            String[] arr1 = "YZA4,RZA4,SYZA4,SRZA4,YWA4,RWA4,RW19KA4,RW19TA4,CAA4,KDA4".Split(',');
            foreach (RepeaterItem item in this.Repeater1.Items)
            {
                condition.Clear();
                condition.Add(new WebFrame.Data.SearchField("num", i + "", WebFrame.SearchFieldType.NumericType));
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
            BusinessRule.CheXianProfile.SetData();
            WebFrame.Util.JAjax.Alert("提示：更新数据操作成功！");
        }

        void button2_Click(object sender, EventArgs e)
        {
            WebFrame.Data.JTable tab1 = new WebFrame.Data.JTable("A2A3FEE");
            System.Collections.Generic.List<WebFrame.Data.SearchField> condition =
                new System.Collections.Generic.List<WebFrame.Data.SearchField>();
            System.Collections.Generic.Dictionary<String, object> data1 =
                new System.Collections.Generic.Dictionary<string, object>();
            int i = 1;
            String[] arr1 = "A2Fee,A3Fee".Split(',');
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
            BusinessRule.CheXianProfile.SetData();
            BusinessRule.RiChangFeeProfile.SetData();
            BusinessRule.A2A3FeeProfile.SetData();
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
            String[] arr1 = "Cost2,Cost21,Cost22".Split(',');
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

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
