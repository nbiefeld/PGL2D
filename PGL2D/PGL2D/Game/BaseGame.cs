using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PGL2D.Input;

namespace PGL2D.Game
{
    public abstract class BaseGame : Microsoft.Xna.Framework.Game
    {
        /// <summary>
        /// Used to indicate what color to make the background of the game
        /// </summary>
        private readonly Color _backgroundColor;

        /// <summary>
        /// Used to control the width and height of the game
        /// </summary>
        protected GraphicsDeviceManager Graphics;

        /// <summary>
        /// Creates a new BaseGame object
        /// </summary>
        /// <param name="width">The width to make the game</param>
        /// <param name="height">The height to make the game</param>
        /// <param name="contentRootDirectory">Where the content directory is located</param>
        /// <param name="backgroundColor">The background color of the game</param>
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

        /// <summary>
        /// Used to render 2D content to the screen
        /// </summary>
        public SpriteBatch SpriteBatch { get; private set; }

        /// <summary>
        /// Used in order to process input
        /// </summary>
        public InputSystem InputSystem { get; private set; }

        /// <summary>
        /// Used to handle game screens, their active status, transitions, and more
        /// </summary>
        protected ScreenSystem.ScreenSystem ScreenSystem { get; private set; }

        /// <summary>
        /// Returns the viewport of the game
        /// </summary>
        public Viewport Viewport => GraphicsDevice.Viewport;

        /// <summary>
        /// The dimensions of the game window
        /// </summary>
        public Rectangle Dimensions { get; private set; }

        /// <summary>
        /// The center X and center Y of the game
        /// </summary>
        public Vector2 AbsoluteCenter { get; private set; }

        /// <summary>
        /// Creates the spritebatch and tells the ScreenSystem to load content
        /// </summary>
        protected sealed override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            ScreenSystem.LoadScreenContent();
        }

        /// <summary>
        /// Updates our input system and notifies the ScreenSystem that it should update
        /// </summary>
        /// <param name="gameTime">The GameTime object to use for updating</param>
        protected sealed override void Update(GameTime gameTime)
        {
            InputSystem.Update();
            ScreenSystem.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Clears our device and notifies the ScreenSystem to draw it's content.
        /// </summary>
        /// <param name="gameTime"></param>
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
