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
        private int foodAmount;
        private int foodMax;
        private bool specialFoodSpawnedAlready = false;
        private float timeLastFoodEaten;

        public int SizeX { get => sizeX; }
        public int SizeY { get => sizeY; }
        public int Score { get => score; }
        public int FoodAmount { get => foodAmount; set => foodAmount = value; }
        public int FoodMax { get => foodMax; set => foodMax = value; }
        public float ElapsedTime { get => elapsedTime; }
        public float TimeLastFoodEaten { get => timeLastFoodEaten; set => timeLastFoodEaten = value; }
        public List<GameObject> AllObjects { get => allObjects; }

        public GameWorld(int sizeX, int sizeY)
        {
            score = 0;
            allObjects = new List<GameObject>();
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            elapsedTime = 0;
            foodMax = 1;
            FoodAmount = 0;
        }

        public void Update()
        {
            CallUpdateAllGameObjects();

            if (foodAmount < foodMax)
            {
                CreateFood();
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
            if (score % 3 == 0 && score != 0 && !specialFoodSpawnedAlready)
            {
                specialFoodSpawnedAlready = true;
                Food food = Food.Create('+', Position.GetRandomPosition(), this, FoodType.Special, 7);
                Food foodNormal = Food.Create('*', Position.GetRandomPosition(), this, FoodType.Normal);
                allObjects.Add(food);
                allObjects.Add(foodNormal);
                foodAmount++;
            }
            else
            {
                Food food = Food.Create('*', Position.GetRandomPosition(), this, FoodType.Normal);
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

            if (timeLastFoodEaten + elapsedTime >= 30)
            {
                GameOver();
            }
        }

        public void GameOver()
        {
            throw new NotImplementedException();
        }
    }
}
