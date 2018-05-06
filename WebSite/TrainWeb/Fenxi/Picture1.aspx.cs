using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSite.TrainWeb.Fenxi
{
    public partial class Picture1 : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
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

                double max1 = Math.Max(data2[data2.Length - 1], data4[data4.Length - 1]);
                int kd = (int)(Math.Ceiling(max1 * 1.1)) / 10;

                if (data2 != null && data2.Length > 0 && data2[data2.Length - 1] > 0)
                {
                    string[] bz = null;
                    BusinessRule.NewTrainBU.GetTrainBianZhuByYear(TrainID, byear, out bz);

                    WebSite.AppCode.MyPicture picture1 = new WebSite.AppCode.MyPicture();
                    int height1 = picture1.Height;
                    picture1.Width = 690;
                    picture1.Height = 650;
                    picture1.AxisHeight = 400;

                    picture1.BorderColor = System.Drawing.Color.Blue;
                    picture1.TextColor = System.Drawing.Color.Blue;
                    picture1.BorderColor = System.Drawing.Color.Empty;
                    picture1.TitlePosX = 200;
                    picture1.TitlePosY = 20;
                    picture1.OriginY = picture1.Height - 180;
                    picture1.UnitLabel = "万元";
                    picture1.Title = String.Format("武汉铁路局{0}车次{1}年收入/累计收入折线图", TrainID, byear);


                    picture1.InitDrawPicture();
                    picture1.DrawCoordAxis(bz, new int[] { kd, 2 * kd, 3 * kd, 4 * kd, 5 * kd, 6 * kd, 7 * kd, 8 * kd, 9 * kd, 10 * kd });


                    picture1.DrawLineChart(data1, System.Drawing.Color.Blue, "当月收入");
                    picture1.DrawLineChart(data2, System.Drawing.Color.Red, "累计收入");
                    picture1.DrawLineChart(data3, System.Drawing.Color.Green, "当月支出");
                    picture1.DrawLineChart(data4, System.Drawing.Color.Black, "累计支出");

                    //绘制说明框
                    picture1.DrawLineText(60, 510, 250, 90, System.Drawing.Color.Blue);
                    picture1.InputPictureToScreen();
                    picture1.Dispose();
                }
            }
            //WebSite.AppCode.JPicture.CreateImage3();
            base.OnLoad(e);
        }
    }
}
