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
            OnDestroy += Food_OnDestroy;
        }

        private void Food_OnDestroy(object sender, GameObjectOnDestroyEventArgs args)
        {
            if (args.destroyedByObject is Player)
            {
                gameWorld.IncreaseScore();
            }
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
