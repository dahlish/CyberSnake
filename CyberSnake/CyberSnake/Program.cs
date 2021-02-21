using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace CyberSnake
{
    class Program
    {
        static bool doNotRenderWalls = false;
        static Statistics playerStats;

        /// <summary>
        /// Checks Console to see if a keyboard key has been pressed, if so returns it as uppercase, otherwise returns '\0'.
        /// </summary>
        static char ReadKeyIfExists() => Console.KeyAvailable ? Console.ReadKey(intercept: true).Key.ToString().ToUpper()[0] : '\0';


        /// <summary>
        /// Starts the game. This will continue to run until a game has ended, which happens once the local variable running is set to false.
        /// </summary>
        /// <param name="difficulty">Represents the game difficulty setting.</param>
        static void Loop(Difficulty difficulty)
        {
            GameWorld world = new GameWorld(50, 20, difficulty, doNotRenderWalls);

            int frameRate = 5 * (int)difficulty;
            int frameCounter = 0;

            ConsoleRenderer renderer = new ConsoleRenderer(world);
            Player player = new Player('8', 'O', Position.GetRandomPositionAvailable(world), world);
            player.Direction = Direction.None;

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
                    playerStats = world.GameStatistics;
                }

                double frameTime = Math.Ceiling((1000.0 / frameRate) - (DateTime.Now - before).TotalMilliseconds);

                if (frameCounter >= frameRate)
                {
                    world.TimeElapsedTick();
                    frameCounter = 0;
                }

                if (frameTime > 0)
                {
                    Thread.Sleep((int)frameTime);
                }

            }
        }

        static void Main(string[] args)
        {
            Console.Title = $"CyberSnake 2077 - Main Menu";
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

        /// <summary>
        /// Fetches the current highest score from the Statistics struct and writes it to the console.
        /// </summary>
        static void PostHighScore()
        {
            Console.WriteLine($"Your highest score is {Statistics.GetHighestScore()}.\n\n");
        }
        /// <summary>
        /// Fetches the game information from the GameInfo class and writes it to the console. Posts game name, author and version.
        /// </summary>
        static void PostGameInfo()
        {
            Console.WriteLine($"{GameInfo.Name} by {GameInfo.Author}\nCurrent version: {GameInfo.Version}\nGame Controls: Move with WASD.\nColliding with walls or yourself ends up in game over.\n" +
                $"Press Q at any time to end the game.\n\n");
        }
        /// <summary>
        /// Fetches the latest game stats and posts it to the console. This method is called after a game is finished.
        /// </summary>
        static void PostGameStats()
        {
            Console.WriteLine($"Game over!\nYour score landed at: {playerStats.Score}\nYour snake length was: {playerStats.SnakeLength}\nYou played on difficulty {playerStats.DifficultyToString(playerStats.DifficultyPlayed)}\n" +
                $"The game lasted for: {playerStats.TimePlayed} seconds. Your highest score ever is: {playerStats.HighestScore}\n\n");
        }
    }
}
