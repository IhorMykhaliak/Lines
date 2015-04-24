using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lines.GameEngine;

namespace Lines.ConsoleUI
{
    public class ConsoleRepresentation
    {
        #region Private Fields
        private int _scale = Settings.RecomededConsoleScale;
        private Field _field;
        private const int x = 10;
        private const int y = 5;
        private int _curX;
        private int _curY;
        #endregion

        #region Constructors
        public ConsoleRepresentation()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        #endregion

        #region Public methods
        public void DrawConsole(Field field, int curX, int curY)
        {
            _field = field;
            _curX = curX;
            _curY = curY;
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
            Console.SetCursorPosition(x + (_scale + 1) * j, y + _scale * i);
            Console.Write('\u2588');
            Console.SetCursorPosition(x + (_scale + 1) * j + 1, y + _scale * i);
            Console.Write('\u2588');
            Console.SetCursorPosition(x + (_scale + 1) * j, y + _scale * i + 1);
            Console.Write('\u2588');
            Console.SetCursorPosition(x + (_scale + 1) * j + 1, y + _scale * i + 1);
            Console.Write('\u2588');
            Console.SetCursorPosition(x + (_scale + 1) * j + 2, y + _scale * i);
            Console.Write('\u2588');
            Console.SetCursorPosition(x + (_scale + 1) * j + 2, y + _scale * i + 1);
            Console.Write('\u2588');
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(x + (_scale + 1) * j + 1, y + 3 * i + 2);
            Console.Write('\u2588');
            Console.SetCursorPosition(x + (_scale + 1) * j + 2, y + 3 * i + 2);
            Console.Write('\u2588');
        }

        private void DrawBubble(int i, int j)
        {
            Console.ForegroundColor = GetColor(_field.Cells[i, j].Color) ?? ConsoleColor.White;
            Console.BackgroundColor = Console.ForegroundColor;
            Console.SetCursorPosition(x + (_scale + 1) * j, y + 3 * i);
            Console.Write('\u2588');
            if (_field.Cells[i, j].Contain == BubbleSize.Big)
            {
                Console.SetCursorPosition(x + (_scale + 1) * j + 1, y + _scale * i);
                Console.Write('\u2588');
                Console.SetCursorPosition(x + (_scale + 1) * j, y + _scale * i + 1);
                Console.Write('\u2588');
                Console.SetCursorPosition(x + (_scale + 1) * j + 1, y + _scale * i + 1);
                Console.Write('\u2588');
                Console.SetCursorPosition(x + (_scale + 1) * j + 2, y + _scale * i);
                Console.Write('\u2588');
                Console.SetCursorPosition(x + (_scale + 1) * j + 2, y + _scale * i + 1);
                Console.Write('\u2588');
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(x + (_scale + 1) * j + 1, y + _scale * i + 2);
                Console.Write('\u2588');
                Console.SetCursorPosition(x + (_scale + 1) * j + 2, y + _scale * i + 2);
                Console.Write('\u2588');
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        private void HighlightCurrentCell()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(x + (_scale + 1) * _curX, y + _scale * _curY);
            Console.Write('o');
            Console.SetCursorPosition(x + (_scale + 1) * _curX + 1, y + _scale * _curY);
            Console.Write('o');
            Console.SetCursorPosition(x + (_scale + 1) * _curX, y + _scale * _curY + 1);
            Console.Write('o');
            Console.SetCursorPosition(x + (_scale + 1) * _curX + 1, y + _scale * _curY + 1);
            Console.Write('o');
            Console.SetCursorPosition(x + (_scale + 1) * _curX + 2, y + _scale * _curY);
            Console.Write('o');
            Console.SetCursorPosition(x + (_scale + 1) * _curX + 2, y + _scale * _curY + 1);
            Console.Write('o');
        }

        private ConsoleColor? GetColor(BubbleColor? color)
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
                    return ConsoleColor.Cyan;

                default:
                    return null;
            }
        }
        #endregion
    }
}
