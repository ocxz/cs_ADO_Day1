using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _08_数据的导入导出
{
    public partial class MianForm : Form
    {
        public MianForm()
        {
            InitializeComponent();
        }

        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "文本文件|*.txt|所有文件|*.*";
                ofd.Title = "选中要导入的数据文件";
                if(ofd.ShowDialog() == DialogResult.OK)   // 点击打开
                {
                    this.txFileName.Text = ofd.FileName;

                    // 导入数据
                    ImportData(txFileName.Text.Trim());
                }
            }
        }

        /// <summary>
        /// 数据导入工作
        /// </summary>
        /// <param name="filePath"></param>
        private void ImportData(string filePath)
        {
            // 第一步：拿取文件
            using (StreamReader reader = new StreamReader(filePath,Encoding.Default))
            {
                StringBuilder sqlBuilder = new StringBuilder("insert StudentInfo(");

                // 第一行是，所有列名信息
                string line = reader.ReadLine();
                string[] vars = line.Split(',');
                for (int i = 1; i < vars.Length-1; i++)
                {
                    sqlBuilder.Append(vars[i] + ",");
                }
                sqlBuilder.Append(vars[vars.Length - 1] + ") values");


                // 循环读取
                while (!string.IsNullOrEmpty(line = reader.ReadLine()))
                {
                    // 拆分，组成sql语句  去掉第一列的userId
                    //Console.WriteLine(line);
                    sqlBuilder.Append("(");
                    string[] vars2 = line.Split(',');
                    for (int i = 1; i < vars2.Length; i++)
                    {
                        string temp;
                        temp = i != vars2.Length-1 ? "'{0}'," : "'{0}'),";
                        sqlBuilder.Append(string.Format(temp, vars2[i]));
                    }
                }

                string sql = sqlBuilder.ToString();
                sql = sql.Substring(0,sql.LastIndexOf(','));

                int r = SqlUtils.ExeNotQueryCmd(sql);
                Console.WriteLine("导入完成，影响了{0}行",r);
            }
        }
    }
}
