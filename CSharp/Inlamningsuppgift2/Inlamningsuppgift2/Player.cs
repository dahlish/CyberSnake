using System;
using System.Collections.Generic;
using System.Text;

namespace Inlamningsuppgift2
{
    class Player : GameObject, IMovable, IRenderable
    {
        private Direction direction;
        private char appearance;
        private List<Tail> tail = new List<Tail>();
        private int tailCounter = 0;

        public Direction Direction { get => direction; set => direction = value; }
        public char Appearance { get => appearance; }
        public List<Tail> Tail { get => tail; }
        

        public Player(char appearance, Position pos, GameWorld world)
        {
            this.appearance = appearance;
            Position = pos;
            base.gameWorld = world;

            Collision += Player_Collision;
        }

        private void Player_Collision(object sender, GameObjectCollisionEventArgs e)
        {
            if (e.collidedGameObject is Food)
            {
                EatFood(e.collidedGameObject as Food);
            }
        }

        public void EatFood(Food f)
        {
            f.Destroy(f, gameWorld);
            gameWorld.IncreaseScore();
            IncreaseTail();
        }

        public override void Update()
        {
            Position previousPos = Position;
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

            if (tail.Count > 0)
            {
                tail.Remove(tail[tail.Count - 1]);
                gameWorld.AllObjects.Remove(tail[tail.Count - 1]);
                if (tail.Count <= tailCounter)
                {
                    tail.Add(new Tail('0', previousPos, gameWorld));
                    gameWorld.AllObjects.Add(tail[tail.Count - 1]);
                }
            }

            CheckCollision();
            base.Update();
        }

        public void CheckCollision()
        {
            if (Position.X < 0)
            {
                Position = new Position(Console.WindowWidth - 1, Position.Y);
                direction = Direction.None;
            }
            else if (Position.X >= Console.WindowWidth)
            {
                Position = new Position(0, Position.Y);
                direction = Direction.None;
            }

            if (Position.Y < 0)
            {
                Position = new Position(Position.X, Console.WindowHeight - 1);
                direction = Direction.None;
            }
            else if (Position.Y >= Console.WindowHeight)
            {
                Position = new Position(Position.X, 0);
                direction = Direction.None;
            }
        }

        public void IncreaseTail()
        {
            tailCounter++;
            tail.Add(new Tail('0', this.Position, gameWorld));
            gameWorld.AllObjects.Add(tail[tail.Count - 1]);
        }
    }
}
