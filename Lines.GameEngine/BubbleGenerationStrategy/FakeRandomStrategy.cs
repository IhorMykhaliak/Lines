using System;
using Lines.GameEngine.Enums;

namespace Lines.GameEngine.BubbleGenerationStrategy
{
    public class FakeRandomStrategy : IGenerationStrategy
    {
        public Cell GenerateBubble(Field field, BubbleSize Size, BubbleColor? Color = null)
        {
            return new Cell(0 ,field.Width - 2 , Size, Color);
        }

        public Cell[] GenerateSmallBubbles(Field field, int smallBubbles)
        {
            Cell[] generatedBubbles = new Cell[3];
            generatedBubbles[0] = new Cell(0, field.Width - 1, BubbleSize.Small, BubbleColor.Green);
            generatedBubbles[1] = new Cell(1, field.Width - 1, BubbleSize.Small, BubbleColor.Green);
            generatedBubbles[2] = new Cell(2, field.Width - 1, BubbleSize.Small, BubbleColor.Green);
            
            return generatedBubbles;
        }

        public Cell[] GenerateBigBubbles(Field field, int bigBubbles)
        {
            Cell[] generatedBubbles = new Cell[3];
            generatedBubbles[0] = new Cell(3, field.Width - 1, BubbleSize.Big, BubbleColor.Green);
            generatedBubbles[1] = new Cell(4, field.Width - 1, BubbleSize.Big, BubbleColor.Green);
            generatedBubbles[2] = new Cell(5, field.Width - 1, BubbleSize.Big, BubbleColor.Green);

            return generatedBubbles; ;
        }
    }
}
