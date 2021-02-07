using System;
using System.Collections.Generic;
using System.Threading;

namespace Inlamningsuppgift2
{
    class Program
    {
        /// <summary>
        /// Checks Console to see if a keyboard key has been pressed, if so returns it as uppercase, otherwise returns '\0'.
        /// </summary>
        static char ReadKeyIfExists() => Console.KeyAvailable ? Console.ReadKey(intercept: true).Key.ToString().ToUpper()[0] : '\0';

        static void Loop()
        {
            // Initialisera spelet
            const int frameRate = 30;
            GameWorld world = new GameWorld(50, 20);
            ConsoleRenderer renderer = new ConsoleRenderer(world);
            Player player = new Player('O', new Position(1, 1), world);
            world.AllObjects.Add(player);

            // TODO Skapa spelare och andra objekt etc. genom korrekta anrop till vår GameWorld-instans
            // ...

            // Huvudloopen
            bool running = true;
            while (running)
            {
                if (!world.FoodExists)
                {
                    world.CreateFood();
                }
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
                        player.Direction = Direction.None;
                        break;

                        // TODO Lägg till logik för andra knapptryckningar
                        // ...
                }

                // Uppdatera världen och rendera om
                renderer.RenderBlank();
                world.Update();
                renderer.Render();

                // Mät hur lång tid det tog
                double frameTime = Math.Ceiling((1000.0 / frameRate) - (DateTime.Now - before).TotalMilliseconds);
                if (frameTime > 0)
                {
                    // Vänta rätt antal millisekunder innan loopens nästa varv
                    Thread.Sleep((int)frameTime);
                }
            }
        }

        static void Main(string[] args)
        {
            // Vi kan ev. ha någon meny här, men annars börjar vi bara spelet direkt
            Loop();
        }
    }

    public enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right
    }
}
