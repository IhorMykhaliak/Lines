using System;
using Lines.GameEngine.Enums;

namespace Lines.GameEngine.BubbleGenerationStrategy
{
    public interface IGenerationStrategy
    {
        Cell GenerateBubble(Field field, BubbleSize size, BubbleColor? color = null);

        Cell[] GenerateSmallBubbles(Field field, int smallBubble);

        Cell[] GenerateBigBubbles(Field field, int bigBubbles);
    }
}
