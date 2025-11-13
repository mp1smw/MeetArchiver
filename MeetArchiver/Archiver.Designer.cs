namespace MeetArchiver
{
    partial class Archiver
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            loadDataToolStripMenuItem = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            button1 = new Button();
            tabControl1 = new TabControl();
            meetTab = new TabPage();
            meetsList = new ListBox();
            diversTab = new TabPage();
            withdrawnLbl = new Label();
            newLbl = new Label();
            mismatchLbl = new Label();
            matchedLbl = new Label();
            newList = new ListBox();
            typoList = new ListBox();
            missingList = new ListBox();
            matchedList = new ListBox();
            clubsTab = new TabPage();
            uploadTab = new TabPage();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tabControl1.SuspendLayout();
            meetTab.SuspendLayout();
            diversTab.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { loadDataToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1122, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // loadDataToolStripMenuItem
            // 
            loadDataToolStripMenuItem.Name = "loadDataToolStripMenuItem";
            loadDataToolStripMenuItem.Size = new Size(72, 20);
            loadDataToolStripMenuItem.Text = "Load Data";
            loadDataToolStripMenuItem.Click += loadDataToolStripMenuItem_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 24);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(button1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tabControl1);
            splitContainer1.Size = new Size(1122, 578);
            splitContainer1.SplitterDistance = 25;
            splitContainer1.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(41, 4);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(meetTab);
            tabControl1.Controls.Add(diversTab);
            tabControl1.Controls.Add(clubsTab);
            tabControl1.Controls.Add(uploadTab);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1122, 549);
            tabControl1.TabIndex = 0;
            // 
            // meetTab
            // 
            meetTab.Controls.Add(meetsList);
            meetTab.Location = new Point(4, 24);
            meetTab.Name = "meetTab";
            meetTab.Padding = new Padding(3);
            meetTab.Size = new Size(1114, 521);
            meetTab.TabIndex = 0;
            meetTab.Text = "Meet";
            meetTab.UseVisualStyleBackColor = true;
            // 
            // meetsList
            // 
            meetsList.FormattingEnabled = true;
            meetsList.ItemHeight = 15;
            meetsList.Location = new Point(8, 6);
            meetsList.Name = "meetsList";
            meetsList.Size = new Size(387, 484);
            meetsList.TabIndex = 0;
            meetsList.SelectedIndexChanged += meetsList_SelectedIndexChanged;
            // 
            // diversTab
            // 
            diversTab.Controls.Add(withdrawnLbl);
            diversTab.Controls.Add(newLbl);
            diversTab.Controls.Add(mismatchLbl);
            diversTab.Controls.Add(matchedLbl);
            diversTab.Controls.Add(newList);
            diversTab.Controls.Add(typoList);
            diversTab.Controls.Add(missingList);
            diversTab.Controls.Add(matchedList);
            diversTab.Location = new Point(4, 24);
            diversTab.Name = "diversTab";
            diversTab.Padding = new Padding(3);
            diversTab.Size = new Size(1114, 521);
            diversTab.TabIndex = 1;
            diversTab.Text = "Divers";
            diversTab.UseVisualStyleBackColor = true;
            // 
            // withdrawnLbl
            // 
            withdrawnLbl.AutoSize = true;
            withdrawnLbl.Location = new Point(798, 7);
            withdrawnLbl.Name = "withdrawnLbl";
            withdrawnLbl.Size = new Size(65, 15);
            withdrawnLbl.TabIndex = 7;
            withdrawnLbl.Text = "Withdrawn";
            // 
            // newLbl
            // 
            newLbl.AutoSize = true;
            newLbl.Location = new Point(533, 7);
            newLbl.Name = "newLbl";
            newLbl.Size = new Size(31, 15);
            newLbl.TabIndex = 6;
            newLbl.Text = "New";
            // 
            // mismatchLbl
            // 
            mismatchLbl.AutoSize = true;
            mismatchLbl.Location = new Point(268, 7);
            mismatchLbl.Name = "mismatchLbl";
            mismatchLbl.Size = new Size(73, 15);
            mismatchLbl.TabIndex = 5;
            mismatchLbl.Text = "Mismatched";
            // 
            // matchedLbl
            // 
            matchedLbl.AutoSize = true;
            matchedLbl.Location = new Point(6, 7);
            matchedLbl.Name = "matchedLbl";
            matchedLbl.Size = new Size(54, 15);
            matchedLbl.TabIndex = 4;
            matchedLbl.Text = "Matched";
            // 
            // newList
            // 
            newList.FormattingEnabled = true;
            newList.ItemHeight = 15;
            newList.Location = new Point(533, 27);
            newList.Name = "newList";
            newList.Size = new Size(259, 439);
            newList.TabIndex = 3;
            // 
            // typoList
            // 
            typoList.FormattingEnabled = true;
            typoList.ItemHeight = 15;
            typoList.Location = new Point(268, 27);
            typoList.Name = "typoList";
            typoList.Size = new Size(259, 439);
            typoList.TabIndex = 2;
            typoList.SelectedIndexChanged += typoList_SelectedIndexChanged;
            // 
            // missingList
            // 
            missingList.FormattingEnabled = true;
            missingList.ItemHeight = 15;
            missingList.Location = new Point(798, 27);
            missingList.Name = "missingList";
            missingList.Size = new Size(259, 439);
            missingList.TabIndex = 1;
            // 
            // matchedList
            // 
            matchedList.FormattingEnabled = true;
            matchedList.ItemHeight = 15;
            matchedList.Location = new Point(3, 27);
            matchedList.Name = "matchedList";
            matchedList.Size = new Size(259, 439);
            matchedList.TabIndex = 0;
            // 
            // clubsTab
            // 
            clubsTab.Location = new Point(4, 24);
            clubsTab.Name = "clubsTab";
            clubsTab.Size = new Size(1114, 521);
            clubsTab.TabIndex = 2;
            clubsTab.Text = "Clubs";
            clubsTab.UseVisualStyleBackColor = true;
            // 
            // uploadTab
            // 
            uploadTab.Location = new Point(4, 24);
            uploadTab.Name = "uploadTab";
            uploadTab.Size = new Size(1114, 521);
            uploadTab.TabIndex = 3;
            uploadTab.Text = "Upload";
            uploadTab.UseVisualStyleBackColor = true;
            // 
            // Archiver
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1122, 602);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Archiver";
            Text = "Meet Archiver";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            meetTab.ResumeLayout(false);
            diversTab.ResumeLayout(false);
            diversTab.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem loadDataToolStripMenuItem;
        private SplitContainer splitContainer1;
        private TabControl tabControl1;
        private TabPage meetTab;
        private TabPage diversTab;
        private TabPage clubsTab;
        private TabPage uploadTab;
        private ListBox meetsList;
        private ListBox matchedList;
        private ListBox newList;
        private ListBox typoList;
        private ListBox missingList;
        private Label newLbl;
        private Label mismatchLbl;
        private Label matchedLbl;
        private Label withdrawnLbl;
        private Button button1;
    }
}
