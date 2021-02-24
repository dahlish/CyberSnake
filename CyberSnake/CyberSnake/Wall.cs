using System;
using System.Collections.Generic;
using System.Text;

namespace CyberSnake
{
    /// <summary>
    /// Wall objects are obstacles in the game that can not be traversed.
    /// </summary>
    public class Wall : GameObject, IRenderable
    {
        private char appearance;

        public char Appearance { get => appearance; }


        /// <summary>
        /// Creates a new wall object but does NOT place it automatically in the game world.
        /// </summary>
        /// <param name="appearance">The apperance of the object.</param>
        /// <param name="position">The position this object should be present at.</param>
        /// <param name="gameWorld">The game world this object belongs to.</param>
        public Wall(char appearance, Position position, GameWorld gameWorld) : base(gameWorld, position)
        {
            this.appearance = appearance;
            IsStatic = true;
        }
    }
}
