using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
                    int rows = ImportData2(txFileName.Text.Trim(), true);
                    MessageBox.Show(string.Format("导入完成，共导入{0}条数据",rows));
                }
            }
        }

        /// <summary>
        /// 将文本文件导入数据库中
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="IsIncludeCol">文件中，是否包含列名</param>
        /// <returns>返回导入的数据条数</returns>
        private int ImportData(string filePath,bool IsIncludeCol)
        {
            // 分解文本文件，得到数据list
            List<string[]> rows = MyUtils.AnalysisFile(filePath, 1, ",");

            // 将数据，加入到数据库中
           
            if (!IsIncludeCol)   // 判断原文件是否包含列名
            {
                rows.RemoveAt(0);  // 去掉第一行
            }

           return MyUtils.AddDataToSql("province", null, rows);
        }

        private int ImportData2(string filePath,bool IsIncludeCol)
        {
            return MyUtils.AddDataToSql("City", null,AnalyFile(filePath,1));
        }



        private List<string[]> AnalyFile(string filePath, int row)
        {
            List<string[]> rows = new List<string[]>();

            using(StreamReader read = new StreamReader(filePath, Encoding.Default))
            {
                for (int i = 0; i < row; i++)
                {
                    read.ReadLine();
                }

                int currow = 0;   // 记录当前都的行数
                string proName="";   // 记录省名
                string proId="";   // 记录省的ID
                string line;   // 记录当前行
                while (!string.IsNullOrEmpty(line=read.ReadLine()))
                {
                    currow++;
                    if (currow % 2 != 0)   // 如果读的是奇数行，则读到的是省名，记录并结束本次循环
                    {
                        proName = line.Trim();
                        string sql = string.Format("select proid,proName from Province where proName like'%{0}%'",line.Trim());

                        using (SqlDataReader sqlReader = SqlUtils.ExeQuery(sql))
                        {
                            while (sqlReader.Read())
                            {
                                proName = sqlReader["proName"].ToString();
                                proId = sqlReader["proid"].ToString();
                            }
                        }
                    }
                    else
                    {
                        string[] citys = line.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string item in citys)
                        {
                            rows.Add(new string[] { item, proName,proId });
                        }
                    }

                }


            }

            return rows;
        }


        private void MianForm_Load(object sender, EventArgs e)
        {

        }


    }
}
