using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using WebFrame;
using WebFrame.Data;
using WebFrame.Util;
using WebFrame.Designer;
using WebFrame.ExpControl;

namespace BusinessRule
{
    public class ChangJiaoFeeBU
    {
        //删除数据
        public void DeleteData(String LineID)
        {
            JTable tab1 = new JTable();
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("LineId", LineID, SearchFieldType.NumericType));
            tab1.TableName = "CHANGJIAOQYFEE";
            tab1.DeleteData(condition);

            tab1.TableName = "CHANGJIAOQYFEECHILD";
            tab1.DeleteData(condition);
            tab1.Close();
        }

        //得到下一个序号
        public static int GetNextNum()
        {
            int result = 1;
            JTable tab1 = new JTable("CHANGJIAOQYFEE");
            DataRow dr1=tab1.GetFirstDataRow(null, "max(num)");
            if (dr1 != null)
            {
                if (dr1[0].ToString() != String.Empty)
                {
                    result = int.Parse(dr1[0].ToString()) + 1;
                }
            }
            tab1.Close();
            return result;
        }

        //更新数据
        public bool UpdateData(Dictionary<String, object> data1)
        {
            bool result = false;
            List<SearchField> condition=new List<SearchField>();
            JTable tab1 = new JTable();
            JTable tab2 = new JTable();
            JTable tab3 = new JTable("LineStation");
            try
            {
                tab1.MyConnect.BeginTrans();

                //更新CHANGJIAOQYFEE表中信息
                tab1.TableName = "CHANGJIAOQYFEE";
                String lineid = String.Empty;
                if (data1["lineid"] == null ||
                    data1["lineid"].ToString() == String.Empty
                    || data1["lineid"].ToString() == "-1")
                {
                    data1.Remove("lineid");
                    tab1.InsertData(data1);

                    tab2.TableName = "CHANGJIAOQYFEE";
                    condition.Clear();
                    condition.Add(new SearchField("linename", data1["linename"].ToString().Trim()));
                    tab2.OrderBy = "lineid desc";
                    DataRow dr1 = tab2.GetFirstDataRow(condition, "lineid");
                    if (dr1 != null)
                    {
                        lineid = dr1["lineid"].ToString().Trim();
                    }
                    tab2.Close();
                }
                else
                {
                    lineid = data1["lineid"].ToString();
                    condition.Clear();
                    condition.Add(new SearchField("lineid", data1["lineid"].ToString().Trim(), SearchFieldType.NumericType));
                    tab1.EditData(data1, condition);
                }


                //更新LineStation中的相关数据
                if (String.IsNullOrEmpty(lineid) == false)
                {
                    //线路
                    String error = String.Empty;
                    String lineName = data1["linename"].ToString();
                    List<String> arrlineID = CheckLine(lineName, out error);
                    int fee1=int.Parse(data1["fee1"].ToString());
                    int fee2=int.Parse(data1["fee2"].ToString());
                    int fee3=0;

                    if (String.IsNullOrEmpty(error))
                    {
                        Dictionary<String, object> data2 = new Dictionary<string, object>();
                        data2["checkflag"] = "1";
                        JTable tab4 = new JTable("CHANGJIAOQYFEE");
                        condition.Clear();
                        condition.Add(new SearchField("lineid", lineid, SearchFieldType.NumericType));
                        tab4.EditData(data2, condition);
                        tab4.Close();

                        //线路站点
                        String jiaolu = data1["jiaolu"].ToString();
                        String[] arrJiaolu = jiaolu.Replace("~", "-").Replace("～", "-").Replace("－", "-").Split('-');
                        for (int i = 0; i < arrJiaolu.Length - 1; i++)
                        {
                            //调整字符中的分隔符。
                            String[] a0 = arrJiaolu[i].Trim().Replace("、", ";").Replace(",", ";").Replace("，", ";").Replace(".", ";")
                                        .Replace("(", ";").Replace(")", "").Replace("（", ";").Replace("）", "").Split(';');
                            String[] a1 = arrJiaolu[i + 1].Trim().Replace("、", ";").Replace(",", ";").Replace("，", ";").Replace(".", ";")
                                        .Replace("(", ";").Replace(")", "").Replace("（", ";").Replace("）", "").Split(';');

                            for (int j = 0; j < a0.Length; j++)
                            {
                                a0[j] = a0[j].Trim();
                                if (j > 0)
                                {
                                    if (a0[j] == "东" || a0[j] == "南" || a0[j] == "西" || a0[j] == "北")
                                    {
                                        a0[j] = a0[j - 1] + a0[j];
                                    }
                                }
                            }

                            for (int j = 0; j < a1.Length; j++)
                            {
                                a1[j] = a1[j].Trim();
                                if (j > 0)
                                {
                                    if (a1[j] == "东" || a1[j] == "南" || a1[j] == "西" || a1[j] == "北")
                                    {
                                        a1[j] = a1[j - 1] + a1[j];
                                    }
                                }
                            }

                            //提交数据更新
                            foreach(String Astation in a0)
                            {
                                foreach(String Bstation in a1)
                                {
                                    UpdateLineStation(tab3,arrlineID, Astation, Bstation, fee1, fee2, fee3);
                                }
                            }
                            
                        }
                    }
                    else
                    {
                        JAjax.Alert(error);
                    }
                }

                //提交事务处理
                tab1.MyConnect.CommitTrans();
                result = true;
            }
            catch (Exception err) 
            {
                tab1.MyConnect.RollBackTrans();
             ;}

            tab1.Close();
            tab2.Close();
            tab3.Close();
            return result;
        }

        //更新长交路数据明细
        private void UpdateChildData(String lineid, Dictionary<String, object> data1)
        {
            JTable tab1 = new JTable();
            List<SearchField> condition = new List<SearchField>();
            tab1.TableName = "CHANGJIAOQYFEECHILD";
            condition.Clear();
            condition.Add(new SearchField("lineid", lineid, SearchFieldType.NumericType));
            tab1.DeleteData(condition);

            //增加新的数据
            DataSet ds1 = tab1.SearchData(condition, -1, "*");

            String jiaolu = data1["jiaolu"].ToString();
            String[] arrJiaolu = jiaolu.Replace("~", "-").Replace("～", "-").Replace("－", "-").Split('-');
            for (int i = 0; i < arrJiaolu.Length - 1; i++)
            {
                //调整字符中的分隔符。
                String[] a0 = arrJiaolu[i].Trim().Replace("、", ";").Replace(",", ";").Replace("，", ";").Replace(".", ";")
                            .Replace("(", ";").Replace(")", "").Replace("（", ";").Replace("）", "").Split(';');
                String[] a1 = arrJiaolu[i + 1].Trim().Replace("、", ";").Replace(",", ";").Replace("，", ";").Replace(".", ";")
                            .Replace("(", ";").Replace(")", "").Replace("（", ";").Replace("）", "").Split(';');

                for (int j = 0; j < a0.Length; j++)
                {
                    a0[j] = a0[j].Trim();
                    if (j > 0)
                    {
                        if (a0[j] == "东" || a0[j] == "南" || a0[j] == "西" || a0[j] == "北")
                        {
                            a0[j] = a0[j - 1] + a0[j];
                        }
                    }
                }

                for (int j = 0; j < a1.Length; j++)
                {
                    a1[j] = a1[j].Trim();
                    if (j > 0)
                    {
                        if (a1[j] == "东" || a1[j] == "南" || a1[j] == "西" || a1[j] == "北")
                        {
                            a1[j] = a1[j - 1] + a1[j];
                        }
                    }
                }

                for (int j = 0; j < a0.Length; j++)
                {
                    for (int k = 0; k < a1.Length; k++)
                    {
                        String A0 = a0[j];
                        String B0 = a1[k];

                        DataRow dr1 = ds1.Tables[0].NewRow();
                        dr1["lineid"] = lineid;
                        dr1["ASTATION"] = A0;
                        dr1["BSTATION"] = B0;
                        dr1["fee1"] = data1["fee1"];
                        dr1["fee2"] = data1["fee2"];
                        dr1["fee3"] = data1["fee3"];
                        ds1.Tables[0].Rows.Add(dr1);

                        DataRow dr2 = ds1.Tables[0].NewRow();
                        dr2["lineid"] = lineid;
                        dr2["ASTATION"] = B0;
                        dr2["BSTATION"] = A0;
                        dr2["fee1"] = data1["fee1"];
                        dr2["fee2"] = data1["fee2"];
                        dr2["fee3"] = data1["fee3"];
                        ds1.Tables[0].Rows.Add(dr2);
                    }
                }
            }

            //更新数据
            if (ds1.Tables[0].Rows.Count > 0)
            {
                tab1.Update(ds1.Tables[0]);
            }
        }

        //更新线路设置
        private void UpdateLineStation(JTable tab,
            List<String> lineID,
            String AStation,
            String BStation,int Fee1,int Fee2,int Fee3)
        {
            List<SearchField> condition = new List<SearchField>();
            tab.OrderBy ="id";
            foreach (String m in lineID)
            {
                if (String.IsNullOrEmpty(AStation) == false 
                    && String.IsNullOrEmpty(BStation) == false)
                {
                    for (int kind = 0; kind <= 1; kind++)
                    {
                        condition.Clear();
                        condition.Add(new SearchField("lineid", m, SearchFieldType.NumericType));
                        condition.Add(new SearchField("direction", kind +"", SearchFieldType.NumericType));
                        DataSet ds1 = tab.SearchData(condition, -1, "*");
                        if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                        {
                            int pos1 = -1;
                            int pos2 = -1;
                            bool firstA = false;
                            bool firstB = false;

                            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                            {
                                String A0 = ds1.Tables[0].Rows[i]["Astation"].ToString();
                                String B0 = ds1.Tables[0].Rows[i]["Bstation"].ToString();
                                if (AStation == A0)
                                {
                                    pos1 = i;
                                    firstA = true;
                                }

                                if (BStation == A0)
                                {
                                    pos2 = i;
                                    firstB = true;
                                }

                                if (i == ds1.Tables[0].Rows.Count - 1)
                                {
                                    if (AStation == B0 && pos1==-1)
                                    {
                                        pos1 = i;
                                    }

                                    if (BStation == B0 && pos2==-1)
                                    {
                                        pos2 = i;
                                    }
                                }
                                

                                if (pos1 != -1 && pos2 != -1 && pos1 != pos2)
                                {
                                    break;
                                }
                            }

                            if (pos1 != -1 && pos2 != -1)
                            {
                                int t1 = Math.Min(pos1, pos2);
                                int t2 = Math.Max(pos1, pos2);
                                if (firstA && firstB)
                                {
                                    t2 = t2 - 1;
                                }

                                for (int i = t1; i <=t2; i++)
                                {
                                    DataRow dr1 = ds1.Tables[0].Rows[i];
                                    dr1["fee1"] = Fee1;
                                    dr1["fee2"] = Fee2;
                                    dr1["fee3"] = Fee3;
                                }
                                tab.Update(ds1.Tables[0]);
                            }
                        }
                    }
                }
            }
        }

        //设置线路的配置
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
                        if (ds1.Tables[0].Rows.Count ==1)
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
                            Error = String.Format("{0}线路的配置数量为{1}(要求为1)，请检查！", m, ds1.Tables[0].Rows.Count);
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
