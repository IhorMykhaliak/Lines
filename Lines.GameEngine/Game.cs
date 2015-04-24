using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lines.GameEngine.PathFinding_Algorithm;
using Lines.GameEngine.Scoring;
using Lines.GameEngine.Logic;

namespace Lines.GameEngine
{
    public class Game
    {

        #region Private Fields
        private GameLogic _gameLogic;
        private int _score;
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

            _score = 0;

            _gameLogic = new GameLogic(Field);
            _gameLogic.DrawHandler += Draw;
            _gameLogic.UpdateScoreHandler += UpdateScore;
            _gameLogic.GameOverHandler += GameOver;
            _gameLogic.NextTurnHandler += NextTurn;
        }

        public Game()
            : this(10, 10)
        {
        }

        public Game(int size)
            :this(size, size)
        {
        }

        public Game(Field field)
        {
            Field = new Field(field);
            _gameLogic = new GameLogic(Field);
            _gameLogic.DrawHandler += Draw;
            _gameLogic.UpdateScoreHandler += UpdateScore;
            _gameLogic.GameOverHandler += GameOver;
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
                return this._score;
            }
        }

        public Field Field { get; set; }

        public bool IsGameOver { get; set; }

        #endregion

        #region Methods

        #region methods which using events

        private void UpdateScore(int points)
        {
            _score += points;

            if (UpdateScoreHandler != null)
            {
                UpdateScoreHandler();
            }
        }

        private void Draw()
        {
            if (DrawFieldHandler != null)
            {
                DrawFieldHandler();
            }
        }

        private void GameOver()
        {
            IsGameOver = true;
            if (GameOverHandler != null)
            {
                Draw();
                GameOverHandler();
            }
        }

        private void NextTurn()
        {
            if (NextTurnHandler != null)
            {
                NextTurnHandler();
            }
        }

        #endregion

        public void Start()
        {
            Field.EmptyCells = CountEmptyCells();

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

            Draw();
            UpdateScore(0);
            NextTurn();
        }

        public void SelectCell(int row, int col)
        {
            _gameLogic.SelectCell(row, col);
        }

        #endregion

        #region Helpers
        private int CountEmptyCells()
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
    }
}
