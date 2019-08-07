using System;
using SqlUtilLib;
using System.Data.SqlClient;
using System.Data;

namespace _14_testSqlHelper
{
    class Program
    {
        static void Main(string[] args)
        {

            #region 一些调试 SqlHelper的代码
            //bool b = SqlHelper.ExecuteTran(() =>
            //{
            //    string sql = "update StudentInfo set StuName=@StuName where StuId=@StuId";
            //    SqlHelper.ExecuteNonQuery(sql, "张灵", 1);
            //});
            //Console.WriteLine(b);

            //Console.WriteLine(SqlUtilLib.SqlHelper.ExecutePageQeruy(sql, 3, 3, "ClassId", 1));

            //SqlHelper.SetPageConf(3, "ClassId", " ");
            //DataTable dt = SqlHelper.ExecutePageQeruy(sql, 3, 1);
            //DataTable dt = SqlHelper.ExecutePageQeruy(sql,3,3,"ClassId"," ",1);

            //foreach (DataRow dr in dt.Rows)
            //{
            //    foreach (DataColumn dc in dt.Columns)
            //    {
            //        Console.WriteLine(dr[dc]);
            //    }
            //}


            //SqlDataReader reader = SqlHelper.GetDataReader(sql, 2);
            //reader.Read();
            //Console.WriteLine(reader[1]);

            //Console.WriteLine(SqlHelper.GetConnStr());

            //Console.WriteLine(SqlHelper.ExecutePageQeruy(sql, 1, 5, "StuId", 1)); 
            #endregion

            //string sql = "insert DemoLastDay(Name) output inserted.Id values (@name)";
            //Console.WriteLine(SqlHelper.ExecuteScalar(sql, "李四"));

            #region 插入Guid类型的数据

            string sql = "Insert into DemoGuid(Id,name) values(@Id,@Name)";


            // Guid.NewGuid();  方法获得guid
            Console.WriteLine(SqlHelper.ExecuteNonQuery(sql,Guid.NewGuid(),"李四"));

            #endregion

            Console.ReadKey();
        }
    }
}
