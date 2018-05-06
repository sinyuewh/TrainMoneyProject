using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFrame.Util;
using WebFrame.Data;
using BusinessRule;
using System.Data;

namespace WebSite.TrainWeb.Train
{
    public partial class EditStation : System.Web.UI.Page
    {
        protected DataTable dt = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }

        }


        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            int t1 = int.Parse(this.miles.Text);
            int index = 0;
            foreach (RepeaterItem item1 in this.Repeater1.Items)
            {
                TextBox txt1 = item1.FindControl("Miles") as TextBox;
                if (txt1 != null)
                {
                    if (index != this.Repeater1.Items.Count - 1)
                    {
                        t1 = t1 - int.Parse(txt1.Text);
                    }
                    else
                    {
                        txt1.Text = t1.ToString();
                    }
                    index++;
                }
            }
        }

        protected void lblcjl2_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            RepeaterItem repeaterItem = (RepeaterItem)textBox.Parent;
            TextBox lblcjl2 = (TextBox)repeaterItem.FindControl("lblcjl2");
            Label lbl = (Label)repeaterItem.FindControl("Lab");
            TrainShouRu1 bu = new TrainShouRu1();

            lbl.Text = bu.GetLineName(lblcjl2.Text.ToString().Trim());
        }

        protected override void OnInit(EventArgs e)
        {
            //this.Repeater1.ItemCommand += new RepeaterCommandEventHandler(Repeater1_ItemCommand);
            this.Repeater1.ItemDataBound += new RepeaterItemEventHandler(Repeater1_ItemDataBound);
            this.Button1.Click += new EventHandler(Button1_Click);
            this.Button2.Click += new EventHandler(Button2_Click);
            this.dqhAll.CheckedChanged += new EventHandler(dqhAll_CheckedChanged);
            TrainShouRu1 bu = new TrainShouRu1();
            dt = bu.GetkzList("");
            //CorpInfoBU bu = new CorpInfoBU();
            //dt = bu.GetCorpName();
            base.OnInit(e);
        }

        //
        void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {   
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = e.Item.DataItem as DataRowView;

                DropDownList dpdlkz = e.Item.FindControl("dpdlkz") as DropDownList;
                HiddenField indexlbl = e.Item.FindControl("IndexLabel") as HiddenField;

                dpdlkz.DataSource = dt;

                dpdlkz.DataTextField = "Title";
                dpdlkz.DataValueField = "ID";
                dpdlkz.DataBind();
                dpdlkz.Items.Insert(0, new ListItem("无", "0"));

                Label lab1 = e.Item.FindControl("lab1") as Label;
                DropDownList drop1 = e.Item.FindControl("dpdlkz") as DropDownList;

                if (drop1.Items.FindByValue(lab1.Text) != null)
                {
                    drop1.SelectedValue = lab1.Text;
                }
            

                TextBox t1 = e.Item.FindControl("CORPNAME") as TextBox;
                t1.Attributes["OnBlur"] = String.Format("javascript:changevalue('{0}','{1}','{2}');", dpdlkz.ClientID, t1.ClientID, indexlbl.ClientID);

                dpdlkz.Attributes.Add("onchange", String.Format("javascript:setsamevalue('{0}','{1}');", dpdlkz.ClientID, indexlbl.ClientID));
            } 
        }

        void dqhAll_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = dqhAll.Checked;
            foreach (RepeaterItem item1 in Repeater1.Items)
            {
                CheckBox chk1 = item1.FindControl("dqh") as CheckBox;
                chk1.Checked = flag;
            }
        }

        public void MyCheckChange(object sender, EventArgs e)
        {
            bool flag = true;
            foreach (RepeaterItem item1 in Repeater1.Items)
            {
                CheckBox chk1 = item1.FindControl("dqh") as CheckBox;
                if (chk1.Checked == false)
                { flag = false; break; }
            }
            dqhAll.Checked = flag;
        }

        void Button2_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt1 = new System.Data.DataTable();
            dt1.Columns.Add("Astation");
            dt1.Columns.Add("Bstation");
            dt1.Columns.Add("Miles", typeof(int));
            dt1.Columns.Add("JnFlag");
            dt1.Columns.Add("dqh");
            dt1.Columns.Add("shipflag");
            dt1.Columns.Add("KZID");
            dt1.Columns.Add("CJLID");
            dt1.Columns.Add("gtllx");
           
            int miles0 = 0;

            TrainShouRu1 bu = new TrainShouRu1();

            foreach (RepeaterItem item in this.Repeater1.Items)
            {
                Label lab1 = item.FindControl("AStation") as Label;
                Label lab2 = item.FindControl("BStation") as Label;
                TextBox txt1 = item.FindControl("Miles") as TextBox;
                CheckBox chk1 = item.FindControl("JnFlag") as CheckBox;
                CheckBox dqh1 = item.FindControl("dqh") as CheckBox;
                CheckBox ship1 = item.FindControl("SHIPFLAG") as CheckBox;
                CheckBox gglx1 = item.FindControl("GTLLX") as CheckBox;
                DropDownList gsid = item.FindControl("dpdlkz") as DropDownList;
                TextBox cjlid = item.FindControl("lblcjl2") as TextBox;
                //TextBox txtFee1 = item.FindControl("Fee1") as TextBox;
                //TextBox txtFee2 = item.FindControl("Fee2") as TextBox;
               
                if (lab1 != null && lab2 != null && txt1 != null)
                {
                    System.Data.DataRow dr1 = dt1.NewRow();
                    dr1["Astation"] = lab1.Text;
                    dr1["Bstation"] = lab2.Text;
                    dr1["Miles"] = int.Parse(txt1.Text);
                  
                    if (chk1.Checked)
                    {
                        dr1["JnFlag"] = "1";
                    }
                    else
                    {
                        dr1["JnFlag"] = DBNull.Value;
                    }
                    if (dqh1.Checked)
                    {
                        dr1["dqh"] = "1";
                    }
                    else
                    {
                        dr1["dqh"] = DBNull.Value;
                    }

                    if (ship1.Checked)
                    {
                        dr1["shipflag"] = "1";
                    }
                    else
                    {
                        dr1["shipflag"] = DBNull.Value;
                    }

                    //高铁联络线
                    if (gglx1.Checked)
                    {
                        dr1["gtllx"] = "1";
                    }
                    else
                    {
                        dr1["gtllx"] = DBNull.Value;
                    }

                    dr1["KZID"] = gsid.SelectedValue;

                    if (cjlid.Text.ToString().Trim() != "")
                    {
                        dr1["CJLID"] = cjlid.Text.ToString().Trim();
                    }
                    else
                    {
                        dr1["CJLID"] = null;
                    }
                    

                    dt1.Rows.Add(dr1);
                    miles0 = miles0 + int.Parse(txt1.Text);

                   /* if (txtFee1.Text != String.Empty)
                    {
                        dr1["Fee1"] = txtFee1.Text;
                    }
                    else
                    {
                        dr1["Fee1"] = DBNull.Value;
                    }

                    if (txtFee2.Text != String.Empty)
                    {
                        dr1["Fee2"] = txtFee2.Text;
                    }
                    else
                    {
                        dr1["Fee2"] = DBNull.Value;
                    }*/
                } 
                
                TextBox box1 = item.FindControl("lblcjl2") as TextBox;
                Label labname = item.FindControl("Lab") as Label;
                if (box1.Text.Trim() != "")
                {
                    labname.Text = bu.GetLineName(box1.Text.Trim());
                }
            }

            if (miles0 != int.Parse(this.miles.Text))
            {
                WebFrame.Util.JAjax.Alert("错误：里程数的总和应等于" + this.miles.Text + "，请重新分配！");
                return;
            }
            else
            {
                String lineid1 = this.Data1.ParaItems[0].ParaValue.ToString();
                String error = BusinessRule.Line.SetMiddleStation(lineid1, dt1);
                if (String.IsNullOrEmpty(error) == false)
                {
                    this.Button2.ExecutePara.Success = false;
                }
                else
                {
                    this.Button2.ExecutePara.Success = true;
                }

                WebFrame.FrameLib.ExecuteButtonInfo(this.Button2);
            }
        }

        //确定中间站点
        void Button1_Click(object sender, EventArgs e)
        {
            String lineid1 = this.Data1.ParaItems[0].ParaValue.ToString();
            System.Data.DataTable dt1 = Line.GetMiddleStation(lineid1, this.midstation.Text);
            ViewState["LineStationData"] = dt1;
            this.Repeater1.DataSource = dt1;
            this.Repeater1.DataBind();
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                String lineid1 = this.Data1.ParaItems[0].ParaValue.ToString();
                System.Data.DataTable dt1 = BusinessRule.Line.GetLineStationData(lineid1);
                this.Repeater1.DataSource = dt1;
                this.Repeater1.DataBind();
                ViewState["LineStationData"] = dt1;

                //设置所有选中的值
                bool flag = true;
                foreach (RepeaterItem item1 in Repeater1.Items)
                {
                    CheckBox chk1 = item1.FindControl("dqh") as CheckBox;
                    if (chk1.Checked == false)
                    { flag = false; break; }
                }
                dqhAll.Checked = flag;
            }
            base.OnLoad(e);
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                String lineid1 = this.Data1.ParaItems[0].ParaValue.ToString();
                this.midstation.Text = BusinessRule.Line.GetLineMiddleStation(lineid1);
                TrainShouRu1 bu=new TrainShouRu1();

                foreach (RepeaterItem item1 in this.Repeater1.Items)
                {
                    Label lab1 = item1.FindControl("lab1") as Label;
                    TextBox box1 = item1.FindControl("lblcjl2") as TextBox;
                    DropDownList drop1 = item1.FindControl("dpdlkz") as DropDownList;

                    if (drop1.Items.FindByValue(lab1.Text) != null)
                    {
                        drop1.SelectedValue = lab1.Text;
                    }

                    Label labname = item1.FindControl("Lab") as Label;
                    if (box1.Text.Trim()!="")
                    {
                       labname.Text = bu.GetLineName(box1.Text.Trim());
                    }
                }
            }
            base.OnPreRenderComplete(e);
        }
    }
}
