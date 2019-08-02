using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _04_生成连接字符串
{
    public partial class MainFm : Form
    {
        public MainFm()
        {
            InitializeComponent();

            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.InitialCatalog = "dbTest4";
            scsb.DataSource = ".\\SQLEXPRESS";
            scsb.UserID = "sa";
            scsb.Password = "980421cxz";
            this.pg4ConStr.SelectedObject = scsb;

        }

        private void MainFm_Load(object sender, EventArgs e)
        {

        }

        private void BtnGetStr_Click(object sender, EventArgs e)
        {
            string str = this.pg4ConStr.SelectedObject.ToString();

            // 剪切板
            Clipboard.Clear();
            Clipboard.SetText(str);
            txtStr.Text = str;
            MessageBox.Show("已经放到剪切板" + str);
        }
    }
}
