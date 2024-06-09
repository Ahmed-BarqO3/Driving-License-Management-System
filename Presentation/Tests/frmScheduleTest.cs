using DVLD_Buisness;
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
    public partial class frmScheduleTest : Form
    {

        int _TestAppoitmentID = -1;
        int _LocalDrivingLicenseID = -1;
        clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;

        public frmScheduleTest(int LocalDrivingLicenseID,clsTestType.enTestType TestTypeID,int AppoitmentID = -1)
        {
            InitializeComponent();

           _LocalDrivingLicenseID= LocalDrivingLicenseID;
            _TestTypeID= TestTypeID;    
            _TestAppoitmentID= AppoitmentID;
        }

        void frmScheduleTest_Load(object sender, EventArgs e)
        {
            ctrlScheduleTest1.TestTypeID = _TestTypeID;
            ctrlScheduleTest1.LoadInfo(_LocalDrivingLicenseID, _TestAppoitmentID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    
}
