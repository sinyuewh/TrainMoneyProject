using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using System.Data.OleDb;
using WebFrame.Data;
using org.in2bits.MyXls;

namespace WebSite.TrainWeb.Train
{
    public partial class NewTrainList : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            this.LinkCheckLine.Click += new EventHandler(LinkCheckLine_Click);
            base.OnInit(e);
        }


        //进行线路检测
        void LinkCheckLine_Click(object sender, EventArgs e)
        {
            int result=BusinessRule.NewTrainBU.CheckAllLine();
            this.Repeater1.DataBind();
            WebFrame.Util.JAjax.Alert(String.Format("提示：一共发现了{0}条错误的线路配置！", result));
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void ExportExcel(DataTable dt, string filename)
        {
            XlsDocument xls = new XlsDocument();//新建一个xls文档
            xls.FileName = filename;//设定文件名
            string sheetName = "列车信息表";
            Worksheet sheet = xls.Workbook.Worksheets.Add(sheetName);//填加名为"chc 实例"的sheet页
            Cells cells = sheet.Cells;//Cells实例是sheet页中单元格（cell）集合


            ////设置列的宽度
            ColumnInfo colInfo = new ColumnInfo(xls, sheet);//生成列格式对象
            //设定colInfo格式的起作用的列为第2列到第5列(列格式为0-base)
            colInfo.ColumnIndexStart = 0;//起始列为第一列,ColumnIndexStart是从0开始
            colInfo.Width = 15 * 256;//列的宽度计量单位为 1/256 字符宽
            sheet.AddColumnInfo(colInfo);//把格式附加到sheet页上（注：AddColumnInfo方法有点小问题，不给把colInfo对象多次附给sheet页）

            //列样式
            XF cellXFColumnText = xls.NewXF();
            cellXFColumnText.HorizontalAlignment = HorizontalAlignments.Centered;//设定文字居中
            cellXFColumnText.Font.FontName = "宋体";//设定字体
            cellXFColumnText.Font.Height = 10 * 20;//设定字大小（字体大小是以 1/20 point 为单位的）
            cellXFColumnText.Pattern = 1;//设定单元格填充风格。如果设定为0，则是纯色填充
            cellXFColumnText.PatternBackgroundColor = Colors.Grey;//填充的底色
            cellXFColumnText.PatternColor = Colors.Grey;//设定填充线条的颜色
            cellXFColumnText.BottomLineStyle = 1;//设定边框底线为粗线
            cellXFColumnText.BottomLineColor = Colors.Grey;
            cellXFColumnText.TopLineStyle = 1;//设定边框底线为粗线
            cellXFColumnText.TopLineColor = Colors.Grey;
            cellXFColumnText.LeftLineStyle = 1;
            cellXFColumnText.LeftLineColor = Colors.Grey;
            cellXFColumnText.RightLineStyle = 1;
            cellXFColumnText.RightLineColor = Colors.Grey;

            cells.Add(1, 1, "序号", cellXFColumnText);//设定第一行，第一列单元格的值
            cells.Add(1, 2, "车次", cellXFColumnText);//设定第一行，第二列单元格的值
            cells.Add(1, 3, "始发站", cellXFColumnText);//设定第一行，第三列单元格的值
            cells.Add(1, 4, "终到站", cellXFColumnText);//设定第一行，第四列单元格的值
            cells.Add(1, 5, "列车类型", cellXFColumnText);
            cells.Add(1, 6, "单程距离", cellXFColumnText);
            cells.Add(1, 7, "开行趟数", cellXFColumnText);
            cells.Add(1, 8, "车底组数", cellXFColumnText);
            cells.Add(1, 9, "硬座", cellXFColumnText);
            cells.Add(1, 10, "软座", cellXFColumnText);
            cells.Add(1, 11, "硬卧", cellXFColumnText);
            cells.Add(1, 12, "软卧", cellXFColumnText);
            cells.Add(1, 13, "餐车", cellXFColumnText);
            cells.Add(1, 14, "发电车", cellXFColumnText);
            cells.Add(1, 15, "行李车", cellXFColumnText);
            cells.Add(1, 16, "邮政车", cellXFColumnText);
            //列值样式
            XF cellXFColumnValue = xls.NewXF();
            cellXFColumnValue.HorizontalAlignment = HorizontalAlignments.Centered;//设定文字居中
            cellXFColumnValue.Font.FontName = "宋体";//设定字体
            cellXFColumnValue.Font.Height = 10 * 20;//设定字大小（字体大小是以 1/20 point 为单位的）
            cellXFColumnValue.UseBorder = true;//使用边框
            cellXFColumnValue.BottomLineStyle = 1;//设定边框底线为粗线
            cellXFColumnValue.BottomLineColor = Colors.Grey;
            cellXFColumnValue.TopLineStyle = 1;//设定边框底线为粗线
            cellXFColumnValue.TopLineColor = Colors.Grey;
            cellXFColumnValue.LeftLineStyle = 1;
            cellXFColumnValue.LeftLineColor = Colors.Grey;
            cellXFColumnValue.RightLineStyle = 1;
            cellXFColumnValue.RightLineColor = Colors.Grey;

            int currentRow = 2;
            foreach (DataRow dr in dt.Rows)
            {
                cells.Add(currentRow, 1, Convert.ToString(dr["num"]), cellXFColumnValue);
                cells.Add(currentRow, 2, Convert.ToString(dr["TRAINNAME"]), cellXFColumnValue);
                cells.Add(currentRow, 3, Convert.ToString(dr["ASTATION"]), cellXFColumnValue);
                cells.Add(currentRow, 4, Convert.ToString(dr["BSTATION"]), cellXFColumnValue);
                cells.Add(currentRow, 5, Convert.ToString(dr["TRAINTYPE"]), cellXFColumnValue);
                cells.Add(currentRow, 6, Convert.ToString(dr["MILE"]), cellXFColumnValue);
                cells.Add(currentRow, 7, Convert.ToString(dr["KXTS"]), cellXFColumnValue);
                cells.Add(currentRow, 8, Convert.ToString(dr["CDZS"]), cellXFColumnValue);
                cells.Add(currentRow, 9, Convert.ToString(dr["YINZUO"]), cellXFColumnValue);
                cells.Add(currentRow, 10, Convert.ToString(dr["RUANZUO"]), cellXFColumnValue);
                cells.Add(currentRow, 11, Convert.ToString(dr["OPENYINWO"]), cellXFColumnValue);
                cells.Add(currentRow, 12, Convert.ToString(dr["RUANWO"]), cellXFColumnValue);
                cells.Add(currentRow, 13, Convert.ToString(dr["CANCHE"]), cellXFColumnValue);
                cells.Add(currentRow, 14, Convert.ToString(dr["FADIANCHE"]), cellXFColumnValue);
                cells.Add(currentRow, 15, Convert.ToString(dr["SHUYINCHE"]), cellXFColumnValue);
                cells.Add(currentRow, 16, Convert.ToString(dr["YOUZHENGCHE"]), cellXFColumnValue);
                currentRow++;
            }
            xls.Send(XlsDocument.SendMethods.Inline);
        }


        protected void ImportExcel(string FilePath,string SheetName)
        {
            JTable tab = new JTable("NEWTRAIN");
            JTable tab1 = new JTable("NEWTRAIN");

            String FileName1 = HttpContext.Current.Server.MapPath( FilePath);
            if (File.Exists(FileName1))
            {
                DataSet ds1 = BusinessRule.PubCode.Util.xsldata(Server.MapPath(FilePath), SheetName);
                List<SearchField> condition = new List<SearchField>();
                Dictionary<string, object> data = new Dictionary<string, object>();
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    int n=ds1.Tables[0].Columns.Count;
                    JCommand cmd = new JCommand(JConnect.GetConnect());
                    cmd.CommandText = "select max(num) from NEWTRAIN";
                    int num =Convert.ToInt32( cmd.ExecuteScalar());
                    cmd.Close();
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        condition.Clear();
                        condition.Add(new SearchField("TRAINNAME", ds1.Tables[0].Rows[i]["车次"].ToString()));
                        data.Clear();
                        data.Add("TRAINNAME", ds1.Tables[0].Rows[i]["车次"].ToString());
                        data.Add("ASTATION", ds1.Tables[0].Rows[i]["始发站"].ToString());
                        data.Add("BSTATION", ds1.Tables[0].Rows[i]["终到站"].ToString());
                        data.Add("TRAINBIGKIND", 0);
                        data.Add("TRAINTYPE", ds1.Tables[0].Rows[i]["列车类型"].ToString());
                        data.Add("MILE", ds1.Tables[0].Rows[i]["单程距离"].ToString());
                        data.Add("KXTS", ds1.Tables[0].Rows[i]["开行趟数"].ToString());
                        data.Add("CDZS", ds1.Tables[0].Rows[i]["车底组数"].ToString());
                        data.Add("YINZUO", ds1.Tables[0].Rows[i]["硬座"].ToString());
                        data.Add("RUANZUO", ds1.Tables[0].Rows[i]["软座"].ToString());
                        data.Add("OPENYINWO", ds1.Tables[0].Rows[i]["硬卧"].ToString());
                        data.Add("RUANWO", ds1.Tables[0].Rows[i]["软卧"].ToString());
                        data.Add("CANCHE", ds1.Tables[0].Rows[i]["餐车"].ToString());
                        data.Add("FADIANCHE", ds1.Tables[0].Rows[i]["发电车"].ToString());
                        data.Add("SHUYINCHE", ds1.Tables[0].Rows[i]["行李车"].ToString());
                        data.Add("YOUZHENGCHE", ds1.Tables[0].Rows[i]["邮政车"].ToString());
                        if (!tab1.HasData(condition))
                        {
                            data.Add("num", Convert.ToInt32(num) + 1);
                            tab.InsertData(data);
                            num++;
                        }
                        else
                        {
                            tab.EditData(data, condition);
                        }
                    }
                }
            }
            tab.Close();
            tab1.Close();
            WebFrame.Util.JAjax.Alert("导入完成！");
        }
 

        protected void Button2_Click(object sender, EventArgs e)
        {
            JTable tab = new JTable("NEWTRAIN");
            List<SearchField> condition=new List<SearchField> ();
            if(!string.IsNullOrEmpty(Request.QueryString["TrainName"]))
            {
                condition.Add(new SearchField("TrainName", Request.QueryString["TrainName"].ToString()));
            }
            if (!string.IsNullOrEmpty( Request.QueryString["AStation"]))
            {
                condition.Add(new SearchField("AStation", Request.QueryString["AStation"].ToString()));
            }
            if (!string.IsNullOrEmpty(Request.QueryString["BStation"]))
            {
                condition.Add(new SearchField( "BStation", Request.QueryString["BStation"].ToString()));
            }
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = tab.SearchData(condition, -1, new string[] { "*" }).Tables[0];
            tab.Close();
            ExportExcel(dt, "TrainInfo");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string filepath = FileUpload1.PostedFile.FileName; //取的路径
                string filename = filepath.Substring(filepath.LastIndexOf("\\") + 1);//文件名称带后缀名
                string newfilename = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Minute.ToString() + filename;
                string pat = "~/Attachment/";//文件在服务器上的地址
                string ServerPath = pat + newfilename;
                FileUpload1.PostedFile.SaveAs(MapPath(ServerPath));
                try
                {
                    ImportExcel(ServerPath, "列车信息表");
                }
                catch(Exception err)
                {
                    WebFrame.Util.JAjax.Alert("导入失败，请确定Excel格式是否正确！");
                }
                finally
                {
                    File.Delete(MapPath(ServerPath));
                }
            }
            else
            {
                WebFrame.Util.JAjax.Alert("请选择需要导入的Excel！");
            }
        }
    }
}
