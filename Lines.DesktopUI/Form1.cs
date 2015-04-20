using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lines.GameEngine;

namespace Lines.DesktopUI
{
    public partial class Lines : Form
    {

        Game game = new Game();
        int scale = Settings.RecomededFormScale;
        
        public Lines()
        {
            InitializeComponent();

            PbGameBoard.Width = game.Field.Width * scale;
            PbGameBoard.Height = game.Field.Height * scale;

            game.UpdateScoreLabelHandler += UpdateScoreLabel;
            game.DrawFieldHandler += DrawEvent;

            //game.Field.Cells[1, 5].Contain = BubbleSize.Big;
            //game.Field.Cells[1, 5].Color = Color.Black;

            // For auto fill Net
            //for (int i = 0; i < game.Field.Height; i++)
            //{
            //    for (int j = 0; j < game.Field.Width - 1; j++)
            //    {
            //        game.Field.Cells[i, j].Contain = BubbleSize.Big;
            //        game.Field.Cells[i, j].Color = BubbleColor.Red;
            //    }
            //}
            game.Field.Cells[1, 1].Contain = BubbleSize.Big;
            game.Field.Cells[1, 1].Color = BubbleColor.Red;
            game.Field.Cells[1, 2].Contain = BubbleSize.Big;
            game.Field.Cells[1, 2].Color = BubbleColor.Red;
            game.Field.Cells[1, 3].Contain = BubbleSize.Big;
            game.Field.Cells[1, 3].Color = BubbleColor.Red;
            game.Field.Cells[1, 8].Contain = BubbleSize.Big;
            game.Field.Cells[1, 8].Color = BubbleColor.Red;
            game.Field.Cells[1, 4].Contain = BubbleSize.Small;
            game.Field.Cells[1, 4].Color = BubbleColor.Blue;
            game.Field.Cells[1, 5].Contain = BubbleSize.Big;
            game.Field.Cells[1, 5].Color = BubbleColor.Red;

            //game.Field.Cells[5, 1].Contain = BubbleSize.Big;
            //game.Field.Cells[5, 1].Color = BubbleColor.Red;
            //game.Field.Cells[4, 2].Contain = BubbleSize.Big;
            //game.Field.Cells[4, 2].Color = BubbleColor.Red;
            //game.Field.Cells[3, 3].Contain = BubbleSize.Big;
            //game.Field.Cells[3, 3].Color = BubbleColor.Red;
            //game.Field.Cells[2, 4].Contain = BubbleSize.Big;
            //game.Field.Cells[2, 4].Color = BubbleColor.Red;
            //game.Field.Cells[1, 5].Contain = BubbleSize.Big;
            //game.Field.Cells[1, 5].Color = BubbleColor.Red;

            //game.Start();
        }


        private void Drawing(object sender, PaintEventArgs e)
        {
            DrawForm(sender, e);
        }

        private void SelectedCell(object sender, MouseEventArgs e)
        {
            game.SelectCell((int)e.Y / scale, (int)e.X / scale);
        }

        private void DrawEvent()
        {
            PbGameBoard.Refresh();
        }

        private void DrawForm(object sender, PaintEventArgs e)
        {

            int radius;
            int smallBubbleCentre;
            Graphics canvas = e.Graphics;
            for (int i = 0; i < game.Field.Height; i++)
            {
                for (int j = 0; j < game.Field.Width; j++)
                {
                    canvas.FillRectangle(Brushes.Silver, j * scale, i * scale, scale - 2, scale - 2);
                    if (game.Field.Cells[i, j].Contain != null)
                    {
                        radius = (game.Field.Cells[i, j].Contain == BubbleSize.Big) ? 2 : 1;
                        smallBubbleCentre = (game.Field.Cells[i, j].Contain == BubbleSize.Small) ? scale / 4 : 0;
                        canvas.FillEllipse(new SolidBrush(GetColor(game.Field.Cells[i, j].Color) ?? Color.Black), scale * j + smallBubbleCentre - 2, scale * i + smallBubbleCentre - 2, scale / 2 * radius, scale / 2 * radius);
                    }
                }
            }

            lbTurn.Text = game.Turn.ToString();
            richTextBox1.Text = Settings.Messege;
        }

        private void UpdateScoreLabel()
        {
            lbScore.Text = Settings.Score.ToString();
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
;

                default:
                    return null;
            }

        }

       

    }
}
