using System;
using System.Collections.Generic;
using System.Text;

namespace Inlamningsuppgift2
{
    public class Tail : GameObject, IRenderable
    {
        private char appearance;
        private Player player;
        public char Appearance { get => appearance; }

        public Tail(char appearance, Position position, GameWorld gameWorld) : base(gameWorld, position)
        {
            this.appearance = appearance;
            player = gameWorld.GetPlayer();
            GameWorld.AllObjects.Add(this);
        }

        public override void Update()
        {
            CheckCollision();
            base.Update();
        }

        public void CheckCollision()
        {
            if (Position.X < 0)
            {
                Position = new Position(Console.WindowWidth - 1, Position.Y);
            }
            else if (Position.X >= Console.WindowWidth)
            {
                Position = new Position(0, Position.Y);
            }

            if (Position.Y < 1)
            {
                Position = new Position(Position.X, Console.WindowHeight - 1);
            }
            else if (Position.Y >= Console.WindowHeight)
            {
                Position = new Position(Position.X, 1);
            }
        }
    }
}
