using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessRule;
using WebFrame.Designer;
using WebFrame.Data;
using WebFrame.Util;
using WebFrame;

namespace WebSite.TrainWeb.SystemProfile
{
    public partial class Fee18_JianXiuFeeRate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.Fee1.Text = JStrInfoBU.GetStrTextByID("检修费率系数");
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.button1.Click += new EventHandler(button1_Click);
            base.OnInit(e);
        }

        void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double d1 = double.Parse(this.Fee1.Text);
                if (d1 <= 1.0)
                {
                    JStrInfoBU bu1 = new JStrInfoBU();
                    List<SearchField> condition = new List<SearchField>();
                    condition.Add(new SearchField("StrID", "检修费率系数"));
                    Dictionary<String, object> data1 = new Dictionary<string, object>();
                    data1["StrText"] = this.Fee1.Text;
                    bu1.UpdateData(condition, data1);

                    BusinessRule.TrainProfile.SetData();
                    WebFrame.Util.JAjax.Alert("提示：更新数据操作成功！");
                }
                else
                {
                    WebFrame.Util.JAjax.Alert("提示：此数据不能大于1！");
                }
            }
            catch (Exception err)
            {
                WebFrame.Util.JAjax.Alert("提示：数据类型错误，请重新输入！");
            }

        }
    }
}
