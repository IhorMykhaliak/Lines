using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lines.GameEngine.Enums;

namespace Lines.GameEngine.BubbleGenerationStrategy
{
    public interface IGenerationStrategy
    {
        Cell GenerateBubble(Field field, BubbleSize Size, BubbleColor? Color = null);

        Cell[] GenerateSmallBubbles(Field field, int smallBubble);

        Cell[] GenerateBigBubbles(Field field, int bigBubbles);
    }
}
