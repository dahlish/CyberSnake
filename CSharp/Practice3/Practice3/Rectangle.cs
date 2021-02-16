using System;
using System.Collections.Generic;
using System.Text;

namespace Practice3
{
    public class Rectangle
    {
        private float height;
        private float width;
        public float Height { get { return height; }  }
        public float Width { get { return width; } }


        public Rectangle(int sideLength)
        {
            width = sideLength;
            height = sideLength;
        }

        public Rectangle(float width, float height)
        {
            this.width = width;
            this.height = height;
        }

        public float GetArea()
        {
            return width * height;
        }
        public float GetCircumference()
        {
            float totalHeight = height * 2;
            float totalWidth = width * 2;
            return totalWidth + totalHeight;
        }

        public static bool operator ==(Square a, Rectangle b)
        {
            if (a.Side == b.Height && a.Side == b.Width)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator !=(Square a, Rectangle b)
        {
            if (a.Side == b.Height && a.Side == b.Width)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool operator ==(Rectangle b, Square a)
        {
            if (a.Side == b.Height && a.Side == b.Width)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator !=(Rectangle b, Square a)
        {
            if (a.Side == b.Height && a.Side == b.Width)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool operator ==(Rectangle a, Rectangle b)
        {
            if (a.Width == b.Width && a.height == b.Height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(Rectangle a, Rectangle b)
        {
            if (a.Width == b.Width && a.height == b.Height)
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
