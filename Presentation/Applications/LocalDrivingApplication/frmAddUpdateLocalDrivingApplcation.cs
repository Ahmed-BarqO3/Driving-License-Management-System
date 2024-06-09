using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Buisness;
using Re_Project.Global_Classes;

namespace Re_Project.Applications.LocalDrivingApplication
{
    public partial class frmAddUpdateLocalDrivingApplcation : Form
    {

        enum enMode { AddNew,Update};
        enMode _Mode;

        int _LocalDrivingLicenseApplicationID;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;


        public frmAddUpdateLocalDrivingApplcation()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
            
        }
        public frmAddUpdateLocalDrivingApplcation(int localDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
        }

        void _FillLicenseClassesInComoboBox()
        {
            DataTable dtLicenseClasses = clsLicenseClass.GetAllLicenseClasses();

            List<string> licenseClasses = new List<string>();

            foreach (DataRow row in dtLicenseClasses.Rows) 
                licenseClasses.Add(row["ClassName"].ToString());
            
            cbLicenseClass.DataSource = licenseClasses;
            cbLicenseClass.SelectedIndex = 2;
        }
        void _RestDefultValues()
        {
            _FillLicenseClassesInComoboBox();
            if(_Mode == enMode.AddNew)
            {
                lblTitle.Text = "New Local Driving License Application";
                this.Text = lblTitle.Text;
                _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
                ctlrPersonCardInfoWithFilter1.FilterFocus();
                tpApplicationInfo.Enabled = false;

                lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense).Fees.ToString();
                lblApplicationDate.Text = DateTime.Now.ToShortDateString();
                //lblCreatedByUser.Text = clsGlobal.CurrenUser.UserName;  
            }
            else
            {
                lblTitle.Text = "Update Local Driving License Application";
                this.Text = lblTitle.Text;

                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
            }
        }

        void _LoadData()
        {
            ctlrPersonCardInfoWithFilter1.FilterEnabled = false;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);

            if(_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Application with ID = " + _LocalDrivingLicenseApplicationID);
                this.Close();

                return;
            }

            ctlrPersonCardInfoWithFilter1.LoadPersonInfo(_LocalDrivingLicenseApplication.ApplicantPersonID);
            lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicenseApplicationID.ToString();
            lblFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString();
            lblApplicationDate.Text = _LocalDrivingLicenseApplication.ApplicationDate.ToString();
            lblCreatedByUser.Text = clsUser.FindByUserID(_LocalDrivingLicenseApplication.CreatedByUserID).UserName;
            cbLicenseClass.SelectedIndex = _LocalDrivingLicenseApplication.LicenseClassID - 1;
        }

        private void frmAddUpdateLocalDrivingApplcation_Load(object sender, EventArgs e)
        {
            _RestDefultValues();
            if(_Mode == enMode.Update)
                _LoadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];
                return;
            }


            //incase of add new mode.
            if (ctlrPersonCardInfoWithFilter1.PersonID != -1)
            {

                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];

            }

            else

            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctlrPersonCardInfoWithFilter1.FilterFocus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int LicenseClassID = clsLicenseClass.Find(cbLicenseClass.Text).LicenseClassID;


            int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(ctlrPersonCardInfoWithFilter1.PersonID, clsApplication.enApplicationType.NewDrivingLicense, LicenseClassID);

            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" + ActiveApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseClass.Focus();
                return;
            }


            //check if user already have issued license of the same driving  class.
            //if (clsLicense.IsLicenseExistByPersonID(ctlrPersonCardInfoWithFilter1.PersonID, LicenseClassID))
            //{

            //    MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            _LocalDrivingLicenseApplication.ApplicantPersonID = ctlrPersonCardInfoWithFilter1.PersonID; ;
            _LocalDrivingLicenseApplication.ApplicationDate = DateTime.Now;
            _LocalDrivingLicenseApplication.ApplicationTypeID = 1;
            _LocalDrivingLicenseApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;
            _LocalDrivingLicenseApplication.PaidFees = Convert.ToSingle(lblFees.Text);
            _LocalDrivingLicenseApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _LocalDrivingLicenseApplication.LicenseClassID = LicenseClassID;


            if (_LocalDrivingLicenseApplication.Save())
            {
                lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                //change form mode to update.
                _Mode = enMode.Update;
                lblTitle.Text = "Update Local Driving License Application";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
