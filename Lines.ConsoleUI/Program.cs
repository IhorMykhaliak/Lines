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
        private static GameRepresentation _gameUI;

        // start drawing field on console with margins
        private const int _leftMargin = 10;
        private const int _topMargin = 10;

        static void Main(string[] args)
        {
            try
            {
                GameInfo();
                Console.ReadKey();
                Console.Clear();

                _game = new Game(8);
                _gameUI = new GameRepresentation(_game, _leftMargin, _topMargin);

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
            int tempX = _curX;
            int tempY = _curY;
            _curX = ((_curX + x > _game.Field.Width - 1) || (_curX + x < 0)) ? _curX : _curX + x;
            _curY = ((_curY + y > _game.Field.Height - 1) || (_curY + y < 0)) ? _curY : _curY + y;
            if ((tempX != _curX) || (tempY != _curY))
            {
                _gameUI.Draw(_curX, _curY);
            }
        }

        #region Console design
        // Назва GameInfo() більше підходить для метода, який би повертав string,
        // можна перейменувати у ShowGameInfo()
        private static void GameInfo()
        {
            Info_GameRules();
            // Групування коду на 'абзаци' - логічно пов'язані групи в межах одного методу,
            // тобто, на місці цього коментаря - пустий рядок
            int top = 18;
            int left = 10;
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black; // Групування - тут навпаки зайвий пустий рядок
            Console.SetCursorPosition(left, top);
            Console.WriteLine("Use arrows to control this     rectangle(it's your cursor)");
            Info_DrawCursor();
            // Групування...
            Console.SetCursorPosition(left, top + 2);
            Console.WriteLine("Press ENTER to select(drag) rectangle");
            Console.SetCursorPosition(left, top + 4);
            Console.WriteLine("Then press ENTER to move(drop) rectangle");
            Console.SetCursorPosition(left, top + 6);
            Console.WriteLine("Your goal is to make lines with lenght >=5");
            Console.SetCursorPosition(left, top + 8);
            Console.WriteLine("Let's go!! Press any key to start .............. Good Luck!");

            #region Draw vetrical lines
            top = 2;
            Console.BackgroundColor = ConsoleColor.Black;
            Info_DrawLines(top, ConsoleColor.Red);
            top += 2;
            Info_DrawLines(top, ConsoleColor.Blue);
            top += 2;
            Info_DrawLines(top, ConsoleColor.Green);
            top += 2;
            Info_DrawLines(top, ConsoleColor.Yellow);
            top += 2;
            Info_DrawLines(top, ConsoleColor.DarkBlue);
            top += 2;
            Info_DrawLines(top, ConsoleColor.DarkMagenta);
            top += 2;
            Info_DrawLines(top, ConsoleColor.Red);
            top += 2;
            Info_DrawLines(top, ConsoleColor.Blue);
            top += 2;
            Info_DrawLines(top, ConsoleColor.Green);
            top += 2;
            Info_DrawLines(top, ConsoleColor.Yellow);
            top += 2;
            Info_DrawLines(top, ConsoleColor.DarkBlue);
            top += 2;
            Info_DrawLines(top, ConsoleColor.DarkMagenta);
            top += 2;
            #endregion
        }

        private static void Info_GameRules()
        {
            int top = 2;
            int left = 10;

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition(left, top);
            Console.WriteLine("Rules & Info : ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(left, top + 2);
            Console.WriteLine("It's small rectangle : ");
            Info_DrawSmallRect();
            Console.SetCursorPosition(left, top + 4);
            Console.WriteLine("It's big rectangle : ");
            Info_DrawBigRect();
            Console.SetCursorPosition(left, top + 6);
            Console.WriteLine("After each move small rectangles increase into big ones");
            Console.SetCursorPosition(left, top + 8);
            Console.WriteLine("If you locate 5 big rectangles in line they disappear");
            Console.SetCursorPosition(left, top + 10);
            Console.WriteLine("PLUS small rectangles doesn't increase");
            Console.SetCursorPosition(left, top + 12);
            Console.WriteLine("You can move rectangle only if path between two cells exist");
            Console.SetCursorPosition(left, top + 14);
            Console.WriteLine("Game ends when all field filled with rectangles");
        }

        private static void Info_DrawCursor()
        {
            int top = 17;
            int left = 37;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(left, top);
            Console.Write('o');
            Console.SetCursorPosition(left + 1, top);
            Console.Write('o');
            Console.SetCursorPosition(left, top + 1);
            Console.Write('o');
            Console.SetCursorPosition(left + 1, top + 1);
            Console.Write('o');
            Console.SetCursorPosition(left + 2, top);
            Console.Write('o');
            Console.SetCursorPosition(left + 2, top + 1);
            Console.Write('o');

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

        }

        private static void Info_DrawSmallRect()
        {
            int left = 32;
            int top = 3;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = Console.ForegroundColor;
            Console.SetCursorPosition(left, top);
            Console.Write('\u2588');
            Console.SetCursorPosition(left + 1, top);

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        private static void Info_DrawBigRect()
        {
            int left = 32;
            int top = 5;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = Console.ForegroundColor;
            Console.SetCursorPosition(left, top);
            Console.Write('\u2588');
            Console.SetCursorPosition(left + 1, top);
            Console.Write('\u2588');
            Console.SetCursorPosition(left, top + 1);
            Console.Write('\u2588');
            Console.SetCursorPosition(left + 1, top + 1);
            Console.Write('\u2588');
            Console.SetCursorPosition(left + 2, top);
            Console.Write('\u2588');
            Console.SetCursorPosition(left + 2, top + 1);
            Console.Write('\u2588');
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

        }

        private static void Info_DrawLines(int top, ConsoleColor color)
        {
            int left = 6;
            int right = 70;
            Console.ForegroundColor = color;
            Console.SetCursorPosition(left, top);
            Console.Write('\u2588');
            Console.SetCursorPosition(left + 1, top);
            Console.Write('\u2588');
            Console.SetCursorPosition(left, top + 1);
            Console.Write('\u2588');
            Console.SetCursorPosition(left + 1, top + 1);
            Console.Write('\u2588');
            Console.SetCursorPosition(left + 2, top);
            Console.Write('\u2588');
            Console.SetCursorPosition(left + 2, top + 1);
            Console.Write('\u2588');

            Console.SetCursorPosition(right, top);
            Console.Write('\u2588');
            Console.SetCursorPosition(right + 1, top);
            Console.Write('\u2588');
            Console.SetCursorPosition(right, top + 1);
            Console.Write('\u2588');
            Console.SetCursorPosition(right + 1, top + 1);
            Console.Write('\u2588');
            Console.SetCursorPosition(right + 2, top);
            Console.Write('\u2588');
            Console.SetCursorPosition(right + 2, top + 1);
            Console.Write('\u2588');

        }

        #endregion
    }
}
