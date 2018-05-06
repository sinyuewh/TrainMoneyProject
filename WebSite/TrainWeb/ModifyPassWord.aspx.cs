using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;
using WebFrame;
using WebFrame.Data;
using WebFrame.Util;
using WebFrame.ExpControl;
using BusinessRule;


namespace OASystemWeb.SysMng
{
    public partial class ModifyPassWord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //提交密码修改
        protected void but1_Click(object sender, EventArgs e)
        {
            if(FrameLib.CheckData(this.but1))
            {
                MyUserName  bu1 = new MyUserName();
                bool succ = bu1.UpdatePassWord(Page.User.Identity.Name, this.oldPass.Text, this.password1.Text);
                if (succ == false)
                {
                    JAjax.Alert("错误：老密码输入不正确，请检查后重新输入！");
                }
                else
                {
                    JAjax.AlertAndGoUrl("提示：密码已成功修改，你需要重新登录！", "/SignOut.aspx");
                }
            }
        }
    }
}
