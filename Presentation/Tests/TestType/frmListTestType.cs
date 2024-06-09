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

namespace Re_Project.Tests.TestType
{
    public partial class frmListTestType : Form
    {
        DataTable _dtAllTestTypes;
        public frmListTestType()
        {
            InitializeComponent();
        }

        private void frmListTestType_Load(object sender, EventArgs e)
        {
            _dtAllTestTypes = clsTestType.GetAllTestTypes();

            dgvTestTypes.DataSource = _dtAllTestTypes;

            dgvTestTypes.Columns[0].HeaderText = "ID";
            dgvTestTypes.Columns[0].Width = 50;

            dgvTestTypes.Columns[1].HeaderText = "Title";
            dgvTestTypes.Columns[1].Width = 180;

            dgvTestTypes.Columns[2].HeaderText = "description";
            dgvTestTypes.Columns[2].Width = 630;

            dgvTestTypes.Columns[3].HeaderText = "Fees";
            dgvTestTypes.Columns[3].Width = 68;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = dgvTestTypes.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;


            int ID = (int)dataRow["TestTypeID"];

            frmEditTestType frm = new frmEditTestType((clsTestType.enTestType)ID);
            frm.ShowDialog();

            frmListTestType_Load(null, null);

        }
    }
}
