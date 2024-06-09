using DVLD_Buisness;
using Re_Project.License.International_Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Re_Project.License.Local_Licenses.Controls
{
    public partial class ctrlDriverLicense : UserControl
    {

        private int _DriverID;
        private clsDriver _Driver;
        private DataTable _dtDriverLocalLicensesHistory;
        private DataTable _dtDriverInternationalLicensesHistory;

        public ctrlDriverLicense()
        {
            InitializeComponent();
        }



        private void _LoadLocalLicenseInfo()
        {

            _dtDriverLocalLicensesHistory = clsDriver.GetLicenses(_DriverID);


            dgvLocalLicensesHistory.DataSource = _dtDriverLocalLicensesHistory;


            if (dgvLocalLicensesHistory.RowCount > 1)
            {
                dgvLocalLicensesHistory.Columns[0].HeaderText = "Lic.ID";
                dgvLocalLicensesHistory.Columns[0].Width = 110;

                dgvLocalLicensesHistory.Columns[1].HeaderText = "App.ID";
                dgvLocalLicensesHistory.Columns[1].Width = 110;

                dgvLocalLicensesHistory.Columns[2].HeaderText = "Class Name";
                dgvLocalLicensesHistory.Columns[2].Width = 270;

                dgvLocalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicensesHistory.Columns[3].Width = 170;

                dgvLocalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicensesHistory.Columns[4].Width = 170;

                dgvLocalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvLocalLicensesHistory.Columns[5].Width = 110;

            }
        }

        private void _LoadInternationalLicenseInfo()
        {

            // _dtDriverInternationalLicensesHistory = clsDriver.GetInternationalLicenses(_DriverID);


            dgvInternationalLicensesHistory.DataSource = _dtDriverInternationalLicensesHistory;


            if (dgvInternationalLicensesHistory.RowCount > 1)
            {
                dgvInternationalLicensesHistory.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicensesHistory.Columns[0].Width = 160;

                dgvInternationalLicensesHistory.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicensesHistory.Columns[1].Width = 130;

                dgvInternationalLicensesHistory.Columns[2].HeaderText = "L.License ID";
                dgvInternationalLicensesHistory.Columns[2].Width = 130;

                dgvInternationalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvInternationalLicensesHistory.Columns[3].Width = 180;

                dgvInternationalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvInternationalLicensesHistory.Columns[4].Width = 180;

                dgvInternationalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvInternationalLicensesHistory.Columns[5].Width = 120;

            }
        }

        public void LoadInfoByPersonID(int PersonID)
        {

            _Driver = clsDriver.FindByPersonID(PersonID);


            if (_Driver is null)
            {
                MessageBox.Show("Not Found", "Driver with id = " + _Driver + " not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (_Driver != null)
            {
                _DriverID = _Driver.DriverID;
            }

            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();
        }

        public void Clear()
        {
            _dtDriverLocalLicensesHistory.Clear();

        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = dgvLocalLicensesHistory.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;

           
            int ID = (int)dataRow["LicenseID"];

            frmShowLicenseInfo frm = new frmShowLicenseInfo(ID);
            frm.ShowDialog();
        }

        private void InternationalLicenseHistorytoolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = dgvInternationalLicensesHistory.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;


            int ID = (int)dataRow["LicenseID"];
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(ID);
            frm.ShowDialog();

            
        }
    }
}

