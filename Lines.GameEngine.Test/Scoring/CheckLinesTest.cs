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

            CheckLines _checkLines = new CheckLines(field, field.Cells[1, 3]);

            Assert.IsTrue(_checkLines.CheckLine_Horizontal(out length, out lineElements));
            Assert.AreEqual(length, 5);
            Assert.AreSame(lineElements[2], field.Cells[1, 1]);
            Assert.AreSame(lineElements[4], field.Cells[1, 5]);
        }
        
        [TestMethod]
        public void TestVerticalLine()
        {
            Field field = new Field(10, 10);
            int length;
            Cell[] lineElements;

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

            CheckLines _checkLines = new CheckLines(field, field.Cells[2, 1]);

            Assert.IsTrue(_checkLines.CheckLine_Vertical(out length, out lineElements));
            Assert.AreEqual(length, 5);
            Assert.AreSame(lineElements[1], field.Cells[1, 1]);
            Assert.AreSame(lineElements[4], field.Cells[5, 1]);
        }

        [TestMethod]
        public void TestLeftDiagonallLine()
        {
            Field field = new Field(10, 10);
            int length;
            Cell[] lineElements;


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

            CheckLines _checkLines = new CheckLines(field, field.Cells[3, 3]);

            Assert.IsTrue(_checkLines.CheckLine_LeftDiagonal(out length, out lineElements));
            Assert.AreEqual(length, 5);
            Assert.AreSame(lineElements[2], field.Cells[1, 1]);
            Assert.AreSame(lineElements[4], field.Cells[5, 5]);
        }

        [TestMethod]
        public void TestRightDiagonalLine()
        {
            Field field = new Field(10, 10);
            int length;
            Cell[] lineElements;

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

            CheckLines _checkLines = new CheckLines(field, field.Cells[2, 4]);

            Assert.IsTrue(_checkLines.CheckLine_RightDiagonal(out length, out lineElements));
            Assert.AreEqual(length, 5);
            Assert.AreSame(lineElements[1], field.Cells[1, 5]);
            Assert.AreSame(lineElements[4], field.Cells[5, 1]);
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
            //+ vertical line
            field.Cells[2, 1].Contain = BubbleSize.Big;
            field.Cells[2, 1].Color = BubbleColor.Red;
            field.Cells[3, 1].Contain = BubbleSize.Big;
            field.Cells[3, 1].Color = BubbleColor.Red;
            field.Cells[4, 1].Contain = BubbleSize.Big;
            field.Cells[4, 1].Color = BubbleColor.Red;
            field.Cells[5, 1].Contain = BubbleSize.Big;
            field.Cells[5, 1].Color = BubbleColor.Red;

            CheckLines _checkLines = new CheckLines(field, field.Cells[1, 1]);

            Assert.IsTrue(_checkLines.CheckLine_LeftDiagonal(out line1Length, out line1Elements));
            Assert.AreEqual(line1Length, 5);
            Assert.AreSame(line1Elements[0], field.Cells[1, 1]);
            Assert.AreSame(line1Elements[4], field.Cells[5, 5]);

            Assert.IsTrue(_checkLines.CheckLine_Vertical(out line2Length, out line2Elements));
            Assert.AreEqual(line2Length, 5);
            Assert.AreSame(line2Elements[0], field.Cells[1, 1]);
            Assert.AreSame(line2Elements[4], field.Cells[5, 1]);
        }

        [TestMethod]
        public void TestCheckMethod_HorizontalLine()
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

            CheckLines _checkLines = new CheckLines(field, field.Cells[1, 3]);

            Assert.IsTrue(_checkLines.Check());
        }

        [TestMethod]
        public void TestCheckMethod_VerticalLine()
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

            CheckLines _checkLines = new CheckLines(field, field.Cells[2, 1]);

            Assert.IsTrue(_checkLines.Check());
        }

        [TestMethod]
        public void TestCheckMethod_LeftDiagonallLine()
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

            CheckLines _checkLines = new CheckLines(field, field.Cells[3, 3]);

            Assert.IsTrue(_checkLines.Check());
        }

        [TestMethod]
        public void TestCheckMethod_RightDiagonalLine()
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

            CheckLines _checkLines = new CheckLines(field, field.Cells[2, 4]);

            Assert.IsTrue(_checkLines.Check());
        }

    }
}
