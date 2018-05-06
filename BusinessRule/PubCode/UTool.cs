using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace BusinessRule
{
    /// <summary>
    /// 实用工具类
    /// </summary>
    public class UTool
    {
        
        public static decimal GetNumber(string str) 
        { 
            decimal result = 0; 
            if (str != null && str != string.Empty) 
           { 
                // 正则表达式剔除非数字字符（不包含小数点.） 
                str = Regex.Replace(str, @"[^\d.\d]", ""); 
                // 如果是数字，则转换为decimal类型 
                if (Regex.IsMatch(str, @"^[+-]?\d*[.]?\d*$")) 
                { 
                    result = decimal.Parse(str); 
                } 
            } 
           return result; 
        } 

        /// 
        /// 获取字符串中的数字 
        /// 
        /// 字符串 
        /// 数字 
        public static int GetNumberInt(string str) 
        { 
            int result = 0; 
            if (str != null && str != string.Empty) 
            { 
                // 正则表达式剔除非数字字符（不包含小数点.） 
                str = Regex.Replace(str, @"[^\d.\d]", ""); 
                // 如果是数字，则转换为decimal类型 
                if (Regex.IsMatch(str, @"^[+-]?\d*[.]?\d*$")) 
                { 
                    result = int.Parse(str); 
                } 
            } 
            return result; 
        }


        //写入日志文件
        public static void WriteErrorLog(String info)
        {
            String fileName = System.Web.HttpContext.Current.Server.MapPath("~/ErrorLog/" + System.DateTime.Now.ToString("yyyyMMdd") + ".txt");
            StreamWriter sw1 = null;
            try
            {

                sw1 = new StreamWriter(fileName, true, System.Text.Encoding.GetEncoding("gb2312"));
                //分别写入时间、信息和分隔符
                sw1.WriteLine(DateTime.Now.ToString());
                sw1.WriteLine(info);
                sw1.WriteLine("----------------------------------------------------------------------------------------------------");
            }
            catch (Exception err)
            {
                ;
            }
            finally
            {
                if(sw1!=null) sw1.Close();
            }
        }



        //写入日志文件
        public static void WriteErrorLog(Exception err)
        {
            if (err != null)
            {
                WriteErrorLog(err.ToString());
            }
        }

    }
}
