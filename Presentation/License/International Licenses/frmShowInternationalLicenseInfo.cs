﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Re_Project.License.International_Licenses
{
    public partial class frmShowInternationalLicenseInfo : Form
    {
        int _InternationalID;
        public frmShowInternationalLicenseInfo(int internationalID)
        {
            InitializeComponent();
            _InternationalID = internationalID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowInternationalLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlDriverInternationalLicenseInfo1.LoadInfo(_InternationalID);
        }
    }
}
