using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PGL2D.ScreenSystem
{
    public class ScreenSystem
    {
        /// <summary>
        /// A list of screens that this system controls
        /// </summary>
        private readonly List<BaseScreen> _screens = new List<BaseScreen>();

        /// <summary>
        /// Removes all screens that need to be deleted, and updates all screens that are not frozen
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if (_screens.Count == 0) return;

            _screens.RemoveAll(screen => screen.RequestDeletion);

            foreach (var screen in _screens.Where(screen => !screen.IsFrozen))
            {
                screen.Update(gameTime);
            }
        }

        /// <summary>
        /// Draws all screens that are not hidden
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var screen in _screens.Where(screen => !screen.IsHidden))
            {
                screen.Draw(gameTime, spriteBatch);
            }
        }

        /// <summary>
        /// Adds a new screen to the list
        /// </summary>
        /// <param name="screen"></param>
        public void AddScreen(BaseScreen screen)
        {
            //TODO:  Move Initialize somewhere else for Beta/Final release?
            screen.Initialize();
            _screens.Add(screen);
        }

        /// <summary>
        /// Terminates the screen and requests that it be deleted
        /// </summary>
        /// <param name="screen"></param>
        public void RemoveScreen(BaseScreen screen)
        {
            screen.Terminate();
        }

        /// <summary>
        /// Loads screen content for all screens that have not loaded content yet
        /// </summary>
        public void LoadScreenContent()
        {
            foreach (var screen in _screens.Where(screen => !screen.ContentLoaded))
            {
                screen.LoadContent();
            }
        }
    }
}
