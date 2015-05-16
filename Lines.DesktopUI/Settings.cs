using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lines.DesktopUI
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();

            SetPreviousFieldSize();

            SetPreviousDifficulty();
        }
        
        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings["FieldSize"].Value = GetFieldSize().ToString();
            config.AppSettings.Settings["Difficulty"].Value = GetGameDifficulty().ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            this.Close();
        }

        #region Helpers

        private void SetPreviousFieldSize()
        {
            int size = int.Parse(ConfigurationManager.AppSettings["FieldSize"]);
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
                throw new InvalidOperationException("Allowed game difficulty was changed");
            }
        }

        private void SetPreviousDifficulty()
        {
            int diff = int.Parse(ConfigurationManager.AppSettings["Difficulty"]);
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
            int diff = 3;

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

            return diff;
        }

        private int GetFieldSize()
        {
            int size = 7;

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

            return size;
        }

        #endregion

    }
}
