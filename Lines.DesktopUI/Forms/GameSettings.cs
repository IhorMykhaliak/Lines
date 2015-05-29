﻿using System;
using System.Configuration;
using System.Windows.Forms;
using Lines.DesktopUI.Properties;

namespace Lines.DesktopUI
{
    public partial class GameSettings : Form
    {
        public GameSettings()
        {
            InitializeComponent();

            SetPreviousFieldSize();

            SetPreviousDifficulty();
        }
        
        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            Settings.Default.FieldSize = GetFieldSize();
            Settings.Default.Difficulty = GetGameDifficulty();
            
            this.Close();
        }

        #region Helpers

        private void SetPreviousFieldSize()
        {
            int size = Settings.Default.FieldSize;
            if (size == int.Parse(rbtnSmallSize.Text))
            {
                rbtnSmallSize.Checked = true;
            }
            else if (size == int.Parse(rbtnMediumSize.Text))
            {
                rbtnMediumSize.Checked = true;
            }
            else if (size == int.Parse(rbtnLargeSize.Text))
            {
                rbtnLargeSize.Checked = true;
            }
            else if (size == int.Parse(rbtnExtraLargeSize.Text))
            {
                rbtnExtraLargeSize.Checked = true;
            }
            else
            {
                throw new InvalidOperationException("Allowed game size was changed");
            }
        }

        private void SetPreviousDifficulty()
        {
            int diff = Settings.Default.Difficulty;
            if (diff == int.Parse(rbtnEasy.Text))
            {
                rbtnEasy.Checked = true;
            }
            else if (diff == int.Parse(rbtnMedium.Text))
            {
                rbtnMedium.Checked = true;
            }
            else if (diff == int.Parse(rbtnHard.Text))
            {
                rbtnHard.Checked = true;
            }
            else
            {
                throw new InvalidOperationException("Allowed game difficulty was changed");
            }
        }

        private int GetGameDifficulty()
        {
            int diff;

            if (rbtnEasy.Checked)
            {
                diff = int.Parse(rbtnEasy.Text);
            }
            else if (rbtnMedium.Checked)
            {
                diff = int.Parse(rbtnMedium.Text);
            }
            else if (rbtnHard.Checked)
            {
                diff = int.Parse(rbtnHard.Text);
            }
            else
            {
                throw new InvalidOperationException("Difficulty wasn't set");
            }

            return diff;
        }

        private int GetFieldSize()
        {
            int size;

            if (rbtnSmallSize.Checked)
            {
                size = int.Parse(rbtnSmallSize.Text);
            }
            else if (rbtnMediumSize.Checked)
            {
                size = int.Parse(rbtnMediumSize.Text);
            }
            else if (rbtnLargeSize.Checked)
            {
                size = int.Parse(rbtnLargeSize.Text);
            }
            else if (rbtnExtraLargeSize.Checked)
            {
                size = int.Parse(rbtnExtraLargeSize.Text);
            }
            else
            {
                throw new InvalidOperationException("Size wasn't set");
            }

            return size;
        }

        #endregion

    }
}