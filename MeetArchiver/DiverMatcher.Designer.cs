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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiverMatcher));
            currDiverLbl = new Label();
            ArchiveDiverLbl = new Label();
            matchLst = new ListBox();
            possibleMatchLbl = new Label();
            takeMatchBtn = new Button();
            overwriteDiverBtn = new Button();
            createDiverBtn = new Button();
            noMatchLbl = new Label();
            SuspendLayout();
            // 
            // currDiverLbl
            // 
            currDiverLbl.AutoSize = true;
            currDiverLbl.Location = new Point(15, 25);
            currDiverLbl.Name = "currDiverLbl";
            currDiverLbl.Size = new Size(77, 15);
            currDiverLbl.TabIndex = 0;
            currDiverLbl.Text = "Current Diver";
            // 
            // ArchiveDiverLbl
            // 
            ArchiveDiverLbl.AutoSize = true;
            ArchiveDiverLbl.Location = new Point(610, 25);
            ArchiveDiverLbl.Name = "ArchiveDiverLbl";
            ArchiveDiverLbl.Size = new Size(77, 15);
            ArchiveDiverLbl.TabIndex = 1;
            ArchiveDiverLbl.Text = "Archive Diver";
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
            // possibleMatchLbl
            // 
            possibleMatchLbl.AutoSize = true;
            possibleMatchLbl.Location = new Point(412, 25);
            possibleMatchLbl.Name = "possibleMatchLbl";
            possibleMatchLbl.Size = new Size(98, 15);
            possibleMatchLbl.TabIndex = 3;
            possibleMatchLbl.Text = "Possible matches";
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
            // noMatchLbl
            // 
            noMatchLbl.AutoSize = true;
            noMatchLbl.Location = new Point(473, 283);
            noMatchLbl.Name = "noMatchLbl";
            noMatchLbl.Size = new Size(60, 15);
            noMatchLbl.TabIndex = 7;
            noMatchLbl.Text = "No match";
            // 
            // DiverMatcher
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(993, 342);
            Controls.Add(noMatchLbl);
            Controls.Add(createDiverBtn);
            Controls.Add(overwriteDiverBtn);
            Controls.Add(takeMatchBtn);
            Controls.Add(possibleMatchLbl);
            Controls.Add(matchLst);
            Controls.Add(ArchiveDiverLbl);
            Controls.Add(currDiverLbl);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "DiverMatcher";
            Text = "Match Diver";
            Load += DiverMatcher_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label currDiverLbl;
        private Label ArchiveDiverLbl;
        private ListBox matchLst;
        private Label possibleMatchLbl;
        private Button takeMatchBtn;
        private Button overwriteDiverBtn;
        private Button createDiverBtn;
        private Label noMatchLbl;
    }
}