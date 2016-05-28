using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PGL2D.ScreenSystem
{
    public class ScreenSystem
    {
        private readonly List<BaseScreen> _screens = new List<BaseScreen>();

        public void Update(GameTime gameTime)
        {
            if (_screens.Count == 0) return;

            _screens.RemoveAll(screen => screen.RequestDeletion);

            foreach (var screen in _screens.Where(screen => !screen.IsFrozen))
            {
                screen.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var screen in _screens.Where(screen => !screen.IsHidden))
            {
                screen.Draw(gameTime, spriteBatch);
            }
        }

        public void AddScreen(BaseScreen screen)
        {
            screen.Initialize();
            _screens.Add(screen);
        }

        public void RemoveScreen(BaseScreen screen)
        {
            screen.Terminate();
        }

        public void LoadScreenContent()
        {
            foreach (var screen in _screens.Where(screen => !screen.ContentLoaded))
            {
                screen.LoadContent();
            }
        }
    }
}
