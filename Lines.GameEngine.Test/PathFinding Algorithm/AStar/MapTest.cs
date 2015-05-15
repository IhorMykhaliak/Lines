using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lines.GameEngine.PathFindingAlgorithm.AStar;
using Lines.GameEngine.Enums;

namespace Lines.GameEngine.Test.Pathfinding_Algorithm
{
    [TestClass]
    public class MapTest
    {
        #region Test GetElementById

        [TestMethod]
        public void TestGetElementById()
        {
            Field field = new Field(10, 10);
            Map map = new Map(field);

            Assert.AreEqual(map.GetElementById(31), map[3, 1]);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGetElementByIdException1()
        {
            Field field = new Field(10, 10);
            Map map = new Map(field);

            map.GetElementById(311);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGetElementByIdException2()
        {
            Field field = new Field(10, 10);
            Map map = new Map(field);

            map.GetElementById(-3);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGetElementByIdException3()
        {
            Field field = new Field(10, 10);
            Map map = new Map(field);

            map.GetElementById(-1);
        }

        #endregion

        #region Test available neighboor (2,3,4)

        [TestMethod]
        public void TestGetAvailableneighboors1()
        {
            Field field = new Field(10, 10);
            Map map = new Map(field);
            MapElement[] neighboors = map.GetAvailableNeighboors(map[0, 0]);
            MapElement[] expectedneighboors = new MapElement[]
            {
                map[1, 0],
                map[0, 1]
            };

            Assert.AreEqual(neighboors.Length, 2);
            CollectionAssert.AllItemsAreNotNull(neighboors);
            CollectionAssert.AllItemsAreUnique(neighboors);
            CollectionAssert.AreEqual(expectedneighboors, neighboors);

        }

        [TestMethod]
        public void TestGetAvailableneighboors2()
        {
            Field field = new Field(10, 10);
            Map map = new Map(field);
            MapElement[] neighboors = map.GetAvailableNeighboors(map[1, 0]);
            MapElement[] expectedneighboors = new MapElement[]
            {
                map[0, 0],
                map[2, 0],
                map[1, 1]
            };

            Assert.AreEqual(neighboors.Length, 3);
            CollectionAssert.AllItemsAreNotNull(neighboors);
            CollectionAssert.AllItemsAreUnique(neighboors);
            CollectionAssert.AreEqual(expectedneighboors, neighboors);
        }

        [TestMethod]
        public void TestGetAvailableneighboors3()
        {
            Field field = new Field(10, 10);
            Map map = new Map(field);
            MapElement[] neighboors = map.GetAvailableNeighboors(map[1, 1]);
            MapElement[] expectedneighboors = new MapElement[]
            {
                map[0, 1],
                map[2, 1],
                map[1, 0],
                map[1, 2]
            };

            Assert.AreEqual(neighboors.Length, 4);
            CollectionAssert.AllItemsAreNotNull(neighboors);
            CollectionAssert.AllItemsAreUnique(neighboors);
            CollectionAssert.AreEqual(expectedneighboors, neighboors);
        }

        #endregion

        #region Test available neighboor with elements that aren't available (1,2,3)

        [TestMethod]
        public void TestGetAvailableneighboors4()
        {
            Field field = new Field(10, 10);
            field[0, 1].ContainedItem = BubbleSize.Big;
            Map map = new Map(field);
            MapElement[] neighboors = map.GetAvailableNeighboors(map[0, 0]);
            MapElement[] expectedneighboors = new MapElement[]
            {
                map[1, 0]
            };

            Assert.AreEqual(neighboors.Length, 1);
            CollectionAssert.AllItemsAreNotNull(neighboors);
            CollectionAssert.AllItemsAreUnique(neighboors);
            CollectionAssert.AreEqual(expectedneighboors, neighboors);
        }

        [TestMethod]
        public void TestGetAvailableneighboors5()
        {
            Field field = new Field(10, 10);
            field[0, 0].ContainedItem = BubbleSize.Big;
            field[1, 1].ContainedItem = BubbleSize.Big;
            Map map = new Map(field);

            MapElement[] neighboors = map.GetAvailableNeighboors(map[1, 0]);
            MapElement[] expectedneighboors = new MapElement[]
            {
                map[2, 0]
            };

            Assert.AreEqual(neighboors.Length, 1);
            CollectionAssert.AllItemsAreNotNull(neighboors);
            CollectionAssert.AllItemsAreUnique(neighboors);
            CollectionAssert.AreEqual(expectedneighboors, neighboors);
        }

        [TestMethod]
        public void TestGetAvailableneighboors6()
        {
            Field field = new Field(10, 10);
            field[0, 0].ContainedItem = BubbleSize.Big;
            field[1, 1].ContainedItem = BubbleSize.Big;
            field[2, 0].ContainedItem = BubbleSize.Big;
            Map map = new Map(field);

            MapElement[] neighboors = map.GetAvailableNeighboors(map[1, 0]);
            MapElement[] expectedneighboors = new MapElement[] { };

            CollectionAssert.AreEqual(expectedneighboors, neighboors);
        }

        #endregion
    }
}
