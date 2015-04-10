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

           
            //game.Field.Cells[1, 5].Contain = ContainedItem.Big;
            //game.Field.Cells[1, 5].Color = Color.Black;

             //For auto fill Net
            //for (int i = 0; i < game.Field.Height; i++)
            //{
            //    for (int j = 0; j < game.Field.Width - 1; j++)
            //    {
            //        game.Field.Cells[i, j].Contain = ContainedItem.Big;
            //        game.Field.Cells[i, j].Color = BubbleColor.Red;
            //    }
            //}

            game.Start();
        }


        private void Drawing(object sender, PaintEventArgs e)
        {
            DrawForm(sender, e);
        }

        private void SelectedCell(object sender, MouseEventArgs e)
        {
            game.SelectCell((int)e.X / scale, (int)e.Y / scale);

            pictureBox1.Refresh();
        }

        private void DrawForm(object sender, PaintEventArgs e)
        {

            int radius;
            Graphics canvas = e.Graphics;
            for (int i = 0; i < game.Field.Height; i++)
            {
                for (int j = 0; j < game.Field.Width; j++)
                {
                    canvas.FillRectangle(Brushes.Silver, i * scale, j * scale, scale - 2, scale - 2);
                    if (game.Field.Cells[i, j].Contain != null)
                    {
                        radius = (game.Field.Cells[i, j].Contain == ContainedItem.Big) ? 2 : 1;
                        canvas.FillEllipse(new SolidBrush(GetColor(game.Field.Cells[i, j].Color) ?? Color.Black), scale * i - 2, scale * j- 2, scale / 2 * radius, scale / 2 * radius);
                    }
                }
            }

            lbScore.Text = Settings.Score.ToString();
            lbTurn.Text = game.Turn.ToString();
            richTextBox1.Text = Settings.Messege;
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

                default:
                    return null;
            }

        }

       

    }
}
