using System;
using System.Collections.Generic;
using System.Text;

namespace CyberSnake
{
    /// <summary>
    /// The wall generator acts as a generator for group of walls. You can use this to create many walls around a specific position.
    /// </summary>
    public class WallGenerator : GameObject
    {
        private char wallAppearance;
        private int amount;
        private List<Wall> walls = new List<Wall>();

        public int Amount { get => amount; set => amount = value; }
        public List<Wall> Walls { get => walls; }

        /// <summary>
        /// Creates a new WallGenerator that acts as a group of walls.
        /// </summary>
        /// <param name="amount">The amount of walls in this wall group.</param>
        /// <param name="wallAppearance">The appearance of the walls.</param>
        /// <param name="gameWorld">The gameworld this object belongs to.</param>
        /// <param name="position">The starting position of the wall group.
        /// </param>
        public WallGenerator(int amount, char wallAppearance, GameWorld gameWorld, Position position) : base (gameWorld, position)
        {
            this.amount = amount;
            this.wallAppearance = wallAppearance;

            if (ConsoleRenderer.IsOutOfBounds(position))
            {
                try
                {
                    Position = Position.GetRandomPositionAvailable(gameWorld);
                }
                catch (NoAvailablePositionFoundException)
                {
                    Destroy(GameWorld, this);
                }
            }
        }

        /// <summary>
        /// Generates new groups of Walls. It will determine the direction of the wall placement and then place a first wall based on this game objects position.
        /// Subsequent walls will be placed next to the previous wall in the random direction decided inside the method.
        /// Should a new wall be outside of bounds of the console, it will change direction of the wall and attempt to place it again.
        /// </summary>
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
                        if (!ConsoleRenderer.IsOutOfBounds(newPos) && !Position.HasGameObject(newPos, GameWorld))
                        {
                            walls.Add(new Wall(wallAppearance, walls[walls.Count - 1].Position -= new Position(0, 1), GameWorld));
                        }
                        else
                        {
                            wallDirection = GetNewDirection();
                            i--;
                        }
                    }
                    else if (wallDirection == Direction.Down)
                    {
                        newPos = new Position(walls[walls.Count - 1].Position.X, walls[walls.Count - 1].Position.Y + 1);
                        if (!ConsoleRenderer.IsOutOfBounds(newPos) && !Position.HasGameObject(newPos, GameWorld))
                        {
                            walls.Add(new Wall(wallAppearance, walls[walls.Count - 1].Position += new Position(0, 1), GameWorld));
                        }
                        else
                        {
                            wallDirection = GetNewDirection();
                            i--;
                        }
                    }
                    else if (wallDirection == Direction.Left)
                    {
                        newPos = new Position(walls[walls.Count - 1].Position.X - 1, walls[walls.Count - 1].Position.Y);
                        if (!ConsoleRenderer.IsOutOfBounds(newPos) && !Position.HasGameObject(newPos, GameWorld))
                        {
                            walls.Add(new Wall(wallAppearance, walls[walls.Count - 1].Position -= new Position(1, 0), GameWorld));
                        }
                        else
                        {
                            wallDirection = GetNewDirection();
                            i--;
                        }
                    }
                    else if (wallDirection == Direction.Right)
                    {
                        newPos = new Position(walls[walls.Count - 1].Position.X + 1, walls[walls.Count - 1].Position.Y);
                        if (!ConsoleRenderer.IsOutOfBounds(newPos) && !Position.HasGameObject(newPos, GameWorld))
                        {
                            walls.Add(new Wall(wallAppearance, walls[walls.Count - 1].Position += new Position(1, 0), GameWorld));
                        }
                        else
                        {
                            wallDirection = GetNewDirection();
                            i--;
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

        /// <summary>
        /// Randomizes a direction for the walls to be built around.
        /// </summary>
        /// <returns>A random direction as a Direction value.</returns>
        private Direction GetNewDirection()
        {
            Random rand = new Random();
            return (Direction)rand.Next(0, 4);
        }
    }
}
