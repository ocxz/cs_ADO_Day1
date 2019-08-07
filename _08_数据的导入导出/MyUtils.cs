using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08_数据的导入导出
{
    public static class MyUtils
    {
        public delegate List<string[]> DelAnalysisFile(string filePath, int row);

        /// <summary>
        /// 分解文本文件，根据分隔符将每行分解成字符数组存放List中
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="row">从那行开始(索引值从1开始）</param>
        /// <param name="separator">分隔符</param>
        /// <returns>分割后的List,每一条List代表一行数据</returns>
        public static List<string[]> AnalysisFile(string filePath, int row, string separator)
        {
            using (StreamReader reader = new StreamReader(filePath.Trim(), Encoding.Default))
            {
                string line;
                List<string[]> rows = new List<string[]>();
                for (int i = 0; i < row; i++)
                {
                    reader.ReadLine();
                }

                while (!string.IsNullOrEmpty(line = reader.ReadLine()))
                {
                    rows.Add(line.Split(new string[] { separator }, StringSplitOptions.None));
                }
                return rows;
            }
        }

        /// <summary>
        /// 添加字典数据到数据库中
        /// </summary>
        /// <param name="tbName">表名</param>
        /// <param name="datas">字典数据</param>
        /// <returns>返回添加的数据量</returns>
        public static int AddDataToSql(string tbName, string[] colName, List<string[]> rows)
        {
            // insert tbName() values(),(),()
            StringBuilder sqlBuilder = new StringBuilder("insert " + tbName);
            if (colName != null && colName.Length != 0)   // 提供列，顺序添加
            {
                sqlBuilder.Append("(");
                for (int i = 0; i < colName.Length - 1; i++)
                {
                    sqlBuilder.Append(colName[i] + ",");
                }
                sqlBuilder.Append(colName[colName.Length - 1] + ") ");
            }

            sqlBuilder.Append(" values");
            foreach (string[] item in rows)
            {
                sqlBuilder.Append("(");
                for (int i = 0; i < item.Length - 1; i++)
                {
                    sqlBuilder.Append(string.Format("'{0}',", item[i]));
                }

                sqlBuilder.Append(string.Format("'{0}'),", item[item.Length - 1]));
            }
            string sql = sqlBuilder.ToString();

            //// 测试
            //Console.WriteLine(sql);
            //sql = sql.Substring(0, sql.Length - 1);
            //return 0;
            sql = sql.Substring(0, sql.Length - 1);
            return SqlUtils.ExeNotQueryCmd(sql);

        }

        /// <summary>
        /// 利用委托的方式，将数据添加到数据库中
        /// </summary>
        /// <param name="tbName">数据库名字</param>
        /// <param name="colName">指定的类名，默认顺序添加</param>
        /// <param name="del">委托，用户分解数据文件，获得List<string[]>数据</string></param>
        /// <returns></returns>
        public static int AddDataToSql(string tbName, string[] colName, Delegate del)
        {
            return AddDataToSql(tbName, colName, del);
        }

        //public static void ObjectToTable()



    }
}
