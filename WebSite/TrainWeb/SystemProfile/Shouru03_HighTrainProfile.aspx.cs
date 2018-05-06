using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using org.in2bits.MyXls;
using WebFrame.Data;
using BusinessRule;

namespace WebSite.TrainWeb.SystemProfile.Back
{
    public partial class Shouru03_HighTrainProfile : System.Web.UI.Page
    {
        //设置数据
        protected override void OnPreRenderComplete(EventArgs e)
        {
            foreach (RepeaterItem item in this.Repeater1.Items)
            {
                for (int i = 1; i <= 5; i++)
                {
                    TextBox t1 = item.FindControl("Rate" + i) as TextBox;
                    if (t1 != null && t1.Text == "0") t1.Text="";

                    if (i == 3)
                    {
                        TextBox t11 = item.FindControl("Rate31") as TextBox;
                        if (t11 != null && t11.Text == "0") t11.Text = "";
                    }
                }
            }

            foreach (RepeaterItem item in this.Repeater2.Items)
            {
                for (int i = 1; i <= 5; i++)
                {
                    TextBox t2 = item.FindControl("PCount" + i) as TextBox;
                    if (t2 != null && t2.Text == "0") t2.Text = "";

                    if (i == 3)
                    {
                        TextBox t21 = item.FindControl("PCount31") as TextBox;
                        if (t21 != null && t21.Text == "0") t21.Text = "";
                    }
                }
            }

            base.OnPreRenderComplete(e);
        }

        protected override void OnInit(EventArgs e)
        {
            this.button1.Click += new EventHandler(button1_Click);
            this.JButton2.Click += new EventHandler(JButton2_Click);
            base.OnInit(e);
        }

        //更新列车的定员
        void JButton2_Click(object sender, EventArgs e)
        {
            WebFrame.Data.JTable tab1 = new WebFrame.Data.JTable("HIGHTRAINPROFILE");
            System.Collections.Generic.List<WebFrame.Data.SearchField> condition =
                new System.Collections.Generic.List<WebFrame.Data.SearchField>();
            System.Collections.Generic.Dictionary<String, object> data1 =
                new System.Collections.Generic.Dictionary<string, object>();
            int i = 1;
            String[] arr1 = "PCount5,PCount4,PCount1,PCount2,PCount3".Split(',');
            foreach (RepeaterItem item in this.Repeater2.Items)
            {
                condition.Clear();
                condition.Add(new WebFrame.Data.SearchField("ID", i + "", WebFrame.SearchFieldType.NumericType));
                data1.Clear();
                foreach (String m in arr1)
                {
                    TextBox t1 = item.FindControl(m) as TextBox;
                    if (t1 != null)
                    {
                        if (t1.Text.Trim() != String.Empty)
                        {
                            data1[m] = t1.Text;
                        }
                        else
                        {
                            data1[m] = "0";
                        }
                    }
                }
                tab1.EditData(data1, condition);
                i++;
            }
            tab1.Close();

            HighTrainProfile.SetData();
            WebFrame.Util.JAjax.Alert("提示：更新数据操作成功！");
        }

        //更新列车的票价
        void button1_Click(object sender, EventArgs e)
        {
            WebFrame.Data.JTable tab1 = new WebFrame.Data.JTable("HIGHTRAINPROFILE");
            System.Collections.Generic.List<WebFrame.Data.SearchField> condition =
                new System.Collections.Generic.List<WebFrame.Data.SearchField>();
            System.Collections.Generic.Dictionary<String, object> data1 =
                new System.Collections.Generic.Dictionary<string, object>();
            int i = 1;
            String[] arr1 = "Rate5,Rate4,Rate1,Rate2,Rate3,Rate31".Split(',');
            foreach (RepeaterItem item in this.Repeater1.Items)
            {
                condition.Clear();
                condition.Add(new WebFrame.Data.SearchField("ID", i + "", WebFrame.SearchFieldType.NumericType));
                data1.Clear();
                foreach (String m in arr1)
                {
                    TextBox t1 = item.FindControl(m) as TextBox;
                    if (t1 != null)
                    {
                        if (t1.Text.Trim() != String.Empty)
                        {
                            data1[m] = t1.Text;
                        }
                        else
                        {
                            data1[m] = "0";
                        }
                    }
                }
                tab1.EditData(data1, condition);
                i++;
            }
            tab1.Close();

            HighTrainProfile.SetData();
            WebFrame.Util.JAjax.Alert("提示：更新数据操作成功！");
        }

        #region 数据的导入和导出
        //数据的导入和导出
        protected void ExportExcel(DataTable dt, string filename)
        {
            XlsDocument xls = new XlsDocument();//新建一个xls文档
            xls.FileName = filename;//设定文件名
            string sheetName = "动车票价和定员表";
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
            cells.Add(1, 2, "动车类型", cellXFColumnText);//设定第一行，第二列单元格的值
            cells.Add(1, 3, "动车类别", cellXFColumnText);//设定第一行，第三列单元格的值
            cells.Add(1, 4, "一等座基本票价", cellXFColumnText);//设定第一行，第四列单元格的值
            cells.Add(1, 5, "一等座定员", cellXFColumnText);//设定第一行，第五列单元格的值
            cells.Add(1, 6, "二等座基本票价", cellXFColumnText);
            cells.Add(1, 7, "二等座定员", cellXFColumnText);
            cells.Add(1, 8, "动卧上铺基本票价", cellXFColumnText);
            cells.Add(1, 9, "动卧下铺基本票价", cellXFColumnText);
            cells.Add(1, 10, "动卧定员", cellXFColumnText);
            cells.Add(1, 11, "商务座基本票价", cellXFColumnText);
            cells.Add(1, 12, "商务座定员", cellXFColumnText);

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
                cells.Add(currentRow, 1, Convert.ToString(dr["id"]), cellXFColumnValue);//动车类型
                cells.Add(currentRow, 2, Convert.ToString(dr["HIGHTRAINTYPE"]), cellXFColumnValue);//动车类型
                cells.Add(currentRow, 3, Convert.ToString(dr["MILETYPE"]), cellXFColumnValue);//里程
                cells.Add(currentRow, 4, Convert.ToString(dr["RATE1"]), cellXFColumnValue);//一等座票价
                cells.Add(currentRow, 5, Convert.ToString(dr["PCOUNT1"]), cellXFColumnValue);
                cells.Add(currentRow, 6, Convert.ToString(dr["RATE2"]), cellXFColumnValue);
                cells.Add(currentRow, 7, Convert.ToString(dr["PCOUNT2"]), cellXFColumnValue);
                cells.Add(currentRow, 8, Convert.ToString(dr["RATE3"]), cellXFColumnValue);
                cells.Add(currentRow, 9, Convert.ToString(dr["RATE31"]), cellXFColumnValue);
                cells.Add(currentRow, 10, Convert.ToString(dr["PCOUNT3"]), cellXFColumnValue);
                cells.Add(currentRow, 11, Convert.ToString(dr["RATE4"]), cellXFColumnValue);
                cells.Add(currentRow, 12, Convert.ToString(dr["PCOUNT4"]), cellXFColumnValue);
                currentRow++;
            }
            xls.Send(XlsDocument.SendMethods.Inline);
        }

        protected void ImportExcel(string FilePath, string SheetName)
        {
            JTable tab = new JTable("HIGHTRAINPROFILE");
            JTable tab1 = new JTable("HIGHTRAINPROFILE");

            String FileName1 = HttpContext.Current.Server.MapPath(FilePath);
            if (File.Exists(FileName1))
            {
                DataSet ds1 = BusinessRule.PubCode.Util.xsldata(Server.MapPath(FilePath), SheetName);
                List<SearchField> condition = new List<SearchField>();
                Dictionary<string, object> data = new Dictionary<string, object>();
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    int n = ds1.Tables[0].Columns.Count;
                    JCommand cmd = new JCommand(JConnect.GetConnect());
                    cmd.CommandText = "select max(id) from HIGHTRAINPROFILE";
                    int num = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Close();
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        condition.Clear();
                        condition.Add(new SearchField("HIGHTRAINTYPE", ds1.Tables[0].Rows[i]["动车类型"].ToString()));
                        data.Clear();
                        data.Add("HIGHTRAINTYPE", ds1.Tables[0].Rows[i]["动车类型"].ToString());
                        data.Add("MILETYPE", ds1.Tables[0].Rows[i]["动车类别"].ToString());
                        data.Add("RATE1", ds1.Tables[0].Rows[i]["一等座基本票价"].ToString());
                        data.Add("PCOUNT1", ds1.Tables[0].Rows[i]["一等座定员"].ToString());
                        data.Add("RATE2", ds1.Tables[0].Rows[i]["二等座基本票价"].ToString());
                        data.Add("PCOUNT2", ds1.Tables[0].Rows[i]["二等座定员"].ToString());
                        data.Add("RATE3", ds1.Tables[0].Rows[i]["动卧上铺基本票价"].ToString());
                        data.Add("RATE31", ds1.Tables[0].Rows[i]["动卧下铺基本票价"].ToString());
                        data.Add("PCOUNT3", ds1.Tables[0].Rows[i]["动卧定员"].ToString());
                        data.Add("RATE4", ds1.Tables[0].Rows[i]["商务座基本票价"].ToString());
                        data.Add("PCOUNT4", ds1.Tables[0].Rows[i]["商务座定员"].ToString());
                      
                        if (!tab1.HasData(condition))
                        {
                            data.Add("id", Convert.ToInt32(num) + 1);
                            data.Add("SPEED",0);
                            data.Add("PRICE",0);
                            data.Add("COST1",0);
                            data.Add("COST2",0);
                            data.Add("COST3",0);
                            data.Add("COST21",0);
                            data.Add("COST22",0);
                            data.Add("SPEED2",0);
                            data.Add("SPEED3",0);
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

        protected void BtImport_Click(object sender, EventArgs e)
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
                    ImportExcel(ServerPath, "动车票价和定员表");
                }
                catch (Exception err)
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

        protected void BtExport_Click(object sender, EventArgs e)
        {
            JTable tab = new JTable("HIGHTRAINPROFILE");
            tab.CommandText = "select * from HIGHTRAINPROFILE order by id";
            DataTable dt= tab.SearchData(-1).Tables[0];
            tab.Close();
            ExportExcel(dt, "动车票价和定员表");
        }
        #endregion
    }

}
