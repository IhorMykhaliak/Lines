using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lines.GameEngine.Scoring;
using Lines.GameEngine.PathFinding_Algorithm;
using Lines.GameEngine.Enums;

namespace Lines.GameEngine.Logic
{
    public class GameLogic
    {
        #region Private Fields

        private CheckLines _checkLine;
        private DestroyLines _destroyLines;

        #endregion

        #region Events

        public event Action DrawHandler;
        public event Action UpdateScoreHandler;
        public event Action GameOverHandler;
        public event Action NextTurnHandler;

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

        #region Methods

        #region methods which using events

        private void OnDraw()
        {
            if (DrawHandler != null)
            {
                DrawHandler();
            }
        }

        private void OnUpdateScore(int points)
        {
            Score += points;

            if (UpdateScoreHandler != null)
            {
                UpdateScoreHandler();
            }
        }

        private void OnGameOver()
        {
            if (GameOverHandler != null)
            {
                GameOverHandler();
            }
        }

        private void OnNextTurn()
        {
            if (NextTurnHandler != null)
            {
                NextTurnHandler();
            }
        }

        private void OnDestroyLines(Cell[][] lines)
        {
            _destroyLines = new DestroyLines(Field);
            _destroyLines.DestroyLine(lines);
        }

        #endregion

        public void NextTurn(bool generateBubbles)
        {
            if (generateBubbles)
            {
                BubbleRaizing();

                int smallBubbles = (Field.EmptyCells > 2) ? 3 : Field.EmptyCells;

                if (Field.EmptyCells == 0)
                {
                    Settings.Messege = "Game Over";
                    OnGameOver();
                }

                BubbleGenerator.Generate(Field, smallBubbles);
            }

            Turn++;
            OnNextTurn();
        }

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

                OnDraw();
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
            
            _checkLine = new CheckLines(Field, currentCell);
            _checkLine.UpdateScoreHandler += OnUpdateScore;
            _checkLine.DestroyLinesHandel += OnDestroyLines;

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
                    Cell CurrentCellDuplicate = new Cell(currentCell);

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

        public int CountEmptyCells()
        {
            int result = 0;
            for (int i = 0; i < Field.Height; i++)
            {
                for (int j = 0; j < Field.Width; j++)
                {
                    if (Field.Cells[i, j].Contain == null)
                    {
                        result++;
                    }
                }
            }
            return result;
        }

        #endregion

        #region Helpers

        private void SelectBubble(Cell bubble)
        {
            SelectedCell = bubble;
        }

        private void BubbleRaizing()
        {
            for (int i = 0; i < Field.Height; i++)
            {
                for (int j = 0; j < Field.Width; j++)
                {
                    if (Field.Cells[i, j].Contain == BubbleSize.Small)
                    {
                        Field.EmptyCells--;
                        Field.Cells[i, j].Contain = BubbleSize.Big;
                    }
                }
            }
        }

        #endregion
    }
}
