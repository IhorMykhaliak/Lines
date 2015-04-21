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

        private CheckLines _checkLine;

        #endregion

        #region Events
        public event Action DrawHandler;
        public event Action<int> UpdateScoreHandler;
        public event Action GameOverHandler;
        public event Action<bool> TurnHandler;
        #endregion

        #region Constructors

        public GameLogic(Field field)
        {
            Turn = 0;
            Score = 0;
            Field = field;
            SelectedCell = null;
        }

        #endregion

        #region Public Properties

        public Field Field { get; set; }
        public Cell SelectedCell { get; set; }
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
        }

        private void GameOver()
        {
            if (GameOverHandler != null)
            {
                GameOverHandler();
            }
        }

        private void NextTurn(bool generateBubbles)
        {
            if (TurnHandler != null)
            {
                TurnHandler(generateBubbles);
            }
        }

        #endregion

        

        public void SelectCell(int row, int col)
        {
            Cell currentCell = Field.Cells[row, col];

            if (SelectedCell == null)
            {
                TrySelectBubble(currentCell);
            }
            else
            {
                TryMoveBubble(currentCell);

                Draw();
            }
        }

        public void TrySelectBubble(Cell currentCell)
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

        public void TryMoveBubble(Cell currentCell)
        {
            Cell CurrentCellDuplicate;
            _checkLine = new CheckLines(Field, currentCell);
            _checkLine.UpdateScoreLabelHandler += UpdateScore;

            if (currentCell.Contain == null)
            {
                if (MoveBubble(SelectedCell, currentCell))
                {
                    SelectedCell = null;

                    if (!_checkLine.Check())
                    {
                        NextTurn(true);
                    }
                    else
                    {
                        Field.EmptyCells += _checkLine.LineLength;
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

                    if (MoveBubble(SelectedCell, currentCell))
                    {
                        SelectedCell = null;

                        if (!_checkLine.Check())
                        {
                            BubbleGenerator.GenerateSmallBubble(Field, BubbleSize.Small, CurrentCellDuplicate.Color);
                            NextTurn(true);
                        }
                        else
                        {
                            Field.EmptyCells += _checkLine.LineLength;
                            Field.Cells[currentCell.Row, currentCell.Column] = CurrentCellDuplicate;
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

        public bool MoveBubble(Cell cellFrom, Cell cellTo)
        {
            List<Cell> Way;
            if (FindPath.GetWay(Field, cellFrom, cellTo, out Way))
            {
                cellTo.Contain = cellFrom.Contain;
                cellTo.Color = cellFrom.Color;

                cellFrom.Contain = null;
                cellFrom.Color = null;

                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Helpers

        private void SelectBubble(Cell bubble)
        {
            SelectedCell = bubble;
        }

        #endregion
    }
}
