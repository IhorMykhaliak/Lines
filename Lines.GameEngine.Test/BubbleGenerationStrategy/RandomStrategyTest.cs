using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lines.GameEngine.BubbleGenerationStrategy;
using Lines.GameEngine.Enums;

namespace Lines.GameEngine.Test.BubbleGenerationStrategy
{
    [TestClass]
    public class RandomStrategyTest
    {
        #region Generate one bubbles

        [TestMethod]
        public void TestGenerateBigBubble()
        {
            RandomStrategy generateBubble = new RandomStrategy();
            Field field = new Field(10, 10);

            Cell bubble = generateBubble.GenerateBubble(field, BubbleSize.Big);

            Assert.IsNotNull(bubble);
            Assert.AreEqual(BubbleSize.Big, bubble.ContainedItem);
            Assert.IsNotNull(bubble.Color);
            Assert.IsTrue(bubble.Row >= 0);
            Assert.IsTrue(bubble.Row <= field.Height - 1);
            Assert.IsTrue(bubble.Column >= 0);
            Assert.IsTrue(bubble.Column <= field.Width - 1);
        }

        [TestMethod]
        public void TestGenerateSmallBubble()
        {
            RandomStrategy generateBubble = new RandomStrategy();
            Field field = new Field(10, 10);

            Cell bubble = generateBubble.GenerateBubble(field, BubbleSize.Small);

            Assert.IsNotNull(bubble);
            Assert.AreEqual(BubbleSize.Small, bubble.ContainedItem);
            Assert.IsNotNull(bubble.Color);
            Assert.IsTrue(bubble.Row >= 0);
            Assert.IsTrue(bubble.Row <= field.Height - 1);
            Assert.IsTrue(bubble.Column >= 0);
            Assert.IsTrue(bubble.Column <= field.Width - 1);
        }

        [TestMethod]
        public void TestGenerateBigRedBubble()
        {
            RandomStrategy generateBubble = new RandomStrategy();
            Field field = new Field(10, 10);

            Cell bubble = generateBubble.GenerateBubble(field, BubbleSize.Big, BubbleColor.Red);

            Assert.IsNotNull(bubble);
            Assert.AreEqual(BubbleSize.Big, bubble.ContainedItem);
            Assert.AreEqual(BubbleColor.Red, bubble.Color);
            Assert.IsTrue(bubble.Row >= 0);
            Assert.IsTrue(bubble.Row <= field.Height - 1);
            Assert.IsTrue(bubble.Column >= 0);
            Assert.IsTrue(bubble.Column <= field.Width - 1);
        }

        [TestMethod]
        public void TestGenerateSmallBlueBubble()
        {
            RandomStrategy generateBubble = new RandomStrategy();
            Field field = new Field(10, 10);

            Cell bubble = generateBubble.GenerateBubble(field, BubbleSize.Small, BubbleColor.Blue);

            Assert.IsNotNull(bubble);
            Assert.AreEqual(BubbleSize.Small, bubble.ContainedItem);
            Assert.AreEqual(BubbleColor.Blue, bubble.Color);
            Assert.IsTrue(bubble.Row >= 0);
            Assert.IsTrue(bubble.Row <= field.Height - 1);
            Assert.IsTrue(bubble.Column >= 0);
            Assert.IsTrue(bubble.Column <= field.Width - 1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGenerationFailed()
        {
            RandomStrategy generateBubble = new RandomStrategy();
            Field field = new Field(10, 10);
            for (int i = 0; i < field.Height; i++)
            {
                for (int j = 0; j < field.Width; j++)
                {
                    field[i, j].ContainedItem = BubbleSize.Big;
                    field[i, j].Color = BubbleColor.Red;
                    field.EmptyCells--;
                }
            }


            Cell bubble = generateBubble.GenerateBubble(field, BubbleSize.Small, BubbleColor.Blue);
        }

        #endregion

        #region Generate Big bubbles

        [TestMethod]
        public void TestGenerateThreeBigBubbles()
        {
            RandomStrategy generateBubble = new RandomStrategy();
            Field field = new Field(10, 10);

            Cell[] bubbles = generateBubble.GenerateBigBubbles(field, 3);

            Assert.IsNotNull(bubbles);
            Assert.AreEqual(BubbleSize.Big, bubbles[0].ContainedItem);
            Assert.AreEqual(BubbleSize.Big, bubbles[1].ContainedItem);
            Assert.AreEqual(BubbleSize.Big, bubbles[2].ContainedItem);
            Assert.IsNotNull(bubbles[0].Color);
            Assert.IsNotNull(bubbles[1].Color);
            Assert.IsNotNull(bubbles[2].Color);
            Assert.IsTrue(bubbles[0].Row >= 0 && bubbles[0].Row <= field.Height - 1);
            Assert.IsTrue(bubbles[1].Row >= 0 && bubbles[1].Row <= field.Height - 1);
            Assert.IsTrue(bubbles[1].Row >= 0 && bubbles[1].Row <= field.Height - 1);
            Assert.IsTrue(bubbles[0].Column >= 0 && bubbles[0].Column <= field.Width - 1);
            Assert.IsTrue(bubbles[1].Column >= 0 && bubbles[1].Column <= field.Width - 1);
            Assert.IsTrue(bubbles[2].Column >= 0 && bubbles[2].Column <= field.Width - 1);
        }

        [TestMethod]
        public void TestGenerateBigBubbles_UniqueResult()
        {
            RandomStrategy generateBubble = new RandomStrategy();
            Field field = new Field(7, 7);

            for (int i = 0; i < field.Height; i++)
            {
                for (int j = 0; j < field.Width - 1; j++)
                {
                    field[i, j].ContainedItem = BubbleSize.Big;
                    field[i, j].Color = BubbleColor.Red;
                }
            }
            field.EmptyCells = 7;

            Cell[] bubbles = generateBubble.GenerateBigBubbles(field, 7);

            Assert.IsTrue(bubbles.Any(x => x.Row == 2 && x.Column == 6));
            Assert.IsTrue(bubbles.Any(x => x.Row == 3 && x.Column == 6));
            Assert.IsTrue(bubbles.Any(x => x.Row == 4 && x.Column == 6));
            Assert.IsTrue(bubbles.Any(x => x.Row == 1 && x.Column == 6));
            Assert.IsTrue(bubbles.Any(x => x.Row == 5 && x.Column == 6));
            Assert.IsTrue(bubbles.Any(x => x.Row == 6 && x.Column == 6));
            Assert.IsTrue(bubbles.Any(x => x.Row == 0 && x.Column == 6));
        }
        #endregion

        #region Generate Small bubbles

        [TestMethod]
        public void TestGenerateThreeSmallBubbles()
        {
            RandomStrategy generateBubble = new RandomStrategy();
            Field field = new Field(10, 10);

            Cell[] bubbles = generateBubble.GenerateSmallBubbles(field, 3);

            Assert.IsNotNull(bubbles);
            Assert.AreEqual(BubbleSize.Small, bubbles[0].ContainedItem);
            Assert.AreEqual(BubbleSize.Small, bubbles[1].ContainedItem);
            Assert.AreEqual(BubbleSize.Small, bubbles[2].ContainedItem);
            Assert.IsNotNull(bubbles[0].Color);
            Assert.IsNotNull(bubbles[1].Color);
            Assert.IsNotNull(bubbles[2].Color);
            Assert.IsTrue(bubbles[0].Row >= 0 && bubbles[0].Row <= field.Height - 1);
            Assert.IsTrue(bubbles[1].Row >= 0 && bubbles[1].Row <= field.Height - 1);
            Assert.IsTrue(bubbles[1].Row >= 0 && bubbles[1].Row <= field.Height - 1);
            Assert.IsTrue(bubbles[0].Column >= 0 && bubbles[0].Column <= field.Width - 1);
            Assert.IsTrue(bubbles[1].Column >= 0 && bubbles[1].Column <= field.Width - 1);
            Assert.IsTrue(bubbles[2].Column >= 0 && bubbles[2].Column <= field.Width - 1);
        }

        [TestMethod]
        public void TestGenerateSmallBubbles_UniqueResult()
        {
            RandomStrategy generateBubble = new RandomStrategy();
            Field field = new Field(7, 7);

            for (int i = 0; i < field.Height; i++)
            {
                for (int j = 0; j < field.Width - 1; j++)
                {
                    field[i, j].ContainedItem = BubbleSize.Big;
                    field[i, j].Color = BubbleColor.Red;
                }
            }
            field.EmptyCells = 7;

            Cell[] bubbles = generateBubble.GenerateSmallBubbles(field, 7);

            Assert.IsTrue(bubbles.Any(x => x.Row == 2 && x.Column == 6));
            Assert.IsTrue(bubbles.Any(x => x.Row == 3 && x.Column == 6));
            Assert.IsTrue(bubbles.Any(x => x.Row == 4 && x.Column == 6));
            Assert.IsTrue(bubbles.Any(x => x.Row == 1 && x.Column == 6));
            Assert.IsTrue(bubbles.Any(x => x.Row == 0 && x.Column == 6));
            Assert.IsTrue(bubbles.Any(x => x.Row == 5 && x.Column == 6));
            Assert.IsTrue(bubbles.Any(x => x.Row == 06&& x.Column == 6));
        }

        #endregion
    }
}
