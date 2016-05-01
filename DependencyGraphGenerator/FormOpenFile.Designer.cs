namespace DependencyGraphGenerator
{
    partial class FormOpenFile
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxExcludeVendors = new System.Windows.Forms.TextBox();
            this.labelExcludeVendors = new System.Windows.Forms.Label();
            this.buttonSelectFiles = new System.Windows.Forms.Button();
            this.labelExecutables = new System.Windows.Forms.Label();
            this.textBoxFiles = new System.Windows.Forms.TextBox();
            this.buttonFileList = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(242, 64);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(161, 64);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxExcludeVendors
            // 
            this.textBoxExcludeVendors.Location = new System.Drawing.Point(111, 38);
            this.textBoxExcludeVendors.Name = "textBoxExcludeVendors";
            this.textBoxExcludeVendors.Size = new System.Drawing.Size(169, 20);
            this.textBoxExcludeVendors.TabIndex = 2;
            // 
            // labelExcludeVendors
            // 
            this.labelExcludeVendors.AutoSize = true;
            this.labelExcludeVendors.Location = new System.Drawing.Point(12, 41);
            this.labelExcludeVendors.Name = "labelExcludeVendors";
            this.labelExcludeVendors.Size = new System.Drawing.Size(93, 13);
            this.labelExcludeVendors.TabIndex = 3;
            this.labelExcludeVendors.Text = "Exclude Vendor(s)";
            // 
            // buttonSelectFiles
            // 
            this.buttonSelectFiles.Location = new System.Drawing.Point(286, 6);
            this.buttonSelectFiles.Name = "buttonSelectFiles";
            this.buttonSelectFiles.Size = new System.Drawing.Size(36, 23);
            this.buttonSelectFiles.TabIndex = 4;
            this.buttonSelectFiles.Text = "...";
            this.buttonSelectFiles.UseVisualStyleBackColor = true;
            this.buttonSelectFiles.Click += new System.EventHandler(this.buttonSelectFiles_Click);
            // 
            // labelExecutables
            // 
            this.labelExecutables.AutoSize = true;
            this.labelExecutables.Location = new System.Drawing.Point(12, 9);
            this.labelExecutables.Name = "labelExecutables";
            this.labelExecutables.Size = new System.Drawing.Size(65, 13);
            this.labelExecutables.TabIndex = 5;
            this.labelExecutables.Text = "Executables";
            this.labelExecutables.Click += new System.EventHandler(this.labelExecutables_Click);
            // 
            // textBoxFiles
            // 
            this.textBoxFiles.Location = new System.Drawing.Point(111, 9);
            this.textBoxFiles.Name = "textBoxFiles";
            this.textBoxFiles.Size = new System.Drawing.Size(169, 20);
            this.textBoxFiles.TabIndex = 6;
            this.textBoxFiles.TextChanged += new System.EventHandler(this.textBoxFiles_TextChanged);
            // 
            // buttonFileList
            // 
            this.buttonFileList.Location = new System.Drawing.Point(80, 64);
            this.buttonFileList.Name = "buttonFileList";
            this.buttonFileList.Size = new System.Drawing.Size(75, 23);
            this.buttonFileList.TabIndex = 7;
            this.buttonFileList.Text = "Load txt/csv";
            this.buttonFileList.UseVisualStyleBackColor = true;
            this.buttonFileList.Click += new System.EventHandler(this.buttonFileList_Click);
            // 
            // FormOpenFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 92);
            this.Controls.Add(this.buttonFileList);
            this.Controls.Add(this.textBoxFiles);
            this.Controls.Add(this.labelExecutables);
            this.Controls.Add(this.buttonSelectFiles);
            this.Controls.Add(this.labelExcludeVendors);
            this.Controls.Add(this.textBoxExcludeVendors);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Name = "FormOpenFile";
            this.Text = "FormOpenFile";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxExcludeVendors;
        private System.Windows.Forms.Label labelExcludeVendors;
        private System.Windows.Forms.Button buttonSelectFiles;
        private System.Windows.Forms.Label labelExecutables;
        private System.Windows.Forms.TextBox textBoxFiles;
        private System.Windows.Forms.Button buttonFileList;
    }
}