namespace MeetArchiver
{
    partial class DiverMatcher
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
            label1 = new Label();
            label2 = new Label();
            matchLst = new ListBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 25);
            label1.Name = "label1";
            label1.Size = new Size(77, 15);
            label1.TabIndex = 0;
            label1.Text = "Current Diver";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(610, 25);
            label2.Name = "label2";
            label2.Size = new Size(92, 15);
            label2.TabIndex = 1;
            label2.Text = "Suggested Diver";
            // 
            // matchLst
            // 
            matchLst.FormattingEnabled = true;
            matchLst.ItemHeight = 15;
            matchLst.Location = new Point(412, 25);
            matchLst.Name = "matchLst";
            matchLst.Size = new Size(177, 259);
            matchLst.TabIndex = 2;
            matchLst.SelectedIndexChanged += matchLst_SelectedIndexChanged;
            // 
            // DiverMatcher
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(993, 342);
            Controls.Add(matchLst);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "DiverMatcher";
            Text = "Match Diver";
            Load += DiverMatcher_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ListBox matchLst;
    }
}