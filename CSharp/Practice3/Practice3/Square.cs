using System;
using System.Collections.Generic;
using System.Text;

namespace Practice3
{
    public class Square
    {
        private float side;
        public float Side { get { return side; } }

        public Square(float side)
        {
            this.side = side;
        }


        public static bool operator ==(Square a, Square b)
        {
            if (a.Side == b.Side && a.Side == b.Side)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(Square a, Square b)
        {
            if (a.Side == b.Side && a.Side == b.Side)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
