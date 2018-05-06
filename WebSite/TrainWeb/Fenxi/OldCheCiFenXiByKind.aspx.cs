using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebSite.TrainWeb.Fenxi
{
    public partial class OldCheCiFenXiByKind : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            this.Button1.Click += new EventHandler(Button1_Click);
            base.OnLoad(e);
        }

        void Button1_Click(object sender, EventArgs e)
        {
            this.SearchInfo.Visible = true;
            this.SearchData();

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //this.Button1.Attributes.Add("onclick", "ShowWaiting();");
                this.byear.Text = DateTime.Now.Year + "";
                this.bmonth.Text = DateTime.Now.Month + "";
                this.SearchInfo.Visible = true;
                this.SearchData();
            }
        }

        private void SearchData()
        {
            DataTable dt1 = BusinessRule.NewTrainBU.GetFenXiDataByKind(int.Parse(this.byear.Text), int.Parse(this.bmonth.Text));
            this.Repeater1.DataSource = dt1;
            this.Repeater1.DataBind();

            this.Repeater2.DataSource = dt1;
            this.Repeater2.DataBind();
        }
    }
}
