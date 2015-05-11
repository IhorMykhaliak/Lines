using System;
using System.Collections.Generic;
using Lines.GameEngine.Scoring;
using Lines.GameEngine.PathFindingAlgorithm;
using Lines.GameEngine.Enums;
using Lines.GameEngine.BubbleGenerationStrategy;
using Lines.GameEngine.PathFindingAlgorithm.Interface;
using Lines.GameEngine.PathFindingAlgorithm.AStar;

namespace Lines.GameEngine.Logic
{
    public class GameLogic
    {
        #region Private Fields

        private int _difficulty;
        private LineChecker _lineChecker;
        private LinesDestroyer _linesDestroyer;
        private IGenerationStrategy _bubbleGenerationStrategy;
        private IPathFindingStrategy _findPath;

        #endregion

        #region Events

        public event EventHandler DrawEventHandler;
        public event EventHandler ScoreChangedEventHandler;
        public event EventHandler GameOverEventHandler;
        public event EventHandler TurnChangedEventHandler;
        public event EventHandler PathDoesntExistEventHandler;
        public event EventHandler PlayerActionChangingFieldEventHandler;

        // Sound Events
        public event EventHandler PlayMoveSoundEventHandler;
        public event EventHandler PlayScoreSoundEventHandler;

        #endregion

        #region Constructors

        public GameLogic(Field field, IGenerationStrategy generationStrategy, int difficulty)
        {
            this._difficulty = difficulty;
            this.Field = field;
            this.Field.CountEmptyCells();
            this._bubbleGenerationStrategy = generationStrategy;
            this._findPath = new FindPath();
            this.Turn = 0;
            this.Score = 0;
            this.SelectedCell = null;
            this._linesDestroyer = new LinesDestroyer(Field);
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
            if (DrawEventHandler != null)
            {
                DrawEventHandler(this, EventArgs.Empty);
            }
        }

        private void OnScoreChange(object sender, int points)
        {
            Score += points;

            if (ScoreChangedEventHandler != null)
            {
                ScoreChangedEventHandler(this, EventArgs.Empty);
            }
        }

        private void OnGameOver()
        {
            if (GameOverEventHandler != null)
            {
                GameOverEventHandler(this, EventArgs.Empty);
            }
        }

        private void OnTurnChange()
        {
            if (TurnChangedEventHandler != null)
            {
                TurnChangedEventHandler(this, EventArgs.Empty);
            }
        }

        private void OnDestroyLines(object sender, Cell[][] lines)
        {
            OnPlayerActionChangingField();

            _linesDestroyer.DestroyLines(lines);
        }

        private void OnPathDoesntExist()
        {
            if (PathDoesntExistEventHandler != null)
            {
                PathDoesntExistEventHandler(this, EventArgs.Empty);
            }
        }

        private void OnPlayerActionChangingField()
        {
            if (PlayerActionChangingFieldEventHandler != null)
            {
                PlayerActionChangingFieldEventHandler(this, EventArgs.Empty);
            }
        }

        private void OnPlayMoveSoundEventHandler()
        {
            if (PlayMoveSoundEventHandler != null)
            {
                PlayMoveSoundEventHandler(this, EventArgs.Empty);
            }
        }

        private void OnPlayScoreSoundEventHandler()
        {
            if (PlayScoreSoundEventHandler != null)
            {
                PlayScoreSoundEventHandler(this, EventArgs.Empty);
            }
        }

        #endregion

        public void NextTurn(bool generateBubbles)
        {
            Turn++;

            if (generateBubbles)
            {
                RaizeSmallBubbles();

                int generateSmallBubbles = (Field.EmptyCells > _difficulty - 1) ? _difficulty : Field.EmptyCells;

                if (Field.EmptyCells == 0)
                {
                    OnGameOver();
                    return;
                }

                Cell[] smallBubbles = _bubbleGenerationStrategy.GenerateSmallBubbles(Field, generateSmallBubbles);
                Field.PlaceBubbles(smallBubbles);
            }

            OnDraw();
            OnTurnChange();
        }

        public void SelectCell(int row, int col)
        {
            Cell currentCell = Field[row, col];
            if (currentCell == SelectedCell)
            {
                return;
            }

            if (SelectedCell == null)
            {
                if (currentCell.Contain == BubbleSize.Big)
                {
                    SelectBubble(currentCell);
                }
            }
            else
            {
                MoveBubble(currentCell);
            }
        }
        
        public void MoveBubble(Cell currentCell)
        {
            _lineChecker = new LineChecker(Field, currentCell);
            _lineChecker.ScoreChangedEventHandler += OnScoreChange;
            _lineChecker.DestroyLinesEventHandler += OnDestroyLines;

            if (currentCell.Contain == null)
            {
                if (TryMoveBubble(SelectedCell, currentCell))
                {
                    SelectedCell = null;

                    if (!_lineChecker.Check())
                    {
                        OnPlayMoveSoundEventHandler();
                        NextTurn(true);
                    }
                    else
                    {
                        OnPlayScoreSoundEventHandler();
                        Field.EmptyCells += _lineChecker.LineLength;
                        NextTurn(false);
                    }
                }
            }
            else
            {
                if (currentCell.Contain == BubbleSize.Small)
                {
                    Cell CurrentCellDuplicate = new Cell(currentCell);

                    if (TryMoveBubble(SelectedCell, currentCell))
                    {
                        SelectedCell = null;

                        if (!_lineChecker.Check())
                        {
                            OnPlayMoveSoundEventHandler();
                            Cell newBubble = _bubbleGenerationStrategy.GenerateBubble(Field, BubbleSize.Small, CurrentCellDuplicate.Color);
                            Field[newBubble.Row, newBubble.Column].Contain = newBubble.Contain;
                            Field[newBubble.Row, newBubble.Column].Color = newBubble.Color;
                            NextTurn(true);
                        }
                        else
                        {
                            OnPlayScoreSoundEventHandler();
                            Field.EmptyCells += _lineChecker.LineLength;
                            Field[currentCell.Row, currentCell.Column] = CurrentCellDuplicate;
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

        public bool TryMoveBubble(Cell cellFrom, Cell cellTo)
        {
            List<Cell> way;
            if (_findPath.TryGetPath(Field, cellFrom, cellTo, out way))
            {
                OnPlayerActionChangingField();

                cellTo.Contain = cellFrom.Contain;
                cellTo.Color = cellFrom.Color;

                cellFrom.Contain = null;
                cellFrom.Color = null;
                
                return true;
            }

            OnPathDoesntExist();
            return false;
        }
        
        #endregion

        #region Memento Methods

        public GameMemento SaveMemento()
        {
            return new GameMemento(Score, Turn, Field, _difficulty);
        }

        public void RestoreMemento(GameMemento memento)
        {
            this.Field = new Field(memento.Field);
            this.Turn = memento.Turn;
            this.Score = memento.Score;
            this._difficulty = memento.Diffculty;
        }

        #endregion

        #region Helpers

        private void SelectBubble(Cell bubble)
        {
            SelectedCell = bubble;
        }

        private void RaizeSmallBubbles()
        {
            for (int i = 0; i < Field.Height; i++)
            {
                for (int j = 0; j < Field.Width; j++)
                {
                    if (Field[i, j].Contain == BubbleSize.Small)
                    {
                        Field.EmptyCells--;
                        Field[i, j].Contain = BubbleSize.Big;
                        // Check if this bubble creates line
                        _lineChecker = new LineChecker(Field, Field[i, j]);
                        _lineChecker.ScoreChangedEventHandler += OnScoreChange;
                        _lineChecker.DestroyLinesEventHandler += OnDestroyLines;
                        _lineChecker.Check();
                        Field.EmptyCells += _lineChecker.LineLength;
                    }
                }
            }
        }

        #endregion
    }
}
