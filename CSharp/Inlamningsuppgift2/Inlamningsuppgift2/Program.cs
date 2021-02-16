﻿using System;
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

        static void Loop()
        {
            // Initialisera spelet
            const int frameRate = 5;
            GameWorld world = new GameWorld(50, 20);
            ConsoleRenderer renderer = new ConsoleRenderer(world);
            Player player = new Player('O', new Position(1, 1), world);
            world.AllObjects.Add(player);

            // TODO Skapa spelare och andra objekt etc. genom korrekta anrop till vår GameWorld-instans
            // ...

            // Huvudloopen
            Stopwatch watch = new Stopwatch();
            watch.Start();
            bool running = true;
            while (running)
            {
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

                // Mät hur lång tid det tog
                double frameTime = Math.Ceiling((1000.0 / frameRate) - (DateTime.Now - before).TotalMilliseconds);
                world.ElapsedTime += (float)(frameTime + frameTime) * 0.001f;

                if (frameTime > 0)
                {
                    // Vänta rätt antal millisekunder innan loopens nästa varv
                    Console.Title = $"CyberSnake 2077 v1.1 - Time: {MathF.Round(world.ElapsedTime, 1)}  Score: {world.Score} Actual: {(double)(watch.ElapsedMilliseconds / 1000.0f)}";
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
