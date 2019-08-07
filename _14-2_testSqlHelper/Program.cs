using System;
using System.Data.SqlClient;
using SqlUtilLib;


namespace _14_2_testSqlHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlHelper.SetConnStr(@"server=.\SQLEXPRESS;uid=sa;pwd=980421cxz;database=dbTest4");
            using (SqlConnection conn = new SqlConnection())
            {

            }
            Console.ReadKey();
        }
    }
}
