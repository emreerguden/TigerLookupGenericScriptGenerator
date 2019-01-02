namespace TigerLookupDataChecker
{
    partial class FrmLookupData
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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbLookupType = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gcData = new DevExpress.XtraGrid.GridControl();
            this.gvData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colOldRecord = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNewRecord = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHasDifference = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtResultFile = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvData)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRefresh.Location = new System.Drawing.Point(0, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(154, 35);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSaveFile);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 415);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1078, 35);
            this.panel1.TabIndex = 7;
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSaveFile.Location = new System.Drawing.Point(877, 0);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(201, 35);
            this.btnSaveFile.TabIndex = 1;
            this.btnSaveFile.Text = "Display New Xml Lines";
            this.btnSaveFile.UseVisualStyleBackColor = true;
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cbLookupType);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1078, 33);
            this.panel2.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Lookup Type";
            // 
            // cbLookupType
            // 
            this.cbLookupType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLookupType.FormattingEnabled = true;
            this.cbLookupType.Items.AddRange(new object[] {
            "CITY-TR.XML",
            "TOWN-TR.XML",
            "TXOF-TR.XML"});
            this.cbLookupType.Location = new System.Drawing.Point(112, 9);
            this.cbLookupType.Name = "cbLookupType";
            this.cbLookupType.Size = new System.Drawing.Size(247, 21);
            this.cbLookupType.TabIndex = 0;
            this.cbLookupType.SelectedIndexChanged += new System.EventHandler(this.cbLookupType_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gcData);
            this.panel3.Controls.Add(this.txtResultFile);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 33);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1078, 382);
            this.panel3.TabIndex = 9;
            // 
            // gcData
            // 
            this.gcData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcData.Location = new System.Drawing.Point(0, 0);
            this.gcData.MainView = this.gvData;
            this.gcData.Name = "gcData";
            this.gcData.Size = new System.Drawing.Size(823, 382);
            this.gcData.TabIndex = 0;
            this.gcData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvData});
            // 
            // gvData
            // 
            this.gvData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colOldRecord,
            this.colNewRecord,
            this.colHasDifference,
            this.gridColumn1});
            this.gvData.GridControl = this.gcData;
            this.gvData.Name = "gvData";
            this.gvData.OptionsBehavior.Editable = false;
            this.gvData.OptionsView.ShowFooter = true;
            // 
            // colOldRecord
            // 
            this.colOldRecord.Caption = "OldRecord";
            this.colOldRecord.FieldName = "SourceLineData";
            this.colOldRecord.Name = "colOldRecord";
            this.colOldRecord.Visible = true;
            this.colOldRecord.VisibleIndex = 0;
            // 
            // colNewRecord
            // 
            this.colNewRecord.Caption = "NewRecord";
            this.colNewRecord.FieldName = "TargetLineData";
            this.colNewRecord.Name = "colNewRecord";
            this.colNewRecord.Visible = true;
            this.colNewRecord.VisibleIndex = 1;
            // 
            // colHasDifference
            // 
            this.colHasDifference.Caption = "Has Difference";
            this.colHasDifference.FieldName = "HasDifference";
            this.colHasDifference.Name = "colHasDifference";
            this.colHasDifference.Visible = true;
            this.colHasDifference.VisibleIndex = 2;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Is New Record";
            this.gridColumn1.FieldName = "IsNewRecord";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "IsNewRecord", "{0}")});
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 3;
            // 
            // txtResultFile
            // 
            this.txtResultFile.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtResultFile.Location = new System.Drawing.Point(823, 0);
            this.txtResultFile.Name = "txtResultFile";
            this.txtResultFile.Size = new System.Drawing.Size(255, 382);
            this.txtResultFile.TabIndex = 1;
            this.txtResultFile.Text = "";
            // 
            // FrmLookupData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 450);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmLookupData";
            this.Text = "Tiger Lookup Data Checker";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbLookupType;
        private DevExpress.XtraGrid.GridControl gcData;
        private DevExpress.XtraGrid.Views.Grid.GridView gvData;
        private DevExpress.XtraGrid.Columns.GridColumn colOldRecord;
        private DevExpress.XtraGrid.Columns.GridColumn colNewRecord;
        private DevExpress.XtraGrid.Columns.GridColumn colHasDifference;
        private System.Windows.Forms.RichTextBox txtResultFile;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}

