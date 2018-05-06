using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebFrame;
using WebFrame.Data;
using WebFrame.Designer;
using WebFrame.ExpControl;
using WebFrame.Util;
using BusinessRule;

namespace WebSite
{
    public partial class NewLogin : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            this.button1.Click += new ImageClickEventHandler(button1_Click);
            base.OnInit(e);
        }

        void button1_Click(object sender, ImageClickEventArgs e)
        {
            //System.Web.Security.FormsAuthentication.RedirectFromLoginPage(this.UserID.Text, false);

            String ip1 = KIPBU.GetIPs();

            if (String.IsNullOrEmpty(ip1) == false)
            {
                String[] ips = ip1.Split(',');
                List<String> list1 = new List<string>();
                foreach (String m in ips)
                {
                    if (list1.Contains(m) == false)
                    {
                        list1.Add(m);
                    }
                }
             
                String soureceIP = Request.UserHostAddress;
                if (list1.Contains(soureceIP))
                {
                   if (String.IsNullOrEmpty(this.UserID.Text)==false
                        && String.IsNullOrEmpty(this.PassWord.Text)==false)
                    {
                        
                        MyUserName user1 = new MyUserName();
                        bool succ = user1.Login(this.UserID.Text, this.PassWord.Text);
                        if (succ)
                        {
                            FrameLib.UserID = this.UserID.Text;
                            FrameLib.DepartID = "DepartID";

                           

                            //基础数据初始化
                            //基础数据发生变化时，重写这些数据；

                            ChexianBianZhuData.Init();
                            LiChengProfile.Init();
                            LiChengJianRate.Init();
                            JiaKuaiProfile.Init();
                            LineProfile.Init();
                            // QianYinFeeProfile.Init();
                            CheXianProfile.Init();
                            HighTrainProfile.Init();
                            TrainLineKindProfile.Init();
                            TrainProfile.Init();
                            PersonGZProfile.Init();
                            RiChangFeeProfile.Init();
                            A2A3FeeProfile.Init();

                            //调整夏冬切换的线路
                            TrainLine.ExchangeSpringAndWinter();

                            //处理系统的升级
                            AppCode.Upgrade.Go();

                            Response.Redirect("TrainWeb/MainFrame.aspx", true);
                        }
                        else
                        {
                            JAjax.Alert("错误：用户名或密码不正确！");
                        }
                    }
                }
                else
                {
                    Response.Redirect("/GenericErrorPage.htm", true);
                }
            }
            else
            {
                Response.Redirect("/GenericErrorPage.htm", true);
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               String username1= JCookie.GetCookieValue("CurrentLogin");
               this.UserID.Text = username1;
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            WebFrame.Data.JConnect.CloseConnect();
            base.OnUnload(e);
        }
    }
}
