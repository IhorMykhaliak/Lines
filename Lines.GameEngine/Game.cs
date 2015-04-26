using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            _bubbleGenerationStrategy = new FakeRandomStrategy();
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

        public bool IsGameOver
        {
            get
            {
                return _gameStatus == GameStatus.Completed;
            }
        }

        #endregion

        #region Methods

        #region methods which using events

        private void OnUpdateScore()
        {
            if (UpdateScoreHandler != null)
            {
                UpdateScoreHandler(this, EventArgs.Empty);
            }
        }

        private void OnDraw()
        {
            if (DrawFieldHandler != null)
            {
                DrawFieldHandler(this, EventArgs.Empty);
            }
        }

        private void OnGameOver()
        {
            if (GameOverHandler != null)
            {
                OnUpdateScore();
                OnNextTurn();
                OnDraw();
                GameOverHandler(this, EventArgs.Empty);
            }
            _gameStatus = GameStatus.Completed;
        }

        private void OnNextTurn()
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
            foreach (var bubble in bigBubbles)
            {
                Field.Cells[bubble.Row, bubble.Column].Contain = bubble.Contain;
                Field.Cells[bubble.Row, bubble.Column].Color = bubble.Color;
            }
            Field.EmptyCells -= generateBigBubbles;

            Cell[] smallBubbles = _bubbleGenerationStrategy.GenerateSmallBubbles(Field, generateSmallBubbles);
            foreach (var bubble in smallBubbles)
            {
                Field.Cells[bubble.Row, bubble.Column].Contain = bubble.Contain;
                Field.Cells[bubble.Row, bubble.Column].Color = bubble.Color;
            }

            OnUpdateScore();
            OnNextTurn();
            OnDraw();
        }

        public void Stop()
        {
            #region Validation
            if (_gameStatus != GameStatus.InProgress)
            {
                throw new InvalidOperationException("Game was already stopped!");
            }
            #endregion

            OnGameOver();
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

        #endregion
    }
}
