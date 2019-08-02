using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08_数据的导入导出
{
    public static class SqlUtils
    {
        private static SqlConnection conn;
        private static SqlCommand cmd;

        #region 1、GetConnection()获取连接对象，打开，不关闭，但一个程序只能获得一个

        /// <summary>
        /// 获得一个已经打开连接的SqlConnection连接对象，不支持多个连接对象的获取
        /// </summary>
        /// <returns>返回SqlConnection连接对象</returns>
        public static SqlConnection GetConnection()
        {
            if (conn == null)
            {
                string connStr = ConfigurationManager.AppSettings["SqlConn"];
                //string connStr = "server=.\\SQLEXPRESS;uid=sa;pwd=980421cxz;database=dbTest4";
                conn = new SqlConnection(connStr);
                conn.Open();
            }
            return conn;
        }

        #endregion

        #region 2、GetCommand()获得SqlCommand对象，也只能有一个

        /// <summary>
        /// 获得SqlCommand空对象，只能获得一个
        /// </summary>
        /// <returns>返回SqlCommand</returns>
        public static SqlCommand GetCommand()
        {
            if (cmd == null)
            {
                cmd = new SqlCommand();
                cmd.Connection = GetConnection();
            }
            return cmd;
        }


        #endregion

        #region 3、ExecuteCmd(string sql)执行Sql语句，返回影响的行数
        /// <summary>
        /// 执行Sql语句，返回影响的行数
        /// </summary>
        /// <param name="sql">要执行的Sql语句</param>
        /// <returns>影响的行数</returns>
        public static int ExeNotQueryCmd(string sql)
        {
            SqlCommand cmd = GetCommand();
            cmd.CommandText = sql;
            return cmd.ExecuteNonQuery();
        }
        #endregion

        #region 4、CloseAll()关闭所有，关闭conn和cmd，释放资源

        /// <summary>
        /// 关闭cmd、conn释放资源
        /// </summary>
        public static void CloseAll()
        {
            if (cmd != null)
            {
                cmd.Dispose();
                cmd = null;
            }
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }

        #endregion

        #region MyRegion

        #region 5、ExeQueryCmd(string sql)执行查询语句，返回查询到的第一行第一列的数据

        /// <summary>
        /// 执行查询语句，返回查询到的第一行第一列的数据
        /// </summary>
        /// <param name="sql">查询的sql语句</param>
        /// <returns>返回查询结果的第一行第一列的对象</returns>
        public static object ExeQueryCmd(string sql)
        {
            SqlCommand cmd = GetCommand();
            cmd.CommandText = sql;
            return cmd.ExecuteScalar();
        } 
        #endregion

        #endregion

    }
}
