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
            label3 = new Label();
            takeMatchBtn = new Button();
            overwriteDiverBtn = new Button();
            createDiverBtn = new Button();
            label4 = new Label();
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
            label2.Size = new Size(77, 15);
            label2.TabIndex = 1;
            label2.Text = "Archive Diver";
            // 
            // matchLst
            // 
            matchLst.FormattingEnabled = true;
            matchLst.ItemHeight = 15;
            matchLst.Location = new Point(412, 53);
            matchLst.Name = "matchLst";
            matchLst.Size = new Size(177, 109);
            matchLst.TabIndex = 2;
            matchLst.SelectedIndexChanged += matchLst_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(412, 25);
            label3.Name = "label3";
            label3.Size = new Size(98, 15);
            label3.TabIndex = 3;
            label3.Text = "Possible matches";
            // 
            // takeMatchBtn
            // 
            takeMatchBtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            takeMatchBtn.Location = new Point(412, 186);
            takeMatchBtn.Name = "takeMatchBtn";
            takeMatchBtn.Size = new Size(177, 29);
            takeMatchBtn.TabIndex = 4;
            takeMatchBtn.Text = "<<     Take archive diver";
            takeMatchBtn.UseVisualStyleBackColor = true;
            takeMatchBtn.Click += takeMatchBtn_Click;
            // 
            // overwriteDiverBtn
            // 
            overwriteDiverBtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            overwriteDiverBtn.Location = new Point(412, 221);
            overwriteDiverBtn.Name = "overwriteDiverBtn";
            overwriteDiverBtn.Size = new Size(177, 29);
            overwriteDiverBtn.TabIndex = 5;
            overwriteDiverBtn.Text = "Overwrite archive diver   >>";
            overwriteDiverBtn.UseVisualStyleBackColor = true;
            overwriteDiverBtn.Click += overwriteDiverBtn_Click;
            // 
            // createDiverBtn
            // 
            createDiverBtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            createDiverBtn.Location = new Point(412, 301);
            createDiverBtn.Name = "createDiverBtn";
            createDiverBtn.Size = new Size(177, 29);
            createDiverBtn.TabIndex = 6;
            createDiverBtn.TabStop = false;
            createDiverBtn.Text = "Create new diver";
            createDiverBtn.UseVisualStyleBackColor = true;
            createDiverBtn.Click += createDiverBtn_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(473, 283);
            label4.Name = "label4";
            label4.Size = new Size(60, 15);
            label4.TabIndex = 7;
            label4.Text = "No match";
            // 
            // DiverMatcher
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(993, 342);
            Controls.Add(label4);
            Controls.Add(createDiverBtn);
            Controls.Add(overwriteDiverBtn);
            Controls.Add(takeMatchBtn);
            Controls.Add(label3);
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
        private Label label3;
        private Button takeMatchBtn;
        private Button overwriteDiverBtn;
        private Button createDiverBtn;
        private Label label4;
    }
}