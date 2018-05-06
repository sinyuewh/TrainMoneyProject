using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using org.in2bits.MyXls;
using System.Data;
using WebFrame.Data;

namespace TrainWebSite
{
    public partial class TrainLineList : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            this.butImport.Click+=new EventHandler(butImport_Click);
            this.butExportData.Click += new EventHandler(butExportData_Click);
            base.OnLoad(e);
        }

        
        protected override void OnPreRenderComplete(EventArgs e)
        {
            foreach (RepeaterItem item1 in this.Repeater1.Items)
            {
                Label lab1 = item1.FindControl("LineType") as Label;
                String linetype1 = lab1.Text.Trim();
                LinkButton link1 = item1.FindControl("butExchange") as LinkButton;
                Label lab2 = item1.FindControl("SpringWinter") as Label;
                String sw = lab2.Text.Trim();

                if (!(linetype1 == "特二类" || linetype1 == "特一类"))
                {
                    link1.Visible = false;
                }
                else
                {
                    if (sw == "1")
                    {
                        link1.Text = "<img src='../images/22.gif' width='16' height='16' />切换";
                        link1.ToolTip = "点击不切换";
                    }
                    else
                    {
                        link1.Text = "<img src='../images/22.gif' width='16' height='16' />不切换";
                        link1.ToolTip = "点击切换";
                    }
                }
            }
            base.OnPreRenderComplete(e);
        }

        //导出数据到Excel
        void butExportData_Click(object sender, EventArgs e)
        {
            XlsDocument xls = new XlsDocument();//新建一个xls文档
            xls.FileName = "train.xls";//设定文件名

            List<String> lineNameList = BusinessRule.Line.GetTrainLindName();
            JTable tab1 = new WebFrame.Data.JTable();
            tab1.TableName = "linestationview";

            foreach (String m in lineNameList)
            {
                string sheetName = m;
                Worksheet sheet = xls.Workbook.Worksheets.Add(sheetName);//填加名为"chc 实例"的sheet页
                Cells cells = sheet.Cells;//Cells实例是sheet页中单元格（cell）集合

                ////设置列的宽度
                #region 设置列的宽度
                ColumnInfo colInfo = new ColumnInfo(xls, sheet);//生成列格式对象
                //设定colInfo格式的起作用的列为第2列到第5列(列格式为0-base)
                colInfo.ColumnIndexStart = 0;//起始列为第一列,ColumnIndexStart是从0开始
                colInfo.Width = 15 * 256;//列的宽度计量单位为 1/256 字符宽
                sheet.AddColumnInfo(colInfo);//把格式附加到sheet页上（注：AddColumnInfo方法有点小问题，不给把colInfo对象多次附给sheet页）

                ColumnInfo colInfo1 = new ColumnInfo(xls, sheet);//生成列格式对象
                //设定colInfo格式的起作用的列为第2列到第5列(列格式为0-base)
                colInfo1.ColumnIndexStart = 1;//起始列为第一列,ColumnIndexStart是从0开始
                colInfo1.Width = 20 * 256;//列的宽度计量单位为 1/256 字符宽
                sheet.AddColumnInfo(colInfo1);//把格式附加到sheet页上（注：AddColumnInfo方法有点小问题，不给把colInfo对象多次附给sheet页）

                ColumnInfo colInfo2 = new ColumnInfo(xls, sheet);//生成列格式对象
                //设定colInfo格式的起作用的列为第2列到第5列(列格式为0-base)
                colInfo2.ColumnIndexStart = 2;//起始列为第一列,ColumnIndexStart是从0开始
                colInfo2.Width = 15 * 256;//列的宽度计量单位为 1/256 字符宽
                sheet.AddColumnInfo(colInfo2);//把格式附加到sheet页上（注：AddColumnInfo方法有点小问题，不给把colInfo对象多次附给sheet页）

                ColumnInfo colInfo3 = new ColumnInfo(xls, sheet);//生成列格式对象
                //设定colInfo格式的起作用的列为第2列到第5列(列格式为0-base)
                colInfo3.ColumnIndexStart = 3;//起始列为第一列,ColumnIndexStart是从0开始
                colInfo3.Width = 15 * 256;//列的宽度计量单位为 1/256 字符宽
                sheet.AddColumnInfo(colInfo3);//把格式附加到sheet页上（注：AddColumnInfo方法有点小问题，不给把colInfo对象多次附给sheet页）

                ColumnInfo colInfo4 = new ColumnInfo(xls, sheet);//生成列格式对象
                //设定colInfo格式的起作用的列为第2列到第5列(列格式为0-base)
                colInfo4.ColumnIndexStart = 4;//起始列为第一列,ColumnIndexStart是从0开始
                colInfo4.Width = 15 * 256;//列的宽度计量单位为 1/256 字符宽
                sheet.AddColumnInfo(colInfo4);//把格式附加到sheet页上（注：AddColumnInfo方法有点小问题，不给把colInfo对象多次附给sheet页）

                ColumnInfo colInfo5 = new ColumnInfo(xls, sheet);//生成列格式对象
                //设定colInfo格式的起作用的列为第2列到第5列(列格式为0-base)
                colInfo5.ColumnIndexStart = 5;//起始列为第一列,ColumnIndexStart是从0开始
                colInfo5.Width = 15 * 256;//列的宽度计量单位为 1/256 字符宽
                sheet.AddColumnInfo(colInfo5);//把格式附加到sheet页上（注：AddColumnInfo方法有点小问题，不给把colInfo对象多次附给sheet页）

                ColumnInfo colInfo6 = new ColumnInfo(xls, sheet);//生成列格式对象
                //设定colInfo格式的起作用的列为第2列到第5列(列格式为0-base)
                colInfo6.ColumnIndexStart = 6;//起始列为第一列,ColumnIndexStart是从0开始
                colInfo6.Width = 15 * 256;//列的宽度计量单位为 1/256 字符宽
                sheet.AddColumnInfo(colInfo6);//把格式附加到sheet页上（注：AddColumnInfo方法有点小问题，不给把colInfo对象多次附给sheet页）

                ColumnInfo colInfo7 = new ColumnInfo(xls, sheet);//生成列格式对象
                //设定colInfo格式的起作用的列为第2列到第5列(列格式为0-base)
                colInfo7.ColumnIndexStart = 7;//起始列为第一列,ColumnIndexStart是从0开始
                colInfo7.Width = 15 * 256;//列的宽度计量单位为 1/256 字符宽
                sheet.AddColumnInfo(colInfo7);//把格式附加到sheet页上（注：AddColumnInfo方法有点小问题，不给把colInfo对象多次附给sheet页）

                ColumnInfo colInfo8 = new ColumnInfo(xls, sheet);//生成列格式对象
                //设定colInfo格式的起作用的列为第2列到第5列(列格式为0-base)
                colInfo8.ColumnIndexStart = 8;//起始列为第一列,ColumnIndexStart是从0开始
                colInfo8.Width = 15 * 256;//列的宽度计量单位为 1/256 字符宽
                sheet.AddColumnInfo(colInfo8);//把格式附加到sheet页上（注：AddColumnInfo方法有点小问题，不给把colInfo对象多次附给sheet页）
                #endregion

                XF cellXFColumnText = xls.NewXF();
                cellXFColumnText.HorizontalAlignment = HorizontalAlignments.Centered;//设定文字居中

                cells.Add(1, 1, "线名代码", cellXFColumnText);//设定第一行，第一列单元格的值
                cells.Add(1, 2, "线名", cellXFColumnText);//设定第一行，第二列单元格的值
                cells.Add(1, 3, "站名代码", cellXFColumnText);//设定第一行，第三列单元格的值

                cells.Add(1, 4, "始发站", cellXFColumnText);//设定第一行，第四列单元格的值
                cells.Add(1, 5, "到达站", cellXFColumnText);//设定第一行，第四列单元格的值
                cells.Add(1, 6, "里程", cellXFColumnText);

                cells.Add(1, 7, "局内", cellXFColumnText);
                cells.Add(1, 8, "电气化", cellXFColumnText);
                cells.Add(1, 9, "轮渡标志", cellXFColumnText);

                int currentRow = 2;
                DataTable dt = BusinessRule.Line.GetTrainLindData(tab1,m);
                foreach (DataRow dr in dt.Rows)
                {
                    String jnFlag = dr["jnflag"].ToString().Trim();
                    if (jnFlag != String.Empty)
                    {
                        jnFlag = "是";
                    }

                    String dqh = dr["dqh"].ToString().Trim();
                    if (dqh != String.Empty)
                    {
                        dqh = "是";
                    }

                    String shipFlag = dr["shipflag"].ToString().Trim();
                    if (shipFlag != String.Empty)
                    {
                        shipFlag = "是";
                    }


                    cells.Add(currentRow, 1, Convert.ToString(dr["lineid"]), cellXFColumnText);
                    cells.Add(currentRow, 2, Convert.ToString(dr["linename"]), cellXFColumnText);
                    cells.Add(currentRow, 3, Convert.ToString(""), cellXFColumnText);

                    cells.Add(currentRow, 4, Convert.ToString(dr["Astation"]), cellXFColumnText);
                    cells.Add(currentRow, 5, Convert.ToString(dr["bstation"]), cellXFColumnText);
                    cells.Add(currentRow, 6, Convert.ToString(dr["miles"]), cellXFColumnText);

                    cells.Add(currentRow, 7, jnFlag , cellXFColumnText);
                    cells.Add(currentRow, 8, dqh, cellXFColumnText);
                    cells.Add(currentRow, 9, shipFlag, cellXFColumnText);
                    currentRow++;
                }
            }
            tab1.Close();

            
            xls.Send(XlsDocument.SendMethods.Inline);
        }

        //导出数据到Excel
        void butExportData_ClickOLD(object sender, EventArgs e)
        {
            XlsDocument xls = new XlsDocument();//新建一个xls文档
            xls.FileName = "train.xls";//设定文件名

            List<String> lineNameList = BusinessRule.Line.GetTrainLindName();
            JTable tab1 = new WebFrame.Data.JTable();
            tab1.TableName = "linestationview";

            foreach (String m in lineNameList)
            {
                string sheetName = m;
                Worksheet sheet = xls.Workbook.Worksheets.Add(sheetName);//填加名为"chc 实例"的sheet页
                Cells cells = sheet.Cells;//Cells实例是sheet页中单元格（cell）集合

                ////设置列的宽度
                #region 设置列的宽度
                ColumnInfo colInfo = new ColumnInfo(xls, sheet);//生成列格式对象
                //设定colInfo格式的起作用的列为第2列到第5列(列格式为0-base)
                colInfo.ColumnIndexStart = 0;//起始列为第一列,ColumnIndexStart是从0开始
                colInfo.Width = 15 * 256;//列的宽度计量单位为 1/256 字符宽
                sheet.AddColumnInfo(colInfo);//把格式附加到sheet页上（注：AddColumnInfo方法有点小问题，不给把colInfo对象多次附给sheet页）

                ColumnInfo colInfo1 = new ColumnInfo(xls, sheet);//生成列格式对象
                //设定colInfo格式的起作用的列为第2列到第5列(列格式为0-base)
                colInfo1.ColumnIndexStart = 1;//起始列为第一列,ColumnIndexStart是从0开始
                colInfo1.Width = 20 * 256;//列的宽度计量单位为 1/256 字符宽
                sheet.AddColumnInfo(colInfo1);//把格式附加到sheet页上（注：AddColumnInfo方法有点小问题，不给把colInfo对象多次附给sheet页）

                ColumnInfo colInfo2 = new ColumnInfo(xls, sheet);//生成列格式对象
                //设定colInfo格式的起作用的列为第2列到第5列(列格式为0-base)
                colInfo2.ColumnIndexStart = 2;//起始列为第一列,ColumnIndexStart是从0开始
                colInfo2.Width = 15 * 256;//列的宽度计量单位为 1/256 字符宽
                sheet.AddColumnInfo(colInfo2);//把格式附加到sheet页上（注：AddColumnInfo方法有点小问题，不给把colInfo对象多次附给sheet页）

                ColumnInfo colInfo3 = new ColumnInfo(xls, sheet);//生成列格式对象
                //设定colInfo格式的起作用的列为第2列到第5列(列格式为0-base)
                colInfo3.ColumnIndexStart = 3;//起始列为第一列,ColumnIndexStart是从0开始
                colInfo3.Width = 15 * 256;//列的宽度计量单位为 1/256 字符宽
                sheet.AddColumnInfo(colInfo3);//把格式附加到sheet页上（注：AddColumnInfo方法有点小问题，不给把colInfo对象多次附给sheet页）

                ColumnInfo colInfo4 = new ColumnInfo(xls, sheet);//生成列格式对象
                //设定colInfo格式的起作用的列为第2列到第5列(列格式为0-base)
                colInfo4.ColumnIndexStart = 4;//起始列为第一列,ColumnIndexStart是从0开始
                colInfo4.Width = 15 * 256;//列的宽度计量单位为 1/256 字符宽
                sheet.AddColumnInfo(colInfo4);//把格式附加到sheet页上（注：AddColumnInfo方法有点小问题，不给把colInfo对象多次附给sheet页）

                ColumnInfo colInfo5 = new ColumnInfo(xls, sheet);//生成列格式对象
                //设定colInfo格式的起作用的列为第2列到第5列(列格式为0-base)
                colInfo5.ColumnIndexStart = 5;//起始列为第一列,ColumnIndexStart是从0开始
                colInfo5.Width = 15 * 256;//列的宽度计量单位为 1/256 字符宽
                sheet.AddColumnInfo(colInfo5);//把格式附加到sheet页上（注：AddColumnInfo方法有点小问题，不给把colInfo对象多次附给sheet页）

                ColumnInfo colInfo6 = new ColumnInfo(xls, sheet);//生成列格式对象
                //设定colInfo格式的起作用的列为第2列到第5列(列格式为0-base)
                colInfo6.ColumnIndexStart = 6;//起始列为第一列,ColumnIndexStart是从0开始
                colInfo6.Width = 15 * 256;//列的宽度计量单位为 1/256 字符宽
                sheet.AddColumnInfo(colInfo6);//把格式附加到sheet页上（注：AddColumnInfo方法有点小问题，不给把colInfo对象多次附给sheet页）

                ColumnInfo colInfo7 = new ColumnInfo(xls, sheet);//生成列格式对象
                //设定colInfo格式的起作用的列为第2列到第5列(列格式为0-base)
                colInfo7.ColumnIndexStart = 7;//起始列为第一列,ColumnIndexStart是从0开始
                colInfo7.Width = 15 * 256;//列的宽度计量单位为 1/256 字符宽
                sheet.AddColumnInfo(colInfo7);//把格式附加到sheet页上（注：AddColumnInfo方法有点小问题，不给把colInfo对象多次附给sheet页）

                ColumnInfo colInfo8 = new ColumnInfo(xls, sheet);//生成列格式对象
                //设定colInfo格式的起作用的列为第2列到第5列(列格式为0-base)
                colInfo8.ColumnIndexStart = 8;//起始列为第一列,ColumnIndexStart是从0开始
                colInfo8.Width = 15 * 256;//列的宽度计量单位为 1/256 字符宽
                sheet.AddColumnInfo(colInfo8);//把格式附加到sheet页上（注：AddColumnInfo方法有点小问题，不给把colInfo对象多次附给sheet页）
                #endregion

                XF cellXFColumnText = xls.NewXF();
                cellXFColumnText.HorizontalAlignment = HorizontalAlignments.Centered;//设定文字居中

                cells.Add(1, 1, "线名代码", cellXFColumnText);//设定第一行，第一列单元格的值
                cells.Add(1, 2, "线名", cellXFColumnText);//设定第一行，第二列单元格的值
                cells.Add(1, 3, "站名代码", cellXFColumnText);//设定第一行，第三列单元格的值

                cells.Add(1, 4, "始发站", cellXFColumnText);//设定第一行，第四列单元格的值
                cells.Add(1, 5, "到达站", cellXFColumnText);//设定第一行，第四列单元格的值

                cells.Add(1, 6, "里程", cellXFColumnText);
                cells.Add(1, 7, "局内", cellXFColumnText);
                cells.Add(1, 8, "电气化", cellXFColumnText);
                cells.Add(1, 9, "轮渡标志", cellXFColumnText);

                int currentRow = 2;
                DataTable dt = BusinessRule.Line.GetTrainLindData(tab1, m);
                int total = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    String jnFlag = dr["jnflag"].ToString().Trim();
                    if (jnFlag != String.Empty)
                    {
                        jnFlag = "是";
                    }

                    String dqh = dr["dqh"].ToString().Trim();
                    if (dqh != String.Empty)
                    {
                        dqh = "是";
                    }

                    String shipFlag = dr["shipflag"].ToString().Trim();
                    if (shipFlag != String.Empty)
                    {
                        shipFlag = "是";
                    }


                    if (currentRow == 2)
                    {
                        cells.Add(currentRow, 1, Convert.ToString(dr["lineid"]), cellXFColumnText);
                        cells.Add(currentRow, 2, Convert.ToString(dr["linename"]), cellXFColumnText);
                        cells.Add(currentRow, 3, Convert.ToString(""), cellXFColumnText);
                        cells.Add(currentRow, 4, Convert.ToString(dr["Astation"]), cellXFColumnText);
                        cells.Add(currentRow, 5, Convert.ToString(total), cellXFColumnText);
                        currentRow++;
                        total = total + int.Parse(dr["miles"].ToString());

                        cells.Add(currentRow, 1, Convert.ToString(dr["lineid"]), cellXFColumnText);
                        cells.Add(currentRow, 2, Convert.ToString(dr["linename"]), cellXFColumnText);
                        cells.Add(currentRow, 3, Convert.ToString(""), cellXFColumnText);
                        cells.Add(currentRow, 4, Convert.ToString(dr["Bstation"]), cellXFColumnText);
                        cells.Add(currentRow, 5, Convert.ToString(total), cellXFColumnText);

                        cells.Add(currentRow, 6, jnFlag, cellXFColumnText);
                        cells.Add(currentRow, 7, dqh, cellXFColumnText);
                        cells.Add(currentRow, 8, shipFlag, cellXFColumnText);
                        currentRow++;
                    }
                    else
                    {
                        total = total + int.Parse(dr["miles"].ToString());
                        cells.Add(currentRow, 1, Convert.ToString(dr["lineid"]), cellXFColumnText);
                        cells.Add(currentRow, 2, Convert.ToString(dr["linename"]), cellXFColumnText);
                        cells.Add(currentRow, 3, Convert.ToString(""), cellXFColumnText);
                        cells.Add(currentRow, 4, Convert.ToString(dr["Bstation"]), cellXFColumnText);
                        cells.Add(currentRow, 5, Convert.ToString(total), cellXFColumnText);
                        cells.Add(currentRow, 6, jnFlag, cellXFColumnText);
                        cells.Add(currentRow, 7, dqh, cellXFColumnText);
                        cells.Add(currentRow, 8, shipFlag, cellXFColumnText);
                        currentRow++;
                    }
                }
            }
            tab1.Close();


            xls.Send(XlsDocument.SendMethods.Inline);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(TrainLineList));
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void butImport_Click(object sender, EventArgs e)
        {
           // BusinessRule.Line.ImportLineData("data2.xls");
            BusinessRule.PubCode.Util.importTrainLineDataToSystem("kylc2.xls");
        }

        /// <summary>
        /// 删除线路数据
        /// </summary>
        /// <param name="lineID"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public string DeleteLine(string lineID)
        {
            String result = String.Empty;
            if (String.IsNullOrEmpty(lineID) == false &&
                WebFrame.Util.JValidator.IsInt(lineID))
            {
                BusinessRule.Line.DeleteLine(int.Parse(lineID));
            }
            return result;
        }


        /// <summary>
        /// 更换线路“夏冬状态标识”
        /// </summary>
        /// <param name="lineID"></param>
        /// <returns></returns>
        [AjaxPro.AjaxMethod]
        public string ChangeSpringAndWinterStatus(String lineID)
        {
            String result = String.Empty;
            if (String.IsNullOrEmpty(lineID) == false &&
                WebFrame.Util.JValidator.IsInt(lineID))
            {
                BusinessRule.Line.ChangeSpringAndWinterStatus(int.Parse(lineID));
            }
            return result;
        }
    }
}
