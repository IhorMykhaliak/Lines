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

        private const int MAX_UNDO_ALLOWED = 3;

        #endregion

        #region Private Fields

        private readonly int _difficulty;
        private int _allowedUndos;
        private GameLogic _logic;
        private GameMemento _memento;
        private Stack<GameMemento> _undo;
        private GameStatus _gameStatus;
        private IGenerationStrategy _bubbleGenerationStrategy;

        #endregion

        #region Events

        public event EventHandler ScoreChangedEventHandler;
        public event EventHandler DrawEventHandler;
        public event EventHandler GameOverEventHandler;
        public event EventHandler TurnChangedEventHandler;
        public event EventHandler PathDoesntExistEventHandler;
        public event EventHandler PlayMoveSoundEventHandler;
        public event EventHandler PlayScoreSoundEventHandler;
        public event EventHandler PlayCancelSoundEventHandler;
        
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
            _allowedUndos = 0;
            _gameStatus = GameStatus.ReadyToStart;
            _bubbleGenerationStrategy = new RandomStrategy();
            _logic = new GameLogic(new Field(fieldheight, fieldWidth), _bubbleGenerationStrategy, _difficulty);
            _undo = new Stack<GameMemento>();
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
            _allowedUndos = 0;
            _gameStatus = GameStatus.ReadyToStart;
            _bubbleGenerationStrategy = generationStrategy;
            _logic = new GameLogic(new Field(10, 10), _bubbleGenerationStrategy, _difficulty);
            _undo = new Stack<GameMemento>();
            SubscribeGameLogicEvents();
        }

        #endregion

        #region Public Properties

        public int Turn
        {
            get { return this._logic.Turn; }
        }

        public int Score
        {
            get { return this._logic.Score; }
        }

        public GameStatus Status
        {
            get { return _gameStatus; }
        }

        public Field Field
        {
            get { return _logic.Field; }
        }

        public int AllowedStepsBack
        {
            get { return _allowedUndos; }
        }

        public Cell SelectedCell
        {
            get { return _logic.SelectedCell; }
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

        private void OnPlayerActionChangingField(object sender, EventArgs e)
        {
            GameMemento memento = _logic.SaveMemento();
            if (_undo.Count == 0)
            {
                _undo.Push(memento);
            }
            else if (_undo.Peek().Turn != memento.Turn)
            {
                _undo.Push(memento);
            }
            _allowedUndos = (_allowedUndos + 1 > MAX_UNDO_ALLOWED) ? _allowedUndos : ++_allowedUndos;
        }

        private void OnPlayMoveSoundEventHandler(object sender, EventArgs e)
        {
            if (PlayMoveSoundEventHandler != null)
            {
                PlayMoveSoundEventHandler(this, EventArgs.Empty);
            }
        }

        private void OnPlayScoreSoundEventHandler(object sender, EventArgs e)
        {
            if (PlayScoreSoundEventHandler != null)
            {
                PlayScoreSoundEventHandler(this, EventArgs.Empty);
            }
        }

        private void OnPlayCancelSoundEventHandler()
        {
            if (PlayCancelSoundEventHandler != null)
            {
                PlayCancelSoundEventHandler(this, EventArgs.Empty);
            }
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

            OnScoreChange(this, EventArgs.Empty);
            OnTurnChange(this, EventArgs.Empty);
            OnDraw(this, EventArgs.Empty);
        }

        public void ReStart()
        {
            _undo.Clear();
            _allowedUndos = 0;
            _gameStatus = GameStatus.ReadyToStart;
            _logic = new GameLogic(new Field(Field.Height, Field.Width), _bubbleGenerationStrategy, _difficulty);
            SubscribeGameLogicEvents();
            Start();
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

            _logic.SelectCell(row, col);
        }

        public void Undo()
        {
            #region Validation
            if (_gameStatus != GameStatus.InProgress)
            {
                throw new InvalidOperationException("You can cancel move only when game is in progress");
            }
            if (_allowedUndos < 1)
            {
                throw new InvalidOperationException("You can cancel move only up to 3 times in a row");
            }
            if (_undo.Count == 0)
            {
                throw new InvalidOperationException("There is no move to cancel");
            }
            #endregion

            _allowedUndos--;
            int turn = this.Turn;
            _logic.RestoreMemento(_undo.Pop());
            if (turn == Turn)
            {
                _logic.RestoreMemento(_undo.Pop());
            }
            if (_allowedUndos == 0)
            {
                _undo.Clear();
            }
            OnPlayCancelSoundEventHandler();
            OnDraw(null, EventArgs.Empty);
            OnScoreChange(null, EventArgs.Empty);
            OnTurnChange(null, EventArgs.Empty);
        }

        #endregion

        #region Helpers

        private void SubscribeGameLogicEvents()
        {
            _logic.DrawEventHandler += OnDraw;
            _logic.ScoreChangedEventHandler += OnScoreChange;
            _logic.GameOverEventHandler += OnGameOver;
            _logic.TurnChangedEventHandler += OnTurnChange;
            _logic.PathDoesntExistEventHandler += OnPathDoesntExist;
            _logic.PlayerActionChangingFieldEventHandler += OnPlayerActionChangingField;
            _logic.PlayMoveSoundEventHandler += OnPlayMoveSoundEventHandler;
            _logic.PlayScoreSoundEventHandler += OnPlayScoreSoundEventHandler;
        }

        #endregion
    }
}
