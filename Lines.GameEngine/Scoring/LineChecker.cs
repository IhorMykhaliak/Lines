using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lines.GameEngine.Enums;

namespace Lines.GameEngine.Scoring
{
    public class LineChecker
    {
        #region Const fields

        private const int GameLineLength = 5;

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

        public LineChecker(Field field, Cell cellToBegin)
        {
            _field = field;
            _cell = cellToBegin;
            _row = _cell.Row;
            _column = _cell.Column;
        }

        #endregion

        #region Public Properties

        public int LineLength { get; private set; }

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

            if (HorizontalLine(out length, out line))
            {
                numberOfLines++;
                lineLength += length;
                lines[0] = line;
            }

            #endregion

            #region Vertical Check

            if (VerticalLine(out length, out line))
            {
                numberOfLines++;
                lineLength += length;
                lines[1] = line;
            }

            #endregion

            #region Left Diagonal

            if (LeftDiagonalLine(out length, out line))
            {
                numberOfLines++;
                lineLength += length;
                lines[2] = line;
            }

            #endregion

            #region Right Diagonal

            if (RightDiagonalLine(out length, out line))
            {
                numberOfLines++;
                lineLength += length;
                lines[3] = line;
            }

            #endregion

            lines = lines.Where(x => x != null).ToArray();

            if (lines.Length > 0)
            {
                OnDestroyLines(lines);
                LineLength = lineLength - numberOfLines + 1;
                OnScoreChange(LineLength * LineLength);
            }

            return lines.Length > 0;
        }

        public bool HorizontalLine(out int length, out Cell[] lineElements)
        {
            length = 1;
            int curRow = _row;
            int curCol = _column - 1;

            lineElements = new Cell[_field.Width];
            lineElements[0] = _field[_row, _column];

            while (LineCondition(curRow, curCol))
            {
                lineElements[length] = _field[curRow, curCol];
                curCol--;
                length++;
            }

            curRow = _row;
            curCol = _column + 1;
            while (LineCondition(curRow, curCol))
            {
                lineElements[length] = _field[curRow, curCol];
                curCol++;
                length++;
            }

            lineElements = lineElements.Where(x => x != null).ToArray();
            return length >= GameLineLength;
        }

        public bool VerticalLine(out int length, out Cell[] lineElements)
        {
            length = 1;
            int curRow = _row - 1;
            int curCol = _column;

            lineElements = new Cell[_field.Height];
            lineElements[0] = _field[_row, _column];

            while (LineCondition(curRow, curCol))
            {
                lineElements[length] = _field[curRow, curCol];
                curRow--;
                length++;
            }

            curRow = _row + 1;
            curCol = _column;
            while (LineCondition(curRow, curCol))
            {
                lineElements[length] = _field[curRow, curCol];
                curRow++;
                length++;
            }

            lineElements = lineElements.Where(x => x != null).ToArray();
            return length >= GameLineLength;
        }

        public bool LeftDiagonalLine(out int length, out Cell[] lineElements)
        {
            length = 1;
            int curRow = _row - 1;
            int curCol = _column - 1;
            int lineMaxLength = (_field.Height > _field.Width) ? _field.Width : _field.Height;

            lineElements = new Cell[lineMaxLength];
            lineElements[0] = _field[_row, _column];
            while (LineCondition(curRow, curCol))
            {
                lineElements[length] = _field[curRow, curCol];
                curRow--;
                curCol--;
                length++;
            }

            curRow = _row + 1;
            curCol = _column + 1;
            while (LineCondition(curRow, curCol))
            {
                lineElements[length] = _field[curRow, curCol];
                curRow++;
                curCol++;
                length++;
            }

            lineElements = lineElements.Where(x => x != null).ToArray();
            return length >= GameLineLength;
        }

        public bool RightDiagonalLine(out int length, out Cell[] lineElements)
        {
            length = 1;
            int curRow = _row - 1;
            int curCol = _column + 1;
            int lineMaxLength = (_field.Height > _field.Width) ? _field.Width : _field.Height;

            lineElements = new Cell[lineMaxLength];
            lineElements[0] = _field[_row, _column];
            while (LineCondition(curRow, curCol))
            {
                lineElements[length] = _field[curRow, curCol];
                curRow--;
                curCol++;
                length++;
            }

            curRow = _row + 1;
            curCol = _column - 1;
            while (LineCondition(curRow, curCol))
            {
                lineElements[length] = _field[curRow, curCol];
                curRow++;
                curCol--;
                length++;
            }

            lineElements = lineElements.Where(x => x != null).ToArray();
            return length >= GameLineLength;
        }

        #endregion

        #region Helpers

        private bool IsCellValid(int row, int col)
        {
            if ((row >= 0) && 
                (row <= _field.Height - 1) && 
                (col >= 0) && 
                (col <= _field.Width - 1))
            {
                return true;
            }
            return false;
        }

        private bool LineCondition(int curRow, int curCol)
        {
            return (IsCellValid(curRow, curCol)) && 
                (_field[curRow, curCol].Color == _cell.Color) && 
                (_field[curRow, curCol].ContainedItem == BubbleSize.Big);
        }

        #endregion
    }
}
