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
        public void TestGetWay1()
        {
            Field Field = new Field(10, 10);

            Cell from = Field.Cells[0, 2];
            Cell to = Field.Cells[3, 4];

            List<Cell> Way;
            List<Cell> ExpectedWay = new List<Cell>()
            {
                Field.Cells[0, 2],
                Field.Cells[1, 2],
                Field.Cells[2, 2],
                Field.Cells[3, 2],
                Field.Cells[3, 3],
                Field.Cells[3, 4]
            };

            Assert.IsTrue(FindPath.GetWay(Field, from, to, out Way));
            CollectionAssert.AllItemsAreNotNull(Way);
            CollectionAssert.AllItemsAreUnique(Way);
            CollectionAssert.AreEqual(ExpectedWay, Way);
        }

        [TestMethod]
        public void TestGetWay2()
        {
            Field Field = new Field(10, 10);
            Field.Cells[1, 2].Contain = BubbleSize.Big;
            Field.Cells[0, 3].Contain = BubbleSize.Big;
            Cell from = Field.Cells[0, 2];
            Cell to = Field.Cells[3, 4];

            List<Cell> Way;
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

            Assert.IsTrue(FindPath.GetWay(Field, from, to, out Way));
            CollectionAssert.AllItemsAreNotNull(Way);
            CollectionAssert.AllItemsAreUnique(Way);
            CollectionAssert.AreEqual(ExpectedWay, Way);
        }

        [TestMethod]
        public void TestGetWay3()
        {
            Field Field = new Field(10, 10);

            Cell from = Field.Cells[0, 2];
            Cell to = Field.Cells[0, 2];

            List<Cell> Way;
            List<Cell> ExpectedWay = new List<Cell>()
            {
                Field.Cells[0, 2]
            };

            Assert.IsTrue(FindPath.GetWay(Field, from, to, out Way));
            CollectionAssert.AllItemsAreNotNull(Way);
            CollectionAssert.AllItemsAreUnique(Way);
            CollectionAssert.AreEqual(ExpectedWay, Way);
        }

        [TestMethod]
        public void TestGetWay_WayDoesntExsist1()
        {
            Field Field = new Field(10, 10);
            Field.Cells[1, 2].Contain = BubbleSize.Big;
            Field.Cells[0, 3].Contain = BubbleSize.Big;
            Field.Cells[0, 1].Contain = BubbleSize.Big;
            Cell from = Field.Cells[0, 2];
            Cell to = Field.Cells[3, 4];
            List<Cell> Way;

            Assert.IsFalse(FindPath.GetWay(Field, from, to, out Way));
        }

        [TestMethod]
        public void TestGetWay_WayDoesntExsist2()
        {
            Field Field = new Field(10, 10);
            Field.Cells[3, 4].Contain = BubbleSize.Big;
            Cell from = Field.Cells[0, 2];
            Cell to = Field.Cells[3, 4];
            List<Cell> Way;

            Assert.IsFalse(FindPath.GetWay(Field, from, to, out Way));
        }

    }
}
