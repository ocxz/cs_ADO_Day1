using System;
using System.Data.SqlClient;

namespace _02_SqlCommand操作数据库
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 1、try-catch-finally写法
            //SqlConnection conn = new SqlConnection();  // 创建SqlConnection对象
            //SqlCommand cmd = new SqlCommand();   // 创建SqlCommand对象
            //try
            //{
            //    #region 1、连接并打开数据库

            //    string connStr = "server=.\\SQLEXPRESS;uid=sa;pwd=980421cxz;database=dbTest4";
            //    conn.ConnectionString = connStr;
            //    conn.Open();
            //    Console.WriteLine("打开成功");

            //    #endregion

            //    #region 2、使用SqlCommand对象，执行sql语句

            //    // 给命令指定连接对象
            //    cmd.Connection = conn;

            //    // 写命令脚本
            //    cmd.CommandText = "update StudentInfo set StuGender=0 where StuId=10";

            //    // 执行非查询命令  返回受影响的行数
            //    int r = cmd.ExecuteNonQuery();

            //    Console.WriteLine("操作了{0}行", r);

            //    Console.ReadKey();

            //    #endregion
            //}
            //finally
            //{
            //    conn.Close();
            //    conn.Dispose();
            //    cmd.Dispose();
            //} 
            #endregion

            #region 2、using写法

            using(SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "server=.\\SQLEXPRESS;uid=sa;pwd=980421cxz;database=dbTest4";
                conn.Open();
                Console.WriteLine("打开数据库完成");

                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "update StudentInfo set StuGender=1 where StuId=10";
                    cmd.Connection = conn;
                    int r = cmd.ExecuteNonQuery();
                    Console.WriteLine("操作完成，影响力{0}行", r);
                    Console.ReadKey();
                }
            }

            #endregion

        }
    }
}
