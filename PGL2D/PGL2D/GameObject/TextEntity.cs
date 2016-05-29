using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PGL2D.GameObject
{
    public class TextEntity : GameEntity, IPhysical
    {
        /// <summary>
        /// The X and Y dimensions of the string in the game world
        /// </summary>
        private Vector2 _measureString;

        /// <summary>
        /// The 
        /// </summary>
        private readonly string _spriteFontAssetName;

        /// <summary>
        /// The font object that will be used to display a string to the screen
        /// </summary>
        private SpriteFont _spriteFont;

        /// <summary>
        /// Creates a new TextEntity object
        /// </summary>
        /// <param name="assetName">The asset name of the spritefont to use</param>
        /// <param name="text">The string to display to the screen</param>
        /// <param name="color">The color of the text</param>
        /// <param name="position">The location of the string in the game window</param>
        /// <param name="scale">The scale of the string to display</param>
        public TextEntity(string assetName, string text, Color color, Vector2 position = default(Vector2), float scale = 1.0f)
        {
            _spriteFontAssetName = assetName;

            Text = text;
            Color = color;
            Scale = scale;
            Position = position;
            ScaleVector = new Vector2(Scale, Scale);
        }

        /// <summary>
        /// The color of the text
        /// </summary>
        public Color Color { get; }

        /// <summary>
        /// The rotation of the text
        /// </summary>
        public float Rotation { get; }

        /// <summary>
        /// The scale of the text
        /// </summary>
        public float Scale { get; }

        /// <summary>
        /// The X and Y scale of the text
        /// </summary>
        public Vector2 ScaleVector { get; }

        /// <summary>
        /// The position of the text in the game window
        /// </summary>
        public Vector2 Position { get; private set; }

        /// <summary>
        /// The string to display to the screen
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// The bounds of the text
        /// </summary>
        public Rectangle Rectangle { get; private set; }

        /// <summary>
        /// The center point of the text
        /// </summary>
        public Vector2 Origin { get; private set; }

        /// <summary>
        /// Event that is handled when the content has been loaded
        /// </summary>
        public event EventHandler ContentLoaded;

        /// <summary>
        /// Event that is handled when the string text has been updated
        /// </summary>
        public event EventHandler TextUpdated;

        /// <summary>
        /// Loads the sprite font based off the asset name and updates the text with a new measurement
        /// </summary>
        /// <param name="content">The ContentManager to use</param>
        public void LoadContent(ContentManager content)
        {
            if (!string.IsNullOrWhiteSpace(_spriteFontAssetName))
            {
                _spriteFont = content.Load<SpriteFont>(_spriteFontAssetName);
                MeasureText();
                UpdateText();
            }

            ContentLoaded?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Unloads the spritefont and reset the measurement and text
        /// </summary>
        public virtual void UnloadContent()
        {
            _spriteFont = null;
            _measureString = Vector2.Zero;
            UpdateText();
        }

        /// <summary>
        /// Moves the text to a new location
        /// </summary>
        /// <param name="position">The new position to set for the text</param>
        public void Move(Vector2 position)
        {
            Position = position;
            UpdateText();
        }

        /// <summary>
        /// Updates the text Rectangle and Origin
        /// </summary>
        private void UpdateText()
        {
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)_measureString.X, (int)_measureString.Y);
            Origin = new Vector2(_measureString.X / 2.0f, _measureString.Y / 2.0f);
        }

        /// <summary>
        /// Updates the text with a new string to display
        /// </summary>
        /// <param name="text">The string to display to the screen</param>
        public void UpdateText(string text)
        {
            Text = text;
            MeasureText();
            UpdateText();

            TextUpdated?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Measures the text to get the width and height of the string
        /// </summary>
        private void MeasureText()
        {
            if (_spriteFont != null)
            {
                _measureString = _spriteFont.MeasureString(Text ?? "");
            }
        }

        /// <summary>
        /// Not used in alpha
        /// </summary>
        /// <param name="gameTime">The GameTime to use for this entity</param>
        protected override void UpdateEntity(GameTime gameTime)
        {
            //Save for beta/full release -- EMPTY FOR NOW
        }

        /// <summary>
        /// Draws the string to the screen using the SpriteFont object
        /// </summary>
        /// <param name="gameTime">The GameTime to use</param>
        /// <param name="spriteBatch">The SpriteBatch to use for drawing</param>
        protected override void DrawEntity(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_spriteFont != null && !string.IsNullOrWhiteSpace(Text))
            {
                spriteBatch.DrawString(_spriteFont, Text, Position, Color, Rotation, Origin, Scale, SpriteEffects.None, 0.0f);
            }
        }
    }
}
