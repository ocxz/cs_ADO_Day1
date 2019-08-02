using System;
using System.Configuration;

namespace _07_配置文件
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(ConfigurationManager.AppSettings["Version"]);
            Console.WriteLine(ConfigurationManager.AppSettings["SqlConn"]);

            string connStr = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
            Console.WriteLine(connStr);
            Console.ReadKey();
        }
    }
}
