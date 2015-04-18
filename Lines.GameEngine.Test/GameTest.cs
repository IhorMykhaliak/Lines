using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lines.GameEngine.Test
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void TestInGameVerticalLine()
        {
            Game game = new Game();
            game.Field.Cells[1, 1].Contain = BubbleSize.Big;
            game.Field.Cells[1, 1].Color = BubbleColor.Red;
            game.Field.Cells[1, 2].Contain = BubbleSize.Big;
            game.Field.Cells[1, 2].Color = BubbleColor.Red;
            game.Field.Cells[1, 3].Contain = BubbleSize.Big;
            game.Field.Cells[1, 3].Color = BubbleColor.Red;
            game.Field.Cells[1, 8].Contain = BubbleSize.Big;
            game.Field.Cells[1, 8].Color = BubbleColor.Red;
            game.Field.Cells[1, 4].Contain = BubbleSize.Small;
            game.Field.Cells[1, 4].Color = BubbleColor.Blue;
            game.Field.Cells[1, 5].Contain = BubbleSize.Big;
            game.Field.Cells[1, 5].Color = BubbleColor.Red;

            //game.Start();

            game.SelectCell(1, 8);
            game.SelectCell(1, 4);

            Assert.AreEqual(game.Field.Cells[1, 1].Contain, null);
            Assert.AreEqual(game.Field.Cells[1, 2].Contain, null);
            Assert.AreEqual(game.Field.Cells[1, 3].Contain, null);
            Assert.AreEqual(game.Field.Cells[1, 4].Contain, BubbleSize.Small);
            Assert.AreEqual(game.Field.Cells[1, 5].Contain, null);


        }
    }
}
