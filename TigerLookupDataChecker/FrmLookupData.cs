using NAF.Common.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TigerLookupDataChecker.Helpers;
using TigerLookupDataChecker.Models;

namespace TigerLookupDataChecker
{
    public partial class FrmLookupData : Form
    {
        private string SelectedFile;
        private List<CompareFileLine> Records = null;

        public FrmLookupData()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            cbLookupType.SelectedIndex = 0;
            PrepareParametersWithSelection();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                gcData.DataSource = null;
                if (SelectedFile.Assigned())
                {
                    Records = await LookupFetchHelper.FetchAndGetComparisonData(SelectedFile);
                    gcData.DataSource = Records;
                    if (Records.Any())
                    {
                        txtResultFile.Text += "Fetch Results!!";
                        txtResultFile.Text += "\n" + LookupFetchHelper.GetSourceAndTargetLabels(SelectedFile) + "\n";
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("There is an error!" + ex.Message + " ErrorDetail" + (ex.InnerException.Assigned() ? ex.InnerException.Message : string.Empty));
            }
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedFile.Assigned())
                {
                    if (MessageBox.Show(
                        string.Format("NewRecords will be saved {0} at outputs folder. Continue?", SelectedFile),
                        "Warning!",
                        buttons: MessageBoxButtons.OKCancel)
                            == DialogResult.OK)
                    {
                        LookupFetchHelper.SaveNewDataToOutputFile(SelectedFile, Records, true);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("There is an error!" + ex.Message + " ErrorDetail" + (ex.InnerException.Assigned() ? ex.InnerException.Message : string.Empty));
            }
        }

        private void cbLookupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrepareParametersWithSelection();
        }

        private void PrepareParametersWithSelection()
        {
            SelectedFile = cbLookupType.Text;
            gcData.DataSource = null;
        }

        private void btnCheckDb_Click(object sender, EventArgs e)
        {

        }
    }
}
