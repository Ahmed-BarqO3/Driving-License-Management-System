using DVLD.Applications.Detain_License;
using DVLD.Applications.Rlease_Detained_License;
using DVLD_Buisness;
using Re_Project.License.Local_Licenses;
using Re_Project.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Re_Project.License.Detaine_Licenses
{
    public partial class frmListDetainedLicenses : Form
    {

        DataTable _dtDetainedLicenses;
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {

            _dtDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();

            dgvDetainedLicenses.DataSource = _dtDetainedLicenses;
          

            if (dgvDetainedLicenses.RowCount > 1)
            {
                dgvDetainedLicenses.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicenses.Columns[0].Width = 90;

                dgvDetainedLicenses.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicenses.Columns[1].Width = 90;

                dgvDetainedLicenses.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicenses.Columns[2].Width = 160;

                dgvDetainedLicenses.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicenses.Columns[3].Width = 110;

                dgvDetainedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicenses.Columns[4].Width = 110;

                dgvDetainedLicenses.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicenses.Columns[5].Width = 160;

                dgvDetainedLicenses.Columns[6].HeaderText = "N.No.";
                dgvDetainedLicenses.Columns[6].Width = 90;

                dgvDetainedLicenses.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicenses.Columns[7].Width = 330;

                dgvDetainedLicenses.Columns[8].HeaderText = "Rlease App.ID";
                dgvDetainedLicenses.Columns[8].Width = 150;

            }

        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = dgvDetainedLicenses.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;


            int ID = (int)dataRow[1];

            frmShowPersonInfo frm = new frmShowPersonInfo(ID);
            frm.ShowDialog();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = dgvDetainedLicenses.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;


            int ID = (int)dataRow[1];

            frmShowLicenseInfo frm = new frmShowLicenseInfo(ID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = dgvDetainedLicenses.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;


            int ID = (int)dataRow[1];

            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ID);
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = dgvDetainedLicenses.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;


            int ID = (int)dataRow[1];

            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication(ID);
            frm.ShowDialog();

            frmListDetainedLicenses_Load(null, null);
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();

            frmListDetainedLicenses_Load(null, null);
        }

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();

            frmListDetainedLicenses_Load(null, null);
        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            DataRowView selectedItem = dgvDetainedLicenses.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;

            releaseDetainedLicenseToolStripMenuItem.Enabled = !(bool)dataRow[3];
        }
    }
}
