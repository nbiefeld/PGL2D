using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PGL2D.Input;

namespace PGL2D.Game
{
    public abstract class BaseGame : Microsoft.Xna.Framework.Game
    {
        private readonly Color _backgroundColor;
        protected GraphicsDeviceManager Graphics;

        protected BaseGame(int width, int height, string contentRootDirectory, Color backgroundColor)
        {
            Graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = width,
                PreferredBackBufferHeight = height
            };

            Dimensions = new Rectangle(0, 0, width, height);
            AbsoluteCenter = new Vector2(width / 2.0f, height / 2.0f);

            Content.RootDirectory = contentRootDirectory;

            InputSystem = new InputSystem();
            ScreenSystem = new ScreenSystem.ScreenSystem();

            _backgroundColor = backgroundColor;
        }

        public SpriteBatch SpriteBatch { get; private set; }
        public InputSystem InputSystem { get; private set; }
        protected ScreenSystem.ScreenSystem ScreenSystem { get; private set; }
        public Viewport Viewport => GraphicsDevice.Viewport;
        public Rectangle Dimensions { get; private set; }
        public Vector2 AbsoluteCenter { get; private set; }

        protected sealed override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            ScreenSystem.LoadScreenContent();
        }

        protected sealed override void Update(GameTime gameTime)
        {
            InputSystem.Update();
            ScreenSystem.Update(gameTime);

            base.Update(gameTime);
        }

        protected sealed override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_backgroundColor);

            SpriteBatch.Begin();

            ScreenSystem.Draw(gameTime, SpriteBatch);

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
