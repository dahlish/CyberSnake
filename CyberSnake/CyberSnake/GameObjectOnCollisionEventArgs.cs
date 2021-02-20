using System;
using System.Collections.Generic;
using System.Text;

namespace CyberSnake
{
    /// <summary>
    /// This class contains arguments for the GameObject collision eventhandler.
    /// </summary>
    public class GameObjectOnCollisionEventArgs : EventArgs
    {
        public GameObject collidedGameObject;
    }
}
