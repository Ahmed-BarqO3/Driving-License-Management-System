using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Classes;
using DVLD_Buisness;
using Re_Project.Properties;

namespace Re_Project
{
    public partial class frmAddEditPerson : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;

        public enum enMode { AddNew = 0, Update = 1 };
        public enum enGendor { Male = 0, Female = 1 };

        private enMode _Mode;
        private int _PersonID = -1;
        clsPerson _Person;


        public frmAddEditPerson()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
            _Person = new clsPerson();
        }

        public frmAddEditPerson(int PersonID)
        {
            InitializeComponent();

            _Mode = enMode.Update;
            _PersonID = PersonID;
        }

        void _FillCountriesInComoboBox()
        {
           DataTable dt = clsCountry.GetAllCountries();

            List<string> countries = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
               countries.Add(row["CountryName"].ToString());
            }

            cmbCountry.DataSource = countries;
            cmbCountry.AutoCompleteMode = AutoCompleteMode.Suggest;
        }

        void _ResetDefualtValues()
        {
            _FillCountriesInComoboBox();

            if (_Mode == enMode.AddNew)
                lblMain.Text = "Add New Person";
            else
                lblMain.Text = "Update Person";


            if(rdMale.Checked)
                pbPersonImage.Image = Resources.Male_512;
            
            else if(rdFemale.Checked)
                pbPersonImage.Image = Resources.Female_512;
            

            BirthOfDate.MaxDateTime = DateTime.Now.AddYears(-18);
            BirthOfDate.Value = BirthOfDate.MaxDateTime;

            //should not allow adding age more than 90 years
            BirthOfDate.MinDateTime = DateTime.Now.AddYears(-90);

            //this will set default country to Libya.
            cmbCountry.SelectedIndex = clsCountry.Find("Libya").ID - 1;

            txtFName.Text = "";
            txtSName.Text = "";
            txtMidName.Text = "";
            txtLastName.Text = "";
            txtNationalNO.Text = "";
            rdMale.Checked = true;
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";

            llRemoveImage.Visible = false;
        }

        void _LoadData()
        {
            _Person = clsPerson.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("No Person with ID = " + _PersonID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

           
            lblPersonID.Text = _PersonID.ToString();
            txtFName.Text = _Person.FirstName;
            txtSName.Text = _Person.SecondName;
            txtMidName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtNationalNO.Text = _Person.NationalNo;
            BirthOfDate.Value = _Person.DateOfBirth;

            if (_Person.Gendor == 0)
                rdMale.Checked = true;
            else
                rdFemale.Checked = true;

            txtAddress.Text = _Person.Address;
            txtPhone.Text = _Person.Phone;
            txtEmail.Text = _Person.Email;

            cmbCountry.SelectedIndex = _Person.NationalityCountryID  - 1;

            if(_Person.ImagePath != "")
                pbPersonImage.ImageLocation = _Person.ImagePath;

            llRemoveImage.Visible = pbPersonImage.ImageLocation != null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (!_HandlePersonImage())
                return;

            int NationalityCountryID = cmbCountry.SelectedIndex + 1;

            _Person.FirstName = txtFName.Text.Trim();
            _Person.SecondName = txtSName.Text.Trim();
            _Person.ThirdName = txtMidName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.NationalNo = txtNationalNO.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.Address = txtAddress.Text.Trim();
            _Person.DateOfBirth = Convert.ToDateTime(BirthOfDate.Value);

            if (rdMale.Checked)
                _Person.Gendor = (short)enGendor.Male;
            else
                _Person.Gendor = (short)enGendor.Female;

            _Person.NationalityCountryID = NationalityCountryID;

            if (pbPersonImage.ImageLocation != null)
                _Person.ImagePath = pbPersonImage.ImageLocation;
            else
                _Person.ImagePath = "";

            if (_Person.Save())
            {
                lblPersonID.Text = _Person.PersonID.ToString();
                //change form mode to update.
                _Mode = enMode.Update;
                lblMain.Text = "Update Person";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);


                // Trigger the event to send data back to the caller form.
                DataBack?.Invoke(this, _Person.PersonID);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }


        bool _HandlePersonImage()
        {
            if (_Person.ImagePath != pbPersonImage.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    //first we delete the old image from the folder in case there is any.

                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {
                        // We could not delete the file.
                        //log it later   
                    }
                }

                if (pbPersonImage.ImageLocation != null)
                {
                    //then we copy the new image to the image folder after we rename it
                    string SourceImageFile = pbPersonImage.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbPersonImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }
            return true;

        }

        private void frmAddEditPerson_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            if(_Mode == enMode.Update)
                _LoadData();
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }
        }

        private void rdMale_CheckChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Resources.Male_512;
        }

        private void rdFemale_CheckChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Resources.Female_512;
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                pbPersonImage.Load(selectedFilePath);
                llRemoveImage.Visible = true;
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;
           
            if (rdMale.Checked)
                pbPersonImage.Image = Resources.Male_512;

            else if (rdFemale.Checked)
                pbPersonImage.Image = Resources.Female_512;

            llRemoveImage.Visible = false;
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text.Trim() == "")
                return;

            //validate email format
            if (!clsValidatoin.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            };
        }

        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
           

          
            if (!clsValidatoin.ValidatePhoneNumbe(txtPhone.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPhone, "Invalid Phone Number Format Shold be like 012-2345678");
            }
            else
            {
                errorProvider1.SetError(txtPhone, null);
            };
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
