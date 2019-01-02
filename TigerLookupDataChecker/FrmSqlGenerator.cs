using NAF.Common.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using TigerLookupDataChecker.Helpers;
using TigerLookupDataChecker.Models;

namespace TigerLookupDataChecker
{
    public partial class FrmSqlGenerator : Form
    {
        
        private List<Firm> firms = new List<Firm>();

        public FrmSqlGenerator()
        {
            InitializeComponent();
        }

        private string GetConnString(string dbName)
        {
            return string.Format(Constants.ConnTemplate, txtDatasource.Text, dbName, txtUsername.Text, txtPassword.Text);
        }

        private void btnCheckConnection_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(GetConnString(txtDatabase.Text)))
                {
                    SqlHelper.CheckConnection(GetConnString(txtDatabase.Text));
                    MessageBox.Show("Connected!");
                }
                else
                {
                    MessageBox.Show("Enter connection string");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! " + ex.Message);
            }
        }

        private void btnFillFirms_Click(object sender, EventArgs e)
        {
            try
            {
               
                var dataList = SqlHelper.GetGenericQueryResult(GetConnString(lbDatabases.SelectedItem.ToString()), Constants.FirmSql);

                firms.Clear();
                if (dataList != null && dataList.Any())
                {
                    foreach (var currentRecord in dataList)
                    {
                        Firm firmRecord = new Firm();
                        firmRecord.IsSelected = true;
                        firmRecord.FirmNr = currentRecord[Constants.ColumnAliasNr].ToString();
                        firmRecord.FirmName = currentRecord[Constants.ColumnAliasName].ToString();
                        firmRecord.Period = currentRecord[Constants.ColumnAliasPerNr].ToString();
                        firms.Add(firmRecord);
                    }
                }

                BindFirmData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }


        private void btnGenerateSqls_Click(object sender, EventArgs e)
        {
            if (firms != null && firms.Any())
            {
                var selectedFirms = firms.Where(f => f.IsSelected).ToList();
                if (selectedFirms != null && selectedFirms.Any())
                {
                    var generatedScript = SqlHelper.GenerateScriptFromTemplate(selectedFirms, txtTemplateSql.Text);
                    FrmGeneratedSql frm = new FrmGeneratedSql();
                    frm.connString = GetConnString(lbDatabases.SelectedItem.ToString());
                    frm.generatedScript = generatedScript;
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Check any firm to generate sql");
                }
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            if (firms != null)
            {
                foreach (var currentFirm in firms)
                {
                    currentFirm.IsSelected = true;
                }
            }
            BindFirmData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (firms != null)
            {
                foreach (var currentFirm in firms)
                {
                    currentFirm.IsSelected = false;
                }
            }
            BindFirmData();
        }

        private void BindFirmData()
        {
            gcFirms.DataSource = null;
            gcFirms.DataSource = firms;
        }

        private void RefreshConnectionString()
        {
            btnFillFirms.PerformClick();
        }

        private void btnFillDatabases_Click(object sender, EventArgs e)
        {
            FillDatabases();
        }

        private void FillDatabases()
        {
            try
            {
                lbDatabases.Items.Clear();

                string firmSql = string.Format(Constants.DatabasesSql, txtDbPattern.Text);
                var dataList = SqlHelper.GetGenericQueryResult(GetConnString(txtDatabase.Text), firmSql);

                if (dataList != null && dataList.Any())
                {
                    foreach (var currentRecord in dataList)
                    {
                        var currentDb = currentRecord[Constants.ColumnAliasDatabase].ToString();
                        lbDatabases.Items.Add(currentDb);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }

        private void lbDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnFillFirms.PerformClick();
        }

        private void btnGenerateLookupSqls_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbDatabases.SelectedItem != null && !string.IsNullOrEmpty(lbDatabases.SelectedItem.ToString()))
                {
                    if (MessageBox.Show("Xmls in the Input folder will be read and searched at the target db's L_CITY L_TAXOFFICE L_TOWN tables. Continue?","", buttons: MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        txtTemplateSql.Text = SqlHelper.CheckInputLookupDataAndGenerateMissingInsertStatements(GetConnString(lbDatabases.SelectedItem.ToString()));
                    }
                }
                else
                {
                    MessageBox.Show("Select database to check lookupdatas");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("There is an error!" + ex.Message + " ErrorDetail" + (ex.InnerException.Assigned() ? ex.InnerException.Message : string.Empty));
            }
        }

        private void btnExecuteSql_Click(object sender, EventArgs e)
        {
            if (firms != null && firms.Any())
            {
                var selectedFirms = firms.Take(1).ToList();
                if (selectedFirms != null && selectedFirms.Any())
                {
                    var generatedScript = SqlHelper.GenerateScriptFromTemplate(selectedFirms, txtTemplateSql.Text);
                    FrmGeneratedSql frm = new FrmGeneratedSql();
                    frm.connString = GetConnString(lbDatabases.SelectedItem.ToString());
                    frm.generatedScript = generatedScript;
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Check any firm to generate sql");
                }
            }
        }
    }
}
