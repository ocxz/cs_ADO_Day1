using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace _03_ConnectionPool连接池
{
    class Program
    {
        static void Main(string[] args)
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();
            int i = 0;

            #region 关闭连接池，执行1000次数据更新

            while (i < 1000)
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = "server=.\\SQLEXPRESS;uid=sa;pwd=980421cxz;database=dbTest4;Pooling=false";
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "update StudentInfo set StuGender=1 where StuId=10";
                        cmd.Connection = conn;
                        int r = cmd.ExecuteNonQuery();
                        //Console.WriteLine("执行完成，影响了{0}条记录", r);
                        //Console.ReadKey();
                    }
                }
                i++;
            }

            #endregion

            sw.Stop();
            Console.WriteLine("关闭连接池执行的时间：" + sw.Elapsed.Milliseconds);

            sw.Reset();
            sw.Restart();

            i = 0;
            #region 不关闭连接池，执行1000次数据跟新

            while (i < 1000)
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = "server=.\\SQLEXPRESS;uid=sa;pwd=980421cxz;database=dbTest4;";
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "update StudentInfo set StuGender=1 where StuId=10";
                        cmd.Connection = conn;
                        int r = cmd.ExecuteNonQuery();
                        //Console.WriteLine("执行完成，影响了{0}条记录", r);
                        //Console.ReadKey();
                    }
                }
                i++;
            }

            #endregion

            sw.Stop();
            Console.WriteLine("打开连接池执行的时间：" + sw.Elapsed.Milliseconds);



        }
    }
}
