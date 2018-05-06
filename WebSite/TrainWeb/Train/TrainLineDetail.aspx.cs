using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFrame.Data;
using WebFrame;
using BusinessRule;

namespace WebSite.TrainWeb.Train
{
    public partial class TrainLineDetail : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            this.button1.Click += new EventHandler(button1_Click);
            base.OnInit(e);
        }

        void button1_Click(object sender, EventArgs e)
        {
            String lineid1 = this.LineID.Text.Trim();
            if (String.IsNullOrEmpty(lineid1) == false)
            {
                bool check1 = true;
                if (this.dqh.SelectedValue == String.Empty)
                {
                    check1 = false;
                }

                //更新线路站点的电气化标志
                JTable tab1 = new JTable("LINESTATION");
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("lineid", lineid1, SearchFieldType.NumericType));
                Dictionary<string, object> data1 = new Dictionary<string, object>();
                if (check1)
                {
                    data1["dqh"] = "1";
                }
                else
                {
                    data1["dqh"] = String.Empty;
                }
                tab1.EditData(data1, condition);
               
                //更新起始点和终点名称
                if (this.Astation1.Text != String.Empty
                    && this.AStation.Text!=String.Empty)
                {
                    condition.Add(new SearchField("Astation", this.Astation1.Text));
                    data1.Clear();
                    data1.Add("Astation", this.AStation.Text);
                    tab1.EditData(data1, condition);

                    condition.Clear();
                    condition.Add(new SearchField("lineid", lineid1, SearchFieldType.NumericType));
                    condition.Add(new SearchField("bstation", this.Astation1.Text));
                    data1.Clear();
                    data1.Add("bstation", this.AStation.Text);
                    tab1.EditData(data1, condition);
                }


                if (this.BStation1.Text != String.Empty
                   && this.BStation.Text != String.Empty)
                {
                    condition.Clear();
                    condition.Add(new SearchField("lineid", lineid1, SearchFieldType.NumericType));
                    condition.Add(new SearchField("BStation", this.BStation1.Text));
                    data1.Clear();
                    data1.Add("BStation", this.BStation.Text);
                    tab1.EditData(data1, condition);

                    condition.Clear();
                    condition.Add(new SearchField("lineid", lineid1, SearchFieldType.NumericType));
                    condition.Add(new SearchField("AStation", this.BStation1.Text));
                    data1.Clear();
                    data1.Add("AStation", this.BStation.Text);
                    tab1.EditData(data1, condition);
                }
                tab1.Close();
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected override void OnPreRenderComplete(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["LineID"]=="-1")
                {
                    int maxnum = 1, maxlineid = 1;
                    Line.GetMaxNumAndLineID(out maxnum, out maxlineid);
                    this.num.Text = maxnum + "";
                    this.LineID.Text = maxlineid + "";
                }
            }
            base.OnPreRenderComplete(e);
        }
    }
}
