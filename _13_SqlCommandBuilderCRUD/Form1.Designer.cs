namespace _13_SqlCommandBuilderCRUD
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
            this.dgvUserInfo = new System.Windows.Forms.DataGridView();
            this.StuId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StuName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StuGender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StuPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StuEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClassId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvUserInfo
            // 
            this.dgvUserInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StuId,
            this.StuName,
            this.StuGender,
            this.StuPhone,
            this.StuEmail,
            this.ClassId});
            this.dgvUserInfo.Location = new System.Drawing.Point(100, 39);
            this.dgvUserInfo.Name = "dgvUserInfo";
            this.dgvUserInfo.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvUserInfo.RowTemplate.Height = 23;
            this.dgvUserInfo.Size = new System.Drawing.Size(644, 229);
            this.dgvUserInfo.TabIndex = 0;
            // 
            // StuId
            // 
            this.StuId.DataPropertyName = "StuId";
            this.StuId.Frozen = true;
            this.StuId.HeaderText = "学生编号";
            this.StuId.Name = "StuId";
            // 
            // StuName
            // 
            this.StuName.DataPropertyName = "StuName";
            this.StuName.Frozen = true;
            this.StuName.HeaderText = "学生姓名";
            this.StuName.Name = "StuName";
            // 
            // StuGender
            // 
            this.StuGender.DataPropertyName = "StuGender";
            this.StuGender.Frozen = true;
            this.StuGender.HeaderText = "性别";
            this.StuGender.Name = "StuGender";
            // 
            // StuPhone
            // 
            this.StuPhone.DataPropertyName = "StuPhone";
            this.StuPhone.Frozen = true;
            this.StuPhone.HeaderText = "联系方式";
            this.StuPhone.Name = "StuPhone";
            // 
            // StuEmail
            // 
            this.StuEmail.DataPropertyName = "StuEmail";
            this.StuEmail.Frozen = true;
            this.StuEmail.HeaderText = "常用邮箱";
            this.StuEmail.Name = "StuEmail";
            // 
            // ClassId
            // 
            this.ClassId.DataPropertyName = "ClassId";
            this.ClassId.Frozen = true;
            this.ClassId.HeaderText = "班级编号";
            this.ClassId.Name = "ClassId";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(100, 318);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgvUserInfo);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUserInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn StuId;
        private System.Windows.Forms.DataGridViewTextBoxColumn StuName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StuGender;
        private System.Windows.Forms.DataGridViewTextBoxColumn StuPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn StuEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClassId;
        private System.Windows.Forms.Button btnSave;
    }
}

