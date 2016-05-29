using Microsoft.Xna.Framework.Input;

namespace PGL2D.Input
{
    public class InputSystem
    {
        /// <summary>
        /// Creates a new InputSystem
        /// </summary>
        public InputSystem()
        {
            CurrentInputState = new InputState();
            PreviousInputState = new InputState();
        }

        /// <summary>
        /// The current state of the input
        /// </summary>
        public InputState CurrentInputState { get; private set; }

        /// <summary>
        /// The previous state of the input
        /// </summary>
        public InputState PreviousInputState { get; private set; }

        /// <summary>
        /// Updates the input by setting the previous state and refreshes the current state
        /// </summary>
        public void Update()
        {
            CurrentInputState.IsFrozen = true;
            PreviousInputState = CurrentInputState;

            CurrentInputState = new InputState();
        }

        /// <summary>
        /// Gets the current state of the keyboard key
        /// </summary>
        /// <param name="k">The keyboard key to query</param>
        /// <returns>The current mode of the key</returns>
        public InputMode GetKeyState(Keys k)
        {
            var isCurrentlyPressedKey = CurrentInputState.KeyboardState.IsKeyDown(k);
            var isPreviouslyPressedKey = PreviousInputState.KeyboardState.IsKeyDown(k);

            if(isCurrentlyPressedKey && !isPreviouslyPressedKey) return InputMode.New;
            if(isCurrentlyPressedKey) return InputMode.Held;
            if(isPreviouslyPressedKey) return InputMode.Released;
            
            return InputMode.None;          
        }
    }
}
