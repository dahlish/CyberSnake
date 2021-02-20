using System;
using System.Collections.Generic;
using System.Text;

namespace Inlamningsuppgift2
{
    public class Tail : GameObject, IMovable, IRenderable
    {
        private char appearance;
        private Direction direction;
        public char Appearance { get => appearance; }
        public Direction Direction { get => Direction; set => direction = value; }

        public Tail(char appearance, Position position, GameWorld gameWorld) : base(gameWorld, position)
        {
            this.appearance = appearance;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
