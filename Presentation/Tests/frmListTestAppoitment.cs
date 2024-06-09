using DVLD.Tests;
using DVLD_Buisness;
using Re_Project.Applications.LocalDrivingApplication;
using Re_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Re_Project.Tests
{
    public partial class frmListTestAppoitment : Form
    {

        int _LocalDrivingLicenseApplicationID;
        clsTestType.enTestType _TestType;
        public frmListTestAppoitment(int LocalDrivingLicensID,clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicensID;
            _TestType = TestTypeID;
        }


        

        void _LoadImageAndTitle()
        {
            switch ( _TestType )
            {
                case clsTestType.enTestType.VisionTest:
                    
                        pbTestTypeImage.Image = Resources.Vision_512;
                        lblTitle.Text = "Vision Test";
                    break;

                case clsTestType.enTestType.WrittenTest:

                    pbTestTypeImage.Image = Resources.Written_Test_512;
                    lblTitle.Text = "Written Test";
                    break;

                case clsTestType.enTestType.StreetTest:

                    pbTestTypeImage.Image = Resources.driving_test_512;
                    lblTitle.Text = "Street Test";
                    break;

            }
        }

        private void frmListTestAppoitment_Load(object sender, EventArgs e)
        {
            _LoadImageAndTitle();

            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID( _LocalDrivingLicenseApplicationID );

            DataTable _dtTestAppointment = clsTestAppointment.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID,_TestType);

            dgvAppoitments.DataSource = _dtTestAppointment;

            if (dgvAppoitments.RowCount > 1)
            {
                dgvAppoitments.SelectedIndex = 0;

                dgvAppoitments.Columns[0].HeaderText = "Appointment ID";
                dgvAppoitments.Columns[0].Width = 150;

                dgvAppoitments.Columns[1].HeaderText = "Appointment Date";
                dgvAppoitments.Columns[1].Width = 250;

                dgvAppoitments.Columns[2].HeaderText = "Paid Fees";
                dgvAppoitments.Columns[2].Width = 150;

                dgvAppoitments.Columns[3].HeaderText = "Is Looked";
                dgvAppoitments.Columns[3].Width = 300;

            }
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);


            if (localDrivingLicenseApplication.IsThereAnActiveScheduledTest(_TestType))
            {
                MessageBox.Show("Person Already have an active appointment for this test, You cannot add new appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsTest LastTest = localDrivingLicenseApplication.GetLastTestPerTestType(_TestType);

            if(LastTest == null)
            {
                frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestType);
                frm.ShowDialog();

                frmListTestAppoitment_Load(null,null);
            }

            if(LastTest.TestResult)
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmScheduleTest frm2 = new frmScheduleTest(LastTest.TestAppointmentInfo.LocalDrivingLicenseApplicationID, _TestType);
                
            frm2.ShowDialog();
            frmListTestAppoitment_Load(null,null); 

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = dgvAppoitments.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;


            int ID = (int)dataRow["TestAppointmentID"];

            frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID,_TestType,ID);
            frm.ShowDialog();

            frmListTestAppoitment_Load(null,null);
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = dgvAppoitments.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;


            int ID = (int)dataRow["TestAppointmentID"];


            frmTakeTest frm = new frmTakeTest(ID, _TestType);
            frm.ShowDialog();   

            frmListTestAppoitment_Load(null ,null);
        }
    }
}
