using System;
using System.Collections.Generic;
using System.Text;

namespace Inlamningsuppgift2
{
    class ConsoleRenderer
    {
        private GameWorld world;
        public ConsoleRenderer(GameWorld gameWorld)
        {
            Console.CursorVisible = false;
            world = gameWorld;
            Console.SetWindowSize(1, 1);
            try
            {
                Console.SetBufferSize(gameWorld.SizeX, gameWorld.SizeY);
                Console.SetWindowSize(gameWorld.SizeX, gameWorld.SizeY);
            }
            catch(ArgumentOutOfRangeException)
            {
                Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            }
        }

        public void Render()
        {
            Console.SetCursorPosition(0, 0);
            RenderUserInterface();
            ResetConsoleColors();

            foreach (var item in world.AllObjects)
            {
                if (item is IRenderable)
                {
                    var itemAppearance = item as IRenderable;
                    Console.SetCursorPosition(item.Position.X, item.Position.Y);
                    Console.Write(itemAppearance.Appearance);
                }
            }
        }

        public void RenderBlank()
        {
            Console.SetCursorPosition(0, 0);

            foreach (var item in world.AllObjects)
            {
                if (item is IRenderable)
                {
                    var itemAppearance = item as IRenderable;
                    Console.SetCursorPosition(item.Position.X, item.Position.Y);
                    Console.Write(' ');
                }
            }
        }

        public void RenderUserInterface()
        {
            int windowWidth = Console.WindowWidth;
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("{0, -" + windowWidth + "}", "Score: " + world.Score);
        }
        public void ResetConsoleColors()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
