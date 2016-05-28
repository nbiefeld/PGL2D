using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PGL2D.Input;

namespace PGL2D.GameObject
{
    public interface IControllable
    {
        InputMap InputMap { get; }

        void HandleInput(List<Keybind> availableKeybinds, GameTime gameTime);
    }
}
