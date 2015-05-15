using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lines.GameEngine.PathFindingAlgorithm.AStar;
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
            Cell from = field[0, 2];
            Cell to = field[3, 4];

            FindPath findPath = new FindPath();

            List<Cell> way;
            List<Cell> expectedWay = new List<Cell>()
            {
                field[0, 2],
                field[1, 2],
                field[2, 2],
                field[3, 2],
                field[3, 3],
                field[3, 4]
            };

            Assert.IsTrue(findPath.TryGetPath(field, from, to, out way));
            CollectionAssert.AllItemsAreNotNull(way);
            CollectionAssert.AllItemsAreUnique(way);
            CollectionAssert.AreEqual(expectedWay, way);
        }

        [TestMethod]
        public void TestGetWay2()
        {
            Field field = new Field(10, 10);
            field[1, 2].ContainedItem = BubbleSize.Big;
            field[0, 3].ContainedItem = BubbleSize.Big;
            Cell from = field[0, 2];
            Cell to = field[3, 4];

            FindPath findPath = new FindPath();
            List<Cell> way;
            List<Cell> expectedWay = new List<Cell>()
            {
                field[0, 2],
                field[0, 1],
                field[1, 1],
                field[2, 1],
                field[3, 1],
                field[3, 2],
                field[3, 3],
                field[3, 4]
            };

            Assert.IsTrue(findPath.TryGetPath(field, from, to, out way));
            CollectionAssert.AllItemsAreNotNull(way);
            CollectionAssert.AllItemsAreUnique(way);
            CollectionAssert.AreEqual(expectedWay, way);
        }

        [TestMethod]
        public void TestGetWay3()
        {
            Field field = new Field(10, 10);

            Cell from = field[0, 2];
            Cell to = field[0, 2];

            FindPath findPath = new FindPath();
            List<Cell> way;
            List<Cell> expectedWay = new List<Cell>()
            {
                field[0, 2]
            };

            Assert.IsTrue(findPath.TryGetPath(field, from, to, out way));
            CollectionAssert.AllItemsAreNotNull(way);
            CollectionAssert.AllItemsAreUnique(way);
            CollectionAssert.AreEqual(expectedWay, way);
        }

        [TestMethod]
        public void TestGetWay_WayDoesntExsist1()
        {
            Field field = new Field(10, 10);
            field[1, 2].ContainedItem = BubbleSize.Big;
            field[0, 3].ContainedItem = BubbleSize.Big;
            field[0, 1].ContainedItem = BubbleSize.Big;
            Cell from = field[0, 2];
            Cell to = field[3, 4];
            FindPath findPath = new FindPath();
            List<Cell> way;

            Assert.IsFalse(findPath.TryGetPath(field, from, to, out way));
        }

        [TestMethod]
        public void TestGetWay_WayDoesntExsist2()
        {
            Field field = new Field(10, 10);
            field[3, 4].ContainedItem = BubbleSize.Big;
            Cell from = field[0, 2];
            Cell to = field[3, 4];
            FindPath findPath = new FindPath();
            List<Cell> way;

            Assert.IsFalse(findPath.TryGetPath(field, from, to, out way));
        }
    }
}
