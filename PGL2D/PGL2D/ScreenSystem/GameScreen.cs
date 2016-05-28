using System.Linq;
using PGL2D.Game;
using PGL2D.GameObject;

namespace PGL2D.ScreenSystem
{
    public abstract class GameScreen : BaseScreen
    {
        protected GameScreen(BaseGame baseGame, bool acceptsInput, bool isFrozen = false, bool isHidden = false)
            : base(acceptsInput, isFrozen, isHidden)
        {
            Game = baseGame;
        }

        public BaseGame Game { get; private set; }

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
