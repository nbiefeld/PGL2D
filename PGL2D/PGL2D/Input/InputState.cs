using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PGL2D.GameObject;

namespace PGL2D.Input
{
    public sealed class InputState : IFreezable
    {
        public InputState()
        {
            KeyboardState = Keyboard.GetState();
            IsFrozen = false;
        }

        public KeyboardState KeyboardState { get; private set; }
        public bool IsFrozen { get; set; }

        public void Update(GameTime gameTime)
        {
            if (IsFrozen) return;

            KeyboardState = Keyboard.GetState();
        }
    }
}
