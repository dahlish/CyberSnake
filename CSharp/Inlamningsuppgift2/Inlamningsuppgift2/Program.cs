using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace Inlamningsuppgift2
{
    class Program
    {
        /// <summary>
        /// Checks Console to see if a keyboard key has been pressed, if so returns it as uppercase, otherwise returns '\0'.
        /// </summary>
        static char ReadKeyIfExists() => Console.KeyAvailable ? Console.ReadKey(intercept: true).Key.ToString().ToUpper()[0] : '\0';
        static bool doNotRenderWalls = false;
        static Statistics playerStats;

        static void Loop(Difficulty difficulty)
        {
            // Initialisera spelet
            GameWorld world = new GameWorld(50, 20, difficulty, doNotRenderWalls);

            int frameRate = 5 * (int)difficulty;
            int frameCounter = 0;

            ConsoleRenderer renderer = new ConsoleRenderer(world);
            Player player = new Player('8', Position.GetRandomPosition(), world);
            player.Direction = Direction.None;
            world.AllObjects.Add(player);

            bool running = true;
            while (running)
            {
                frameCounter++;
                DateTime before = DateTime.Now;

                char key = ReadKeyIfExists();
                switch (key)
                {
                    case 'W':
                        if (player.Direction != Direction.Down)
                        {
                            player.Direction = Direction.Up;
                        }
                        break;
                    case 'S':
                        if (player.Direction != Direction.Up)
                        {
                            player.Direction = Direction.Down;
                        }
                        break;
                    case 'A':
                        if (player.Direction != Direction.Right)
                        {
                            player.Direction = Direction.Left;
                        }
                        break;
                    case 'D':
                        if (player.Direction != Direction.Left)
                        {
                            player.Direction = Direction.Right;
                        }
                        break;
                    case 'Q':
                        running = false;
                        world.GameOver();
                        break;
                }

                renderer.RenderBlank();
                world.Update();
                renderer.Render();
                running = !world.GameIsOver;

                if (world.GameIsOver)
                {
                    playerStats = world.GetGameStats();
                }

                double frameTime = Math.Ceiling((1000.0 / frameRate) - (DateTime.Now - before).TotalMilliseconds);

                if (frameCounter >= frameRate)
                {
                    world.TimeElapsedTick();
                    frameCounter = 0;
                }

                if (frameTime > 0)
                {
                    // Vänta rätt antal millisekunder innan loopens nästa varv
                    Console.Title = $"CyberSnake 2077 v{GameInfo.Version}";
                    Thread.Sleep((int)frameTime);
                }

            }
        }

        static void Main(string[] args)
        {
            bool keepRunning = true;
            do
            {
                Console.WriteLine($"Welcome to CyberSnake 2077. \n1. New Game\n2. Highest Score\n3. Help\n4. Exit");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Choose a difficulty: \n1. Easy\n2. Medium\n3. Hard\n4. Very Hard");
                        string difficultyInput = Console.ReadLine();
                        if (difficultyInput != "nowalls")
                        {
                            int.TryParse(difficultyInput, out int difficulty);

                            if (difficulty <= 0 || difficulty > 4)
                            {
                                Console.WriteLine("You didn't choose a difficulty.");
                                break;
                            }
                            Console.Clear();
                            Loop((Difficulty)difficulty);
                        }
                        else
                        {
                            Console.Clear();
                            doNotRenderWalls = true;
                            Loop(Difficulty.VeryHard);
                            doNotRenderWalls = false;
                        }
                        Console.Clear();
                        PostGameStats();
                        break;
                    case "2":
                        PostHighScore();
                        break;
                    case "3":
                        PostGameInfo();
                        break;
                    case "4":
                        keepRunning = false;
                        break;
                }

            } while (keepRunning);
        }
        static void PostHighScore()
        {
            Console.WriteLine($"Your highest score is {Statistics.GetHighestScore()}.");
        }
        static void PostGameInfo()
        {
            Console.WriteLine($"{GameInfo.Name} by {GameInfo.Author}\nCurrent version: {GameInfo.Version}\nGame Controls: Move with WASD.\nColliding with walls or yourself ends up in game over.\n" +
                $"Press Q at any time to end the game.\n\n");
        }
        static void PostGameStats()
        {
            Console.WriteLine($"Game over!\nYour score landed at: {playerStats.Score}\nYour snake length was: {playerStats.SnakeLength}\nYou played on difficulty {playerStats.DifficultyToString()}\n" +
                $"The game lasted for: {playerStats.TimePlayed} seconds. Your highest score ever is: {playerStats.HighestScore}\n\n");
        }
    }
}
