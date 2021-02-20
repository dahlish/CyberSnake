using System;
using System.Collections.Generic;
using System.Text;

namespace Inlamningsuppgift2
{
    class Food : GameObject, IRenderable
    {
        public delegate void OnFoodExpiredEventHandler(object sender);
        public event OnFoodExpiredEventHandler OnFoodExpired;

        private char appearance;
        private float createdTime;
        private float maxTime = 10;
        private FoodType foodType;

        public char Appearance { get => appearance; }
        public float CreatedTime { get => createdTime; }
        public FoodType FoodType { get => foodType; }

        public Food(char appearance, Position position, GameWorld gameWorld, FoodType foodType, float maxTime) : base(gameWorld, position)
        {
            this.appearance = appearance;
            this.foodType = foodType;
            this.maxTime = maxTime;
            createdTime = gameWorld.ElapsedTime;
            OnDestroy += Food_OnDestroy;
            OnFoodExpired += Food_OnFoodExpired;
        }

        private void Food_OnFoodExpired(object sender)
        {
            Destroy(GameWorld, this);
        }

        private void Food_OnDestroy(object sender, GameObjectOnDestroyEventArgs args)
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

        public override void Update()
        {
            if (createdTime + maxTime <= GameWorld.ElapsedTime)
            {
                OnFoodExpired(this);
            }

            base.Update();
        }

        public static Food Create(char appearance, Position position, GameWorld gameWorld, FoodType foodType, float maxTime = 10)
        {
            return new Food(appearance, position, gameWorld, foodType, maxTime);
        }
    }

    public enum FoodType
    {
        Normal = 1,
        Special = 2
    }
}
