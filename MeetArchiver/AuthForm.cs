using System;
using System.Windows.Forms;

namespace MeetArchiver
{
    public partial class AuthForm : Form
    {
        /// <summary>
        /// The password entered by the user. Available after dialog closes with DialogResult.OK.
        /// </summary>
        public string EnteredPassword { get; private set; } = string.Empty;

        public AuthForm()
        {
            InitializeComponent();

            // Ensure OK button behaves as accept and Cancel as cancel
            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }

        private void chkShow_CheckedChanged(object? sender, EventArgs e)
        {
            // Toggle password reveal
            txtPassword.UseSystemPasswordChar = !chkShow.Checked;
        }

        private void btnOK_Click(object? sender, EventArgs e)
        {
            // capture and close with OK
            EnteredPassword = txtPassword.Text ?? string.Empty;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object? sender, EventArgs e)
        {
            // ensure password cleared and close with Cancel
            EnteredPassword = string.Empty;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}