using Microsoft.VisualStudio.TestTools.UnitTesting;
using CyberSnake;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyberSnake.Tests
{
    [TestClass()]
    public class GameWorldTests
    {
        /// <summary>
        /// This test method will check if the score increment is correct.
        /// </summary>
        [TestMethod()]
        public void IncreaseScoreTest()
        {
            GameWorld world = new GameWorld(10, 10, Difficulty.Easy);

            world.IncreaseScore(4);
            world.IncreaseScore(7);

            int expectedTest1 = 11;
            int actualTest1 = world.Score;

            Assert.AreEqual(expectedTest1, actualTest1);

            world = new GameWorld(10, 10, Difficulty.Easy);

            world.IncreaseScore(4);
            world.IncreaseScore(-3);

            int expectedTest2 = 1;
            int actualTest2 = world.Score;

            Assert.AreEqual(expectedTest2, actualTest2);
        }

        /// <summary>
        /// This test method will make sure that the elapsed time increases as it should and that the game ends when the starvation timer is met.
        /// </summary>
        [TestMethod()]
        public void TimeElapsedTickTest()
        {
            GameWorld world = new GameWorld(10, 10, Difficulty.VeryHard); //This sets the starvation timer to 10.
            world.TimeLastFoodEaten = 0;

            world.TimeElapsedTick();
            world.TimeElapsedTick();
            float timeElapsedExpectedTest1 = 2;
            float timeElapsedActualTest1 = world.ElapsedTime;
            Assert.AreEqual(timeElapsedExpectedTest1, timeElapsedActualTest1);
            world.TimeElapsedTick();
            world.TimeElapsedTick();
            world.TimeElapsedTick();
            world.TimeElapsedTick();
            world.TimeElapsedTick();
            world.TimeElapsedTick();
            world.TimeElapsedTick();
            float timeElapsedExpectedTest2 = 9;
            float timeElapsedActualTest2 = world.ElapsedTime;
            Assert.AreEqual(timeElapsedExpectedTest2, timeElapsedActualTest2);

            world.TimeElapsedTick();
            bool gameOverExpected = true;
            bool gameOverActual = world.GameIsOver;
            Assert.AreEqual(gameOverExpected, gameOverActual);

            float timeElapsedExpectedTest3 = 10;
            float timeElapsedActualTest3 = world.ElapsedTime;
            Assert.AreEqual(timeElapsedExpectedTest3, timeElapsedActualTest3);

        }

        /// <summary>
        /// This method tests that the GetPlayer method works by returning null if allObjects in a GameWorld does not contain a Player object.
        /// </summary>
        [TestMethod()]
        public void GetPlayerTest()
        {
            GameWorld world1 = new GameWorld(10, 10, Difficulty.VeryHard); //This sets the starvation timer to 10.
            Player player = new Player('P', 'X', new Position(3, 2), world1);
            Assert.IsNotNull(world1.GetPlayer());

            GameWorld world2 = new GameWorld(10, 10, Difficulty.VeryHard);
            Assert.IsNull(world2.GetPlayer());
        }

        /// <summary>
        /// This method tests that all game stats are generated properly after a game is over.
        /// </summary>
        [TestMethod()]
        public void UpdateGameStatsTest()
        {
            GameWorld world = new GameWorld(10, 10, Difficulty.Easy);
            Player player = new Player('P', 'X', new Position(3, 2), world);
            world.IncreaseScore(5);
            world.TimeElapsedTick();
            world.TimeElapsedTick();
            world.TimeElapsedTick();
            world.GameOver();
            Statistics expected = new Statistics(5, 0, Difficulty.Easy, 3);
            Statistics actual = world.GameStatistics;

            Assert.AreEqual(true, world.GameIsOver);
            Assert.AreEqual(expected.Score, actual.Score);
            Assert.AreEqual(expected.SnakeLength, actual.SnakeLength);
            Assert.AreEqual(expected.DifficultyPlayed, actual.DifficultyPlayed);
            Assert.AreEqual(expected.TimePlayed, actual.TimePlayed);
        }

        /// <summary>
        /// This method tests that food is created properly within the CreateFood method and that the food counter increases.
        /// </summary>
        [TestMethod()]
        public void CreateFoodTest()
        {
            GameWorld world = new GameWorld(10, 10, Difficulty.Easy);
            world.CreateFood();
            bool existExpected = true;
            bool existActual = false;
            int foodCounterExpected = 1;
            int foodCounterActual = world.FoodAmount;
            foreach(var item in world.AllObjects)
            {
                if (item is Food)
                {
                    existActual = true;
                }
            }

            Assert.AreEqual(existExpected, existActual);
            Assert.AreEqual(foodCounterExpected, foodCounterActual);
        
        }
    }
}