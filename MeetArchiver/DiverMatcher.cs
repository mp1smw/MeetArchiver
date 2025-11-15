using DR_APIs.Models;
using MeetArchiver.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MeetArchiver
{
    public partial class DiverMatcher : Form
    {
        public DiverMatcher()
        {
            InitializeComponent();
        }

        public DiverMatcher(Diver diverIn)
        {
            InitializeComponent();
            _currDiver = diverIn;
        }


        DiverCtrl currDiverCtrl;
        DiverCtrl suggestedDiverCtrl;

        private Diver _currDiver;
        public Diver CurrDiver
        {
            get { return _currDiver; }
            set
            {
                _currDiver = value;
                setupDiverControls();
            }
        }

        private void setupDiverControls()
        {
            currDiverCtrl.EditedDiver = CurrDiver;
            foreach (var diver in _currDiver.PossibleMatches)
            {
                matchLst.Items.Add($"{diver.FullName}       ({diver.Born})");
            }
            if (matchLst.Items.Count > 0)
                matchLst.SelectedIndex = 0;
        }


        private void DiverMatcher_Load(object sender, EventArgs e)
        {
            currDiverCtrl = new DiverCtrl(_currDiver);
            this.Controls.Add(currDiverCtrl);
            currDiverCtrl.Location = new Point(15, 35);
            suggestedDiverCtrl = new DiverCtrl();
            this.Controls.Add(suggestedDiverCtrl);
            suggestedDiverCtrl.Location = new Point(610, 35);
            setupDiverControls();
        }

        private void matchLst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (matchLst.SelectedIndex >= 0 && matchLst.SelectedIndex <= _currDiver.PossibleMatches.Count)
                suggestedDiverCtrl.EditedDiver = _currDiver.PossibleMatches[matchLst.SelectedIndex];
        }

        private void takeMatchBtn_Click(object sender, EventArgs e)
        {
            _currDiver.FirstName = suggestedDiverCtrl.EditedDiver.FirstName;
            _currDiver.LastName = suggestedDiverCtrl.EditedDiver.LastName;
            _currDiver.Born = suggestedDiverCtrl.EditedDiver.Born;
            _currDiver.Representing = suggestedDiverCtrl.EditedDiver.Representing;
            _currDiver.TCode = suggestedDiverCtrl.EditedDiver.TCode;
            _currDiver.ArchiveID = suggestedDiverCtrl.EditedDiver.ArchiveID;
            _currDiver.RecordStatus = RecordStatus.Valid;
            this.Close();
        }

        private void overwriteDiverBtn_Click(object sender, EventArgs e)
        {
            // this is the server PK for the diver so we can overwrite their copy with ours.
            _currDiver.ArchiveID = suggestedDiverCtrl.EditedDiver.ArchiveID;
            _currDiver.RecordStatus = RecordStatus.Updated;
            this.Close();
        }

        private void createDiverBtn_Click(object sender, EventArgs e)
        {
            _currDiver.RecordStatus = RecordStatus.New;
            this.Close();
        }
    }
}
