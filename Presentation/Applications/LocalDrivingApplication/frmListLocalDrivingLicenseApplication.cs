using DVLD_Buisness;
using Re_Project.License.Local_Licenses;
using Re_Project.People;
using Re_Project.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Re_Project.Applications.LocalDrivingApplication
{
    public partial class frmListLocalDrivingLicenseApplication : Form
    {

        DataTable _dtAllLocalDrivingLicenseApplications;
        public frmListLocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
           

            _dtAllLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvLocalDrivingLicenseApplications.DataSource = _dtAllLocalDrivingLicenseApplications;

         
            if (dgvLocalDrivingLicenseApplications.RowCount > 0)
            {
                dgvLocalDrivingLicenseApplications.SelectedIndex = 0;

                dgvLocalDrivingLicenseApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenseApplications.Columns[0].Width = 120;

                dgvLocalDrivingLicenseApplications.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplications.Columns[1].Width = 300;

                dgvLocalDrivingLicenseApplications.Columns[2].HeaderText = "National No.";
                dgvLocalDrivingLicenseApplications.Columns[2].Width = 150;

                dgvLocalDrivingLicenseApplications.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplications.Columns[3].Width = 350;

                dgvLocalDrivingLicenseApplications.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplications.Columns[4].Width = 170;

                dgvLocalDrivingLicenseApplications.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplications.Columns[5].Width = 150;
            }
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingApplcation frm = new frmAddUpdateLocalDrivingApplcation();
            frm.ShowDialog();

            frmListLocalDrivingLicenseApplication_Load(null,null);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = dgvLocalDrivingLicenseApplications.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;


            int ID = (int)dataRow["LocalDrivingLicenseApplicationID"];

            frmLocalDrivingLicenseApplicationInfo frm = new frmLocalDrivingLicenseApplicationInfo(ID);
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = dgvLocalDrivingLicenseApplications.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;


            int ID = (int)dataRow["LocalDrivingLicenseApplicationID"];

            frmAddUpdateLocalDrivingApplcation frm = new frmAddUpdateLocalDrivingApplcation(ID);
            frm.ShowDialog();

            frmListLocalDrivingLicenseApplication_Load(null,null);

        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            DataRowView selectedItem = dgvLocalDrivingLicenseApplications.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;


            int LocalDrivingLicenseApplicationID = (int)dataRow["LocalDrivingLicenseApplicationID"];

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);


             bool LicenseExists = LocalDrivingLicenseApplication.IsLicenseIssued();

            int TotalPassedTests = (int)dataRow[5];

            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (TotalPassedTests == 3) && !LicenseExists;

            showLicenseToolStripMenuItem.Enabled = LicenseExists;
            editToolStripMenuItem.Enabled = !LicenseExists && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);
            ScheduleTestsMenue.Enabled = !LicenseExists;

            CancelApplicaitonToolStripMenuItem.Enabled = (LocalDrivingLicenseApplication.ApplicationStatus == clsLocalDrivingLicenseApplication.enApplicationStatus.New);

            DeleteApplicationToolStripMenuItem.Enabled = (LocalDrivingLicenseApplication.ApplicationStatus == clsLocalDrivingLicenseApplication.enApplicationStatus.New);

            

            bool PassedVisionTest =  LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest);
            bool PassedWrittenTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.WrittenTest);
            bool PassedStreetTest  = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.StreetTest);


            ScheduleTestsMenue.Enabled = (!PassedVisionTest || !PassedWrittenTest || !PassedStreetTest) && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);


            if(ScheduleTestsMenue.Enabled)
            {
                scheduleVisionTestToolStripMenuItem.Enabled = !PassedVisionTest;

                scheduleWrittenTestToolStripMenuItem.Enabled = PassedVisionTest && !PassedWrittenTest;

                scheduleStreetTestToolStripMenuItem.Enabled = (PassedVisionTest && PassedWrittenTest && !PassedStreetTest);
            }
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = dgvLocalDrivingLicenseApplications.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;


            int ID = (int)dataRow["LocalDrivingLicenseApplicationID"];

            frmListTestAppoitment frm = new frmListTestAppoitment(ID, clsTestType.enTestType.VisionTest);
            frm.ShowDialog();

            frmListLocalDrivingLicenseApplication_Load(null,null);
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = dgvLocalDrivingLicenseApplications.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;


            int ID = (int)dataRow["LocalDrivingLicenseApplicationID"];

            frmListTestAppoitment frm = new frmListTestAppoitment(ID, clsTestType.enTestType.WrittenTest);
            frm.ShowDialog();

            frmListLocalDrivingLicenseApplication_Load(null, null);
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = dgvLocalDrivingLicenseApplications.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;


            int ID = (int)dataRow["LocalDrivingLicenseApplicationID"];

            frmListTestAppoitment frm = new frmListTestAppoitment(ID, clsTestType.enTestType.StreetTest);
            frm.ShowDialog();

            frmListLocalDrivingLicenseApplication_Load(null, null);
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = dgvLocalDrivingLicenseApplications.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;


            int ID = (int)dataRow["LocalDrivingLicenseApplicationID"];

            frmIssueDriverLicenseFirstTime frm = new frmIssueDriverLicenseFirstTime(ID);
                frm.ShowDialog();

            frmListLocalDrivingLicenseApplication_Load(null, null);
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = dgvLocalDrivingLicenseApplications.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;
            int ID = (int)dataRow["LocalDrivingLicenseApplicationID"];

            int LicenseID = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(
              ID).GetActiveLicenseID();

            if (LicenseID != -1)
            {

                frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
                frm.ShowDialog();

            }
            else
            {
                MessageBox.Show("No License Found!", "No License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void DeleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            DataRowView selectedItem = dgvLocalDrivingLicenseApplications.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;

            int LocalDrivingLicenseApplicationID = (int)dataRow["LocalDrivingLicenseApplicationID"];

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    frmListLocalDrivingLicenseApplication_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not delete applicatoin, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CancelApplicaitonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to cancel this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;


            DataRowView selectedItem = dgvLocalDrivingLicenseApplications.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;

            int LocalDrivingLicenseApplicationID = (int)dataRow["LocalDrivingLicenseApplicationID"];

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Cancel())
                {
                    MessageBox.Show("Application Cancelled Successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    frmListLocalDrivingLicenseApplication_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not cancel applicatoin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = dgvLocalDrivingLicenseApplications.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;

            int ID = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID((int)dataRow[0]).ApplicantPersonID;

            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ID);
            frm.ShowDialog();
        }
    }
}
