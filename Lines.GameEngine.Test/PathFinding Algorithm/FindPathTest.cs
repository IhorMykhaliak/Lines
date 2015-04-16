using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lines.GameEngine.PathFinding_Algorithm;
using System.Collections.Generic;

namespace Lines.GameEngine.Test.Pathfinding_Algorithm
{
    [TestClass]
    public class FindPathTest
    {
        [TestMethod]
        public void TestGetWay()
        {
            NetOfCells Field = new NetOfCells();

            Cell from = Field.Cells[0, 2];
            Cell to = Field.Cells[3, 4];

            FindPath findPath = new FindPath(Field, from, to);
            List<Cell> Way;
            //findPath.GetWay(out Way);
            List<Cell> ExpectedWay = new List<Cell>()
            {
                Field.Cells[0, 2],
                Field.Cells[1, 2],
                Field.Cells[2, 2],
                Field.Cells[3, 2],
                Field.Cells[3, 3],
                Field.Cells[3, 4]
            };
            Assert.IsTrue(findPath.GetWay(out Way));
            CollectionAssert.AllItemsAreNotNull(Way);
            CollectionAssert.AllItemsAreUnique(Way);
            CollectionAssert.AreEqual(ExpectedWay, Way);
        }

        [TestMethod]
        public void TestGetWay2()
        {
            NetOfCells Field = new NetOfCells();
            Field.Cells[1, 2].Contain = BubbleSize.Big;
            Field.Cells[0, 3].Contain = BubbleSize.Big;
            Cell from = Field.Cells[0, 2];
            Cell to = Field.Cells[3, 4];

            FindPath findPath = new FindPath(Field, from, to);
            List<Cell> Way;
            //findPath.GetWay(out Way);
            List<Cell> ExpectedWay = new List<Cell>()
            {
                Field.Cells[0, 2],
                Field.Cells[0, 1],
                Field.Cells[1, 1],
                Field.Cells[2, 1],
                Field.Cells[3, 1],
                Field.Cells[3, 2],
                Field.Cells[3, 3],
                Field.Cells[3, 4]
            };
            Assert.IsTrue(findPath.GetWay(out Way));
            CollectionAssert.AllItemsAreNotNull(Way);
            CollectionAssert.AllItemsAreUnique(Way);
            CollectionAssert.AreEqual(ExpectedWay, Way);
        }

       



    }
}
