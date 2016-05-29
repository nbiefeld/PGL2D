using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PGL2D.GameObject
{
    public abstract class PhysicalEntity : GameEntity, IPhysical
    {
        /// <summary>
        /// The asset name of the texture to use during content loading
        /// </summary>
        private readonly string _textureName;

        /// <summary>
        /// Creates a new PhysicalEntity object
        /// </summary>
        /// <param name="color">The color tint of the object</param>
        /// <param name="position">The physical location of the object in the game world</param>
        /// <param name="textureName">The asset name of the texture to use during content loading</param>
        /// <param name="rotation">The rotation of the object</param>
        /// <param name="scale">The scale of the object</param>
        protected PhysicalEntity(Color color, Vector2 position, string textureName, float rotation, float scale)
        {
            Color = color;
            Rotation = rotation;
            Scale = scale;
            ScaleVector = new Vector2(Scale, Scale);
            Position = position;
            _textureName = textureName;
        }

        /// <summary>
        /// The color tint of the object
        /// </summary>
        public Color Color { get; }

        /// <summary>
        /// The rotation of the object
        /// </summary>
        public float Rotation { get; }

        /// <summary>
        /// The scale of the object
        /// </summary>
        public float Scale { get; }

        /// <summary>
        /// The X and Y scale of the object
        /// </summary>
        public Vector2 ScaleVector { get; }

        /// <summary>
        /// The physical location of the object in the game world
        /// </summary>
        public Vector2 Position { get; protected set; }

        /// <summary>
        /// The Rectangle of the object (used for rectangle collision)
        /// </summary>
        public Rectangle Rectangle { get; private set; }

        /// <summary>
        /// The texture of our object that will be drawn to the screen
        /// </summary>
        public Texture2D Texture { get; private set; }

        /// <summary>
        /// The center point of the object
        /// </summary>
        protected Vector2 Origin { get; private set; }

        /// <summary>
        /// Event to be called when the content has been loaded (useful for setting the position based off the texture dimensions)
        /// </summary>
        public event EventHandler ContentLoaded;

        /// <summary>
        /// Loads the texture asset as a Texture2D object and sets the origin and rectangle
        /// </summary>
        /// <param name="content">The ContentManager to use to load the content</param>
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

        /// <summary>
        /// Disposes the texture and changes the origin
        /// </summary>
        public virtual void UnloadContent()
        {
            Texture.Dispose();
            Origin = Vector2.Zero;
        }

        /// <summary>
        /// Draws the entity to the screen using the texture that was loaded
        /// </summary>
        /// <param name="gameTime">The GameTime to use for drawing this entity</param>
        /// <param name="spriteBatch">The SpriteBatch object to use to draw the entity</param>
        protected override void DrawEntity(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Texture != null)
            {
                spriteBatch.Draw(Texture, Position, null, null, Origin, Rotation, ScaleVector, Color, SpriteEffects.None, 0f);
            }
        }

        /// <summary>
        /// Updates the rectangle based off the top left of the object and the width and height of the texture
        /// </summary>
        protected void UpdateRectangle()
        {
            var topLeft = Position - Origin;
            Rectangle = new Rectangle((int)topLeft.X, (int)topLeft.Y, Texture?.Width ?? 0, Texture?.Height ?? 0);
        }
    }
}
