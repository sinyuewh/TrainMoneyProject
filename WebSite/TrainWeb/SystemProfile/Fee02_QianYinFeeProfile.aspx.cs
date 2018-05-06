using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFrame.Data;
using WebFrame.Designer;

namespace WebSite.TrainWeb.SystemProfile
{
    public partial class Fee02_QianYinFeeProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.ShipFee1.Text = BusinessRule.TrainProfile.ShipFee1 + "";
                this.ShipFee2.Text = BusinessRule.TrainProfile.ShipFee2 + "";

                this.Qyfj1.Text = BusinessRule.TrainProfile.QianYinFjFee1 + "";
                this.Qyfj2.Text = BusinessRule.TrainProfile.QianYinFjFee2 + "";
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.button1.Click += new EventHandler(button1_Click);
            this.JButton2.Click += new EventHandler(JButton2_Click);
            this.JButton6.Click += new EventHandler(JButton6_Click);
            this.JButton4.Click += new EventHandler(JButton4_Click);
            base.OnInit(e);
        }

        //更新直供电牵引附加费
        void JButton4_Click(object sender, EventArgs e)
        {
            JStrInfoBU bu1 = new JStrInfoBU();
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("StrID", "直供电内燃牵引附加费"));
            Dictionary<String, object> data1 = new Dictionary<string, object>();
            data1["StrText"] = this.Qyfj1.Text;
            bu1.UpdateData(condition, data1);

            condition.Clear();
            condition.Add(new SearchField("StrID", "直供电电力牵引附加费"));
            data1["StrText"] = this.Qyfj2.Text;
            bu1.UpdateData(condition, data1);

            BusinessRule.TrainProfile.SetData();
            WebFrame.Util.JAjax.Alert("提示：更新数据操作成功！");
        }

        //更新轮渡费单价表
        void JButton6_Click(object sender, EventArgs e)
        {
            JStrInfoBU bu1 = new JStrInfoBU();
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("StrID", "空调客车轮渡费"));
            Dictionary<String, object> data1 = new Dictionary<string, object>();
            data1["StrText"] = this.ShipFee1.Text;
            bu1.UpdateData(condition, data1);

            condition.Clear();
            condition.Add(new SearchField("StrID", "非空调客车轮渡费"));
            data1["StrText"] = this.ShipFee2.Text;
            bu1.UpdateData(condition, data1);

            BusinessRule.TrainProfile.SetData();
            WebFrame.Util.JAjax.Alert("提示：更新数据操作成功！");
            
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            String[] arr1 = "YzWeight,YwWeight,SYzWeight,SRzWeight,Rzweight,RwWeight,GRw19KWeight,GRw19TWeight,CaWeight,KdWeight".Split(',');
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
            base.OnPreRenderComplete(e);
        }

        void JButton2_Click(object sender, EventArgs e)
        {
            JTable tab1 = new JTable("COMMTRAINWEIGHTPROFILE");
            System.Collections.Generic.List<WebFrame.Data.SearchField> condition =
                new System.Collections.Generic.List<WebFrame.Data.SearchField>();
            System.Collections.Generic.Dictionary<String, object> data1 =
                new System.Collections.Generic.Dictionary<string, object>();
            int i = 0;
            String[] arr1 = "YzWeight,YwWeight,SYzWeight,SRzWeight,Rzweight,RwWeight,GRw19KWeight,GRw19TWeight,CaWeight,KdWeight".Split(',');
            foreach (RepeaterItem item in this.Repeater2.Items)
            {
                condition.Clear();
                condition.Add(new WebFrame.Data.SearchField("num", (i +1)+ "", WebFrame.SearchFieldType.NumericType));
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

        void button1_Click(object sender, EventArgs e)
        {
            JTable  tab1 = new JTable("QIANYINFEEPROFILE");
            System.Collections.Generic.List<WebFrame.Data.SearchField> condition =
                new System.Collections.Generic.List<WebFrame.Data.SearchField>();
            System.Collections.Generic.Dictionary<String, object> data1 =
                new System.Collections.Generic.Dictionary<string, object>();
            int i = 0;
            String[] arr1 = "Fee2,Fee3".Split(',');
            foreach (RepeaterItem item in this.Repeater1.Items)
            {
                condition.Clear();
                condition.Add(new WebFrame.Data.SearchField("QIANYINTYPE", i + "", WebFrame.SearchFieldType.NumericType));
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
            BusinessRule.QianYinFeeProfile.SetData();
            WebFrame.Util.JAjax.Alert("提示：更新数据操作成功！");
        }
    }
}
