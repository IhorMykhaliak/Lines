using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lines.GameEngine.Logic;
using Lines.GameEngine.Enums;

namespace Lines.GameEngine.Test.Logic
{
    [TestClass]
    public class GameLogicTest
    {
        #region Select Bubble

        [TestMethod]
        public void TestSelectEmptyCell()
        {
            Field field = new Field(10, 10);
            GameLogic gameLogic = new GameLogic(field);

            gameLogic.SelectCell(1, 8);

            Assert.AreEqual(gameLogic.SelectedCell, null);
        }

        [TestMethod]
        public void TestSelectBubble()
        {
            Field field = new Field(10, 10);
            field.Cells[0, 0].Contain = BubbleSize.Big;
            field.Cells[0, 0].Color = BubbleColor.Red;

            GameLogic gameLogic = new GameLogic(field);

            gameLogic.SelectCell(0, 0);

            Assert.AreEqual(gameLogic.SelectedCell, field.Cells[0, 0]);
        }

        [TestMethod]
        public void TestSelectBubbleTwice()
        {
            Field field = new Field(10, 10);
            field.Cells[0, 0].Contain = BubbleSize.Big;
            field.Cells[0, 0].Color = BubbleColor.Red;
            field.Cells[0, 2].Contain = BubbleSize.Big;
            field.Cells[0, 2].Color = BubbleColor.Red;
            GameLogic gameLogic = new GameLogic(field);

            gameLogic.SelectCell(0, 0);
            gameLogic.SelectCell(0, 2);

            Assert.AreEqual(gameLogic.SelectedCell, field.Cells[0, 2]);
        }


        #endregion

        #region Move Bubble

        [TestMethod]
        public void TestMoveToEmptyCell()
        {
            Field field = new Field(10, 10);
            field.Cells[0, 0].Contain = BubbleSize.Big;
            field.Cells[0, 0].Color = BubbleColor.Red;

            GameLogic gameLogic = new GameLogic(field);

            gameLogic.SelectCell(0, 0);
            gameLogic.SelectCell(0, 5);

            Assert.AreEqual(gameLogic.SelectedCell, null);
            Assert.AreEqual(field.Cells[0, 0].Contain, null);
            Assert.AreEqual(field.Cells[0, 5].Contain, BubbleSize.Big);
            Assert.AreEqual(field.Cells[0, 5].Color, BubbleColor.Red);
        }

        [TestMethod]
        public void TestMoveToEmptyCell_PathLocked()
        {
            Field field = new Field(10, 10);
            field.Cells[0, 0].Contain = BubbleSize.Big;
            field.Cells[0, 0].Color = BubbleColor.Red;
            field.Cells[0, 1].Contain = BubbleSize.Big;
            field.Cells[0, 1].Color = BubbleColor.Red;
            field.Cells[1, 0].Contain = BubbleSize.Big;
            field.Cells[1, 0].Color = BubbleColor.Red;
            GameLogic gameLogic = new GameLogic(field);

            gameLogic.SelectCell(0, 0);
            gameLogic.SelectCell(0, 5);

            Assert.AreEqual(gameLogic.SelectedCell, field.Cells[0, 0]);
            Assert.AreEqual(field.Cells[0, 0].Contain, BubbleSize.Big);
            Assert.AreEqual(field.Cells[0, 0].Color, BubbleColor.Red);
            Assert.AreEqual(field.Cells[0, 5].Contain, null);
        }

        [TestMethod]
        public void TestMoveToCellWithSmallBubble()
        {
            Field field = new Field(10, 10);

            field.Cells[0, 0].Contain = BubbleSize.Big;
            field.Cells[0, 0].Color = BubbleColor.Red;
            field.Cells[0, 5].Contain = BubbleSize.Small;
            field.Cells[0, 5].Color = BubbleColor.Blue;

            GameLogic gameLogic = new GameLogic(field);

            gameLogic.SelectCell(0, 0); //need to use fake bubble generator, sometimes test failes
            gameLogic.SelectCell(0, 5);

            bool smallBlueWasGenerated = false;

            for (int i = 0; i < gameLogic.Field.Height; i++)
            {
                for (int j = 0; j < gameLogic.Field.Width; j++)
                {
                    if (gameLogic.Field.Cells[i, j].Contain == BubbleSize.Big && gameLogic.Field.Cells[i, j].Color == BubbleColor.Blue)
                    {
                        smallBlueWasGenerated = true;
                    }
                }
            }

            Assert.AreEqual(gameLogic.SelectedCell, null);
            Assert.AreEqual(field.Cells[0, 0].Contain, null);
            Assert.AreEqual(field.Cells[0, 5].Contain, BubbleSize.Big);
            Assert.AreEqual(field.Cells[0, 5].Color, BubbleColor.Red);
            Assert.IsTrue(smallBlueWasGenerated);
        }


        #endregion

        #region Make Line

        [TestMethod]
        public void TestVerticalLineWithSmallBubble()
        {
            Field field = new Field(10, 10);
            GameLogic gameLogic = new GameLogic(field);
            gameLogic.Field.Cells[1, 1].Contain = BubbleSize.Big;
            gameLogic.Field.Cells[1, 1].Color = BubbleColor.Red;
            gameLogic.Field.Cells[1, 2].Contain = BubbleSize.Big;
            gameLogic.Field.Cells[1, 2].Color = BubbleColor.Red;
            gameLogic.Field.Cells[1, 3].Contain = BubbleSize.Big;
            gameLogic.Field.Cells[1, 3].Color = BubbleColor.Red;
            gameLogic.Field.Cells[1, 8].Contain = BubbleSize.Big;
            gameLogic.Field.Cells[1, 8].Color = BubbleColor.Red;
            gameLogic.Field.Cells[1, 4].Contain = BubbleSize.Small;
            gameLogic.Field.Cells[1, 4].Color = BubbleColor.Blue;
            gameLogic.Field.Cells[1, 5].Contain = BubbleSize.Big;
            gameLogic.Field.Cells[1, 5].Color = BubbleColor.Red;

            gameLogic.SelectCell(1, 8);
            gameLogic.SelectCell(1, 4);

            Assert.AreEqual(gameLogic.Field.Cells[1, 1].Contain, null);
            Assert.AreEqual(gameLogic.Field.Cells[1, 2].Contain, null);
            Assert.AreEqual(gameLogic.Field.Cells[1, 3].Contain, null);
            Assert.AreEqual(gameLogic.Field.Cells[1, 4].Contain, BubbleSize.Small);
            Assert.AreEqual(gameLogic.Field.Cells[1, 5].Contain, null);
        }

        [TestMethod]
        public void TestInGameDoubleLine()
        {
            Field field = new Field(10, 10);
            GameLogic gameLogic = new GameLogic(field);

            //left diagonal line
            gameLogic.Field.Cells[0, 1].Contain = BubbleSize.Big;
            gameLogic.Field.Cells[0, 1].Color = BubbleColor.Red;
            gameLogic.Field.Cells[2, 2].Contain = BubbleSize.Big;
            gameLogic.Field.Cells[2, 2].Color = BubbleColor.Red;
            gameLogic.Field.Cells[3, 3].Contain = BubbleSize.Big;
            gameLogic.Field.Cells[3, 3].Color = BubbleColor.Red;
            gameLogic.Field.Cells[4, 4].Contain = BubbleSize.Big;
            gameLogic.Field.Cells[4, 4].Color = BubbleColor.Red;
            gameLogic.Field.Cells[5, 5].Contain = BubbleSize.Big;
            gameLogic.Field.Cells[5, 5].Color = BubbleColor.Red;
            //gameLogic.+ vertical line
            gameLogic.Field.Cells[2, 1].Contain = BubbleSize.Big;
            gameLogic.Field.Cells[2, 1].Color = BubbleColor.Red;
            gameLogic.Field.Cells[3, 1].Contain = BubbleSize.Big;
            gameLogic.Field.Cells[3, 1].Color = BubbleColor.Red;
            gameLogic.Field.Cells[4, 1].Contain = BubbleSize.Big;
            gameLogic.Field.Cells[4, 1].Color = BubbleColor.Red;
            gameLogic.Field.Cells[5, 1].Contain = BubbleSize.Big;
            gameLogic.Field.Cells[5, 1].Color = BubbleColor.Red;

            gameLogic.SelectCell(0, 1);
            gameLogic.SelectCell(1, 1);

            Assert.AreEqual(gameLogic.Field.Cells[1, 1].Contain, null);
            Assert.AreEqual(gameLogic.Field.Cells[2, 2].Contain, null);
            Assert.AreEqual(gameLogic.Field.Cells[3, 3].Contain, null);
            Assert.AreEqual(gameLogic.Field.Cells[4, 4].Contain, null);
            Assert.AreEqual(gameLogic.Field.Cells[5, 5].Contain, null);
            Assert.AreEqual(gameLogic.Field.Cells[2, 1].Contain, null);
            Assert.AreEqual(gameLogic.Field.Cells[3, 1].Contain, null);
            Assert.AreEqual(gameLogic.Field.Cells[4, 1].Contain, null);
            Assert.AreEqual(gameLogic.Field.Cells[5, 1].Contain, null);
        }

        #endregion
    }
}
