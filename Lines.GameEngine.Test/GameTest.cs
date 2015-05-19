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
                    if (game.Field[i, j].ContainedItem == BubbleSize.Big)
                    {
                        bigBubbles++;
                    }
                    if (game.Field[i, j].ContainedItem == BubbleSize.Small)
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
                    game.Field[i, j].ContainedItem = BubbleSize.Big;
                    game.Field[i, j].Color = BubbleColor.Red;
                }
            }
            game.Start();
        }

        [TestMethod]
        public void TestRestart()
        {
            Game game = new Game();
            game.Start();
            game.Stop();
            game.ReStart();

            Assert.AreEqual(game.Status, GameStatus.InProgress);
            Assert.AreEqual(game.AllowedStepsBack, 0);
            Assert.AreEqual(game.Turn, 0);
            Assert.AreEqual(game.Score, 0);
        }
        #endregion

        #region Gameplay

        #region Undo

        [TestMethod]
        public void TestUndo()
        {
            Game game = new Game(new FakeRandomStrategy());
            game.Start();

            game.SelectCell(3, 9);
            game.SelectCell(0, 0);

            game.Undo();

            Assert.AreEqual(game.Field[0, 0].ContainedItem, null);
            Assert.AreEqual(game.Field[0, 0].Color, null);
            Assert.AreEqual(game.Field[3, 9].ContainedItem, BubbleSize.Big);
            Assert.AreEqual(game.Field[3, 9].Color, BubbleColor.Green);
        }

        [TestMethod]
        public void TestUndo_1()
        {
            Game game = new Game(new FakeRandomStrategy());
            game.PlayCancelSoundEventHandler += (s, e) => { return; };
            game.Start();

            game.SelectCell(3, 9);
            game.SelectCell(0, 0);

            game.SelectCell(0, 0);
            game.SelectCell(0, 4);

            game.SelectCell(0, 4);
            game.SelectCell(5, 5);

            game.Undo();
            game.Undo();
            game.Undo();

            Assert.AreEqual(game.Field[0, 0].ContainedItem, null);
            Assert.AreEqual(game.Field[0, 0].Color, null);
            Assert.AreEqual(game.Field[3, 9].ContainedItem, BubbleSize.Big);
            Assert.AreEqual(game.Field[3, 9].Color, BubbleColor.Green);
            Assert.AreEqual(game.AllowedStepsBack, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestUndo_Wrong_1()
        {
            Game game = new Game(new FakeRandomStrategy());
            game.Start();

            game.Undo();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestUndo_Wrong_2()
        {
            Game game = new Game(new FakeRandomStrategy());
            game.Start();
            game.Stop();
            game.Undo();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestUndo_Wrong_3()
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

            game.Undo();
            game.Undo();
            game.Undo();

            game.Undo();
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
        public void TestPathDoesntExist()
        {
            Game game = new Game(new FakeRandomStrategy());
            game.PathDoesntExistEventHandler += (s, e) => { return; };
            game.Field[0, 1].ContainedItem = BubbleSize.Big;
            game.Field[0, 1].Color = BubbleColor.Red;
            game.Field[1, 0].ContainedItem = BubbleSize.Big;
            game.Field[1, 0].Color = BubbleColor.Red;
            game.Field[3, 3].ContainedItem = BubbleSize.Big;
            game.Field[3, 3].Color = BubbleColor.Red;

            game.Start();

            game.SelectCell(3, 3);
            game.SelectCell(0, 0);

            Assert.AreEqual(game.Field[3, 3], game.SelectedCell);
            Assert.AreEqual(game.Field[0, 0].ContainedItem, null);
            Assert.AreEqual(game.Field[3, 3].ContainedItem, BubbleSize.Big);
        }

        [TestMethod]
        public void TestInGame_VerticalLine_WithSmallBubble()
        {
            Game game = new Game(new FakeRandomStrategy());
            game.Field[1, 1].ContainedItem = BubbleSize.Big;
            game.Field[1, 1].Color = BubbleColor.Red;
            game.Field[1, 2].ContainedItem = BubbleSize.Big;
            game.Field[1, 2].Color = BubbleColor.Red;
            game.Field[1, 3].ContainedItem = BubbleSize.Big;
            game.Field[1, 3].Color = BubbleColor.Red;
            game.Field[1, 8].ContainedItem = BubbleSize.Big;
            game.Field[1, 8].Color = BubbleColor.Red;
            game.Field[1, 4].ContainedItem = BubbleSize.Small;
            game.Field[1, 4].Color = BubbleColor.Blue;
            game.Field[1, 5].ContainedItem = BubbleSize.Big;
            game.Field[1, 5].Color = BubbleColor.Red;

            game.Start();

            game.SelectCell(1, 8);
            game.SelectCell(1, 4);

            Assert.AreEqual(game.Field[1, 1].ContainedItem, null);
            Assert.AreEqual(game.Field[1, 2].ContainedItem, null);
            Assert.AreEqual(game.Field[1, 3].ContainedItem, null);
            Assert.AreEqual(game.Field[1, 4].ContainedItem, BubbleSize.Small);
            Assert.AreEqual(game.Field[1, 5].ContainedItem, null);
        }

        [TestMethod]
        public void TestInGame_DoubleLine()
        {
            Game game = new Game(new FakeRandomStrategy());
            game.ScoreChangedEventHandler += (s, e) => { return; };
            game.PlayScoreSoundEventHandler += (s, e) => { return; };
            int previousScore = game.Score;
            int previousTurn = game.Turn;

            //left diagonal line
            game.Field[0, 1].ContainedItem = BubbleSize.Big;
            game.Field[0, 1].Color = BubbleColor.Red;
            game.Field[2, 2].ContainedItem = BubbleSize.Big;
            game.Field[2, 2].Color = BubbleColor.Red;
            game.Field[3, 3].ContainedItem = BubbleSize.Big;
            game.Field[3, 3].Color = BubbleColor.Red;
            game.Field[4, 4].ContainedItem = BubbleSize.Big;
            game.Field[4, 4].Color = BubbleColor.Red;
            game.Field[5, 5].ContainedItem = BubbleSize.Big;
            game.Field[5, 5].Color = BubbleColor.Red;
            //+ vertical line
            game.Field[2, 1].ContainedItem = BubbleSize.Big;
            game.Field[2, 1].Color = BubbleColor.Red;
            game.Field[3, 1].ContainedItem = BubbleSize.Big;
            game.Field[3, 1].Color = BubbleColor.Red;
            game.Field[4, 1].ContainedItem = BubbleSize.Big;
            game.Field[4, 1].Color = BubbleColor.Red;
            game.Field[5, 1].ContainedItem = BubbleSize.Big;
            game.Field[5, 1].Color = BubbleColor.Red;

            game.Start();

            game.SelectCell(0, 1);
            game.SelectCell(1, 1);

            Assert.AreEqual(game.Field[1, 1].ContainedItem, null);
            Assert.AreEqual(game.Field[2, 2].ContainedItem, null);
            Assert.AreEqual(game.Field[3, 3].ContainedItem, null);
            Assert.AreEqual(game.Field[4, 4].ContainedItem, null);
            Assert.AreEqual(game.Field[5, 5].ContainedItem, null);
            Assert.AreEqual(game.Field[2, 1].ContainedItem, null);
            Assert.AreEqual(game.Field[3, 1].ContainedItem, null);
            Assert.AreEqual(game.Field[4, 1].ContainedItem, null);
            Assert.AreEqual(game.Field[5, 1].ContainedItem, null);
            Assert.AreEqual(game.Score, previousScore + 924);
            Assert.AreEqual(game.Turn, previousTurn + 1);
        }

        [TestMethod]
        public void TestGameOver()
        {
            Game game = new Game(new FakeRandomStrategy());
            game.PlayMoveSoundEventHandler += (s, e) => { return; };
            game.GameOverEventHandler += (s, e) => { return; };
            game.TurnChangedEventHandler += (s, e) => { return; };
            game.DrawEventHandler += (s, e) => { return; };

            for (int i = 0; i < game.Field.Height; i++)
            {
                for (int j = 0; j < game.Field.Width - 1; j++)
                {
                    BubbleColor color = (j == 8) ? BubbleColor.Blue : BubbleColor.Red;
                    game.Field[i, j].ContainedItem = BubbleSize.Big;
                    game.Field[i, j].Color = color;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                game.Field[6 + i, 9].ContainedItem = BubbleSize.Big;
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
