using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessRule;

namespace WebSite.TrainWeb.Fenxi
{
    public partial class OldCheCiFenxi : System.Web.UI.Page
    {
        //private double Rate = 0.9676;
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BindData();
            }
            base.OnLoad(e);
        }
        protected override void OnInit(EventArgs e)
        {
            this.repeater1.ItemDataBound += new RepeaterItemEventHandler(repeater1_ItemDataBound);
            this.Button1.Click += new EventHandler(Button1_Click);
            base.OnInit(e);
        }

        void Button1_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        //绑定数据计算收入和支出
        void repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            double SRate = SRateProfileBU.GetRate(int.Parse(this.byear.Text));

            Label lab1 = e.Item.FindControl("labtrainType") as Label;
            String trainType = lab1.Text;
            if (String.IsNullOrEmpty(trainType) == false)
            {
                NewTrainBU bu1 = new NewTrainBU();
                List<NewTrainData> list1 = bu1.GetTrainListByType(trainType);
                int person1 = 0;
                double zc1 = 0;
                bu1.GetTrainPersonAndMoney(list1, out person1, out zc1);
                zc1 = zc1 / 10000;

                //设置当月的理论人数和理论收入
                Label lab2 = e.Item.FindControl("labPerson") as Label;
                lab2.Text = person1 + "";
                Label lab3 = e.Item.FindControl("labShouRou") as Label;
                lab3.Text = String.Format("{0:n0}", zc1);

                //计算当月的累计人数和累计收入数
                Label lab21 = e.Item.FindControl("labPerson1") as Label;
                lab21.Text = person1 * int.Parse(this.bmonth.Text) + "";
                Label lab31 = e.Item.FindControl("labShouRou1") as Label;
                lab31.Text = String.Format("{0:n0}", zc1 * int.Parse(this.bmonth.Text));

                //计算实际值的当月收入和累计收入
                String name1 = bu1.GetTrainNameListByKind(trainType);
                Label labfactshour = e.Item.FindControl("labShouRou2") as Label;
                bu1.GetTrainFactPersonAndMoney(name1, int.Parse(this.byear.Text),
                    int.Parse(this.bmonth.Text), out person1, out zc1);
                zc1 = zc1 / 10000;
                labfactshour.Text = String.Format("{0:n0}", zc1);
                Label labPerson2 = e.Item.FindControl("labPerson2") as Label;
                labPerson2.Text = person1 + "";

                //计算累计运输的人数
                Label labfactshour2 = e.Item.FindControl("labShouRou3") as Label;
                bu1.GetTrainFactSumPersonAndMoney(name1, int.Parse(this.byear.Text),
                    int.Parse(this.bmonth.Text), out person1, out zc1);
                zc1 = zc1 / 10000;
                labfactshour2.Text = String.Format("{0:n0}", zc1);
                Label labPerson3 = e.Item.FindControl("labPerson3") as Label;
                labPerson3.Text = person1 + "";

                //计算当月支持和累计支出
                int year1=int.Parse(this.byear.Text);
                int month1=int.Parse(this.bmonth.Text);
                double z1=0,z2=0;
                NewTrainZhiChuBU.GetTrainZhiChuByKind(trainType, year1, month1, out z1, out z2);
                Label labZc1 = e.Item.FindControl("zhichu1") as Label;
                Label labZc2 = e.Item.FindControl("zhichu2") as Label;
                labZc1.Text = String.Format("{0:n0}", z1);
                labZc2.Text = String.Format("{0:n0}", z2);

                //计算盈亏
                double temp1 = double.Parse((e.Item.FindControl("labShouRou2") as Label).Text);
                double temp2 = double.Parse((e.Item.FindControl("labShouRou3") as Label).Text);
                double temp3 = double.Parse((e.Item.FindControl("zhichu1") as Label).Text);
                double temp4 = double.Parse((e.Item.FindControl("zhichu2") as Label).Text);

                (e.Item.FindControl("yk1") as Label).Text = String.Format("{0:n0}", temp1*SRate-temp3);
                (e.Item.FindControl("yk2") as Label).Text = String.Format("{0:n0}", temp2*SRate-temp4);
            }
        }


        private void BindData()
        {
            String[] arr1 = new String[] { "动车组", "空调特快", "空调快速", "空调普快", "快速", "普快" };
            this.repeater1.DataSource = arr1;
            this.repeater1.DataBind();
        }
    }
}
