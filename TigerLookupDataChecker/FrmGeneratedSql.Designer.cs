namespace TigerLookupDataChecker
{
    partial class FrmGeneratedSql
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
            this.txtGeneratedSql = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTargetDatabse = new System.Windows.Forms.Label();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnExecute = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.txtExecutionLog = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtGeneratedSql
            // 
            this.txtGeneratedSql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGeneratedSql.Location = new System.Drawing.Point(0, 0);
            this.txtGeneratedSql.Name = "txtGeneratedSql";
            this.txtGeneratedSql.ReadOnly = true;
            this.txtGeneratedSql.Size = new System.Drawing.Size(814, 452);
            this.txtGeneratedSql.TabIndex = 0;
            this.txtGeneratedSql.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTargetDatabse);
            this.panel1.Controls.Add(this.btnCopy);
            this.panel1.Controls.Add(this.btnExecute);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 576);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1036, 47);
            this.panel1.TabIndex = 1;
            // 
            // lblTargetDatabse
            // 
            this.lblTargetDatabse.AutoSize = true;
            this.lblTargetDatabse.Location = new System.Drawing.Point(24, 17);
            this.lblTargetDatabse.Name = "lblTargetDatabse";
            this.lblTargetDatabse.Size = new System.Drawing.Size(0, 13);
            this.lblTargetDatabse.TabIndex = 2;
            // 
            // btnCopy
            // 
            this.btnCopy.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCopy.Location = new System.Drawing.Point(814, 0);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(111, 47);
            this.btnCopy.TabIndex = 1;
            this.btnCopy.Text = "Copy To Clipboard For Paste";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnExecute
            // 
            this.btnExecute.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExecute.Location = new System.Drawing.Point(925, 0);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(111, 47);
            this.btnExecute.TabIndex = 0;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtLog.Location = new System.Drawing.Point(814, 0);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(222, 576);
            this.txtLog.TabIndex = 2;
            this.txtLog.Text = "";
            // 
            // txtExecutionLog
            // 
            this.txtExecutionLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtExecutionLog.Location = new System.Drawing.Point(0, 452);
            this.txtExecutionLog.Name = "txtExecutionLog";
            this.txtExecutionLog.ReadOnly = true;
            this.txtExecutionLog.Size = new System.Drawing.Size(814, 124);
            this.txtExecutionLog.TabIndex = 3;
            this.txtExecutionLog.Text = "";
            // 
            // FrmGeneratedSql
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 623);
            this.Controls.Add(this.txtGeneratedSql);
            this.Controls.Add(this.txtExecutionLog);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.panel1);
            this.Name = "FrmGeneratedSql";
            this.Text = "FrmGeneratedSql";
            this.Load += new System.EventHandler(this.FrmGeneratedSql_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtGeneratedSql;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Label lblTargetDatabse;
        private System.Windows.Forms.RichTextBox txtExecutionLog;
    }
}