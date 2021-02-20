using System;
using System.Collections.Generic;
using System.Text;

namespace Inlamningsuppgift2
{
    class WallGenerator : GameObject
    {
        private char wallAppearance;
        private List<Wall> walls = new List<Wall>();
        private int amount;

        public int Amount { get => amount; set => amount = value; }
        public List<Wall> Walls { get => walls; }


        public WallGenerator(int amount, char wallAppearance, GameWorld gameWorld, Position position) : base (gameWorld, position)
        {
            this.amount = amount;
            this.wallAppearance = wallAppearance;
        }

        public void Generate()
        {
            Random rand = new Random();
            Direction wallDirection = GetNewDirection();

            for (int i = 1; i <= amount; i++)
            {
                if (walls.Count > 0)
                {
                    Position newPos;

                    if (wallDirection == Direction.Up)
                    {
                        newPos = new Position(walls[walls.Count - 1].Position.X, walls[walls.Count - 1].Position.Y - 1);
                        if (!ConsoleRenderer.CheckOutOfBounds(newPos))
                        {
                            walls.Add(new Wall(wallAppearance, new Position(walls[walls.Count - 1].Position.X, walls[walls.Count - 1].Position.Y - 1), GameWorld));
                        }
                        else
                        {
                            wallDirection = GetNewDirection();
                        }
                    }
                    else if (wallDirection == Direction.Down)
                    {
                        newPos = new Position(walls[walls.Count - 1].Position.X, walls[walls.Count - 1].Position.Y + 1);
                        if (!ConsoleRenderer.CheckOutOfBounds(newPos))
                        {
                            walls.Add(new Wall(wallAppearance, new Position(walls[walls.Count - 1].Position.X, walls[walls.Count - 1].Position.Y + 1), GameWorld));
                        }
                        else
                        {
                            wallDirection = GetNewDirection();
                        }
                    }
                    else if (wallDirection == Direction.Left)
                    {
                        newPos = new Position(walls[walls.Count - 1].Position.X - 1, walls[walls.Count - 1].Position.Y);
                        if (!ConsoleRenderer.CheckOutOfBounds(newPos))
                        {
                            walls.Add(new Wall(wallAppearance, new Position(walls[walls.Count - 1].Position.X - 1, walls[walls.Count - 1].Position.Y), GameWorld));
                        }
                        else
                        {
                            wallDirection = GetNewDirection();
                        }
                    }
                    else if (wallDirection == Direction.Right)
                    {
                        newPos = new Position(walls[walls.Count - 1].Position.X + 1, walls[walls.Count - 1].Position.Y);
                        if (!ConsoleRenderer.CheckOutOfBounds(newPos))
                        {
                            walls.Add(new Wall(wallAppearance, new Position(walls[walls.Count - 1].Position.X + 1, walls[walls.Count - 1].Position.Y), GameWorld));
                        }
                        else
                        {
                            wallDirection = GetNewDirection();
                        }
                    }
                }
                else
                {
                    walls.Add(new Wall(wallAppearance, Position, GameWorld));
                }

                GameWorld.AllObjects.Add(walls[walls.Count - 1]);
            }
        }

        private Direction GetNewDirection()
        {
            Random rand = new Random();
            return (Direction)rand.Next(0, 4);
        }
    }
}
