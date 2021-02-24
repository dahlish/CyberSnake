using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CyberSnake
{
    /// <summary>
    /// This class contains the majority of the game data during gameplay.
    /// </summary>
    public class GameWorld
    {
        private int sizeX;
        private int sizeY;
        private float timeLastFoodEaten;
        private float elapsedTime = 0;
        private int spawnSpecialFoodAtScore = 5;
        private int amountOfWallGroups = 0;
        private int wallGroupMaxAmount = 0;
        private int starvationTime = 30;
        private int foodAmount = 0;
        private int foodMax = 1;
        private int score = 0;
        private bool specialFoodSpawnedAlready = false;
        private bool createWalls = true;
        private bool gameIsOver = false;
        private Difficulty difficulty;
        private Statistics gameStatistics;
        private List<GameObject> allObjects = new List<GameObject>();

        public int SizeX { get => sizeX; }
        public int SizeY { get => sizeY; }
        public int Score { get => score; }
        public int FoodAmount { get => foodAmount; set => foodAmount = value; }
        public int FoodMax { get => foodMax; }
        public int StarvationTime { get => starvationTime; }
        public bool GameIsOver { get => gameIsOver; }
        public float ElapsedTime { get => elapsedTime; }
        public float TimeLastFoodEaten { get => timeLastFoodEaten; set => timeLastFoodEaten = value; }
        public Difficulty Difficulty { get => difficulty; }
        public Statistics GameStatistics { get => gameStatistics; }
        public List<GameObject> AllObjects { get => allObjects; }

        /// <summary>
        /// Creates a new GameWorld object with a set of parameters to determine the game settings.
        /// </summary>
        /// <param name="sizeX">The width of the game screen.</param>
        /// <param name="sizeY">The size of the game screen.</param>
        /// <param name="difficulty">The difficulty setting of the game.</param>
        /// <param name="wallsCreated">A cheat. If you want no walls to be generated, you set this to false.</param>
        public GameWorld(int sizeX, int sizeY, Difficulty difficulty, bool createWalls = true)
        {
            this.createWalls = createWalls;
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.difficulty = difficulty;

            switch(difficulty)
            {
                case Difficulty.Easy:
                    amountOfWallGroups = 0;
                    wallGroupMaxAmount = 0;
                    starvationTime = 30;
                    break;
                case Difficulty.Medium:
                    amountOfWallGroups = 6;
                    wallGroupMaxAmount = 3;
                    starvationTime = 20;
                    break;
                case Difficulty.Hard:
                    amountOfWallGroups = 6;
                    wallGroupMaxAmount = 7;
                    starvationTime = 15;
                    break;
                case Difficulty.VeryHard:
                    amountOfWallGroups = 6;
                    wallGroupMaxAmount = 10;
                    starvationTime = 10;
                    break;
            }
        }

        /// <summary>
        /// This method is called before the first update.
        /// </summary>
        public void Start()
        {
            if (createWalls)
            {
                for (int i = 0; i < amountOfWallGroups; i++)
                {
                    WallGenerator walls = new WallGenerator(wallGroupMaxAmount, 'X', this, Position.GetRandomPositionAvailable(this));
                    walls.Generate();
                }
            }
        }

        /// <summary>
        /// Update is called whenever a new frame is generated in the Program.Loop() method.
        /// This method also generates all the walls that are to be placed in the world.
        /// </summary>
        public void Update()
        {
            CallUpdateOnAllGameObjects();

            if (foodAmount < foodMax)
            {
                CreateFood();
            }
        }

        /// <summary>
        /// This method goes through every single GameObject in the game world and calls the Update() method on them.
        /// </summary>
        public void CallUpdateOnAllGameObjects()
        {
            for (int i = allObjects.Count - 1; i >= 0; i--)
            {
                allObjects[i].Update();
            }
        }

        /// <summary>
        /// Generates a new Food object in the game world.
        /// </summary>
        public void CreateFood()
        {
            Random rand = new Random();
            if (score >= spawnSpecialFoodAtScore && score != 0 && !specialFoodSpawnedAlready)
            {
                specialFoodSpawnedAlready = true;
                Food food = Food.Create('+', Position.GetRandomPositionAvailable(this), this, FoodType.Special, 8 - (int)difficulty);
                Food foodNormal = Food.Create('*', Position.GetRandomPositionAvailable(this), this, FoodType.Normal);
                spawnSpecialFoodAtScore = score + rand.Next(5, 20);
            }
            else
            {
                Food food = Food.Create('*', Position.GetRandomPositionAvailable(this), this, FoodType.Normal, 12 - (int)difficulty);
                specialFoodSpawnedAlready = false;
            }
        }

        /// <summary>
        /// Increases the player score in the game by the amount specified in the amount parameter.
        /// </summary>
        /// <param name="amount">Amount to increase the score with.</param>
        public void IncreaseScore(int amount)
        {
            score += amount;
        }

        /// <summary>
        /// This is called approximately every second in the Program.Loop() method to keep track of the total time the game has been running.
        /// </summary>
        public void TimeElapsedTick()
        {
            elapsedTime += 1;

            if (elapsedTime - timeLastFoodEaten >= starvationTime)
            {
                GameOver();
            }
        }

        /// <summary>
        /// Attempts to find a Player object present in the game world.
        /// </summary>
        /// <returns>Returns the gameobject as a Player, else it returns null.</returns>
        public Player GetPlayer()
        {
            for (int i = allObjects.Count - 1; i >= 0; i--)
            {
                if (allObjects[i] is Player)
                {
                    return allObjects[i] as Player;
                }
            }

            return null;
        }

        /// <summary>
        /// Ends the game and posts statistics to the gameStatistics field.
        /// </summary>
        public void GameOver()
        {
            gameIsOver = true;
            UpdateGameStats();
        }

        /// <summary>
        /// Whenever this method is called, the gameStatistics field will be populated with the current game data, such as score, tail count, difficulty and elapsed time.
        /// Does nothing if there is no Player in the game world.
        /// </summary>
        public void UpdateGameStats()
        {
            Player player = GetPlayer();
            if (player != null)
            {
                gameStatistics = new Statistics(Score, player.Tail.Count, Difficulty, ElapsedTime);
            }
        }
    }

    /// <summary>
    /// Represents the Difficulty of the game.
    /// </summary>
    public enum Difficulty
    {
        Easy = 1,
        Medium = 2,
        Hard = 3,
        VeryHard = 4
    }
}
