using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TigerLookupDataChecker
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnShowLookupDataGenerator_Click(object sender, EventArgs e)
        {
            FrmLookupData frm = new FrmLookupData();
            frm.ShowDialog();
        }

        private void btnShowSqlScriptGenerator_Click(object sender, EventArgs e)
        {
            FrmSqlGenerator frm = new FrmSqlGenerator();
            frm.ShowDialog();
        }
    }
}
