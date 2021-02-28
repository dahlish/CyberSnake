using Microsoft.VisualStudio.TestTools.UnitTesting;
using CyberSnake;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyberSnake.Tests
{
    [TestClass()]
    public class GameObjectTests
    {
        /// <summary>
        /// This test tests that the collision between a Wall and a Player works. In this case, we test that the player is in the same place as the wall, and that the event OnCollision is fired by checking that 
        /// world.GameIsOver is set to true.
        /// </summary>
        [TestMethod()]
        public void UpdateTest()
        {
            GameWorld world = new GameWorld(10, 10, Difficulty.Easy);

            Player player = new Player('O', '0', new Position(3, 2), world);
            player.Direction = Direction.None;

            Wall wall = new Wall('X', new Position(3, 2), world);
            world.AllObjects.Add(wall);

            Assert.IsTrue(player.Position == wall.Position);

            player.Update();
            wall.Update();

            Assert.IsTrue(world.GameIsOver);

        }
    }
}