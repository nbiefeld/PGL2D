using Microsoft.Xna.Framework.Input;

namespace PGL2D.Input
{
    public class Input
    {
        /// <summary>
        /// Creates a new Input object
        /// </summary>
        /// <param name="key">The Keyboard key to set as the input</param>
        public Input(Keys key)
        {
            Key = key;
        }

        /// <summary>
        /// The Keyboard key that belongs to this input
        /// </summary>
        public Keys Key { get; private set; }

        /// <summary>
        /// The current mode of our input
        /// </summary>
        public InputMode Mode { get; private set; }
        
        /// <summary>
        /// Updates the input by refreshing the mode
        /// </summary>
        /// <param name="inputSystem"></param>
        public void Update(InputSystem inputSystem)
        {
            Mode = inputSystem.GetKeyState(Key);
        }
    }
}
