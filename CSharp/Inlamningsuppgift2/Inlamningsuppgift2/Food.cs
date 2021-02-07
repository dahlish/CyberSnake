using System;
using System.Collections.Generic;
using System.Text;

namespace Inlamningsuppgift2
{
    class Food : GameObject, IRenderable
    {
        private char appearance;
        public char Appearance { get => appearance; }

        public Food(char appearance, Position pos, GameWorld world)
        {
            this.appearance = appearance;
            Position = pos;
            base.gameWorld = world;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
