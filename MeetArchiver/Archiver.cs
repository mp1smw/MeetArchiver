using DR_APIs.Models;
using Microsoft.VisualBasic.FileIO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace MeetArchiver
{
    public partial class Archiver : Form
    {

        List<Meet> meets = new List<Meet>();
        List<Event> events = new List<Event>();
        List<Diver> divers = new List<Diver>();
        List<DiveSheet> diveSheets = new List<DiveSheet>();

        Meet selectedMeet;
        List<Event> selectedEvents = new List<Event>();
        List<Diver> selectedDivers = new List<Diver>();
        List<Diver> checkedDivers = new List<Diver>();
        List<DiveSheet> selectedDiveSheets = new List<DiveSheet>();

        List<Diver> mismatchedDivers = new List<Diver>();
        List<Diver> newDivers = new List<Diver>();
        List<Diver> validatedDivers = new List<Diver>();

        List<Club> checkedClubs = new List<Club>();
        List<Club> mismatchedClubs = new List<Club>();


        public Archiver()
        {
            InitializeComponent();
        }

        private void loadDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dataFolder = SpecialDirectories.AllUsersApplicationData + @"\..\..\..\MDT\DiveRecorder\Archive";

            var meetFile = Path.Combine(dataFolder, "MeetsTable");

            if (!File.Exists(meetFile))
            {
                MessageBox.Show($"Meet file not found: {meetFile}");
                return;
            }

            // check the file times and suggest they might not be up to date if older than 1 day
            FileInfo f = new FileInfo(meetFile);
            if (f.LastWriteTime < DateTime.Now.AddDays(-1))
            {
                var result = MessageBox.Show("The DR export is older than 1 day, make sure you are archiving the latest copy of your data. Are you sure you want to continue?", "Data File Age Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    this.Close();
                    return;
                }
            }

            try
            {
                meets = Meet.ParseMeets(meetFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error parsing meets: {ex.Message}");
            }

            var eventFile = Path.Combine(dataFolder, "EventsTable");

            if (!File.Exists(eventFile))
            {
                MessageBox.Show($"Event file not found: {eventFile}");
                return;
            }

            try
            {
                events = Event.ParseEvents(eventFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error parsing events: {ex.Message}");
            }

            var diverFile = Path.Combine(dataFolder, "DiversTable");

            if (!File.Exists(diverFile))
            {
                MessageBox.Show($"Event file not found: {diverFile}");
                return;
            }

            try
            {
                divers = Diver.ParseDivers(diverFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error parsing diver: {ex.Message}");
            }

            var diveSheetFile = Path.Combine(dataFolder, "DiveSheetsTable");

            if (!File.Exists(diveSheetFile))
            {
                MessageBox.Show($"Event file not found: {diveSheetFile}");
                return;
            }

            try
            {
                diveSheets = DiveSheet.ParseDiveSheets(diveSheetFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error parsing dive sheets: {ex.Message}");
            }

            // add meets to listbox
            meetsList.Items.Clear();
            foreach (var meet in meets)
            {
                meetsList.Items.Add($"{meet.Title}       ({meet.StartDate} to {meet.EndDate})");
            }

            InstructionLbl.Text = "Step 1: Select meet for archiving";


        }

        private void meetsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (meetsList.SelectedIndex == -1)
                return;

            // find diver with ID 0 and delete. This is only in here cos you can't have null in the DB and dive sheets
            // require a DiverB so this is a dummy entry 
            divers.RemoveAll(dv => dv.ID == 0);

            tabControl1.SelectedTab = diversTab;

            selectedMeet = meets[meetsList.SelectedIndex];
            selectedEvents = events.Where(ev => ev.MeetRef == selectedMeet.MRef).ToList();
            selectedDiveSheets = diveSheets.Where(ds => selectedEvents.Any(ev => ev.ERef == ds.Event)).ToList();
            selectedDivers = divers.Where(dv => selectedDiveSheets.Any(ds => ds.DiverA == dv.ID || ds.DiverB == dv.ID)).ToList();


            var missingDivers = Diver.MissingDivers(divers, selectedDivers);
            missingList.Items.Clear();
            foreach (var diver in missingDivers)
            {
                missingList.Items.Add($"{diver.FullName}       ({diver.Born})");
            }
            withdrawnLbl.Text = $"Withdrawn Divers ({missingList.Items.Count})";

            WorkingForm.Show("Validating Divers... Please wait, this can take some time.");
            var t = Diver.CheckDiversAsync(selectedDivers);
            t.Wait();
            checkedDivers = t.Result;
            WorkingForm.Close();

            RebuildDiverLists(checkedDivers);
        }

        private void RebuildDiverLists(List<Diver> chekkedDivers)
        {
            validatedDivers = chekkedDivers.Where(ds => ds.RecordStatus == RecordStatus.Valid || ds.RecordStatus == RecordStatus.Updated).ToList();
            mismatchedDivers = chekkedDivers.Where(ds => ds.RecordStatus == RecordStatus.PossibleMatches).ToList();
            newDivers = chekkedDivers.Where(ds => ds.RecordStatus == RecordStatus.New).ToList();

            matchedList.Items.Clear();
            foreach (var diver in validatedDivers)
            {
                matchedList.Items.Add($"{diver.FullName}       [{diver.Born}]");
            }
            matchedLbl.Text = $"Matched Divers ({matchedList.Items.Count})";

            typoList.Items.Clear();
            foreach (var diver in mismatchedDivers)
            {
                typoList.Items.Add($"{diver.FullName}       [{diver.Born}]");
            }
            mismatchLbl.Text = $"Possible Mismatches ({typoList.Items.Count})";
            if (mismatchedDivers.Count > 0)
            {
                typoList.BackColor = Color.Orange;
            }

            newList.Items.Clear();
            foreach (var diver in newDivers)
            {
                newList.Items.Add($"{diver.FullName}       [{diver.Born}]");
            }
            newLbl.Text = $"New Divers ({newList.Items.Count})";

            if (mismatchedDivers.Count == 0)
            {
                typoList.BackColor = SystemColors.Window;
                var result = MessageBox.Show("All Mismatched divers are now resolved, would you like to move onto fixing Club errors", "Diver cleansing complete", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    tabControl1.SelectedTab = clubsTab;
                }
            }

        }

        private void typoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (typoList.SelectedIndex == -1)
                return;

            var frm = new DiverMatcher(mismatchedDivers[typoList.SelectedIndex]);
            frm.ShowDialog();
            RebuildDiverLists(checkedDivers);
        }

        private void CheckClubData(object sender, EventArgs e)
        {
            // find distinct clubs from local dataset
            List<Club> distinctClubs = new List<Club>();
            var clubs = selectedDivers.Select(dv => new Club { Representing = dv.Representing, TCode = dv.TCode }).ToList();
            foreach (var club in clubs)
            {
                var exists = distinctClubs.Any(c => c.Representing == club.Representing && c.TCode == club.TCode);
                if (!exists)
                {
                    distinctClubs.Add(club);
                }
            }

            var t = Club.CheckClubsAsync(distinctClubs);
            t.Wait();
            checkedClubs = t.Result;

            mismatchedClubs = checkedClubs.Where(c => !c.Validated).ToList();

            sugestedClubLst.Items.Clear();
            searchClubLst.Items.Clear();

            mismatchedClubsLst.Items.Clear();
            foreach (var club in mismatchedClubs)
            {
                mismatchedClubsLst.Items.Add($"{club.Representing}       [{club.TCode}]");
            }
            mismatchedClubsLst.Tag = mismatchedClubs;
            if (mismatchedClubs.Count == 0)
            {
                mismatchedClubsLst.BackColor = Color.White;
                var result = MessageBox.Show("All club mismatches are now resolved, would you like to move onto uploading the meet", "Club cleansing complete", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    tabControl1.SelectedTab = uploadTab;
                }
            }
            else
            {
                mismatchedClubsLst.BackColor = Color.Orange;
            }
        }

        private void mismatchedClubsLst_SelectedIndexChanged(object sender, EventArgs e)
        {
            // A quick search by club code is all we can do here
            var t = Club.FindClubAsync(mismatchedClubs[mismatchedClubsLst.SelectedIndex].TCode);
            t.Wait();
            var suggestedClubs = t.Result;
            sugestedClubLst.Items.Clear();
            if (suggestedClubs.Count != 0)
            {
                sugestedClubLst.Tag = suggestedClubs;
                foreach (var club in suggestedClubs)
                {
                    sugestedClubLst.Items.Add($"{club.Representing}       [{club.TCode}]");
                }
            }
        }

        private void findBtn_Click(object sender, EventArgs e)
        {
            var t = Club.FindClubAsync(searchTxt.Text);
            t.Wait();
            var suggestedClubs = t.Result;
            searchClubLst.Items.Clear();
            if (suggestedClubs.Count != 0)
            {
                searchClubLst.Tag = suggestedClubs;
                foreach (var club in suggestedClubs)
                {
                    searchClubLst.Items.Add($"{club.Representing}       [{club.TCode}]");
                }
            }
        }

        private void acceptSuggestionBtn_Click(object sender, EventArgs e)
        {
            if (sugestedClubLst.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a suggested club first.");
                return;
            }
            var sugestedClubSelection = ((List<Club>)sugestedClubLst.Tag)[sugestedClubLst.SelectedIndex];
            var mismatchedClub = mismatchedClubs[mismatchedClubsLst.SelectedIndex];

            var divesrToFix = selectedDivers.Where(dv => mismatchedClub.Representing == dv.Representing && mismatchedClub.TCode == dv.TCode).ToList();
            foreach (var diver in divesrToFix)
            {
                diver.Representing = sugestedClubSelection.Representing;
                diver.TCode = sugestedClubSelection.TCode;
                // don't need to update record status here cos if a diver has been validated it can never apear
                // on the list of invalid clubs so leave it all as it is and updates/new will happes as they shold

            }
            CheckClubData(null, null);
        }

        private void acceptSearchResultBtn_Click(object sender, EventArgs e)
        {
            if (searchClubLst.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a search result first.");
                return;
            }
            var sugestedClubSelection = ((List<Club>)searchClubLst.Tag)[searchClubLst.SelectedIndex];
            var mismatchedClub = mismatchedClubs[mismatchedClubsLst.SelectedIndex];

            var divesrToFix = selectedDivers.Where(dv => mismatchedClub.Representing == dv.Representing && mismatchedClub.TCode == dv.TCode).ToList();
            foreach (var diver in divesrToFix)
            {
                diver.Representing = sugestedClubSelection.Representing;
                diver.TCode = sugestedClubSelection.TCode;
                // don't need to update record status here cos if a diver has been validated it can never apear
                // on the list of invalid clubs so leave it all as it is and updates/new will happes as they shold

            }
            CheckClubData(null, null);
        }

        private void ArchiveEventNow(object sender, EventArgs e)
        {
            logTxtBox.Clear();
            logTxtBox.AppendText("Beginning Diver Processing...\n");
            logTxtBox.AppendText($"Total Divers to process: {checkedDivers.Count} - {checkedDivers.Where(a => a.RecordStatus == RecordStatus.New).Count()} New Divers and {checkedDivers.Where(a => a.RecordStatus == RecordStatus.Updated).Count()} Updated Divers\n");
            WorkingForm.Show("Updating Divers... Please wait");
            var t = Diver.UpdateDiversAsync(checkedDivers, Program.CurrentUser);
            t.Wait();
            var d = t.Result;
            logTxtBox.AppendText("Running finlal validation on diver list.\n");
            var t2 = Diver.CheckDiversAsync(checkedDivers);
            t2.Wait();
            checkedDivers = t2.Result;
            WorkingForm.Close();
            if (checkedDivers.Where(a => a.RecordStatus == RecordStatus.New).Count() == 0
                && checkedDivers.Where(a => a.RecordStatus == RecordStatus.Updated).Count() == 0
                && checkedDivers.Where(a => a.RecordStatus == RecordStatus.PossibleMatches).Count() == 0)
            {
                logTxtBox.AppendText("All divers processed successfully with no errors.\n");
            }
            else
            {
                logTxtBox.AppendText("Some divers failed to process correctly. Please review the diver lists again.\n");
                return;
            }

            // check if meet already exists
            var t4 = Meet.GetByGuidAsync(selectedMeet.MeetGUID);
            t4.Wait();
            var existingMeet = t4.Result;
            if (existingMeet != null)
            {
                logTxtBox.AppendText($"A meet with the same GUID already exists in the central database (MRef: {existingMeet.MRef}). Aborting upload to prevent duplicates.\n");
                var result = MessageBox.Show("Meet alreay exists. Do you want to proceed with deleting the meet from the central database so you can republish it?", "Confirm Meet Delete", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    logTxtBox.AppendText("Meet upload aborted by user.\n");
                    return;
                }
                // delete meet
                try
                {
                    var t5 = Meet.DeleteByGuidAsync(existingMeet.MeetGUID, Program.CurrentUser);
                    t5.Wait();
                    int res = t5.Result;
                    logTxtBox.AppendText($"Existing meet with MRef: {existingMeet.MRef} deleted from central database.\n");
                }
                catch (Exception ex)
                {
                    logTxtBox.AppendText($"Error deleting existing meet: {ex.Message}\n");
                    return;
                }
            }

            logTxtBox.AppendText("Creating new meet.\n");
            var t3 = Meet.AddMeetAsync(selectedMeet, Program.CurrentUser);
            t3.Wait();
            var newMeetRef = t3.Result;
            logTxtBox.AppendText($"New meet created with MRef: {newMeetRef}\n");

            // now we want to update the events and divesheets with the correct MRef IDs
            foreach(Event ev in selectedEvents)
            {
                ev.MeetRef = newMeetRef;
            }

            foreach(DiveSheet ds in selectedDiveSheets)
            {
                ds.Meet = newMeetRef; // should be the same but just to be sure
            }
            logTxtBox.AppendText($"Events and DiveSheets updated with new MeetRef\n");// build lookup of ID -> ArchiveID (only those with ArchiveID)


            // replace diver IDs with server version
            logTxtBox.AppendText($"Updating Diveshets from local IDs to global ones\n");
            var idToArchive = checkedDivers
                .Where(d => d.ArchiveID.HasValue)
                .ToDictionary(d => d.ID, d => d.ArchiveID!.Value);

            // replace DiverA where a mapping exists
            foreach (var ds in diveSheets)
            {
                if (idToArchive.TryGetValue(ds.DiverA, out var archiveId))
                    ds.DiverA = archiveId;
                if (idToArchive.TryGetValue(ds.DiverB, out var archiveId2))
                    ds.DiverB = archiveId2;
            }

            logTxtBox.AppendText("All identifiers updated, begining archive process\n");


            var t6 = Event.AddEventsAsync(selectedEvents);
            t6.Wait();
            var eventRet = t6.Result;
            logTxtBox.AppendText($"Events archived succesfully\n");

            WorkingForm.Show("Adding dive results . . . this can take a while!");
            var t7 = DiveSheet.AddDiveSheetsAsync(selectedDiveSheets);
            t7.Wait();
            var diveRet = t7.Result;
            logTxtBox.AppendText($"DiveSheets archived succesfully\n");
            logTxtBox.AppendText("Meet archiving complete!\n");
            WorkingForm.Close();

        }

        private void Archiver_Load(object sender, EventArgs e)
        {

            // lets validate their key first, no pint going any further if we can't
            string apikey = "";
            string email = "";
            using var f = new AuthForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                apikey = f.EnteredPassword; /* use pw */
                email = f.EnteredEmail; /* use email */
            }
            else
            {
                this.Close();
                return;
            }

            var t = User.GetUserAsync(apikey, email);
            t.Wait();
            var user = t.Result;

            if (user.Role == null || (user.Role != "Admin" && user.Role != "DataManager"))
            {
                MessageBox.Show("You do not have sufficient privileges to use the Meet Archiver. Please contact your system administrator.");
                this.Close();
                return;
            }
            user.APIKey = apikey; // store the key for later use
            Program.CurrentUser = user; // squirel this away in Program for later use

            loadDataToolStripMenuItem_Click(this, EventArgs.Empty);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == clubsTab)
            {
                CheckClubData(sender, e);
                InstructionLbl.Text = "Step 3: Review the list of Mismatched clubs below. These clubs are in the local dataset but are similar to clubs found in the central database.";
            }

            if (tabControl1.SelectedTab == diversTab)
            {
                InstructionLbl.Text = "Step 2: Review the list of Mismatched divers below. These divers are in the local dataset but are similar to divers found in the central database.\nClick diver to edit.";
            }

            if (tabControl1.SelectedTab == uploadTab)
            {
                // Set location for nation combo box
                nationCmb.Text = Program.CountryCode;
                InstructionLbl.Text = "Step 4: Begin upload of the meet.";
            }
        }

        private void nationCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedMeet.Nation = nationCmb.Text;
        }
    }
}
