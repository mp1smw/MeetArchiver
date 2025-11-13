using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DR_APIs.Models;
using System.ComponentModel;

namespace MeetArchiver.Controls
{
    public partial class DiverCtrl : UserControl
    {
        private Diver _editedDiver = new Diver();
        // Public edited instance (a clone of the input to avoid mutating the original)
        public Diver EditedDiver
        { 
            get { return _editedDiver; }
            set
            {
                _editedDiver = value;
                PopulateControlsFromDiver();
            }
        }

        /// <summary>
        /// Create the control and optionally load values from the provided Diver.
        /// The provided Diver is cloned so the original is not modified by the control.
        /// </summary>
        public DiverCtrl(Diver? diver = null)
        {
            InitializeComponent();

            EditedDiver = diver;
            PopulateControlsFromDiver();
        }

        private void PopulateControlsFromDiver()
        {
            if (EditedDiver == null) EditedDiver = new Diver();

            txtId.Text = EditedDiver.ID.ToString();
            txtArchiveId.Text = EditedDiver.ArchiveID?.ToString() ?? string.Empty;
            txtFirstName.Text = EditedDiver.FirstName ?? string.Empty;
            txtLastName.Text = EditedDiver.LastName ?? string.Empty;
            txtRepresenting.Text = EditedDiver.Representing ?? string.Empty;
            cmbSex.SelectedItem = string.IsNullOrEmpty(EditedDiver.Sex) ? string.Empty : EditedDiver.Sex;
            nudBorn.Value = EditedDiver.Born ?? 0;
            txtCoach.Text = EditedDiver.Coach ?? string.Empty;
            txtTCode.Text = EditedDiver.TCode ?? string.Empty;
            txtNation.Text = EditedDiver.Nation ?? string.Empty;
        }

        private static string? NormalizeEmpty(string? s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim();

    }
}