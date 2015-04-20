using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lines.GameEngine
{
    
    public class Field
    {
        #region Constructors

        public Field()
        {
            this.Height = Settings.Height;
            this.Width = Settings.Width;

            Cells = new Cell[Height, Width];

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Cells[i, j] = new Cell() { Row = i, Column = j, Contain = null, Color = null };
                }
            }
        }

        #endregion

        #region Public Properties

        public int Width { get; set; }
        public int Height { get; set; }
        public Cell[,] Cells { get; set; }

        #endregion
    }
}
