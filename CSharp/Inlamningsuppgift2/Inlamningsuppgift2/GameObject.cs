using System;
using System.Collections.Generic;
using System.Text;

namespace Inlamningsuppgift2
{
    public abstract class GameObject
    {
        public delegate void GameObjectOnCollisionEventHandler(object sender, GameObjectOnCollisionEventArgs args);
        public delegate void GameObjectOnDestroyEventHandler(object sender, GameObjectOnDestroyEventArgs args);

        private Position position;
        private GameWorld gameWorld;
        public Position Position { get => position; set => position = value; }
        public event GameObjectOnCollisionEventHandler OnCollision;
        public event GameObjectOnDestroyEventHandler OnDestroy;
        public GameWorld GameWorld { get => gameWorld; }

        public GameObject(GameWorld gameWorld, Position position)
        {
            this.position = position;
            this.gameWorld = gameWorld;
        }

        public virtual void Update()
        {
            var allObjects = gameWorld.AllObjects;

            for (int i = allObjects.Count - 1; i >= 0; i--)
            {
                if (allObjects[i].Position == position && allObjects[i] != this)
                {
                    GameObjectOnCollisionEventArgs args = new GameObjectOnCollisionEventArgs { collidedGameObject = allObjects[i] };
                    OnCollision?.Invoke(this, args);
                }
            }
        }

        public virtual void Destroy(GameWorld world, GameObject destroyedByObject)
        {
            world.AllObjects.Remove(this);
            GameObjectOnDestroyEventArgs args = new GameObjectOnDestroyEventArgs { destroyedObject = this, timeElapsed = world.ElapsedTime, destroyedByObject = destroyedByObject};
            OnDestroy?.Invoke(this, args);
        }
    }
}
