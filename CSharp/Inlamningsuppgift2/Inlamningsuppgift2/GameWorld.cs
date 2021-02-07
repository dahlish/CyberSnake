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
        private bool foodExists;

        public int SizeX { get => sizeX; }
        public int SizeY { get => sizeY; }
        public int Score { get => score; }
        public bool FoodExists { get => foodExists; }
        public List<GameObject> AllObjects { get => allObjects; }

        public GameWorld(int sizeX, int sizeY)
        {
            score = 0;
            allObjects = new List<GameObject>();
            this.sizeX = sizeX;
            this.sizeY = sizeY;
        }

        public void Update()
        {
            Console.Title = $"Cyberpunk 2077 v1.0 - Score: {score}";
            for (int i = allObjects.Count - 1; i >= 0; i--)
            {
                allObjects[i].Update();
            }
        }

        public void CreateFood()
        {
            Random rand = new Random();
            Food food = new Food('*', new Position(rand.Next(0, Console.WindowWidth), rand.Next(0, Console.WindowHeight)), this);
            allObjects.Add(food);
            foodExists = true;
        }

        public void IncreaseScore()
        {
            score++;
            foodExists = false;
        }
    }
}
