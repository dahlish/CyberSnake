using System;
using System.Collections.Generic;
using System.Text;

namespace Inlamningsuppgift2
{
    class Wall : GameObject, IRenderable
    {
        private char appearance;

        public char Appearance { get => appearance; }


        public Wall(char appearance, Position position, GameWorld gameWorld) : base(gameWorld, position)
        {
            this.appearance = appearance;
            OnCollision += Wall_OnCollision;
        }

        private void Wall_OnCollision(object sender, GameObjectOnCollisionEventArgs args)
        {
            if (args.collidedGameObject is Player)
            {
                Player player = args.collidedGameObject as Player;
                player.Position = Position.GetRandomPosition();
            }
        }
    }
}
