namespace _04_生成连接字符串
{
    partial class MainFm
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
            this.btnGetStr = new System.Windows.Forms.Button();
            this.txtStr = new System.Windows.Forms.TextBox();
            this.pg4ConStr = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // btnGetStr
            // 
            this.btnGetStr.Location = new System.Drawing.Point(51, 60);
            this.btnGetStr.Name = "btnGetStr";
            this.btnGetStr.Size = new System.Drawing.Size(101, 23);
            this.btnGetStr.TabIndex = 0;
            this.btnGetStr.Text = "获得连接字符串";
            this.btnGetStr.UseVisualStyleBackColor = true;
            this.btnGetStr.Click += new System.EventHandler(this.BtnGetStr_Click);
            // 
            // txtStr
            // 
            this.txtStr.Location = new System.Drawing.Point(26, 89);
            this.txtStr.Multiline = true;
            this.txtStr.Name = "txtStr";
            this.txtStr.Size = new System.Drawing.Size(151, 194);
            this.txtStr.TabIndex = 1;
            // 
            // pg4ConStr
            // 
            this.pg4ConStr.Location = new System.Drawing.Point(215, 62);
            this.pg4ConStr.Name = "pg4ConStr";
            this.pg4ConStr.Size = new System.Drawing.Size(235, 376);
            this.pg4ConStr.TabIndex = 2;
            // 
            // MainFm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 450);
            this.Controls.Add(this.pg4ConStr);
            this.Controls.Add(this.txtStr);
            this.Controls.Add(this.btnGetStr);
            this.Name = "MainFm";
            this.Text = "生成连接字符串";
            this.Load += new System.EventHandler(this.MainFm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetStr;
        private System.Windows.Forms.TextBox txtStr;
        private System.Windows.Forms.PropertyGrid pg4ConStr;
    }
}

