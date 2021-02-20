using System;
using System.Collections.Generic;
using System.Text;

namespace Inlamningsuppgift2
{
    public class GameWorld
    {
        private int sizeX;
        private int sizeY;
        private int score;
        private List<GameObject> allObjects;
        private float elapsedTime;
        private int foodAmount = 0;
        private int foodMax = 1;
        private bool specialFoodSpawnedAlready = false;
        private float timeLastFoodEaten;
        private bool wallsCreated = false;
        private Difficulty difficulty;
        private int amountOfWallGroups = 0;
        private int wallGroupMaxAmount = 0;
        private Statistics gameStats;
        private bool gameIsOver = false;
        private int spawnSpecialFoodAtScore = 5;
        private int starvationTime = 30;

        public int SizeX { get => sizeX; }
        public int SizeY { get => sizeY; }
        public int Score { get => score; }
        public int FoodAmount { get => foodAmount; set => foodAmount = value; }
        public int FoodMax { get => foodMax; set => foodMax = value; }
        public float ElapsedTime { get => elapsedTime; }
        public float TimeLastFoodEaten { get => timeLastFoodEaten; set => timeLastFoodEaten = value; }
        public List<GameObject> AllObjects { get => allObjects; }
        public Difficulty Difficulty { get => difficulty; }
        public bool GameIsOver { get => gameIsOver; }
        public int StarvationTime { get => starvationTime; }

        public GameWorld(int sizeX, int sizeY, Difficulty difficulty, bool wallsCreated = true)
        {
            this.wallsCreated = wallsCreated;
            score = 0;
            allObjects = new List<GameObject>();
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            elapsedTime = 0;
            this.difficulty = difficulty;

            switch(difficulty)
            {
                case Difficulty.Easy:
                    amountOfWallGroups = 0;
                    wallGroupMaxAmount = 0;
                    starvationTime = 30;
                    break;
                case Difficulty.Medium:
                    amountOfWallGroups = 2;
                    wallGroupMaxAmount = 3;
                    starvationTime = 20;
                    break;
                case Difficulty.Hard:
                    amountOfWallGroups = 4;
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

        public void Update()
        {
            CallUpdateAllGameObjects();

            if (foodAmount < foodMax)
            {
                CreateFood();
            }

            if (!wallsCreated)
            {
                for (int i = 0; i < amountOfWallGroups; i++)
                {
                    WallGenerator walls = new WallGenerator(wallGroupMaxAmount, 'X', this, Position.GetRandomPosition());
                    walls.Generate();
                }
                wallsCreated = true;
            }
        }

        public void CallUpdateAllGameObjects()
        {
            for (int i = allObjects.Count - 1; i >= 0; i--)
            {
                allObjects[i].Update();
            }
        }

        public void CreateFood()
        {
            Random rand = new Random();
            if (score >= spawnSpecialFoodAtScore && score != 0 && !specialFoodSpawnedAlready)
            {
                specialFoodSpawnedAlready = true;
                Food food = Food.Create('+', Position.GetRandomPosition(), this, FoodType.Special, 8 - (int)difficulty);
                Food foodNormal = Food.Create('*', Position.GetRandomPosition(), this, FoodType.Normal);
                allObjects.Add(food);
                allObjects.Add(foodNormal);
                spawnSpecialFoodAtScore = score + rand.Next(5, 20);
                foodAmount++;
            }
            else
            {
                Food food = Food.Create('*', Position.GetRandomPosition(), this, FoodType.Normal, 12 - (int)difficulty);
                allObjects.Add(food);
                specialFoodSpawnedAlready = false;
            }

            foodAmount++;
        }

        public void IncreaseScore(int amount)
        {
            score += amount;
        }

        public void TimeElapsedTick()
        {
            elapsedTime += 1;

            if (elapsedTime - timeLastFoodEaten >= starvationTime)
            {
                GameOver();
            }
        }

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

        public void GameOver()
        {
            gameIsOver = true;
        }

        public Statistics GetGameStats()
        {
            return new Statistics(Score, GetPlayer().Tail.Count, Difficulty, ElapsedTime);
        }
    }

    public enum Difficulty
    {
        Easy = 1,
        Medium = 2,
        Hard = 3,
        VeryHard = 4
    }

    public enum Direction
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3,
        None = 4
    }
}
