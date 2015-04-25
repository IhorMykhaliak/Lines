using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lines.GameEngine.Logic;
using Lines.GameEngine.Enums;

namespace Lines.GameEngine.Test.Logic
{
    [TestClass]
    public class DestroyLinesTest
    {
        [TestMethod]
        public void TestDestroyHorizontalLine()
        {
            Field field = new Field(10, 10);

            field.Cells[1, 1].Contain = BubbleSize.Big;
            field.Cells[1, 1].Color = BubbleColor.Red;
            field.Cells[1, 2].Contain = BubbleSize.Big;
            field.Cells[1, 2].Color = BubbleColor.Red;
            field.Cells[1, 3].Contain = BubbleSize.Big;
            field.Cells[1, 3].Color = BubbleColor.Red;
            field.Cells[1, 4].Contain = BubbleSize.Big;
            field.Cells[1, 4].Color = BubbleColor.Red;
            field.Cells[1, 5].Contain = BubbleSize.Big;
            field.Cells[1, 5].Color = BubbleColor.Red;

            Cell[][] lines = new Cell[1][];
            lines[0] = new Cell[5];
            lines[0][0] = field.Cells[1, 1];
            lines[0][1] = field.Cells[1, 2];
            lines[0][2] = field.Cells[1, 3];
            lines[0][3] = field.Cells[1, 4];
            lines[0][4] = field.Cells[1, 5];

            DestroyLines _destroy = new DestroyLines(field);

            _destroy.DestroyLine(lines);

            Assert.AreEqual(field.Cells[1, 1].Contain, null);
            Assert.AreEqual(field.Cells[1, 2].Contain, null);
            Assert.AreEqual(field.Cells[1, 3].Contain, null);
            Assert.AreEqual(field.Cells[1, 4].Contain, null);
            Assert.AreEqual(field.Cells[1, 5].Contain, null);
        }

        [TestMethod]
        public void TestDestroyVerticalLine()
        {
            Field field = new Field(10, 10);

            field.Cells[1, 1].Contain = BubbleSize.Big;
            field.Cells[1, 1].Color = BubbleColor.Red;
            field.Cells[2, 1].Contain = BubbleSize.Big;
            field.Cells[2, 1].Color = BubbleColor.Red;
            field.Cells[3, 1].Contain = BubbleSize.Big;
            field.Cells[3, 1].Color = BubbleColor.Red;
            field.Cells[4, 1].Contain = BubbleSize.Big;
            field.Cells[4, 1].Color = BubbleColor.Red;
            field.Cells[5, 1].Contain = BubbleSize.Big;
            field.Cells[5, 1].Color = BubbleColor.Red;

            DestroyLines _destroy = new DestroyLines(field);

            Cell[][] lines = new Cell[1][];
            lines[0] = new Cell[5];
            lines[0][0] = field.Cells[1, 1];
            lines[0][1] = field.Cells[2, 1];
            lines[0][2] = field.Cells[3, 1];
            lines[0][3] = field.Cells[4, 1];
            lines[0][4] = field.Cells[5, 1];

            _destroy.DestroyLine(lines);

            Assert.AreEqual(field.Cells[1, 1].Contain, null);
            Assert.AreEqual(field.Cells[2, 1].Contain, null);
            Assert.AreEqual(field.Cells[3, 1].Contain, null);
            Assert.AreEqual(field.Cells[4, 1].Contain, null);
            Assert.AreEqual(field.Cells[5, 1].Contain, null);
        }

        [TestMethod]
        public void TestDestroyLeftDiagonallLine()
        {
            Field field = new Field(10, 10);

            field.Cells[1, 1].Contain = BubbleSize.Big;
            field.Cells[1, 1].Color = BubbleColor.Red;
            field.Cells[2, 2].Contain = BubbleSize.Big;
            field.Cells[2, 2].Color = BubbleColor.Red;
            field.Cells[3, 3].Contain = BubbleSize.Big;
            field.Cells[3, 3].Color = BubbleColor.Red;
            field.Cells[4, 4].Contain = BubbleSize.Big;
            field.Cells[4, 4].Color = BubbleColor.Red;
            field.Cells[5, 5].Contain = BubbleSize.Big;
            field.Cells[5, 5].Color = BubbleColor.Red;

            DestroyLines _destroy = new DestroyLines(field);

            Cell[][] lines = new Cell[1][];
            lines[0] = new Cell[5];
            lines[0][0] = field.Cells[1, 1];
            lines[0][1] = field.Cells[2, 2];
            lines[0][2] = field.Cells[3, 3];
            lines[0][3] = field.Cells[4, 4];
            lines[0][4] = field.Cells[5, 5];

            _destroy.DestroyLine(lines);

            Assert.AreEqual(field.Cells[1, 1].Contain, null);
            Assert.AreEqual(field.Cells[2, 2].Contain, null);
            Assert.AreEqual(field.Cells[3, 3].Contain, null);
            Assert.AreEqual(field.Cells[4, 4].Contain, null);
            Assert.AreEqual(field.Cells[5, 5].Contain, null);
        }

        [TestMethod]
        public void TestDestroyRightDiagonalLine()
        {
            Field field = new Field(10, 10);

            field.Cells[5, 1].Contain = BubbleSize.Big;
            field.Cells[5, 1].Color = BubbleColor.Red;
            field.Cells[4, 2].Contain = BubbleSize.Big;
            field.Cells[4, 2].Color = BubbleColor.Red;
            field.Cells[3, 3].Contain = BubbleSize.Big;
            field.Cells[3, 3].Color = BubbleColor.Red;
            field.Cells[2, 4].Contain = BubbleSize.Big;
            field.Cells[2, 4].Color = BubbleColor.Red;
            field.Cells[1, 5].Contain = BubbleSize.Big;
            field.Cells[1, 5].Color = BubbleColor.Red;


            DestroyLines _destroy = new DestroyLines(field);

            Cell[][] lines = new Cell[1][];
            lines[0] = new Cell[5];
            lines[0][0] = field.Cells[5, 1];
            lines[0][1] = field.Cells[4, 2];
            lines[0][2] = field.Cells[3, 3];
            lines[0][3] = field.Cells[2, 4];
            lines[0][4] = field.Cells[1, 5];

            _destroy.DestroyLine(lines);
            Assert.AreEqual(field.Cells[5, 1].Contain, null);
            Assert.AreEqual(field.Cells[4, 2].Contain, null);
            Assert.AreEqual(field.Cells[3, 3].Contain, null);
            Assert.AreEqual(field.Cells[2, 4].Contain, null);
            Assert.AreEqual(field.Cells[1, 5].Contain, null);
        }
    }
}
