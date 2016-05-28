using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PGL2D.GameObject
{
    public abstract class PhysicalEntity : GameEntity, IPhysical
    {
        private readonly string _textureName;

        protected PhysicalEntity(Color color, Vector2 position, string textureName, float rotation, float scale)
        {
            Color = color;
            Rotation = rotation;
            Scale = scale;
            ScaleVector = new Vector2(Scale, Scale);
            Position = position;
            _textureName = textureName;
        }

        public Color Color { get; }
        public float Rotation { get; }
        public float Scale { get; }
        public Vector2 ScaleVector { get; }
        public Vector2 Position { get; protected set; }
        public Rectangle Rectangle { get; private set; }
        public Texture2D Texture { get; private set; }
        protected Vector2 Origin { get; private set; }

        public event EventHandler ContentLoaded;

        internal virtual void LoadContent(ContentManager content)
        {
            if (!string.IsNullOrWhiteSpace(_textureName))
            {
                Texture = content.Load<Texture2D>(_textureName);
                Origin = new Vector2(Texture.Width / 2.0f, Texture.Height / 2.0f);

                Rectangle = new Rectangle((int) Position.X, (int) Position.Y, Texture.Width, Texture.Height);
            }

            ContentLoaded?.Invoke(this, EventArgs.Empty);
        }

        public virtual void UnloadContent()
        {
            Texture.Dispose();
            Origin = Vector2.Zero;
        }

        protected override void DrawEntity(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Texture != null)
            {
                spriteBatch.Draw(Texture, Position, null, null, Origin, Rotation, ScaleVector, Color, SpriteEffects.None, 0f);
            }
        }

        protected void UpdateRectangle()
        {
            var topLeft = Position - Origin;
            Rectangle = new Rectangle((int)topLeft.X, (int)topLeft.Y, Texture?.Width ?? 0, Texture?.Height ?? 0);
        }
    }
}
