using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lines.GameEngine.Enums;

namespace Lines.GameEngine.BubbleGenerationStrategy
{
    public class RandomStrategy : IGenerationStrategy
    {
        private Random _random = new Random();

        public Cell GenerateBubble(Field field, BubbleSize Size, BubbleColor? Color = null)
        {
            Cell result;
            int randomRow;
            int randomCol;
            do
            {
                randomRow = _random.Next(0, field.Height);
                randomCol = _random.Next(0, field.Width);
                if (field.Cells[randomRow, randomCol].Contain == null)
                {
                    int randomColor = _random.Next(0, Enum.GetNames(typeof(BubbleColor)).Length);
                    result = new Cell(randomRow, randomCol, Size, Color ?? (BubbleColor)randomColor);
                    return result;
                }
            } while (field.Cells[randomRow, randomCol].Contain != null && field.EmptyCells != 0);

            throw new InvalidOperationException("Field is already full.Generation failed");
        }

        public Cell[] GenerateSmallBubbles(Field field, int smallBubbles)
        {
            Cell[] generatedBubbles = new Cell[smallBubbles];
            while (smallBubbles > 0)
            {
                Cell bubble = GenerateBubble(field, BubbleSize.Small);
                if (IsUnique(generatedBubbles, bubble))
                {
                    generatedBubbles[smallBubbles - 1] = bubble;
                    smallBubbles--;
                }
            }
            return generatedBubbles;
        }

        public Cell[] GenerateBigBubbles(Field field, int bigBubbles)
        {
            Cell[] generatedBubbles = new Cell[bigBubbles];
            while (bigBubbles > 0)
            {
                Cell bubble = GenerateBubble(field, BubbleSize.Big);
                if (IsUnique(generatedBubbles, bubble))
                {
                    generatedBubbles[bigBubbles - 1] = bubble;
                    bigBubbles--;
                }
            }
            return generatedBubbles;
        }

        private bool IsUnique(Cell[] existing, Cell suspect)
        {
            existing = existing.Where(x => x != null).ToArray();
            foreach (var bubble in existing)
            {
                if (bubble.Row == suspect.Row && bubble.Column == suspect.Column)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
