using System;
using System.Collections.Generic;
using Lines.GameEngine.Scoring;
using Lines.GameEngine.Logic;
using Lines.GameEngine.Enums;
using Lines.GameEngine.BubbleGenerationStrategy;

namespace Lines.GameEngine
{
    public class Game
    {
        #region Const fields

        private const int MaxStepsBack = 3;

        #endregion

        #region Private Fields

        private readonly int _difficulty;
        private int _allowedStepsBack;
        private GameLogic _gameLogic;
        private GameMemento _memento;
        private readonly Stack<GameMemento> _stepBack = new Stack<GameMemento>();
        private GameStatus _gameStatus;
        private IGenerationStrategy _bubbleGenerationStrategy;

        #endregion

        #region Events

        public event EventHandler ScoreChangedEventHandler;
        public event EventHandler DrawEventHandler;
        public event EventHandler GameOverEventHandler;
        public event EventHandler TurnChangedEventHandler;
        public event EventHandler PathDoesntExistEventHandler;

        #endregion

        #region Constructors

        public Game(int fieldheight, int fieldWidth, int difficulty)
        {
            #region Validation
            if ((difficulty > 5) || (difficulty < 3))
            {
                throw new InvalidOperationException("Difficulty must be between 3 and 5");
            }
            #endregion

            _difficulty = difficulty;
            _allowedStepsBack = MaxStepsBack;
            _gameStatus = GameStatus.ReadyToStart;
            _bubbleGenerationStrategy = new RandomStrategy();
            _gameLogic = new GameLogic(new Field(fieldheight, fieldWidth), _bubbleGenerationStrategy, _difficulty);
            SubscribeGameLogicEvents();
        }

        public Game()
            : this(10, 10, 3)
        {
        }

        public Game(int size)
            : this(size, size, 3)
        {
        }

        public Game(int size, int difficulty)
            : this(size, size, difficulty)
        {
        }

        public Game(IGenerationStrategy generationStrategy)
        {
            _difficulty = 3;
            _allowedStepsBack = MaxStepsBack;
            _gameStatus = GameStatus.ReadyToStart;
            _bubbleGenerationStrategy = generationStrategy;
            _gameLogic = new GameLogic(new Field(10, 10), _bubbleGenerationStrategy, _difficulty);
            SubscribeGameLogicEvents();
        }

        #endregion

        #region Public Properties

        public int Turn
        {
            get { return this._gameLogic.Turn; }
        }

        public int Score
        {
            get { return this._gameLogic.Score; }
        }

        public GameStatus Status
        {
            get { return _gameStatus; }
        }

        public Field Field
        {
            get { return _gameLogic.Field; }
        }

        public int AllowedStepsBack
        {
            get { return _allowedStepsBack; }
        }

        #endregion

        #region Methods

        #region methods which using events

        private void OnScoreChange(object sender, EventArgs e)
        {
            if (ScoreChangedEventHandler != null)
            {
                ScoreChangedEventHandler(this, EventArgs.Empty);
            }
        }

        private void OnDraw(object sender, EventArgs e)
        {
            if (DrawEventHandler != null)
            {
                DrawEventHandler(this, EventArgs.Empty);
            }
        }

        private void OnGameOver(object sender, EventArgs e)
        {
            _gameLogic.Score *= _difficulty;
            if (GameOverEventHandler != null)
            {
                OnTurnChange(this, EventArgs.Empty);
                OnDraw(this, EventArgs.Empty);
                GameOverEventHandler(this, EventArgs.Empty);
            }
            _gameStatus = GameStatus.Completed;
        }

        private void OnTurnChange(object sender, EventArgs e)
        {
            if (TurnChangedEventHandler != null)
            {
                TurnChangedEventHandler(this, EventArgs.Empty);
            }
        }

        private void OnPathDoesntExist(object sender, EventArgs e)
        {
            if (PathDoesntExistEventHandler != null)
            {
                PathDoesntExistEventHandler(this, EventArgs.Empty);
            }
        }

        private void SaveMemento(object sender, EventArgs e)
        {
            GameMemento memento = _gameLogic.SaveMemento();
            if (_stepBack.Count == 0)
            {
                _stepBack.Push(memento);
            }
            else if (_stepBack.Peek().Turn != memento.Turn)
            {
                _stepBack.Push(memento);
            }
            _allowedStepsBack = (_allowedStepsBack + 1 > 3) ? _allowedStepsBack : ++_allowedStepsBack;
        }

        #endregion

        public void Start()
        {
            #region Validation
            if (_gameStatus != GameStatus.ReadyToStart)
            {
                throw new InvalidOperationException("Only game with status 'ReadyToStart' can be started");
            }
            #endregion

            _gameStatus = GameStatus.InProgress;
            Field.CountEmptyCells();

            if (Field.EmptyCells < 2 * _difficulty)
            {
                throw new InvalidOperationException("Can't start game! Not enought empty cells");
            }

            Cell[] bigBubbles = _bubbleGenerationStrategy.GenerateBigBubbles(Field, _difficulty);
            Field.PlaceBubbles(bigBubbles);
            Field.EmptyCells -= _difficulty;

            Cell[] smallBubbles = _bubbleGenerationStrategy.GenerateSmallBubbles(Field, _difficulty);
            Field.PlaceBubbles(smallBubbles);

            OnDraw(this, EventArgs.Empty);
        }

        public void Stop()
        {
            #region Validation
            if (_gameStatus != GameStatus.InProgress)
            {
                throw new InvalidOperationException("Game was already stopped!");
            }
            #endregion

            OnGameOver(this, EventArgs.Empty);
        }

        public void SelectCell(int row, int col)
        {
            #region Validation
            if (_gameStatus != GameStatus.InProgress)
            {
                throw new InvalidOperationException("You can select cell only when game is in progress");
            }
            #endregion

            _gameLogic.SelectCell(row, col);
        }

        public void CancelMove()
        {
            #region Validation
            if (_gameStatus != GameStatus.InProgress)
            {
                throw new InvalidOperationException("You can cancel move only when game is in progress");
            }
            if (_allowedStepsBack < 1)
            {
                throw new InvalidOperationException("You can cancel move only up to 3 times in a row");
            }
            if (_stepBack.Count == 0)
            {
                throw new InvalidOperationException("There is no move to cancel");
            }
            #endregion

            _allowedStepsBack--;
            int turn = this.Turn;
            _gameLogic.RestoreMemento(_stepBack.Pop());
            if (turn == Turn)
            {
                _gameLogic.RestoreMemento(_stepBack.Pop());
            }
            if (_allowedStepsBack == 0)
            {
                _stepBack.Clear();
            }
            OnDraw(null, EventArgs.Empty);
            OnScoreChange(null, EventArgs.Empty);
            OnTurnChange(null, EventArgs.Empty);
        }

        #endregion

        #region Helpers

        private void SubscribeGameLogicEvents()
        {
            _gameLogic.DrawEventHandler += OnDraw;
            _gameLogic.ScoreChangedEventHandler += OnScoreChange;
            _gameLogic.GameOverEventHandler += OnGameOver;
            _gameLogic.TurnChangedEventHandler += OnTurnChange;
            _gameLogic.PathDoesntExistEventHandler += OnPathDoesntExist;
            _gameLogic.PlayerActionChangingFieldEventHandler += SaveMemento;
        }

        #endregion
    }
}
