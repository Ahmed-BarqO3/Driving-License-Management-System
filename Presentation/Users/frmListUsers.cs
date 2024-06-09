using DVLD.User;
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

namespace Re_Project.Users
{
    public partial class frmListUsers : Form
    {
        static DataTable _dtAllUsers;
        public frmListUsers()
        {
            InitializeComponent();
        }

      

        private void frmListUsers_Load(object sender, EventArgs e)
        {
            _dtAllUsers = clsUser.GetAllUsers();
            dgvUsers.DataSource = _dtAllUsers;

            dgvUsers.Columns[0].HeaderText = "User ID";
            dgvUsers.Columns[0].Width = 110;

            dgvUsers.Columns[1].HeaderText = "Person ID";
            dgvUsers.Columns[1].Width = 120;

            dgvUsers.Columns[2].HeaderText = "Full Name";
            dgvUsers.Columns[2].Width = 350;

            dgvUsers.Columns[3].HeaderText = "UserName";
            dgvUsers.Columns[3].Width = 120;

            dgvUsers.Columns[4].HeaderText = "Is Active";
            dgvUsers.Columns[4].Width = 120;

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = dgvUsers.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;


            int UserID = (int)dataRow["UserID"];

            frmUserInfo frm = new frmUserInfo(UserID);
            frm.ShowDialog();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.ShowDialog();

            frmListUsers_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = dgvUsers.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;


            int UserID = (int)dataRow["UserID"];

            frmAddUpdateUser frm = new frmAddUpdateUser(UserID);
            frm.ShowDialog();

            frmListUsers_Load(null, null);

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = dgvUsers.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;


            int UserID = (int)dataRow["UserID"];



            if (clsUser.DeleteUser(UserID))
            {
                MessageBox.Show("User has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmListUsers_Load(null, null);
            }

            else
                MessageBox.Show("User is not delted due to data connected to it.", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView selectedItem = dgvUsers.CurrentItem as DataRowView;
            var dataRow = (selectedItem as DataRowView).Row;


            int UserID = (int)dataRow["UserID"];

            frmChangePassword frm = new frmChangePassword(UserID);
            frm.ShowDialog();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void callPhoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.ShowDialog();

            frmListUsers_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
