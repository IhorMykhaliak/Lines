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
        private int _turn;
        #endregion

        #region Events

        public event Action UpdateScoreLabelHandler;
        public event Action DrawFieldHandler;
        public event Action GameOverHandler;

        #endregion

        #region Constructors

        public Game(int fieldheight, int fieldWidth)
        {
            Field = new Field(fieldheight, fieldWidth);
            _gameLogic = new GameLogic(Field);
            _gameLogic.DrawHandler += Draw;
            _gameLogic.UpdateScoreHandler += UpdateScore;
            _gameLogic.GameOverHandler += GameOver;
            _gameLogic.TurnHandler += NextTurn;
        }

        public Game()
            : this(10, 10)
        {
        }

        public Game(int size)
            :this(size, size)
        {
        }

        #endregion

        #region Public Properties

        public Field Field { get; set; }

        public int Turn
        {
            get
            {
                return this._turn;
            }
        }

        public int Score
        {
            get
            {
                return this._score;
            }
        }

        #endregion

        #region Public Methods

        #region methods which using events

        private void UpdateScore(int points)
        {
            _score += points;

            if (UpdateScoreLabelHandler != null)
            {
                UpdateScoreLabelHandler();
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
            if (GameOverHandler != null)
            {
                Draw();
                GameOverHandler();
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

            UpdateScore(0);
        }

        public void SelectCell(int row, int col)
        {
            _gameLogic.SelectCell(row, col);
        }


        #endregion

        #region Helpers

        public void NextTurn(bool generateBubbles)
        {
            _turn++;
            if (generateBubbles)
            {
                // Small bubbles raize into Big ones
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

                int smallBubbles = (Field.EmptyCells > 2) ? 3 : Field.EmptyCells;

                if (Field.EmptyCells == 0)
                {
                    Settings.Messege = "Game Over";
                    GameOver();
                }

                BubbleGenerator.Generate(Field, smallBubbles);
            }
        }

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
