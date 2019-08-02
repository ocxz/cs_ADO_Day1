namespace _05_登录_数据库完成校验_
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtUName = new System.Windows.Forms.TextBox();
            this.txtUPwd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lbUName = new System.Windows.Forms.Label();
            this.lbUPwd = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtUName
            // 
            this.txtUName.Location = new System.Drawing.Point(180, 50);
            this.txtUName.Name = "txtUName";
            this.txtUName.Size = new System.Drawing.Size(186, 21);
            this.txtUName.TabIndex = 0;
            this.txtUName.TextChanged += new System.EventHandler(this.TxtUName_TextChanged);
            this.txtUName.Leave += new System.EventHandler(this.TxtUName_Leave);
            // 
            // txtUPwd
            // 
            this.txtUPwd.Location = new System.Drawing.Point(180, 126);
            this.txtUPwd.Name = "txtUPwd";
            this.txtUPwd.PasswordChar = '*';
            this.txtUPwd.Size = new System.Drawing.Size(186, 21);
            this.txtUPwd.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(124, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "用户名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(124, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "密码";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(180, 221);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(127, 43);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // lbUName
            // 
            this.lbUName.AutoSize = true;
            this.lbUName.ForeColor = System.Drawing.Color.Red;
            this.lbUName.Location = new System.Drawing.Point(393, 59);
            this.lbUName.Name = "lbUName";
            this.lbUName.Size = new System.Drawing.Size(95, 12);
            this.lbUName.TabIndex = 5;
            this.lbUName.Text = "*用户名不能为空";
            this.lbUName.Visible = false;
            // 
            // lbUPwd
            // 
            this.lbUPwd.AutoSize = true;
            this.lbUPwd.ForeColor = System.Drawing.Color.Red;
            this.lbUPwd.Location = new System.Drawing.Point(393, 129);
            this.lbUPwd.Name = "lbUPwd";
            this.lbUPwd.Size = new System.Drawing.Size(83, 12);
            this.lbUPwd.TabIndex = 5;
            this.lbUPwd.Text = "*密码不能为空";
            this.lbUPwd.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 450);
            this.Controls.Add(this.lbUPwd);
            this.Controls.Add(this.lbUName);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUPwd);
            this.Controls.Add(this.txtUName);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUName;
        private System.Windows.Forms.TextBox txtUPwd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lbUName;
        private System.Windows.Forms.Label lbUPwd;
    }
}

