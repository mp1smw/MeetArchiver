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
            if(matchLst.SelectedIndex >= 0 && matchLst.SelectedIndex <= _currDiver.PossibleMatches.Count)
                suggestedDiverCtrl.EditedDiver = _currDiver.PossibleMatches[matchLst.SelectedIndex];
        }
    }
}
