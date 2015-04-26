using System;
using Lines.GameEngine.Enums;

namespace Lines.GameEngine
{
    public class Cell
    {
        private readonly int _row;
        private readonly int _col;
        #region Public Properties

        public int Row { get { return _row; } }
        public int Column { get { return _col; } }
        public BubbleSize? Contain { get; set; }
        public BubbleColor? Color { get; set; }

        #endregion

        #region Constructors

        public Cell(int row, int col, BubbleSize? contain, BubbleColor? color)
        {
            this._row = row;
            this._col = col;
            this.Contain = contain;
            this.Color = color;
        }

        public Cell(Cell previous)
        {
            this._row = previous.Row;
            this._col = previous.Column;
            this.Contain = previous.Contain;
            this.Color = previous.Color;
        }

        #endregion
    }
}
