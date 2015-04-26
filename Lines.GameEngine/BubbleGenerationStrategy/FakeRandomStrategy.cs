using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lines.GameEngine.Enums;

namespace Lines.GameEngine.BubbleGenerationStrategy
{
    public class FakeRandomStrategy : IGenerationStrategy
    {
        public Cell GenerateBubble(Field field, BubbleSize Size, BubbleColor? Color = null)
        {
            return new Cell(field.Height - 2 ,field.Width - 2 , Size, Color);
        }

        public Cell[] GenerateSmallBubbles(Field field, int smallBubbles)
        {
            Cell[] generatedBubbles = new Cell[3];
            generatedBubbles[0] = new Cell(field.Height - 1, field.Width - 1, BubbleSize.Small, BubbleColor.Red);
            generatedBubbles[1] = new Cell(field.Height - 2, field.Width - 1, BubbleSize.Small, BubbleColor.Red);
            generatedBubbles[2] = new Cell(field.Height - 3, field.Width - 1, BubbleSize.Small, BubbleColor.Red);
            
            return generatedBubbles;
        }

        public Cell[] GenerateBigBubbles(Field field, int bigBubbles)
        {
            Cell[] generatedBubbles = new Cell[3];
            generatedBubbles[0] = new Cell(field.Height - 1, field.Width - 2, BubbleSize.Big, BubbleColor.Red);
            generatedBubbles[1] = new Cell(field.Height - 1, field.Width - 3, BubbleSize.Big, BubbleColor.Red);
            generatedBubbles[2] = new Cell(field.Height - 1, field.Width - 4, BubbleSize.Big, BubbleColor.Red);

            return generatedBubbles; ;
        }
    }
}
