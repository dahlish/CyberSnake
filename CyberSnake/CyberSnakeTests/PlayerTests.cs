using Microsoft.VisualStudio.TestTools.UnitTesting;
using CyberSnake;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyberSnake.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        /// <summary>
        /// This test method checks that the player moves properly when using the Move() method.
        /// If the player hits the console edge, it will be transferred to the other side etc.
        /// </summary>
        [TestMethod()]
        public void MoveTest()
        {
            GameWorld world = new GameWorld(10, 10, Difficulty.Easy);
            Player player = new Player('8', '0', new Position(1, 2), world);
            player.Direction = Direction.Down;
            player.Update();
            Position expectedPosTest1 = new Position(1, 3);
            Position actualPosTest1 = player.Position;
            Assert.AreEqual(expectedPosTest1, actualPosTest1);

            player.Direction = Direction.Up;
            player.Update();
            Position expectedPosTest2 = new Position(1, 2);
            Position actualPosTest2 = player.Position;
            Assert.AreEqual(expectedPosTest2, actualPosTest2);

            player.Direction = Direction.Left;
            player.Update();
            Position expectedPosTest3 = new Position(0, 2);
            Position actualPosTest3 = player.Position;
            Assert.AreEqual(expectedPosTest3, actualPosTest3);

            player.Direction = Direction.Left;
            player.Update();
            Position expectedPosTest4 = new Position(ConsoleRenderer.ConsoleWidth - 1, 2);
            Position actualPosTest4 = player.Position;
            Assert.AreEqual(expectedPosTest4, actualPosTest4);

            player.Direction = Direction.Right;
            player.Update();
            Position expectedPosTest5 = new Position(0, 2);
            Position actualPosTest5 = player.Position;
            Assert.AreEqual(expectedPosTest5, actualPosTest5);
        }
    }
}
