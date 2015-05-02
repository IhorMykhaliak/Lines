using System;
using Lines.GameEngine.Enums;

namespace Lines.GameEngine.BubbleGenerationStrategy
{
    public interface IGenerationStrategy
    {
        /*
         * Review GY: імена параметрів методу повинні починатись з маленької літери (Size, Color).
         */
        Cell GenerateBubble(Field field, BubbleSize Size, BubbleColor? Color = null);

        Cell[] GenerateSmallBubbles(Field field, int smallBubble);

        Cell[] GenerateBigBubbles(Field field, int bigBubbles);
    }
}
