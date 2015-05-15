using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lines.GameEngine.Scoring;
using Lines.GameEngine.Enums;

namespace Lines.GameEngine.Test.Scoring
{
    [TestClass]
    public class CheckLinesTest
    {
        [TestMethod]
        public void TestHorizontalLine()
        {
            Field field = new Field(10, 10);
            int length;
            Cell[] lineElements;

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

            LineChecker lineChecker = new LineChecker(field, field[1, 3]);

            Assert.IsTrue(lineChecker.HorizontalLine(out length, out lineElements));
            Assert.AreEqual(length, 5);
            Assert.AreSame(lineElements[2], field[1, 1]);
            Assert.AreSame(lineElements[4], field[1, 5]);
        }
        
        [TestMethod]
        public void TestVerticalLine()
        {
            Field field = new Field(10, 10);
            int length;
            Cell[] lineElements;

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

            LineChecker lineChecker = new LineChecker(field, field[2, 1]);

            Assert.IsTrue(lineChecker.VerticalLine(out length, out lineElements));
            Assert.AreEqual(length, 5);
            Assert.AreSame(lineElements[1], field[1, 1]);
            Assert.AreSame(lineElements[4], field[5, 1]);
        }

        [TestMethod]
        public void TestLeftDiagonallLine()
        {
            Field field = new Field(10, 10);
            int length;
            Cell[] lineElements;


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

            LineChecker lineChecker = new LineChecker(field, field[3, 3]);

            Assert.IsTrue(lineChecker.LeftDiagonalLine(out length, out lineElements));
            Assert.AreEqual(length, 5);
            Assert.AreSame(lineElements[2], field[1, 1]);
            Assert.AreSame(lineElements[4], field[5, 5]);
        }

        [TestMethod]
        public void TestRightDiagonalLine()
        {
            Field field = new Field(10, 10);
            int length;
            Cell[] lineElements;

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

            LineChecker lineChecker = new LineChecker(field, field[2, 4]);

            Assert.IsTrue(lineChecker.RightDiagonalLine(out length, out lineElements));
            Assert.AreEqual(length, 5);
            Assert.AreSame(lineElements[1], field[1, 5]);
            Assert.AreSame(lineElements[4], field[5, 1]);
        }

        [TestMethod]
        public void TestDoubleLine()
        {
            Field field = new Field(10, 10);
            int line1Length;
            Cell[] line1Elements;
            int line2Length;
            Cell[] line2Elements;

            //left diagonal line
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
            //+ vertical line
            field[2, 1].ContainedItem = BubbleSize.Big;
            field[2, 1].Color = BubbleColor.Red;
            field[3, 1].ContainedItem = BubbleSize.Big;
            field[3, 1].Color = BubbleColor.Red;
            field[4, 1].ContainedItem = BubbleSize.Big;
            field[4, 1].Color = BubbleColor.Red;
            field[5, 1].ContainedItem = BubbleSize.Big;
            field[5, 1].Color = BubbleColor.Red;

            LineChecker lineChecker = new LineChecker(field, field[1, 1]);

            Assert.IsTrue(lineChecker.LeftDiagonalLine(out line1Length, out line1Elements));
            Assert.AreEqual(line1Length, 5);
            Assert.AreSame(line1Elements[0], field[1, 1]);
            Assert.AreSame(line1Elements[4], field[5, 5]);

            Assert.IsTrue(lineChecker.VerticalLine(out line2Length, out line2Elements));
            Assert.AreEqual(line2Length, 5);
            Assert.AreSame(line2Elements[0], field[1, 1]);
            Assert.AreSame(line2Elements[4], field[5, 1]);
        }

        [TestMethod]
        public void TestCheckMethod_HorizontalLine()
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

            LineChecker lineChecker = new LineChecker(field, field[1, 3]);

            Assert.IsTrue(lineChecker.Check());
        }

        [TestMethod]
        public void TestCheckMethod_VerticalLine()
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

            LineChecker lineChecker = new LineChecker(field, field[2, 1]);

            Assert.IsTrue(lineChecker.Check());
        }

        [TestMethod]
        public void TestCheckMethod_LeftDiagonallLine()
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

            LineChecker lineChecker = new LineChecker(field, field[3, 3]);

            Assert.IsTrue(lineChecker.Check());
        }

        [TestMethod]
        public void TestCheckMethod_RightDiagonalLine()
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

            LineChecker lineChecker = new LineChecker(field, field[2, 4]);

            Assert.IsTrue(lineChecker.Check());
        }

    }
}
