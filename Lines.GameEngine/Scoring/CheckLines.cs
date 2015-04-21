using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lines.GameEngine.Scoring
{
    public class CheckLines
    {
        #region Private Fields

        private Field _field;
        private int _row;
        private int _column;
        private Cell _cell;
        private DestroyLines _destroyLines;

        #endregion

        #region Events

        public event Action<int> UpdateScoreLabelHandler;
        
        #endregion

        #region Constructors

        public CheckLines(Field field, Cell cell)
        {
            _field = field;
            _cell = cell;
            _destroyLines = new DestroyLines(_field);
            _row = _cell.Row;
            _column = _cell.Column;
        }

        #endregion
        
        #region Public Properties

        
        public int LineLength { get; set; }

        #endregion

        #region Public Methods

        #region Methods which using events

        private void UpdateScore(int points)
        {
            if (UpdateScoreLabelHandler != null)
            {
                UpdateScoreLabelHandler(points);
            }
        }

        #endregion

        public bool Check()
        {
            int lineLength = 0;
            int numberOfLines = 0;

            Cell[] lines = new Cell[8];

            Cell lineBegin;
            Cell lineEnd;

            int length;

            #region Horizontal Check

            if (CheckLine_Horizontal(out length, out lineBegin, out lineEnd))
            {
                numberOfLines++;
                lineLength += length;
                lines[0] = lineBegin;
                lines[1] = lineEnd;
            }

            #endregion

            #region Vertical Check

            if (CheckLine_Vertical(out length, out lineBegin, out lineEnd))
            {
                numberOfLines++;
                lineLength += length;
                lines[2] = lineBegin;
                lines[3] = lineEnd;
            }

            #endregion

            #region Left Diagonal


            if (CheckLine_LeftDiagonal(out length, out lineBegin, out lineEnd))
            {
                numberOfLines++;
                lineLength += length;
                lines[4] = lineBegin;
                lines[5] = lineEnd;
            }

            #endregion

            #region Right Diagonal


            if (CheckLine_RightDiagonal(out length, out lineBegin, out lineEnd))
            {
                numberOfLines++;
                lineLength += length;
                lines[6] = lineBegin;
                lines[7] = lineEnd;
            }


            #endregion

            if (numberOfLines > 0)
            {
                UpdateScore((lineLength - numberOfLines + 1) * (lineLength - numberOfLines + 1));
                LineLength += lineLength - numberOfLines + 1;
            }
            // Delete null objects
            lines = lines.Where(x => x != null).ToArray();

            for (int i = 0; i < lines.Length; i += 2)
            {
                _destroyLines.Destroy(lines[i], lines[i + 1]);
            }

            return lines.Length > 1;
        }

        public bool CheckLine_Horizontal(out int length, out Cell lineBegin, out Cell lineEnd)
        {
            lineBegin = _cell;
            lineEnd = lineBegin;

            length = 1;
            int step = 0;

            while ((_column - step - 1) >= 0 && _field.Cells[_row, _column - step - 1].Color == _cell.Color && _field.Cells[_row, _column - step - 1].Contain == BubbleSize.Big)
            {
                lineBegin = _field.Cells[_row, _column - step - 1];
                step++;
                length++;
            }

            step = 0;
            while ((_column + step + 1) < _field.Width && _field.Cells[_row, _column + step + 1].Color == _cell.Color && _field.Cells[_row, _column + step + 1].Contain == BubbleSize.Big)
            {
                lineEnd = _field.Cells[_row, _column + step + 1];
                step++;
                length++;
            }

            return length > 4 ? true : false;
        }

        public bool CheckLine_Vertical(out int length, out Cell lineBegin, out Cell lineEnd)
        {
            lineBegin = _cell;
            lineEnd = lineBegin;

            length = 1;
            int step = 0;
            lineBegin = lineEnd = _cell;

            while ((_row - step - 1) >= 0 && _field.Cells[_row - step - 1, _column].Color == _cell.Color && _field.Cells[_row - step - 1, _column].Contain == BubbleSize.Big)
            {
                lineBegin = _field.Cells[_row - step - 1, _column];
                step++;
                length++;
            }

            step = 0;
            while ((_row + step + 1) < _field.Height && _field.Cells[_row + step + 1, _column].Color == _cell.Color && _field.Cells[_row + step + 1, _column].Contain == BubbleSize.Big)
            {
                lineEnd = _field.Cells[_row + step + 1, _column];
                step++;
                length++;
            }

            return length > 4 ? true : false;
        }

        public bool CheckLine_LeftDiagonal(out int length, out Cell lineBegin, out Cell lineEnd)
        {
            lineBegin = _cell;
            lineEnd = lineBegin;

            length = 1;
            int step = 0;
            lineBegin = lineEnd = _cell;

            while ((_column - step - 1) >= 0 && (_row - step - 1) >= 0 && _field.Cells[_row - step - 1, _column - step - 1].Color == _cell.Color && _field.Cells[_row - step - 1, _column - step - 1].Contain == BubbleSize.Big)
            {
                lineBegin = _field.Cells[_row - step - 1, _column - step - 1];
                step++;
                length++;
            }

            step = 0;
            while ((_column + step + 1) < _field.Width && (_row + step + 1) < _field.Height && _field.Cells[_row + step + 1, _column + step + 1].Color == _cell.Color && _field.Cells[_row + step + 1, _column + step + 1].Contain == BubbleSize.Big)
            {
                lineEnd = _field.Cells[_row + step + 1, _column + step + 1];
                step++;
                length++;
            }

            return length > 4 ? true : false;
        }

        public bool CheckLine_RightDiagonal(out int length, out Cell lineBegin, out Cell lineEnd)
        {
            lineBegin = _cell;
            lineEnd = lineBegin;

            length = 1;
            int step = 0;
            lineBegin = lineEnd = _cell;

            while ((_row - step - 1) >= 0 && (_column + step + 1) < _field.Width && _field.Cells[_row - step - 1, _column + step + 1].Color == _cell.Color && _field.Cells[_row - step - 1, _column + step + 1].Contain == BubbleSize.Big)
            {
                lineBegin = _field.Cells[_row - step - 1, _column + step + 1];
                step++;
                length++;
            }

            step = 0;
            while ((_column - step - 1) >= 0 && (_row + step + 1) < _field.Height && _field.Cells[_row + step + 1, _column - step - 1].Color == _cell.Color && _field.Cells[_row + step + 1, _column - step - 1].Contain == BubbleSize.Big)
            {
                lineEnd = _field.Cells[_row + step + 1, _column - step - 1];
                step++;
                length++;
            }

            return length > 4 ? true : false;
        }


        #endregion
    }
}
