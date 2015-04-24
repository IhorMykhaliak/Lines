using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lines.GameEngine;

namespace Lines.ConsoleUI
{
    class Program
    {
        private static Game game;
        private static int curX = 0;
        private static int curY = 0;
        private static ConsoleRepresentation console = new ConsoleRepresentation();
        static int scale = Settings.RecomededConsoleScale;

        static void Main(string[] args)
        {
            game = new Game(8);
            ConsoleRepresentation console = new ConsoleRepresentation();
            game.DrawFieldHandler += DrawConsole;
            game.UpdateScoreHandler += UpdateScoreLabel;
            game.GameOverHandler += GameOver;
            game.NextTurnHandler += NextTurn;

            game.Start();
            Console.WindowHeight *= 2;
            //DrawConsole();

            ConsoleKeyInfo keyInfo;
            while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        Move(0, -1);
                        break;

                    case ConsoleKey.RightArrow:
                        Move(1, 0);
                        break;

                    case ConsoleKey.DownArrow:
                        Move(0, 1);
                        break;

                    case ConsoleKey.LeftArrow:
                        Move(-1, 0);
                        break;

                    case ConsoleKey.Enter:
                        game.SelectCell(curY, curX);
                        break;
                }
            }
        }

        private static void NextTurn()
        {
            Console.SetCursorPosition(50, 5);
            Console.WriteLine("Turn: {0}", game.Turn);
        }

        private static void GameOver()
        {
            Console.SetCursorPosition(15, 10);
            Console.WriteLine("Game Over! Your score is {0}", game.Score);
            Console.SetCursorPosition(15, 11);
            Console.WriteLine("Press ESCAPE to leave");
        }

        private static void DrawConsole()
        {
            if (game.IsGameOver)
            {
                GameOver();
            }
            else
            {
                console.DrawConsole(game.Field, curX, curY);
            }
        }

        private static void UpdateScoreLabel()
        {
            Console.SetCursorPosition(50, 10);
            Console.Write("Score {0}", game.Score.ToString());
        }

        public static void Move(int x, int y)
        {
            curX = (curX + x > game.Field.Width - 1 || curX + x < 0) ? curX : curX + x;
            curY = (curY + y > game.Field.Height - 1 || curY + y < 0) ? curY : curY + y;
            DrawConsole();
        }






    }
}
