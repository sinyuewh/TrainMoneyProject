using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessRule;
using WebFrame.Util;

namespace WebSite.TrainWeb.ParamSet
{
    public partial class JoinStation : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            this.button1.Click += new EventHandler(button1_Click);
            this.button2.Click += new EventHandler(button2_Click);
            base.OnInit(e);
        }
        //删除站点和更新路线
        void button2_Click(object sender, EventArgs e)
        {
            /// <summary>
            /// 删除线路中的站点
            /// 说明：根据站点的名称，找到合适的线路（同上）
            /// 根据线路LineStation中Direction=0 和 Direction=1的两种情况分别按num进行排序
            /// 将线路中LINESTATION中 的所有站点数据 按下面的规则进行调整
            /// 如果首站点的 第一条数据的AStation=StationName，则删除该数据，并重新调整编号num
            /// 如果是最后一站，则判断BStation ,如果相等，则直接删除
            /// 如果中间站点 AStation 不等于 StationName，则只比较BStation中的数据是否和StationName相等
            /// 如果相同，则删除该数据，并将下一个数据的AStation改成该条数据的AStation
            /// 对站点处理完成后，要及时退出循环，避免做无用的循环数据
            /// 要分别对Direction=0 和 Direction=1的处理。
            /// NEWTRAIN中的数据调整比较简单，只要把Line中的 武汉- 和 -武汉 替换成空字符串就可以了。
            /// 此操作比较重要，要使用事务处理。
            /// </summary>

            if (int.Parse(this.scount.Value) > 0)
            {
                NewTrainBU bu = new NewTrainBU();
                bool flag = bu.DeleteTrainStation(this.txt1.Text.Trim());
                if (flag)
                {
                    JAjax.Alert("删除成功");
                }
                else
                {
                    JAjax.Alert("删除失败");
                }
            }
            else
            {
                JAjax.Alert("删除的站点不存在！");
            }
        }

        void button1_Click(object sender, EventArgs e)
        {
            this.Repeater1.DataSource =GetSearchLine(this.txt1.Text.Trim());
            this.Repeater1.DataBind();
            this.scount.Value  = this.Repeater1.Items.Count + "";
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 根据站点的名称查询合适的线路
        /// 说明：在表 LINESTATION中根据AStation或BStation中查询满足条件的线路LineID (Direction=0)
        /// 根据 LineID 在表TrainLine中查询到合适的线路数据 
        /// 使用 Exists 查询比较合适
        /// select * from TrainLine where exists (select 1 from LINESTATION where lineid=TrainLine.lineid and
        ///         direction=0 and (Astation='wh' or BStation='wh' ) )
        /// </summary>
        /// <param name="StationName">输入的站点名称</param>
        /// <returns>返回保护该站点名称的线路</returns>
        private DataTable GetSearchLine(String StationName)
        {
            NewTrainBU bu = new NewTrainBU();
            return bu.GetLineList(StationName,"0");
            
        }
      
    }
}
