using DVLD_Buisness;
using Syncfusion.WinForms.DataGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Re_Project.People
{
    public partial class frmListPeople : Form
    {
        public frmListPeople()
        {
            InitializeComponent();
        }

        private static DataTable _dtAllPeople = clsPerson.GetAllPeople();

        //only select the columns that you want to show in the grid
        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                         "FirstName", "SecondName", "ThirdName", "LastName",
                                                         "GendorCaption", "DateOfBirth", "CountryName",
                                                         "Phone", "Email");

        private void _RefreshPeoplList()
        {
            _dtAllPeople = clsPerson.GetAllPeople();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "GendorCaption", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");

            dgvPeople.DataSource = _dtPeople;
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = dgvPeople.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;

            //mention the first column mapping name to get first column value in Selected row 
            int PeronID = (int)dataRow["PersonID"];

            Form frm = new frmShowPersonInfo(PeronID);
            frm.ShowDialog();

        }

        private void frmListPeople_Load(object sender, EventArgs e)
        {
            dgvPeople.DataSource = _dtPeople;


            if (dgvPeople.RowCount > 0)
            {

                dgvPeople.Columns[0].HeaderText = "Person ID";
                dgvPeople.Columns[0].Width = 110;

                dgvPeople.Columns[1].HeaderText = "National No.";
                dgvPeople.Columns[1].Width = 120;


                dgvPeople.Columns[2].HeaderText = "First Name";
                dgvPeople.Columns[2].Width = 120;

                dgvPeople.Columns[3].HeaderText = "Second Name";
                dgvPeople.Columns[3].Width = 140;


                dgvPeople.Columns[4].HeaderText = "Third Name";
                dgvPeople.Columns[4].Width = 120;

                dgvPeople.Columns[5].HeaderText = "Last Name";
                dgvPeople.Columns[5].Width = 120;

                dgvPeople.Columns[6].HeaderText = "Gendor";
                dgvPeople.Columns[6].Width = 90;

                dgvPeople.Columns[7].HeaderText = "Date Of Birth";
                dgvPeople.Columns[7].Width = 140;

                dgvPeople.Columns[8].HeaderText = "Nationality";
                dgvPeople.Columns[8].Width = 120;


                dgvPeople.Columns[9].HeaderText = "Phone";
                dgvPeople.Columns[9].Width = 120;


                dgvPeople.Columns[10].HeaderText = "Email";
                dgvPeople.Columns[10].Width = 180;
            }
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditPerson();

            frm.ShowDialog();
            _RefreshPeoplList();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditPerson();

            frm.ShowDialog();
            _RefreshPeoplList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = dgvPeople.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;


            int PeronID = (int)dataRow["PersonID"];

            Form frm = new frmAddEditPerson(PeronID);

            frm.ShowDialog();
            _RefreshPeoplList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = dgvPeople.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;


            int PeronID = (int)dataRow["PersonID"];


            if (MessageBox.Show("Are you sure you want to delete Person [" + PeronID + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsPerson.DeletePerson(PeronID))
                {
                    MessageBox.Show("Person Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshPeoplList();
                }

                else
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void emailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void phoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
