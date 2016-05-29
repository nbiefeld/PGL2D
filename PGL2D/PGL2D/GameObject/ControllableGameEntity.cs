using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using PGL2D.Input;

namespace PGL2D.GameObject
{
    public abstract class ControllableGameEntity : MoveableEntity, IControllable
    {
        /// <summary>
        /// A reference to the input system needed to process input
        /// </summary>
        private readonly InputSystem _inputSystem;

        /// <summary>
        /// Creates a new ControllableGameEntity object
        /// </summary>
        /// <param name="inputSystem">The reference to the input system that this object should use to query for input</param>
        /// <param name="color">The color of our object</param>
        /// <param name="position">Where our object lives in X and Y coordinates</param>
        /// <param name="angle">The direction of where our object should move</param>
        /// <param name="speed">The speed of our object during movement</param>
        /// <param name="textureName">The asset name of our texture that will be used during content loading</param>
        protected ControllableGameEntity(InputSystem inputSystem, Color color, Vector2 position, float angle,
            float speed, string textureName)
            : base(color, position, angle, speed, textureName)
        {
            _inputSystem = inputSystem;
            InputMap = new InputMap();
        }

        /// <summary>
        /// A map of actions and inputs for this object
        /// </summary>
        public InputMap InputMap { get; private set; }

        /// <summary>
        /// Updates this entity by using the pre and post pattern
        /// </summary>
        /// <param name="gameTime"></param>
        protected sealed override void UpdateEntity(GameTime gameTime)
        {
            PreMovement(gameTime);

            ProcessInput(gameTime);

            PostMovement(gameTime);

            base.UpdateEntity(gameTime);
        }

        /// <summary>
        /// Processes user input
        /// </summary>
        /// <param name="gameTime">The GameTime to use for this object</param>
        private void ProcessInput(GameTime gameTime)
        {
            InputMap.Update(_inputSystem);

            var inputs =
                InputMap.Keybinds.Where(
                    k => k.Inputs.Count(i => i.Mode != InputMode.Released && i.Mode != InputMode.None) > 0).ToList();

            HandleInput(inputs, gameTime);
        }

        /// <summary>
        /// Abstract method of handling input since each object will handle input differently
        /// </summary>
        /// <param name="availableKeybinds">Only available keybinds to process</param>
        /// <param name="gameTime">The GameTime to use for this object</param>
        public abstract void HandleInput(List<Keybind> availableKeybinds, GameTime gameTime);

        /// <summary>
        /// Code to execute before the object moves from user input
        /// 
        /// Alpha status:  Empty at the moment
        /// </summary>
        /// <param name="gameTime">The GameTime to use for this object</param>
        protected virtual void PreMovement(GameTime gameTime)
        {
            //Base implementation - do nothing
        }

        /// <summary>
        /// Code to execute after the object moves from user input
        /// 
        /// Alpha status:  Empty at the moment
        /// </summary>
        /// <param name="gameTime">The GameTime to use for this object</param>
        protected virtual void PostMovement(GameTime gameTime)
        {
            //Base implementation - do nothing
        }
    }
}
