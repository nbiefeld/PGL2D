using Microsoft.Xna.Framework.Input;

namespace PGL2D.Input
{
    public class InputSystem
    {
        public InputSystem()
        {
            CurrentInputState = new InputState();
            PreviousInputState = new InputState();
        }

        public InputState CurrentInputState { get; private set; }
        public InputState PreviousInputState { get; private set; }

        public void Update()
        {
            CurrentInputState.IsFrozen = true;
            PreviousInputState = CurrentInputState;

            CurrentInputState = new InputState();
        }

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
