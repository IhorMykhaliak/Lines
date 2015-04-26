using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lines.GameEngine.Enums;
using Lines.GameEngine.BubbleGenerationStrategy;

namespace Lines.GameEngine.Test
{
    [TestClass]
    public class GameTest
    {
        #region field

        [TestMethod]
        public void TestFieldCreation()
        {
            Game game = new Game();

            Assert.IsNotNull(game.Field);
            Assert.AreEqual(10, game.Field.Width);
            Assert.AreEqual(10, game.Field.Height);
        }

        [TestMethod]
        public void TestFieldSize_1()
        {
            Game game = new Game(6, 8);

            Assert.IsNotNull(game.Field);
            Assert.AreEqual(6, game.Field.Height);
            Assert.AreEqual(8, game.Field.Width);
        }

        [TestMethod]
        public void TestFieldSize_2()
        {
            Game game = new Game(6);

            Assert.IsNotNull(game.Field);
            Assert.AreEqual(6, game.Field.Height);
            Assert.AreEqual(6, game.Field.Width);
        }

        #endregion

        #region Game Lifecycle

        [TestMethod]
        public void TestUsualLifecycle()
        {
            Game game = new Game();
            Assert.AreEqual(GameStatus.ReadyToStart, game.Status);
            game.Start();
            Assert.AreEqual(GameStatus.InProgress, game.Status);
            game.Stop();
            Assert.AreEqual(GameStatus.Completed, game.Status);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestStart_WrongStatus_1()
        {
            Game game = new Game();            
            game.Start();            
            game.Start();                    
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestStart_WrongStatus_2()
        {
            Game game = new Game();
            game.Start();
            game.Stop();
            game.Start();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestStop_WrongStatus_1()
        {
            Game game = new Game();
            game.Stop();            
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestStop_WrongStatus_2()
        {
            Game game = new Game();
            game.Start();
            game.Stop();
            game.Stop();
        }

        [TestMethod]
        public void TestUsualStart()
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

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestStart_WrongField()
        {
            Game game = new Game();
            for (int i = 0; i < game.Field.Height; i++)
            {
                for (int j = 0; j < game.Field.Width; j++)
                {
                    game.Field.Cells[i, j].Contain = BubbleSize.Big;
                    game.Field.Cells[i, j].Color = BubbleColor.Red;
                }
            }
            game.Start();
        }
        #endregion

        #region Gameplay

        [TestMethod]
        public void TestInGame_VerticalLine_WithSmallBubble()
        {
            Game game = new Game(new FakeRandomStrategy());
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

            game.Start();

            game.SelectCell(1, 8);
            game.SelectCell(1, 4);

            Assert.AreEqual(game.Field.Cells[1, 1].Contain, null);
            Assert.AreEqual(game.Field.Cells[1, 2].Contain, null);
            Assert.AreEqual(game.Field.Cells[1, 3].Contain, null);
            Assert.AreEqual(game.Field.Cells[1, 4].Contain, BubbleSize.Small);
            Assert.AreEqual(game.Field.Cells[1, 5].Contain, null);
        }

        [TestMethod]
        public void TestInGame_DoubleLine()
        {
            Game game = new Game(new FakeRandomStrategy());
            int previousScore = game.Score;
            int previousTurn = game.Turn;

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
            //+ vertical line
            game.Field.Cells[2, 1].Contain = BubbleSize.Big;
            game.Field.Cells[2, 1].Color = BubbleColor.Red;
            game.Field.Cells[3, 1].Contain = BubbleSize.Big;
            game.Field.Cells[3, 1].Color = BubbleColor.Red;
            game.Field.Cells[4, 1].Contain = BubbleSize.Big;
            game.Field.Cells[4, 1].Color = BubbleColor.Red;
            game.Field.Cells[5, 1].Contain = BubbleSize.Big;
            game.Field.Cells[5, 1].Color = BubbleColor.Red;

            game.Start();

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
            Assert.AreEqual(game.Score, previousScore + 81);
            Assert.AreEqual(game.Turn, previousScore + 1);
        }

        [TestMethod]
        public void TestGameOver()
        {
            Game game = new Game(new FakeRandomStrategy());
            game.GameOverHandler += delegate(object sender, EventArgs e) { return; };
            game.NextTurnHandler += delegate(object sender, EventArgs e) { return; };
            game.DrawFieldHandler += delegate(object sender, EventArgs e) { return; };
            game.UpdateScoreHandler += delegate(object sender, EventArgs e) { return; };
            for (int i = 0; i < game.Field.Height; i++)
            {
                for (int j = 0; j < game.Field.Width - 1; j++)
                {
                    BubbleColor color = (j == 8) ? BubbleColor.Blue : BubbleColor.Red;
                    game.Field.Cells[i, j].Contain = BubbleSize.Big;
                    game.Field.Cells[i, j].Color = color;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                game.Field.Cells[6 + i, 9].Contain = BubbleSize.Big;
                game.Field.Cells[6 + i, 9].Color = BubbleColor.Red;
            }
            game.Start();

            game.SelectCell(0, 8);
            game.SelectCell(0, 9);

            Assert.AreEqual(game.Status, GameStatus.Completed);

        }

        #endregion
    }
}
