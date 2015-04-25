using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lines.GameEngine.Enums;

namespace Lines.GameEngine
{
    public static class BubbleGenerator
    {
        private static Random _random = new Random();

        public static void GenerateSmallBubble(Field Field, BubbleSize Size = BubbleSize.Small, BubbleColor? Color = null)
        {
            int randomRow;
            int randomCell;
            do
            {
                randomRow = _random.Next(0, Field.Height);
                randomCell = _random.Next(0, Field.Width);
                if (Field.Cells[randomRow, randomCell].Contain == null)
                {
                    int randomColor = _random.Next(0, Enum.GetNames(typeof(BubbleColor)).Length);
                    Field.Cells[randomRow, randomCell].Contain = Size;
                    Field.Cells[randomRow, randomCell].Color = Color ?? (BubbleColor)randomColor;
                    return;
                }
            } while (Field.Cells[randomRow, randomCell].Contain != null);

        }

        public static void Generate(Field Field, int smallBubbles)
        {
            while (smallBubbles > 0)
            {
                GenerateSmallBubble(Field);
                smallBubbles--;
            }
        }
    }
}
