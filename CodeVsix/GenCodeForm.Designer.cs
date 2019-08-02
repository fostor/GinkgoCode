namespace Fostor.CodeVsix
{
    partial class GenCodeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGenEntityClass = new System.Windows.Forms.Button();
            this.btnGenCode = new System.Windows.Forms.Button();
            this.ddlGenType = new System.Windows.Forms.ComboBox();
            this.btnSelFile = new System.Windows.Forms.Button();
            this.btnSelRoot = new System.Windows.Forms.Button();
            this.txtEntityPath = new System.Windows.Forms.TextBox();
            this.lblGenType = new System.Windows.Forms.Label();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.txtFolderRoot = new System.Windows.Forms.TextBox();
            this.lblFolderRoot = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rtbInfo = new System.Windows.Forms.RichTextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnGenEntityClass);
            this.panel1.Controls.Add(this.btnGenCode);
            this.panel1.Controls.Add(this.ddlGenType);
            this.panel1.Controls.Add(this.btnSelFile);
            this.panel1.Controls.Add(this.btnSelRoot);
            this.panel1.Controls.Add(this.txtEntityPath);
            this.panel1.Controls.Add(this.lblGenType);
            this.panel1.Controls.Add(this.lblFilePath);
            this.panel1.Controls.Add(this.txtFolderRoot);
            this.panel1.Controls.Add(this.lblFolderRoot);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 153);
            this.panel1.TabIndex = 0;
            // 
            // btnGenEntityClass
            // 
            this.btnGenEntityClass.Location = new System.Drawing.Point(609, 102);
            this.btnGenEntityClass.Name = "btnGenEntityClass";
            this.btnGenEntityClass.Size = new System.Drawing.Size(148, 23);
            this.btnGenEntityClass.TabIndex = 5;
            this.btnGenEntityClass.Text = "实体类生成工具";
            this.btnGenEntityClass.UseVisualStyleBackColor = true;
            this.btnGenEntityClass.Click += new System.EventHandler(this.btnGenEntityClass_Click);
            // 
            // btnGenCode
            // 
            this.btnGenCode.Location = new System.Drawing.Point(428, 102);
            this.btnGenCode.Name = "btnGenCode";
            this.btnGenCode.Size = new System.Drawing.Size(75, 23);
            this.btnGenCode.TabIndex = 4;
            this.btnGenCode.Text = "生成代码";
            this.btnGenCode.UseVisualStyleBackColor = true;
            this.btnGenCode.Click += new System.EventHandler(this.btnGenCode_Click);
            // 
            // ddlGenType
            // 
            this.ddlGenType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlGenType.FormattingEnabled = true;
            this.ddlGenType.Items.AddRange(new object[] {
            "应用服务层模块代码",
            "弹窗风格的增删改查界面",
            "新开页面的增删改查界面",
            "混合风格的增删改查界面"});
            this.ddlGenType.Location = new System.Drawing.Point(139, 102);
            this.ddlGenType.Name = "ddlGenType";
            this.ddlGenType.Size = new System.Drawing.Size(253, 23);
            this.ddlGenType.TabIndex = 3;
            // 
            // btnSelFile
            // 
            this.btnSelFile.Location = new System.Drawing.Point(659, 63);
            this.btnSelFile.Name = "btnSelFile";
            this.btnSelFile.Size = new System.Drawing.Size(57, 23);
            this.btnSelFile.TabIndex = 2;
            this.btnSelFile.Text = "选择";
            this.btnSelFile.UseVisualStyleBackColor = true;
            this.btnSelFile.Click += new System.EventHandler(this.btnSelFile_Click);
            // 
            // btnSelRoot
            // 
            this.btnSelRoot.Location = new System.Drawing.Point(659, 21);
            this.btnSelRoot.Name = "btnSelRoot";
            this.btnSelRoot.Size = new System.Drawing.Size(57, 23);
            this.btnSelRoot.TabIndex = 2;
            this.btnSelRoot.Text = "选择";
            this.btnSelRoot.UseVisualStyleBackColor = true;
            this.btnSelRoot.Click += new System.EventHandler(this.btnSelRoot_Click);
            // 
            // txtEntityPath
            // 
            this.txtEntityPath.Location = new System.Drawing.Point(138, 62);
            this.txtEntityPath.Name = "txtEntityPath";
            this.txtEntityPath.Size = new System.Drawing.Size(514, 25);
            this.txtEntityPath.TabIndex = 1;
            // 
            // lblGenType
            // 
            this.lblGenType.AutoSize = true;
            this.lblGenType.Location = new System.Drawing.Point(26, 106);
            this.lblGenType.Name = "lblGenType";
            this.lblGenType.Size = new System.Drawing.Size(105, 15);
            this.lblGenType.TabIndex = 0;
            this.lblGenType.Text = "生成代码类别:";
            this.lblGenType.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(12, 67);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(120, 15);
            this.lblFilePath.TabIndex = 0;
            this.lblFilePath.Text = "实体类文件路径:";
            // 
            // txtFolderRoot
            // 
            this.txtFolderRoot.Location = new System.Drawing.Point(138, 20);
            this.txtFolderRoot.Name = "txtFolderRoot";
            this.txtFolderRoot.Size = new System.Drawing.Size(514, 25);
            this.txtFolderRoot.TabIndex = 1;
            // 
            // lblFolderRoot
            // 
            this.lblFolderRoot.AutoSize = true;
            this.lblFolderRoot.Location = new System.Drawing.Point(12, 25);
            this.lblFolderRoot.Name = "lblFolderRoot";
            this.lblFolderRoot.Size = new System.Drawing.Size(120, 15);
            this.lblFolderRoot.TabIndex = 0;
            this.lblFolderRoot.Text = "模板文件根目录:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rtbInfo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 153);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 297);
            this.panel2.TabIndex = 1;
            // 
            // rtbInfo
            // 
            this.rtbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbInfo.Location = new System.Drawing.Point(0, 0);
            this.rtbInfo.Name = "rtbInfo";
            this.rtbInfo.ReadOnly = true;
            this.rtbInfo.Size = new System.Drawing.Size(800, 297);
            this.rtbInfo.TabIndex = 0;
            this.rtbInfo.Text = "";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // GenCodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "GenCodeForm";
            this.Text = "Ginkgo代码生成工具";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnGenCode;
        private System.Windows.Forms.ComboBox ddlGenType;
        private System.Windows.Forms.Button btnSelFile;
        private System.Windows.Forms.Button btnSelRoot;
        private System.Windows.Forms.TextBox txtEntityPath;
        private System.Windows.Forms.Label lblGenType;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.TextBox txtFolderRoot;
        private System.Windows.Forms.Label lblFolderRoot;
        private System.Windows.Forms.RichTextBox rtbInfo;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnGenEntityClass;
    }
}