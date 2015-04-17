using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lines.GameEngine.PathFinding_Algorithm;

namespace Lines.GameEngine
{
    public class Game
    {
        #region Properties

        public NetOfCells Field { get; set; }
        public int Turn { get; private set; }

        private Cell _selectedCell = null;
        private Random _random = new Random();
        private int _emptyCells;
        private CheckLines _checkLine;

        #endregion

        #region Events

        public event Action UpdateScoreLabelHandler;
        public event Action DrawFieldHandler;

        #endregion

        #region Constructor

        public Game()
        {
            Field = new NetOfCells();
            _emptyCells = 0;
            Turn = 0;
        }

        #endregion

        #region Methods

        #region methods which using events

        private void UpdateScore()
        {
            if (UpdateScoreLabelHandler != null)
            {
                UpdateScoreLabelHandler();
            }
        }

        private void Draw()
        {
            if (DrawFieldHandler != null)
            {
                DrawFieldHandler();
            }
        }

        #endregion

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
                GenerateBubble(BubbleSize.Big);
                generateBigBubbles--;
            }
            while (generateSmallBubbles != 0)
            {
                GenerateBubble(BubbleSize.Small);
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

        private void GenerateBubble(BubbleSize Size, BubbleColor? Color = null)
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
                    if (Size == BubbleSize.Big)
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
                    if (Field.Cells[i, j].Contain == BubbleSize.Small)
                    {
                        _emptyCells--;
                        Field.Cells[i, j].Contain = BubbleSize.Big;
                    }
                }
            }

            int generateSmallBubbles = (_emptyCells > 2) ? 3 : _emptyCells;

            if (_emptyCells == 0)
            {
                Settings.Messege = "Game Over";
                Settings.GameOver = true;
            }
            while (generateSmallBubbles != 0)
            {
                GenerateBubble(BubbleSize.Small);
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

                Draw();
            }
        }

        private void TrySelectBubble(int row, int col)
        {

            if (Field.Cells[row, col].Contain == BubbleSize.Big)
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
                _checkLine = new CheckLines(Field, row, col);
                _checkLine.UpdateScoreLabelHandler += UpdateScore;

                if (MoveBubble(_selectedCell, Field.Cells[row, col]))
                {

                    _selectedCell = null;

                    if (!_checkLine.Check())
                    {
                        NextTurn(true);
                    }
                    else
                    {
                        _emptyCells += _checkLine.LineLength;
                        NextTurn(false);
                    }
                }
            }
            else
            {
                if (Field.Cells[row, col].Contain == BubbleSize.Small)
                {
                    _checkLine = new CheckLines(Field, row, col);
                    _checkLine.UpdateScoreLabelHandler += UpdateScore;
                    CurrentCell = new Cell()
                    {
                        Row = row,
                        Column = col,
                        Contain = Field.Cells[row, col].Contain,
                        Color = Field.Cells[row, col].Color
                    };

                    if (MoveBubble(_selectedCell, Field.Cells[row, col]))
                    {
                        _selectedCell = null;

                        if (!_checkLine.Check())
                        {
                            GenerateBubble(BubbleSize.Small, CurrentCell.Color);
                            NextTurn(true);
                        }
                        else
                        {
                            _emptyCells += _checkLine.LineLength;
                            Field.Cells[row, col] = CurrentCell;
                            NextTurn(false);
                        }
                    }
                }
                else
                {
                    _selectedCell = Field.Cells[row, col];
                }
            }
        }

        public bool MoveBubble(Cell cellFrom, Cell cellTo)
        {
            List<Cell> Way;
            if (FindPath.GetWay(Field, cellFrom, cellTo,out Way))
            {
                cellTo.Contain = cellFrom.Contain;
                cellTo.Color = cellFrom.Color;

                cellFrom.Contain = null;
                cellFrom.Color = null;

                return true;
            }
            else
            {
                Settings.Messege = "No way!!";
                return false;
            }
        }
        
        #endregion


    }
}
