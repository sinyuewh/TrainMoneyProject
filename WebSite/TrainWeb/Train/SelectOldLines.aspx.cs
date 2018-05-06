using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessRule;

using WebFrame.Data;
using WebFrame;
using System.Collections;

namespace WebSite.TrainWeb
{
    public partial class SelectOldLines : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BindData();
               
            }
        }

        private void BindData()
        {
            String a1 = Request.QueryString["astation"];
            String b1 = Request.QueryString["bstation"];
            JTable tab1 = new JTable("NEWTRAIN");
            List<SearchField> condition = new List<SearchField>();
            SearchField search1 = new SearchField("Line", a1 + "-", SearchOperator.Contains);
            SearchField search2 = new SearchField("Line", "-" + b1, SearchOperator.Contains);
            SearchField search0 = search1 & search2;

           // SearchField search3 = new SearchField("Line", b1 + "-", SearchOperator.Contains);
           // SearchField search4 = new SearchField("Line", "-" + a1, SearchOperator.Contains);
           // SearchField search5 = search3 & search4;

           // condition.Add(search0 | search5);
            condition.Add(search0 );
            
            DataTable dt1 = tab1.SearchData(condition, -1, "*").Tables[0];
            dt1.Columns.Add("linevalue");
            for (int i = dt1.Rows.Count - 1; i >= 0;i-- )
            {
                DataRow dr1 = dt1.Rows[i];
                String l1 = dr1["line"].ToString();
                String[] arr1 = l1.Split('-');
                bool exist1 = ((IList)arr1).Contains(a1);
                bool exist2 = ((IList)arr1).Contains(b1);
                if (exist1 == false || exist2 == false)
                {
                    dr1.Delete();
                }
            }
            dt1.AcceptChanges();

            foreach (DataRow dr1 in dt1.Rows)
            {
                String l1=dr1["line"].ToString();
                if(String.IsNullOrEmpty(l1)==false)
                {
                    String[] arr1=l1.Split('-');
                    int pos1=-1;
                    int pos2=-1;
                    for(int i=0;i<arr1.Length;i++)
                    {
                        if(arr1[i]==a1)
                        {
                            pos1 =i;
                        }

                        if(arr1[i]==b1)
                        {
                            pos2=i;
                        }
                    }

                    if(pos1>-1 && pos2>-1 && pos2>pos1)
                    {
                        String line1=String.Empty;
                        bool first=true;
                        for(int i=pos1;i<=pos2;i++)
                        {
                            if(first)
                            {
                                //line1 =arr1[i]+"-"+arr1[i+1]+"(0#1)";
                                line1 = arr1[i];
                                first=false;
                            }
                            else
                            {
                                //line1 =line1+","+ arr1[i] + "-" + arr1[i + 1] + "(0#1)";
                                line1 = line1 + "-" + arr1[i];
                            }
                        }

                        dr1["linevalue"] = line1;
                    }
                }
            }


            this.Repeater1.DataSource = dt1;
            this.Repeater1.DataBind();
            tab1.Close();
        }

        //设置Line的Value
        private String getLineValue()
        {
            return String.Empty;
        }
    }
}
