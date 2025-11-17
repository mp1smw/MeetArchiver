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
            label1 = new Label();
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
            // label1
            // 
            label1.Location = new Point(12, 18);
            label1.Name = "label1";
            label1.Size = new Size(396, 24);
            label1.TabIndex = 5;
            label1.Text = "Please enter your email:";
            label1.TextAlign = ContentAlignment.MiddleLeft;
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
            Controls.Add(label1);
            Controls.Add(txtEmail);
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
        private Label label1;
        private TextBox txtEmail;
    }
}