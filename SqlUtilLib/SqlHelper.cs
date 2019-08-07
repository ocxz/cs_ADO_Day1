using System;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace SqlUtilLib
{
    public static class SqlHelper
    {
        private static SqlConnection conn;
        private static SqlCommand cmd;
        private static string connStr;
        private static int _pageSize = 0;
        public static int PageSize
        {
            set
            {
                _pageSize = value <= 0 ? 1000 : value;
            }
            get
            {
                return _pageSize;
            }
        }
        private static string _orderColName = "id";
        public static string OrderColName
        {
            set
            {
                _orderColName = string.IsNullOrEmpty(value) ? "id" : value;
            }

            get
            {
                return _orderColName;
            }
        }
        private static string _orderStyle = " ";
        public static string OrderStyle
        {
            set
            {
                _orderStyle = value == "desc" ? "desc" : " ";
            }
            get
            {
                return _orderStyle;
            }
        }

        // 声明一个委托，用来处理事务内部
        public delegate void DelTran();


        #region 1、设置连接字符串  从配置文件中设置

        /// <summary>
        /// 获得连接字符串，必须要在App.config中配置name=SqlConn的连接字符串
        /// </summary>
        /// <returns>返回配置的连接字符串</returns>
        public static void SetConnStr()
        {
            try
            {
                connStr = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
            }
            catch
            {

                connStr = ConfigurationManager.AppSettings["SqlConn"].ToString();
            }
        }

        /// <summary>
        /// 指定连接字符串，设置连接字符串
        /// </summary>
        /// <param name="str">要是在的连接字符串</param>
        public static void SetConnStr(string str)
        {
            connStr = str;
        }

        /// <summary>
        /// 获得连接字符串，当连接字符串不存在时，默认从配置中获取
        /// </summary>
        /// <returns></returns>
        public static string GetConnStr()
        {
            if (connStr == null)
            {
                try
                {
                    SetConnStr();
                }
                catch
                {
                    return null;
                }
            }
            return connStr;
        }

        #endregion

        #region 2、GetConn() 获取连接对象
        /// <summary>
        /// 获取连接对象，单例的
        /// </summary>
        /// <returns>返回连接对象</returns>
        public static SqlConnection GetConn()
        {
            if (conn == null)
            {
                if (string.IsNullOrEmpty(connStr))
                {
                    SetConnStr();
                }
                conn = new SqlConnection(connStr);
                conn.Open();
            }
            return conn;
        }
        #endregion

        #region 3、GetCmd() 获得SqlCommand命令执行对象
        /// <summary>
        /// GetCmd 获得执行sqlCommand对象，可防止sql注入
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="pars">sql语句的相关参数</param>
        /// <returns></returns>
        public static SqlCommand GetCmd(string sql, params object[] pars)
        {
            SqlCommand cmd = GetCmd();
            cmd.CommandText = sql;
            if (pars.Length != 0)   // 有参数，sql注入，给cmd参数赋值
            {
                var rm = Regex.Matches(sql, @"@\w+");
                for (int i = 0; i < pars.Length; i++)
                {
                    cmd.Parameters.AddWithValue(rm[i].ToString(), pars[i]);
                }
            }

            return cmd;
        }

        /// <summary>
        /// 默认，没有参数，单例获取cmd对象，获取前清空参数列表
        /// </summary>
        /// <returns>获取到的cmd对象</returns>
        public static SqlCommand GetCmd()
        {
            if (cmd == null)
            {
                cmd = new SqlCommand();
                cmd.Connection = GetConn();
            }
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            return cmd;
        }

        /// <summary>
        /// 获取带有额外参数，注入的方法（不使用切割字符串的方法，获得参数）
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="exPars">额外注入的参数</param>
        /// <param name="cmdType">命令执行的类型</param>
        /// <param name="pars">注入的参数列表</param>
        /// <returns>返回cmd对象</returns>
        public static SqlCommand GetCmd(string sql, Hashtable exPars, CommandType cmdType, params object[] pars)
        {
            SqlCommand cmd = GetCmd(sql, pars);
            if (exPars != null && exPars.Count != 0)
            {
                foreach (string key in exPars.Keys)
                {
                    cmd.Parameters.AddWithValue(key, exPars[key]);
                }
            }
            cmd.CommandType = cmdType;
            return cmd;
        }

        /// <summary>
        /// 获取带有额外参数和out参数，注入的方法（不使用切割字符串的方法，获得参数）
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="exPars">额外参数列表</param>
        /// <param name="cmdType">cmd执行命令的类型</param>
        /// <param name="outParsName">out参数名</param>
        /// <param name="outParsType">out参数类型</param>
        /// <param name="pars">注入的参数列表</param>
        /// <returns>返回cmd对象</returns>
        public static SqlCommand GetCmd(string sql, Hashtable exPars, CommandType cmdType, SqlParameter outPar, params object[] pars)
        {
            SqlCommand cmd = GetCmd(sql, exPars, cmdType, pars);
            if (outPar != null)
            {
                outPar.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outPar);
            }
            return cmd;
        }
        #endregion

        #region 4、CloseAll()关闭所有对象
        /// <summary>
        /// 关闭所有，包括cmd、conn对象
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

        #region 5、ExecuteNonQuery(string sql,params string[] param) 执行sql语句，返回受影响的行数

        /// <summary>
        /// 执行sql非查询语句，支持参数注入
        /// </summary>
        /// <param name="sql">要执行sql语句</param>
        /// <param name="pars">参数</param>
        /// <returns>返回影响的行数</returns>
        public static int ExecuteNonQuery(string sql, params object[] pars)
        {
            return GetCmd(sql, pars).ExecuteNonQuery();
        }

        public static int ExecuteNonQuery(string sql, Hashtable exPars, CommandType cmdType, params object[] pars)
        {
            return GetCmd(sql, exPars, cmdType, pars).ExecuteNonQuery();
        }

        public static int ExecuteNonQuery(string sql, Hashtable exPars, CommandType cmdType, SqlParameter outPar, params object[] pars)
        {
            return GetCmd(sql, exPars, cmdType, outPar, pars).ExecuteNonQuery();
        }


        #endregion

        #region 6、ExecuteScalar(string sql, params string[] param) 执行sql查询语句，返回查询到的第一行第一列
        /// <summary>
        /// 执行sql查询语句，返回查询到的第一行第一列数据
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="pars">要注入的参数</param>
        /// <returns>查询到的结果</returns>
        public static object ExecuteScalar(string sql, params object[] pars)
        {
            return GetCmd(sql, pars).ExecuteScalar();
        }

        public static T ExecuteSclar<T>(string sql, params object[] pars)
            where T : class
            // where 是对T的约束
            // where T:class T必须是类
            // where T:new () T必须有默认构造函数
            // where T:UserInfo T必须是UserInfo的子类
        {
            return (T)GetCmd(sql, pars).ExecuteScalar();
        }

        public static object ExecuteScalar(string sql, Hashtable exPars, CommandType cmdType, params object[] pars)
        {
            return GetCmd(sql, exPars, cmdType, pars).ExecuteScalar();
        }

        public static object ExecuteScalar(string sql, Hashtable exPars, CommandType cmdType, SqlParameter outPar, params object[] pars)
        {
            return GetCmd(sql, exPars, cmdType, outPar, pars).ExecuteScalar();
        }

        #endregion

        #region 7、GetDataReader(string sql, params object[] param) 执行sql查询语句，返回查询到的SqlReader对象
        /// <summary>
        /// 执行sql查询语句，返回查询的指针SqlReader对象
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="param">要注入的参数</param>
        /// <returns>查询表单SqlReader对象</returns>
        public static SqlDataReader GetDataReader(string sql, params object[] param)
        {
            return GetCmd(sql, param).ExecuteReader();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="exPars"></param>
        /// <param name="cmdType"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public static SqlDataReader GetDataReader(string sql, Hashtable exPars, CommandType cmdType, params object[] pars)
        {
            return GetCmd(sql, exPars, cmdType, pars).ExecuteReader();
        }

        public static SqlDataReader GetDataReader(string sql, Hashtable exPars, CommandType cmdType, SqlParameter outPar, params object[] pars)
        {
            return GetCmd(sql, exPars, cmdType, outPar, pars).ExecuteReader();
        }
        #endregion

        #region 8、GetDataTable()执行sql查询语句，获得DataTable对象

        /// <summary>
        /// 执行sql查询语句，获得DataTable对象
        /// </summary>
        /// <param name="sql">要执行的sql查询语句</param>
        /// <returns>获得的DataTable对象</returns>
        public static DataTable GetDataTable(string sql, params object[] pars)
        {
            return GetDataTable(sql, null, CommandType.Text, null, pars);
        }

        public static DataTable GetDataTable(string sql, Hashtable exPars, CommandType cmdType, params object[] pars)
        {
            return GetDataTable(sql, exPars, cmdType, null, pars);
        }

        public static DataTable GetDataTable(string sql, Hashtable exPars, CommandType cmdType, SqlParameter outPar, params object[] pars)
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, GetConnStr()))
            {
                if (pars.Length != 0)   // 有参数，sql注入，给cmd参数赋值
                {
                    adapter.SelectCommand.Parameters.Clear();   // 清空原来的参数列表
                    var rm = Regex.Matches(sql, @"@\w+");
                    for (int i = 0; i < pars.Length; i++)
                    {
                        adapter.SelectCommand.Parameters.AddWithValue(rm[i].ToString(), pars[i]);
                    }
                }

                if (exPars != null || exPars.Count > 0)
                {
                    foreach (string key in exPars.Keys)
                    {
                        adapter.SelectCommand.Parameters.AddWithValue(key, exPars[key]);
                    }
                }

                if (outPar != null)
                {
                    outPar.Direction = ParameterDirection.Output;
                    adapter.SelectCommand.Parameters.Add(outPar);
                }
                adapter.SelectCommand.CommandType = cmdType;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        #endregion

        #region 9、设置分页查询配置

        /// <summary>
        /// 设置分页查询参数，包括页容量，排序列名，排序方式
        /// </summary>
        /// <param name="pageSize">页容量</param>
        /// <param name="orderColName">排序列名</param>
        /// <param name="orderStyle">排序方式</param>
        public static void SetPageConf(int pageSize, string orderColName, string orderStyle)
        {
            PageSize = pageSize;
            OrderColName = orderColName;
            OrderStyle = orderStyle;
        }

        #endregion

        #region 10、分页查询，得到页数 页数从第1页开始

        /// <summary>
        /// 执行sql语句，获得页数，从1开始
        /// </summary>
        /// <param name="sql">查询的sql语句</param>
        /// <param name="pars">查询参数</param>
        /// <returns>返回页数</returns>
        public static int GetTableMaxPage(string sql, object[] pars)
        {
            return (int)ExecuteNonQuery(sql, pars) / PageSize;
        }

        public static int GetTableMaxPage(string sql, Hashtable exPars, CommandType cmdType, SqlParameter outPar, params object[] pars)
        {
            return (int)ExecuteNonQuery(sql, exPars, cmdType, outPar, pars) / PageSize;
        }

        public static int GetTableMaxPage(string sql, Hashtable exPars, CommandType cmdType, params object[] pars)
        {
            return (int)ExecuteNonQuery(sql, exPars, cmdType, pars) / PageSize;
        }

        #endregion

        #region 11、分页查询，得到dataTable
        /// <summary>
        /// 分页查询，获得查询结果DataTable
        /// </summary>
        /// <param name="sql">要查询的语句</param>
        /// <param name="page">查询页</param>
        /// <param name="pars">查询参数</param>
        /// <returns>返回查询的结果</returns>
        public static DataTable ExecutePageQeruy(string sql, int page, params object[] pars)
        {
            string hsql;
            object[] ps = HanderPageStrAndPars(sql, out hsql, page, pars);
            return GetDataTable(hsql, ps);
        }

        public static DataTable ExecutePageQeruy(string sql, Hashtable exPars, CommandType cmdType, SqlParameter outPar, params object[] pars)
        {
            return GetDataTable(sql, exPars, cmdType, outPar, pars);
        }

        public static DataTable ExecutePageQeruy(string sql, Hashtable exPars, CommandType cmdType, params object[] pars)
        {
            return GetDataTable(sql, exPars, cmdType, pars);
        }

        public static DataTable ExecutePageQeruy(string sql, int page, int pageSize, string orderColName, string orderStyle, params object[] pars)
        {
            SetPageConf(pageSize, orderColName, orderStyle);
            return ExecutePageQeruy(sql, page, pars);
        }

        #endregion

        #region 12、object[] HanderPageStrAndPars(string sql, out string hsql, int page, params object[] pars) 处理分页查询

        /// <summary>
        /// 处理分页查询，生成相应的查询语句，和参数
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="hsql">处理后的语句</param>
        /// <param name="page">查询页</param>
        /// <param name="pars">参数列表</param>
        /// <returns>out hsql返回处理后的语句，返回处理后的参数列表</returns>
        private static object[] HanderPageStrAndPars(string sql, out string hsql, int page, params object[] pars)
        {
            // select * from (select *, ROW_NUMBER() over(order by StuId) as num from StudentInfo where ClassId = 1) as Twhere T.num between(@bi-1)*@ps + 1 and @bi*@ps

            // 将 from 换成, ROW_NUMBER() over(order by StuId) as num from StudentInfo where ClassId = 1
            // 生成的 sql语句为select *, ROW_NUMBER() over(order by StuId) as num
            string sql1 = Regex.Replace(sql, @" from", string.Format(",ROW_NUMBER() over(order by {0}) as num from", OrderColName + OrderStyle));
            string sql2 = "select {0} from({1}) as T where T.num between(@sfadfsdafsd-1)*@asdfgdafd+1 and @sfadfsdafsd*@asdfgdafd";

            // 得到sql语句中 select 和 from之间的内容 如：*  或者 StuId,StuName
            string temp = sql.Split(new string[] { "from", "select" }, StringSplitOptions.RemoveEmptyEntries)[0];

            // 如果是：* 则中间值为*   如果不是* 则为 StuId,StuName,num
            string temp2 = temp.Contains("*") ? "*" : temp + ",num";
            hsql = string.Format(sql2, temp2, sql1);

            object[] pars2;
            if (pars != null || pars.Length > 0)
            {
                pars2 = new object[pars.Length + 2];
                pars.CopyTo(pars2, 0);
                pars2[pars.Length] = page;
                pars2[pars.Length + 1] = PageSize;

            }
            else
            {
                pars2 = new object[2];
                pars2[0] = page;
                pars2[1] = PageSize;
            }
            return pars2;
        }

        #endregion

        #region 13、分页查询，得到SqlDataReader

        /// <summary>
        /// 分页查询，得到查询结果的SqlDataReader
        /// </summary>
        /// <param name="sql">查询sql语句</param>
        /// <param name="page">查询页</param>
        /// <param name="pars">查询参数</param>
        /// <returns>返回查询结果，SqlDataReader</returns>
        public static SqlDataReader ExecutePageQueryReader(string sql, int page, params object[] pars)
        {
            string hsql;
            object[] ps = HanderPageStrAndPars(sql, out hsql, page, pars);
            return GetDataReader(hsql, ps);
        }

        public static SqlDataReader ExecutePageQueryReader(string sql, int page, int pageSize, string orderColName, string orderStyle, params object[] pars)
        {
            SetPageConf(pageSize, orderColName, orderStyle);
            return ExecutePageQueryReader(sql, page, pars);
        }

        public static SqlDataReader ExecutePageQueryReader(string sql, Hashtable exPars, CommandType cmdType, SqlParameter outPar, params object[] pars)
        {
            return GetDataReader(sql, exPars, cmdType, outPar, pars);
        }

        public static SqlDataReader ExecutePageQueryReader(string sql, Hashtable exPars, CommandType cmdType, params object[] pars)
        {
            return GetDataReader(sql, exPars, cmdType, pars);
        }

        #endregion

        #region 14、事务的执行，ExecuteTran(DelTran del) 返回事务执行结果
        /// <summary>
        /// 执行事务，传入一个无参无返回值方法，用于执行相应的sql操作，返回事务执行结果
        /// </summary>
        /// <param name="del">处理sql操作的方法委托（无参无返回值）</param>
        /// <returns>返回事务执行的结果，true Or false</returns>
        //public static bool ExecuteTran(DelTran del)
        //{
        //    SqlConnection conn = GetConn();
        //    SqlCommand cmd = GetCmd();
        //    SqlTransaction tran = conn.BeginTransaction();  // 初始化，并开启一个事务
        //    cmd.Transaction = tran;
        //    try
        //    {
        //        del();
        //        tran.Commit();
        //        return true;
        //    }
        //    catch
        //    {
        //        tran.Rollback();
        //        return false;
        //    }

        //} 
        public static bool ExecuteTran(DelTran del)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())  // 声明一个事务域
                {
                    del();   // 执行sql操作，委托函数
                    scope.Complete();   // 提交事务
                    return true;
                }
            }
            catch
            {
                return false;   //异常处理
            }
        }
        #endregion
    }
}
