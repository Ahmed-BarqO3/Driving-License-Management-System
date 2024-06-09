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

namespace Re_Project.People
{
    public partial class ctlrPersonCardInfoWithFilter : UserControl
    {
        public ctlrPersonCardInfoWithFilter()
        {
            InitializeComponent();
        }

        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }
            set
            {
                _ShowAddPerson = value;
                btnAddNewPerson.Visible = _ShowAddPerson;
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                groupBox1.Enabled = _FilterEnabled;
            }
        }



        public int PersonID
        {
            get { return ctrlPersonCard1.PersonID; }
        }

        public clsPerson PersonSelectInfo
        {
            get { return ctrlPersonCard1.SelectedPersonInfo; }
        }


        public void LoadPersonInfo(int PersonID)
        {

            cbFilter.SelectedIndex = 1;
            txtFilterValue.Text = PersonID.ToString();
            FindPerson();

        }

        void FindPerson()
        {
            switch (cbFilter.Text)
            {
                case "Person ID":
                    ctrlPersonCard1.LoadPersonInfo(int.Parse(txtFilterValue.Text));
                    break;
                case "National No":
                    ctrlPersonCard1.LoadPersonInfo(txtFilterValue.Text);
                    break;
            }
        }

        private void ctlrPersonCardInfoWithFilter_Load(object sender, EventArgs e)
        {
           
            txtFilterValue.Focus();
            
            List<string> list = new List<string>();

            list.Add("National No");
            list.Add("Person ID");

            cbFilter.DataSource = list;

            cbFilter.SelectedIndex = 0;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                    MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            FindPerson();
        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilterValue.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtFilterValue, null);
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                btnFind.PerformClick();
            }

            if(cbFilter.SelectedIndex == 1)
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        

        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

         void DataBackEvent(object sender, int PersonID)
        {
         

            cbFilter.SelectedIndex = 1;
            txtFilterValue.Text = PersonID.ToString();
            ctrlPersonCard1.LoadPersonInfo(PersonID);
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm1 = new frmAddEditPerson();
            frm1.DataBack += DataBackEvent; 
            frm1.ShowDialog();
        }
    }
}
