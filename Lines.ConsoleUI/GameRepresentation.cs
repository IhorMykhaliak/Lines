using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lines.GameEngine;
using Lines.GameEngine.Enums;
using System.Configuration;


namespace Lines.ConsoleUI
{
    public class GameRepresentation
    {
        #region Private Fields
        private Game _game;
        private Field _field;
        private int _leftMargin;
        private int _topMargin;
        private int _curX;
        private int _curY;
        #endregion

        #region Constructors
        public GameRepresentation(Game game,int left, int top)
        {
            this._game = game;
            this._leftMargin = left;
            this._topMargin = top;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Scale = int.Parse(ConfigurationManager.AppSettings["RecomendedConsoleScale"]);
            Console.WindowHeight = int.Parse(ConfigurationManager.AppSettings["Height"]);
            Subscribe();

            DrawInfo();

        }

        #endregion

        #region Properties

        public int Scale { get;private set; }

        #endregion

        #region Public methods

        public void Draw(int curX, int curY)
        {
           this._field = _game.Field;
           this._curX = curX;
           this._curY = curY;
            for (int i = 0; i < _field.Height; i++)
            {
                for (int j = 0; j < _field.Width; j++)
                {
                    DrawCell(i, j);

                    if (_field.Cells[i, j].Contain != null)
                    {
                        DrawBubble(i, j);
                    }
                }
                HighlightCurrentCell();
            }
        }

        #endregion

        #region Helpers

        private void DrawCell(int i, int j)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = Console.ForegroundColor;
            Console.SetCursorPosition(_leftMargin + (Scale + 1) * j, _topMargin + Scale * i);
            Console.Write('\u2588');
            Console.SetCursorPosition(_leftMargin + (Scale + 1) * j + 1, _topMargin + Scale * i);
            Console.Write('\u2588');
            Console.SetCursorPosition(_leftMargin + (Scale + 1) * j, _topMargin + Scale * i + 1);
            Console.Write('\u2588');
            Console.SetCursorPosition(_leftMargin + (Scale + 1) * j + 1, _topMargin + Scale * i + 1);
            Console.Write('\u2588');
            Console.SetCursorPosition(_leftMargin + (Scale + 1) * j + 2, _topMargin + Scale * i);
            Console.Write('\u2588');
            Console.SetCursorPosition(_leftMargin + (Scale + 1) * j + 2, _topMargin + Scale * i + 1);
            Console.Write('\u2588');
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(_leftMargin + (Scale + 1) * j + 1, _topMargin + 3 * i + 2);
            Console.Write('\u2588');
            Console.SetCursorPosition(_leftMargin + (Scale + 1) * j + 2, _topMargin + 3 * i + 2);
            Console.Write('\u2588');
        }

        private void DrawBubble(int i, int j)
        {
            Console.ForegroundColor = GetColor(_field.Cells[i, j].Color) ?? ConsoleColor.White;
            Console.BackgroundColor = Console.ForegroundColor;
            Console.SetCursorPosition(_leftMargin + (Scale + 1) * j, _topMargin + Scale * i);
            Console.Write('\u2588');
            if (_field.Cells[i, j].Contain == BubbleSize.Big)
            {
                Console.SetCursorPosition(_leftMargin + (Scale + 1) * j + 1, _topMargin + Scale * i);
                Console.Write('\u2588');
                Console.SetCursorPosition(_leftMargin + (Scale + 1) * j, _topMargin + Scale * i + 1);
                Console.Write('\u2588');
                Console.SetCursorPosition(_leftMargin + (Scale + 1) * j + 1, _topMargin + Scale * i + 1);
                Console.Write('\u2588');
                Console.SetCursorPosition(_leftMargin + (Scale + 1) * j + 2, _topMargin + Scale * i);
                Console.Write('\u2588');
                Console.SetCursorPosition(_leftMargin + (Scale + 1) * j + 2, _topMargin + Scale * i + 1);
                Console.Write('\u2588');
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(_leftMargin + (Scale + 1) * j + 1, _topMargin + Scale * i + 2);
                Console.Write('\u2588');
                Console.SetCursorPosition(_leftMargin + (Scale + 1) * j + 2, _topMargin + Scale * i + 2);
                Console.Write('\u2588');
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        private void HighlightCurrentCell()
        {
            Console.BackgroundColor = GetColor(_game.Field.Cells[_curY, _curX].Color) ?? ConsoleColor.White;
            //Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(_leftMargin + (Scale + 1) * _curX, _topMargin + Scale * _curY);
            Console.Write('o');
            if (_game.Field.Cells[_curY, _curX].Contain == BubbleSize.Small)
            {
                Console.BackgroundColor = ConsoleColor.White;
            }
            Console.SetCursorPosition(_leftMargin + (Scale + 1) * _curX + 1, _topMargin + Scale * _curY);
            Console.Write('o');
            Console.SetCursorPosition(_leftMargin + (Scale + 1) * _curX, _topMargin + Scale * _curY + 1);
            Console.Write('o');
            Console.SetCursorPosition(_leftMargin + (Scale + 1) * _curX + 1, _topMargin + Scale * _curY + 1);
            Console.Write('o');
            Console.SetCursorPosition(_leftMargin + (Scale + 1) * _curX + 2, _topMargin + Scale * _curY);
            Console.Write('o');
            Console.SetCursorPosition(_leftMargin + (Scale + 1) * _curX + 2, _topMargin + Scale * _curY + 1);
            Console.Write('o');
        }

        private void Subscribe()
        {
            _game.DrawEventHandler += DrawConsole;
            _game.ScoreChangedEventHandler += UpdateScoreLabel;
            _game.GameOverEventHandler += GameOver;
            _game.NextTurnEventHandler += NextTurn;
        }

        private void DrawInfo()
        {
            int top = 1;
            int left = 10;
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(left, top);
            Console.WriteLine("Press E to end game");
        }

        private void NextTurn(object sender, EventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition(50, 10);
            Console.WriteLine("Turn: {0}", _game.Turn);
        }

        private void UpdateScoreLabel(object sender, EventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition(50, 15);
            Console.Write("Score {0}", _game.Score.ToString());

        }

        private void GameOver(object sender, EventArgs e)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(15, 14);
            Console.WriteLine(new string(' ', 30));
            Console.SetCursorPosition(15, 15);
            Console.WriteLine(new string(' ', 30));
            Console.SetCursorPosition(15, 15);
            Console.WriteLine("  Game Over! Your score is {0}", _game.Score);
            Console.SetCursorPosition(15, 16);
            Console.WriteLine(new string(' ', 30));
        }

        private void DrawConsole(object sender, EventArgs e)
        {
            if (_game.Status != GameStatus.Completed)
            {
                Draw(_curX, _curY);
            }
        }

        public ConsoleColor? GetColor(BubbleColor? color)
        {
            switch (color)
            {
                case BubbleColor.Red:
                    return ConsoleColor.Red;
                case BubbleColor.Green:
                    return ConsoleColor.Green;
                case BubbleColor.Blue:
                    return ConsoleColor.Blue;
                case BubbleColor.Yellow:
                    return ConsoleColor.Yellow;
                case BubbleColor.Purple:
                    return ConsoleColor.DarkMagenta;
                case BubbleColor.Pink:
                    return ConsoleColor.DarkBlue;

                default:
                    return null;
            }
        }

        
        #endregion
    }
}
