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
    public partial class frmLocalDrivingLicenseApplicationInfo : Form
    {
        int _LocalDrivingLicenseID = -1;
        public frmLocalDrivingLicenseApplicationInfo(int LocalDrivingLicenseID)
        {
            InitializeComponent();
            _LocalDrivingLicenseID = LocalDrivingLicenseID;
        }

        private void frmLocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
