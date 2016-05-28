using Microsoft.Xna.Framework.Input;

namespace PGL2D.Input
{
    public class Input
    {
        public Input(Keys key)
        {
            Key = key;
        }

        public Keys Key { get; private set; }
        public InputMode Mode { get; private set; }

        public void Update(InputSystem inputSystem)
        {
            Mode = inputSystem.GetKeyState(Key);
        }
    }
}
