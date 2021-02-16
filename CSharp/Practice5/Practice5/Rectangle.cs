using System;
using System.Collections.Generic;
using System.Text;

namespace Practice5
{
    class Rectangle<T>
    {
        private T width;
        private T height;

        public T Width
        {
            get { return width; }
            set { width = value; }
        }

        public T Height
        {
            get { return height; }
            set { height = value; }
        }

        public Rectangle(T width, T height)
        {
            this.width = width;
            this.height = height;
        }
    }
}
