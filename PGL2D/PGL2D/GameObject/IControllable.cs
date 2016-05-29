using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PGL2D.Input;

namespace PGL2D.GameObject
{
    public interface IControllable
    {
        /// <summary>
        /// A map of actions and inputs to use for this object
        /// </summary>
        InputMap InputMap { get; }

        /// <summary>
        /// Handles input with the provided keybinds
        /// </summary>
        /// <param name="availableKeybinds">Only available keybinds to use for processing input</param>
        /// <param name="gameTime">The GameTime to use for this object</param>
        void HandleInput(List<Keybind> availableKeybinds, GameTime gameTime);
    }
}
