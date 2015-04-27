using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lines.GameEngine.Enums;

namespace Lines.GameEngine.Scoring
{
    public class CheckLines
    {

        #region Private const field
        private const int _lineLength = 5;
        #endregion

        #region Private Fields

        private Field _field;
        private int _row;
        private int _column;
        private Cell _cell;

        #endregion

        #region Events

        public event EventHandler<int> ScoreChangedEventHandler;
        public event EventHandler<Cell[][]> DestroyLinesEventHandler;

        #endregion

        #region Constructors

        public CheckLines(Field field, Cell cellToBegin)
        {
            _field = field;
            _cell = cellToBegin;
            _row = _cell.Row;
            _column = _cell.Column;
        }

        #endregion

        #region Public Properties

        public int LineLength { get; set; }

        #endregion

        #region Methods

        #region Methods which using events

        private void OnScoreChange(int points)
        {
            if (ScoreChangedEventHandler != null)
            {
                ScoreChangedEventHandler(this, points);
            }
        }

        private void OnDestroyLines(Cell[][] lines)
        {
            if (DestroyLinesEventHandler != null)
            {
                DestroyLinesEventHandler(this, lines);
            }
        }

        #endregion

        public bool Check()
        {
            int lineLength = 0;
            int numberOfLines = 0;

            // Because only 4 lines can be created at one move
            Cell[][] lines = new Cell[4][];

            Cell[] line;

            int length;

            #region Horizontal Check

            if (CheckLine_Horizontal(out length, out line))
            {
                numberOfLines++;
                lineLength += length;
                lines[0] = line;
            }

            #endregion

            #region Vertical Check

            if (CheckLine_Vertical(out length, out line))
            {
                numberOfLines++;
                lineLength += length;
                lines[1] = line;
            }

            #endregion

            #region Left Diagonal


            if (CheckLine_LeftDiagonal(out length, out line))
            {
                numberOfLines++;
                lineLength += length;
                lines[2] = line;
            }

            #endregion

            #region Right Diagonal


            if (CheckLine_RightDiagonal(out length, out line))
            {
                numberOfLines++;
                lineLength += length;
                lines[3] = line;
            }


            #endregion

            if (numberOfLines > 0)
            {
                OnScoreChange((lineLength - numberOfLines + 1) * (lineLength - numberOfLines + 1));
                LineLength += lineLength - numberOfLines + 1;
            }

            lines = lines.Where(x => x != null).ToArray();

            if (lines.Length > 0)
            {
                OnDestroyLines(lines);
                return true;
            }
            return false;
        }

        public bool CheckLine_Horizontal(out int length, out Cell[] lineElements)
        {
            length = 1;
            int curRow = _row;
            int curCol = _column - 1;

            lineElements = new Cell[_field.Width];
            lineElements[0] = _field.Cells[_row, _column];

            while (LineCondition(curRow, curCol))
            {
                lineElements[length] = _field.Cells[curRow, curCol];
                curCol--;
                length++;
            }

            curRow = _row;
            curCol = _column + 1;
            while (LineCondition(curRow, curCol))
            {
                lineElements[length] = _field.Cells[curRow, curCol];
                curCol++;
                length++;
            }

            lineElements = lineElements.Where(x => x != null).ToArray();
            return length >= _lineLength ? true : false;
        }

        public bool CheckLine_Vertical(out int length, out Cell[] lineElements)
        {
            length = 1;
            int curRow = _row - 1;
            int curCol = _column;

            lineElements = new Cell[_field.Height];
            lineElements[0] = _field.Cells[_row, _column];

            while (LineCondition(curRow, curCol))
            {
                lineElements[length] = _field.Cells[curRow, curCol];
                curRow--;
                length++;
            }

            curRow = _row + 1;
            curCol = _column;
            while (LineCondition(curRow, curCol))
            {
                lineElements[length] = _field.Cells[curRow, curCol];
                curRow++;
                length++;
            }

            lineElements = lineElements.Where(x => x != null).ToArray();
            return length >= _lineLength ? true : false;
        }

        public bool CheckLine_LeftDiagonal(out int length, out Cell[] lineElements)
        {
            length = 1;
            int curRow = _row - 1;
            int curCol = _column - 1;
            int lineMaxLength = (_field.Height > _field.Width) ? _field.Width : _field.Height;

            lineElements = new Cell[lineMaxLength];
            lineElements[0] = _field.Cells[_row, _column];
            while (LineCondition(curRow, curCol))
            {
                lineElements[length] = _field.Cells[curRow, curCol];
                curRow--;
                curCol--;
                length++;
            }

            curRow = _row + 1;
            curCol = _column + 1;
            while (LineCondition(curRow, curCol))
            {
                lineElements[length] = _field.Cells[curRow, curCol];
                curRow++;
                curCol++;
                length++;
            }

            lineElements = lineElements.Where(x => x != null).ToArray();
            return length >= _lineLength ? true : false;
        }

        public bool CheckLine_RightDiagonal(out int length, out Cell[] lineElements)
        {
            length = 1;
            int curRow = _row - 1;
            int curCol = _column + 1;
            int lineMaxLength = (_field.Height > _field.Width) ? _field.Width : _field.Height;

            lineElements = new Cell[lineMaxLength];
            lineElements[0] = _field.Cells[_row, _column];
            while (LineCondition(curRow, curCol))
            {
                lineElements[length] = _field.Cells[curRow, curCol];
                curRow--;
                curCol++;
                length++;
            }

            curRow = _row + 1;
            curCol = _column - 1;
            while (LineCondition(curRow, curCol))
            {
                lineElements[length] = _field.Cells[curRow, curCol];
                curRow++;
                curCol--;
                length++;
            }

            lineElements = lineElements.Where(x => x != null).ToArray();
            return length >= _lineLength ? true : false;
        }

        #endregion

        #region Helpers

        private bool IsCellValid(int row, int col)
        {
            if ((row >= 0) && (row <= _field.Height - 1) && (col >= 0) && (col <= _field.Width - 1))
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
