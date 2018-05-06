using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessRule;

namespace WebSite.TrainWeb.Test
{
    public partial class Test2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                /*
                Line line1 = new Line("A0", "D0");
                List<Line> allList = new List<Line>();
                allList.Add(line1);

                allList=Line.AddStation(allList);
                foreach (Line m in allList)
                {
                    Response.Write(m.ToString() + "<br>");
                }*/
            }
        }
    }
}
