using System;
using System.Collections.Generic;
using System.Text;

namespace Inlamningsuppgift2
{
    public struct Position
    {
        private int x;
        private int y;

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static bool operator ==(Position p1, Position p2)
        {
            if (p1.x == p2.x && p1.y == p2.y)
            {
                return true;
            }
            else return false;
        }
        public static bool operator !=(Position p1, Position p2)
        {
            if (p1.x == p2.x && p1.y == p2.y)
            {
                return false;
            }
            else return true;
        }
        public static Position operator +(Position p1, Position p2)
        {
            return new Position(p1.x + p2.x, p1.y + p2.y);
        }
        public static Position operator -(Position p1, Position p2)
        {
            return new Position(p1.x - p2.x, p1.y - p2.y);

        }

        public static Position GetRandomPosition()
        {
            Random rand = new Random();

            return new Position(rand.Next(0, Console.WindowWidth), rand.Next(1, Console.WindowHeight));
        }
    }
}
