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
    public class TrainMaxCheXianBU
    {
        public static void SaveMaxCheXian(String traintype, int yz, int yw, int rw)
        {
            /*
            JTable tab1 = new JTable("TRAINMAXCHEXIAN");
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("TRAINTYPE", traintype));
            DataSet ds1 = tab1.SearchData(condition, -1, "*");
            if (ds1.Tables[0].Rows.Count == 0)
            {
                DataRow dr1 = ds1.Tables[0].NewRow();
                dr1["traintype"] = traintype;
                dr1["yz"] = yz;
                dr1["yw"] = yw;
                dr1["rw"] = rw;
                ds1.Tables[0].Rows.Add(dr1);
                tab1.Update(ds1.Tables[0]);
            }
            tab1.Close();*/
        }

        public static void GetMaxCheXian(String traintype, 
            out int yz, out int yw, out int rw)
        {
            yz = 0;
            yw = 0;
            rw = 0;

            /*
            JTable tab1 = new JTable("TRAINMAXCHEXIAN");
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("TRAINTYPE", traintype));
            DataRow dr1 = tab1.GetFirstDataRow(condition, "*");
            if (dr1 != null)
            {
                yz = int.Parse(dr1["yz"].ToString());
                yw = int.Parse(dr1["yw"].ToString());
                rw = int.Parse(dr1["rw"].ToString());
            }
            tab1.Close();*/
        }
    }
}
