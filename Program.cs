using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SIPVoipSDK;

namespace MlSampleWindowCS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
			try
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new SIPPhoneForm());
			}
			catch(Exception exc)
			{
				MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK);
				return;
			}
        }
    }
}