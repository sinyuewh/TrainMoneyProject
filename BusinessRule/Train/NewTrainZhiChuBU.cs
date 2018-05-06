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
    /*
     1.线路使用费 Fee1--OK
     2.机车牵引费 Fee2--OK
     3.电网和接触网使用费--OK
     4.售票服务费---OK
     5.旅客服务费----OK
     6.列车上水费----OK
     7.人员和工资附加费--OK
     8.车辆折旧费--OK（车底数相关）
     9.日常检修成本--OK（车底数相关）
     10.定期检修成本--OK
     11.车辆备用品消耗--OK（车底数相关）
     12.空调车用油--OK
     13.人员其他的费用--OK
     14.车辆购置利息（车底数相关）
     14.轮渡费
     */

    /// <summary>
    /// 计算列车的支出类
    /// </summary>
    public class NewTrainZhiChuBU:IDisposable
    {
        private const double UnitRate = 10000;
        private JTable tab1 = null;
        public String TrainName { get; set; }

                     
        private int year=DateTime.Now.Year;
        public int Year
        {
            get{ return this.year; }
            set { this.year = value; }
        }
        private int month=DateTime.Now.Month;
        public int Month
        {
            get { return this.month; }
            set { this.month = value; }
        }

        public NewTrainZhiChuBU()
        {
            this.tab1  = new JTable();
        }

        ~NewTrainZhiChuBU()
        {
            if (this.tab1 != null) tab1.Close();
        }


        //根据车的名称得到支出
        public static void GetTrainZhiChuByName(String TrainName,
           String trainType,
           int year, int month,
           out double Fee0, out double Fee1)
        {
            Fee0 = 0;
            Fee1 = 0;

            NewTrainZhiChuBU bu1 = new NewTrainZhiChuBU();
            bu1.Year = year;
            bu1.Month = month;
            bu1.TrainName = TrainName;

            bool sum = false;
            double f0 = bu1.GetFee1(sum) + bu1.GetFee2(sum) + bu1.GetFee3(sum)
                  + bu1.GetFee4(sum, trainType) + bu1.GetFee5(sum, trainType) + bu1.GetFee6(sum, trainType) + bu1.GetFee7(sum)
                   + bu1.GetFee8(sum) + bu1.GetFee9(sum) + bu1.GetFee10(sum) + bu1.GetFee11(sum)
                   + bu1.GetFee12(sum) + bu1.GetFee13(sum) + bu1.GetFee14(sum)+bu1.GetFee15(sum);

            Fee0 = Fee0 + f0;
            sum = true;
            double f1 = bu1.GetFee1(sum) + bu1.GetFee2(sum) + bu1.GetFee3(sum) + bu1.GetFee4(sum, trainType)
                  + bu1.GetFee5(sum, trainType) + bu1.GetFee6(sum, trainType) + bu1.GetFee7(sum)
                    + bu1.GetFee8(sum) + bu1.GetFee9(sum) + bu1.GetFee10(sum) + bu1.GetFee11(sum)
                    + bu1.GetFee12(sum) + bu1.GetFee13(sum) + bu1.GetFee14(sum) + bu1.GetFee15(sum);
            Fee1 = Fee1 + f1;
        }

        /// <summary>
        /// 得到某类列车(或某列表的某年月)的支出
        /// </summary>
        public static void GetTrainZhiChuByKind(String kindName,
            int year,int month,
            out double Fee0, out double Fee1)
        {
            Fee0 = 0;
            Fee1 = 0;
       
            if (String.IsNullOrEmpty(kindName)==false )
            {
                bool sum = false;
                String[] arr1 = GetTrainListByKind(kindName);
                if (arr1 != null && arr1.Length > 0)
                {
                    NewTrainZhiChuBU bu1 = new NewTrainZhiChuBU();
                    bu1.Year = year;
                    bu1.Month = month;

                    foreach (String m in arr1)
                    {
                        bu1.TrainName = m;
                        sum = false;
                        double f0 = bu1.GetFee1(sum) + bu1.GetFee2(sum) + bu1.GetFee3(sum) 
                              + bu1.GetFee4(sum,kindName) + bu1.GetFee5(sum,kindName) + bu1.GetFee6(sum,kindName) + bu1.GetFee7(sum)
                               + bu1.GetFee8(sum) + bu1.GetFee9(sum) + bu1.GetFee10(sum) + bu1.GetFee11(sum) 
                               + bu1.GetFee12(sum) + bu1.GetFee13(sum) + bu1.GetFee14(sum)+bu1.GetFee15(sum);

                        Fee0 = Fee0 + f0;


                        sum = true;
                        double f1 = bu1.GetFee1(sum) + bu1.GetFee2(sum) + bu1.GetFee3(sum) + bu1.GetFee4(sum,kindName) 
                              + bu1.GetFee5(sum,kindName) + bu1.GetFee6(sum,kindName) + bu1.GetFee7(sum)
                                + bu1.GetFee8(sum) + bu1.GetFee9(sum) + bu1.GetFee10(sum) + bu1.GetFee11(sum)
                                + bu1.GetFee12(sum) + bu1.GetFee13(sum) + bu1.GetFee14(sum) + bu1.GetFee15(sum);
                        Fee1 = Fee1 + f1;
                    }
                   
                    bu1.Dispose();
                }
            }
        }



        //根据类别名称得到列车数组
        private static String[] GetTrainListByKind(String kindName)
        {
            String[] arr1 = null;
            JTable tab1 = new JTable("NEWTRAIN");
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("TRAINTYPE", kindName));
            DataSet ds1 = tab1.SearchData(condition, -1, "distinct TRAINNAME");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                arr1 = new String[ds1.Tables[0].Rows.Count];
                int index1 = 0;
                foreach (DataRow dr1 in ds1.Tables[0].Rows)
                {
                    arr1[index1] = dr1[0].ToString();
                    index1++;
                }
            }
            tab1.Close();
            return arr1;
        }

        //根据车名称得到车的类型
        private static String GetTrainType(String trainName)
        {
            String result = String.Empty;
            JTable tab1 = new JTable("NEWTRAIN");
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("TRAINNAME", trainName));
            DataSet ds1 = tab1.SearchData(condition, -1, "distinct TRAINTYPE");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                result = ds1.Tables[0].Rows[0][0].ToString().Trim();
            }
            tab1.Close();
            return result;
        }

        //电网和接触网使用费--?（是否是导入或者计算，如计算需要固化线路）
        private double GetFee3(bool sum)
        {
            double result = 0;
            return result;
        }


        #region 列车的费用计算
        //1.线路使用费 Fee1---（直接导入数据OK）
        private double GetFee1(bool sum)
        {
            double Fee0 = 0;
            String temp1 = String.Empty;
            if (String.IsNullOrEmpty(this.TrainName) == false)
            {
                String[] arr1 = this.TrainName.Split('/');
                temp1 = this.TrainName;
                foreach (String m in arr1)
                {
                    temp1 = temp1 + "," + m;
                }

                if (String.IsNullOrEmpty(temp1) == false)
                {
                    List<SearchField> condition = new List<SearchField>();
                    tab1.TableName = "NEWTRAINXIANLUFEE";
                    condition.Add(new SearchField("TRAINNAME", temp1, SearchOperator.Collection));
                    condition.Add(new SearchField("BYEAR", this.Year + "", SearchFieldType.NumericType));
                    if (sum == false)
                    {
                        condition.Add(new SearchField("BMONTH", this.Month + "", SearchFieldType.NumericType));
                    }
                    else
                    {
                        condition.Add(new SearchField("BMONTH", this.Month + "", SearchOperator.SmallerAndEqual,
                            SearchFieldType.NumericType));
                    }

                    object obj1 = tab1.SearchScalar(condition, "sum(Fee1+Fee2+Fee3)");
                    if (obj1 != null && obj1.ToString().Trim() != String.Empty)
                    {
                        Fee0 = double.Parse(obj1.ToString());
                    }
                }
            }
            return JMath.Round1(Fee0/UnitRate,2);
        }

        //2.机车牵引费 Fee2--（直接导入数据OK）
        private double GetFee2(bool sum)
        {
            double Fee0 = 0;
            String temp1 = String.Empty;
            if (String.IsNullOrEmpty(this.TrainName) == false)
            {
                String[] arr1 = this.TrainName.Split('/');
                temp1 = this.TrainName;
                foreach (String m in arr1)
                {
                    temp1 = temp1 + "," + m;
                }

                if (String.IsNullOrEmpty(temp1) == false)
                {
                    List<SearchField> condition = new List<SearchField>();
                    tab1.TableName = "NEWTRAINQIANYINFEE";
                    condition.Add(new SearchField("TRAINNAME", temp1, SearchOperator.Collection));
                    condition.Add(new SearchField("BYEAR", this.Year + "", SearchFieldType.NumericType));
                    if (sum == false)
                    {
                        condition.Add(new SearchField("BMONTH", this.Month + "", SearchFieldType.NumericType));
                    }
                    else
                    {
                        condition.Add(new SearchField("BMONTH", this.Month + "", SearchOperator.SmallerAndEqual,
                            SearchFieldType.NumericType));
                    }

                    object obj1 = tab1.SearchScalar(condition, "sum(Fee1+Fee2+Fee3)");
                    if (obj1 != null && obj1.ToString().Trim() != String.Empty)
                    {
                        Fee0 = double.Parse(obj1.ToString());
                    }
                }
            }
            return JMath.Round1(Fee0/UnitRate,2);
        }



        //售票服务费---（根据列车的收入和一个比例进行计算，分动车比例和普通车比例）
        private double GetFee4(bool sum, String trainType)
        {
            double Fee0 = 0;
            String temp1 = String.Empty;
            if (String.IsNullOrEmpty(this.TrainName) == false)
            {
                String[] arr1 = this.TrainName.Split('/');
                temp1 = this.TrainName;
                foreach (String m in arr1)
                {
                    temp1 = temp1 + "," + m;
                }

                if (String.IsNullOrEmpty(temp1) == false)
                {
                    List<SearchField> condition = new List<SearchField>();
                    tab1.TableName = "NEWTRAINSHOUROU";
                    condition.Add(new SearchField("TRAINNAME", temp1, SearchOperator.Collection));
                    condition.Add(new SearchField("BYEAR", this.Year + "", SearchFieldType.NumericType));
                    if (sum == false)
                    {
                        condition.Add(new SearchField("BMONTH", this.Month + "", SearchFieldType.NumericType));
                    }
                    else
                    {
                        condition.Add(new SearchField("BMONTH", this.Month + "", SearchOperator.SmallerAndEqual,
                            SearchFieldType.NumericType));
                    }

                    object obj1 = tab1.SearchScalar(condition, "sum(SHOUROU1)");
                    if (obj1 != null && obj1.ToString().Trim() != String.Empty)
                    {
                        Fee0 = double.Parse(obj1.ToString());
                    }
                }
            }

            double Rate0 = 1;
            if (trainType == "动车组")
            {
                Rate0 = TrainProfile.SaleRateForHigh/100d;
            }
            else
            {
                Rate0 = TrainProfile.SaleRateForComm/100d;
            }

            //得到比例
            return JMath.Round1(Fee0 * Rate0/UnitRate,2);
        }


        //旅客服务费--（根据列车运送的人数进行计算）
        private double GetFee5(bool sum, String trainType)
        {
            double Fee0 = 0;
            String temp1 = String.Empty;
            if (String.IsNullOrEmpty(this.TrainName) == false)
            {
                String[] arr1 = this.TrainName.Split('/');
                temp1 = this.TrainName;
                foreach (String m in arr1)
                {
                    temp1 = temp1 + "," + m;
                }

                if (String.IsNullOrEmpty(temp1) == false)
                {
                    List<SearchField> condition = new List<SearchField>();
                    tab1.TableName = "NEWTRAINSERVERPEOPLE";
                    condition.Add(new SearchField("TRAINNAME", temp1, SearchOperator.Collection));
                    condition.Add(new SearchField("BYEAR", this.Year + "", SearchFieldType.NumericType));
                    if (sum == false)
                    {
                        condition.Add(new SearchField("BMONTH", this.Month + "", SearchFieldType.NumericType));
                    }
                    else
                    {
                        condition.Add(new SearchField("BMONTH", this.Month + "", SearchOperator.SmallerAndEqual,
                            SearchFieldType.NumericType));
                    }

                    object obj1 = tab1.SearchScalar(condition, "sum(PC1)");
                    if (obj1 != null && obj1.ToString().Trim() != String.Empty)
                    {
                        Fee0 = double.Parse(obj1.ToString());
                    }
                }
            }

            double Rate0 = 1;
            if (trainType == "动车组")
            {
                Rate0 = TrainProfile.ServiceRateForHigh;
            }
            else
            {
                Rate0 = TrainProfile.ServiceRateForComm;
            }

            //得到比例
            return JMath.Round1(Fee0 * Rate0/UnitRate,2);
        }

        //列车上水费----(根据水站的数量和标准计算)
        private double GetFee6(bool sum, String trainType)
        {
            double result = 0;
            int waterCount = 2;
            double WaterRate = 0;
            if (trainType == "动车组" || trainType == "空调特快")
            {
                waterCount = 1;
            }
            if (trainType == "动车组")
            {
                WaterRate = TrainProfile.WaterRateForHigh;
            }
            else
            {
                WaterRate = TrainProfile.WaterRateForComm;
            }

            double fee0 = waterCount * WaterRate * 2 * 365;
            if (sum == false)
            {
                result = fee0 / 12.0;
            }
            else
            {
                result = (fee0 / 12.0) * this.Month;
            }
            return JMath.Round1(result/UnitRate,2);
        }

        //人员和工资附加费--OK
        private double GetFee7(bool sum)
        {
            double result = 0;
            Train train1 = NewTrainBU.GetTrainObject(this.TrainName);
            if (train1 != null)
            {
                double fee1 = train1.GetFee7();
                if (sum == false)
                {
                    result = fee1 / 12;
                }
                else
                {
                    result = this.Month * (fee1 / 12);
                }
            }
            return result;
        }

        //车辆折旧费--OK
        private double GetFee8(bool sum)
        {
            double result = 0;
            Train train1 = NewTrainBU.GetTrainObject(this.TrainName);
            if (train1 != null)
            {
                double fee1 = train1.GetFee8();
                if (sum == false)
                {
                    result = fee1 / 12;
                }
                else
                {
                    result = this.Month * (fee1 / 12);
                }
            }
            return result;
        }

        //日常检修成本--OK
        private double GetFee9(bool sum)
        {
            double result = 0;
            Train train1 = NewTrainBU.GetTrainObject(this.TrainName);
            if (train1 != null)
            {
                double fee1 = train1.GetFee9();
                if (sum == false)
                {
                    result = fee1 / 12;
                }
                else
                {
                    result = this.Month * (fee1 / 12);
                }
            }
            return result;
        }

        //定期检修成本--OK
        private double GetFee10(bool sum)
        {
            double result = 0;
            Train train1 = NewTrainBU.GetTrainObject(this.TrainName);
            if (train1 != null)
            {
                double fee1 = train1.GetFee10();
                if (sum == false)
                {
                    result = fee1 / 12;
                }
                else
                {
                    result = this.Month * (fee1 / 12);
                }
            }
            return result;
        }

        //车辆备用品消耗--OK
        private double GetFee11(bool sum)
        {
            double result = 0;
            Train train1 = NewTrainBU.GetTrainObject(this.TrainName);
            if (train1 != null)
            {
                double fee1 = train1.GetFee11();
                if (sum == false)
                {
                    result = fee1 / 12;
                }
                else
                {
                    result = this.Month * (fee1 / 12);
                }
            }
            return result;
        }

        //空调车用油--OK
        private double GetFee12(bool sum)
        {
            double result = 0;
            Train train1 = NewTrainBU.GetTrainObject(this.TrainName);
            if (train1 != null)
            {
                double fee1 = train1.GetFee12();
                if (sum == false)
                {
                    result = fee1 / 12;
                }
                else
                {
                    result = this.Month * (fee1 / 12);
                }
            }
            return result;
        }

        //人员其他的费用--OK
        private double GetFee13(bool sum)
        {
            double result = 0;
            Train train1 = NewTrainBU.GetTrainObject(this.TrainName);
            if (train1 != null)
            {
                double fee1 = train1.GetFee13();
                if (sum == false)
                {
                    result = fee1 / 12;
                }
                else
                {
                    result = this.Month * (fee1 / 12);
                }
            }
            return result;
        }

        //车辆购置利息--OK
        private double GetFee14(bool sum)
        {
            double result = 0;
            Train train1 = NewTrainBU.GetTrainObject(this.TrainName);
            if (train1 != null)
            {
                double fee1 = train1.GetFee14();
                if (sum == false)
                {
                    result = fee1 / 12;
                }
                else
                {
                    result = this.Month * (fee1 / 12);
                }
            }
            return result;
        }


        //车船费
        private double GetFee15(bool sum)
        {
            double result = 0;
            Train train1 = NewTrainBU.GetTrainObject(this.TrainName);
            if (train1 != null)
            {
                double fee1 = train1.GetFee15();
                if (sum == false)
                {
                    result = fee1 / 12;
                }
                else
                {
                    result = this.Month * (fee1 / 12);
                }
            }
            return result;
        }
        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            if (this.tab1 != null)
            {
                this.tab1.Close();
                this.tab1.Dispose();
            }
        }

        #endregion
    }
}
