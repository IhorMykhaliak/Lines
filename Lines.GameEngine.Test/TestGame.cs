using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lines.GameEngine.Test
{
    [TestClass]
    public class TestGame
    {
        [TestMethod]
        public void TestHorizontalLine()
        {
            Game game = new Game();
            game.Field.Cells[1, 1].Contain = ContainedItem.Big;
            game.Field.Cells[1, 1].Color = BubbleColor.Red;
            game.Field.Cells[1, 2].Contain = ContainedItem.Big;
            game.Field.Cells[1, 2].Color = BubbleColor.Red;
            game.Field.Cells[1, 3].Contain = ContainedItem.Big;
            game.Field.Cells[1, 3].Color = BubbleColor.Red;
            game.Field.Cells[1, 8].Contain = ContainedItem.Big;
            game.Field.Cells[1, 8].Color = BubbleColor.Red;
            game.Field.Cells[1, 4].Contain = ContainedItem.Small;
            game.Field.Cells[1, 4].Color = BubbleColor.Blue;
            game.Field.Cells[1, 5].Contain = ContainedItem.Big;
            game.Field.Cells[1, 5].Color = BubbleColor.Red;

            game.Start();

            game.SelectCell(1, 8);
            game.SelectCell(1, 4);

            Assert.AreEqual(game.Field.Cells[1, 1].Contain, null);
            Assert.AreEqual(game.Field.Cells[1, 2].Contain, null);
            Assert.AreEqual(game.Field.Cells[1, 3].Contain, null);
            Assert.AreEqual(game.Field.Cells[1, 4].Contain, ContainedItem.Small);
            Assert.AreEqual(game.Field.Cells[1, 5].Contain, null);


        }
    }
}
