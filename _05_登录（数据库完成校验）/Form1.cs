using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _05_登录_数据库完成校验_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 登录名失去焦点时，进行登录名验证判断，只传入一个参数，判断非空、存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtUName_Leave(object sender, EventArgs e)
        {
            string msg;
            lbUName.Visible = !VerifierFactory.GetVerifier(VerifierKinds.LoginVerify).Verify(out msg, txtUName.Text.Trim());
            lbUName.Text = "*"+msg;
        }

        /// <summary>
        /// 登录名输入框输入时，错误提示消息消失
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtUName_TextChanged(object sender, EventArgs e)
        {
            lbUName.Visible = false;
        }

        /// <summary>
        /// 登录按钮按下时，进行登录验证，传入两个参数，若登录成功则关闭连接和cmd对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string msg;
            bool b = VerifierFactory.GetVerifier(VerifierKinds.LoginVerify).Verify(out msg, txtUName.Text.Trim(), txtUPwd.Text);
            MessageBox.Show(msg);

            if (b)
            {
                SqlUtils.CloseAll();
            }
        }

        /// <summary>
        /// 窗体加载时，初始化cmd对象，提高程序运行效率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // 开始就加载cmd
            SqlUtils.GetCommand();
        }
    }
}
