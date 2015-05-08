using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lines.GameEngine;
using Lines.GameEngine.Enums;

namespace Lines.ConsoleUI
{
    class Program
    {
        private static Game _game;
        private static int _curX = 0;
        private static int _curY = 0;
        private static GameEngineWrapper _gameUI;
        private static GameInfo _gameInfo;

        // start drawing field on console with margins
        private const int _leftMargin = 10;
        private const int _topMargin = 10;

        static void Main(string[] args)
        {
            try
            {
                _gameInfo = new GameInfo();
                _gameInfo.ShowGameInfo();
                Console.ReadKey();
                Console.Clear();

                _game = new Game(8);
                _gameUI = new GameEngineWrapper(_game, _leftMargin, _topMargin);

                _game.Start();

                ConsoleKeyInfo keyInfo;
                while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape && _game.Status != GameStatus.Completed)
                {
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            MoveCurrentCell(0, -1);
                            break;

                        case ConsoleKey.RightArrow:
                            MoveCurrentCell(1, 0);
                            break;

                        case ConsoleKey.DownArrow:
                            MoveCurrentCell(0, 1);
                            break;

                        case ConsoleKey.LeftArrow:
                            MoveCurrentCell(-1, 0);
                            break;

                        case ConsoleKey.E:
                            _game.Stop();
                            break;

                        case ConsoleKey.Enter:
                            _game.SelectCell(_curY, _curX);
                            break;
                    }
                }
                Console.SetCursorPosition(5, 40);
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        private static void MoveCurrentCell(int x, int y)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(1, 5);
            Console.WriteLine(new string(' ', 35));
            int tempX = _curX;
            int tempY = _curY;
            _curX = ((_curX + x > _game.Field.Width - 1) || (_curX + x < 0)) ? _curX : _curX + x;
            _curY = ((_curY + y > _game.Field.Height - 1) || (_curY + y < 0)) ? _curY : _curY + y;
            if ((tempX != _curX) || (tempY != _curY))
            {
                _gameUI.Draw(_curX, _curY);
            }
        }
    }
}
