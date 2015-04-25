using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lines.GameEngine.PathFinding_Algorithm;
using Lines.GameEngine.Scoring;
using Lines.GameEngine.Logic;
using Lines.GameEngine.Enums;

namespace Lines.GameEngine
{
    public class Game
    {

        #region Private Fields
        private GameStatus _gameStatus;
        private GameLogic _gameLogic;
        private DestroyLines _destroyLines;
        #endregion

        #region Events

        public event Action UpdateScoreHandler;
        public event Action DrawFieldHandler;
        public event Action GameOverHandler;
        public event Action NextTurnHandler;

        #endregion

        #region Constructors

        public Game(int fieldheight, int fieldWidth)
        {
            Field = new Field(fieldheight, fieldWidth);
            IsGameOver = false;
            _gameStatus = GameStatus.ReadyToStart;
            _gameLogic = new GameLogic(Field);
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

        public bool IsGameOver { get; set; }

        #endregion

        #region Methods

        #region methods which using events

        private void OnUpdateScore()
        {
            if (UpdateScoreHandler != null)
            {
                UpdateScoreHandler();
            }
        }

        private void OnDraw()
        {
            if (DrawFieldHandler != null)
            {
                DrawFieldHandler();
            }
        }

        private void OnGameOver()
        {
            IsGameOver = true;
            if (GameOverHandler != null)
            {
                OnDraw();
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

            int generateBigBubbles = 3;
            int smallBubbles = 3;

            while (generateBigBubbles != 0)
            {
                BubbleGenerator.GenerateSmallBubble(Field, BubbleSize.Big);
                generateBigBubbles--;
                Field.EmptyCells--;
            }
            while (smallBubbles != 0)
            {
                BubbleGenerator.GenerateSmallBubble(Field);
                smallBubbles--;
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

            _gameStatus = GameStatus.Completed;
            IsGameOver = true;
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
