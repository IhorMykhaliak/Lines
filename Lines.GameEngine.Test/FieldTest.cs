using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lines.GameEngine.Enums;

namespace Lines.GameEngine.Test
{
    [TestClass]
    public class FieldTest
    {
        [TestMethod]
        public void TestFieldInitialize()
        {
            Field field = new Field(10, 10);

            Assert.AreEqual(field.Height, 10);
            Assert.AreEqual(field.Width, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestFieldInitializeException1()
        {
            Field field = new Field(2, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestFieldInitializeException2()
        {
            Field field = new Field(4, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestFieldInitializeException3()
        {
            Field field = new Field(10, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestFieldInitializeException4()
        {
            Field field = new Field(-2, 8);
        }
    }
}
