using Microsoft.VisualStudio.TestTools.UnitTesting;
using CyberSnake;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyberSnake.Tests
{
    [TestClass()]
    public class WallGeneratorTests
    {
        [TestMethod()]
        public void GenerateTest()
        {

            //Test 1 checks if the position is randomized since it is out of bounds.
            WallGenerator generatorTest1 = new WallGenerator(1, 'X', new GameWorld(10, 10, Difficulty.Easy), new Position(-3, -4));
            Position notExpectedTest1 = new Position(-3, -4);
            Position actualTest1 = generatorTest1.Position;

            Assert.AreNotEqual(notExpectedTest1, actualTest1);


            //Test 2 checks if the position is in the correct position when placed within bounds.
            WallGenerator generatorTest2 = new WallGenerator(1, 'X', new GameWorld(10, 10, Difficulty.Easy), new Position(9, 9));
            Position expectedTest2 = new Position(9, 9);
            Position actualTest2 = generatorTest2.Position;

            Assert.AreEqual(expectedTest2, actualTest2);


            //Test 3 checks if the position is randomized since one of the values are out of bounds.
            WallGenerator generatorTest3 = new WallGenerator(1, 'X', new GameWorld(10, 10, Difficulty.Easy), new Position(152, 9));
            Position notExpectedTest3 = new Position(152, 9);
            Position actualTest3 = generatorTest3.Position;

            Assert.AreNotEqual(notExpectedTest3, actualTest3);
        }
    }
}