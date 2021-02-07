using System;
using System.Collections.Generic;
using System.Text;

namespace Inlamningsuppgift2
{
    public abstract class GameObject
    {
        private Position position;
        public Position Position { get => position; set => position = value; }
        public event EventHandler<GameObjectCollisionEventArgs> Collision;
        public GameWorld gameWorld;

        public virtual void Update()
        {
            var allObjects = gameWorld.AllObjects;

            for (int i = allObjects.Count - 1; i >= 0; i--)
            {
                if (allObjects[i].Position == position && allObjects[i] != this)
                {
                    GameObjectCollisionEventArgs args = new GameObjectCollisionEventArgs { collidedGameObject = allObjects[i] };
                    Collision?.Invoke(this, args);
                }
            }
        }

        public virtual void Destroy(GameObject obj, GameWorld world)
        {
            world.AllObjects.Remove(obj);
        }
    }
}
