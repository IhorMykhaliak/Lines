using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lines.GameEngine.PathFinding_Algorithm;

namespace Lines.GameEngine.Test.Pathfinding_Algorithm
{
    [TestClass]
    public class MapTest
    {
        #region Test GetElementById

        [TestMethod]
        public void TestGetElementById()
        {
            Field Field = new Field();
            Map Map = new Map(Field);

            Assert.AreEqual(Map.GetElementById(31), Map.Elements[3, 1]);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGetElementByIdException1()
        {
            Field Field = new Field();
            Map Map = new Map(Field);

            Map.GetElementById(311);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGetElementByIdException2()
        {
            Field Field = new Field();
            Map Map = new Map(Field);

            Map.GetElementById(-3);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGetElementByIdException3()
        {
            Field Field = new Field();
            Map Map = new Map(Field);

            Map.GetElementById(-1);
        }

        #endregion

        #region Test available neighboor (2,3,4)

        [TestMethod]
        public void TestGetAvailableNeighboors1()
        {
            Field Field = new Field();
            Map Map = new Map(Field);
            MapElement[] Neighboors = Map.GetAvailableNeighboors(Map.Elements[0, 0]);
            MapElement[] ExpectedNeighboors = new MapElement[]
            {
                Map.Elements[1, 0],
                Map.Elements[0, 1]
            };

            Assert.AreEqual(Neighboors.Length, 2);
            CollectionAssert.AllItemsAreNotNull(Neighboors);
            CollectionAssert.AllItemsAreUnique(Neighboors);
            CollectionAssert.AreEqual(ExpectedNeighboors, Neighboors);

        }

        [TestMethod]
        public void TestGetAvailableNeighboors2()
        {
            Field Field = new Field();
            Map Map = new Map(Field);
            MapElement[] Neighboors = Map.GetAvailableNeighboors(Map.Elements[1, 0]);
            MapElement[] ExpectedNeighboors = new MapElement[]
            {
                Map.Elements[0, 0],
                Map.Elements[2, 0],
                Map.Elements[1, 1]
            };

            Assert.AreEqual(Neighboors.Length, 3);
            CollectionAssert.AllItemsAreNotNull(Neighboors);
            CollectionAssert.AllItemsAreUnique(Neighboors);
            CollectionAssert.AreEqual(ExpectedNeighboors, Neighboors);

        }

        [TestMethod]
        public void TestGetAvailableNeighboors3()
        {
            Field Field = new Field();
            Map Map = new Map(Field);
            MapElement[] Neighboors = Map.GetAvailableNeighboors(Map.Elements[1, 1]);
            MapElement[] ExpectedNeighboors = new MapElement[]
            {
                Map.Elements[0, 1],
                Map.Elements[2, 1],
                Map.Elements[1, 0],
                Map.Elements[1, 2]
            };

            Assert.AreEqual(Neighboors.Length, 4);
            CollectionAssert.AllItemsAreNotNull(Neighboors);
            CollectionAssert.AllItemsAreUnique(Neighboors);
            CollectionAssert.AreEqual(ExpectedNeighboors, Neighboors);

        }

        #endregion

        #region Test available neighboor with elements that aren't available (1,2,3)

        [TestMethod]
        public void TestGetAvailableNeighboors4()
        {
            Field Field = new Field();
            Field.Cells[0, 1].Contain = BubbleSize.Big;
            Map Map = new Map(Field);
            MapElement[] Neighboors = Map.GetAvailableNeighboors(Map.Elements[0, 0]);
            MapElement[] ExpectedNeighboors = new MapElement[]
            {
                Map.Elements[1, 0]
            };

            Assert.AreEqual(Neighboors.Length, 1);
            CollectionAssert.AllItemsAreNotNull(Neighboors);
            CollectionAssert.AllItemsAreUnique(Neighboors);
            CollectionAssert.AreEqual(ExpectedNeighboors, Neighboors);

        }

        [TestMethod]
        public void TestGetAvailableNeighboors5()
        {
            Field Field = new Field();
            Field.Cells[0, 0].Contain = BubbleSize.Big;
            Field.Cells[1, 1].Contain = BubbleSize.Big;
            Map Map = new Map(Field);

            MapElement[] Neighboors = Map.GetAvailableNeighboors(Map.Elements[1, 0]);
            MapElement[] ExpectedNeighboors = new MapElement[]
            {
                Map.Elements[2, 0]
            };

            Assert.AreEqual(Neighboors.Length, 1);
            CollectionAssert.AllItemsAreNotNull(Neighboors);
            CollectionAssert.AllItemsAreUnique(Neighboors);
            CollectionAssert.AreEqual(ExpectedNeighboors, Neighboors);

        }

        [TestMethod]
        public void TestGetAvailableNeighboors6()
        {
            Field Field = new Field();
            Field.Cells[0, 0].Contain = BubbleSize.Big;
            Field.Cells[1, 1].Contain = BubbleSize.Big;
            Field.Cells[2, 0].Contain = BubbleSize.Big;
            Map Map = new Map(Field);

            MapElement[] Neighboors = Map.GetAvailableNeighboors(Map.Elements[1, 0]);
            MapElement[] ExpectedNeighboors = new MapElement[] { };

            CollectionAssert.AreEqual(ExpectedNeighboors, Neighboors);

        }

        #endregion

    }
}
