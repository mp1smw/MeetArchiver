using System.Drawing;
using System.Windows.Forms;

namespace MeetArchiver
{
    partial class AuthForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblPrompt;
        private TextBox txtPassword;
        private CheckBox chkShow;
        private Button btnOK;
        private Button btnCancel;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblPrompt = new Label();
            txtPassword = new TextBox();
            chkShow = new CheckBox();
            btnOK = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // lblPrompt
            // 
            lblPrompt.Location = new Point(12, 12);
            lblPrompt.Name = "lblPrompt";
            lblPrompt.Size = new Size(396, 24);
            lblPrompt.TabIndex = 0;
            lblPrompt.Text = "Please enter your passkey:";
            lblPrompt.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(15, 42);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(330, 23);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // chkShow
            // 
            chkShow.Location = new Point(353, 42);
            chkShow.Name = "chkShow";
            chkShow.Size = new Size(55, 24);
            chkShow.TabIndex = 2;
            chkShow.Text = "Show";
            chkShow.CheckedChanged += chkShow_CheckedChanged;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(240, 80);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(85, 28);
            btnOK.TabIndex = 3;
            btnOK.Text = "OK";
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(330, 80);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(85, 28);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // AuthForm
            // 
            AcceptButton = btnOK;
            CancelButton = btnCancel;
            ClientSize = new Size(420, 130);
            Controls.Add(lblPrompt);
            Controls.Add(txtPassword);
            Controls.Add(chkShow);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AuthForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Authentication";
            ResumeLayout(false);
            PerformLayout();
        }

        /// <summary> 
        /// Dispose resources.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}