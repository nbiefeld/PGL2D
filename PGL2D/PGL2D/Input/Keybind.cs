using System.Collections.Generic;
using System.Linq;

namespace PGL2D.Input
{
    public class Keybind
    {
        public Keybind(string action, Input initialInput = null)
        {
            Action = action;
            Inputs = new List<Input>();

            if (initialInput != null)
            {
                Inputs.Add(initialInput);
            }
        }

        public string Action { get; private set; }
        public List<Input> Inputs { get; private set; }

        public void Update(InputSystem inputSystem)
        {
            foreach (var input in Inputs)
            {
                input.Update(inputSystem);
            }
        }

        public bool CheckMode(InputMode inputMode)
        {
            return Inputs.Any(i => i.Mode == inputMode);
        }

        public void Add(Input input)
        {
            Inputs.Add(input);
        }
    }
}
