using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebFrame.Util;
using WebFrame.Data;
using WebFrame;
using System.Data;

namespace WebSite.TrainWeb.SystemProfile
{
    public partial class UserList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            this.Repeater1.ItemCommand += new RepeaterCommandEventHandler(Repeater1_ItemCommand);
            base.OnInit(e);
        }

        void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            String userid = e.CommandArgument.ToString();
            if (String.IsNullOrEmpty(userid) == false)
            {
                JTable tab1 = new JTable("MYUSERNAME");
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("UserName", userid));
                tab1.DeleteData(condition);
                tab1.Close();

                //更新角色中的相关数据
                JTable tab2 = new JTable();
                tab2.TableName = "JROLEUSERS";
                condition.Clear();
                condition.Add(new SearchField("RoleID", "001"));
                condition.Add(new SearchField("UserID", userid));
                tab2.DeleteData(condition);
                tab2.Close();
                this.Repeater1.DataBind();
            }
        }
    }
}
