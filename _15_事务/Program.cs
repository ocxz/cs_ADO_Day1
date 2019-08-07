using System;
using System.Data.SqlClient;
using SqlUtilLib;

namespace _15_事务
{
    class Program
    {
        static void Main(string[] args)
        {
            // 使用ado.net 执行事务

            SqlConnection conn = SqlHelper.GetConn();
            SqlTransaction trans = conn.BeginTransaction();   // 开启事务
            try
            {
                string sql = @"update StudentInfo set StuName='张三' where StuId=@StuId
		                       --update StudentInfo set ClassId='我是一般的' where ClassId=@ClassId";
                SqlCommand cmd = SqlHelper.GetCmd(sql, 1, 1);
                cmd.Transaction = trans;  // 还需要把，事务给cmd命令
                cmd.ExecuteNonQuery();
                trans.Commit();   // 事务提交
            }
            catch
            {
                trans.Rollback();   // 事务回滚
            }

            Console.WriteLine("执行完成");
            Console.ReadKey();
        }
    }
}
