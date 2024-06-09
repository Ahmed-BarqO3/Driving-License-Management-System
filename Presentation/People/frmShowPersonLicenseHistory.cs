using System;
using System.Windows.Forms;

namespace Re_Project.People
{
    public partial class frmShowPersonLicenseHistory : Form
    {

        int _PersonID = -1;
        public frmShowPersonLicenseHistory()
        {
            InitializeComponent();
        }

        public frmShowPersonLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {
            if(_PersonID != -1)
            {
                ctlrPersonCardInfoWithFilter1.LoadPersonInfo(_PersonID);
                ctlrPersonCardInfoWithFilter1.FilterEnabled = false;

                ctrlDriverLicense1.LoadInfoByPersonID(_PersonID);
            }
            else
            {
                ctlrPersonCardInfoWithFilter1.FilterEnabled = true;
                ctlrPersonCardInfoWithFilter1.FilterFocus();
            }
        }
    }
}
