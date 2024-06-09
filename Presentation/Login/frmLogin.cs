using DVLD.Classes;
using DVLD_Buisness;
using Re_Project.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Re_Project.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser user = clsUser.FindByUsernameAndPassword(txtUsername.Text,clsUtil.Hash(txtPassword.Text));

          

            if (user != null)
            {
                if(chkRememberMe.Checked)
                {
                    clsGlobal.RememberUsernameAndPassword(txtUsername.Text,txtPassword.Text);
                }
                else
                    clsGlobal.RememberUsernameAndPassword(null,null);

                clsGlobal.CurrentUser = user;
                this.Hide();
                frmMain frm = new frmMain(this);
                frm.ShowDialog();
               

            }
            else
            {

            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
   
            string usernam = null,password = null;

            if(clsGlobal.GetStoredCredential(ref usernam,ref password))
            {
                chkRememberMe.Checked = true;

                txtUsername.Text = usernam;
                txtPassword.Text = password;
            }
            else
                chkRememberMe.Checked = false;

            btnLogin.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == Convert.ToChar( Keys.Enter))
                btnLogin.PerformClick();
        }
    }
}
