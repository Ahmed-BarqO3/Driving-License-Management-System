using DVLD_Buisness;
using Re_Project.License.International_Licenses;
using Re_Project.People;
using System;
using System.Data;
using System.Windows.Forms;

namespace Re_Project.Applications.International_License
{
    public partial class frmListInternationalLicesnseApplications : Form
    {

            DataTable _dtInternationalLicenseApplications;
        public frmListInternationalLicesnseApplications()
        {
            InitializeComponent();
        }

       
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListInternationalLicesnseApplications_Load(object sender, EventArgs e)
        {
            _dtInternationalLicenseApplications = clsInternationalLicense.GetAllInternationalLicenses();
           
            dgvInternationalLicenses.DataSource = _dtInternationalLicenseApplications;
          
            if (dgvInternationalLicenses.RowCount > 1)
            {
                dgvInternationalLicenses.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicenses.Columns[0].Width = 160;

                dgvInternationalLicenses.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicenses.Columns[1].Width = 150;

                dgvInternationalLicenses.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicenses.Columns[2].Width = 130;

                dgvInternationalLicenses.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLicenses.Columns[3].Width = 130;

                dgvInternationalLicenses.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns[4].Width = 180;

                dgvInternationalLicenses.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns[5].Width = 180;

                dgvInternationalLicenses.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns[6].Width = 120;

            }
        }

        private void btnNewApplication_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = dgvInternationalLicenses.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;


            int ID = (int)dataRow[2];
            int PersonID = clsDriver.FindByDriverID(ID).PersonID;

            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DataRowView selectedItem = dgvInternationalLicenses.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;


            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo((int)dataRow[0]);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = dgvInternationalLicenses.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;


            int ID = (int)dataRow[2];
            int PersonID = clsDriver.FindByDriverID(ID).PersonID;

            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }
    }
}
