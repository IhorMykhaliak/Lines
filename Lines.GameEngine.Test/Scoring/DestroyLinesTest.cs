using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lines.GameEngine.Scoring;

namespace Lines.GameEngine.Test.Scoring
{
    [TestClass]
    public class DestroyLinesTest
    {
        [TestMethod]
        public void TestDestroyHorizontalLine()
        {
            Field field = new Field();

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


            Cell from = field.Cells[1, 1];
            Cell to = field.Cells[1, 5];

            DestroyLines _destroy = new DestroyLines(field);

            _destroy.Destroy(from, to);

            Assert.AreEqual(field.Cells[1, 1].Contain, null);
            Assert.AreEqual(field.Cells[1, 2].Contain, null);
            Assert.AreEqual(field.Cells[1, 3].Contain, null);
            Assert.AreEqual(field.Cells[1, 4].Contain, null);
            Assert.AreEqual(field.Cells[1, 5].Contain, null);
        }

        [TestMethod]
        public void TestDestroyVerticalLine()
        {
            Field field = new Field();

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

            Cell from = field.Cells[1, 1];
            Cell to = field.Cells[5, 1];

            _destroy.Destroy(from, to);

            Assert.AreEqual(field.Cells[1, 1].Contain, null);
            Assert.AreEqual(field.Cells[2, 1].Contain, null);
            Assert.AreEqual(field.Cells[3, 1].Contain, null);
            Assert.AreEqual(field.Cells[4, 1].Contain, null);
            Assert.AreEqual(field.Cells[5, 1].Contain, null);
        }

        [TestMethod]
        public void TestDestroyLeftDiagonallLine()
        {
            Field field = new Field();

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

            Cell from = field.Cells[1, 1];
            Cell to = field.Cells[5, 5];

            _destroy.Destroy(from, to);

            Assert.AreEqual(field.Cells[1, 1].Contain, null);
            Assert.AreEqual(field.Cells[2, 2].Contain, null);
            Assert.AreEqual(field.Cells[3, 3].Contain, null);
            Assert.AreEqual(field.Cells[4, 4].Contain, null);
            Assert.AreEqual(field.Cells[5, 5].Contain, null);
        }

        [TestMethod]
        public void TestDestroyRightDiagonalLine()
        {
            Field field = new Field();

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

            Cell from = field.Cells[1, 5];
            Cell to = field.Cells[5, 1];

            DestroyLines _destroy = new DestroyLines(field);

            _destroy.Destroy(from, to);

            Assert.AreEqual(field.Cells[5, 1].Contain, null);
            Assert.AreEqual(field.Cells[4, 2].Contain, null);
            Assert.AreEqual(field.Cells[3, 3].Contain, null);
            Assert.AreEqual(field.Cells[2, 4].Contain, null);
            Assert.AreEqual(field.Cells[1, 5].Contain, null);
        }
    }
}
