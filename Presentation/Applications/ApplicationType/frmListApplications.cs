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

namespace Re_Project.Applications.ApplicationType
{
    public partial class frmListApplications : Form
    {

        DataTable _dtAllApplicationTypes;
        public frmListApplications()
        {
            InitializeComponent();
        }

        private void frmListApplications_Load(object sender, EventArgs e)
        {
            _dtAllApplicationTypes = clsApplicationType.GetAllApplicationTypes();

            dgvApplicationType.DataSource = _dtAllApplicationTypes;

            dgvApplicationType.Columns[0].HeaderText = "ID";
            dgvApplicationType.Columns[0].Width = 80;

            dgvApplicationType.Columns[1].HeaderText = "Title";
            dgvApplicationType.Columns[1].Width = 400;

            dgvApplicationType.Columns[2].HeaderText = "Fees";
            dgvApplicationType.Columns[2].Width = 200;


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = dgvApplicationType.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;


            int ID = (int)dataRow["ApplicationTypeID"];

            frmUpdateApplicationType frm = new frmUpdateApplicationType(ID);
            frm.ShowDialog();
            frmListApplications_Load(null,null);
        }
    }
}
