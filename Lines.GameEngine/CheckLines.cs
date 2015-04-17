using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lines.GameEngine
{
    public class CheckLines
    {
        public NetOfCells Field { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int LineLength { get; set; }

        public event Action UpdateScoreLabelHandler;

        public CheckLines(NetOfCells field, int row, int col)
        {
            Field = field;
            Row = row;
            Column = col;
        }

        #region Checking score conditions

        public bool Check()
        {
            int lineLength = 0;
            int numberOfLines = 0;

            Cell[] lines = new Cell[8];
            Cell currentCell = Field.Cells[Row, Column];

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
                Settings.Score += (lineLength - numberOfLines + 1) * (lineLength - numberOfLines + 1);
                UpdateScoreLabelHandler();
                LineLength += lineLength - numberOfLines + 1;
            }
            // Delete null objects
            lines = lines.Where(x => x != null).ToArray();

            for (int i = 0; i < lines.Length; i += 2)
            {
                DestroyLines(lines[i], lines[i + 1]);
            }

            return lines.Length > 1;
        }

        public bool CheckLine_Horizontal(out int length, out Cell lineBegin, out Cell lineEnd)
        {
            Cell currentCell = Field.Cells[Row, Column];
            lineBegin = currentCell;
            lineEnd = lineBegin;

            length = 1;
            int step = 0;

            while ((Column - step - 1) >= 0 && Field.Cells[Row, Column - step - 1].Color == currentCell.Color && Field.Cells[Row, Column - step - 1].Contain == BubbleSize.Big)
            {
                lineBegin = Field.Cells[Row, Column - step - 1];
                step++;
                length++;
            }

            step = 0;
            while ((Column + step + 1) < Settings.Height && Field.Cells[Row, Column + step + 1].Color == currentCell.Color && Field.Cells[Row, Column + step + 1].Contain == BubbleSize.Big)
            {
                lineEnd = Field.Cells[Row, Column + step + 1];
                step++;
                length++;
            }

            return length > 4 ? true : false;
        }

        public bool CheckLine_Vertical(out int length, out Cell lineBegin, out Cell lineEnd)
        {
            Cell currentCell = Field.Cells[Row, Column];
            lineBegin = currentCell;
            lineEnd = lineBegin;

            length = 1;
            int step = 0;
            lineBegin = lineEnd = currentCell;

            while ((Row - step - 1) >= 0 && Field.Cells[Row - step - 1, Column].Color == currentCell.Color && Field.Cells[Row - step - 1, Column].Contain == BubbleSize.Big)
            {
                lineBegin = Field.Cells[Row - step - 1, Column];
                step++;
                length++;
            }

            step = 0;
            while ((Row + step + 1) < Settings.Height && Field.Cells[Row + step + 1, Column].Color == currentCell.Color && Field.Cells[Row + step + 1, Column].Contain == BubbleSize.Big)
            {
                lineEnd = Field.Cells[Row + step + 1, Column];
                step++;
                length++;
            }

            return length > 4 ? true : false;
        }

        public bool CheckLine_LeftDiagonal(out int length, out Cell lineBegin, out Cell lineEnd)
        {
            Cell currentCell = Field.Cells[Row, Column];
            lineBegin = currentCell;
            lineEnd = lineBegin;

            length = 1;
            int step = 0;
            lineBegin = lineEnd = currentCell;

            while ((Column - step - 1) >= 0 && (Row - step - 1) >= 0 && Field.Cells[Row - step - 1, Column - step - 1].Color == currentCell.Color && Field.Cells[Row - step - 1, Column - step - 1].Contain == BubbleSize.Big)
            {
                lineBegin = Field.Cells[Row - step - 1, Column - step - 1];
                step++;
                length++;
            }

            step = 0;
            while ((Column + step + 1) < Settings.Height && (Row + step + 1) < Settings.Height && Field.Cells[Row + step + 1, Column + step + 1].Color == currentCell.Color && Field.Cells[Row + step + 1, Column + step + 1].Contain == BubbleSize.Big)
            {
                lineEnd = Field.Cells[Row + step + 1, Column + step + 1];
                step++;
                length++;
            }

            return length > 4 ? true : false;
        }

        public bool CheckLine_RightDiagonal(out int length, out Cell lineBegin, out Cell lineEnd)
        {
            Cell currentCell = Field.Cells[Row, Column];
            lineBegin = currentCell;
            lineEnd = lineBegin;

            length = 1;
            int step = 0;
            lineBegin = lineEnd = currentCell;

            while ((Row - step - 1) >= 0 && (Column + step + 1) < Settings.Height && Field.Cells[Row - step - 1, Column + step + 1].Color == currentCell.Color && Field.Cells[Row - step - 1, Column + step + 1].Contain == BubbleSize.Big)
            {
                lineBegin = Field.Cells[Row - step - 1, Column + step + 1];
                step++;
                length++;
            }

            step = 0;
            while ((Column - step - 1) >= 0 && (Row + step + 1) < Settings.Height && Field.Cells[Row + step + 1, Column - step - 1].Color == currentCell.Color && Field.Cells[Row + step + 1, Column - step - 1].Contain == BubbleSize.Big)
            {
                lineEnd = Field.Cells[Row + step + 1, Column - step - 1];
                step++;
                length++;
            }

            return length > 4 ? true : false;
        }

        private void DestroyLines(Cell cellFrom, Cell cellTo)
        {
            int cell1_row = cellFrom.Row;
            int cell1_col = cellFrom.Column;

            int cell2_row = cellTo.Row;
            int cell2_col = cellTo.Column;


            if (cell1_row == cell2_row)
            {
                for (int j = cell1_col; j <= cell2_col; j++)
                {
                    Field.Cells[cell1_row, j].Contain = null;
                    Field.Cells[cell1_row, j].Color = null;
                }
                return;
            }


            if (cell1_col == cell2_col)
            {
                for (int i = cell1_row; i <= cell2_row; i++)
                {
                    Field.Cells[i, cell1_col].Contain = null;
                    Field.Cells[i, cell1_col].Color = null;
                }
                return;
            }

            if (cell1_row <= cell2_row && cell1_col <= cell2_col)
            {
                for (int i = 0; i < cell2_row - cell1_row + 1; i++)
                {
                    Field.Cells[cell1_row + i, cell1_col + i].Contain = null;
                    Field.Cells[cell1_row + i, cell1_col + i].Color = null;
                }
                return;
            }

            if (cell1_row <= cell2_row && cell1_col >= cell2_col)
            {
                for (int i = 0; i < cell2_row - cell1_row + 1; i++)
                {
                    Field.Cells[cell1_row + i, cell1_col - i].Contain = null;
                    Field.Cells[cell1_row + i, cell1_col - i].Color = null;
                }
                return;
            }
        }

        #endregion
    }
}
