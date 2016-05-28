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

        public bool AcceptsInput { get; private set; }
        public bool ContentLoaded { get; private set; }
        public bool ContentUnloaded { get; private set; }
        public InputMap InputMap { get; private set; }
        protected List<GameEntity> Entities { get; private set; }
        protected List<TextEntity> TextEntities { get; private set; }

        public void LoadContent()
        {
            LoadEntityContent();

            DoneLoadingContent();
        }

        protected abstract void LoadEntityContent();

        protected virtual void DoneLoadingContent()
        {
            ContentLoaded = true;
        }

        public override void Terminate()
        {
            UnloadContent();

            RequestDeletion = true;

            base.Terminate();
        }

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

        protected sealed override void UpdateEntity(GameTime gameTime)
        {
            Entities.RemoveAll(e => e.RequestDeletion);
            TextEntities.RemoveAll(te => te.RequestDeletion);

            var updateableEntities = Entities.Where(e => !e.IsFrozen).ToList();
            var updateableTextEntities = TextEntities.Where(te => !te.IsFrozen).ToList();

            UpdateScreen(gameTime, updateableEntities, updateableTextEntities);
        }

        protected abstract void UpdateScreen(GameTime gameTime, List<GameEntity> updateableEntities,
            List<TextEntity> updateableTextEntities);

        protected override void DrawEntity(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var drawableEntities = Entities.Where(e => !e.IsHidden).ToList();
            var drawableTextEntities = TextEntities.Where(te => !te.IsHidden).ToList();

            DrawScreen(gameTime, spriteBatch, drawableEntities, drawableTextEntities);
        }

        protected abstract void DrawScreen(GameTime gameTime, SpriteBatch spriteBatch, List<GameEntity> drawableEntities,
            List<TextEntity> drawableTextEntities);

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
