using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSite.TrainWeb.Fenxi
{
    public partial class PictureFenXi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                String TrainID = Request.QueryString["TrainID"];
                int byear = int.Parse(Request.QueryString["byear"]);
                double[] data1 = null;
                double[] data2 = null;

                double[] data3 = null;
                double[] data4 = null;

                BusinessRule.NewTrainBU.GetTrainMoneyByYear(TrainID, byear, out data1, out data2);
                BusinessRule.NewTrainBU.GetTrainZhiChuByYear(TrainID, byear, out data3, out data4);
                System.Data.DataTable dt1 = new System.Data.DataTable();
                dt1.Columns.Add("FeeKind");
                dt1.Columns.Add("1月");
                dt1.Columns.Add("2月");
                dt1.Columns.Add("3月");
                dt1.Columns.Add("4月");
                dt1.Columns.Add("5月");
                dt1.Columns.Add("6月");
                dt1.Columns.Add("7月");
                dt1.Columns.Add("8月");
                dt1.Columns.Add("9月");
                dt1.Columns.Add("10月");
                dt1.Columns.Add("11月");
                dt1.Columns.Add("12月");

                string[] sListKind = { "当月收入", "累计收入", "当月支出", "累计支出" };
                System.Data.DataRow dr1 = dt1.NewRow();

                dr1["FeeKind"] = sListKind[0].Trim();
                dr1["1月"] = data1[0].ToString();
                dr1["2月"] = data1[1].ToString();
                dr1["3月"] = data1[2].ToString();
                dr1["4月"] = data1[3].ToString();
                dr1["5月"] = data1[4].ToString();
                dr1["6月"] = data1[5].ToString();
                dr1["7月"] = data1[6].ToString();
                dr1["8月"] = data1[7].ToString();
                dr1["9月"] = data1[8].ToString();
                dr1["10月"] = data1[9].ToString();
                dr1["11月"] = data1[10].ToString();
                dr1["12月"] = data1[11].ToString();

                dt1.Rows.Add(dr1);

                System.Data.DataRow dr2 = dt1.NewRow();
                dr2["FeeKind"] = sListKind[1].Trim();
                dr2["1月"] = data2[0].ToString();
                dr2["2月"] = data2[1].ToString();
                dr2["3月"] = data2[2].ToString();
                dr2["4月"] = data2[3].ToString();
                dr2["5月"] = data2[4].ToString();
                dr2["6月"] = data2[5].ToString();
                dr2["7月"] = data2[6].ToString();
                dr2["8月"] = data2[7].ToString();
                dr2["9月"] = data2[8].ToString();
                dr2["10月"] = data2[9].ToString();
                dr2["11月"] = data2[10].ToString();
                dr2["12月"] = data2[11].ToString();

                dt1.Rows.Add(dr2);

                System.Data.DataRow dr3 = dt1.NewRow();
                dr3["FeeKind"] = sListKind[2].Trim();
                dr3["1月"] = data3[0].ToString();
                dr3["2月"] = data3[1].ToString();
                dr3["3月"] = data3[2].ToString();
                dr3["4月"] = data3[3].ToString();
                dr3["5月"] = data3[4].ToString();
                dr3["6月"] = data3[5].ToString();
                dr3["7月"] = data3[6].ToString();
                dr3["8月"] = data3[7].ToString();
                dr3["9月"] = data3[8].ToString();
                dr3["10月"] = data3[9].ToString();
                dr3["11月"] = data3[10].ToString();
                dr3["12月"] = data3[11].ToString();

                dt1.Rows.Add(dr3);

                System.Data.DataRow dr4 = dt1.NewRow();
                dr4["FeeKind"] = sListKind[3].Trim();
                dr4["1月"] = data4[0].ToString();
                dr4["2月"] = data4[1].ToString();
                dr4["3月"] = data4[2].ToString();
                dr4["4月"] = data4[3].ToString();
                dr4["5月"] = data4[4].ToString();
                dr4["6月"] = data4[5].ToString();
                dr4["7月"] = data4[6].ToString();
                dr4["8月"] = data4[7].ToString();
                dr4["9月"] = data4[8].ToString();
                dr4["10月"] = data4[9].ToString();
                dr4["11月"] = data4[10].ToString();
                dr4["12月"] = data4[11].ToString();

                dt1.Rows.Add(dr4);

                this.Repeater1.DataSource = dt1;
                this.Repeater1.DataBind();
                //FeeTitle.Text = String.Format("数据表", TrainID, byear);
            }
        }
    }
}
