using System;
using System.Collections.Generic;
using System.Text;

namespace CyberSnake
{
    /// <summary>
    /// All GameObjects need a Position to navigate through the world.
    /// </summary>
    public struct Position
    {
        private int x;
        private int y;

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        /// <summary>
        /// Creates a new position based on parameter x and y.
        /// </summary>
        /// <param name="x">The position on the x-scale.</param>
        /// <param name="y">The position on the y-scale.</param>
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// CHecks if two positions are the same.
        /// </summary>
        /// <param name="p1">First position to compare.</param>
        /// <param name="p2">Second position to compare.</param>
        /// <returns>Returns true if the positions are the same, else it returns false.</returns>
        public static bool operator ==(Position p1, Position p2)
        {
            if (p1.x == p2.x && p1.y == p2.y)
            {
                return true;
            }
            else return false;
        }
        /// <summary>
        /// CHecks if two positions are not the same.
        /// </summary>
        /// <param name="p1">First position to compare.</param>
        /// <param name="p2">Second position to compare.</param>
        /// <returns>Returns false if the positions are not the same, else it returns true.</returns>
        public static bool operator !=(Position p1, Position p2)
        {
            if (p1.x == p2.x && p1.y == p2.y)
            {
                return false;
            }
            else return true;
        }
        /// <summary>
        /// Allows you to add two positions to each other.
        /// </summary>
        /// <param name="p1">The first position to add.</param>
        /// <param name="p2">The second position to add.</param>
        /// <returns>A new position with the combined X and Y of both parameter values.</returns>
        public static Position operator +(Position p1, Position p2)
        {
            return new Position(p1.x + p2.x, p1.y + p2.y);
        }
        /// <summary>
        /// Allows you to subtract two positions with each other.
        /// </summary>
        /// <param name="p1">The first position to subtract.</param>
        /// <param name="p2">The second position to subtract.</param>
        /// <returns>A new position with the subtracted X and Y of both parameter values.</returns>
        public static Position operator -(Position p1, Position p2)
        {
            return new Position(p1.x - p2.x, p1.y - p2.y);
        }

        /// <summary>
        /// Creates and returns a new random position within the console bounds.
        /// </summary>
        /// <returns>Returns a new Position object with random X and Y.</returns>
        public static Position GetRandomPosition()
        {
            Random rand = new Random();

            return new Position(rand.Next(0, Console.WindowWidth), rand.Next(1, Console.WindowHeight));
        }
    }
}
