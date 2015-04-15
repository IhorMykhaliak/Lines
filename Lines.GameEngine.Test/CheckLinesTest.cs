using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lines.GameEngine.Test
{
    [TestClass]
    public class CheckLinesTest
    {
        [TestMethod]
        public void TestVerticalLine()
        {
            NetOfCells Field = new NetOfCells();
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

            Assert.AreEqual(_checkLines.CheckLine_Horizontal(out length, out from, out to), true);
            Assert.AreEqual(length, 5);
            Assert.AreSame(from, Field.Cells[1, 1]);
            Assert.AreSame(to, Field.Cells[1, 5]);


        }
    }
}
