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
            splitContainer1 = new SplitContainer();
            InstructionLbl = new Label();
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
            button1 = new Button();
            findBtn = new Button();
            searchTxt = new TextBox();
            searchClubLst = new ListBox();
            acceptSearchResultBtn = new Button();
            sugestedClubLst = new ListBox();
            acceptSuggestionBtn = new Button();
            mismatchedClubsLst = new ListBox();
            label1 = new Label();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            uploadTab = new TabPage();
            textBox1 = new TextBox();
            internationalDescriptionTxt = new TextBox();
            checkBox1 = new CheckBox();
            label3 = new Label();
            nationCmb = new ComboBox();
            label2 = new Label();
            logTxtBox = new RichTextBox();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tabControl1.SuspendLayout();
            meetTab.SuspendLayout();
            diversTab.SuspendLayout();
            clubsTab.SuspendLayout();
            uploadTab.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(InstructionLbl);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tabControl1);
            splitContainer1.Size = new Size(1122, 602);
            splitContainer1.SplitterDistance = 63;
            splitContainer1.TabIndex = 1;
            // 
            // InstructionLbl
            // 
            InstructionLbl.AutoSize = true;
            InstructionLbl.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            InstructionLbl.ForeColor = Color.DarkGreen;
            InstructionLbl.Location = new Point(12, 11);
            InstructionLbl.Name = "InstructionLbl";
            InstructionLbl.Size = new Size(51, 20);
            InstructionLbl.TabIndex = 0;
            InstructionLbl.Text = "label2";
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
            tabControl1.Size = new Size(1122, 535);
            tabControl1.TabIndex = 0;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // meetTab
            // 
            meetTab.Controls.Add(meetsList);
            meetTab.Location = new Point(4, 24);
            meetTab.Name = "meetTab";
            meetTab.Padding = new Padding(3);
            meetTab.Size = new Size(1114, 507);
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
            diversTab.Size = new Size(1114, 507);
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
            clubsTab.Controls.Add(button1);
            clubsTab.Controls.Add(findBtn);
            clubsTab.Controls.Add(searchTxt);
            clubsTab.Controls.Add(searchClubLst);
            clubsTab.Controls.Add(acceptSearchResultBtn);
            clubsTab.Controls.Add(sugestedClubLst);
            clubsTab.Controls.Add(acceptSuggestionBtn);
            clubsTab.Controls.Add(mismatchedClubsLst);
            clubsTab.Controls.Add(label1);
            clubsTab.Controls.Add(groupBox1);
            clubsTab.Controls.Add(groupBox2);
            clubsTab.Location = new Point(4, 24);
            clubsTab.Name = "clubsTab";
            clubsTab.Size = new Size(1114, 507);
            clubsTab.TabIndex = 2;
            clubsTab.Text = "Clubs";
            clubsTab.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(316, 411);
            button1.Name = "button1";
            button1.Size = new Size(126, 23);
            button1.TabIndex = 14;
            button1.Text = "Create new Club";
            button1.UseVisualStyleBackColor = true;
            // 
            // findBtn
            // 
            findBtn.Location = new Point(574, 229);
            findBtn.Name = "findBtn";
            findBtn.Size = new Size(75, 23);
            findBtn.TabIndex = 11;
            findBtn.Text = "Find";
            findBtn.UseVisualStyleBackColor = true;
            findBtn.Click += findBtn_Click;
            // 
            // searchTxt
            // 
            searchTxt.Location = new Point(332, 229);
            searchTxt.Name = "searchTxt";
            searchTxt.Size = new Size(222, 23);
            searchTxt.TabIndex = 10;
            // 
            // searchClubLst
            // 
            searchClubLst.FormattingEnabled = true;
            searchClubLst.ItemHeight = 15;
            searchClubLst.Location = new Point(332, 272);
            searchClubLst.Name = "searchClubLst";
            searchClubLst.Size = new Size(222, 94);
            searchClubLst.TabIndex = 9;
            // 
            // acceptSearchResultBtn
            // 
            acceptSearchResultBtn.Location = new Point(574, 272);
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
            // mismatchedClubsLst
            // 
            mismatchedClubsLst.FormattingEnabled = true;
            mismatchedClubsLst.ItemHeight = 15;
            mismatchedClubsLst.Location = new Point(12, 40);
            mismatchedClubsLst.Name = "mismatchedClubsLst";
            mismatchedClubsLst.Size = new Size(270, 394);
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
            // groupBox1
            // 
            groupBox1.Location = new Point(316, 178);
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
            // uploadTab
            // 
            uploadTab.Controls.Add(textBox1);
            uploadTab.Controls.Add(internationalDescriptionTxt);
            uploadTab.Controls.Add(checkBox1);
            uploadTab.Controls.Add(label3);
            uploadTab.Controls.Add(nationCmb);
            uploadTab.Controls.Add(label2);
            uploadTab.Controls.Add(logTxtBox);
            uploadTab.Controls.Add(button2);
            uploadTab.Location = new Point(4, 24);
            uploadTab.Name = "uploadTab";
            uploadTab.Size = new Size(1114, 507);
            uploadTab.TabIndex = 3;
            uploadTab.Text = "Upload";
            uploadTab.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.MistyRose;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Location = new Point(777, 147);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(242, 68);
            textBox1.TabIndex = 7;
            textBox1.Text = "This is the country the meet took place in, it is auto selected on your current location please change if you are not where the meet took place";
            // 
            // internationalDescriptionTxt
            // 
            internationalDescriptionTxt.BackColor = Color.MistyRose;
            internationalDescriptionTxt.BorderStyle = BorderStyle.None;
            internationalDescriptionTxt.Location = new Point(777, 39);
            internationalDescriptionTxt.Multiline = true;
            internationalDescriptionTxt.Name = "internationalDescriptionTxt";
            internationalDescriptionTxt.Size = new Size(242, 68);
            internationalDescriptionTxt.TabIndex = 6;
            internationalDescriptionTxt.Text = "Only select this option if all participant are representing their country, it will display their nation rather than their club  in the  results section";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(604, 39);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(123, 19);
            checkBox1.TabIndex = 5;
            checkBox1.Text = "International Meet";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(606, 129);
            label3.Name = "label3";
            label3.Size = new Size(43, 15);
            label3.TabIndex = 4;
            label3.Text = "Nation";
            // 
            // nationCmb
            // 
            nationCmb.AutoCompleteCustomSource.AddRange(new string[] { "AFG", "ALA", "ALB", "DZA", "ASM", "AND", "AGO", "AIA", "ATA", "ATG", "ARG", "ARM", "ABW", "AUS", "AUT", "AZE", "BHS", "BHR", "BGD", "BRB", "BLR", "BEL", "BLZ", "BEN", "BMU", "BTN", "BOL", "BES", "BIH", "BWA", "BVT", "BRA", "IOT", "BRN", "BGR", "BFA", "BDI", "CPV", "KHM", "CMR", "CAN", "CYM", "CAF", "TCD", "CHL", "CHN", "CXR", "CCK", "COL", "COM", "COD", "COG", "COK", "CRI", "HRV", "CUB", "CUW", "CYP", "CZE", "CIV", "DNK", "DJI", "DMA", "DOM", "ECU", "EGY", "SLV", "GNQ", "ERI", "EST", "SWZ", "ETH", "FLK", "FRO", "FJI", "FIN", "FRA", "GUF", "PYF", "ATF", "GAB", "GMB", "GEO", "DEU", "GHA", "GIB", "GRC", "GRL", "GRD", "GLP", "GUM", "GTM", "GGY", "GIN", "GNB", "GUY", "HTI", "HMD", "VAT", "HND", "HKG", "HUN", "ISL", "IND", "IDN", "IRN", "IRQ", "IRL", "IMN", "ISR", "ITA", "JAM", "JPN", "JEY", "JOR", "KAZ", "KEN", "KIR", "PRK", "KOR", "KWT", "KGZ", "LAO", "LVA", "LBN", "LSO", "LBR", "LBY", "LIE", "LTU", "LUX", "MAC", "MDG", "MWI", "MYS", "MDV", "MLI", "MLT", "MHL", "MTQ", "MRT", "MUS", "MYT", "MEX", "FSM", "MDA", "MCO", "MNG", "MNE", "MSR", "MAR", "MOZ", "MMR", "NAM", "NRU", "NPL", "NLD", "NCL", "NZL", "NIC", "NER", "NGA", "NIU", "NFK", "MNP", "NOR", "OMN", "PAK", "PLW", "PSE", "PAN", "PNG", "PRY", "PER", "PHL", "PCN", "POL", "PRT", "PRI", "QAT", "MKD", "ROU", "RUS", "RWA", "REU", "BLM", "SHN", "KNA", "LCA", "MAF", "SPM", "VCT", "WSM", "SMR", "STP", "SAU", "SEN", "SRB", "SYC", "SLE", "SGP", "SXM", "SVK", "SVN", "SLB", "SOM", "ZAF", "SGS", "SSD", "ESP", "LKA", "SDN", "SUR", "SJM", "SWE", "CHE", "SYR", "TWN", "TJK", "TZA", "THA", "TLS", "TGO", "TKL", "TON", "TTO", "TUN", "TUR", "TKM", "TCA", "TUV", "UGA", "UKR", "ARE", "GBR", "UMI", "USA", "URY", "UZB", "VUT", "VEN", "VNM", "VGB", "VIR", "WLF", "ESH", "YEM", "ZMB", "ZWE" });
            nationCmb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            nationCmb.AutoCompleteSource = AutoCompleteSource.ListItems;
            nationCmb.FormattingEnabled = true;
            nationCmb.Items.AddRange(new object[] { "AFG", "ALA", "ALB", "DZA", "ASM", "AND", "AGO", "AIA", "ATA", "ATG", "ARG", "ARM", "ABW", "AUS", "AUT", "AZE", "BHS", "BHR", "BGD", "BRB", "BLR", "BEL", "BLZ", "BEN", "BMU", "BTN", "BOL", "BES", "BIH", "BWA", "BVT", "BRA", "IOT", "BRN", "BGR", "BFA", "BDI", "CPV", "KHM", "CMR", "CAN", "CYM", "CAF", "TCD", "CHL", "CHN", "CXR", "CCK", "COL", "COM", "COD", "COG", "COK", "CRI", "HRV", "CUB", "CUW", "CYP", "CZE", "CIV", "DNK", "DJI", "DMA", "DOM", "ECU", "EGY", "SLV", "GNQ", "ERI", "EST", "SWZ", "ETH", "FLK", "FRO", "FJI", "FIN", "FRA", "GUF", "PYF", "ATF", "GAB", "GMB", "GEO", "DEU", "GHA", "GIB", "GRC", "GRL", "GRD", "GLP", "GUM", "GTM", "GGY", "GIN", "GNB", "GUY", "HTI", "HMD", "VAT", "HND", "HKG", "HUN", "ISL", "IND", "IDN", "IRN", "IRQ", "IRL", "IMN", "ISR", "ITA", "JAM", "JPN", "JEY", "JOR", "KAZ", "KEN", "KIR", "PRK", "KOR", "KWT", "KGZ", "LAO", "LVA", "LBN", "LSO", "LBR", "LBY", "LIE", "LTU", "LUX", "MAC", "MDG", "MWI", "MYS", "MDV", "MLI", "MLT", "MHL", "MTQ", "MRT", "MUS", "MYT", "MEX", "FSM", "MDA", "MCO", "MNG", "MNE", "MSR", "MAR", "MOZ", "MMR", "NAM", "NRU", "NPL", "NLD", "NCL", "NZL", "NIC", "NER", "NGA", "NIU", "NFK", "MNP", "NOR", "OMN", "PAK", "PLW", "PSE", "PAN", "PNG", "PRY", "PER", "PHL", "PCN", "POL", "PRT", "PRI", "QAT", "MKD", "ROU", "RUS", "RWA", "REU", "BLM", "SHN", "KNA", "LCA", "MAF", "SPM", "VCT", "WSM", "SMR", "STP", "SAU", "SEN", "SRB", "SYC", "SLE", "SGP", "SXM", "SVK", "SVN", "SLB", "SOM", "ZAF", "SGS", "SSD", "ESP", "LKA", "SDN", "SUR", "SJM", "SWE", "CHE", "SYR", "TWN", "TJK", "TZA", "THA", "TLS", "TGO", "TKL", "TON", "TTO", "TUN", "TUR", "TKM", "TCA", "TUV", "UGA", "UKR", "ARE", "GBR", "UMI", "USA", "URY", "UZB", "VUT", "VEN", "VNM", "VGB", "VIR", "WLF", "ESH", "YEM", "ZMB", "ZWE" });
            nationCmb.Location = new Point(606, 147);
            nationCmb.Name = "nationCmb";
            nationCmb.Size = new Size(121, 23);
            nationCmb.TabIndex = 3;
            nationCmb.SelectedIndexChanged += nationCmb_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(35, 70);
            label2.Name = "label2";
            label2.Size = new Size(32, 15);
            label2.TabIndex = 2;
            label2.Text = "Logs";
            // 
            // logTxtBox
            // 
            logTxtBox.Location = new Point(35, 88);
            logTxtBox.Name = "logTxtBox";
            logTxtBox.Size = new Size(470, 384);
            logTxtBox.TabIndex = 1;
            logTxtBox.Text = "";
            // 
            // button2
            // 
            button2.Location = new Point(32, 26);
            button2.Name = "button2";
            button2.Size = new Size(107, 23);
            button2.TabIndex = 0;
            button2.Text = "Archive Meet";
            button2.UseVisualStyleBackColor = true;
            button2.Click += ArchiveEventNow;
            // 
            // Archiver
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1122, 602);
            Controls.Add(splitContainer1);
            Name = "Archiver";
            Text = "Meet Archiver";
            Load += Archiver_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            meetTab.ResumeLayout(false);
            diversTab.ResumeLayout(false);
            diversTab.PerformLayout();
            clubsTab.ResumeLayout(false);
            clubsTab.PerformLayout();
            uploadTab.ResumeLayout(false);
            uploadTab.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
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
        private ListBox sugestedClubLst;
        private Button acceptSuggestionBtn;
        private ListBox searchClubLst;
        private Button acceptSearchResultBtn;
        private Button findBtn;
        private TextBox searchTxt;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button button2;
        private Label InstructionLbl;
        private Label label2;
        private RichTextBox logTxtBox;
        private Button button1;
        private CheckBox checkBox1;
        private Label label3;
        private ComboBox nationCmb;
        private TextBox textBox1;
        private TextBox internationalDescriptionTxt;
    }
}
