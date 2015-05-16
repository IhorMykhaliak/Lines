using System;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using Lines.GameEngine;
using Lines.GameEngine.Enums;

using Lines.GameEngine.BubbleGenerationStrategy;
using System.Drawing.Drawing2D;

namespace Lines.DesktopUI
{
    public partial class Lines : Form
    {
        #region Const fields

        // Used for drawing bubbles
        private const int BUBBLE_MARGIN = 5;
        private const int BUBBLE_SIZE_DECREASE = 11;
        private const int NORMALIZE_BUBBLE_CENTRE = 4;

        private const int ELLIPSE_MARGIN = BUBBLE_MARGIN - 2;
        private const int ELLIPSE_SIZE_DECREASE = BUBBLE_SIZE_DECREASE - 4;
        private const int ELLIPSE_THICKNESS = 3;
        #endregion

        #region Fields

        private Game _game;
        private Sound _sound = new Sound();
        private int _scale = int.Parse(ConfigurationManager.AppSettings["RecomendedDesktopScale"]);

        #endregion

        #region Constructor

        public Lines()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        public void InitializeGame()
        {
            int size = int.Parse(ConfigurationManager.AppSettings["FieldSize"]);
            int diff = int.Parse(ConfigurationManager.AppSettings["Difficulty"]);
            _game = new Game(size, diff);
            pbxGameBoard.Width = _game.Field.Width * _scale;
            pbxGameBoard.Height = _game.Field.Height * _scale;
            this.Height = pbxGameBoard.Height + 220;
            this.Width = pbxGameBoard.Width + 50;

            SubscribeGameEvents();

            _game.Start();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Game Events

        private void GameOver(object sender, EventArgs e)
        {
            MessageBox.Show("Game over on turn " + _game.Turn + ".Your final score is " + _game.Score.ToString());
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

        private void btnRestartGame_Click(object sender, EventArgs e)
        {
            _game.ReStart();
        }
        
        private void pbxSound_Click(object sender, EventArgs e)
        {
            _sound.IsSoundOn = !_sound.IsSoundOn;
            pbxSound.Image = (_sound.IsSoundOn) ? Properties.Resources.sound : Properties.Resources.no_sound;
        }

        #endregion

        #region Interaction with field

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
                        smallBubbleCentre = (_game.Field[i, j].ContainedItem == BubbleSize.Small) ? _scale / NORMALIZE_BUBBLE_CENTRE : 0;
                        canvas.FillEllipse(CreateBrush(_game.Field[i, j].Color),
                                            _scale * j + smallBubbleCentre + BUBBLE_MARGIN,
                                            _scale * i + smallBubbleCentre + BUBBLE_MARGIN,
                                            _scale / 2 * radius - BUBBLE_SIZE_DECREASE,
                                            _scale / 2 * radius - BUBBLE_SIZE_DECREASE);
                    }
                }
            }
            if (_game.SelectedCell != null)
            {
                canvas.DrawEllipse(new Pen(Color.White, ELLIPSE_THICKNESS),
                                        _scale * _game.SelectedCell.Column + ELLIPSE_MARGIN,
                                        _scale * _game.SelectedCell.Row + ELLIPSE_MARGIN,
                                        _scale - ELLIPSE_SIZE_DECREASE,
                                        _scale - ELLIPSE_SIZE_DECREASE);
            }
        }
        
        private void pbxGameBoard_MouseClick(object sender, MouseEventArgs e)
        {
            lblPath.Text = "";
            _game.SelectCell((int)e.Y / _scale, (int)e.X / _scale);
        }
        
        #endregion

        #endregion

        #region Helpers

        private Brush CreateBrush(BubbleColor? color)
        {
            LinearGradientBrush linGrBrush = new LinearGradientBrush(
            new Point(0, 10),
            new Point(_scale, 10),
            GetColor(color),
            Color.Black);

            return linGrBrush;
        }

        public Color GetColor(BubbleColor? color)
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
                    throw new InvalidOperationException("Can't find appropriate equivalent");
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
