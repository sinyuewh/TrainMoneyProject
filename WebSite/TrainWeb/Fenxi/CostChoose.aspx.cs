using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessRule;
using System.Data;
using WebFrame.Util;

namespace WebSite.TrainWeb.Fenxi
{
    public partial class CostChoose : System.Web.UI.Page
    {
        protected override void  OnInit(EventArgs e)
        {
            this.butSubmit.Click += new EventHandler(butSubmit_Click);
            base.OnInit(e);
        }

        
        void butSubmit_Click(object sender, EventArgs e)
        {
            int chooseNum = 0;
            string num = "";
            bool first = true;
            foreach (RepeaterItem item in Repeater1.Items)
            {
                CheckBox cbox = item.FindControl("SelCost") as CheckBox;
                if (cbox != null && cbox.Checked)
                {
                    TextBox box = item.FindControl("TextBox1") as TextBox;
                    Label lbllinename = item.FindControl("lblname") as Label;
                   
                    if (first)
                    {
                        if (box != null)
                        {
                            num = box.Text.Trim();
                            first = false;
                        }
                    }
                    else
                    {
                        num = num + "&&" + box.Text.Trim();
                    } 
                    
                    chooseNum++;
                }
            }
            if (chooseNum == 0)
            {
                JAjax.Alert("请勾选可变成本！");
            }
            else
            {

                this.selectDetialId.Value = num;
                this.winClose.Value = "1";
            }
        }
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                bind();
            }
        }

        private void bind()
        {
            /*PayProj bu = new PayProj();
            DataTable dt=bu.GetPayPrjList();*/
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("NUM");
            dt.Columns.Add("PAYNAME");

            foreach(int myValue in Enum.GetValues(typeof(EFeeKind)))
            {
                System.Data.DataRow dr = dt.NewRow();
                dr["NUM"] = myValue.ToString().Trim();
                dr["PAYNAME"] = Enum.GetName(typeof(EFeeKind), myValue).Trim();
                dt.Rows.Add(dr);
            }
            this.Repeater1.DataSource = dt;
            this.Repeater1.DataBind();
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                String findcond;
                if (Session["FindCond"] == null || Session["FindCond"].ToString().Trim()=="")
                {
                    foreach (RepeaterItem item in Repeater1.Items)
                    {
                        CheckBox cbox = item.FindControl("SelCost") as CheckBox;
                        cbox.Checked = true;
                    }
                }
                else
                {
                    findcond = Session["FindCond"].ToString().Trim();
                    string[] finditem = findcond.Replace("&&", "&").Split('&');

                    foreach (RepeaterItem item in Repeater1.Items)
                    {
                        CheckBox cbox = item.FindControl("SelCost") as CheckBox;
                        TextBox box = item.FindControl("TextBox1") as TextBox;
                        int enumindex = 0;

                        for (enumindex = 0; enumindex < finditem.Length; ++enumindex)
                        {
                            if (box.Text.ToString().Trim() == finditem[enumindex])
                            {
                                cbox.Checked = true;
                                break;
                            }
                        }

                        if (enumindex == finditem.Length)
                        {
                            cbox.Checked = false;
                        }
                    }
                }
            }
            base.OnPreRenderComplete(e);
        } 
    }
}
