using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessRule;
using System.Reflection;
using System.Data;
using WebFrame.Util;
using WebFrame;
using org.in2bits.MyXls;
using BusinessRule.PubCode;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace WebSite.TrainWeb
{
    public partial class SelectTrainLine : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.AStation.Attributes["ReadOnly"] = "true";
                this.BStation.Attributes["ReadOnly"] = "true";
                this.AStation.Text = Request.QueryString["astation"];
                this.BStation.Text = Request.QueryString["bstation"];
                this.Button1.Attributes.Add("onclick", "ShowWaiting();");

            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.Button1.Click += new EventHandler(Button1_Click);
            base.OnInit(e);
        }

        void Button1_Click(object sender, EventArgs e)
        {
            this.SearchInfo.Visible = true;
            this.SearchLine();
        }

        #region Privae 方法
        // 分析线路
        private void SearchLine()
        {           
            String A0 = this.AStation.Text.Trim();
            String B0 = this.BStation.Text.Trim();
            if (String.IsNullOrEmpty(A0) == false
                && String.IsNullOrEmpty(B0) == false)
            {
                bool flag = true;
                if (Line.isExistsStation(A0) == false)
                {
                    WebFrame.Util.JAjax.Alert("错误：起始站【" + A0 + "】不存在！");
                    flag = false;
                }
                if (flag)
                {
                    if (Line.isExistsStation(B0) == false)
                    {
                        WebFrame.Util.JAjax.Alert("错误：终点站【" + B0 + "】不存在！");
                        flag = false;
                    }
                }
                if (flag)
                {
                    String traintype=Request.QueryString["traintype"];
                    ETrainType type1=ETrainType.空调车25T;

                    if(traintype=="动车组")
                    {
                        type1=ETrainType.动车CRH2A;
                    }
                    else if(traintype =="空调特快")
                    {
                        type1 =ETrainType.空调车25T;
                    }
                    else if(traintype =="空调快速")
                    {
                        type1 =ETrainType.空调车25G;
                    }
                    else if(traintype =="空调普快")
                    {
                        type1=ETrainType.空调车25G;
                    }
                    else if(traintype=="快速")
                    {
                        type1 =ETrainType.空调车25K;
                    }
                    else if(traintype =="普快")
                    {
                        type1 =ETrainType.绿皮车25B;
                    }

                    String[] arr1 = null;
                    if (this.mulLine.Text.Trim() == String.Empty)
                    {
                        arr1=Line.GetLineArr(this.AStation.Text, this.BStation.Text, type1, this.middleStation.Text,false);
                    }
                    else
                    {
                        arr1 = Line.GetLineArrByFengDuanSearch(this.AStation.Text,this.BStation.Text,
                            this.mulLine.Text.Trim(), type1, this.middleStation.Text,false);
                    }

                    if(arr1!=null)
                    {
                        this.Repeater1.DataSource = arr1;
                        this.Repeater1.DataBind();
                    }
                    else
                    {
                         WebFrame.Util.JAjax.Alert("错误：没有搜索到合适的线路！");
                    }
                }
            }
            else
            {
                WebFrame.Util.JAjax.Alert("错误：请输入车次的起点和终点");
            }
        }

        #endregion
    }
}
