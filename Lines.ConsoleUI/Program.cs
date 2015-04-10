using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lines.GameEngine;

namespace LinesForms
{
    class Program
    {
        private static Game game = new Game();
        private static int x = 0;
        private static int y = 0;
        private static int curX = 4;
        private static int curY = 3;

        static void Main(string[] args)
        {
            game.Field.Cells[1, 1].Contain = ContainedItem.Big;
            game.Field.Cells[1, 1].Color = BubbleColor.Red;
            game.Field.Cells[1, 2].Contain = ContainedItem.Big;
            game.Field.Cells[1, 2].Color = BubbleColor.Red;
            game.Field.Cells[1, 3].Contain = ContainedItem.Big;
            game.Field.Cells[1, 3].Color = BubbleColor.Red;
            game.Field.Cells[1, 8].Contain = ContainedItem.Big;
            game.Field.Cells[1, 8].Color = BubbleColor.Red;
            game.Field.Cells[1, 4].Contain = ContainedItem.Small;
            game.Field.Cells[1, 4].Color = BubbleColor.Blue;
            game.Field.Cells[1, 5].Contain = ContainedItem.Big;
            game.Field.Cells[1, 5].Color = BubbleColor.Red;

            game.Start();

            Console.WindowHeight *= 2;
            DrawConsole();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = Console.ForegroundColor;

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
                        game.SelectCell(curX, curY);
                        DrawConsole();
                        break;
                }
            }

        }

        public static void Move(int x, int y)
        {
            curX = (curX + x > Settings.Width - 1 || curX + x < 0) ? curX : curX + x;
            curY = (curY + y > Settings.Height - 1|| curY + y < 0) ? curY : curY + y;
            DrawConsole();
        }

        public static void DrawConsole()
        {
            int scale = Settings.RecomededConsoleScale;
            int radius;

            
            for (int i = 0; i < game.Field.Height; i++)
            {
                for (int j = 0; j < game.Field.Width; j++)
                {
                    // Draw field

                    Console.ForegroundColor = ConsoleColor.Gray;

                    Console.BackgroundColor = Console.ForegroundColor;
                    //Console.BackgroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(x + (scale + 1) * i, y + scale * j);
                    Console.Write('\u2588');
                    Console.SetCursorPosition(x + (scale + 1) * i + 1, y + scale * j);
                    Console.Write('\u2588');
                    Console.SetCursorPosition(x + (scale + 1) * i, y + scale * j + 1);
                    Console.Write('\u2588');
                    Console.SetCursorPosition(x + (scale + 1) * i + 1, y + scale * j + 1);
                    Console.Write('\u2588');
                    Console.SetCursorPosition(x + (scale + 1) * i + 2, y + scale * j);
                    Console.Write('\u2588');
                    Console.SetCursorPosition(x + (scale + 1) * i + 2, y + scale * j + 1);
                    Console.Write('\u2588');
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(x + (scale + 1) * i + 1, y + 3 * j + 2);
                    Console.Write('\u2588');
                    Console.SetCursorPosition(x + (scale + 1) * i + 2, y + 3 * j + 2);
                    Console.Write('\u2588');


                    if (game.Field.Cells[i, j].Contain != null)
                    {
                        radius = (game.Field.Cells[i, j].Contain == ContainedItem.Big) ? 2 : 1;
                        Console.ForegroundColor = GetColor(game.Field.Cells[i, j].Color) ?? ConsoleColor.White;

                        Console.BackgroundColor = Console.ForegroundColor;
                        //Console.BackgroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(x + (scale + 1) * i, y + 3 * j);
                        Console.Write('\u2588');
                        if (game.Field.Cells[i, j].Contain == ContainedItem.Big)
                        {

                            Console.SetCursorPosition(x + (scale + 1) * i + 1, y + scale * j);
                            Console.Write('\u2588');

                            Console.SetCursorPosition(x + (scale + 1) * i, y + scale * j + 1);
                            Console.Write('\u2588');
                            Console.SetCursorPosition(x + (scale + 1) * i + 1, y + scale * j + 1);
                            Console.Write('\u2588');

                            Console.SetCursorPosition(x + (scale + 1) * i + 2, y + scale * j);
                            Console.Write('\u2588');
                            Console.SetCursorPosition(x + (scale + 1) * i + 2, y + scale * j + 1);
                            Console.Write('\u2588');
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.SetCursorPosition(x + (scale + 1) * i + 1, y + scale * j + 2);
                            Console.Write('\u2588');
                            Console.SetCursorPosition(x + (scale + 1) * i + 2, y + scale * j + 2);
                            Console.Write('\u2588');
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                    }
                }

                // Highlight current Cell
            
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(x + (scale + 1) * curX, y + scale * curY);
                Console.Write('o');
                Console.SetCursorPosition(x + (scale + 1) * curX + 1, y + scale * curY);
                Console.Write('o');
                Console.SetCursorPosition(x + (scale + 1) * curX, y + scale * curY + 1);
                Console.Write('o');
                Console.SetCursorPosition(x + (scale + 1) * curX + 1, y + scale * curY + 1);
                Console.Write('o');
                Console.SetCursorPosition(x + (scale + 1) * curX + 2, y + scale * curY);
                Console.Write('o');
                Console.SetCursorPosition(x + (scale + 1) * curX + 2, y + scale * curY + 1);
                Console.Write('o');


            }

            Console.SetCursorPosition(50, 10);
            Console.Write("Score {0}",Settings.Score.ToString());

            Console.SetCursorPosition(50, 5);
            Console.WriteLine("Turn: {0}",game.Turn);

        }

        public static ConsoleColor? GetColor(BubbleColor? color)
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

                default:
                    return null;
            }

        }


    }
}
