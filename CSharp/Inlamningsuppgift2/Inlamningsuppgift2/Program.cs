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
        static Statistics playerStats;

        static void Loop(Difficulty difficulty)
        {
            // Initialisera spelet
            GameWorld world = new GameWorld(50, 20, difficulty);

            int frameRate = 5 * (int)difficulty;
            int frameCounter = 0;

            ConsoleRenderer renderer = new ConsoleRenderer(world);
            Player player = new Player('O', Position.GetRandomPosition(), world);
            player.Direction = Direction.None;
            world.AllObjects.Add(player);


            // TODO Skapa spelare och andra objekt etc. genom korrekta anrop till vår GameWorld-instans
            // ...

            // Huvudloopen
            bool running = true;
            while (running)
            {
                frameCounter++;
                // Kom ihåg vad klockan var i början
                DateTime before = DateTime.Now;

                // Hantera knapptryckningar från användaren
                char key = ReadKeyIfExists();
                switch (key)
                {
                    case 'W':
                        player.Direction = Direction.Up;
                        break;
                    case 'S':
                        player.Direction = Direction.Down;
                        break;
                    case 'A':
                        player.Direction = Direction.Left;
                        break;
                    case 'D':
                        player.Direction = Direction.Right;
                        break;
                    case 'Q':
                        running = false;
                        break;
                    default:
                        //player.Direction = Direction.None;
                        break;

                        // TODO Lägg till logik för andra knapptryckningar
                        // ...
                }

                // Uppdatera världen och rendera om
                renderer.RenderBlank();
                world.Update();
                renderer.Render();
                running = !world.GameIsOver;

                if (world.GameIsOver)
                {
                    playerStats = world.GetGameStats();
                }
                // Mät hur lång tid det tog
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
                        int.TryParse(difficultyInput, out int difficulty);

                        if (difficulty <= 0 || difficulty > 4)
                        {
                            Console.WriteLine("You didn't choose a difficulty.");
                            break;
                        }
                        Console.Clear();
                        Loop((Difficulty)difficulty);
                        Console.Clear();
                        PostGameStats();
                        break;

                    case "2":
                        break;

                    case "3":
                        PostGameInfo();
                        break;
                    case "4":
                        keepRunning = false;
                        break;
                }

            } while (keepRunning);
            // Vi kan ev. ha någon meny här, men annars börjar vi bara spelet direkt
            //Console.Clear();
        }
        static void PostGameInfo()
        {
            Console.WriteLine($"{GameInfo.Name} by {GameInfo.Author}\nCurrent version: {GameInfo.Version}\nGame Controls: Move with WASD.\nColliding with walls or yourself ends up in game over.\n\n");
        }
        static void PostGameStats()
        {
            Console.WriteLine($"Game over!\nYour score landed at: {playerStats.Score}\nYour snake length was: {playerStats.SnakeLength}\nYou played on difficulty {playerStats.DifficultyToString()}\n" +
                $"The game lasted for: {playerStats.TimePlayed} seconds. Your highest score ever is: {playerStats.HighestScore}\n\n");
        }
    }
}
