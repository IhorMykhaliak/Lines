using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lines.GameEngine.Test
{
    [TestClass]
    public class GameTest
    {
        #region Field

        [TestMethod]
        public void TestFieldCreation()
        {
            Game game = new Game();

            Assert.IsNotNull(game.Field);
        }

        [TestMethod]
        public void TestFieldSize1()
        {
            Game game = new Game();

            Assert.AreEqual(10, game.Field.Width);
            Assert.AreEqual(10, game.Field.Height);
        }

        [TestMethod]
        public void TestFieldSize2()
        {
            Game game = new Game(6, 8);

            Assert.AreEqual(6, game.Field.Height);
            Assert.AreEqual(8, game.Field.Width);
        }

        [TestMethod]
        public void TestFieldSize3()
        {
            Game game = new Game(6);

            Assert.AreEqual(6, game.Field.Height);
            Assert.AreEqual(6, game.Field.Width);
        }

        #endregion

        #region Start

        [TestMethod]
        public void TestStart()
        {
            Game game = new Game();
            game.Start();

            int smallBubbles = 0;
            int bigBubbles = 0;

            for (int i = 0; i < game.Field.Height; i++)
            {
                for (int j = 0; j < game.Field.Width; j++)
                {
                    if (game.Field.Cells[i, j].Contain == BubbleSize.Big)
                    {
                        bigBubbles++;
                    }
                    if (game.Field.Cells[i, j].Contain == BubbleSize.Small)
                    {
                        smallBubbles++;
                    }
                }
            }

            Assert.AreEqual(smallBubbles, 3);
            Assert.AreEqual(bigBubbles, 3);
        }

        #endregion

        #region Make Line

        [TestMethod]
        public void TestInGameVerticalLineWithSmallBubble()
        {
            Game game = new Game();
            game.Field.Cells[1, 1].Contain = BubbleSize.Big;
            game.Field.Cells[1, 1].Color = BubbleColor.Red;
            game.Field.Cells[1, 2].Contain = BubbleSize.Big;
            game.Field.Cells[1, 2].Color = BubbleColor.Red;
            game.Field.Cells[1, 3].Contain = BubbleSize.Big;
            game.Field.Cells[1, 3].Color = BubbleColor.Red;
            game.Field.Cells[1, 8].Contain = BubbleSize.Big;
            game.Field.Cells[1, 8].Color = BubbleColor.Red;
            game.Field.Cells[1, 4].Contain = BubbleSize.Small;
            game.Field.Cells[1, 4].Color = BubbleColor.Blue;
            game.Field.Cells[1, 5].Contain = BubbleSize.Big;
            game.Field.Cells[1, 5].Color = BubbleColor.Red;

            //gameLogic.Start();

            game.SelectCell(1, 8);
            game.SelectCell(1, 4);

            Assert.AreEqual(game.Field.Cells[1, 1].Contain, null);
            Assert.AreEqual(game.Field.Cells[1, 2].Contain, null);
            Assert.AreEqual(game.Field.Cells[1, 3].Contain, null);
            Assert.AreEqual(game.Field.Cells[1, 4].Contain, BubbleSize.Small);
            Assert.AreEqual(game.Field.Cells[1, 5].Contain, null);
        }

        [TestMethod]
        public void TestInGameDoubleLine()
        {
            Game game = new Game();

            //left diagonal line
            game.Field.Cells[0, 1].Contain = BubbleSize.Big;
            game.Field.Cells[0, 1].Color = BubbleColor.Red;
            game.Field.Cells[2, 2].Contain = BubbleSize.Big;
            game.Field.Cells[2, 2].Color = BubbleColor.Red;
            game.Field.Cells[3, 3].Contain = BubbleSize.Big;
            game.Field.Cells[3, 3].Color = BubbleColor.Red;
            game.Field.Cells[4, 4].Contain = BubbleSize.Big;
            game.Field.Cells[4, 4].Color = BubbleColor.Red;
            game.Field.Cells[5, 5].Contain = BubbleSize.Big;
            game.Field.Cells[5, 5].Color = BubbleColor.Red;
            //gameLogic.+ vertical line
            game.Field.Cells[2, 1].Contain = BubbleSize.Big;
            game.Field.Cells[2, 1].Color = BubbleColor.Red;
            game.Field.Cells[3, 1].Contain = BubbleSize.Big;
            game.Field.Cells[3, 1].Color = BubbleColor.Red;
            game.Field.Cells[4, 1].Contain = BubbleSize.Big;
            game.Field.Cells[4, 1].Color = BubbleColor.Red;
            game.Field.Cells[5, 1].Contain = BubbleSize.Big;
            game.Field.Cells[5, 1].Color = BubbleColor.Red;

            game.SelectCell(0, 1);
            game.SelectCell(1, 1);

            Assert.AreEqual(game.Field.Cells[1, 1].Contain, null);
            Assert.AreEqual(game.Field.Cells[2, 2].Contain, null);
            Assert.AreEqual(game.Field.Cells[3, 3].Contain, null);
            Assert.AreEqual(game.Field.Cells[4, 4].Contain, null);
            Assert.AreEqual(game.Field.Cells[5, 5].Contain, null);
            Assert.AreEqual(game.Field.Cells[2, 1].Contain, null);
            Assert.AreEqual(game.Field.Cells[3, 1].Contain, null);
            Assert.AreEqual(game.Field.Cells[4, 1].Contain, null);
            Assert.AreEqual(game.Field.Cells[5, 1].Contain, null);
        }

        #endregion
    }
}
