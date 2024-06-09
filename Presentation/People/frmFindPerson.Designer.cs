namespace Re_Project.People
{
    partial class frmFindPerson
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctlrPersonCardInfoWithFilter1 = new Re_Project.People.ctlrPersonCardInfoWithFilter();
            this.lblMain = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnClose = new Syncfusion.WinForms.Controls.SfButton();
            this.SuspendLayout();
            // 
            // ctlrPersonCardInfoWithFilter1
            // 
            this.ctlrPersonCardInfoWithFilter1.Location = new System.Drawing.Point(3, 84);
            this.ctlrPersonCardInfoWithFilter1.Name = "ctlrPersonCardInfoWithFilter1";
            this.ctlrPersonCardInfoWithFilter1.Size = new System.Drawing.Size(883, 389);
            this.ctlrPersonCardInfoWithFilter1.TabIndex = 0;
            // 
            // lblMain
            // 
            this.lblMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.lblMain.Location = new System.Drawing.Point(309, 25);
            this.lblMain.Name = "lblMain";
            this.lblMain.Size = new System.Drawing.Size(204, 38);
            this.lblMain.TabIndex = 150;
            this.lblMain.Text = "Find Person";
            // 
            // btnClose
            // 
            this.btnClose.AccessibleName = "Button";
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClose.Location = new System.Drawing.Point(691, 483);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(122, 34);
            this.btnClose.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnClose.Style.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClose.Style.HoverBackColor = System.Drawing.Color.RosyBrown;
            this.btnClose.TabIndex = 151;
            this.btnClose.Text = "Close";
            this.btnClose.ThemeName = "Office2016Black";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmFindPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 528);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblMain);
            this.Controls.Add(this.ctlrPersonCardInfoWithFilter1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmFindPerson";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find Person";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctlrPersonCardInfoWithFilter ctlrPersonCardInfoWithFilter1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel lblMain;
        private Syncfusion.WinForms.Controls.SfButton btnClose;
    }
}