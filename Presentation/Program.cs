using Re_Project.Login;
using System;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Re_Project
{
    static class Program
    {
        [DllImport("Shcore.dll")]
        static extern int SetProcessDpiAwareness(int PROCESS_DPI_AWARENESS);

        // According to https://msdn.microsoft.com/en-us/library/windows/desktop/dn280512(v=vs.85).aspx
        private enum DpiAwareness
        {
            None = 0,
            SystemAware = 1,
            PerMonitorAware = 2
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        [Obsolete]
        static void Main()
        {
            

            string LicenseKey = ConfigurationSettings.AppSettings[0];
            //Register Syncfusion license https://help.syncfusion.com/common/essential-studio/licensing/how-to-generate

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(LicenseKey);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SetProcessDpiAwareness((int)DpiAwareness.PerMonitorAware);

            Application.Run(new frmLogin());
 
        }
    }
}
