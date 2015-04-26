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
            Assert.AreEqual(BubbleSize.Big, bubble.Contain);
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
            Assert.AreEqual(BubbleSize.Small, bubble.Contain);
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
            Assert.AreEqual(BubbleSize.Big, bubble.Contain);
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
            Assert.AreEqual(BubbleSize.Small, bubble.Contain);
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
                    field.Cells[i, j].Contain = BubbleSize.Big;
                    field.Cells[i, j].Color = BubbleColor.Red;
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
            Assert.AreEqual(BubbleSize.Big, bubbles[0].Contain);
            Assert.AreEqual(BubbleSize.Big, bubbles[1].Contain);
            Assert.AreEqual(BubbleSize.Big, bubbles[2].Contain);
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
            Field field = new Field(5, 5);

            for (int i = 0; i < field.Height; i++)
            {
                for (int j = 0; j < field.Width - 1; j++)
                {
                    field.Cells[i, j].Contain = BubbleSize.Big;
                    field.Cells[i, j].Color = BubbleColor.Red;
                }
            }
            field.EmptyCells = 5;

            Cell[] bubbles = generateBubble.GenerateBigBubbles(field, 5);

            Assert.IsTrue(bubbles.Any(x => x.Row == 2 && x.Column == 4));
            Assert.IsTrue(bubbles.Any(x => x.Row == 3 && x.Column == 4));
            Assert.IsTrue(bubbles.Any(x => x.Row == 4 && x.Column == 4));
            Assert.IsTrue(bubbles.Any(x => x.Row == 1 && x.Column == 4));
            Assert.IsTrue(bubbles.Any(x => x.Row == 2 && x.Column == 4));
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
            Assert.AreEqual(BubbleSize.Small, bubbles[0].Contain);
            Assert.AreEqual(BubbleSize.Small, bubbles[1].Contain);
            Assert.AreEqual(BubbleSize.Small, bubbles[2].Contain);
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
            Field field = new Field(5, 5);

            for (int i = 0; i < field.Height; i++)
            {
                for (int j = 0; j < field.Width - 1; j++)
                {
                    field.Cells[i, j].Contain = BubbleSize.Big;
                    field.Cells[i, j].Color = BubbleColor.Red;
                }
            }
            field.EmptyCells = 5;

            Cell[] bubbles = generateBubble.GenerateSmallBubbles(field, 5);

            Assert.IsTrue(bubbles.Any(x => x.Row == 2 && x.Column == 4));
            Assert.IsTrue(bubbles.Any(x => x.Row == 3 && x.Column == 4));
            Assert.IsTrue(bubbles.Any(x => x.Row == 4 && x.Column == 4));
            Assert.IsTrue(bubbles.Any(x => x.Row == 1 && x.Column == 4));
            Assert.IsTrue(bubbles.Any(x => x.Row == 2 && x.Column == 4));
        }

        #endregion
    }
}
