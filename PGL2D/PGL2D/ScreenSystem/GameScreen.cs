using System.Linq;
using PGL2D.Game;
using PGL2D.GameObject;

namespace PGL2D.ScreenSystem
{
    public abstract class GameScreen : BaseScreen
    {
        /// <summary>
        /// Creates a new GameScreen
        /// </summary>
        /// <param name="baseGame">The reference to the game</param>
        /// <param name="acceptsInput">Determines if this screen accepts input</param>
        /// <param name="isFrozen">Determines if this screen is initially frozen</param>
        /// <param name="isHidden">Determines if this screen is initially hidden</param>
        protected GameScreen(BaseGame baseGame, bool acceptsInput, bool isFrozen = false, bool isHidden = false)
            : base(acceptsInput, isFrozen, isHidden)
        {
            Game = baseGame;
        }

        /// <summary>
        /// The reference to the game
        /// </summary>
        public BaseGame Game { get; private set; }

        /// <summary>
        /// Loads entity content from the list of entity objects
        /// </summary>
        protected override void LoadEntityContent()
        {
            foreach (var physicalEntity in Entities.OfType<PhysicalEntity>())
            {
                physicalEntity.LoadContent(Game.Content);
            }

            foreach (var textEntity in TextEntities)
            {
                textEntity.LoadContent(Game.Content);
            }
        }
    }
}
