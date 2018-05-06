using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessRule;
using System.Data;

namespace WebSite.TrainWeb.Train
{
    public partial class NewTrainLiLunList : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            this.link1.Click += new EventHandler(link1_Click);
            this.Repeater1.ItemDataBound += new RepeaterItemEventHandler(Repeater1_ItemDataBound);
            base.OnInit(e);
        }

        void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRow dr = (e.Item.DataItem as DataRowView).Row;
            double sshou1 = double.Parse(dr["sshouru"].ToString());
            double shou1 = double.Parse(dr["shouru"].ToString());
            Label lab1 = e.Item.FindControl("szr") as Label;
            if (shou1 > 0)
            {
                lab1.Text =String.Format("{0:n2}",sshou1*100/shou1);
            }
        }

        //计算车次的理论值
        void link1_Click(object sender, EventArgs e)
        {
            if (this.msg1.Value.IndexOf("-") > 0)
            {
                int year1 = DateTime.Now.Year;
                int month1 = DateTime.Now.Month;
                try
                {
                    String str1 = this.msg1.Value + "-1";
                    DateTime dt1 = DateTime.Parse(str1);
                    year1 = dt1.Year;
                    month1 = dt1.Month;
                }
                catch (Exception err) { ;}
                NewTrainBU.CalLiLunValue(year1, month1);
            }
            else
            {
                int year1 = DateTime.Now.Year;
                try
                {
                    String str1 = this.msg1.Value + "-1-1";
                    DateTime dt1 = DateTime.Parse(str1);
                    year1 = dt1.Year;
                }
                catch (Exception err) { ;}

                for (int i = 1; i <= 12; i++)
                {
                    NewTrainBU.CalLiLunValue(year1, i);
                }
            }
            this.Repeater1.DataBind();
            WebFrame.Util.JAjax.Alert("提示：导入数据操作成功！");
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
