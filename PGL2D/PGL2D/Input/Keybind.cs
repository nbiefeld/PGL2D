using System.Collections.Generic;
using System.Linq;

namespace PGL2D.Input
{
    public class Keybind
    {
        /// <summary>
        /// Creates a new Keybind (maps inputs with actions)
        /// </summary>
        /// <param name="action">The name of the action</param>
        /// <param name="initialInput">The initial input to add to this action</param>
        public Keybind(string action, Input initialInput = null)
        {
            Action = action;
            Inputs = new List<Input>();

            if (initialInput != null)
            {
                Inputs.Add(initialInput);
            }
        }

        /// <summary>
        /// The name of the action
        /// </summary>
        public string Action { get; private set; }

        /// <summary>
        /// The list of inputs available for this action
        /// </summary>
        public List<Input> Inputs { get; private set; }

        /// <summary>
        /// Updates all inputs for this action
        /// </summary>
        /// <param name="inputSystem">The InputSystem to use for updating</param>
        public void Update(InputSystem inputSystem)
        {
            foreach (var input in Inputs)
            {
                input.Update(inputSystem);
            }
        }

        /// <summary>
        /// Checks to see if any inputs satisfy the requested input mode
        /// </summary>
        /// <param name="inputMode">The InputMode to test</param>
        /// <returns>True if any of the inputs of this action matches the InputMode</returns>
        public bool CheckMode(InputMode inputMode)
        {
            return Inputs.Any(i => i.Mode == inputMode);
        }

        /// <summary>
        /// Adds a new input to this action
        /// </summary>
        /// <param name="input">The new input to add</param>
        public void Add(Input input)
        {
            Inputs.Add(input);
        }
    }
}
