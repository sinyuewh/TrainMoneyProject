using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSite.TrainWeb.Fenxi
{
    public partial class OldCheCiFenxiPaiHang : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            this.Button1.Click += new EventHandler(Button1_Click);
            base.OnInit(e);
        }

        //执行排行的搜索
        void Button1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int year1 = DateTime.Now.Year;
                int month1 = DateTime.Now.Month;
                this.byear.Text = year1 + "";

                for (int i = 1; i <= 12; i++)
                {
                    ListItem list1 = new ListItem(i + "月", i + "");
                    this.bmonth.Items.Add(list1);
                }

                ListItem list0 = new ListItem("半年", "-1");
                this.bmonth.Items.Add(list0);

                ListItem list2 = new ListItem("全年", "-2");
                this.bmonth.Items.Add(list2);
                this.bmonth.SelectedValue = month1 + "";
            }
        }
    }
}
