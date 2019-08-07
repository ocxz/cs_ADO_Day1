using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _10_DataSet数据集
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 创建一个内存数据集
            DataSet ds = new DataSet("ds_dbTest4");

            #region 第一张表的定义和数据添加

            // 创建一张内存表
            DataTable dt1 = new DataTable("UserInfo");

            // 将内存表放入内存集中
            ds.Tables.Add(dt1);

            // 给表定义列
            DataColumn dcUId = new DataColumn("UserId", typeof(int));
            DataColumn dcUName = new DataColumn("UserName", typeof(string));
            DataColumn dcUpwd = new DataColumn("UserPwd", typeof(string));

            // 将列放入表中
            dt1.Columns.AddRange(new DataColumn[] { dcUId, dcUName, dcUpwd });

            // 给表添加数据
            dt1.Rows.Add(1, "张三", "123");
            dt1.Rows.Add(2, "李四", "123");
            dt1.Rows.Add(3, "王五", "123");

            #endregion

            #region 第二张表的定义和数据添加

            // 创建一张内存表
            DataTable dt2 = new DataTable("UserInfo2");

            // 将内存表放入内存集中
            ds.Tables.Add(dt2);

            // 给表定义列
            DataColumn dcUId2 = new DataColumn("UserId", typeof(int));
            DataColumn dcUName2 = new DataColumn("UserName", typeof(string));
            DataColumn dcUpwd2 = new DataColumn("UserPwd", typeof(string));

            // 将列放入表中
            dt2.Columns.AddRange(new DataColumn[] { dcUId2, dcUName2, dcUpwd2 });

            // 给表添加数据
            dt2.Rows.Add(1, "张三", "123");
            dt2.Rows.Add(2, "李四", "123");
            dt2.Rows.Add(3, "王五", "123");

            #endregion

            #region 循环遍历，将数据取出，首先循环变量表，如何循环遍历列，最后取出

            foreach (DataTable dt in ds.Tables)  // 循环遍历表
            {
                foreach (DataColumn dc0 in dt.Columns)   // 循环遍历列，拿取列名
                {
                    Console.Write(dc0.ColumnName);
                    Console.Write(" ");
                }
                Console.WriteLine();

                foreach (DataRow dr in dt.Rows)   // 循环遍历行 拿取每行值
                {
                    foreach (DataColumn dc in dt.Columns)   // 循环遍历列，拿取每行每列值
                    {
                        Console.Write(dr[dc]);
                        Console.Write(" ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("***********************");
            }

            #endregion
        }
    }
}
