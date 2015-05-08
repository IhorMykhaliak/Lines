using System;
using Lines.GameEngine.Enums;

namespace Lines.GameEngine
{
    public class Field
    {
        #region Fields

        private Cell[,] _cells;

        #endregion

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

            this._cells = new Cell[this.Height, this.Width];

            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    this._cells[i, j] = new Cell(i, j, null, null);
                }
            }

            this.EmptyCells = this.Height * this.Width;
        }

        public Field(Field other)
        {
            this.Height = other.Height;
            this.Width = other.Width;

            this._cells = new Cell[this.Height, this.Width];

            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    this[i, j] = new Cell(other[i, j]);
                }
            }

            this.EmptyCells = other.EmptyCells;
        }

        #endregion

        #region Public Properties

        public int Width { get; private set; }
        public int Height { get; private set; }
        public Cell this [int row, int col]
        {
            get { return _cells[row, col];}
            set {_cells[row, col] = value;}
        }
        public int EmptyCells { get; set; }

        #endregion

        #region Public Methods

        public void CountEmptyCells()
        {
            int result = 0;
            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    if (this._cells[i, j].Contain != BubbleSize.Big)
                    {
                        result++;
                    }
                }
            }
            EmptyCells = result;
        }

        public void PlaceBubbles(Cell[] bubbles)
        {
            foreach (var bubble in bubbles)
            {
                this[bubble.Row, bubble.Column].Contain = bubble.Contain;
                this[bubble.Row, bubble.Column].Color = bubble.Color;
            }
        }

        #endregion
    }
}
