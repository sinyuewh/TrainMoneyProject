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
    public partial class UserEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.button1.Click += new EventHandler(button1_Click);
            base.OnInit(e);
        }

        void button1_Click(object sender, EventArgs e)
        {
            if (FrameLib.CheckData(button1))
            {
                JTable tab1 = new JTable("MYUSERNAME");
                Control[] con1 = new Control[] { num, password, UserName };
                Dictionary<String, object> data1 = JControl.GetControlValuesToDictionary(con1);
                List<SearchField> condition = new List<SearchField>();
                condition.Add(new SearchField("UserName", this.UserName.Text));
                if (this.isAdmin.Checked)
                {
                    data1.Add("isAdmin", "1");
                }
                else
                {
                    data1.Add("isAdmin", "0");
                }
                tab1.EditData(data1, condition, true);
                tab1.Close();

                //更新角色中的相关数据
                JTable tab2 = new JTable();
                tab2.TableName = "JROLEUSERS";
                condition.Clear();
                condition.Add(new SearchField("RoleID", "001"));
                condition.Add(new SearchField("UserID", this.UserName.Text.Trim()));
                DataTable dt1 = tab2.SearchData(condition, -1, "*").Tables[0];
                if (this.isAdmin.Checked)
                {
                    if (dt1.Rows.Count == 0)
                    {
                        DataRow dr1 = dt1.NewRow();
                        dr1["num"] = 1;
                        dr1["id"] = WebFrame.Util.JString.GetUnique32ID();
                        dr1["userid"] = this.UserName.Text.Trim();
                        dr1["roleid"] = "001";
                        dt1.Rows.Add(dr1);
                        tab2.Update(dt1);
                    }
                }
                else
                {
                    if (dt1.Rows.Count > 0)
                    {
                        DataRow dr1 = dt1.Rows[0];
                        dr1.Delete();
                        tab2.Update(dt1);
                    }
                }
                tab2.Close();
                JAjax.AlertAndGoUrl("提示：操作成功", button1.UrlReferrer);
            }
        }
    }
}
