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
        private static ConsoleRepresentation _consoleUI;

        private const int leftMargin = 10;
        private const int topMargin = 10;// start drawing field from that point

        static void Main(string[] args)
        {
            try
            {

                GameInfo();
                Console.ReadKey();
                Console.Clear();

                _game = new Game(8);
                _consoleUI = new ConsoleRepresentation(_game, leftMargin, topMargin);

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
            _curX = (_curX + x > _game.Field.Width - 1 || _curX + x < 0) ? _curX : _curX + x;
            _curY = (_curY + y > _game.Field.Height - 1 || _curY + y < 0) ? _curY : _curY + y;
            _consoleUI.Draw(_curX, _curY);
        }

        private static void GameInfo()
        {
            int top = 2;
            int left = 10;
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(left, top);
            Console.WriteLine("Use arrows to control this     rectangle(it's your cursor)");
            Console.SetCursorPosition(left, top +2);
            Console.WriteLine("Press ENTER to select(drag) rectangle");
            Console.SetCursorPosition(left, top+ 4);
            Console.WriteLine("Then press ENTER to move(drop) rectangle");
            Console.SetCursorPosition(left, top+ 6);
            Console.WriteLine("Your goal is to make lines with lenght >=5");
            Console.SetCursorPosition(left, top+ 8);
            Console.WriteLine("Let's go!! Press any key to start");
            Console.SetCursorPosition(left, top + 10);
            Console.WriteLine("Good Luck!");
            Info_DrawCursor();

            top--;
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
        }

        private static void Info_DrawCursor()
        {
            int top = 1;
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
    }
}
