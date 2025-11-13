using DR_APIs.Models;
using Microsoft.VisualBasic.FileIO;
using System.Security.Cryptography;

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
        List<DiveSheet> selectedDiveSheets = new List<DiveSheet>();

        List<Diver> mismatchedDivers = new List<Diver>();
        List<Diver> newDivers = new List<Diver>();
        List<Diver> validatedDivers = new List<Diver>();

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
            var chekkedDivers = t.Result;
            WorkingForm.Close();

             validatedDivers = chekkedDivers.Where(ds => ds.Validated).ToList();
             mismatchedDivers = chekkedDivers.Where(ds => ds.PossibleMatches != null && ds.PossibleMatches.Count() != 0).ToList();
             newDivers = chekkedDivers.Except(validatedDivers).Except(mismatchedDivers).ToList();

            matchedList.Items.Clear();
            foreach (var diver in validatedDivers)
            {
                matchedList.Items.Add($"{diver.FullName}       ({diver.Born})");
            }
            matchedLbl.Text = $"Matched Divers ({matchedList.Items.Count})";

            typoList.Items.Clear();
            foreach (var diver in mismatchedDivers)
            {
                typoList.Items.Add($"{diver.FullName}       ({diver.Born})");
            }
            mismatchLbl.Text = $"Possible Mismatches ({typoList.Items.Count})";

            newList.Items.Clear();
            foreach (var diver in newDivers)
            {
                newList.Items.Add($"{diver.FullName}       ({diver.Born})");
            }
            newLbl.Text = $"New Divers ({newList.Items.Count})";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = new DiverMatcher();
            frm.ShowDialog();
        }

        private void typoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var frm = new DiverMatcher(mismatchedDivers[typoList.SelectedIndex]);
            frm.ShowDialog();
        }
    }
}
