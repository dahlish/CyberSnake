using System;
using System.Collections.Generic;
using System.Text;

namespace Inlamningsuppgift2
{
    /// <summary>
    /// This class contains everything needed for food objects in the game.
    /// </summary>
    public class Food : GameObject, IRenderable
    {
        public delegate void OnFoodExpiredEventHandler(Food sender);
        /// <summary>
        /// This event is invoked whenever the food has expired.
        /// </summary>
        public event OnFoodExpiredEventHandler OnFoodExpired;

        private char appearance;
        private float createdTime;
        private float maxTime = 10;
        private FoodType foodType;

        public char Appearance { get => appearance; }
        public float CreatedTime { get => createdTime; }
        public FoodType FoodType { get => foodType; }


        /// <summary>
        /// Creates a new Food object and adds it to the AllObjects list in the gameWorld object.
        /// </summary>
        /// <param name="appearance">The appearance of the food.</param>
        /// <param name="position">The current position you want to create the food at.</param>
        /// <param name="gameWorld">The gameworld which this object will be placed in.</param>
        /// <param name="foodType">Which type this food is. This affects the score it gives if it is eaten by a player.</param>
        /// <param name="maxTime">The time in seconds this object will be present in the game world before it expires and is destroyed.</param>
        public Food(char appearance, Position position, GameWorld gameWorld, FoodType foodType, float maxTime) : base(gameWorld, position)
        {
            this.appearance = appearance;
            this.foodType = foodType;
            this.maxTime = maxTime;
            gameWorld.AllObjects.Add(this);
            createdTime = gameWorld.ElapsedTime;
            OnDestroy += Food_OnDestroy;
            OnFoodExpired += Food_OnFoodExpired;
            OnCollision += Food_OnCollision;
        }

        /// <summary>
        /// This is called whenever the event OnCollision is invoked inside the GameObject class. 
        /// It is called when this objects position is the same as the colliding object.
        /// </summary>
        /// <param name="sender">The object that invoked the event.</param>
        /// <param name="args">Contains arguments such as the colliding gameobject.</param>
        private void Food_OnCollision(GameObject sender, GameObjectOnCollisionEventArgs args)
        {
            if (args.collidedGameObject is Wall || args.collidedGameObject is Tail)
            {
                Destroy(GameWorld, this);
            }
        }

        /// <summary>
        /// This is called when the event OnFoodExpired is invoked inside the GameObject class.
        /// It destroys the gameobject to make place for a new food object in the world.
        /// </summary>
        /// <param name="sender">The object that invoked the event.</param>
        private void Food_OnFoodExpired(Food sender)
        {
            Destroy(GameWorld, this);
        }

        /// <summary>
        /// This is called when the event OnDestroy is invoked inside the GameObject class.
        /// It increases the player score if the Player is the destroyedByObject inside the args.
        /// </summary>
        /// <param name="sender">The object that invoked the event.</param>
        /// <param name="args">Contains arguments such as the destroying object.</param>
        private void Food_OnDestroy(GameObject sender, GameObjectOnDestroyEventArgs args)
        {
            if (args.destroyedByObject is Player)
            {
                GameWorld.IncreaseScore((int)foodType);
                GameWorld.FoodAmount--;
            }
            else
            {
                GameWorld.FoodAmount--;
            }
        }

        /// <summary>
        /// This method is called every frame and checks if the Food object has expired or not. If it has, it invokes the OnFoodExpired event.
        /// </summary>
        public override void Update()
        {
            if (createdTime + maxTime <= GameWorld.ElapsedTime)
            {
                OnFoodExpired?.Invoke(this);
            }

            base.Update();
        }

        /// <summary>
        /// Creates a new Food object and returns it. It contains a few optional parameters if you would like to use the Food default settings.
        /// </summary>
        /// <param name="appearance">The appearance of the gameobject.</param>
        /// <param name="position">The position this object will be placed at in the game world.</param>
        /// <param name="gameWorld">The world this object will belong to.</param>
        /// <param name="foodType">Which type this food is. This affects the score it gives if it is eaten by a player.</param>
        /// <param name="maxTime">The time in seconds this object will be present in the game world before it expires and is destroyed.</param>
        /// <returns>A new Food object with the properties set.</returns>
        public static Food Create(char appearance, Position position, GameWorld gameWorld, FoodType foodType = FoodType.Normal, float maxTime = 10)
        {
            return new Food(appearance, position, gameWorld, foodType, maxTime);
        }
    }

    /// <summary>
    /// Specifies the type of food. This affects the score it will give when it is destroyed by the player.
    /// </summary>
    public enum FoodType
    {
        Normal = 1,
        Special = 3
    }
}
