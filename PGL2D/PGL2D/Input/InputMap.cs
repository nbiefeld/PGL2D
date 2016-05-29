using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace PGL2D.Input
{
    public class InputMap
    {
        /// <summary>
        /// Creates a new InputMap object
        /// </summary>
        public InputMap()
        {
            Keybinds = new List<Keybind>();
        }

        /// <summary>
        /// A list of keybinds that belong to this inputmap object
        /// </summary>
        public List<Keybind> Keybinds { get; private set; }

        /// <summary>
        /// Updates all keybinds in this object
        /// </summary>
        /// <param name="inputSystem">The InputSystem to use for updating keybinds</param>
        public void Update(InputSystem inputSystem)
        {
            foreach (var keybind in Keybinds)
            {
                keybind.Update(inputSystem);
            }
        }

        /// <summary>
        /// Inserts a new action, or updates an existing action if it exists
        /// </summary>
        /// <param name="action">The name of the action</param>
        /// <param name="input">The input to use or append for this action</param>
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

        /// <summary>
        /// Inserts a new action based off the keyboard key
        /// </summary>
        /// <param name="action">The name of the action</param>
        /// <param name="k">The keyboard key</param>
        public void NewAction(string action, Keys k)
        {
            var input = new Input(k);
            NewAction(action, input);
        }

        /// <summary>
        /// Gets a list of inputs from a given action name
        /// </summary>
        /// <param name="actionName">The action name to use as the query</param>
        /// <returns></returns>
        public List<Input> GetKeybinds(string actionName)
        {
            var keybind = Keybinds.FirstOrDefault(k => k.Action.Equals(actionName));
            return keybind != null ? keybind.Inputs : new List<Input>();
        }
    }
}
