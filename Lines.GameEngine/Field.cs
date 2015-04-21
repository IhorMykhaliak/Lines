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

        public Field(int height, int width)
        {
            #region Validation

            if (width <= 5)
            {
                throw new ArgumentException("'width' cannot be <= 5");
            }

            if (height <= 5)
            {
                throw new ArgumentException("'height' cannot be <= 5");
            }

            #endregion
            this.Height = height;
            this.Width = width;

            Cells = new Cell[this.Height, this.Width];

            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    this.Cells[i, j] = new Cell() { Row = i, Column = j, Contain = null, Color = null };
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
