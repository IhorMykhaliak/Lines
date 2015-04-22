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
        private static Game game = new Game();
        private static int x = 0;
        private static int y = 0;
        private static int curX = 0;
        private static int curY = 0;
        private static ConsoleRepresentation console = new ConsoleRepresentation();
        static int scale = Settings.RecomededConsoleScale;
        
        static void Main(string[] args)
        {
            ConsoleRepresentation console = new ConsoleRepresentation();
            game.DrawFieldHandler += DrawConsole;
            game.UpdateScoreLabelHandler += UpdateScoreLabel;

            game.Field.Cells[1, 1].Contain = BubbleSize.Big;
            game.Field.Cells[1, 1].Color = BubbleColor.Red;
            game.Field.Cells[1, 2].Contain = BubbleSize.Big;
            game.Field.Cells[1, 2].Color = BubbleColor.Red;
            game.Field.Cells[1, 3].Contain = BubbleSize.Big;
            game.Field.Cells[1, 3].Color = BubbleColor.Red;
            game.Field.Cells[1, 8].Contain = BubbleSize.Big;
            game.Field.Cells[1, 8].Color = BubbleColor.Red;
            game.Field.Cells[1, 4].Contain = BubbleSize.Small;
            game.Field.Cells[1, 4].Color = BubbleColor.Blue;
            game.Field.Cells[1, 5].Contain = BubbleSize.Big;
            game.Field.Cells[1, 5].Color = BubbleColor.Red;
           
            game.Start();
            Console.WindowHeight *= 2;
            DrawConsole();

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

        private static void DrawConsole()
        {
            console.DrawConsole(game.Field);
            Console.SetCursorPosition(50, 5);
            Console.WriteLine("Turn: {0}", game.Turn);
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
            console.CurX = curX;
            console.CurY = curY;
            console.DrawConsole(game.Field);
        }

        


        
       
    }
}
