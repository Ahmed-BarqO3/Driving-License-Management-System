using DVLD_Buisness;
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

namespace Re_Project.Drivers
{
    public partial class frmListDrivers : Form
    {

        int _DriverID;
        DataTable _dtDrivers;
        public frmListDrivers()
        {
            InitializeComponent();
        }

        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            _dtDrivers = clsDriver.GetAllDrivers();

            dgvDrivers.DataSource = _dtDrivers;

            if (_dtDrivers.Rows.Count > 0)
            {
                dgvDrivers.Columns[0].HeaderText = "Driver ID";
                dgvDrivers.Columns[0].Width = 100;

                dgvDrivers.Columns[1].HeaderText = "Person ID";
                dgvDrivers.Columns[1].Width = 100;

                dgvDrivers.Columns[2].HeaderText = "National NO";
                dgvDrivers.Columns[2].Width = 120;

                dgvDrivers.Columns[3].HeaderText = "Full Name";
                dgvDrivers.Columns[3].Width = 400;

                dgvDrivers.Columns[4].HeaderText = "Create Date";
                dgvDrivers.Columns[4].Width = 200;

                dgvDrivers.Columns[5].HeaderText = "Activate Licenses";
                dgvDrivers.Columns[5].Width = 250;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DataRowView selectedItem = dgvDrivers.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;

            //mention the first column mapping name to get first column value in Selected row 
            int ID = (int)dataRow["PersonID"];

            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ID);
            frm.ShowDialog();
        }
    }
}
