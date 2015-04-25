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

            if (width < 5)
            {
                throw new ArgumentException("'width' cannot be < 5");
            }

            if (height < 5)
            {
                throw new ArgumentException("'height' cannot be < 5");
            }

            if (width > 10)
            {
                throw new ArgumentException("'width' cannot be > 10");
            }

            if (height > 10)
            {
                throw new ArgumentException("'height' cannot be > 10");
            }

            #endregion

            this.Height = height;
            this.Width = width;

            this.Cells = new Cell[this.Height, this.Width];

            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    this.Cells[i, j] = new Cell(i, j, null, null);
                }
            }

            this.EmptyCells = 0;
        }

        #endregion

        #region Public Properties

        public int Width { get; private set; }
        public int Height { get; private set; }
        public Cell[,] Cells { get; set; }
        public int EmptyCells { get; set; }

        #endregion
    }
}
