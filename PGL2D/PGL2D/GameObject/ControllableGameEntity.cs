using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using PGL2D.Input;

namespace PGL2D.GameObject
{
    public abstract class ControllableGameEntity : MoveableEntity, IControllable
    {
        private readonly InputSystem _inputSystem;

        protected ControllableGameEntity(InputSystem inputSystem, Color color, Vector2 position, float angle,
            float speed, string textureName)
            : base(color, position, angle, speed, textureName)
        {
            _inputSystem = inputSystem;
            InputMap = new InputMap();
        }

        public InputMap InputMap { get; private set; }

        protected sealed override void UpdateEntity(GameTime gameTime)
        {
            PreMovement(gameTime);

            ProcessInput(gameTime);

            PostMovement(gameTime);

            base.UpdateEntity(gameTime);
        }

        private void ProcessInput(GameTime gameTime)
        {
            InputMap.Update(_inputSystem);

            var inputs =
                InputMap.Keybinds.Where(
                    k => k.Inputs.Count(i => i.Mode != InputMode.Released && i.Mode != InputMode.None) > 0).ToList();

            HandleInput(inputs, gameTime);
        }

        public abstract void HandleInput(List<Keybind> availableKeybinds, GameTime gameTime);

        protected virtual void PreMovement(GameTime gameTime)
        {
            //Base implementation - do nothing
        }

        protected virtual void PostMovement(GameTime gameTime)
        {
            //Base implementation - do nothing
        }
    }
}
