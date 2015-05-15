﻿using System;
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
        #region Fields

        private Game _game = new Game(8, 4);
        private Sound _sound = new Sound();
        private int _scale = int.Parse(ConfigurationManager.AppSettings["RecomendedDesktopScale"]);

        #endregion

        #region Constructor

        public Lines()
        {
            InitializeComponent();

            pbxGameBoard.Width = _game.Field.Width * _scale;
            pbxGameBoard.Height = _game.Field.Height * _scale;
            this.Height = pbxGameBoard.Height + 220;
            this.Width = pbxGameBoard.Width + 50;
            lblAllowedUndos.BackColor = Color.Transparent;

            SubscribeGameEvents();

            _game.Start();
        }

        #endregion

        #region Methods

        private void GameOver(object sender, EventArgs e)
        {
            MessageBox.Show("Game over on turn " + _game.Turn + ".Your final score is " + _game.Score.ToString());
        }

        private void pbxGameBoard_Paint(object sender, PaintEventArgs e)
        {
            int radius;
            int smallBubbleCentre;
            Graphics canvas = e.Graphics;
            canvas.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            for (int i = 0; i < _game.Field.Height; i++)
            {
                for (int j = 0; j < _game.Field.Width; j++)
                {
                    canvas.FillRectangle(Brushes.Silver, j * _scale + 1, i * _scale + 1, _scale - 2, _scale - 2);
                    if (_game.Field[i, j].ContainedItem != null)
                    {
                        radius = (_game.Field[i, j].ContainedItem == BubbleSize.Big) ? 2 : 1;
                        smallBubbleCentre = (_game.Field[i, j].ContainedItem == BubbleSize.Small) ? _scale / 4 : 0;
                        canvas.FillEllipse(new SolidBrush(GetColor(_game.Field[i, j].Color) ?? Color.Black),
                                            _scale * j + smallBubbleCentre,
                                            _scale * i + smallBubbleCentre,
                                            _scale / 2 * radius - 1,
                                            _scale / 2 * radius - 1);
                    }
                }
            }
        }

        private void pbxGameBoard_MouseClick(object sender, MouseEventArgs e)
        {
            lblPath.Text = "";
            _game.SelectCell((int)e.Y / _scale, (int)e.X / _scale);
        }

        private void Draw(object sender, EventArgs e)
        {
            pbxGameBoard.Refresh();
            lblAllowedUndos.Text = "Allowed : " + _game.AllowedStepsBack.ToString();
        }

        private void UpdateScore(object sender, EventArgs e)
        {
            lblScore.Text = "Score : " + _game.Score.ToString();
        }

        private void PathDoesntExist(object sender, EventArgs e)
        {
            lblPath.Text = "Between two cells path doesn't exist";
        }

        private void NextTurn(object sender, EventArgs e)
        {
            lblTurn.Text = "Turn : " + _game.Turn.ToString();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _game.Stop();
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            _game.Undo();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            _game.ReStart();
        }
        
        private void pbxSound_Click(object sender, EventArgs e)
        {
            _sound.IsSoundOn = !_sound.IsSoundOn;
            pbxSound.Image = (_sound.IsSoundOn) ? Properties.Resources.sound : Properties.Resources.no_sound;
        }
        #endregion

        #region Helpers

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

        private void SubscribeGameEvents()
        {
            _game.ScoreChangedEventHandler += UpdateScore;
            _game.DrawEventHandler += Draw;
            _game.GameOverEventHandler += GameOver;
            _game.TurnChangedEventHandler += NextTurn;
            _game.PathDoesntExistEventHandler += PathDoesntExist;
            _game.PlayMoveSoundEventHandler += _sound.PlayMoveSound;
            _game.PlayScoreSoundEventHandler += _sound.PlayScoreSound;
            _game.PlayCancelSoundEventHandler += _sound.PlayCancelSound;
            _game.PathDoesntExistEventHandler += _sound.PathNotExistSound;
        }

        #endregion
    }
}
