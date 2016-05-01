namespace DependencyGraphGenerator
{
    partial class FormExportFile
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelSelectExporter = new System.Windows.Forms.Label();
            this.listBoxExporters = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(141, 41);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(141, 12);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // labelSelectExporter
            // 
            this.labelSelectExporter.AutoSize = true;
            this.labelSelectExporter.Location = new System.Drawing.Point(12, 9);
            this.labelSelectExporter.Name = "labelSelectExporter";
            this.labelSelectExporter.Size = new System.Drawing.Size(82, 13);
            this.labelSelectExporter.TabIndex = 8;
            this.labelSelectExporter.Text = "Select Exporter:";
            // 
            // listBoxExporters
            // 
            this.listBoxExporters.FormattingEnabled = true;
            this.listBoxExporters.Location = new System.Drawing.Point(15, 25);
            this.listBoxExporters.Name = "listBoxExporters";
            this.listBoxExporters.Size = new System.Drawing.Size(120, 95);
            this.listBoxExporters.TabIndex = 10;
            this.listBoxExporters.SelectedIndexChanged += new System.EventHandler(this.listBoxExporters_SelectedIndexChanged);
            // 
            // FormExportFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 126);
            this.Controls.Add(this.listBoxExporters);
            this.Controls.Add(this.labelSelectExporter);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Name = "FormExportFile";
            this.Text = "FormExportFile";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelSelectExporter;
        private System.Windows.Forms.ListBox listBoxExporters;
    }
}