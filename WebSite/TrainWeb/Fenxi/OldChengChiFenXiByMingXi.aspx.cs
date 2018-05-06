using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessRule;

namespace WebSite.TrainWeb.Fenxi
{
    public partial class OldChengChiFenXiByMingXi : System.Web.UI.Page
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
                if (String.IsNullOrEmpty(Request.QueryString["byear"]) == false)
                {
                    this.byear.Text = Request.QueryString["byear"];
                }
                else
                {
                    this.byear.Text = DateTime.Now.Year + "";
                }

                if (String.IsNullOrEmpty(Request.QueryString["bmonth"]) == false)
                {
                    this.bmonth.Text = Request.QueryString["bmonth"];
                }
                else
                {
                    this.bmonth.Text = DateTime.Now.Month + "";
                }

                NewTrainBU.SetListControlTrainType(this.traintype, String.Empty);
                if (String.IsNullOrEmpty(Request.QueryString["kind"]) == false)
                {
                    String temp1 = Request.QueryString["kind"];
                    if (this.traintype.Items.FindByValue(temp1) != null)
                    {
                        this.traintype.SelectedValue = temp1;
                    }
                }
                
                
                this.SearchInfo.Visible = true;
                this.SearchData();
            }
        }

        private void SearchData()
        {
            DataTable dt1 = BusinessRule.NewTrainBU.GetFenXiDataByMingXi(this.traintype.SelectedValue,
                int.Parse(this.byear.Text), int.Parse(this.bmonth.Text));
            this.Repeater1.DataSource = dt1;
            this.Repeater1.DataBind();

            this.Repeater2.DataSource = dt1;
            this.Repeater2.DataBind();
        }
    }
}
