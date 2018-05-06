using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFrame;
using WebFrame.Util;

namespace WebSite.TrainWeb.Fenxi
{
    public partial class SelectMulTrainLine : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            this.buttonSearch.Click += new EventHandler(buttonSearch_Click);
            base.OnInit(e);
        }

        //确定返回
        void buttonSearch_Click(object sender, EventArgs e)
        {
            String selLine = String.Empty;
            foreach (RepeaterItem item in this.repeater1.Items)
            {
                String t1 = String.Empty;
                String a1 = (item.FindControl("astation") as TextBox).Text.Trim();
                String b1 = (item.FindControl("bstation") as TextBox).Text.Trim();
                String c1 = (item.FindControl("sd") as DropDownList).SelectedValue.Trim();

                if (a1 !=String.Empty  && b1 != String.Empty)
                {
                    String id1 = (item.FindControl("lineid") as TextBox).Text.Trim();
                    if (id1 != String.Empty)
                    {
                        t1 = a1 + "-" + b1 + "(" + id1 + "#" + c1 + ")";
                    }
                    else
                    {
                        t1 = a1 + "-" + b1 + "(" + "0" + "#" + c1 + ")";
                    }
                    if (selLine == String.Empty)
                    {
                        selLine = t1;
                    }
                    else
                    {
                        selLine = selLine + "," + t1;
                    }
                }
            }

            if (selLine != String.Empty)
            {
                String js="";
                js=js+"var parentid = '"+Request.QueryString["parent"] +"';";
                js = js + " if (parentid != '' && window.opener != null) { ";
                js=js+" window.opener.document.getElementById(parentid).value = '"+selLine+"'; ";
                js=js+" window.close(); }";
                JAjax.ExecuteJS(js);
            }
            else
            {
                JAjax.Alert("错误：没有选择合适的分段！");
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!Page.IsPostBack)
                {
                    int[] arr1 = new int[] {1,2,3,4,5,6,7,8,9,10};
                    this.repeater1.DataSource = arr1;
                    this.repeater1.DataBind();

                    RepeaterItem item1 = this.repeater1.Items[0];
                    TextBox t1 = item1.FindControl("astation") as TextBox;
                    if (t1 != null) t1.Text = Request.QueryString["astation"];

                    TextBox t2 = item1.FindControl("bstation") as TextBox;
                    if (t2 != null) t2.Text = Request.QueryString["bstation"];

                    RepeaterItem item2 = this.repeater1.Items[this.repeater1.Items.Count-1];
                    TextBox t3 = item2.FindControl("bstation") as TextBox;
                    t3.Attributes["ReadOnly"] = "true";
                    t3.ToolTip = "只读控件";

                    for (int i = 0; i < this.repeater1.Items.Count - 1; i++)
                    {
                        TextBox c0 = this.repeater1.Items[i].FindControl("astation") as TextBox;
                        TextBox c1 = this.repeater1.Items[i].FindControl("bstation") as TextBox;
                        TextBox c2 = this.repeater1.Items[i + 1].FindControl("astation") as TextBox;
                        TextBox c3 = this.repeater1.Items[i + 1].FindControl("bstation") as TextBox;
                        c0.Attributes["ReadOnly"] = "true";

                        if (i < this.repeater1.Items.Count - 2)
                        {
                            c1.Attributes["onblur"] = String.Format("javascript:changevalue('{0}','{1}','{2}','{3}');"
                                                       , c0.ClientID, c1.ClientID, c2.ClientID, c3.ClientID);
                        }

                        TextBox lineid = this.repeater1.Items[i].FindControl("lineid") as TextBox;
                        Button button1 = this.repeater1.Items[i].FindControl("button1") as Button;
                        button1.Attributes["onclick"] = String.Format("javascript:selLine('{0}');return false;",lineid.ClientID);
                    }
                }
            }
        }
    }
}
