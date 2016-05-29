using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PGL2D.GameObject;
using PGL2D.Input;

namespace PGL2D.ScreenSystem
{
    public abstract class BaseScreen : GameEntity
    {
        /// <summary>
        /// Creates a new BaseScreen
        /// </summary>
        /// <param name="acceptsInput">Determines if this screen should accept user input</param>
        /// <param name="isFrozen">Determines if this screen is initially frozen</param>
        /// <param name="isHidden">Determines if this screen is initially hidden</param>
        protected BaseScreen(bool acceptsInput, bool isFrozen = false, bool isHidden = false)
        {
            AcceptsInput = acceptsInput;
            IsFrozen = isFrozen;
            IsHidden = isHidden;
            RequestDeletion = false;

            InputMap = new InputMap();
            Entities = new List<GameEntity>();
            TextEntities = new List<TextEntity>();
        }

        /// <summary>
        /// Determines if this screen allows user input
        /// </summary>
        public bool AcceptsInput { get; private set; }

        /// <summary>
        /// Determines if this screen has loaded all content
        /// </summary>
        public bool ContentLoaded { get; private set; }

        /// <summary>
        /// Determines if this screen has unloaded all content
        /// </summary>
        public bool ContentUnloaded { get; private set; }

        /// <summary>
        /// The InputMap that belongs to this screen
        /// </summary>
        public InputMap InputMap { get; private set; }

        /// <summary>
        /// The list of sprites, platforms, powerups and more game elements
        /// </summary>
        protected List<GameEntity> Entities { get; private set; }

        /// <summary>
        /// The list of text entities
        /// </summary>
        protected List<TextEntity> TextEntities { get; private set; }

        /// <summary>
        /// Loads the content for the screen
        /// </summary>
        public void LoadContent()
        {
            LoadEntityContent();

            DoneLoadingContent();
        }

        /// <summary>
        /// Loads the entity content which must be implemented in derived classes
        /// </summary>
        protected abstract void LoadEntityContent();

        /// <summary>
        /// Method to execute afer content is done loading
        /// </summary>
        protected virtual void DoneLoadingContent()
        {
            ContentLoaded = true;
        }

        /// <summary>
        /// Terminates this screen by unloading content and being requested to be deleted
        /// </summary>
        public override void Terminate()
        {
            UnloadContent();

            RequestDeletion = true;

            base.Terminate();
        }

        /// <summary>
        /// Unloads the entity content
        /// </summary>
        protected virtual void UnloadContent()
        {
            foreach (var physicalEntity in Entities.OfType<PhysicalEntity>())
            {
                physicalEntity.UnloadContent();
            }

            foreach (var textEntity in TextEntities)
            {
                textEntity.UnloadContent();
            }

            ContentUnloaded = true;
        }

        /// <summary>
        /// Removes entities requested to be deleted and gets a list of only updateable entities and calls UpdateScreen
        /// </summary>
        /// <param name="gameTime">The GameTime to use</param>
        protected sealed override void UpdateEntity(GameTime gameTime)
        {
            Entities.RemoveAll(e => e.RequestDeletion);
            TextEntities.RemoveAll(te => te.RequestDeletion);

            var updateableEntities = Entities.Where(e => !e.IsFrozen).ToList();
            var updateableTextEntities = TextEntities.Where(te => !te.IsFrozen).ToList();

            UpdateScreen(gameTime, updateableEntities, updateableTextEntities);
        }

        /// <summary>
        /// Updates the screen
        /// </summary>
        /// <param name="gameTime">The GameTime to use</param>
        /// <param name="updateableEntities">A list of entities that should be updated</param>
        /// <param name="updateableTextEntities">A list of text entities that should be updated</param>
        protected abstract void UpdateScreen(GameTime gameTime, List<GameEntity> updateableEntities,
            List<TextEntity> updateableTextEntities);

        /// <summary>
        /// Gets a list of only drawable entities and calls DrawScreen
        /// </summary>
        /// <param name="gameTime">The GameTime to use</param>
        /// <param name="spriteBatch">The SpriteBatch to use for drawing</param>
        protected override void DrawEntity(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var drawableEntities = Entities.Where(e => !e.IsHidden).ToList();
            var drawableTextEntities = TextEntities.Where(te => !te.IsHidden).ToList();

            DrawScreen(gameTime, spriteBatch, drawableEntities, drawableTextEntities);
        }

        /// <summary>
        /// Draws the screen
        /// </summary>
        /// <param name="gameTime">The GameTime to use</param>
        /// <param name="spriteBatch">The SpriteBatch to use for drawing</param>
        /// <param name="drawableEntities">A list of entities that should be drawn</param>
        /// <param name="drawableTextEntities">A list of text entities that should be drawn</param>
        protected abstract void DrawScreen(GameTime gameTime, SpriteBatch spriteBatch, List<GameEntity> drawableEntities,
            List<TextEntity> drawableTextEntities);

        /// <summary>
        /// Adds a new entity to the list
        /// </summary>
        /// <param name="gameEntity">The entity to add to the screen</param>
        public void AddEntity(GameEntity gameEntity)
        {
            var textEntity = gameEntity as TextEntity;
            if (textEntity != null)
            {
                TextEntities.Add(textEntity);
            }
            else
            {
                Entities.Add(gameEntity);
            }
        }
    }
}
