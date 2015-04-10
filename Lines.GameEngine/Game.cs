using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lines.GameEngine
{
    public class Game
    {
        #region Properties

        public NetOfCells Field { get; set; }
        public Settings GameSettings { get; set; }
        public int Turn { get; private set; }

        private Cell _selectedCell = null;
        private Random _random = new Random();
        private int _emptyCells;
      
        #endregion

        #region Constructor

        public Game()
        {
            GameSettings = new Settings();
            Field = new NetOfCells();
            _emptyCells = 0;
            Turn = 0;
        }

        #endregion

        #region Methods

        public void Start()
        {
            for (int i = 0; i < Field.Height; i++)
            {
                for (int j = 0; j < Field.Width; j++)
                {
                    if (Field.Cells[i, j].Contain == null)
                    {
                        _emptyCells++;
                    }
                }
            }

            int generateBigBubbles = 3;
            int generateSmallBubbles = 3;

            while (generateBigBubbles != 0)
            {
                GenerateBubble(ContainedItem.Big);
                generateBigBubbles--;
            }
            while (generateSmallBubbles != 0)
            {
                GenerateBubble(ContainedItem.Small);
                generateSmallBubbles--;
            }
        }

        private void NextTurn(bool generateBubbles)
        {
            Turn++;
            if (generateBubbles)
            {
                BubbleGenerator();
            }
        }

        private void GenerateBubble(ContainedItem Size, BubbleColor? Color = null)
        {
            int randomRow;
            int randomCell;
            do
            {
                randomRow = _random.Next(0, Field.Width);
                randomCell = _random.Next(0, Field.Height);
                if (Field.Cells[randomRow, randomCell].Contain == null)
                {
                    int randomColor = _random.Next(0, Enum.GetNames(typeof(BubbleColor)).Length);
                    Field.Cells[randomRow, randomCell].Contain = Size;
                    Field.Cells[randomRow, randomCell].Color = Color ?? (BubbleColor)randomColor;
                    if (Size == ContainedItem.Big)
                    {
                        _emptyCells--;
                    }
                    return;
                }
            } while (Field.Cells[randomRow, randomCell].Contain != null && _emptyCells != 0);

        }

        private void BubbleGenerator()
        {
            Settings.Messege = _emptyCells.ToString();
            // Small bubbles raize into Big ones
            for (int i = 0; i < Field.Height; i++)
            {
                for (int j = 0; j < Field.Width; j++)
                {
                    if (Field.Cells[i, j].Contain == ContainedItem.Small)
                    {
                        _emptyCells--;
                        Field.Cells[i, j].Contain = ContainedItem.Big;
                    }
                }
            }

            int generateSmallBubbles = 0;

            if (_emptyCells > 1)
            {
                generateSmallBubbles = (_emptyCells == 2) ? 2 : 3;
            }
            else
            {
                Settings.Messege = "Game Over";
                Settings.GameOver = true;
            }
            while (generateSmallBubbles != 0)
            {
                GenerateBubble(ContainedItem.Small);
                generateSmallBubbles--;
            }
            Settings.Messege += "  " + _emptyCells.ToString();
        }

        public void SelectCell(int row, int col)
        {

            if (_selectedCell == null)
            {
                TrySelectBubble(row, col);
            }
            else
            {
                TryMoveBubble(row, col);
            }
        }

        private void TrySelectBubble(int row, int col)
        {

            if (Field.Cells[row, col].Contain == ContainedItem.Big)
            {
                _selectedCell = Field.Cells[row, col];
            }
            else
            {
                return;
            }
        }

        private void TryMoveBubble(int row, int col)
        {
            Cell CurrentCell = null;

            if (Field.Cells[row, col].Contain == null)
            {
                MoveBubble(_selectedCell, Field.Cells[row, col]);
                _selectedCell = null;

                // Check Lines Condiotion
                if (!CheckIfLine(row, col))
                {
                    // Begining of next Turn
                    NextTurn(true);
                }
                else
                    NextTurn(false);
            }
            else
            {
                if (Field.Cells[row, col].Contain == ContainedItem.Small)
                {
                    CurrentCell = new Cell()
                    {
                        Row = row,
                        Column = col,
                        Contain = Field.Cells[row, col].Contain,
                        Color = Field.Cells[row, col].Color
                    };

                    MoveBubble(_selectedCell, Field.Cells[row, col]);
                    _selectedCell = null;

                    // Check Lines Condiotion
                    if (!CheckIfLine(row, col))
                    {
                        GenerateBubble(ContainedItem.Small, CurrentCell.Color);
                        // Begining of next Turn
                        NextTurn(true);
                    }
                    else
                    {
                        Field.Cells[row, col] = CurrentCell;
                        NextTurn(false);
                    }
                }
                else
                {
                    _selectedCell = Field.Cells[row, col];
                }
            }
        }

        private void MoveBubble(Cell cellFrom, Cell cellTo)
        {
            cellTo.Contain = cellFrom.Contain;
            cellTo.Color = cellFrom.Color;

            cellFrom.Contain = null;
            cellFrom.Color = null;
        }


        #region Checking score condition

        private bool CheckIfLine(int row, int col)
        {
            int lineLength = 0;
            int numberOfLines = 0;

            Cell[] lines = new Cell[8];
            Cell currentCell = Field.Cells[row, col];

            Cell lineBegin;
            Cell lineEnd;

            int length;

            #region Vertical Check

            if (CheckLine_Vertical(row, col, out length, out lineBegin, out lineEnd))
            {
                numberOfLines++;
                lineLength += length;
                lines[0] = lineBegin;
                lines[1] = lineEnd;
            }

            #endregion

            #region Horizontal Check

            if (CheckLine_Horizontal(row, col, out length, out lineBegin, out lineEnd))
            {
                numberOfLines++;
                lineLength += length;
                lines[2] = lineBegin;
                lines[3] = lineEnd;
            }

            #endregion

            #region Left Diagonal


            if (CheckLine_LeftDiagonal(row, col, out length, out lineBegin, out lineEnd))
            {
                numberOfLines++;
                lineLength += length;
                lines[4] = lineBegin;
                lines[5] = lineEnd;
            }

            #endregion

            #region Right Diagonal


            if (CheckLine_RightDiagonal(row, col, out length, out lineBegin, out lineEnd))
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
                _emptyCells += lineLength - numberOfLines + 1;
            }
            // Delete null objects
            lines = lines.Where(x => x != null).ToArray();

            for (int i = 0; i < lines.Length; i += 2)
            {
                DestroyLines(lines[i], lines[i + 1]);
            }

            return lines.Length > 1;
        }

        private bool CheckLine_Vertical(int row, int col, out int length, out Cell lineBegin, out Cell lineEnd)
        {
            Cell currentCell = Field.Cells[row, col];
            lineBegin = currentCell;
            lineEnd = lineBegin;

            length = 1;
            int step = 0;

            while ((col - step - 1) >= 0 && Field.Cells[row, col - step - 1].Color == currentCell.Color && Field.Cells[row, col - step - 1].Contain == ContainedItem.Big)
            {
                lineBegin = Field.Cells[row, col - step - 1];
                step++;
                length++;
            }

            step = 0;
            while ((col + step + 1) < Settings.Height && Field.Cells[row, col + step + 1].Color == currentCell.Color && Field.Cells[row, col + step + 1].Contain == ContainedItem.Big)
            {
                lineEnd = Field.Cells[row, col + step + 1];
                step++;
                length++;
            }

            return length > 4 ? true : false;
        }

        private bool CheckLine_Horizontal(int row, int col, out int length, out Cell lineBegin, out Cell lineEnd)
        {
            Cell currentCell = Field.Cells[row, col];
            lineBegin = currentCell;
            lineEnd = lineBegin;

            length = 1;
            int step = 0;
            lineBegin = lineEnd = currentCell;

            while ((row - step - 1) >= 0 && Field.Cells[row - step - 1, col].Color == currentCell.Color && Field.Cells[row - step - 1, col].Contain == ContainedItem.Big)
            {
                lineBegin = Field.Cells[row - step - 1, col];
                step++;
                length++;
            }

            step = 0;
            while ((row + step + 1) < Settings.Height && Field.Cells[row + step + 1, col].Color == currentCell.Color && Field.Cells[row + step + 1, col].Contain == ContainedItem.Big)
            {
                lineEnd = Field.Cells[row + step + 1, col];
                step++;
                length++;
            }

            return length > 4 ? true : false;
        }

        private bool CheckLine_LeftDiagonal(int row, int col, out int length, out Cell lineBegin, out Cell lineEnd)
        {
            Cell currentCell = Field.Cells[row, col];
            lineBegin = currentCell;
            lineEnd = lineBegin;

            length = 1;
            int step = 0;
            lineBegin = lineEnd = currentCell;

            while ((col - step - 1) >= 0 && (row - step - 1) >= 0 && Field.Cells[row - step - 1, col - step - 1].Color == currentCell.Color && Field.Cells[row - step - 1, col - step - 1].Contain == ContainedItem.Big)
            {
                lineBegin = Field.Cells[row - step - 1, col - step - 1];
                step++;
                length++;
            }

            step = 0;
            while ((col + step + 1) < Settings.Height && (row + step + 1) < Settings.Height && Field.Cells[row + step + 1, col + step + 1].Color == currentCell.Color && Field.Cells[row + step + 1, col + step + 1].Contain == ContainedItem.Big)
            {
                lineEnd = Field.Cells[row + step + 1, col + step + 1];
                step++;
                length++;
            }
            
            return length > 4 ? true : false;
        }

        private bool CheckLine_RightDiagonal(int row, int col, out int length, out Cell lineBegin, out Cell lineEnd)
        {
            Cell currentCell = Field.Cells[row, col];
            lineBegin = currentCell;
            lineEnd = lineBegin;

            length = 1;
            int step = 0;
            lineBegin = lineEnd = currentCell;

            while ((row - step - 1) >= 0 && (col + step + 1) < Settings.Height && Field.Cells[row - step - 1, col + step + 1].Color == currentCell.Color && Field.Cells[row - step - 1, col + step + 1].Contain == ContainedItem.Big)
            {
                lineBegin = Field.Cells[row - step - 1, col + step + 1];
                step++;
                length++;
            }

            step = 0;
            while ((col - step - 1) >= 0 && (row + step + 1) < Settings.Height && Field.Cells[row + step + 1, col - step - 1].Color == currentCell.Color && Field.Cells[row + step + 1, col - step - 1].Contain == ContainedItem.Big)
            {
                lineEnd = Field.Cells[row + step + 1, col - step - 1];
                step++;
                length++;
            }

            return length > 4 ? true : false;
        }

        #endregion

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
