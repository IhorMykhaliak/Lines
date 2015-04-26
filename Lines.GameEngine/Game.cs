using System;
using Lines.GameEngine.PathFinding_Algorithm;
using Lines.GameEngine.Scoring;
using Lines.GameEngine.Logic;
using Lines.GameEngine.Enums;
using Lines.GameEngine.BubbleGenerationStrategy;

namespace Lines.GameEngine
{
    public class Game
    {
        #region Private Fields
        private GameLogic _gameLogic;
        private GameStatus _gameStatus;
        private IGenerationStrategy _bubbleGenerationStrategy;
        #endregion

        #region Events

        public event EventHandler UpdateScoreHandler;
        public event EventHandler DrawFieldHandler;
        public event EventHandler GameOverHandler;
        public event EventHandler NextTurnHandler;

        #endregion

        #region Constructors

        public Game(int fieldheight, int fieldWidth)
        {
            Field = new Field(fieldheight, fieldWidth);
            _gameStatus = GameStatus.ReadyToStart;
            _bubbleGenerationStrategy = new RandomStrategy();
            _gameLogic = new GameLogic(Field, _bubbleGenerationStrategy);
            _gameLogic.DrawHandler += OnDraw;
            _gameLogic.UpdateScoreHandler += OnUpdateScore;
            _gameLogic.GameOverHandler += OnGameOver;
            _gameLogic.NextTurnHandler += OnNextTurn;
        }

        public Game()
            : this(10, 10)
        {
        }

        public Game(int size)
            : this(size, size)
        {
        }

        public Game(IGenerationStrategy generationStrategy)
        {
            Field = new Field(10, 10);
            _gameStatus = GameStatus.ReadyToStart;
            _bubbleGenerationStrategy = generationStrategy;
            _gameLogic = new GameLogic(Field, _bubbleGenerationStrategy);
            _gameLogic.DrawHandler += OnDraw;
            _gameLogic.UpdateScoreHandler += OnUpdateScore;
            _gameLogic.GameOverHandler += OnGameOver;
            _gameLogic.NextTurnHandler += OnNextTurn;
        }

        #endregion

        #region Public Properties

        public int Turn
        {
            get
            {
                return this._gameLogic.Turn;
            }
        }

        public int Score
        {
            get
            {
                return this._gameLogic.Score;
            }
        }

        public Field Field { get; private set; }

        public GameStatus Status
        {
            get
            {
                return _gameStatus;
            }
        }

        #endregion

        #region Methods

        #region methods which using events

        private void OnUpdateScore(object sender, EventArgs e)
        {
            if (UpdateScoreHandler != null)
            {
                UpdateScoreHandler(this, EventArgs.Empty);
            }
        }

        private void OnDraw(object sender, EventArgs e)
        {
            if (DrawFieldHandler != null)
            {
                DrawFieldHandler(this, EventArgs.Empty);
            }
        }

        private void OnGameOver(object sender, EventArgs e)
        {
            if (GameOverHandler != null)
            {
                OnUpdateScore(this, EventArgs.Empty);
                OnNextTurn(this, EventArgs.Empty);
                OnDraw(this, EventArgs.Empty);
                GameOverHandler(this, EventArgs.Empty);
            }
            _gameStatus = GameStatus.Completed;
        }

        private void OnNextTurn(object sender, EventArgs e)
        {
            if (NextTurnHandler != null)
            {
                NextTurnHandler(this, EventArgs.Empty);
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
            Field.EmptyCells = _gameLogic.CountEmptyCells();

            if (Field.EmptyCells < 6)
            {
                throw new InvalidOperationException("Can't start game,not enought empty cells");
            }

            int generateBigBubbles = 3;
            int generateSmallBubbles = 3;

            Cell[] bigBubbles = _bubbleGenerationStrategy.GenerateBigBubbles(Field, generateBigBubbles);
            PlaceBubblesOnField(bigBubbles);
            Field.EmptyCells -= generateBigBubbles;

            Cell[] smallBubbles = _bubbleGenerationStrategy.GenerateSmallBubbles(Field, generateSmallBubbles);
            PlaceBubblesOnField(smallBubbles);

            OnUpdateScore(this, EventArgs.Empty);
            OnNextTurn(this, EventArgs.Empty);
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
            if (_gameStatus == GameStatus.InProgress)
            {
                _gameLogic.SelectCell(row, col);
            }
        }

        #endregion

        #region Helpers

        private void PlaceBubblesOnField(Cell[] bubbles)
        {
            foreach (var bubble in bubbles)
            {
                Field.Cells[bubble.Row, bubble.Column].Contain = bubble.Contain;
                Field.Cells[bubble.Row, bubble.Column].Color = bubble.Color;
            }
        }

        #endregion
    }
}
