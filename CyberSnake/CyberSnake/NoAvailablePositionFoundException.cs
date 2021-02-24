using System;
using System.Collections.Generic;
using System.Text;

namespace CyberSnake
{
    public class NoAvailablePositionFoundException : Exception
    {
        public NoAvailablePositionFoundException(string message) : base(message)
        {
            
        }
    }
}
