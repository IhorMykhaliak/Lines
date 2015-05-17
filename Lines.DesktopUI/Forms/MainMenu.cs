using System;
using System.Windows.Forms;

namespace Lines.DesktopUI
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            Lines gameWindow = new Lines();
            gameWindow.InitializeGame();
            gameWindow.FormClosing += (s, args) => { this.Show(); };
            gameWindow.Show();
            this.Hide();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.FormClosing += (s, args) => { this.Show(); };
            settings.Show();
            this.Hide();
        }

        private void btnRules_Click(object sender, EventArgs e)
        {
            AboutGame about = new AboutGame();
            about.FormClosing += (s, args) => { this.Show(); };
            about.Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
