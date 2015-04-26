using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lines.GameEngine.PathFinding_Algorithm;
using System.Collections.Generic;
using Lines.GameEngine.Enums;

namespace Lines.GameEngine.Test.Pathfinding_Algorithm
{
    [TestClass]
    public class FindPathTest
    {
        [TestMethod]
        public void TestGetWay1()
        {
            Field field = new Field(10, 10);
            Cell from = field.Cells[0, 2];
            Cell to = field.Cells[3, 4];

            FindPath findPath = new FindPath(field, from, to);

            List<Cell> Way;
            List<Cell> ExpectedWay = new List<Cell>()
            {
                field.Cells[0, 2],
                field.Cells[1, 2],
                field.Cells[2, 2],
                field.Cells[3, 2],
                field.Cells[3, 3],
                field.Cells[3, 4]
            };

            Assert.IsTrue(findPath.GetWay(out Way));
            CollectionAssert.AllItemsAreNotNull(Way);
            CollectionAssert.AllItemsAreUnique(Way);
            CollectionAssert.AreEqual(ExpectedWay, Way);
        }

        [TestMethod]
        public void TestGetWay2()
        {
            Field field = new Field(10, 10);
            field.Cells[1, 2].Contain = BubbleSize.Big;
            field.Cells[0, 3].Contain = BubbleSize.Big;
            Cell from = field.Cells[0, 2];
            Cell to = field.Cells[3, 4];

            FindPath findPath = new FindPath(field, from, to);
            List<Cell> Way;
            List<Cell> ExpectedWay = new List<Cell>()
            {
                field.Cells[0, 2],
                field.Cells[0, 1],
                field.Cells[1, 1],
                field.Cells[2, 1],
                field.Cells[3, 1],
                field.Cells[3, 2],
                field.Cells[3, 3],
                field.Cells[3, 4]
            };

            Assert.IsTrue(findPath.GetWay(out Way));
            CollectionAssert.AllItemsAreNotNull(Way);
            CollectionAssert.AllItemsAreUnique(Way);
            CollectionAssert.AreEqual(ExpectedWay, Way);
        }

        [TestMethod]
        public void TestGetWay3()
        {
            Field field = new Field(10, 10);

            Cell from = field.Cells[0, 2];
            Cell to = field.Cells[0, 2];

            FindPath findPath = new FindPath(field, from, to);
            List<Cell> Way;
            List<Cell> ExpectedWay = new List<Cell>()
            {
                field.Cells[0, 2]
            };

            Assert.IsTrue(findPath.GetWay( out Way));
            CollectionAssert.AllItemsAreNotNull(Way);
            CollectionAssert.AllItemsAreUnique(Way);
            CollectionAssert.AreEqual(ExpectedWay, Way);
        }

        [TestMethod]
        public void TestGetWay_WayDoesntExsist1()
        {
            Field field = new Field(10, 10);
            field.Cells[1, 2].Contain = BubbleSize.Big;
            field.Cells[0, 3].Contain = BubbleSize.Big;
            field.Cells[0, 1].Contain = BubbleSize.Big;
            Cell from = field.Cells[0, 2];
            Cell to = field.Cells[3, 4];
            FindPath findPath = new FindPath(field, from, to);
            List<Cell> Way;

            Assert.IsFalse(findPath.GetWay( out Way));
        }

        [TestMethod]
        public void TestGetWay_WayDoesntExsist2()
        {
            Field field = new Field(10, 10);
            field.Cells[3, 4].Contain = BubbleSize.Big;
            Cell from = field.Cells[0, 2];
            Cell to = field.Cells[3, 4];
            FindPath findPath = new FindPath(field, from, to);
            List<Cell> Way;

            Assert.IsFalse(findPath.GetWay( out Way));
        }
    }
}
