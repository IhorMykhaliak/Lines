using System;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using Lines.GameEngine;
using Lines.GameEngine.Enums;

using Lines.GameEngine.BubbleGenerationStrategy;

namespace Lines.DesktopUI
{
    public partial class Lines : Form
    {

        private Game _game = new Game(8, 4);
        private int _scale = int.Parse(ConfigurationManager.AppSettings["RecomendedDesktopScale"]);

        public Lines()
        {
            InitializeComponent();

            pbxGameBoard.Width = _game.Field.Width * _scale;
            pbxGameBoard.Height = _game.Field.Height * _scale;
            lblScore.Text = "0";
            lblTurn.Text = "0";

            _game.ScoreChangedEventHandler += UpdateScore;
            _game.DrawEventHandler += DrawEvent;
            _game.GameOverEventHandler += GameOver;
            _game.TurnChangedEventHandler += NextTurn;
            _game.PathDoesntExistEventHandler += PathDoesntExist;

            _game.Start();
        }

        private void GameOver(object sender, EventArgs e)
        {
            MessageBox.Show("Game over on turn " + _game.Turn + ".Your final score is " + _game.Score.ToString());
        }

        private void Drawing(object sender, PaintEventArgs e)
        {
            int radius;
            int smallBubbleCentre;
            Graphics canvas = e.Graphics;
            for (int i = 0; i < _game.Field.Height; i++)
            {
                for (int j = 0; j < _game.Field.Width; j++)
                {
                    canvas.FillRectangle(Brushes.Silver, j * _scale + 1, i * _scale + 1, _scale - 2, _scale - 2);
                    if (_game.Field[i, j].Contain != null)
                    {
                        radius = (_game.Field[i, j].Contain == BubbleSize.Big) ? 2 : 1;
                        smallBubbleCentre = (_game.Field[i, j].Contain == BubbleSize.Small) ? _scale / 4 : 0;
                        canvas.FillEllipse(new SolidBrush(GetColor(_game.Field[i, j].Color) ?? Color.Black), _scale * j + smallBubbleCentre, _scale * i + smallBubbleCentre, _scale / 2 * radius - 1, _scale / 2 * radius - 1);
                    }
                }
            }
        }

        private void SelectCell(object sender, MouseEventArgs e)
        {
            lblPath.Text = "";
            _game.SelectCell((int)e.Y / _scale, (int)e.X / _scale);
        }

        private void DrawEvent(object sender, EventArgs e)
        {
            pbxGameBoard.Refresh();
        }

        private void UpdateScore(object sender, EventArgs e)
        {
            lblScore.Text = _game.Score.ToString();
        }

        private void PathDoesntExist(object sender, EventArgs e)
        {
            lblPath.Text = "Between two cells path doesn't exist";
        }

        private void NextTurn(object sender, EventArgs e)
        {
            lblTurn.Text = _game.Turn.ToString();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _game.Stop();
        }

        private void btnStepBack_Click(object sender, EventArgs e)
        {
            _game.CancelMove();
        }

        public Color? GetColor(BubbleColor? color)
        {
            switch (color)
            {
                case BubbleColor.Red:
                    return Color.Red;
                case BubbleColor.Green:
                    return Color.Green;
                case BubbleColor.Blue:
                    return Color.Blue;
                case BubbleColor.Yellow:
                    return Color.Yellow;
                case BubbleColor.Purple:
                    return Color.Purple;
                case BubbleColor.Pink:
                    return Color.DeepPink;

                default:
                    return null;
            }
        }
    }
}
