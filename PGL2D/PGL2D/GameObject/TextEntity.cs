using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PGL2D.GameObject
{
    public class TextEntity : GameEntity, IPhysical
    {
        private Vector2 _measureString;
        private readonly string _spriteFontAssetName;
        private SpriteFont _spriteFont;

        public TextEntity(string assetName, string text, Color color, Vector2 position = default(Vector2), float scale = 1.0f)
        {
            _spriteFontAssetName = assetName;

            Text = text;
            Color = color;
            Scale = scale;
            Position = position;
            ScaleVector = new Vector2(Scale, Scale);
        }

        public Color Color { get; }
        public float Rotation { get; }
        public float Scale { get; }
        public Vector2 ScaleVector { get; }
        public Vector2 Position { get; private set; }
        public string Text { get; private set; }
        public Rectangle Rectangle { get; private set; }
        public Vector2 Origin { get; private set; }

        public event EventHandler ContentLoaded;
        public event EventHandler TextUpdated;

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

        public virtual void UnloadContent()
        {
            _spriteFont = null;
            _measureString = Vector2.Zero;
            UpdateText();
        }

        public void Move(Vector2 position)
        {
            Position = position;
            UpdateText();
        }

        private void UpdateText()
        {
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)_measureString.X, (int)_measureString.Y);
            Origin = new Vector2(_measureString.X / 2.0f, _measureString.Y / 2.0f);
        }

        public void UpdateText(string text)
        {
            Text = text;
            MeasureText();
            UpdateText();

            TextUpdated?.Invoke(this, EventArgs.Empty);
        }

        private void MeasureText()
        {
            if (_spriteFont != null)
            {
                _measureString = _spriteFont.MeasureString(Text ?? "");
            }
        }

        protected override void UpdateEntity(GameTime gameTime)
        {
            //Save for beta/full release -- EMPTY FOR NOW
        }

        protected override void DrawEntity(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_spriteFont != null && !string.IsNullOrWhiteSpace(Text))
            {
                spriteBatch.DrawString(_spriteFont, Text, Position, Color, Rotation, Origin, Scale, SpriteEffects.None, 0.0f);
            }
        }
    }
}
