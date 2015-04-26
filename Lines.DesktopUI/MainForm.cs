using System;
using System.Drawing;
using System.Windows.Forms;
using Lines.GameEngine;
using Lines.GameEngine.Enums;

using Lines.GameEngine.BubbleGenerationStrategy;

namespace Lines.DesktopUI
{
    public partial class Lines : Form
    {

        Game game = new Game();
        int scale = 50; // read from app congig
        
        public Lines()
        {
            InitializeComponent();

            PbGameBoard.Width = game.Field.Width * scale;
            PbGameBoard.Height = game.Field.Height * scale;

            game.UpdateScoreHandler += UpdateScore;
            game.DrawFieldHandler += DrawEvent;
            game.GameOverHandler += GameOver;
            game.NextTurnHandler += NextTurn;

            #region Fill 9x10

            //game.field.Cells[1, 5].Contain = BubbleSize.Big;
            //game.field.Cells[1, 5].Color = Color.Black;

            // For auto fill Net
            //for (int i = 0; i < game.field.Height; i++)
            //{
            //    for (int j = 0; j < game.field.Width - 1; j++)
            //    {
            //        game.field.Cells[i, j].Contain = BubbleSize.Big;
            //        game.field.Cells[i, j].Color = BubbleColor.Red;
            //    }
            //}

            #endregion

            #region Fill with double line
            //game.field.Cells[1, 1].Contain = BubbleSize.Big;
            //game.field.Cells[1, 1].Color = BubbleColor.Red;
            //game.field.Cells[1, 2].Contain = BubbleSize.Big;
            //game.field.Cells[1, 2].Color = BubbleColor.Red;
            //game.field.Cells[1, 3].Contain = BubbleSize.Big;
            //game.field.Cells[1, 3].Color = BubbleColor.Red;
            //game.field.Cells[1, 8].Contain = BubbleSize.Big;
            //game.field.Cells[1, 8].Color = BubbleColor.Red;
            //game.field.Cells[1, 4].Contain = BubbleSize.Big;
            //game.field.Cells[1, 4].Color = BubbleColor.Red;

            //game.field.Cells[5, 1].Contain = BubbleSize.Big;
            //game.field.Cells[5, 1].Color = BubbleColor.Red;
            //game.field.Cells[4, 2].Contain = BubbleSize.Big;
            //game.field.Cells[4, 2].Color = BubbleColor.Red;
            //game.field.Cells[3, 3].Contain = BubbleSize.Big;
            //game.field.Cells[3, 3].Color = BubbleColor.Red;
            //game.field.Cells[2, 4].Contain = BubbleSize.Big;
            //game.field.Cells[2, 4].Color = BubbleColor.Red;
            #endregion

            #region fill with line and small bubble
            //game.field.Cells[1, 1].Contain = BubbleSize.Big;
            //game.field.Cells[1, 1].Color = BubbleColor.Red;
            //game.field.Cells[1, 2].Contain = BubbleSize.Big;
            //game.field.Cells[1, 2].Color = BubbleColor.Red;
            //game.field.Cells[1, 3].Contain = BubbleSize.Big;
            //game.field.Cells[1, 3].Color = BubbleColor.Red;
            //game.field.Cells[1, 8].Contain = BubbleSize.Big;
            //game.field.Cells[1, 8].Color = BubbleColor.Red;
            //game.field.Cells[1, 4].Contain = BubbleSize.Small;
            //game.field.Cells[1, 4].Color = BubbleColor.Blue;
            //game.field.Cells[1, 5].Contain = BubbleSize.Big;
            //game.field.Cells[1, 5].Color = BubbleColor.Red;
            #endregion


            game.Start();
        }

        private void GameOver(object sender, EventArgs e)
        {
            MessageBox.Show("Game over on turn " + game.Turn + ".Your result = " + game.Score.ToString());
        }

        private void Drawing(object sender, PaintEventArgs e)
        {
            int radius;
            int smallBubbleCentre;
            Graphics canvas = e.Graphics;
            for (int i = 0; i < game.Field.Height; i++)
            {
                for (int j = 0; j < game.Field.Width; j++)
                {
                    canvas.FillRectangle(Brushes.Silver, j * scale + 1, i * scale + 1, scale - 2, scale - 2);
                    if (game.Field.Cells[i, j].Contain != null)
                    {
                        radius = (game.Field.Cells[i, j].Contain == BubbleSize.Big) ? 2 : 1;
                        smallBubbleCentre = (game.Field.Cells[i, j].Contain == BubbleSize.Small) ? scale / 4 : 0;
                        canvas.FillEllipse(new SolidBrush(GetColor(game.Field.Cells[i, j].Color) ?? Color.Black), scale * j + smallBubbleCentre, scale * i + smallBubbleCentre, scale / 2 * radius - 1, scale / 2 * radius - 1);
                    }
                }
            }
        }

        private void SelectCell(object sender, MouseEventArgs e)
        {
            game.SelectCell((int)e.Y / scale, (int)e.X / scale);
        }

        private void DrawEvent(object sender, EventArgs e)
        {
            PbGameBoard.Refresh();
        }

        private void UpdateScore(object sender, EventArgs e)
        {
            lbScore.Text = game.Score.ToString();
        }

        private void NextTurn(object sender, EventArgs e)
        {
            lbTurn.Text = game.Turn.ToString();
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

        private void btnStop_Click(object sender, EventArgs e)
        {
            game.Stop();

            btnStop.Enabled = false;
        }
    }
}
