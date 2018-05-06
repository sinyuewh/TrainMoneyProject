using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFrame.Data;
using WebFrame.Util;
using System.Data;
using System.Data.OleDb;

namespace WebSite.TrainWeb.Train
{
    public partial class ChangJiaoFeeList : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            this.JButton2.Click += new EventHandler(JButton2_Click);
            base.OnInit(e);
        }

        //导入数据
        void JButton2_Click(object sender, EventArgs e)
        {
            DataSet ds = GetData();
            DataTable dt = ds.Tables[0];

            //导入数据
            Dictionary<String, object> data1 = new Dictionary<string, object>();
            JTable tab1 = new JTable("CHANGJIAOQYFEE");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String lineType = "";
                String lineName = "";
                String qy = "";
                String fee1 = "";
                String fee2 = "";
                String fee3 = "";

                DataRow dr = dt.Rows[i];
                //得到当前行的数据
                if (dr[0].ToString().Trim() != String.Empty)
                {
                    lineType = dr[0].ToString().Trim();
                    lineName = dr[1].ToString().Trim().Replace("～", "-");
                    qy = dr[2].ToString().Trim();
                    if (qy == "内燃")
                    {
                        fee1 = dr[3].ToString().Trim();
                    }
                    else
                    {
                        fee2 = dr[3].ToString().Trim();
                    }
                    fee3 = dr[4].ToString().Trim();
                }

                if (lineType != String.Empty)
                {
                    //得到下一行的数据
                    if (i < dt.Rows.Count - 1)
                    {
                        DataRow dr1 = dt.Rows[i + 1];
                        if (dr1[0].ToString().Trim() == String.Empty)
                        {
                            qy = dr1[2].ToString().Trim();
                            if (qy == "内燃")
                            {
                                fee1 = dr1[3].ToString().Trim();
                            }
                            else
                            {
                                fee2 = dr1[3].ToString().Trim();
                            }
                        }
                    }
                }

                //验证数据是否正确
                if (String.IsNullOrEmpty(lineType) == false)
                {
                    data1.Clear();
                    data1["num"] = BusinessRule.ChangJiaoFeeBU.GetNextNum();
                    data1["linename"] = lineType;
                    data1["jiaolu"] = lineName;

                    if (fee1 != String.Empty)
                    {
                        data1["fee1"] = int.Parse(fee1);
                    }
                    else
                    {
                        data1["fee1"] = 0;
                    }

                    if (fee2 != String.Empty)
                    {
                        data1["fee2"] = int.Parse(fee2);
                    }
                    else
                    {
                        data1["fee2"] = 0;
                    }

                    if (fee3 != String.Empty)
                    {
                        data1["fee3"] = int.Parse(fee3);
                    }
                    else
                    {
                        data1["fee3"] = 0;
                    }
                    tab1.InsertData(data1);
                }
            }

            tab1.Close();   //关闭数据库的链接
            this.Repeater1.DataBind();

        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }


        private DataSet GetData()
        {
            String filepath = Server.MapPath("/TrainWeb/Train/cjl.xls");
            string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
            System.Data.OleDb.OleDbConnection Conn = new System.Data.OleDb.OleDbConnection(strCon);

            Conn.Open();
            DataTable dt = Conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            String tableName = dt.Rows[0][2].ToString().Trim();
            tableName = "客运长交路机车牵引费单价表$";
            string strCom = String.Format("SELECT * FROM [{0}]", tableName);


            System.Data.OleDb.OleDbDataAdapter myCommand = new System.Data.OleDb.OleDbDataAdapter(strCom, Conn);
            DataSet ds = new DataSet();
            myCommand.Fill(ds);
            Conn.Close();
            return ds;
        }

        private static List<String> CheckLine(String LineName, out String Error)
        {
            List<String> result = new List<string>();
            Error = String.Empty;
            JTable tab1 = new JTable("TRAINLINE");
            List<SearchField> condition = new List<SearchField>();
            if (String.IsNullOrEmpty(LineName) == false)
            {
                String[] line = LineName.Replace("、", ";").Replace("，", ";").Replace(",", ";").Split(';');
                if (line != null)
                {
                    foreach (String m in line)
                    {
                        condition.Clear();

                        condition.Add(new SearchField("trim(LINENAME)", m.Trim()));
                        DataSet ds1 = tab1.SearchData(condition, -1, "lineid");
                        if (ds1.Tables[0].Rows.Count == 1)
                        {
                            foreach (DataRow dr1 in ds1.Tables[0].Rows)
                            {
                                if (result.Contains(dr1[0].ToString()) == false)
                                {
                                    result.Add(dr1[0].ToString());
                                }
                            }
                        }
                        else
                        {
                            Error = String.Format("【{0}】线路的配置数量为{1}(要求为1)，请检查！", m, ds1.Tables[0].Rows.Count);
                            break;
                        }
                    }
                }
            }
            tab1.Close();
            if (String.IsNullOrEmpty(Error) == false)
            {
                result.Clear();
                result = null;
            }
            return result;
        }
    }
}
