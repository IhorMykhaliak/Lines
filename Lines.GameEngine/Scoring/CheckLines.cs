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

        public event Action<int> UpdateScoreHandler;
        
        #endregion

        #region Constructors

        public CheckLines(Field field, Cell cellToBegin)
        {
            _field = field;
            _cell = cellToBegin;
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
            if (UpdateScoreHandler != null)
            {
                UpdateScoreHandler(points);
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
            int curRow = _row;
            int curCol = _column - 1;

            while (LineCondition(curRow, curCol))
            {
                lineBegin = _field.Cells[curRow, curCol];
                curCol--;
                length++;
            }

            curRow = _row;
            curCol = _column + 1;
            while (LineCondition(curRow, curCol))
            {
                lineEnd = _field.Cells[curRow, curCol];
                curCol++;
                length++;
            }

            return length > 4 ? true : false;
        }

        public bool CheckLine_Vertical(out int length, out Cell lineBegin, out Cell lineEnd)
        {
            lineBegin = _cell;
            lineEnd = lineBegin;

            length = 1;
            int curRow = _row - 1;
            int curCol = _column;

            while (LineCondition(curRow, curCol))
            {
                lineBegin = _field.Cells[curRow, curCol];
                curRow--;
                length++;
            }

            curRow = _row + 1;
            curCol = _column;
            while (LineCondition(curRow, curCol))
            {
                lineEnd = _field.Cells[curRow, curCol];
                curRow++;
                length++;
            }

            return length > 4 ? true : false;
        }

        public bool CheckLine_LeftDiagonal(out int length, out Cell lineBegin, out Cell lineEnd)
        {
            lineBegin = _cell;
            lineEnd = lineBegin;

            length = 1; 
            int curRow = _row - 1;
            int curCol = _column - 1;

            while (LineCondition(curRow, curCol))
            {
                lineBegin = _field.Cells[curRow, curCol];
                curRow--;
                curCol--;
                length++;
            }

            curRow = _row + 1;
            curCol = _column + 1;
            while (LineCondition(curRow, curCol))
            {
                lineEnd = _field.Cells[curRow, curCol];
                curRow++;
                curCol++;
                length++;
            }

            return length > 4 ? true : false;
        }

        public bool CheckLine_RightDiagonal(out int length, out Cell lineBegin, out Cell lineEnd)
        {
            lineBegin = _cell;
            lineEnd = lineBegin;

            length = 1;
            int curRow = _row - 1;
            int curCol = _column + 1;

            while (LineCondition(curRow, curCol))
            {
                lineBegin = _field.Cells[curRow, curCol];
                curRow--;
                curCol++;
                length++;
            }

            curRow = _row + 1;
            curCol = _column - 1;
            while (LineCondition(curRow, curCol))
            {
                lineEnd = _field.Cells[curRow, curCol];
                curRow++;
                curCol--;
                length++;
            }

            return length > 4 ? true : false;
        }

        #endregion

        #region Helpers

        private bool IsRowValid(int row)
        {
            if (row >= 0 &&  row <= _field.Height - 1)
            {
                return true;
            }
            return false;
        }

        private bool IsColumnValid(int col)
        {
            if (col >= 0 && col <= _field.Width - 1)
            {
                return true;
            }
            return false;
        }

        private bool IsCellValid(int row, int col)
        {
            if (row >= 0 && row <= _field.Height - 1 && col >= 0 && col <= _field.Width - 1)
            {
                return true;
            }
            return false;
        }

        private bool LineCondition(int curRow, int curCol)
        {
            return IsCellValid(curRow, curCol) && _field.Cells[curRow, curCol].Color == _cell.Color && _field.Cells[curRow, curCol].Contain == BubbleSize.Big;
        }

        #endregion
    }
}
