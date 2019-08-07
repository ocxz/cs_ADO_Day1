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

namespace _11_SqlAdapter适配器
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            // 将UserInfo表中的数据加载到 窗体 DataGridView
            string connStr = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;

            //
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "select StuId, StuName, StuGender, StuPhone, StuEmail, ClassId from StudentInfo";
                // 创建适配器对象
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql,conn))
                {
                    // fill 填充
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // 展示
                    this.dgvUserInfo.DataSource = dt;
                }

            }
        }
    }
}
