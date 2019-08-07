using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using SqlUtilLib;

namespace _16_调用存储过程
{
    class Program
    {
        static void Main(string[] args)
        {
            string connStr = "server=.\\SQLEXPRESS;uid=sa;pwd=980421cxz;database=dbTest4";
            #region 手动写执行存储过程代码
            //using (SqlConnection conn = new SqlConnection(connStr))
            //{
            //    using (SqlCommand cmd = conn.CreateCommand())
            //    {
            //        conn.Open();
            //        string sql = "Pro_StudentInfo_GetPageStudentInfo";
            //        cmd.CommandText = sql;
            //        cmd.Parameters.Add(new SqlParameter("@pageSize", (object)2));
            //        cmd.Parameters.Add(new SqlParameter("@pageIndex", (object)2));

            //        // 创建一个输出参数
            //        SqlParameter sqlTotalParameter = new SqlParameter();
            //        sqlTotalParameter.ParameterName = "@TotalCount";
            //        sqlTotalParameter.SqlDbType = System.Data.SqlDbType.Int;

            //        // 设置参数为输出参数
            //        sqlTotalParameter.Direction = System.Data.ParameterDirection.Output;
            //        cmd.Parameters.Add(sqlTotalParameter);

            //        // 执行存储过程命令
            //        cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //        using (SqlDataReader reader = cmd.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                Console.WriteLine(reader[0] + "       " + reader[1]);
            //            }
            //        }

            //        // sql执行完成后，ado.net会自动给输出参数赋值
            //        Console.WriteLine(sqlTotalParameter.Value);
            //    }
            //} 
            #endregion

            #region SqlHelper执行

            //string sql = "Pro_StudentInfo_GetPageStudentInfo";
            //SqlHelper.SetConnStr(connStr);
            //SqlParameter outPar = new SqlParameter("@TotalCount",System.Data.SqlDbType.Int);
            //Hashtable exPars = new Hashtable();
            //exPars.Add("@pageSize", 2);
            //exPars.Add("@pageIndex", 2);
            //SqlDataReader reader = SqlHelper.GetDataReader(sql, exPars, System.Data.CommandType.StoredProcedure, outPar);
            //while (reader.Read())
            //{
            //    Console.WriteLine(reader[0] + "       " + reader[1]);
            //}
            //reader.Close();
            //Console.WriteLine(outPar.Value);

            #endregion

            #region SqlHelper执行存储过程代码，返回DataTable

            string sql = "Pro_StudentInfo_GetPageStudentInfo";
            SqlHelper.SetConnStr(connStr);
            SqlParameter outPar = new SqlParameter("@TotalCount", System.Data.SqlDbType.Int);
            Hashtable exPars = new Hashtable();
            exPars.Add("@pageSize", 2);
            exPars.Add("@pageIndex", 2);

            DataTable dt = SqlHelper.GetDataTable(sql, exPars, System.Data.CommandType.StoredProcedure,outPar);

            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    Console.WriteLine(dr[dc]);
                }
            }

            Console.WriteLine(outPar.Value);

            #endregion
        }
    }
}
