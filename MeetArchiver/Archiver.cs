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
            var t = Diver.CheckAthletesAsync(selectedDivers);
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

            newList.Items.Clear();
            foreach (var diver in newDivers)
            {
                newList.Items.Add($"{diver.FullName}       [{diver.Born}]");
            }
            newLbl.Text = $"New Divers ({newList.Items.Count})";
        }

        private void typoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (typoList.SelectedIndex == -1)
                return;

            var frm = new DiverMatcher(mismatchedDivers[typoList.SelectedIndex]);
            frm.ShowDialog();
            RebuildDiverLists(checkedDivers);
        }

        private void button1_Click(object sender, EventArgs e)
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

            mismatchedClubsLst.Items.Clear();
            foreach (var club in mismatchedClubs)
            {
                mismatchedClubsLst.Items.Add($"{club.Representing}       [{club.TCode}]");
            }
            mismatchedClubsLst.Tag = mismatchedClubs;
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
            if(sugestedClubLst.SelectedIndex == -1)
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
        }
    }
}
