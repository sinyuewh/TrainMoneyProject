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
    /// <summary>
    /// jrole ҵ����
    /// ���룺���ټ�
    /// ʱ�䣺2011��06��21��
    /// ����޸ģ�
    /// </summary>
    public partial class JroleBU
    {
        private const String TableName = "jrole";
        
        #region һ���DAL
        /// <summary>
        /// ���ܣ�����һ������
        /// <param name="data1">Ҫ���������</param>
        /// </summary>
        public void InsertData(Dictionary<string,object> data1)
        {
            JTable tab1 = new JTable(TableName);
            tab1.InsertData(data1);
            
            tab1.Close();
        }

        /// <summary>
        /// ���ܣ�ɾ������
        /// </summary>
        /// <param name="condition">Ҫɾ�����ݵ�����</param>
        public void DeleteData(List<SearchField> condition)
        {
            JTable tab1 = new JTable(TableName);
            tab1.DeleteData(condition);
            tab1.Close();
        }

        /// <summary>
        /// ���ܣ���������
        /// </summary>
        /// <param name="condition">�������ݵ�����</param>
        /// <param name="newData">�����ݵ�ֵ</param>
        public void UpdateData(List<SearchField> condition,Dictionary<string,object> newData)
        {
            JTable tab1 = new JTable(TableName);
            tab1.EditData(newData, condition);
            tab1.Close();
        }


        /// <summary>
        /// ���ܣ���ѯ�ض�������
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <param name="curPage">��ǰҳ</param>
        /// <param name="pageSize">ҳ��С</param>
        /// <param name="orderBy">������ʽ</param>
        /// <param name="totalRow">���ص�������</param>
        /// <param name="fs">Ҫ��ʾ���ֶ�</param>
        /// <returns></returns>
        public DataSet SearchData(List<SearchField> condition, int curPage,
            int pageSize,String orderBy, out int totalRow, params String[] fs)
        {
            totalRow = 0;
            JTable tab1 = new JTable(TableName);
            tab1.PageSize = pageSize;
            if (String.IsNullOrEmpty(orderBy) == false)
            {
                tab1.OrderBy = orderBy;
            }
            DataSet ds1 = tab1.SearchData(condition, curPage, fs);
            totalRow = tab1.GetTotalRow();
            tab1.Close();
            return ds1;
        }
        #endregion

        #region ҵ�񷽷�

        #endregion

        #region Example
        /*
        /// <summary>
        /// ���ܣ����ﴦ�����ο�
        /// </summary>
        public void ExampleForTrans()
        {
            JConnect conn1 = JConnect.GetConnect();     //�õ�Ĭ�ϵ����ݿ�����
            JTable tab1 =null;
            conn1.BeginTrans();
            try
            {
                tab1 = new JTable(conn1, TableName);
                //......

                conn1.CommitTrans();                  //�ύ���ﴦ��
            }
            catch (Exception err)
            {
                conn1.RollBackTrans();               //�ع����ﴦ��
            }
            finally
            {
                if (tab1 != null) tab1.Close();
            }
        }

        /// <summary>
        /// ���ܣ�ʹ��Update�������ݣ��ʺϲ�ѯ�����ݺ�������ύ
        /// </summary>
        public void ExampleForUpdate()
        {
            JTable tab1 = new JTable(TableName);
            List<SearchField> condition = new List<SearchField>();
            condition.Add(new SearchField("id", "1", SearchFieldType.NumericType));
            DataSet ds1 = tab1.SearchData(condition, -1, "test1", "test2","id");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                DataRow dr1 = ds1.Tables[0].Rows[0];
                dr1["test1"] = "abcd";
                dr1["test2"] = "dd";
                tab1.Update(ds1.Tables[0]);
            }
            tab1.Close();
        }*/
        #endregion
    }
}
