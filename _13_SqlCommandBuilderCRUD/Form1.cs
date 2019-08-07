using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _13_SqlCommandBuilderCRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 获取连接对象
            string connStr = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;

            string sql = "select StuId, StuName, StuGender, StuPhone, StuEmail, ClassId from StudentInfo";

            using(SqlDataAdapter adapter = new SqlDataAdapter(sql, connStr))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                this.dgvUserInfo.DataSource =dt;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // 获取连接对象
            string connStr = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;

            //修改的sql一定要跟 查询的sql脚本一致
            string sql = "select StuId, StuName, StuGender, StuPhone, StuEmail, ClassId from StudentInfo";

            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connStr))
            {
                // 获取DataGridView中的数据，封装成DataTable
                DataTable dt = this.dgvUserInfo.DataSource as DataTable;

                // 帮助我们的adapter生成相关的CRUD  sqlComman对象
                using(SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adapter))
                {
                    // 将DataTable的数据，映射到数据库中
                    adapter.Update(dt);
                }
            }

            MessageBox.Show("保存成功");
        }
    }
}
