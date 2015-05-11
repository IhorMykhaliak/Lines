using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lines.GameEngine.Enums;
using Lines.GameEngine.BubbleGenerationStrategy;

namespace Lines.GameEngine.Test
{
    [TestClass]
    public class GameTest
    {
        #region Game properties

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
            Game game = new Game(7, 8, 3);

            Assert.IsNotNull(game.Field);
            Assert.AreEqual(7, game.Field.Height);
            Assert.AreEqual(8, game.Field.Width);
        }

        [TestMethod]
        public void TestFieldSize_2()
        {
            Game game = new Game(7);

            Assert.IsNotNull(game.Field);
            Assert.AreEqual(7, game.Field.Height);
            Assert.AreEqual(7, game.Field.Width);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGameDifficulty_Wrong_1()
        {
            Game game = new Game(6, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGameDifficulty_Wrong_2()
        {
            Game game = new Game(6, 6);
        }
        #endregion

        #region Game Lifecycle

        [TestMethod]
        public void TestUsualLifecycle()
        {
            Game game = new Game(8, 4);
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
                    if (game.Field[i, j].Contain == BubbleSize.Big)
                    {
                        bigBubbles++;
                    }
                    if (game.Field[i, j].Contain == BubbleSize.Small)
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
        public void TestStart_FullField()
        {
            Game game = new Game();
            for (int i = 0; i < game.Field.Height; i++)
            {
                for (int j = 0; j < game.Field.Width; j++)
                {
                    game.Field[i, j].Contain = BubbleSize.Big;
                    game.Field[i, j].Color = BubbleColor.Red;
                }
            }
            game.Start();
        }
        #endregion

        #region Gameplay

        #region Cancel Move

        [TestMethod]
        public void TestCancelMove()
        {
            Game game = new Game(new FakeRandomStrategy());
            game.Start();

            game.SelectCell(3, 9);
            game.SelectCell(0, 0);

            game.CancelMove();

            Assert.AreEqual(game.Field[0, 0].Contain, null);
            Assert.AreEqual(game.Field[0, 0].Color, null);
            Assert.AreEqual(game.Field[3, 9].Contain, BubbleSize.Big);
            Assert.AreEqual(game.Field[3, 9].Color, BubbleColor.Green);
        }

        [TestMethod]
        public void TestCancelMove_1()
        {
            Game game = new Game(new FakeRandomStrategy());
            game.Start();

            game.SelectCell(3, 9);
            game.SelectCell(0, 0);

            game.SelectCell(0, 0);
            game.SelectCell(0, 4);

            game.SelectCell(0, 4);
            game.SelectCell(5, 5);

            game.CancelMove();
            game.CancelMove();
            game.CancelMove();

            Assert.AreEqual(game.Field[0, 0].Contain, null);
            Assert.AreEqual(game.Field[0, 0].Color, null);
            Assert.AreEqual(game.Field[3, 9].Contain, BubbleSize.Big);
            Assert.AreEqual(game.Field[3, 9].Color, BubbleColor.Green);
            Assert.AreEqual(game.AllowedStepsBack, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestCancelMove_Wrong_1()
        {
            Game game = new Game(new FakeRandomStrategy());
            game.Start();

            game.CancelMove();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestCancelMove_Wrong_2()
        {
            Game game = new Game(new FakeRandomStrategy());
            game.Start();
            game.Stop();
            game.CancelMove();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestCancelMove_Wrong_3()
        {
            Game game = new Game(new FakeRandomStrategy());
            game.Start();

            game.SelectCell(3, 9);
            game.SelectCell(0, 0);

            game.SelectCell(0, 0);
            game.SelectCell(0, 4);

            game.SelectCell(0, 4);
            game.SelectCell(5, 5);

            game.SelectCell(5, 5);
            game.SelectCell(1, 1);

            game.CancelMove();
            game.CancelMove();
            game.CancelMove();

            game.CancelMove();
        }

        #endregion

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestSelectCell_Wrong()
        {
            Game game = new Game(new FakeRandomStrategy());
            game.Start();
            game.Stop();

            game.SelectCell(5, 5);
        }


        [TestMethod]
        public void TestInGame_VerticalLine_WithSmallBubble()
        {
            Game game = new Game(new FakeRandomStrategy());
            game.Field[1, 1].Contain = BubbleSize.Big;
            game.Field[1, 1].Color = BubbleColor.Red;
            game.Field[1, 2].Contain = BubbleSize.Big;
            game.Field[1, 2].Color = BubbleColor.Red;
            game.Field[1, 3].Contain = BubbleSize.Big;
            game.Field[1, 3].Color = BubbleColor.Red;
            game.Field[1, 8].Contain = BubbleSize.Big;
            game.Field[1, 8].Color = BubbleColor.Red;
            game.Field[1, 4].Contain = BubbleSize.Small;
            game.Field[1, 4].Color = BubbleColor.Blue;
            game.Field[1, 5].Contain = BubbleSize.Big;
            game.Field[1, 5].Color = BubbleColor.Red;

            game.Start();

            game.SelectCell(1, 8);
            game.SelectCell(1, 4);

            Assert.AreEqual(game.Field[1, 1].Contain, null);
            Assert.AreEqual(game.Field[1, 2].Contain, null);
            Assert.AreEqual(game.Field[1, 3].Contain, null);
            Assert.AreEqual(game.Field[1, 4].Contain, BubbleSize.Small);
            Assert.AreEqual(game.Field[1, 5].Contain, null);
        }

        [TestMethod]
        public void TestInGame_DoubleLine()
        {
            Game game = new Game(new FakeRandomStrategy());
            game.ScoreChangedEventHandler += (s, e) => { return; };
            int previousScore = game.Score;
            int previousTurn = game.Turn;

            //left diagonal line
            game.Field[0, 1].Contain = BubbleSize.Big;
            game.Field[0, 1].Color = BubbleColor.Red;
            game.Field[2, 2].Contain = BubbleSize.Big;
            game.Field[2, 2].Color = BubbleColor.Red;
            game.Field[3, 3].Contain = BubbleSize.Big;
            game.Field[3, 3].Color = BubbleColor.Red;
            game.Field[4, 4].Contain = BubbleSize.Big;
            game.Field[4, 4].Color = BubbleColor.Red;
            game.Field[5, 5].Contain = BubbleSize.Big;
            game.Field[5, 5].Color = BubbleColor.Red;
            //+ vertical line
            game.Field[2, 1].Contain = BubbleSize.Big;
            game.Field[2, 1].Color = BubbleColor.Red;
            game.Field[3, 1].Contain = BubbleSize.Big;
            game.Field[3, 1].Color = BubbleColor.Red;
            game.Field[4, 1].Contain = BubbleSize.Big;
            game.Field[4, 1].Color = BubbleColor.Red;
            game.Field[5, 1].Contain = BubbleSize.Big;
            game.Field[5, 1].Color = BubbleColor.Red;

            game.Start();

            game.SelectCell(0, 1);
            game.SelectCell(1, 1);

            Assert.AreEqual(game.Field[1, 1].Contain, null);
            Assert.AreEqual(game.Field[2, 2].Contain, null);
            Assert.AreEqual(game.Field[3, 3].Contain, null);
            Assert.AreEqual(game.Field[4, 4].Contain, null);
            Assert.AreEqual(game.Field[5, 5].Contain, null);
            Assert.AreEqual(game.Field[2, 1].Contain, null);
            Assert.AreEqual(game.Field[3, 1].Contain, null);
            Assert.AreEqual(game.Field[4, 1].Contain, null);
            Assert.AreEqual(game.Field[5, 1].Contain, null);
            Assert.AreEqual(game.Score, previousScore + 81);
            Assert.AreEqual(game.Turn, previousScore + 1);
        }

        [TestMethod]
        public void TestGameOver()
        {
            Game game = new Game(new FakeRandomStrategy());
            game.GameOverEventHandler += (s, e) => { return; };
            game.TurnChangedEventHandler += (s, e) => { return; };
            game.DrawEventHandler += (s, e) => { return; };

            for (int i = 0; i < game.Field.Height; i++)
            {
                for (int j = 0; j < game.Field.Width - 1; j++)
                {
                    BubbleColor color = (j == 8) ? BubbleColor.Blue : BubbleColor.Red;
                    game.Field[i, j].Contain = BubbleSize.Big;
                    game.Field[i, j].Color = color;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                game.Field[6 + i, 9].Contain = BubbleSize.Big;
                game.Field[6 + i, 9].Color = BubbleColor.Red;
            }
            game.Start();

            game.SelectCell(1, 8);
            game.SelectCell(1, 9);

            Assert.AreEqual(game.Status, GameStatus.Completed);
        }

        #endregion
    }
}
