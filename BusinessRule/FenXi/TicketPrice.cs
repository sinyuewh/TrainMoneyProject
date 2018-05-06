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
    
    public class TicketPrice
    {
        private double _YZPrice;//硬座基本票价
        public double YZPrice
        {
            set { this._YZPrice = value; }
            get { return this._YZPrice; }
        }
        private double _RZPrice;//软座基本票价
        public double RZPrice
        {
            set { this._RZPrice = value; }
            get { return this._RZPrice; }
        }
        private double _PTJKPrice;//普通加快快票价
        public double PTJKPrice
        {
            set { this._PTJKPrice = value; }
            get { return this._PTJKPrice; }
        }
        private double _KSJKPrice;//快速加快票价
        public double KSJKPrice
        {
            set { this._KSJKPrice = value; }
            get { return this._KSJKPrice; }
        }
        private double _YWSPrice;//硬卧上铺票价
        public double YWSPrice
        {
            set { this._YWSPrice = value; }
            get { return this._YWSPrice; }
        }
        private double _YWZPrice;//硬卧中铺票价
        public double YWZPrice
        {
            set { this._YWZPrice = value; }
            get { return this._YWZPrice; }
        }
        private double _YWXPrice;//硬卧下铺票价
        public double YWXPrice
        {
            set { this._YWXPrice = value; }
            get { return this._YWXPrice; }
        }
        private double _RWSPrice;//软卧上铺票价
        public double RWSPrice
        {
            set { this._RWSPrice = value; }
            get { return this._RWSPrice; }
        }
        private double _RWZPrice;
        public double RWZPrice
        {
            set { this._RWZPrice = value; }
            get { return this._RWZPrice; }
        }
        private double _RWXPrice;//软卧下铺票价
        public double RWXPrice
        {
            set { this._RWXPrice = value; }
            get { return this._RWXPrice; }
        }
        private double _KDPrice;//空调费
        public double KDPrice
        {
            set { this._KDPrice = value; }
            get { return this._KDPrice; }
        }
        private int _TrainType;//列车类型，1为普通列车，2为新型空调列车
        public int TrainType
        {
            set { this._TrainType = value; }
            get { return this._TrainType; }
        }


        /// <summary>
        /// 获取指定运行里程下所有的票价信息
        /// </summary>
        /// <param name="Yxlc">运行里程</param>
        /// <returns></returns>
        public bool GetPriceInfo(int Yxlc, int trainType)
        {
            bool result = false;
            JTable tab = new JTable("TicketPrice");
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("StartMile", Yxlc.ToString(), SearchOperator.SmallerAndEqual, SearchFieldType.NumericType));
            condition.Add(new SearchField("EndMile", Yxlc.ToString(), SearchOperator.BiggerAndEqual, SearchFieldType.NumericType));
            condition.Add(new SearchField("TrainType", trainType.ToString(), SearchOperator.Equal, SearchFieldType.NumericType));

            DataRow dr = tab.GetFirstDataRow(condition, new string[] { "*" });
            string sql = tab.CommandText;
            if (dr != null)
            {
                if (!Convert.IsDBNull(dr["YZPrice"]) && dr["YZPrice"] != null)
                {
                    this.YZPrice = Convert.ToDouble(dr["YZPrice"]);
                }
                if (!Convert.IsDBNull(dr["RZPrice"]) && dr["RZPrice"] != null)
                {
                    this.RZPrice = Convert.ToDouble(dr["RZPrice"]);
                }
                if (!Convert.IsDBNull(dr["PTJKPrice"]) && dr["PTJKPrice"] != null)
                {
                    this.PTJKPrice = Convert.ToDouble(dr["PTJKPrice"]);
                }

                if (!Convert.IsDBNull(dr["KSJKPrice"]) && dr["KSJKPrice"] != null)
                {
                    this.KSJKPrice = Convert.ToDouble(dr["KSJKPrice"]);
                }

                if (!Convert.IsDBNull(dr["YWSPrice"]) && dr["YWSPrice"] != null)
                {
                    this.YWSPrice = Convert.ToDouble(dr["YWSPrice"]);
                }
                if (!Convert.IsDBNull(dr["YWZPrice"]) && dr["YWZPrice"] != null)
                {
                    this.YWZPrice = Convert.ToDouble(dr["YWZPrice"]);
                }
                if (!Convert.IsDBNull(dr["YWXPrice"]) && dr["YWXPrice"] != null)
                {
                    this.YWXPrice = Convert.ToDouble(dr["YWXPrice"]);
                }

                if (!Convert.IsDBNull(dr["RWSPrice"]) && dr["RWSPrice"] != null)
                {
                    this.RWSPrice = Convert.ToDouble(dr["RWSPrice"]);
                }

                if (!Convert.IsDBNull(dr["RWZPrice"]) && dr["RWZPrice"] != null)
                {
                    this.RWZPrice = Convert.ToDouble(dr["RWZPrice"]);
                }

                if (!Convert.IsDBNull(dr["RWXPrice"]) && dr["RWXPrice"] != null)
                {
                    this.RWXPrice = Convert.ToDouble(dr["RWXPrice"]);
                }

                if (!Convert.IsDBNull(dr["KDPrice"]) && dr["KDPrice"] != null)
                {
                    this.KDPrice = Convert.ToDouble(dr["KDPrice"]);
                }
                result = true;

            }
            else
            {
                result = false;//没有找到对应里程的数据
            }
            tab.Dispose();
            tab.Close();
            return result;
        }


        /// <summary>
        /// 更新票价数据
        /// </summary>
        public static void UpdatePriceData(
            int TrainType,
            int StartMile, int EndMile,
            Dictionary<String,object> data1)
        {
            JTable tab = new JTable("TicketPrice");
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("StartMile", StartMile+"", SearchFieldType.NumericType));
            condition.Add(new SearchField("EndMile", EndMile+"", SearchFieldType.NumericType));
            condition.Add(new SearchField("TrainType", TrainType+"", SearchFieldType.NumericType));
            tab.EditData(data1, condition);
            tab.Close();
        }
    }
}
