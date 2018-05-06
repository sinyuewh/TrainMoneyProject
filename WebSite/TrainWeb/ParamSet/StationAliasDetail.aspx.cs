using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebFrame.Util;
using WebFrame.Data;

namespace WebSite.TrainWeb.ParamSet
{
    public partial class StationAliasDetail : System.Web.UI.Page
    {
        Control[] con1 = null; 
        protected void Page_Load(object sender, EventArgs e)
        {
            con1 = new Control[] { num,TrainName,TrainAlias};
        }

        protected override void OnInit(EventArgs e)
        {
            this.button1.Click += new EventHandler(button1_Click);
            base.OnInit(e);
        }

        void button1_Click(object sender, EventArgs e)
        {
            if (WebFrame.FrameLib.CheckData(this.button1))
            {
                this.TrainAlias.Text = this.TrainAlias.Text.Replace("，", ",");
                Dictionary<String,object> data1= JControl.GetControlValuesToDictionary(con1);
                JTable tab1 = new JTable("TRAINALIAS");
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("TRAINNAME",Request.QueryString["TRAINNAME"]));
                tab1.EditData(data1, condition, true);
            }
        }

    }
}
