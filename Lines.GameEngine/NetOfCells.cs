using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lines.GameEngine
{
    
    public class NetOfCells
    {
        //private readonly Color[] bubbleColor = new Color[] { Color.Red };

        //private readonly Color[] bubbleColor = new Color[] { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Purple, };

        

        #region Properties

        public int Width { get; set; }
        public int Height { get; set; }
        public Cell[,] Cells { get; set; }

        #endregion

        #region Constructor

        public NetOfCells()
        {
            this.Width = Settings.Width;
            this.Height = Settings.Height;

            Cells = new Cell[Width, Height];

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Cells[i, j] = new Cell() { Row = i, Column = j, Contain = null, Color = null };
                }
            }


        }

        #endregion


    }
}
