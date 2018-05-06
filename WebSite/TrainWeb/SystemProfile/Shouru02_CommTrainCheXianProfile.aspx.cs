using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebFrame.Data;
using BusinessRule;

namespace WebSite.TrainWeb.SystemProfile
{
    public partial class Shouru02_CommTrainCheXianProfile : System.Web.UI.Page
    {
        protected override void OnPreRenderComplete(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                double price0 = double.Parse(WebFrame.Designer.JStrInfoBU.GetStrTextByID("基本硬座费率"));
                DataTable dt1 = Data1.GetListData();
                int i = 1;
                foreach (DataRow dr in dt1.Rows)
                {
                    TextBox t1 = Rate1.Parent.FindControl("Rate" + i) as TextBox;
                    if (t1 != null) t1.Text = dr["Rate"].ToString().Trim();

                    Label lab1 = Price1.Parent.FindControl("Price" + i) as Label;
                    if (lab1 != null)
                    {
                        double v1 = Math.Round(price0 * double.Parse(dr["Rate"].ToString().Trim()) / 100, 5);
                        lab1.Text = String.Format("{0:0.00000}", v1);
                    }

                    TextBox t2 = Person1.Parent.FindControl("Person" + i) as TextBox;
                    if (t2 != null) t2.Text = dr["PCount"].ToString().Trim();

                    i++;
                }
            }
            base.OnPreRenderComplete(e);
        }

        protected override void OnInit(EventArgs e)
        {
            this.button1.Click += new EventHandler(button1_Click);
            base.OnInit(e);
        }

        //Submit Data
        void button1_Click(object sender, EventArgs e)
        {
            WebFrame.Data.JTable tab1 = new WebFrame.Data.JTable("CHEXIANBIANZHU");
            System.Collections.Generic.List<WebFrame.Data.SearchField> condition =
                new System.Collections.Generic.List<WebFrame.Data.SearchField>();
            System.Collections.Generic.Dictionary<String, object> data1 =
                new System.Collections.Generic.Dictionary<string, object>();
            for (int i = 1; i <= 17; i++)
            {
                condition.Clear();
                condition.Add(new WebFrame.Data.SearchField("id", i + "", WebFrame.SearchFieldType.NumericType));
                data1.Clear();

                TextBox t1 = Rate1.Parent.FindControl("Rate" + i) as TextBox;
                if (t1 != null)
                {
                    data1["Rate"] = t1.Text;
                }


                TextBox t2 = Person1.Parent.FindControl("Person" + i) as TextBox;
                if (t2 != null)
                {
                    data1["Person"] = t2.Text;
                }
                tab1.EditData(data1, condition);
            }
            tab1.Close();
            ChexianBianZhuData.SetData();
            WebFrame.Util.JAjax.Alert("提示：更新数据操作成功！");
        }
    }
}
