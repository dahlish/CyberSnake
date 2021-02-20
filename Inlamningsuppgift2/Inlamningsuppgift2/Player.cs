using System;
using System.Collections.Generic;
using System.Text;

namespace Inlamningsuppgift2
{
    /// <summary>
    /// This class describes the Player that you will control during the game.
    /// </summary>
    public class Player : GameObject, IMovable, IRenderable
    {
        private char appearance;
        private char tailAppearance;
        private int tailCounter = 0;
        private Direction direction;
        private Position previousPosition;
        private List<Tail> tail = new List<Tail>();

        public char Appearance { get => appearance; }
        public char TailAppearance { get => tailAppearance; }
        public Direction Direction { get => direction; set => direction = value; }    
        public Position PreviousPosition { get => previousPosition; }
        public List<Tail> Tail { get => tail; set => tail = value; }

        /// <summary>
        /// Creates a  new player object with the set properties and places it in the game world.
        /// </summary>
        /// <param name="appearance">The appearance of the player.</param>
        /// <param name="tailAppearance">The appearance of the players tail.</param>
        /// <param name="position">The position this object will be placed at in the game world.</param>
        /// <param name="gameWorld">The gameworld this object belongs to.</param>
        public Player(char appearance, char tailAppearance, Position position, GameWorld gameWorld) : base(gameWorld, position)
        {
            this.appearance = appearance;
            this.tailAppearance = tailAppearance;
            gameWorld.AllObjects.Add(this);
            OnCollision += Player_OnCollision;
        }

        /// <summary>
        /// This is called when the OnCollision event in the GameObject class is invoked.
        /// It checks what it collides with, and acts accordingly. Such as eating food if the colliding object is Food, or resulting in GameOver if colliding 
        /// with the tail or a wall.
        /// </summary>
        /// <param name="sender">The object that invoked this event.</param>
        /// <param name="args">The event arguments that contains properties such as the colliding object.</param>
        private void Player_OnCollision(GameObject sender, GameObjectOnCollisionEventArgs args)
        {
            if (args.collidedGameObject is Food)
            {
                EatFood(args.collidedGameObject as Food);
            }
            else if (args.collidedGameObject is Tail)
            {
                GameWorld.GameOver();
            }
            else if (args.collidedGameObject is Wall)
            {
                GameWorld.GameOver();
            }
        }

        /// <summary>
        /// Calls the Destroy method on the Food parameter object.
        /// Also increases the tail and resets the time until starvation.
        /// </summary>
        /// <param name="food">The food object which is to be eaten.</param>
        public void EatFood(Food food)
        {
            food.Destroy(GameWorld, this);
            GameWorld.TimeLastFoodEaten = GameWorld.ElapsedTime;
            tailCounter++;
        }

        /// <summary>
        /// This is called on every frame. 
        /// It calls for movement and tail generation.
        /// </summary>
        public override void Update()
        {
            GenerateTail();
            Move();
            CheckConsoleBorderCollision();
            previousPosition = Position;
            base.Update();
        }

        /// <summary>
        /// Moves based on the Players current direction field. It places the player in a new position based on its last position and direction.
        /// </summary>
        public void Move()
        {
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

        /// <summary>
        /// Generates the tail in the world. The tail will stop generating new tails if the tail list count is larger than the tailCounter.
        /// </summary>
        public void GenerateTail()
        {
            tail.Insert(0, new Tail(tailAppearance, previousPosition, GameWorld));
            Tail tailToRemove = tail[tail.Count - 1];

            if (tail.Count < tailCounter)
            {
                tail.Insert(0, new Tail(tailAppearance, previousPosition, GameWorld));
            }
            else if (tail.Count > tailCounter)
            {
                GameWorld.AllObjects.Remove(tailToRemove);
                tail.RemoveAt(tail.Count - 1);
            }
        }
    }
}
