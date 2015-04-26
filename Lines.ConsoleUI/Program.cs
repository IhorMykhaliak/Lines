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
        private static Game game;
        private static int curX = 0;
        private static int curY = 0;
        private static ConsoleRepresentation consoleUI;

        static void Main(string[] args)
        {
            game = new Game(5);
            consoleUI = new ConsoleRepresentation();
            game.DrawFieldHandler += DrawConsole;
            game.UpdateScoreHandler += UpdateScoreLabel;
            game.GameOverHandler += GameOver;
            game.NextTurnHandler += NextTurn;

            game.Start();
            Console.WindowHeight *= 2;

            ConsoleKeyInfo keyInfo;
            while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape && !game.IsGameOver)
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

                    case ConsoleKey.S:
                        game.Stop();
                        break;

                    case ConsoleKey.Enter:
                        game.SelectCell(curY, curX);
                        break;
                }
            }
            Console.SetCursorPosition(5, 40);
        }

        private static void NextTurn(object sender, EventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition(50, 10);
            Console.WriteLine("Turn: {0}", game.Turn);
        }

        private static void UpdateScoreLabel(object sender, EventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition(50, 15);
            Console.Write("Score {0}", game.Score.ToString());
        }

        private static void GameOver(object sender, EventArgs e)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(15, 14);
            Console.WriteLine(new string(' ', 30));
            Console.SetCursorPosition(15, 15);
            Console.WriteLine(new string(' ', 30));
            Console.SetCursorPosition(15, 15);
            Console.WriteLine("  Game Over! Your score is {0}", game.Score);
            Console.SetCursorPosition(15, 16);
            Console.WriteLine(new string(' ', 30));
        }

        private static void DrawConsole(object sender, EventArgs e)
        {
            if (!game.IsGameOver)
            {
                consoleUI.DrawConsole(game.Field, curX, curY);
            }
        }

        private static void MoveCurrentCell(int x, int y)
        {
            curX = (curX + x > game.Field.Width - 1 || curX + x < 0) ? curX : curX + x;
            curY = (curY + y > game.Field.Height - 1 || curY + y < 0) ? curY : curY + y;
            if (!game.IsGameOver)
            {
                consoleUI.DrawConsole(game.Field, curX, curY);
            }
        }
    }
}
