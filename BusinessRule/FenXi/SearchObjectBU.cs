using System;
using System.Collections.Generic;

using System.Text;
using System.Data;

using WebFrame;
using WebFrame.Data;
using WebFrame.Util;
using WebFrame.Designer;
using WebFrame.ExpControl;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace BusinessRule
{
    public class SearchObjectBU
    {
        public static void GetSearchInfoBynum(String num,out String A0,out String B0,out String ShenDu,out String Fengduan)
        {
            A0 = String.Empty; B0 = String.Empty; ShenDu = "30"; Fengduan = String.Empty;
            JTable tab1 = new JTable("SEARCHOBJECTLIST");
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("num", num,SearchFieldType.NumericType));
            DataRow dr = tab1.GetFirstDataRow(condition, "*");
            if (dr != null)
            {
                A0 = dr["Astation"].ToString();
                B0 = dr["Bstation"].ToString();
                ShenDu = dr["shendu"].ToString();
                Fengduan = dr["fengduan"].ToString();
            }
            tab1.Close();
        }

        /// <summary>
        /// 从数据库中恢复数据
        /// </summary>
        /// <param name="AStation"></param>
        /// <param name="BStation"></param>
        /// <param name="Shendu"></param>
        /// <param name="newLine"></param>
        /// <returns></returns>
        public static bool RestoreSearchResultFromDb
            (
            String AStation,
            String BStation,
            String fengduanText,
            out Line newLine)
        {
            bool succResult = false;
            String FileName = String.Empty;
            newLine = null; 
            
            JTable tab1 = new JTable("SEARCHOBJECTLIST");
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("ASTATION", AStation));
            condition.Add(new SearchField("BSTATION", BStation));
            condition.Add(new SearchField("SaveTime is not null", "", SearchOperator.UserDefine));

            if (String.IsNullOrEmpty(fengduanText) == false)
            {
                condition.Add(new SearchField("FENGDUAN", fengduanText));
            }
            else
            {
                condition.Add(new SearchField("FENGDUAN is null", "", SearchOperator.UserDefine));
            }

            tab1.OrderBy = "ShenDu desc,num desc";
           
            DataSet ds1 = tab1.SearchData(condition, -1, "*");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                DataRow dr1 = ds1.Tables[0].Rows[0];
                FileName = dr1["NEWLINEFILENAME"].ToString();
                newLine = RestoreObjectFormFile(FileName) as Line;
            }
            tab1.Close();
            return succResult;
        }

        /// <summary>
        /// 将查询结果保存到数据库
        /// </summary>
        /// <param name="AStation"></param>
        /// <param name="BStation"></param>
        /// <param name="Shendu"></param>
        /// <param name="newLine"></param>
        /// <param name="QianZhiBaoCun"></param>
        public static bool SaveSearchResultToDb(
            String AStation,
            String BStation,
            Line newLine,
            String fengduanText)
        {
            bool succResult = true;
            String Temp = String.Empty;
            String FileName = String.Empty;
            
            JTable tab1 = new JTable("SEARCHOBJECTLIST");
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("ASTATION", AStation));
            condition.Add(new SearchField("BSTATION", BStation));

            if (String.IsNullOrEmpty(fengduanText) == false)
            {
                condition.Add(new SearchField("FENGDUAN", fengduanText));
            }
            else
            {
                condition.Add(new SearchField("FENGDUAN is null", "",SearchOperator.UserDefine));
            }
            tab1.OrderBy = "ShenDu desc,num desc";

            DataSet ds1 = tab1.SearchData(condition, -1, "*");
            DataRow dr1 = null;
            bool updateFlag = false;
            if (ds1.Tables[0].Rows.Count > 0)
            {
                dr1 = ds1.Tables[0].Rows[0];
                if (dr1["SAVETIME"].ToString().Trim()==String.Empty)     //表示线路数据发生了变化（线路数据和站点别名等数据发生了变化）
                {
                    dr1["SaveTime"] = DateTime.Now;       
                    FileName = dr1["NEWLINEFILENAME"].ToString();
                    updateFlag = true;
                }
            }
            else
            {
                Temp = JString.GetUnique32ID();
                FileName = Temp + ".bin";

                dr1 = ds1.Tables[0].NewRow();
                dr1["SHENDU"] = 100;
                dr1["Astation"] = AStation;
                dr1["bStation"] = BStation;
                dr1["NEWLINEFILENAME"] = FileName;
                dr1["SaveTime"] = DateTime.Now;
                if (String.IsNullOrEmpty(fengduanText) == false)
                {
                    dr1["FENGDUAN"] = fengduanText;
                }
                ds1.Tables[0].Rows.Add(dr1);
                updateFlag = true;
            }

            if (dr1 != null && updateFlag)
            {
                bool succ = SaveObjectToFile(newLine , FileName);
                if (succ)
                {
                    tab1.Update(ds1.Tables[0]);
                }
                succResult = succ;
            }
            tab1.Close();
            return succResult;
        }


        public static bool SaveSearchResultToDb(
            String AStation,
            String BStation,
            Line newLine)
        {
            return SaveSearchResultToDb(AStation, BStation,newLine,String.Empty);
        }


        #region Private Function
        /// <summary>
        /// 将对象保存到文件
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="FullFileName"></param>
        private static bool SaveObjectToFile(object obj, String FullFileName)
        {
            bool result = true;
            try
            {
                String Temp = System.Web.HttpContext.Current.Server.MapPath("/Attachment/SearchResult/"+FullFileName);
                if (obj != null && String.IsNullOrEmpty(FullFileName) == false)
                {
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream(Temp, FileMode.Create,
                        FileAccess.Write, FileShare.None);
                    
                    formatter.Serialize(stream, obj);
                    stream.Close();
                }
            }
            catch (Exception err) { result = false; }
            return result;
        }

        /// <summary>
        /// 从文件中恢复对象
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="FullFileName"></param>
        /// <returns></returns>
        private static object  RestoreObjectFormFile(String FullFileName)
        {
            object obj1 = null;
            try
            {
                String Temp = System.Web.HttpContext.Current.Server.MapPath("/Attachment/SearchResult/" + FullFileName);
                if (File.Exists(Temp))
                {
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream(Temp, FileMode.Open, FileAccess.Read, FileShare.Read);
                    obj1 = formatter.Deserialize(stream);
                    stream.Close();
                }
            }
            catch (Exception err)
            {
                ; 
            }
            return obj1;
        }
        #endregion

    }
}
