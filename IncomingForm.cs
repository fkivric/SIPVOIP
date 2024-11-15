using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static MlSampleWindowCS.SIPPhoneForm;

namespace MlSampleWindowCS
{
	public partial class IncomingForm : Form
	{
        DbQuery conn = new DbQuery();
        SqlConnection sql = new SqlConnection(Properties.Settings.Default.ConnectionString);
        public IncomingForm()
		{
			InitializeComponent();
		}
        string phoneNumber = "";

        private void buttonYes_Click(object sender, EventArgs e)
		{
            string input = textBoxCaller.Text;

            // Regular expression ile numarayý almak için desen
            string pattern = @"sip:(\d+)@";

            // Regex ile eþleþmeyi yap
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                phoneNumber = match.Groups[1].Value; // Gruplardan 1. grubu al
            }
            else
            {
                Console.WriteLine("Numara bulunamadý.");
            }
            CallList call = new CallList();
            call.CallNumber = phoneNumber;
            call.CallType = 2;
            call.CallUser = SIPPhoneForm.loginuser;
            call.CallRecall = "1";
            var sonuc = DbQuery.GetValue($"SELECT [CallNumber] FROM [CallCenter].[dbo].[CallList] where CallNumber = '{phoneNumber}'", Properties.Settings.Default.ConnectionString);
            if (sonuc != phoneNumber)
            {
                conn.InsertCallList(call, sql);
            }
            this.DialogResult = DialogResult.Yes;
			this.Close();
		}

		private void buttonNo_Click(object sender, EventArgs e)
        {
            string input = textBoxCaller.Text;

            // Regular expression ile numarayý almak için desen
            string pattern = @"sip:(\d+)@";

            // Regex ile eþleþmeyi yap
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                phoneNumber = match.Groups[1].Value; // Gruplardan 1. grubu al
            }
            CallList call = new CallList();
            call.CallNumber = phoneNumber;
            call.CallType = 2;
            call.CallUser = SIPPhoneForm.loginuser;
            call.CallRecall = "0";
            var sonuc = DbQuery.GetValue($"SELECT [CallNumber] FROM [CallCenter].[dbo].[CallList] where CallNumber = '{phoneNumber}'", Properties.Settings.Default.ConnectionString);
            if (sonuc != phoneNumber)
            {
                conn.InsertCallList(call, sql);
            }
            this.DialogResult = DialogResult.No;
			this.Close();
		}

        private void IncomingForm_Load(object sender, EventArgs e)
        {

        }

        private void IncomingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string input = textBoxCaller.Text;

            // Regular expression ile numarayý almak için desen
            string pattern = @"sip:(\d+)@";

            // Regex ile eþleþmeyi yap
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                phoneNumber = match.Groups[1].Value; // Gruplardan 1. grubu al
            }
            CallList call = new CallList();
            call.CallNumber = phoneNumber;
            call.CallType = 2;
            call.CallUser = SIPPhoneForm.loginuser;
            call.CallRecall = "0";
            var sonuc = DbQuery.GetValue($"SELECT [CallNumber] FROM [CallCenter].[dbo].[CallList] where CallNumber = '{phoneNumber}'", Properties.Settings.Default.ConnectionString);
            if (sonuc != phoneNumber)
            {
                conn.InsertCallList(call, sql);
            }

        }
    }
}