using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lines.GameEngine.Logic;
using Lines.GameEngine.Enums;

namespace Lines.GameEngine.Test.Logic
{
    [TestClass]
    public class LinesDestroyerTest
    {
        [TestMethod]
        public void TestDestroyHorizontalLine()
        {
            Field field = new Field(10, 10);

            field[1, 1].ContainedItem = BubbleSize.Big;
            field[1, 1].Color = BubbleColor.Red;
            field[1, 2].ContainedItem = BubbleSize.Big;
            field[1, 2].Color = BubbleColor.Red;
            field[1, 3].ContainedItem = BubbleSize.Big;
            field[1, 3].Color = BubbleColor.Red;
            field[1, 4].ContainedItem = BubbleSize.Big;
            field[1, 4].Color = BubbleColor.Red;
            field[1, 5].ContainedItem = BubbleSize.Big;
            field[1, 5].Color = BubbleColor.Red;

            Cell[][] lines = new Cell[1][];
            lines[0] = new Cell[5];
            lines[0][0] = field[1, 1];
            lines[0][1] = field[1, 2];
            lines[0][2] = field[1, 3];
            lines[0][3] = field[1, 4];
            lines[0][4] = field[1, 5];

            LinesDestroyer destroyer = new LinesDestroyer(field);

            destroyer.DestroyLines(lines);

            Assert.AreEqual(field[1, 1].ContainedItem, null);
            Assert.AreEqual(field[1, 2].ContainedItem, null);
            Assert.AreEqual(field[1, 3].ContainedItem, null);
            Assert.AreEqual(field[1, 4].ContainedItem, null);
            Assert.AreEqual(field[1, 5].ContainedItem, null);
        }

        [TestMethod]
        public void TestDestroyVerticalLine()
        {
            Field field = new Field(10, 10);

            field[1, 1].ContainedItem = BubbleSize.Big;
            field[1, 1].Color = BubbleColor.Red;
            field[2, 1].ContainedItem = BubbleSize.Big;
            field[2, 1].Color = BubbleColor.Red;
            field[3, 1].ContainedItem = BubbleSize.Big;
            field[3, 1].Color = BubbleColor.Red;
            field[4, 1].ContainedItem = BubbleSize.Big;
            field[4, 1].Color = BubbleColor.Red;
            field[5, 1].ContainedItem = BubbleSize.Big;
            field[5, 1].Color = BubbleColor.Red;

            LinesDestroyer destroyer = new LinesDestroyer(field);

            Cell[][] lines = new Cell[1][];
            lines[0] = new Cell[5];
            lines[0][0] = field[1, 1];
            lines[0][1] = field[2, 1];
            lines[0][2] = field[3, 1];
            lines[0][3] = field[4, 1];
            lines[0][4] = field[5, 1];

            destroyer.DestroyLines(lines);

            Assert.AreEqual(field[1, 1].ContainedItem, null);
            Assert.AreEqual(field[2, 1].ContainedItem, null);
            Assert.AreEqual(field[3, 1].ContainedItem, null);
            Assert.AreEqual(field[4, 1].ContainedItem, null);
            Assert.AreEqual(field[5, 1].ContainedItem, null);
        }

        [TestMethod]
        public void TestDestroyLeftDiagonallLine()
        {
            Field field = new Field(10, 10);

            field[1, 1].ContainedItem = BubbleSize.Big;
            field[1, 1].Color = BubbleColor.Red;
            field[2, 2].ContainedItem = BubbleSize.Big;
            field[2, 2].Color = BubbleColor.Red;
            field[3, 3].ContainedItem = BubbleSize.Big;
            field[3, 3].Color = BubbleColor.Red;
            field[4, 4].ContainedItem = BubbleSize.Big;
            field[4, 4].Color = BubbleColor.Red;
            field[5, 5].ContainedItem = BubbleSize.Big;
            field[5, 5].Color = BubbleColor.Red;

            LinesDestroyer destroyer = new LinesDestroyer(field);

            Cell[][] lines = new Cell[1][];
            lines[0] = new Cell[5];
            lines[0][0] = field[1, 1];
            lines[0][1] = field[2, 2];
            lines[0][2] = field[3, 3];
            lines[0][3] = field[4, 4];
            lines[0][4] = field[5, 5];

            destroyer.DestroyLines(lines);

            Assert.AreEqual(field[1, 1].ContainedItem, null);
            Assert.AreEqual(field[2, 2].ContainedItem, null);
            Assert.AreEqual(field[3, 3].ContainedItem, null);
            Assert.AreEqual(field[4, 4].ContainedItem, null);
            Assert.AreEqual(field[5, 5].ContainedItem, null);
        }

        [TestMethod]
        public void TestDestroyRightDiagonalLine()
        {
            Field field = new Field(10, 10);

            field[5, 1].ContainedItem = BubbleSize.Big;
            field[5, 1].Color = BubbleColor.Red;
            field[4, 2].ContainedItem = BubbleSize.Big;
            field[4, 2].Color = BubbleColor.Red;
            field[3, 3].ContainedItem = BubbleSize.Big;
            field[3, 3].Color = BubbleColor.Red;
            field[2, 4].ContainedItem = BubbleSize.Big;
            field[2, 4].Color = BubbleColor.Red;
            field[1, 5].ContainedItem = BubbleSize.Big;
            field[1, 5].Color = BubbleColor.Red;


            LinesDestroyer destroyer = new LinesDestroyer(field);

            Cell[][] lines = new Cell[1][];
            lines[0] = new Cell[5];
            lines[0][0] = field[5, 1];
            lines[0][1] = field[4, 2];
            lines[0][2] = field[3, 3];
            lines[0][3] = field[2, 4];
            lines[0][4] = field[1, 5];

            destroyer.DestroyLines(lines);
            Assert.AreEqual(field[5, 1].ContainedItem, null);
            Assert.AreEqual(field[4, 2].ContainedItem, null);
            Assert.AreEqual(field[3, 3].ContainedItem, null);
            Assert.AreEqual(field[2, 4].ContainedItem, null);
            Assert.AreEqual(field[1, 5].ContainedItem, null);
        }
    }
}
