using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lines.GameEngine
{
    public class CheckLines
    {
        public Field Field { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int LineLength { get; set; }

        private DestroyLines _destroyLines;

        public event Action UpdateScoreLabelHandler;

        public CheckLines(Field field, int row, int col)
        {
            Field = field;
            Row = row;
            Column = col;
            _destroyLines = new DestroyLines(Field);
        }

        private void UpdateScore()
        {
            if (UpdateScoreLabelHandler != null)
            {
                UpdateScoreLabelHandler();
            }
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
                UpdateScore();
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

       
        #endregion
    }
}
