using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PGL2D.GameObject
{
    public abstract class GameEntity : IFreezable, IHideable, IRemoveable
    {
        /// <summary>
        /// Creates a new GameEntity object
        /// </summary>
        protected GameEntity()
        {
            Initialized = false;
        }

        /// <summary>
        /// Determines if the entity is frozen and should not be updated
        /// </summary>
        public bool IsFrozen { get; set; }

        /// <summary>
        /// Determines if the entity is hidden and should not be drawn
        /// </summary>
        public bool IsHidden { get; set; }

        /// <summary>
        /// Determines if the entity should be deleted in the next game loop
        /// </summary>
        public bool RequestDeletion { get; internal set; }

        /// <summary>
        /// Determines if the object has been initialized or not
        /// </summary>
        public bool Initialized { get; private set; }

        /// <summary>
        /// Initializes the game object
        /// </summary>
        public virtual void Initialize()
        {
            Initialized = true;
        }

        /// <summary>
        /// Terminates (or deinitializes) the game object
        /// </summary>
        public virtual void Terminate()
        {
            Initialized = false;
        }

        /// <summary>
        /// Checks to see if the object is frozen and calls the update method if not frozen
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if (IsFrozen) return;

            UpdateEntity(gameTime);
        }

        /// <summary>
        /// Abstract method handling updating the entity since each entity will update differently
        /// </summary>
        /// <param name="gameTime">The GameTime to use for this object</param>
        protected abstract void UpdateEntity(GameTime gameTime);

        /// <summary>
        /// Checks to see if the object is hidden and calls the draw method if not hidden
        /// </summary>
        /// <param name="gameTime">The GameTime to use for this object</param>
        /// <param name="spriteBatch">The SpriteBatch to use for drawing</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (IsHidden) return;

            DrawEntity(gameTime, spriteBatch);
        }

        /// <summary>
        /// Abstract method handling drawing the entity since each entity will be drawn differently
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        protected abstract void DrawEntity(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
