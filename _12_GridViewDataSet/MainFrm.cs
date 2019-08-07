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

namespace _12_GridViewDataSet
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            // 获取连接字符串
            string connStr = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;

            string sql = "select StuId, StuName, StuGender, StuPhone, StuEmail, ClassId from StudentInfo;" +
                "select ClassId, ClassName from ClassInfo";

            using(SqlDataAdapter adapter = new SqlDataAdapter(sql, connStr))
            {
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                this.dgvUserInfo.DataSource = ds.Tables[0];
            }
        }
    }
}
