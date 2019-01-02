namespace TigerLookupDataChecker
{
    partial class FrmSqlGenerator
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
            this.btnGenerateLookupSqls = new System.Windows.Forms.Button();
            this.btnGenerateSqls = new System.Windows.Forms.Button();
            this.gcFirms = new DevExpress.XtraGrid.GridControl();
            this.gvFirms = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDatasource = new DevExpress.XtraEditors.TextEdit();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnFillDatabases = new System.Windows.Forms.Button();
            this.lbDatabases = new System.Windows.Forms.ListBox();
            this.txtDatabase = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUsername = new DevExpress.XtraEditors.TextEdit();
            this.txtDbPattern = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnFillFirms = new System.Windows.Forms.Button();
            this.btnCheckConnection = new System.Windows.Forms.Button();
            this.txtTemplateSql = new System.Windows.Forms.RichTextBox();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnExecuteSql = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcFirms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFirms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDatasource.Properties)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDatabase.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDbPattern.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnGenerateLookupSqls);
            this.panel1.Controls.Add(this.btnExecuteSql);
            this.panel1.Controls.Add(this.btnGenerateSqls);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 582);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(935, 36);
            this.panel1.TabIndex = 0;
            // 
            // btnGenerateLookupSqls
            // 
            this.btnGenerateLookupSqls.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnGenerateLookupSqls.Location = new System.Drawing.Point(343, 0);
            this.btnGenerateLookupSqls.Name = "btnGenerateLookupSqls";
            this.btnGenerateLookupSqls.Size = new System.Drawing.Size(223, 36);
            this.btnGenerateLookupSqls.TabIndex = 2;
            this.btnGenerateLookupSqls.Text = "Generate Lookup Sqls From Input Xmls";
            this.btnGenerateLookupSqls.UseVisualStyleBackColor = true;
            this.btnGenerateLookupSqls.Click += new System.EventHandler(this.btnGenerateLookupSqls_Click);
            // 
            // btnGenerateSqls
            // 
            this.btnGenerateSqls.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnGenerateSqls.Location = new System.Drawing.Point(762, 0);
            this.btnGenerateSqls.Name = "btnGenerateSqls";
            this.btnGenerateSqls.Size = new System.Drawing.Size(173, 36);
            this.btnGenerateSqls.TabIndex = 1;
            this.btnGenerateSqls.Text = "Generate Sqls For Every Firm";
            this.btnGenerateSqls.UseVisualStyleBackColor = true;
            this.btnGenerateSqls.Click += new System.EventHandler(this.btnGenerateSqls_Click);
            // 
            // gcFirms
            // 
            this.gcFirms.Location = new System.Drawing.Point(290, 62);
            this.gcFirms.MainView = this.gvFirms;
            this.gcFirms.Name = "gcFirms";
            this.gcFirms.Size = new System.Drawing.Size(633, 212);
            this.gcFirms.TabIndex = 1;
            this.gcFirms.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvFirms});
            // 
            // gvFirms
            // 
            this.gvFirms.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gvFirms.GridControl = this.gcFirms;
            this.gvFirms.Name = "gvFirms";
            this.gvFirms.OptionsView.ShowFooter = true;
            this.gvFirms.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "IsSelected";
            this.gridColumn4.FieldName = "IsSelected";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "IsSelected", "{0}")});
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "FirmNr";
            this.gridColumn1.FieldName = "FirmNr";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "FirmName";
            this.gridColumn2.FieldName = "FirmName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Period";
            this.gridColumn3.FieldName = "Period";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Datasource";
            // 
            // txtDatasource
            // 
            this.txtDatasource.EditValue = "172.16.60.54";
            this.txtDatasource.Location = new System.Drawing.Point(95, 11);
            this.txtDatasource.Name = "txtDatasource";
            this.txtDatasource.Size = new System.Drawing.Size(134, 20);
            this.txtDatasource.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnFillDatabases);
            this.panel2.Controls.Add(this.lbDatabases);
            this.panel2.Controls.Add(this.txtDatabase);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtPassword);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtUsername);
            this.panel2.Controls.Add(this.txtDbPattern);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btnSelectAll);
            this.panel2.Controls.Add(this.btnClear);
            this.panel2.Controls.Add(this.btnFillFirms);
            this.panel2.Controls.Add(this.btnCheckConnection);
            this.panel2.Controls.Add(this.gcFirms);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtDatasource);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(935, 276);
            this.panel2.TabIndex = 4;
            // 
            // btnFillDatabases
            // 
            this.btnFillDatabases.Location = new System.Drawing.Point(542, 34);
            this.btnFillDatabases.Name = "btnFillDatabases";
            this.btnFillDatabases.Size = new System.Drawing.Size(123, 22);
            this.btnFillDatabases.TabIndex = 17;
            this.btnFillDatabases.Text = "Fill Databases";
            this.btnFillDatabases.UseVisualStyleBackColor = true;
            this.btnFillDatabases.Click += new System.EventHandler(this.btnFillDatabases_Click);
            // 
            // lbDatabases
            // 
            this.lbDatabases.FormattingEnabled = true;
            this.lbDatabases.Location = new System.Drawing.Point(20, 62);
            this.lbDatabases.Name = "lbDatabases";
            this.lbDatabases.Size = new System.Drawing.Size(264, 212);
            this.lbDatabases.TabIndex = 16;
            this.lbDatabases.SelectedIndexChanged += new System.EventHandler(this.lbDatabases_SelectedIndexChanged);
            // 
            // txtDatabase
            // 
            this.txtDatabase.EditValue = "master";
            this.txtDatabase.Location = new System.Drawing.Point(343, 36);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(134, 20);
            this.txtDatabase.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(239, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Selected Database";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(451, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.EditValue = "1234qqqQ";
            this.txtPassword.Location = new System.Drawing.Point(521, 11);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(134, 20);
            this.txtPassword.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(241, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Username";
            // 
            // txtUsername
            // 
            this.txtUsername.EditValue = "sa";
            this.txtUsername.Location = new System.Drawing.Point(311, 11);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(134, 20);
            this.txtUsername.TabIndex = 11;
            // 
            // txtDbPattern
            // 
            this.txtDbPattern.EditValue = "TIGERWINGS";
            this.txtDbPattern.Location = new System.Drawing.Point(95, 36);
            this.txtDbPattern.Name = "txtDbPattern";
            this.txtDbPattern.Size = new System.Drawing.Size(134, 20);
            this.txtDbPattern.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Database Pattern";
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(800, 34);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(123, 22);
            this.btnSelectAll.TabIndex = 7;
            this.btnSelectAll.Text = "Selected All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(800, 9);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(123, 22);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear Selection";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnFillFirms
            // 
            this.btnFillFirms.Location = new System.Drawing.Point(671, 34);
            this.btnFillFirms.Name = "btnFillFirms";
            this.btnFillFirms.Size = new System.Drawing.Size(123, 22);
            this.btnFillFirms.TabIndex = 5;
            this.btnFillFirms.Text = "Fill Firms";
            this.btnFillFirms.UseVisualStyleBackColor = true;
            this.btnFillFirms.Click += new System.EventHandler(this.btnFillFirms_Click);
            // 
            // btnCheckConnection
            // 
            this.btnCheckConnection.Location = new System.Drawing.Point(671, 9);
            this.btnCheckConnection.Name = "btnCheckConnection";
            this.btnCheckConnection.Size = new System.Drawing.Size(123, 22);
            this.btnCheckConnection.TabIndex = 4;
            this.btnCheckConnection.Text = "Check Connection";
            this.btnCheckConnection.UseVisualStyleBackColor = true;
            this.btnCheckConnection.Click += new System.EventHandler(this.btnCheckConnection_Click);
            // 
            // txtTemplateSql
            // 
            this.txtTemplateSql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTemplateSql.Location = new System.Drawing.Point(2, 20);
            this.txtTemplateSql.Name = "txtTemplateSql";
            this.txtTemplateSql.Size = new System.Drawing.Size(931, 284);
            this.txtTemplateSql.TabIndex = 5;
            this.txtTemplateSql.Text = "";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtTemplateSql);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 276);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(935, 306);
            this.groupControl1.TabIndex = 8;
            this.groupControl1.Text = "Template Sql Script";
            // 
            // btnExecuteSql
            // 
            this.btnExecuteSql.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExecuteSql.Location = new System.Drawing.Point(566, 0);
            this.btnExecuteSql.Name = "btnExecuteSql";
            this.btnExecuteSql.Size = new System.Drawing.Size(196, 36);
            this.btnExecuteSql.TabIndex = 3;
            this.btnExecuteSql.Text = "Execute Scripts Directly";
            this.btnExecuteSql.UseVisualStyleBackColor = true;
            this.btnExecuteSql.Click += new System.EventHandler(this.btnExecuteSql_Click);
            // 
            // FrmSqlGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 618);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmSqlGenerator";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcFirms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFirms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDatasource.Properties)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDatabase.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDbPattern.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl gcFirms;
        private DevExpress.XtraGrid.Views.Grid.GridView gvFirms;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtDatasource;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCheckConnection;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private System.Windows.Forms.Button btnFillFirms;
        private System.Windows.Forms.Button btnGenerateSqls;
        private System.Windows.Forms.RichTextBox txtTemplateSql;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnClear;
        private DevExpress.XtraEditors.TextEdit txtDbPattern;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtUsername;
        private DevExpress.XtraEditors.TextEdit txtDatabase;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lbDatabases;
        private System.Windows.Forms.Button btnFillDatabases;
        private System.Windows.Forms.Button btnGenerateLookupSqls;
        private System.Windows.Forms.Button btnExecuteSql;
    }
}

