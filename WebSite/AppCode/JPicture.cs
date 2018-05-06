using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;

namespace WebSite.AppCode
{
    public enum EHistogramPostionType
    {
        中间,偏左,偏右
    }
    public class MyPicture
    {
        #region 图形的基本要素
        private int height = 480;
        /// <summary>
        /// 图形的高度
        /// </summary>
        public int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        private int width = 700;
        /// <summary>
        /// 图形的宽度
        /// </summary>
        public int Width
        {
            get { return this.width; }
            set { this.width = value; }
        }


        private Color bgColor = Color.AliceBlue;
        /// <summary>
        /// 图形的背景色
        /// </summary>
        public Color BgColor
        {
            get { return this.bgColor; }
            set { this.bgColor = value; }
        }

        private Color borderColor = Color.Empty;
        /// <summary>
        /// 图形的边框颜色
        /// </summary>
        public Color BorderColor
        {
            get { return this.borderColor; }
            set { this.borderColor = value; }
        }
        #endregion

        #region 相关的图形Title信息
        private string title = "这是一个测试的图形文本";
        /// <summary>
        /// 图形的Text
        /// </summary>
        public String Title 
        { 
            get { return this.title; } 
            set { this.title = value; } 
        }

        private int titlePosX = 85;
        /// <summary>
        /// 图形Title的PosX
        /// </summary>
        public int TitlePosX
        {
            get { return this.titlePosX; }
            set { this.titlePosX = value; }
        }

        private int titlePosY = 30;
        /// <summary>
        /// 图形Title的PosY
        /// </summary>
        public int TitlePosY
        {
            get { return this.titlePosY; }
            set { this.titlePosY = value; }
        }

        private Color textColor = Color.Black;
        /// <summary>
        /// Title的颜色
        /// </summary>
        public Color TextColor
        {
            get { return this.textColor; }
            set { this.textColor=value; }
        }

        private Font textFont = new System.Drawing.Font("宋体", 14, FontStyle.Regular);
        /// <summary>
        /// Title的字体
        /// </summary>
        public Font TextFont
        {
            get { return this.textFont; }
            set { this.textFont = value; }
        }
        #endregion

        #region 坐标轴相关信息
        private int originX = 60;
        /// <summary>
        /// 原点坐标-X
        /// </summary>
        public int OriginX
        {
            get { return this.originX; }
            set { this.originX = value; }
        }

        private int originY =-1;
        /// <summary>
        /// 原点坐标-Y
        /// </summary>
        public int OriginY
        {
            get
            {
                if (this.originY == -1)
                {
                    if (this.height - 60 >= 0)
                    {
                        return this.height - 60;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return this.originY;
                }
            }
            set { this.originY = value; }
        }

        private int axisHeight = -1;
        /// <summary>
        /// 坐标轴的高度
        /// </summary>
        public int AxisHeight
        {
            get
            {
                if (this.axisHeight == -1)
                {
                    if (this.OriginY - 60 > 0)
                    {
                        return this.OriginY - 60;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return this.axisHeight;
                }
            }
            set
            {
                this.axisHeight = value;
            }
        }
        
        private int axisWidth = -1;
        /// <summary>
        /// 坐标轴的宽度
        /// </summary>
        public int AxisWidth
        {
            get
            {
                if (this.axisWidth == -1)
                {
                    if (this.width -this.OriginX- 60 > 0)
                    {
                        return this.width - this.OriginX - 60;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return this.axisWidth;
                }
            }
            set
            {
                this.axisWidth = value;
            }
        }

        private Color axisColor = Color.Blue;
        /// <summary>
        /// 坐标轴边框的颜色
        /// </summary>
        public Color AxisColor
        {
            get { return this.axisColor; }
            set { this.axisColor = value; }
        }

        private int axisBorderWidth = 3;
        /// <summary>
        /// 坐标轴边框的宽度
        /// </summary>
        public int AxisBorderWidth
        {
            get { return this.axisBorderWidth; }
            set { this.axisBorderWidth = value; }
        }

        private Font axisTextfont = new System.Drawing.Font("Arial", 9, FontStyle.Regular);
        /// <summary>
        /// 坐标轴文字的Font
        /// </summary>
        public Font AxisTextfont
        {
            get { return this.axisTextfont; }
            set { this.axisTextfont = value; }
        }

        private Color axisTextColor = Color.Red;
        /// <summary>
        /// 坐标轴文字的颜色
        /// </summary>
        public Color AxisTextColor
        {
            get { return this.axisTextColor; }
            set { this.axisTextColor = value; }
        }
        #endregion

        #region 相关的数据
        private String[] xdata = null;
        private int[] ydata = null;
        private List<Color>  LineColor = new List<Color>();
        private List<String> LineText = new List<string>();
        private string unitLabel = "个";
        /// <summary>
        /// 设置Y轴单位
        /// </summary>
        public String UnitLabel
        {
            get { return this.unitLabel; }
            set { this.unitLabel = value; }
        }
        #endregion

        Bitmap image = null;
        Graphics g = null;
        public Graphics MyGraphics
        {
            get { return this.g; }
        }

        /// <summary>
        /// 初始化绘图
        /// </summary>
        public void InitDrawPicture()
        {
            try
            {
                this.image = new Bitmap(this.width, this.height);
                this.g = Graphics.FromImage(image);

                //清空图片背景色
                g.Clear(Color.White);

                //绘制图形的背景颜色
                g.FillRectangle(new SolidBrush(this.bgColor), 0, 0, width, height);

                //画的边框线
                if (this.borderColor != Color.Empty)
                {
                    g.DrawRectangle(new Pen(this.borderColor), 0, 0, image.Width - 1, image.Height - 1);
                }

                //绘制图形的Title
                if (String.IsNullOrEmpty(this.title) == false)
                {
                    Font font1 = this.textFont;
                    Brush brush1 = new SolidBrush(this.textColor);
                    g.DrawString(this.title, font1, brush1, new PointF(this.titlePosX, this.titlePosY));
                }
                
            }
            catch(Exception e)
            {
                this.g.Dispose();
                this.image.Dispose();
            }
        }

        //释放资源
        public void Dispose()
        {
            if (this.g != null)
            { 
                g.Dispose(); 
            }
            if (this.image != null)
            {
                image.Dispose();
            }
        }

        //析构函数
        ~MyPicture()
        {
            this.Dispose();
        }

        //绘制坐标轴
        public void DrawCoordAxis(String[] Xdata, int[] Ydata)
        {
            this.DrawCoordAxis(Xdata, Ydata, true);
        }
        public void DrawCoordAxis(String[] Xdata,int[] Ydata,bool ShowGrid)
        {
            if (this.image != null && this.g != null)
            {
                try
                {
                    Pen mypen1 = new Pen(this.AxisColor, this.AxisBorderWidth);
                    Pen mypen2 = new Pen(this.AxisColor);
                   
                    //绘制X轴
                    int xTemp = this.AxisWidth / Xdata.Length;
                    int maxX = this.OriginX + xTemp * Xdata.Length + 1;
                    g.DrawLine(mypen1, this.OriginX, this.OriginY, maxX, this.OriginY);
                    for (int i = 1; i <= Xdata.Length; i++)
                    {
                        int x1 = this.OriginX + i * xTemp;
                        if (ShowGrid)
                        {
                            g.DrawLine(mypen2, x1, this.OriginY, x1, this.OriginY - this.AxisHeight);
                        }
                        SizeF sf1 = g.MeasureString(Xdata[i - 1], this.AxisTextfont);
                        int x2 = x1  - (int)sf1.Width / 2;
                        g.DrawString(Xdata[i - 1].ToString(), this.AxisTextfont, new SolidBrush(this.AxisTextColor), x2, this.OriginY + 5);
                    }
                    
                    //绘制Y轴
                    int yTemp = this.AxisHeight / Ydata.Length;
                    g.DrawLine(mypen1, this.OriginX, this.OriginY, this.OriginX, this.OriginY - this.AxisHeight);
                    for (int i = 1; i <= Ydata.Length; i++)
                    {
                        int y1 = this.OriginY - i * yTemp;
                        if (ShowGrid)
                        {
                            g.DrawLine(mypen2, this.OriginX, y1, maxX - 1, y1);
                        }
                        SizeF sf1 = g.MeasureString(Ydata[i - 1].ToString(), this.AxisTextfont);
                        int y2 = y1 - 5;
                        g.DrawString(Ydata[i - 1].ToString(), this.AxisTextfont, new SolidBrush(this.AxisTextColor), this.OriginX - (int)sf1.Width - 5, y2);
                    }

                    //绘制Y轴标签
                    if (String.IsNullOrEmpty(this.UnitLabel) == false)
                    {
                        String unit1 = "单位（" + this.UnitLabel + "）";
                        SizeF sf1 = g.MeasureString(unit1, this.AxisTextfont);
                        g.DrawString(unit1, this.AxisTextfont, new SolidBrush(this.AxisTextColor), this.OriginX, this.OriginY - this.AxisHeight - 20);
                    }

                    //保存xdata标签
                    this.xdata = Xdata;
                    this.ydata = Ydata;

                }
                catch (Exception e)
                {
                    g.Dispose();
                    image.Dispose();
                }
            }
        }

        //绘制折线图
        public void DrawLineChart(double[] LineData)
        {
            this.DrawLineChart(LineData, this.AxisColor,"Line1");
        }
        public void DrawLineChart(double[] LineData,Color LineColor,String LineText)
        {
            if (this.image != null && this.g != null
                && this.xdata!=null && this.xdata.Length>0
                && this.ydata!=null && this.ydata.Length>0 )
            {
                try
                {
                    Pen mypen3 = new Pen(LineColor, 2);
                    this.LineColor.Add(LineColor);
                    this.LineText.Add(LineText);
     
                    //绘制线条
                    int xTemp = this.AxisWidth / this.xdata.Length;
                    int yTemp = this.AxisHeight / this.ydata.Length;

                    int unit2 = ydata[0];
                    for (int i = 0; i < LineData.Length - 1; i++)
                    {
                        int x1 = this.OriginX + xTemp * (i+1);
                        double temp = (LineData[i] / unit2) * yTemp;
                        int y1 = this.OriginY - (int)temp;
                        //g.DrawString(LineData[i].ToString(), this.AxisTextfont, new SolidBrush(this.AxisTextColor), x1, y1);

                        int x2 = this.OriginX + xTemp * (i + 2);
                        temp = (LineData[i + 1] / unit2) * yTemp;
                        int y2 = this.OriginY - (int)temp;


                        if (LineData[i] > 0)
                        {
                            g.DrawString(LineData[i].ToString(), this.AxisTextfont, new SolidBrush(this.AxisTextColor), x1, y1);
                        }
                        if (i == LineData.Length - 2)
                        {
                            if (LineData[i + 1] > 0)
                            {
                                g.DrawString(LineData[i + 1].ToString(), this.AxisTextfont, new SolidBrush(this.AxisTextColor), x2, y2);
                            }
                        }
                       
                         g.DrawLine(mypen3, x1, y1, x2, y2);

                    }

                }
                catch (Exception e)
                {
                    g.Dispose();
                    image.Dispose();
                }
            }
        }

        //绘制柱状图
        public void DrawHistogram(double[] LineData, 
            Color LineColor,
            int HistogramWidth,
            EHistogramPostionType type1,
            String LineText)
        {
            if (this.image != null && this.g != null
                && this.xdata != null && this.xdata.Length > 0
                && this.ydata != null && this.ydata.Length > 0)
            {
                try
                {
                    Pen mypen3 = new Pen(LineColor, 2);
                    this.LineColor.Add(LineColor);
                    this.LineText.Add(LineText);

                    //绘制线条
                    int xTemp = this.AxisWidth / this.xdata.Length;
                    int yTemp = this.AxisHeight / this.ydata.Length;

                    int unit2 = ydata[0];
                    for (int i = 0; i < LineData.Length - 1; i++)
                    {
                        int x1 = this.OriginX + xTemp * (i + 1);
                        if (type1 == EHistogramPostionType.偏右)
                        {
                            x1 = x1 - HistogramWidth;
                        }
                        else if (type1 == EHistogramPostionType.中间)
                        {
                            x1 = x1 - HistogramWidth / 2;
                        }
                        

                        double temp = (LineData[i] / unit2) * yTemp;
                        int y1 = this.OriginY - (int)temp;
                        g.DrawString(LineData[i].ToString(), this.AxisTextfont, new SolidBrush(this.AxisTextColor), x1, y1);

                        int x2 = this.OriginX + xTemp * (i + 2);
                        temp = (LineData[i + 1] / unit2) * yTemp;
                        int y2 = this.OriginY - (int)temp;


                        g.DrawString(LineData[i].ToString(), this.AxisTextfont, new SolidBrush(this.AxisTextColor), x1, y1);

                        if (i == LineData.Length - 2)
                        {
                            g.DrawString(LineData[i + 1].ToString(), this.AxisTextfont, new SolidBrush(this.AxisTextColor), x2, y2);
                        }
                        g.DrawLine(mypen3, x1, y1, x2, y2);

                    }

                }
                catch (Exception e)
                {
                    g.Dispose();
                    image.Dispose();
                }
            }
        }

        //绘制图形说明框
        public void DrawLineText(int beginX, int beginY)
        {
            DrawLineText(beginX, beginY, 250, 50, this.AxisColor);
        }

        public void DrawLineText(int beginX,int beginY,
                                int TextWidth,int TextHeight,
                                Color TextBorderColor)
        {
            Font font3 = new System.Drawing.Font("Arial", 10, FontStyle.Regular);

            g.DrawRectangle(new Pen(TextBorderColor), beginX,beginY,TextWidth,TextHeight); //绘制范围框
            int posY = beginY + 10;
            for (int i = 0; i < this.LineColor.Count; i++)
            {
                g.FillRectangle(new SolidBrush(this.LineColor[i]), beginX+30, posY+i*20, 20, 10); //绘制小矩形
                g.DrawString(this.LineText[i], font3, new SolidBrush(this.LineColor[i]), beginX + 60, posY + i * 20);
            }
        }

        //图形输出到屏幕
        public void InputPictureToScreen()
        {
            //图形输出
            if (this.image != null && this.g !=null)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                this.image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                System.Web.HttpContext.Current.Response.ClearContent();
                System.Web.HttpContext.Current.Response.ContentType = "image/Jpeg";
                System.Web.HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            }
        }
    }


    public class JPicture
    {
        /// <summary>
        /// 折线统计图的绘制
        /// </summary>
        public static void CreateImage2()
        {
            int height = 480, width = 700;
            Bitmap image = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //清空图片背景色
                g.Clear(Color.White);

                Font font = new System.Drawing.Font("Arial", 9, FontStyle.Regular);
                Font font1 = new System.Drawing.Font("宋体", 20, FontStyle.Regular);
                Font font2 = new System.Drawing.Font("Arial", 8, FontStyle.Regular);
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.Blue, 1.2f, true);

                g.FillRectangle(Brushes.AliceBlue, 0, 0, width, height);
                Brush brush1 = new SolidBrush(Color.Blue);
                Brush brush2 = new SolidBrush(Color.SaddleBrown);

                g.DrawString("省建设厅" + " " + "2006年" +
                " 成绩统计折线图", font1, brush1, new PointF(85, 30));

                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Blue), 0, 0, image.Width - 1, image.Height - 1);

                Pen mypen = new Pen(brush, 1);
                Pen mypen2 = new Pen(Color.Red, 2);
                //绘制线条
                //绘制纵向线条
                int x = 60;
                for (int i = 0; i < 8; i++)
                {
                    g.DrawLine(mypen, x, 80, x, 340);
                    x = x + 80;
                }
                Pen mypen1 = new Pen(Color.Blue, 3);
                x = 60;
                g.DrawLine(mypen1, x, 82, x, 340);

                //绘制横向线条
                int y = 106;
                for (int i = 0; i < 10; i++)
                {
                    g.DrawLine(mypen, 60, y, 620, y);
                    y = y + 26;
                }
                // y = 106;
                g.DrawLine(mypen1, 60, y - 26, 620, y - 26);

                //x轴
                String[] n = { "第一期", "第二期", "第三期", "第四期", "上半年", "下半年", "全年统计" };
                x = 45;
                for (int i = 0; i < 7; i++)
                {
                    g.DrawString(n[i].ToString(), font, Brushes.Red, x, 348); //设置文字内容及输出位置
                    x = x + 77;
                }

                //y轴
                String[] m = { "220人", " 200人", " 175人", "150人", " 125人", " 100人", " 75人", " 50人", " 25人" };
                y = 100;
                for (int i = 0; i < 9; i++)
                {
                    g.DrawString(m[i].ToString(), font, Brushes.Red, 10, y); //设置文字内容及输出位置
                    y = y + 26;
                }

                int[] Count1 = new int[] { 39, 111, 71, 40, 150, 111, 261 };
                int[] Count2 = new int[] { 26, 68, 35, 14, 94, 49, 143 };

                //显示折线效果
                Font font3 = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
                SolidBrush mybrush = new SolidBrush(Color.Red);
                Point[] points1 = new Point[7];
                points1[0].X = 60; points1[0].Y = 340 - Count1[0]; //从106纵坐标开始, 到(0, 0)坐标时
                points1[1].X = 140; points1[1].Y = 340 - Count1[1];
                points1[2].X = 220; points1[2].Y = 340 - Count1[2];
                points1[3].X = 300; points1[3].Y = 340 - Count1[3];

                points1[4].X = 380; points1[4].Y = 340 - Count1[4];
                points1[5].X = 460; points1[5].Y = 340 - Count1[5];

                points1[6].X = 540; points1[6].Y = 340 - Count1[6];
                g.DrawLines(mypen2, points1); //绘制折线

                //绘制数字
                g.DrawString(Count1[0].ToString(), font3, Brushes.Red, 58, points1[0].Y - 20);
                g.DrawString(Count1[1].ToString(), font3, Brushes.Red, 138, points1[1].Y - 20);
                g.DrawString(Count1[2].ToString(), font3, Brushes.Red, 218, points1[2].Y - 20);
                g.DrawString(Count1[3].ToString(), font3, Brushes.Red, 298, points1[3].Y - 20);

                g.DrawString(Count1[4].ToString(), font3, Brushes.Red, 378, points1[4].Y - 20);
                g.DrawString(Count1[5].ToString(), font3, Brushes.Red, 458, points1[5].Y - 20);

                g.DrawString(Count1[6].ToString(), font3, Brushes.Red, 538, points1[6].Y - 20);

                Pen mypen3 = new Pen(Color.Green, 2);
                Point[] points2 = new Point[7];
                points2[0].X = 60; points2[0].Y = 340 - Count2[0];
                points2[1].X = 140; points2[1].Y = 340 - Count2[1];
                points2[2].X = 220; points2[2].Y = 340 - Count2[2];
                points2[3].X = 300; points2[3].Y = 340 - Count2[3];

                points2[4].X = 380; points2[4].Y = 340 - Count2[4];
                points2[5].X = 460; points2[5].Y = 340 - Count2[5];

                points2[6].X = 540; points2[6].Y = 340 - Count2[6];
                g.DrawLines(mypen3, points2); //绘制折线

                //绘制通过人数
                g.DrawString(Count2[0].ToString(), font3, Brushes.Green, 61, points2[0].Y - 15);
                g.DrawString(Count2[1].ToString(), font3, Brushes.Green, 131, points2[1].Y - 15);
                g.DrawString(Count2[2].ToString(), font3, Brushes.Green, 221, points2[2].Y - 15);
                g.DrawString(Count2[3].ToString(), font3, Brushes.Green, 301, points2[3].Y - 15);

                g.DrawString(Count2[4].ToString(), font3, Brushes.Green, 381, points2[4].Y - 15);
                g.DrawString(Count2[5].ToString(), font3, Brushes.Green, 461, points2[5].Y - 15);

                g.DrawString(Count2[6].ToString(), font3, Brushes.Green, 541, points2[6].Y - 15);

                //绘制标识
                g.DrawRectangle(new Pen(Brushes.Red), 180, 390, 250, 50); //绘制范围框
                g.FillRectangle(Brushes.Red, 270, 402, 20, 10); //绘制小矩形
                g.DrawString("报名人数", font2, Brushes.Red, 292, 400);

                g.FillRectangle(Brushes.Green, 270, 422, 20, 10);
                g.DrawString("通过人数", font2, Brushes.Green, 292, 420);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                System.Web.HttpContext.Current.Response.ClearContent();
                System.Web.HttpContext.Current.Response.ContentType = "image/Jpeg";
                System.Web.HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        /// <summary>
        /// 柱状图的完整代码
        /// </summary>
        public static void CreateImage1()
        {
            int height = 500, width = 700;
            Bitmap image = new Bitmap(width, height);
            //创建Graphics类对象
            Graphics g = Graphics.FromImage(image);

            try
            {
                //清空图片背景色
                g.Clear(Color.White);

                Font font = new Font("Arial", 10, FontStyle.Regular);
                Font font1 = new Font("宋体", 20, FontStyle.Bold);

                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height),
                Color.Blue, Color.BlueViolet, 1.2f, true);
                g.FillRectangle(Brushes.WhiteSmoke, 0, 0, width, height);
                // Brush brush1 = new SolidBrush(Color.Blue);

                g.DrawString("省建设厅" + " " + "2006年" + " 成绩统计柱状图", font1, brush, new PointF(70, 30));
                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Blue), 0, 0, image.Width - 1, image.Height - 1);

                Pen mypen = new Pen(brush, 1);
                //绘制线条
                //绘制横向线条
                int x = 100;
                for (int i = 0; i < 14; i++)
                {
                    g.DrawLine(mypen, x, 80, x, 340);
                    x = x + 40;
                }
                Pen mypen1 = new Pen(Color.Blue, 2);
                x = 60;
                g.DrawLine(mypen1, x, 80, x, 340);

                //绘制纵向线条
                int y = 106;
                for (int i = 0; i < 9; i++)
                {
                    g.DrawLine(mypen, 60, y, 620, y);
                    y = y + 26;
                }
                g.DrawLine(mypen1, 60, y, 620, y);

                //x轴
                String[] n = { "第一期", "第二期", "第三期", "第四期", "上半年", "下半年", "全年统计" };
                x = 78;
                for (int i = 0; i < 7; i++)
                {
                    g.DrawString(n[i].ToString(), font, Brushes.Blue, x, 348); //设置文字内容及输出位置
                    x = x + 78;
                }

                //y轴
                String[] m = { "250", "225", "200", "175", "150", "125", "100", " 75", " 50", " 25", " 0" };
                y = 72;
                for (int i = 0; i < 10; i++)
                {
                    g.DrawString(m[i].ToString(), font, Brushes.Blue, 25, y); //设置文字内容及输出位置
                    y = y + 26;
                }

                int[] Count1 = new int[] { 39, 111, 71, 40, 150, 111, 261 };
                int[] Count2 = new int[] { 26, 68, 35, 14, 94, 49, 143 };


                //绘制柱状图.
                x = 80;
                Font font2 = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
                SolidBrush mybrush = new SolidBrush(Color.Red);
                SolidBrush mybrush2 = new SolidBrush(Color.Green);

                //第一期
                g.FillRectangle(mybrush, x, 340 - Count1[0], 20, Count1[0]);
                g.DrawString(Count1[0].ToString(), font2, Brushes.Red, x, 340 - Count1[0] - 15);

                x = x + 20;
                g.FillRectangle(mybrush2, x, 340 - Count2[0], 20, Count2[0]);
                g.DrawString(Count2[0].ToString(), font2, Brushes.Green, x, 340 - Count2[0] - 15);

                //第二期
                x = x + 60;
                g.FillRectangle(mybrush, x, 340 - Count1[1], 20, Count1[1]);
                g.DrawString(Count1[1].ToString(), font2, Brushes.Red, x, 340 - Count1[1] - 15);

                x = x + 20;
                g.FillRectangle(mybrush2, x, 340 - Count2[1], 20, Count2[1]);
                g.DrawString(Count2[1].ToString(), font2, Brushes.Green, x, 340 - Count2[1] - 15);

                //第三期
                x = x + 60;
                g.FillRectangle(mybrush, x, 340 - Count1[2], 20, Count1[2]);
                g.DrawString(Count1[2].ToString(), font2, Brushes.Red, x, 340 - Count1[2] - 15);

                x = x + 20;
                g.FillRectangle(mybrush2, x, 340 - Count2[2], 20, Count2[2]);
                g.DrawString(Count2[2].ToString(), font2, Brushes.Green, x, 340 - Count2[2] - 15);

                //第四期
                x = x + 60;
                g.FillRectangle(mybrush, x, 340 - Count1[3], 20, Count1[3]);
                g.DrawString(Count1[3].ToString(), font2, Brushes.Red, x, 340 - Count1[3] - 15);

                x = x + 20;
                g.FillRectangle(mybrush2, x, 340 - Count2[3], 20, Count2[3]);
                g.DrawString(Count2[3].ToString(), font2, Brushes.Green, x, 340 - Count2[3] - 15);

                //上半年
                x = x + 60;
                g.FillRectangle(mybrush, x, 340 - Count1[4], 20, Count1[4]);
                g.DrawString(Count1[4].ToString(), font2, Brushes.Red, x, 340 - Count1[4] - 15);

                x = x + 20;
                g.FillRectangle(mybrush2, x, 340 - Count2[4], 20, Count2[4]);
                g.DrawString(Count2[4].ToString(), font2, Brushes.Green, x, 340 - Count2[4] - 15);

                //下半年
                x = x + 60;
                g.FillRectangle(mybrush, x, 340 - Count1[5], 20, Count1[5]);
                g.DrawString(Count1[5].ToString(), font2, Brushes.Red, x, 340 - Count1[5] - 15);

                x = x + 20;
                g.FillRectangle(mybrush2, x, 340 - Count2[5], 20, Count2[5]);
                g.DrawString(Count2[5].ToString(), font2, Brushes.Green, x, 340 - Count2[5] - 15);

                //全年
                x = x + 60;
                g.FillRectangle(mybrush, x, 340 - Count1[6], 20, Count1[6]);
                g.DrawString(Count1[6].ToString(), font2, Brushes.Red, x, 340 - Count1[6] - 15);

                x = x + 20;
                g.FillRectangle(mybrush2, x, 340 - Count2[6], 20, Count2[6]);
                g.DrawString(Count2[6].ToString(), font2, Brushes.Green, x, 340 - Count2[6] - 15);

                //绘制标识
                Font font3 = new System.Drawing.Font("Arial", 10, FontStyle.Regular);
                g.DrawRectangle(new Pen(Brushes.Blue), 170, 400, 250, 50); //绘制范围框
                g.FillRectangle(Brushes.Red, 270, 410, 20, 10); //绘制小矩形
                g.DrawString("报名人数", font3, Brushes.Red, 292, 408);

                g.FillRectangle(Brushes.Green, 270, 430, 20, 10);
                g.DrawString("通过人数", font3, Brushes.Green, 292, 428);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                System.Web.HttpContext.Current.Response.ClearContent();
                System.Web.HttpContext.Current.Response.ContentType = "image/Jpeg";
                System.Web.HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        
        /// <summary>
        /// 扇形统计图的绘制
        /// </summary>
        public static void CreateImage3()
        {
            float Total = 0.0f, Tmp;

            //转换成单精度。也可写成Convert.ToInt32
            Total = Convert.ToSingle(94);

            // Total=Convert.ToSingle(ds.Tables[0].Rows[0][this.count[0]]);
            //设置字体，fonttitle为主标题的字体
            Font fontlegend = new Font("verdana", 9);
            Font fonttitle = new Font("verdana", 10, FontStyle.Bold);

            //背景宽
            int width = 350;
            int bufferspace = 15;
            int legendheight = fontlegend.Height * 10 + bufferspace; //高度
            int titleheight = fonttitle.Height + bufferspace;
            int height = width + legendheight + titleheight + bufferspace;//白色背景高
            int pieheight = width;
            Rectangle pierect = new Rectangle(0, titleheight, width, pieheight);

            //加上各种随机色
            ArrayList colors = new ArrayList();
            Random rnd = new Random();
            for (int i = 0; i < 2; i++)
                colors.Add(new SolidBrush(Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255))));

            //创建一个bitmap实例
            Bitmap objbitmap = new Bitmap(width, height);
            Graphics objgraphics = Graphics.FromImage(objbitmap);

            //画一个白色背景
            objgraphics.FillRectangle(new SolidBrush(Color.White), 0, 0, width, height);

            //画一个亮黄色背景 
            objgraphics.FillRectangle(new SolidBrush(Color.Beige), pierect);

            //以下为画饼图(有几行row画几个)
            float currentdegree = 0.0f;

            //画通过人数
            objgraphics.FillPie((SolidBrush)colors[1], pierect, currentdegree,
            Convert.ToSingle(82) / Total * 360);
            currentdegree += Convert.ToSingle(82) / Total * 360;

            //未通过人数饼状图
            objgraphics.FillPie((SolidBrush)colors[0], pierect, currentdegree,
            ((Convert.ToSingle(100)) - (Convert.ToSingle(82))) / Total * 360);
            currentdegree += ((Convert.ToSingle(100)) -
            (Convert.ToSingle(82))) / Total * 360;

            //以下为生成主标题
            SolidBrush blackbrush = new SolidBrush(Color.Black);
            SolidBrush bluebrush = new SolidBrush(Color.Blue);
            string title = " 机关单位成绩统计饼状图: "
            + "\n \n\n";
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            objgraphics.DrawString(title, fonttitle, blackbrush,
            new Rectangle(0, 0, width, titleheight), stringFormat);

            //列出各字段与得数目
            objgraphics.DrawRectangle(new Pen(Color.Red, 2), 0, height + 10 - legendheight, width, legendheight + 50);

            objgraphics.DrawString("----------------统计信息------------------",
            fontlegend, bluebrush, 20, height - legendheight + fontlegend.Height * 1 + 1);
            objgraphics.DrawString("统计单位: " + "华中科技大学",
            fontlegend, blackbrush, 20, height - legendheight + fontlegend.Height * 3 + 1);
            objgraphics.DrawString("统计年份: " + "2009",
            fontlegend, blackbrush, 20, height - legendheight + fontlegend.Height * 4 + 1);
            objgraphics.DrawString("统计期数: " + "2",
            fontlegend, blackbrush, 20, height - legendheight + fontlegend.Height * 5 + 1);

            objgraphics.FillRectangle((SolidBrush)colors[1], 5, height - legendheight + fontlegend.Height * 8 + 1, 10, 10);
            objgraphics.DrawString("报名总人数: " + Convert.ToString(100),
            fontlegend, blackbrush, 20, height - legendheight + fontlegend.Height * 7 + 1);
            objgraphics.FillRectangle((SolidBrush)colors[0], 5, height - legendheight + fontlegend.Height * 9 + 1, 10, 10);
            objgraphics.DrawString("通过总人数: " + Convert.ToString(82),
            fontlegend, blackbrush, 20, height - legendheight + fontlegend.Height * 8 + 1);
            objgraphics.DrawString("未通过人数: " + 18, fontlegend, blackbrush, 20, height - legendheight + fontlegend.Height * 9 + 1);

            objgraphics.DrawString("通过率: " + "0.8236%", fontlegend,
            blackbrush, 20, height - legendheight + fontlegend.Height * 10 + 1);

            System.Web.HttpContext.Current.Response.ContentType = "image/Jpeg";
            objbitmap.Save(System.Web.HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            objgraphics.Dispose();
            objbitmap.Dispose();
        }
    }
}
