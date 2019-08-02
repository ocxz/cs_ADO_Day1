using System;
using System.Data.SqlClient;

namespace _01_打开数据库
{
    class Program
    {
        static void Main(string[] args)
        {
            // 连接字符串：是我们对连接进行设置的字符串
            // 包括：服务器名、身份验证、用户名、密码、要连接的数据库
            string connStr = "server=.\\SQLEXPRESS;uid=sa;pwd=980421cxz;database=dbTest4";

            // 创建连接对象
            SqlConnection conn = new SqlConnection(connStr);

            // 打开数据库  成功失败抛出异常
            conn.Open();
            Console.WriteLine("数据库打开成功");

            // 关闭数据库，是否资源
            conn.Close();
            conn.Dispose();

            Console.ReadKey();
        }
    }
}
