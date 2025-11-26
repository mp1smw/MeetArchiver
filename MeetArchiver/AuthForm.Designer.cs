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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthForm));
            lblPrompt = new Label();
            txtPassword = new TextBox();
            chkShow = new CheckBox();
            btnOK = new Button();
            btnCancel = new Button();
            emailTxt = new Label();
            txtEmail = new TextBox();
            SuspendLayout();
            // 
            // lblPrompt
            // 
            lblPrompt.Location = new Point(12, 79);
            lblPrompt.Name = "lblPrompt";
            lblPrompt.Size = new Size(396, 24);
            lblPrompt.TabIndex = 0;
            lblPrompt.Text = "Please enter your passkey:";
            lblPrompt.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(15, 109);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(330, 23);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // chkShow
            // 
            chkShow.Location = new Point(353, 109);
            chkShow.Name = "chkShow";
            chkShow.Size = new Size(55, 24);
            chkShow.TabIndex = 2;
            chkShow.Text = "Show";
            chkShow.CheckedChanged += chkShow_CheckedChanged;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(240, 147);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(85, 28);
            btnOK.TabIndex = 3;
            btnOK.Text = "OK";
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(330, 147);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(85, 28);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // emailTxt
            // 
            emailTxt.Location = new Point(12, 18);
            emailTxt.Name = "emailTxt";
            emailTxt.Size = new Size(396, 24);
            emailTxt.TabIndex = 5;
            emailTxt.Text = "Please enter your email:";
            emailTxt.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(15, 48);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(330, 23);
            txtEmail.TabIndex = 6;
            // 
            // AuthForm
            // 
            AcceptButton = btnOK;
            CancelButton = btnCancel;
            ClientSize = new Size(420, 184);
            Controls.Add(emailTxt);
            Controls.Add(txtEmail);
            Controls.Add(lblPrompt);
            Controls.Add(txtPassword);
            Controls.Add(chkShow);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AuthForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Authentication";
            TopMost = true;
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
        private Label emailTxt;
        private TextBox txtEmail;
    }
}