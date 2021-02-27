using Microsoft.VisualStudio.TestTools.UnitTesting;
using CyberSnake;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyberSnake.Tests
{
    [TestClass()]
    public class PositionTests
    {

        /// <summary>
        /// This test method tests the addition operator for the Position struct.
        /// </summary>
        [TestMethod()]
        public void PositionAddOperatorTest()
        {
            Position pos1Test1 = new Position(3, 4);
            Position pos2Test1 = new Position(7, 6);
            Position expectedTest1 = new Position(10, 10);
            Position actualTest1 = pos1Test1 + pos2Test1;
            Assert.AreEqual(expectedTest1, actualTest1);


            Position pos1Test2 = new Position(25, 25);
            Position pos2Test2 = new Position(-25, -25);
            Position expectedTest2 = new Position(0, 0);
            Position actualTest2 = pos1Test2 + pos2Test2;
            Assert.AreEqual(expectedTest2, actualTest2);

        }

        /// <summary>
        /// This test method tests the subtraction operator for the Position struct.
        /// </summary>
        [TestMethod()]
        public void PositionSubtractOperatorTest()
        {
            Position pos1Test1 = new Position(3, 4);
            Position pos2Test1 = new Position(7, 6);
            Position expectedTest1 = new Position(-4, -2);
            Position actualTest1 = pos1Test1 - pos2Test1;
            Assert.AreEqual(expectedTest1, actualTest1);


            Position pos1Test2 = new Position(25, 25);
            Position pos2Test2 = new Position(-25, -25);
            Position expectedTest2 = new Position(50, 50);
            Position actualTest2 = pos1Test2 - pos2Test2;
            Assert.AreEqual(expectedTest2, actualTest2);
        }

        /// <summary>
        /// This test method tests the Equals operator for the Position struct.
        /// </summary>
        [TestMethod()]
        public void PositionEqualOperatorTest()
        {
            Position pos1Test1 = new Position(3, 4);
            Position pos2Test1 = new Position(3, 4);
            bool expectedTest1 = true;
            bool actualTest1 = pos1Test1 == pos2Test1;
            Assert.AreEqual(expectedTest1, actualTest1);

            Position pos1Test2 = new Position(12, 13);
            Position pos2Test2 = new Position(13, 12);
            bool expectedTest2 = false;
            bool actualTest2 = pos1Test2 == pos2Test2;
            Assert.AreEqual(expectedTest2, actualTest2);
        }

        /// <summary>
        /// This test method tests the not equals operator for the Position struct.
        /// </summary>
        [TestMethod()]
        public void PositionNotEqualOperatorTest()
        {
            Position pos1Test1 = new Position(3, 4);
            Position pos2Test1 = new Position(3, 4);
            bool expectedTest1 = false;
            bool actualTest1 = pos1Test1 != pos2Test1;
            Assert.AreEqual(expectedTest1, actualTest1);

            Position pos1Test2 = new Position(12, 13);
            Position pos2Test2 = new Position(13, 12);
            bool expectedTest2 = true;
            bool actualTest2 = pos1Test2 != pos2Test2;
            Assert.AreEqual(expectedTest2, actualTest2);
        }

        /// <summary>
        /// This method tests to make sure that the Position class can find an available position.
        /// We also check another time that all in cases where there is an available position, we always get a Position type, and not an exception.
        /// </summary>
        [TestMethod()]
        public void GetRandomPositionAvailableTest()
        {
            GameWorld world1 = new GameWorld(10, 10, Difficulty.Easy);

            ConsoleRenderer.ConsoleWidth = 2;
            ConsoleRenderer.ConsoleHeight = 2;
            Wall wall1 = new Wall('X', new Position(0, 1), world1);
            Wall wall2 = new Wall('X', new Position(1, 1), world1);


            world1.AllObjects.Add(wall1);
            world1.AllObjects.Add(wall2);

            for (int i = 0; i <= 100000; i++)
            {
                Assert.ThrowsException<NoAvailablePositionFoundException>(() => Position.GetRandomPositionAvailable(world1));
            }

            GameWorld world2 = new GameWorld(10, 10, Difficulty.Easy);
            Wall wall3 = new Wall('X', new Position(0, 1), world2);
            world2.AllObjects.Add(wall3);

            for (int i = 0; i <= 100000; i++)
            {
                Assert.IsInstanceOfType(Position.GetRandomPosition(), typeof(Position));
            }

            //Restore default values to not mess up other tests that rely on the static values for testing.
            ConsoleRenderer.ConsoleWidth = 10;
            ConsoleRenderer.ConsoleHeight = 10;
        }

        /// <summary>
        /// This test method checks that the method to see if a game object exists on a specific position in a gameworld works as expected.
        /// </summary>
        [TestMethod()]
        public void HasGameObjectTest()
        {
            GameWorld world = new GameWorld(10, 10, Difficulty.Easy);
            Player player = new Player('P', 'X', new Position(3, 2), world);
            Wall wall = new Wall('C', new Position(7, 8), world);
            world.AllObjects.Add(wall);

            bool expectedTest1 = true;
            bool expectedTest2 = true;
            bool expectedTest3 = false;
            bool expectedTest4 = false;

            bool actualTest1 = Position.HasGameObject(new Position(3, 2), world);
            bool actualTest2 = Position.HasGameObject(new Position(7, 8), world);
            bool actualTest3 = Position.HasGameObject(new Position(1, 2), world);
            bool actualTest4 = Position.HasGameObject(new Position(5, 4), world);

            Assert.AreEqual(expectedTest1, actualTest1);
            Assert.AreEqual(expectedTest2, actualTest2);
            Assert.AreEqual(expectedTest3, actualTest3);
            Assert.AreEqual(expectedTest4, actualTest4);
        }

    }
}
