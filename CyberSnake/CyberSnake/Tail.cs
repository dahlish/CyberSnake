using System;
using System.Collections.Generic;
using System.Text;

namespace CyberSnake
{
    /// <summary>
    /// This class describes the players Tail.
    /// </summary>
    public class Tail : GameObject, IRenderable
    {
        private char appearance;

        public char Appearance { get => appearance; }

        /// <summary>
        /// Creates a new Tail object and spawns it in the game world.
        /// </summary>
        /// <param name="appearance">The appearance of the tail.</param>
        /// <param name="position">The position to spawn it on.</param>
        /// <param name="gameWorld">The world this object belongs to.</param>
        public Tail(char appearance, Position position, GameWorld gameWorld) : base(gameWorld, position)
        {
            this.appearance = appearance;
            GameWorld.AllObjects.Add(this);
        }

        /// <summary>
        /// This method is called on every frame.
        /// Only checks if the tail is out of bounds from the console or collides with another gameobject.
        /// </summary>
        public override void Update()
        {
            CheckConsoleBorderCollision();
            base.Update();
        }

    }
}
