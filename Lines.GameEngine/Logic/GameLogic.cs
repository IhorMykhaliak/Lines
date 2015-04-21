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
        public event Action DrawHandler;
        public event Action<int> UpdateScoreHandler;
        public event Action GameOverHandler;
        #endregion

        #region Constructors

        public GameLogic(Field field)
        {
            EmptyCells = 0;
            Turn = 0;
            Score = 0;
            _field = field;
        }

        #endregion

        #region Public Properties

        public int EmptyCells { get; set; }
        public int Turn { get; set; }
        public int Score { get; set; }

        #endregion

        #region Public Methods

        #region methods which using events

        private void Draw()
        {
            if (DrawHandler != null)
            {
                DrawHandler();
            }
        }

        private void UpdateScore(int points)
        {
            if (UpdateScoreHandler != null)
            {
                UpdateScoreHandler(points);
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
                    GameOver();
                }

                BubbleGenerator.Generate(_field, smallBubbles);
            }
        }

        public void SelectCell(int row, int col)
        {
            Cell currentCell = _field.Cells[row, col];

            if (_selectedCell == null)
            {
                TrySelectBubble(currentCell);
            }
            else
            {
                TryMoveBubble(currentCell);

                Draw();
            }
        }

        private void TrySelectBubble(Cell currentCell)
        {

            if (currentCell.Contain == BubbleSize.Big)
            {
                SelectBubble(currentCell);
            }
            else
            {
                return;
            }
        }

        private void TryMoveBubble(Cell currentCell)
        {
            Cell CurrentCellDuplicate;
            _checkLine = new CheckLines(_field, currentCell);
            _checkLine.UpdateScoreLabelHandler += UpdateScore;

            if (currentCell.Contain == null)
            {
                if (MoveBubble(_selectedCell, currentCell))
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
                if (currentCell.Contain == BubbleSize.Small)
                {
                    CurrentCellDuplicate = new Cell()
                    {
                        Row = currentCell.Row,
                        Column = currentCell.Column,
                        Contain = currentCell.Contain,
                        Color = currentCell.Color
                    };

                    if (MoveBubble(_selectedCell, currentCell))
                    {
                        _selectedCell = null;

                        if (!_checkLine.Check())
                        {
                            BubbleGenerator.GenerateSmallBubble(_field, BubbleSize.Small, CurrentCellDuplicate.Color);
                            NextTurn(true);
                        }
                        else
                        {
                            EmptyCells += _checkLine.LineLength;
                            _field.Cells[currentCell.Row, currentCell.Column] = CurrentCellDuplicate;
                            NextTurn(false);
                        }
                    }
                }
                else
                {
                    SelectBubble(currentCell);
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

        #region Helpers

        private void SelectBubble(Cell bubble)
        {
            _selectedCell = bubble;
        }

        #endregion


        #endregion
    }
}
