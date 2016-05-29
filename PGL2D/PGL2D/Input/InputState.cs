using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PGL2D.GameObject;

namespace PGL2D.Input
{
    public sealed class InputState : IFreezable
    {
        /// <summary>
        /// Creates a new InputState object
        /// </summary>
        public InputState()
        {
            KeyboardState = Keyboard.GetState();
            IsFrozen = false;
        }

        /// <summary>
        /// The current state of the keyboard
        /// </summary>
        public KeyboardState KeyboardState { get; private set; }

        /// <summary>
        /// Determines if new keyboard states will be retrieved
        /// </summary>
        public bool IsFrozen { get; set; }

        /// <summary>
        /// Updates the InputState by getting a new keyboard state (unless frozen)
        /// </summary>
        /// <param name="gameTime">The GameTime to use</param>
        public void Update(GameTime gameTime)
        {
            if (IsFrozen) return;

            KeyboardState = Keyboard.GetState();
        }
    }
}
