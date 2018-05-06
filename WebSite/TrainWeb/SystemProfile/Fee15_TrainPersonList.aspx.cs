using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BusinessRule;
using System.Data;
using WebFrame.ExpControl;
using WebFrame.Data;

namespace WebSite.TrainWeb.SystemProfile
{
    public partial class Fee15_TrainPersonList : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            this.link1.Click += new EventHandler(link1_Click);
            this.JButton2.Click += new EventHandler(JButton2_Click);
            base.OnInit(e);
        }

        //提交人员数据更新
        void JButton2_Click(object sender, EventArgs e)
        {
            this.UpdateData();
            this.BindData();
            WebFrame.Util.JAjax.Alert("提示：更新数据操作成功！");
        }

        //人员数据初始化
        void link1_Click(object sender, EventArgs e)
        {
            //TrainPersonBU.PersonInit();
            this.BindData();
            WebFrame.Util.JAjax.Alert("提示：操作成功！");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BindData();
            }
        }

        //显示编制的值
        private void BindData()
        {
            String[] gw1=null;
            String[] gw2=null;

            TrainPersonBU.GetTrainGw(out gw1, out gw2);
            String[] trainType1 = new String[] { "25T", "25G(直)", "25G(非直)", "25B" };
            if (gw1 != null && gw1.Length > 0)
            {
               CreateTableData(tab1, gw1, trainType1,"tabData1");
            }

            String[] trainType2 = new String[] { "CRH动坐(8)", "CRH动坐(16)", "CRH动卧" };
            if (gw2 != null && gw2.Length > 0)
            {
               CreateTableData(tab2, gw2, trainType2,"tabData2");
            }
        }

        //更新编制的值
        private void UpdateData()
        {
            JTable tab1 = new JTable("TRAINPERSON");
            List<String> list1 = ViewState["tabData1"] as List<string>;
            if (list1 != null && list1.Count > 0)
            {
                foreach (String m in list1)
                {
                    String m1 = Request.Form["ctl00$ContentPlaceHolder1$" + m];
                    if (m1.Trim() == String.Empty) m1 = "0";
                    String[] arr = m.Split('_');
                    String gw = arr[0];
                    String traintype = arr[1];
                    this.SetValue(tab1, gw, traintype, double.Parse(m1),"0");
                }
            }

            List<String> list2 = ViewState["tabData2"] as List<string>;
            if (list2 != null && list2.Count > 0)
            {
                foreach (String m in list2)
                {
                    String m1 = Request.Form["ctl00$ContentPlaceHolder1$" + m];
                    if (m1.Trim() == String.Empty) m1 = "0";
                    String[] arr = m.Split('_');
                    String gw = arr[0];
                    String traintype = arr[1];
                    this.SetValue(tab1, gw, traintype, double.Parse(m1),"1");
                }
            }
            tab1.Close();
        }

        //设置编制的值
        private void CreateTableData(Table tab, String[] gw, 
            String[] trainType,String ListName)
        {
            if (gw != null && gw.Length > 0)
            {
                //标题行
                TableRow dr = new TableRow();

                TableCell cel0 = new TableCell();
                cel0.Text = "车型";
                cel0.Attributes["class"] = "Caption";
                dr.Cells.Add(cel0);

                List<String> list1 = new List<string>();

                foreach (String m in gw)
                {
                    TableCell cell = new TableCell();
                    cell.Text = m;
                    cell.Attributes["class"] = "Caption";
                    dr.Cells.Add(cell);
                }
                tab.Rows.Add(dr);

                //数据行
                JTable tab1 = new JTable("TRAINPERSON");
                foreach (String m in trainType)
                {
                    dr = new TableRow();
                    cel0 = new TableCell();
                    cel0.Text = m;
                    cel0.Attributes["class"] = "Data";
                    dr.Cells.Add(cel0);

                    foreach (String m1 in gw)
                    {
                        TextBox t1 = new TextBox();
                        t1.ID = m1 + "_" + m;
                        t1.Width = Unit.Parse("60");
                        t1.Style["text-align"] = "center";
                        t1.Style["background-color"] = "#E7E7E7";
                        t1.Style.Add("border", "1px solid #d9e6f0");
                        t1.Style["gw"] = m1;
                        t1.Style["traintype"] = m;
                        double  value1 = GetValue(tab1, m1, m);
                        if (value1 != 0)
                        {
                            t1.Text = GetValue(tab1, m1, m) + "";
                        }
                       
                        TableCell cell = new TableCell();
                        cell.Controls.Add(t1);
                        cell.Attributes["class"] = "Data";
                        
                        dr.Cells.Add(cell);
                        list1.Add(t1.ID);
                    }
                    tab.Rows.Add(dr);
                }
                tab1.Close();
                ViewState[ListName] = list1;
            }
        }

        //得到编制的值
        private double  GetValue(JTable tab1,String gw, String traintype)
        {
            double  result = 0;
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("gw", gw));
            condition.Add(new SearchField("traintype", traintype));
            DataRow dr = tab1.GetFirstDataRow(condition, "pcount");
            if (dr != null) result = double.Parse(dr[0].ToString());
            return result;
        }

        //设置编制的值
        private void SetValue(JTable tab1, String gw, String traintype, double Value,String kind)
        {
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("gw", gw));
            condition.Add(new SearchField("traintype", traintype));
            Dictionary<String, object> data1 = new Dictionary<string, object>();
            DataSet ds = tab1.SearchData(condition, -1, "*");
            if (ds != null)
            {
                DataRow dr1 = null;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dr1 = ds.Tables[0].Rows[0];
                    dr1["pcount"] = Value;
                }
                else
                {
                    dr1 = ds.Tables[0].NewRow();
                    dr1["kind"] = kind;
                    dr1["gw"] = gw;
                    dr1["traintype"] = traintype;
                    dr1["pcount"] = Value;
                    ds.Tables[0].Rows.Add(dr1);
                }
                tab1.Update(ds.Tables[0]);
            }
        }
    }
}
