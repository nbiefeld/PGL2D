using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace PGL2D.Input
{
    public class InputMap
    {
        public InputMap()
        {
            Keybinds = new List<Keybind>();
        }

        public List<Keybind> Keybinds { get; private set; }

        public void Update(InputSystem inputSystem)
        {
            foreach (var keybind in Keybinds)
            {
                keybind.Update(inputSystem);
            }
        }

        public void NewAction(string action, Input input)
        {
            var existingKeybind = Keybinds.FirstOrDefault(k => k.Action.Equals(action));
            if (existingKeybind != null)
            {
                existingKeybind.Add(input);
            }
            else
            {
                Keybinds.Add(new Keybind(action, input));
            }
        }

        public void NewAction(string action, Keys k)
        {
            var input = new Input(k);
            NewAction(action, input);
        }

        public List<Input> GetKeybinds(string actionName)
        {
            var keybind = Keybinds.FirstOrDefault(k => k.Action.Equals(actionName));
            return keybind != null ? keybind.Inputs : new List<Input>();
        }
    }
}
