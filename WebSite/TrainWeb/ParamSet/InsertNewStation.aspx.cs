using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessRule;
using System.Data;
using WebFrame.Util;
using WebFrame.Data;

namespace WebSite.TrainWeb.ParamSet
{
    public partial class InsertNewStation : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            this.button1.Click += new EventHandler(button1_Click);
            this.btnSubmit.Click += new EventHandler(btnSubmit_Click);
            base.OnInit(e);
        }

        protected void txtbegin_TextChanged(object sender, EventArgs e)
        {
            int temp = 0;
            this.lblbegin.Text = this.txt1.Text;
            this.lblmid.Text = this.newstation.Text;
            this.lblmiddle.Text = this.newstation.Text;
            this.ldllast.Text = this.txt2.Text;

            if (this.txtbegin.Text.Trim() == "")
            {
                JAjax.Alert(string.Format("{0} 至{1}的距离不能为空！", this.lblbegin.Text, this.lblmid.Text));
                return;
            }

            if (int.TryParse(this.txtbegin.Text.Trim(), out temp) == false)
            {
                JAjax.Alert(string.Format("{0} 至{1}的距离必须是整数！", this.lblbegin.Text, this.lblmid.Text));
                return;
            }

            NewTrainBU bu = new NewTrainBU();
            DataRow dr = bu.GetDataRowFromLineStation(this.txt1.Text.Trim(), this.txt2.Text.Trim(),"0");//在LineStation表中得到Astation ，bstation的值等于选中路线值的lineid,注意方向Directory=0的情况

            if(dr == null)
            {
                JAjax.Alert(string.Format("不存在{0} 至{1}的直通线路，中间已存在其它站点！", this.txt1.Text.Trim(), this.txt2.Text.Trim()));
                return;
            }

            if (Convert.ToInt32(this.txtbegin.Text.ToString().Trim()) > Convert.ToInt32(dr["Miles"]))
            {
                JAjax.Alert(string.Format("{0} 至{1}的距离已大于{2}到{3}的距离！", this.lblbegin.Text, this.lblmid.Text, this.lblbegin.Text, this.ldllast.Text));
                return;
            }

            if (dr !=null)
            {
                txtlast.Text = (Convert.ToInt32(dr["Miles"]) - Convert.ToInt32(txtbegin.Text.ToString())).ToString();
            }
            
        }

        //提交数据,插入站点
        void btnSubmit_Click(object sender, EventArgs e)
        {
            //初始化
            this.lblbegin.Text = this.txt1.Text;
            this.lblmid.Text = this.newstation.Text;
            this.lblmiddle.Text = this.newstation.Text;
            this.ldllast.Text = this.txt2.Text;

            int temp = 0;
            if (this.lblbegin.Text.ToString().Trim() == this.lblmid.Text.ToString().Trim())
            {
                JAjax.Alert(string.Format("新站点{0} 与起始站点{1}同名！", this.lblbegin.Text, this.lblmid.Text));
                return;
            }
            if (this.lblmiddle.Text.ToString().Trim() == this.ldllast.Text.ToString().Trim())
            {
                JAjax.Alert(string.Format("新站点{0} 与到达站点{1}同名！", this.lblmiddle.Text, this.ldllast.Text));
                return;
            }
            if (int.TryParse(this.txtbegin.Text.Trim(), out temp) == false)
            {
                JAjax.Alert(string.Format("{0} 至{1}的距离必须是整数！",this.lblbegin.Text,this.lblmid.Text));
                return;
            }
            else if (int.TryParse(this.txtlast.Text.Trim(), out temp) == false)
            {

                JAjax.Alert(string.Format("{0} 至{1}的距离必须是整数！", this.lblmiddle.Text, this.ldllast.Text));
                return;
            }
            else
            {
                NewTrainBU bu = new NewTrainBU();
                DataRow dr = bu.GetDataRowFromLineStation(this.txt1.Text.Trim(), this.txt2.Text.Trim(),"0");//在LineStation表中得到Astation ，bstation的值等于选中路线值的lineid,注意方向Directory=0的情况
                if (dr ==null)
                {
                    JAjax.Alert("该站点路线不存在！");
                    return;

                }
                if(Convert.ToInt32(this.txtbegin.Text)+Convert.ToInt32(txtlast.Text)!=Convert.ToInt32(dr["Miles"]))
                {
                    JAjax.Alert(string.Format("{0} 至{1}的距离与{2}至{3}的距离之和与{4}到{5}的距离不相等！", this.lblbegin.Text, this.lblmid.Text,this.lblmiddle.Text,this.ldllast.Text,this.lblbegin.Text,this.ldllast.Text));
                    return;
                }
                else
                {
                    JConnect conn = JConnect.GetConnect();
                    conn.BeginTrans();
                    try
                    {
                        if (dr != null)
                        {
                            InsertLineStation(dr, "0");//direction=0的插入操作
                        }
                        DataRow dr1 = bu.GetDataRowFromLineStation(this.txt2.Text.Trim(), this.txt1.Text.Trim(), "1");
                        if (dr1 != null)
                        {
                            InsertLineStation(dr1, "1");//direction=1的插入操作
                        }
                        conn.CommitTrans();
                        JAjax.Alert("提交成功！");
                    }
                    catch(Exception ex)
                    {
                        conn.RollBackTrans();
                        JAjax.Alert("提交失败！");
                    }
                }
            }
        }

        //查询站点路线
        void button1_Click(object sender, EventArgs e)
        {
            bind();
           
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        private void bind() 
        {
            NewTrainBU bu = new NewTrainBU();
            DataTable dt=bu.GetNextLineList(this.txt1.Text.Trim(), this.txt2.Text.Trim(), "0");
            this.Repeater1.DataSource = dt;
            this.Repeater1.DataBind();
        }
        /// <summary>
        /// 在linestation中完成插入站点功能
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="direction"></param>
        private void InsertLineStation(DataRow dr,string direction) 
        {
            NewTrainBU bu = new NewTrainBU();
            string id = dr["id"].ToString();
            int num = Convert.ToInt32(dr["num"]);//得到排序号
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("bstation", this.newstation.Text.Trim());
            dic.Add("miles", this.txtbegin.Text.Trim());
            bu.EditLineStation(dic, id);

            //更改Num值
            string lineid = dr["lineid"].ToString();
            DataTable dtNums = bu.GetNumsLineStation(lineid, num.ToString(), direction);
            List<SearchField> condition = new List<SearchField>();
            foreach (DataRow drNum in dtNums.Rows)
            {
                dic.Clear();
                condition.Clear();
                dic.Add("num", Convert.ToInt32(drNum["num"]) + 1);
                condition.Add(new SearchField("Id", drNum["id"].ToString()));
                bu.EditLineStation(dic, condition);
            }
            //新增
            dic.Clear();
            dic.Add("astation", this.newstation.Text.Trim());
            dic.Add("bstation", dr["bstation"].ToString());
            dic.Add("lineid",dr["lineid"].ToString());
            dic.Add("miles", this.txtlast.Text.Trim());
            dic.Add("num", (num + 1).ToString());
            dic.Add("direction",direction);
            bu.NewLineStation(dic);
        
        }
    }
}
