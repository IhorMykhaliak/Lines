using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lines.GameEngine.Scoring;
using Lines.GameEngine.PathFinding_Algorithm;

namespace Lines.GameEngine.Logic
{
    public class GameLogic
    {
        #region Private Fields

        private Cell _selectedCell = null;
        private Field _field;
        private CheckLines _checkLine;

        #endregion

        #region Events
        public event Action DrawEventHandler;
        public event Action UpdateScoreEventHandler;
        public event Action GameOverHandler;
        #endregion

        #region Constructors

        public GameLogic(Field field)
        {
            EmptyCells = 0;
            Turn = 0;
            _field = field;
        }

        #endregion

        #region Public Properties

        public int EmptyCells { get; set; }
        public int Turn { get; set; }

        #endregion

        #region Public Methods

        #region methods which using events

        private void Draw()
        {
            if (DrawEventHandler != null)
            {
                DrawEventHandler();
            }
        }

        private void UpdateScore()
        {
            if (UpdateScoreEventHandler != null)
            {
                UpdateScoreEventHandler();
            }
            else
            {
                throw new InvalidOperationException("Update score event wasn't initialized");
            }
        }

        private void GameOver()
        {
            if (GameOverHandler != null)
            {
                GameOverHandler();
            }
        }

        #endregion

        private void NextTurn(bool generateBubbles)
        {
            Turn++;
            if (generateBubbles)
            {
                Settings.Messege = EmptyCells.ToString();
                // Small bubbles raize into Big ones
                for (int i = 0; i < _field.Height; i++)
                {
                    for (int j = 0; j < _field.Width; j++)
                    {
                        if (_field.Cells[i, j].Contain == BubbleSize.Small)
                        {
                            EmptyCells--;
                            _field.Cells[i, j].Contain = BubbleSize.Big;
                        }
                    }
                }

                int smallBubbles = (EmptyCells > 2) ? 3 : EmptyCells;

                if (EmptyCells == 0)
                {
                    Settings.Messege = "Game Over";
                    Settings.GameOver = true;
                    GameOver();
                }

                BubbleGenerator.Generate(_field, smallBubbles);

                Settings.Messege += "  " + EmptyCells.ToString();
            }
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

            if (_field.Cells[row, col].Contain == BubbleSize.Big)
            {
                _selectedCell = _field.Cells[row, col];
            }
            else
            {
                return;
            }
        }

        private void TryMoveBubble(int row, int col)
        {
            Cell CurrentCell = null;
            _checkLine = new CheckLines(_field, row, col);
            _checkLine.UpdateScoreLabelHandler += UpdateScore;

            if (_field.Cells[row, col].Contain == null)
            {
                if (MoveBubble(_selectedCell, _field.Cells[row, col]))
                {
                    _selectedCell = null;

                    if (!_checkLine.Check())
                    {
                        NextTurn(true);
                    }
                    else
                    {
                        EmptyCells += _checkLine.LineLength;
                        NextTurn(false);
                    }
                }
            }
            else
            {
                if (_field.Cells[row, col].Contain == BubbleSize.Small)
                {
                    CurrentCell = new Cell()
                    {
                        Row = row,
                        Column = col,
                        Contain = _field.Cells[row, col].Contain,
                        Color = _field.Cells[row, col].Color
                    };

                    if (MoveBubble(_selectedCell, _field.Cells[row, col]))
                    {
                        _selectedCell = null;

                        if (!_checkLine.Check())
                        {
                            BubbleGenerator.GenerateSmallBubble(_field, BubbleSize.Small, CurrentCell.Color);
                            NextTurn(true);
                        }
                        else
                        {
                            EmptyCells += _checkLine.LineLength;
                            _field.Cells[row, col] = CurrentCell;
                            NextTurn(false);
                        }
                    }
                }
                else
                {
                    _selectedCell = _field.Cells[row, col];
                }
            }
        }

        private bool MoveBubble(Cell cellFrom, Cell cellTo)
        {
            List<Cell> Way;
            if (FindPath.GetWay(_field, cellFrom, cellTo, out Way))
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
