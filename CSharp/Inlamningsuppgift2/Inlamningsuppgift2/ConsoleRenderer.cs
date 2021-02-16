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
    }
}
