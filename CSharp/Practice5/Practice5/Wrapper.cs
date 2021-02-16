using System;
using System.Collections.Generic;
using System.Text;

namespace Practice5
{
    class Wrapper<T> where T: struct
    {
        private T? value;

        public Wrapper(T value)
        {
            this.value = value;
        }
        public Wrapper()
        {
            value = null;
        }

        public void Show()
        {
            if (value == null)
            {
                Console.WriteLine("Inget");
            }
            else
            {
                Console.WriteLine(value);
            }
        }
        public void Set(T newValue)
        {
            value = newValue;
        }
        public void Reset()
        {
            value = null;
        }
    }
}
