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
            findBtn = new Button();
            searchTxt = new TextBox();
            searchClubLst = new ListBox();
            acceptSearchResultBtn = new Button();
            sugestedClubLst = new ListBox();
            acceptSuggestionBtn = new Button();
            button1 = new Button();
            mismatchedClubsLst = new ListBox();
            label1 = new Label();
            uploadTab = new TabPage();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tabControl1.SuspendLayout();
            meetTab.SuspendLayout();
            diversTab.SuspendLayout();
            clubsTab.SuspendLayout();
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
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tabControl1);
            splitContainer1.Size = new Size(1122, 578);
            splitContainer1.SplitterDistance = 25;
            splitContainer1.TabIndex = 1;
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
            clubsTab.Controls.Add(findBtn);
            clubsTab.Controls.Add(searchTxt);
            clubsTab.Controls.Add(searchClubLst);
            clubsTab.Controls.Add(acceptSearchResultBtn);
            clubsTab.Controls.Add(sugestedClubLst);
            clubsTab.Controls.Add(acceptSuggestionBtn);
            clubsTab.Controls.Add(button1);
            clubsTab.Controls.Add(mismatchedClubsLst);
            clubsTab.Controls.Add(label1);
            clubsTab.Controls.Add(groupBox1);
            clubsTab.Controls.Add(groupBox2);
            clubsTab.Location = new Point(4, 24);
            clubsTab.Name = "clubsTab";
            clubsTab.Size = new Size(1114, 521);
            clubsTab.TabIndex = 2;
            clubsTab.Text = "Clubs";
            clubsTab.UseVisualStyleBackColor = true;
            // 
            // findBtn
            // 
            findBtn.Location = new Point(574, 269);
            findBtn.Name = "findBtn";
            findBtn.Size = new Size(75, 23);
            findBtn.TabIndex = 11;
            findBtn.Text = "Find";
            findBtn.UseVisualStyleBackColor = true;
            findBtn.Click += findBtn_Click;
            // 
            // searchTxt
            // 
            searchTxt.Location = new Point(332, 269);
            searchTxt.Name = "searchTxt";
            searchTxt.Size = new Size(222, 23);
            searchTxt.TabIndex = 10;
            // 
            // searchClubLst
            // 
            searchClubLst.FormattingEnabled = true;
            searchClubLst.ItemHeight = 15;
            searchClubLst.Location = new Point(332, 312);
            searchClubLst.Name = "searchClubLst";
            searchClubLst.Size = new Size(222, 94);
            searchClubLst.TabIndex = 9;
            // 
            // acceptSearchResultBtn
            // 
            acceptSearchResultBtn.Location = new Point(574, 312);
            acceptSearchResultBtn.Name = "acceptSearchResultBtn";
            acceptSearchResultBtn.Size = new Size(75, 23);
            acceptSearchResultBtn.TabIndex = 8;
            acceptSearchResultBtn.Text = "Accept";
            acceptSearchResultBtn.UseVisualStyleBackColor = true;
            acceptSearchResultBtn.Click += acceptSearchResultBtn_Click;
            // 
            // sugestedClubLst
            // 
            sugestedClubLst.FormattingEnabled = true;
            sugestedClubLst.ItemHeight = 15;
            sugestedClubLst.Location = new Point(332, 49);
            sugestedClubLst.Name = "sugestedClubLst";
            sugestedClubLst.Size = new Size(222, 94);
            sugestedClubLst.TabIndex = 6;
            // 
            // acceptSuggestionBtn
            // 
            acceptSuggestionBtn.Location = new Point(574, 49);
            acceptSuggestionBtn.Name = "acceptSuggestionBtn";
            acceptSuggestionBtn.Size = new Size(75, 23);
            acceptSuggestionBtn.TabIndex = 5;
            acceptSuggestionBtn.Text = "Accept";
            acceptSuggestionBtn.UseVisualStyleBackColor = true;
            acceptSuggestionBtn.Click += acceptSuggestionBtn_Click;
            // 
            // button1
            // 
            button1.Location = new Point(133, 11);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 2;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // mismatchedClubsLst
            // 
            mismatchedClubsLst.FormattingEnabled = true;
            mismatchedClubsLst.ItemHeight = 15;
            mismatchedClubsLst.Location = new Point(12, 40);
            mismatchedClubsLst.Name = "mismatchedClubsLst";
            mismatchedClubsLst.Size = new Size(222, 394);
            mismatchedClubsLst.TabIndex = 1;
            mismatchedClubsLst.SelectedIndexChanged += mismatchedClubsLst_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 14);
            label1.Name = "label1";
            label1.Size = new Size(93, 15);
            label1.TabIndex = 0;
            label1.Text = "Clubs not found";
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
            // groupBox1
            // 
            groupBox1.Location = new Point(316, 225);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(360, 206);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            groupBox1.Text = "Manual club search";
            // 
            // groupBox2
            // 
            groupBox2.Location = new Point(316, 25);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(362, 134);
            groupBox2.TabIndex = 13;
            groupBox2.TabStop = false;
            groupBox2.Text = "Suggested clubs";
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
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            meetTab.ResumeLayout(false);
            diversTab.ResumeLayout(false);
            diversTab.PerformLayout();
            clubsTab.ResumeLayout(false);
            clubsTab.PerformLayout();
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
        private ListBox mismatchedClubsLst;
        private Label label1;
        private Button button1;
        private ListBox sugestedClubLst;
        private Button acceptSuggestionBtn;
        private ListBox searchClubLst;
        private Button acceptSearchResultBtn;
        private Button findBtn;
        private TextBox searchTxt;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
    }
}
