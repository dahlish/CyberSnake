using System;
using System.Collections.Generic;
using System.Text;

namespace CyberSnake
{
    /// <summary>
    /// This class controls everything needed to render the game world.
    /// </summary>
    public class ConsoleRenderer
    {
        private GameWorld world;

        //Varför jag lade till dessa statiska värden i ConsoleRenderer var för att det var svårt att testa mina metoder som berodde på 
        //Console.WindowHeight/Console.WindowWidth då det i testerna inte finns någon konsol. Så dessa defaultvärden overrideas när ConsoleRenderer skapas, men defaultar
        //till nedan värden så det också kan testas :) Finns det bättre sätt eller är detta det rimligaste sättet?
        private static int consoleWidth = 10;
        private static int consoleHeight = 10;
        private static int consoleMaximumWidth = 50;
        private static int consoleMaximumHeight = 50;

        public static int ConsoleWidth { get => consoleWidth; set => consoleWidth = value; }
        public static int ConsoleHeight { get => consoleHeight; set => consoleHeight = value; }
        public static int ConsoleMaximumWidth { get => consoleMaximumWidth; set => consoleMaximumWidth = value; }
        public static int ConsoleMaximumHeight { get => consoleMaximumHeight; set => consoleMaximumHeight = value; }
        /// <summary>
        /// Generates a new ConsoleRenderer object that can be used to render the gameworld. If the gameWorld size is larger than the maximum allowed size
        /// for the Console, it will be set to the maximum size.
        /// </summary>
        /// <param name="gameWorld">The game world to be rendered.</param>
        public ConsoleRenderer(GameWorld gameWorld)
        {
            Console.CursorVisible = false;
            world = gameWorld;
            Console.SetWindowSize(1, 1);
            try
            {
                Console.SetBufferSize(gameWorld.SizeX, gameWorld.SizeY);
                Console.SetWindowSize(gameWorld.SizeX, gameWorld.SizeY);
                consoleWidth = Console.WindowWidth;
                consoleHeight = Console.WindowHeight;
                consoleMaximumWidth = Console.LargestWindowWidth;
                consoleMaximumHeight = Console.LargestWindowHeight;
            }
            catch(ArgumentOutOfRangeException)
            {
                Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                ConsoleWidth = Console.WindowWidth;
                ConsoleHeight = Console.WindowHeight;
                ConsoleMaximumWidth = Console.LargestWindowWidth;
                ConsoleMaximumHeight = Console.LargestWindowHeight;
            }
        }
        
        /// <summary>
        /// This method is called when a new frame needs to be rendered to the Console.
        /// It sets the cursor position to the GameObject Position property and writes the appearance to that position.
        /// We also render the User Interface here.
        /// </summary>
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

                    if (!item.IsStatic || (item.IsStatic && !item.HasRendered))
                    {
                        Console.SetCursorPosition(item.Position.X, item.Position.Y);
                        Console.Write(itemAppearance.Appearance);
                        item.HasRendered = true;
                    }
                    
                }
            }
        }

        /// <summary>
        /// This method is called before the update and the next Render to prevent screen flickering.
        /// </summary>
        public void RenderBlank()  
        {
            //Har inte riktigt förstått hur den fungerar, men kan det vara för att eftersom vi rör på oss så behöver vi återställa varje "plats" där
            //och därmed blinkar det inte?

            Console.SetCursorPosition(0, 0);
            RenderUserInterface();
            ResetConsoleColors();

            foreach (var item in world.AllObjects)
            {
                if (item is IRenderable)
                {
                    var itemAppearance = item as IRenderable;
                    if (!item.IsStatic)
                    {
                        Console.SetCursorPosition(item.Position.X, item.Position.Y);
                        Console.Write(' ');
                    }
                }
            }
        }

        /// <summary>
        /// Renders the user interface on the first row of the console. It writes out the score and the starvation timer with a different background color to make sure that the player is
        /// not confusing the UI row with the play area.
        /// </summary>
        private void RenderUserInterface()
        {
            int windowWidth = ConsoleWidth;
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("{0, -" + windowWidth + "}", $"Score: {world.Score} | Starvation Timer ({world.StarvationTime}): " + (world.ElapsedTime - world.TimeLastFoodEaten));
        }

        /// <summary>
        /// Sets the Console colors back to its original black and white.
        /// </summary>
        private void ResetConsoleColors()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        
        /// <summary>
        /// Checks if the parameter Position value is out of bounds for rendering.
        /// </summary>
        /// <param name="position"></param>
        /// <returns>Returns true if it is out of bounds, else it returns false.</returns>
        public static bool IsOutOfBounds(Position position)
        {
            if (position.X < 0)
            {
                return true;
            }
            else if (position.X >= ConsoleWidth)
            {
                return true;
            }

            if (position.Y < 1) //1 because the first Y row is dedicated to user interface.
            {
                return true;
            }
            else if (position.Y >= ConsoleHeight)
            {
                return true;
            }

            return false;
        }
    }
}
