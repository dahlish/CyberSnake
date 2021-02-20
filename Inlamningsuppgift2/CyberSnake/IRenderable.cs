using System;
using System.Collections.Generic;
using System.Text;

namespace CyberSnake
{
    /// <summary>
    /// If you want your GameObject to be renderable it must implement this interface.
    /// </summary>
    interface IRenderable
    {
        public char Appearance { get; }
    }
}
