﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CyberSnake
{
    /// <summary>
    /// The base class of every game object in the game. It is strongly advised to derive new objects from this class.
    /// </summary>
    public abstract class GameObject
    {
        public delegate void GameObjectOnCollisionEventHandler(GameObject sender, GameObjectOnCollisionEventArgs args);
        public delegate void GameObjectOnDestroyEventHandler(GameObject sender, GameObjectOnDestroyEventArgs args);
        public event GameObjectOnCollisionEventHandler OnCollision;
        public event GameObjectOnDestroyEventHandler OnDestroy;

        private bool isStatic = false;
        private bool hasRendered = false;
        private GameWorld gameWorld;
        private Position position;
        

        /// <summary>
        /// GameObjects implementing IRenderable will only be rendered once (the first frame) when this property is set to true.
        /// Setting this to true is recommended for objects that never change position during their entire existence.
        /// </summary>
        public bool IsStatic { get => isStatic; set => isStatic = value; }
        public bool HasRendered { get => hasRendered; set => hasRendered = value; }
        public GameWorld GameWorld { get => gameWorld; }
        public Position Position { get => position; set => position = value; }

        /// <summary>
        /// Creates a GameObject in the parameter gameWorld at the specified position.
        /// </summary>
        /// <param name="gameWorld">The world this object belongs to.</param>
        /// <param name="position">The position this object should be at.</param>
        public GameObject(GameWorld gameWorld, Position position)
        {
            this.position = position;
            this.gameWorld = gameWorld;
        }

        /// <summary>
        /// This method is called on every frame.
        /// It checks if the game object collides with any other game object in the world.
        /// Should a collision happen, it invokes the OnCollision event.
        /// </summary>
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

        /// <summary>
        /// Destroys the gameobject and invokes the OnDestroy event.
        /// It removes the object from the worlds global object list.
        /// </summary>
        /// <param name="world">The world the object belongs to.</param>
        /// <param name="destroyedByObject">The object that destroyed this object.</param>
        public virtual void Destroy(GameWorld world, GameObject destroyedByObject)
        {
            GameObjectOnDestroyEventArgs args = new GameObjectOnDestroyEventArgs { destroyedObject = this, timeElapsed = world.ElapsedTime, destroyedByObject = destroyedByObject};
            OnDestroy?.Invoke(this, args);
            world.AllObjects.Remove(this);
        }

        /// <summary>
        /// Controls if a game object is outside of the Console window width or height and transfers it to the other side should it be.
        /// This makes the console feel like a globe.
        /// Checkmate Flat-earthers.
        /// </summary>
        public void CheckConsoleBorderCollision()
        {
            if (Position.X < 0)
            {
                Position = new Position(ConsoleRenderer.ConsoleWidth - 1, Position.Y);
            }
            else if (Position.X >= ConsoleRenderer.ConsoleWidth)
            {
                Position = new Position(0, Position.Y);
            }

            if (Position.Y < 1)
            {
                Position = new Position(Position.X, ConsoleRenderer.ConsoleHeight - 1);
            }
            else if (Position.Y >= ConsoleRenderer.ConsoleHeight)
            {
                Position = new Position(Position.X, 1);
            }
        }
    }
}
