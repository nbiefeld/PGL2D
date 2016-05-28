using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PGL2D.GameObject
{
    public abstract class GameEntity : IFreezable, IHideable, IRemoveable
    {
        protected GameEntity()
        {
            Initialized = false;
        }

        public bool IsFrozen { get; set; }
        public bool IsHidden { get; set; }
        public bool RequestDeletion { get; internal set; }
        public bool Initialized { get; private set; }

        public virtual void Initialize()
        {
            Initialized = true;
        }

        public virtual void Terminate()
        {
            Initialized = false;
        }

        public void Update(GameTime gameTime)
        {
            if (IsFrozen) return;

            UpdateEntity(gameTime);
        }
        protected abstract void UpdateEntity(GameTime gameTime);

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (IsHidden) return;

            DrawEntity(gameTime, spriteBatch);
        }

        protected abstract void DrawEntity(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
