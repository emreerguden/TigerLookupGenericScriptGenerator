namespace TigerLookupDataChecker
{
    partial class FrmMain
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
            this.btnShowLookupDataGenerator = new System.Windows.Forms.Button();
            this.btnShowSqlScriptGenerator = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnShowLookupDataGenerator
            // 
            this.btnShowLookupDataGenerator.Location = new System.Drawing.Point(12, 12);
            this.btnShowLookupDataGenerator.Name = "btnShowLookupDataGenerator";
            this.btnShowLookupDataGenerator.Size = new System.Drawing.Size(260, 101);
            this.btnShowLookupDataGenerator.TabIndex = 0;
            this.btnShowLookupDataGenerator.Text = "Lookup Data Generator";
            this.btnShowLookupDataGenerator.UseVisualStyleBackColor = true;
            this.btnShowLookupDataGenerator.Click += new System.EventHandler(this.btnShowLookupDataGenerator_Click);
            // 
            // btnShowSqlScriptGenerator
            // 
            this.btnShowSqlScriptGenerator.Location = new System.Drawing.Point(12, 119);
            this.btnShowSqlScriptGenerator.Name = "btnShowSqlScriptGenerator";
            this.btnShowSqlScriptGenerator.Size = new System.Drawing.Size(260, 106);
            this.btnShowSqlScriptGenerator.TabIndex = 1;
            this.btnShowSqlScriptGenerator.Text = "DB Script Generator";
            this.btnShowSqlScriptGenerator.UseVisualStyleBackColor = true;
            this.btnShowSqlScriptGenerator.Click += new System.EventHandler(this.btnShowSqlScriptGenerator_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 236);
            this.Controls.Add(this.btnShowSqlScriptGenerator);
            this.Controls.Add(this.btnShowLookupDataGenerator);
            this.Name = "FrmMain";
            this.Text = "FrmMain";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnShowLookupDataGenerator;
        private System.Windows.Forms.Button btnShowSqlScriptGenerator;
    }
}