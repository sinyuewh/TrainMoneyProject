using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using SqlServerProject1.SendMessageWebService;

public partial class Triggers
{
    // 为目标输入现有表或视图并取消对属性行的注释
    [Microsoft.SqlServer.Server.SqlTrigger (Name="SendMessage", Target="JRole", Event="FOR INSERT")]
    public static void Trigger1()
    {
        // 用您的代码替换
       // SqlContext.Pipe.Send("Trigger FIRED");
       WebServiceInterface ser1 = new WebServiceInterface();
       ser1.SendNote("13971135896", "hello", "kingorient", "347221","197",null,"1061");
       ser1.Dispose();
    }
}
