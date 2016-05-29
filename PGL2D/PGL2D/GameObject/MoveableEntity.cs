using System;
using Microsoft.Xna.Framework;
using PGL2D.Collision;

namespace PGL2D.GameObject
{
    public abstract class MoveableEntity : PhysicalEntity, IMoveable
    {
        /// <summary>
        /// Restricts the object's movement to a rectangle if not null
        /// </summary>
        private Rectangle? _bounds;

        /// <summary>
        /// Creates a new MoveableEntity object
        /// </summary>
        /// <param name="color">The color tint of the object</param>
        /// <param name="position">The physical location of the object in the game world</param>
        /// <param name="angle">The direction of the object during movement</param>
        /// <param name="maxSpeed">The maximum speed the object can travel</param>
        /// <param name="textureName">The asset name of the texture to use during content loading</param>
        /// <param name="rotation">The rotation of the object (defaults to 0)</param>
        /// <param name="scale">The scale of the object (defaults to 1)</param>
        /// <param name="bounds">The rectangle to bind the element (defaults to null)</param>
        protected MoveableEntity(Color color, Vector2 position, float angle, float maxSpeed,
            string textureName, float rotation = 0.0f, float scale = 1.0f, Rectangle? bounds = null)
            : base(color, position, textureName, rotation, scale)
        {
            _bounds = bounds;

            Angle = angle;
            Speed = 0;
            MaxSpeed = maxSpeed;
            Acceleration = Vector2.Zero;

            UpdateVelocity();
            UpdateRectangle();
        }

        /// <summary>
        /// The direction of the object's movement
        /// </summary>
        public float Angle { get; private set; }

        /// <summary>
        /// The speed of the object's movement
        /// </summary>
        public float Speed { get; protected set; }

        /// <summary>
        /// The velocity of the object's movement
        /// </summary>
        public Vector2 Velocity { get; private set; }

        /// <summary>
        /// The acceleration of the object's movement
        /// </summary>
        public Vector2 Acceleration { get; protected set; }

        /// <summary>
        /// The maximum speed this object can travel
        /// </summary>
        public float MaxSpeed { get; }

        /// <summary>
        /// Updates the velocity when the speed or angle changes
        /// </summary>
        private void UpdateVelocity()
        {
            Velocity = new Vector2((float)(Speed * Math.Cos(Angle)), (float)(Speed * Math.Sin(Angle)));
        }

        /// <summary>
        /// Binds the entity to a rectangle where the object cannot escape
        /// </summary>
        /// <param name="bounds">The rectangle to bind the object</param>
        public void BindEntity(Rectangle bounds)
        {
            _bounds = bounds;
        }

        /// <summary>
        /// Removes the movement restrictions
        /// </summary>
        public void UnbindEntity()
        {
            _bounds = null;
        }

        /// <summary>
        /// Initial movement method to be called when the object goes from a stopped state to a moving state
        /// </summary>
        /// <param name="initialSpeed">The initial speed the object should travel</param>
        public void Start(float? initialSpeed = null)
        {
            //Check to see if we have a speed already -- we have already started moving
            if (Speed > 0) return;

            if (initialSpeed != null)
            {
                Speed = Math.Min(initialSpeed.Value, MaxSpeed);
            }
            else if (MaxSpeed > 0) Speed = MaxSpeed;

            UpdateVelocity();
        }

        /// <summary>
        /// The method to use when the object goes from a moving state to a stopped state
        /// </summary>
        public void Stop()
        {
            Speed = 0;
            UpdateVelocity();
        }

        /// <summary>
        /// Used to change where the object is moving
        /// </summary>
        /// <param name="angle">The new direction the object should be moving</param>
        public void ChangeDirection(float angle)
        {
            Angle = angle;

            UpdateVelocity();
        }

        /// <summary>
        /// Increases the speed the object is moving
        /// </summary>
        /// <param name="deltaSpeed">The delta speed to increase the overall speed of the object</param>
        public void SpeedUp(float deltaSpeed)
        {
            if (deltaSpeed <= 0)
            {
                return;
            }

            Speed = Math.Min(Speed + deltaSpeed, MaxSpeed);

            UpdateVelocity();
        }

        /// <summary>
        /// Decreases the speed the object is moving
        /// </summary>
        /// <param name="deltaSpeed">The delta speed to decrease the overall speed of the object</param>
        public void SlowDown(float deltaSpeed)
        {
            if (deltaSpeed <= 0)
            {
                return;
            }

            Speed = Math.Max(0, Speed - deltaSpeed);

            UpdateVelocity();
        }

        /// <summary>
        /// Updates the entity by updating the velocity and position as well as updating the rectangle and restricting the movement if needed
        /// </summary>
        /// <param name="gameTime">The GameTime to be used for updating</param>
        protected override void UpdateEntity(GameTime gameTime)
        {
            Velocity = Velocity + Vector2.Multiply(Acceleration, (float) gameTime.ElapsedGameTime.TotalSeconds);
            Position = Position + Vector2.Multiply(Velocity, (float) gameTime.ElapsedGameTime.TotalSeconds);

            UpdateRectangle();
            CheckBounds();
        }

        /// <summary>
        /// Checks to make sure the object is still within the bounded rectangle
        /// </summary>
        private void CheckBounds()
        {
            if (_bounds == null) return;

            var tempPosition = Position;
            var collisionPoint = RectangleCollisionPoint.None;

            if (Rectangle.Left < _bounds.Value.X)
            {
                tempPosition.X = _bounds.Value.X + Origin.X;

                collisionPoint = RectangleCollisionPoint.Left;
            }

            else if (Rectangle.Right > _bounds.Value.Right)
            {
                tempPosition.X = _bounds.Value.Right - Origin.X;

                collisionPoint = RectangleCollisionPoint.Right;
            }

            if (Rectangle.Top < _bounds.Value.Y)
            {
                tempPosition.Y = _bounds.Value.Y + Origin.Y;

                collisionPoint = collisionPoint == RectangleCollisionPoint.None
                    ? RectangleCollisionPoint.Top
                    : collisionPoint.Combine(RectangleCollisionPoint.Top);
            }
            else if (Rectangle.Bottom > _bounds.Value.Bottom)
            {
                tempPosition.Y = _bounds.Value.Bottom - Origin.Y;

                collisionPoint = collisionPoint == RectangleCollisionPoint.None
                    ? RectangleCollisionPoint.Bottom
                    : collisionPoint.Combine(RectangleCollisionPoint.Bottom);
            }

            var change = tempPosition - Position;
            if (change == Vector2.Zero) return;

            HitBounds(change, collisionPoint.GetNormal(true), collisionPoint);

            Position = tempPosition;
            UpdateRectangle();
        }

        /// <summary>
        /// Method for handling when the object has hit the bounds of the binded rectangle
        /// </summary>
        /// <param name="change">The amount of asjustment needed to bring the object back in the bounds</param>
        /// <param name="normal">The normal vector of the collision with the bounded walls</param>
        /// <param name="collisionPoint">The point where the collision occurred</param>
        public virtual void HitBounds(Vector2 change, Vector2 normal, RectangleCollisionPoint collisionPoint)
        {
            // Base implementation -- DO NOTHING
        }
    }
}
