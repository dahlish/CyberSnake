using Microsoft.VisualStudio.TestTools.UnitTesting;
using CyberSnake;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyberSnake.Tests
{
    [TestClass()]
    public class TailTests
    {
        /// <summary>
        /// These three tests make sure that all new  Tail objects are being added to the allObjects list in the GameWorld to be rendered.
        /// </summary>
        [TestMethod()]
        public void TailTest()
        {
            GameWorld world = new GameWorld(10, 10, Difficulty.Easy);
            Tail tailTest1 = new Tail('0', new Position(3, 4), world);

            Assert.IsTrue(world.AllObjects.Contains(tailTest1));

            Tail tailTest2 = new Tail('0', new Position(7, 1), world);

            Assert.IsTrue(world.AllObjects.Contains(tailTest2));


            Tail tailTest3 = new Tail('0', new Position(1, 4), world);

            Assert.IsTrue(world.AllObjects.Contains(tailTest3));

        }
    }
}