using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lines.GameEngine.Test
{
    [TestClass]
    public class CheckLinesTest
    {
        [TestMethod]
        public void TestHorizontalLine()
        {
            Field Field = new Field();
            int length;
            Cell from;
            Cell to;

            Field.Cells[1, 1].Contain = BubbleSize.Big;
            Field.Cells[1, 1].Color = BubbleColor.Red;
            Field.Cells[1, 2].Contain = BubbleSize.Big;
            Field.Cells[1, 2].Color = BubbleColor.Red;
            Field.Cells[1, 3].Contain = BubbleSize.Big;
            Field.Cells[1, 3].Color = BubbleColor.Red;
            Field.Cells[1, 4].Contain = BubbleSize.Big;
            Field.Cells[1, 4].Color = BubbleColor.Red;
            Field.Cells[1, 5].Contain = BubbleSize.Big;
            Field.Cells[1, 5].Color = BubbleColor.Red;

            CheckLines _checkLines = new CheckLines(Field, 1, 3);

            Assert.IsTrue(_checkLines.CheckLine_Horizontal(out length, out from, out to));
            Assert.AreEqual(length, 5);
            Assert.AreSame(from, Field.Cells[1, 1]);
            Assert.AreSame(to, Field.Cells[1, 5]);
        }
       
        [TestMethod]
        public void TestVerticalLine()
        {
            Field Field = new Field();
            int length;
            Cell from;
            Cell to;

            Field.Cells[1, 1].Contain = BubbleSize.Big;
            Field.Cells[1, 1].Color = BubbleColor.Red;
            Field.Cells[2, 1].Contain = BubbleSize.Big;
            Field.Cells[2, 1].Color = BubbleColor.Red;
            Field.Cells[3, 1].Contain = BubbleSize.Big;
            Field.Cells[3, 1].Color = BubbleColor.Red;
            Field.Cells[4, 1].Contain = BubbleSize.Big;
            Field.Cells[4, 1].Color = BubbleColor.Red;
            Field.Cells[5, 1].Contain = BubbleSize.Big;
            Field.Cells[5, 1].Color = BubbleColor.Red;

            CheckLines _checkLines = new CheckLines(Field, 2, 1);

            Assert.IsTrue(_checkLines.CheckLine_Vertical(out length, out from, out to));
            Assert.AreEqual(length, 5);
            Assert.AreSame(from, Field.Cells[1, 1]);
            Assert.AreSame(to, Field.Cells[5, 1]);
        }

        [TestMethod]
        public void TestLeftDiagonallLine()
        {
            Field Field = new Field();
            int length;
            Cell from;
            Cell to;

            Field.Cells[1, 1].Contain = BubbleSize.Big;
            Field.Cells[1, 1].Color = BubbleColor.Red;
            Field.Cells[2, 2].Contain = BubbleSize.Big;
            Field.Cells[2, 2].Color = BubbleColor.Red;
            Field.Cells[3, 3].Contain = BubbleSize.Big;
            Field.Cells[3, 3].Color = BubbleColor.Red;
            Field.Cells[4, 4].Contain = BubbleSize.Big;
            Field.Cells[4, 4].Color = BubbleColor.Red;
            Field.Cells[5, 5].Contain = BubbleSize.Big;
            Field.Cells[5, 5].Color = BubbleColor.Red;

            CheckLines _checkLines = new CheckLines(Field, 3, 3);

            Assert.IsTrue(_checkLines.CheckLine_LeftDiagonal(out length, out from, out to));
            Assert.AreEqual(length, 5);
            Assert.AreSame(from, Field.Cells[1, 1]);
            Assert.AreSame(to, Field.Cells[5, 5]);
        }

        [TestMethod]
        public void TestRightDiagonalLine()
        {
            Field Field = new Field();
            int length;
            Cell from;
            Cell to;

            Field.Cells[5, 1].Contain = BubbleSize.Big;
            Field.Cells[5, 1].Color = BubbleColor.Red;
            Field.Cells[4, 2].Contain = BubbleSize.Big;
            Field.Cells[4, 2].Color = BubbleColor.Red;
            Field.Cells[3, 3].Contain = BubbleSize.Big;
            Field.Cells[3, 3].Color = BubbleColor.Red;
            Field.Cells[2, 4].Contain = BubbleSize.Big;
            Field.Cells[2, 4].Color = BubbleColor.Red;
            Field.Cells[1, 5].Contain = BubbleSize.Big;
            Field.Cells[1, 5].Color = BubbleColor.Red;

            CheckLines _checkLines = new CheckLines(Field, 2, 4);

            Assert.IsTrue(_checkLines.CheckLine_RightDiagonal(out length, out from, out to));
            Assert.AreEqual(length, 5);
            Assert.AreSame(from, Field.Cells[1, 5]);
            Assert.AreSame(to, Field.Cells[5, 1]);
        }

        [TestMethod]
        public void TestCheckMethod_HorizontalLine()
        {
            Field Field = new Field();

            Field.Cells[1, 1].Contain = BubbleSize.Big;
            Field.Cells[1, 1].Color = BubbleColor.Red;
            Field.Cells[1, 2].Contain = BubbleSize.Big;
            Field.Cells[1, 2].Color = BubbleColor.Red;
            Field.Cells[1, 3].Contain = BubbleSize.Big;
            Field.Cells[1, 3].Color = BubbleColor.Red;
            Field.Cells[1, 4].Contain = BubbleSize.Big;
            Field.Cells[1, 4].Color = BubbleColor.Red;
            Field.Cells[1, 5].Contain = BubbleSize.Big;
            Field.Cells[1, 5].Color = BubbleColor.Red;
            
            CheckLines _checkLines = new CheckLines(Field, 1, 3);

            Assert.IsTrue(_checkLines.Check());
        }

        [TestMethod]
        public void TestCheckMethod_VerticalLine()
        {
            Field Field = new Field();

            Field.Cells[1, 1].Contain = BubbleSize.Big;
            Field.Cells[1, 1].Color = BubbleColor.Red;
            Field.Cells[2, 1].Contain = BubbleSize.Big;
            Field.Cells[2, 1].Color = BubbleColor.Red;
            Field.Cells[3, 1].Contain = BubbleSize.Big;
            Field.Cells[3, 1].Color = BubbleColor.Red;
            Field.Cells[4, 1].Contain = BubbleSize.Big;
            Field.Cells[4, 1].Color = BubbleColor.Red;
            Field.Cells[5, 1].Contain = BubbleSize.Big;
            Field.Cells[5, 1].Color = BubbleColor.Red;

            CheckLines _checkLines = new CheckLines(Field, 2, 1);

            Assert.IsTrue(_checkLines.Check());
        }

        [TestMethod]
        public void TestCheckMethod_LeftDiagonallLine()
        {
            Field Field = new Field();

            Field.Cells[1, 1].Contain = BubbleSize.Big;
            Field.Cells[1, 1].Color = BubbleColor.Red;
            Field.Cells[2, 2].Contain = BubbleSize.Big;
            Field.Cells[2, 2].Color = BubbleColor.Red;
            Field.Cells[3, 3].Contain = BubbleSize.Big;
            Field.Cells[3, 3].Color = BubbleColor.Red;
            Field.Cells[4, 4].Contain = BubbleSize.Big;
            Field.Cells[4, 4].Color = BubbleColor.Red;
            Field.Cells[5, 5].Contain = BubbleSize.Big;
            Field.Cells[5, 5].Color = BubbleColor.Red;

            CheckLines _checkLines = new CheckLines(Field, 3, 3);

            Assert.IsTrue(_checkLines.Check());
        }

        [TestMethod]
        public void TestCheckMethod_RightDiagonalLine()
        {
            Field Field = new Field();

            Field.Cells[5, 1].Contain = BubbleSize.Big;
            Field.Cells[5, 1].Color = BubbleColor.Red;
            Field.Cells[4, 2].Contain = BubbleSize.Big;
            Field.Cells[4, 2].Color = BubbleColor.Red;
            Field.Cells[3, 3].Contain = BubbleSize.Big;
            Field.Cells[3, 3].Color = BubbleColor.Red;
            Field.Cells[2, 4].Contain = BubbleSize.Big;
            Field.Cells[2, 4].Color = BubbleColor.Red;
            Field.Cells[1, 5].Contain = BubbleSize.Big;
            Field.Cells[1, 5].Color = BubbleColor.Red;

            CheckLines _checkLines = new CheckLines(Field, 2, 4);

            Assert.IsTrue(_checkLines.Check());
        }

    }
}
