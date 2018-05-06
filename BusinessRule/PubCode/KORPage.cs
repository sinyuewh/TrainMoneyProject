using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessRule
{
    public class MyPageAdapter : System.Web.UI.Adapters.PageAdapter
    {
        
        
        /// <summary>
        /// 释放数据库的连接资源
        /// </summary>
        /// <param name="e"></param>
        protected override void OnUnload(EventArgs e)
        {
            WebFrame.Data.JConnect.CloseConnect();
            base.OnUnload(e);
        }
    }

}
