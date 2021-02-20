using System;
using System.Collections.Generic;
using System.Text;

namespace Inlamningsuppgift2
{
    class Player : GameObject, IMovable, IRenderable
    {
        private Direction direction;
        private char appearance;
        private Queue<Tail> tail = new Queue<Tail>();
        private int tailCounter = 0;
        private Position previousPos;

        public Direction Direction { get => direction; set => direction = value; }
        public char Appearance { get => appearance; }        

        public Player(char appearance, Position position, GameWorld gameWorld) : base(gameWorld, position)
        {
            this.appearance = appearance;

            OnCollision += Player_OnCollision;
        }

        private void Player_OnCollision(object sender, GameObjectOnCollisionEventArgs e)
        {
            if (e.collidedGameObject is Food)
            {
                EatFood(e.collidedGameObject as Food);
            }
        }

        public void EatFood(Food f)
        {
            f.Destroy(GameWorld, this);
            GameWorld.TimeLastFoodEaten = GameWorld.ElapsedTime;
            IncreaseTail();
        }

        public override void Update()
        {
            Move();
            CheckCollision();
            base.Update();
        }

        public void Move()
        {
            previousPos = Position;
            if (direction == Direction.Up)
            {
                Position = new Position(Position.X, Position.Y - 1);
            }
            else if (direction == Direction.Down)
            {
                Position = new Position(Position.X, Position.Y + 1);
            }
            else if (direction == Direction.Left)
            {
                Position = new Position(Position.X - 1, Position.Y);
            }
            else if (direction == Direction.Right)
            {
                Position = new Position(Position.X + 1, Position.Y);
            }
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

        public void IncreaseTail()
        {
            tailCounter++;
        }
    }
}
