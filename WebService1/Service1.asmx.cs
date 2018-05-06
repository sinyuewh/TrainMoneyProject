using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;

namespace WebService1
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {

        [WebMethod]
        public void HelloWorld()
        {
            SqlConnection conn1=new SqlConnection();
            conn1.ConnectionString="Data Source=.;Initial Catalog=MyTest;Integrated Security=True";
            conn1.Open();
            SqlCommand comm1=new SqlCommand();
            comm1.Connection=conn1;
            comm1.CommandText="insert into student(sname) values ('jin')";
            comm1.ExecuteNonQuery();
            comm1.Dispose();
            conn1.Close();
            conn1.Dispose();
        }
    }
}
