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

namespace TigerLookupDataChecker
{
    public partial class FrmGeneratedSql : Form
    {
        public string generatedScript;
        public string connString;

        public FrmGeneratedSql()
        {
            InitializeComponent();
        }

        public FrmGeneratedSql(string paramSql)
        {
            InitializeComponent();
            generatedScript = paramSql;
           
        }

        private void FrmGeneratedSql_Load(object sender, EventArgs e)
        {
            txtGeneratedSql.Text = generatedScript;
            lblTargetDatabse.Text = "Target Connection String! Check!\n" + connString;
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtGeneratedSql.Text) && MessageBox.Show(lblTargetDatabse.Text,"", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    txtExecutionLog.Text = SqlHelper.ExecuteSqls(connString,txtGeneratedSql.Text);
                    MessageBox.Show("Scripts executed!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Scripts has error please check with sql server manmager transaction rollbacked!");
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtGeneratedSql.Text);
        }
    }
}
