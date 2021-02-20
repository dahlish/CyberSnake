using System;
using System.Collections.Generic;
using System.Text;

namespace CyberSnake
{
    /// <summary>
    /// Implement this interface if you wish your object to be able to move.
    /// </summary>
    interface IMovable
    {
        public Direction Direction { get; }
    }

    /// <summary>
    /// Specifies the direction of an object that derives from IMovable.
    /// </summary>
    public enum Direction
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3,
        None = 4
    }
}
