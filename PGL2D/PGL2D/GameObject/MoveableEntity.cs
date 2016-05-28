using System;
using Microsoft.Xna.Framework;
using PGL2D.Collision;

namespace PGL2D.GameObject
{
    public abstract class MoveableEntity : PhysicalEntity, IMoveable
    {
        private Rectangle? _bounds;

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

        public float Angle { get; private set; }
        public float Speed { get; protected set; }
        public Vector2 Velocity { get; private set; }
        public Vector2 Acceleration { get; protected set; }
        public float MaxSpeed { get; }

        private void UpdateVelocity()
        {
            Velocity = new Vector2((float)(Speed * Math.Cos(Angle)), (float)(Speed * Math.Sin(Angle)));
        }

        public void BindEntity(Rectangle bounds)
        {
            _bounds = bounds;
        }

        public void UnbindEntity()
        {
            _bounds = null;
        }

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

        public void Stop()
        {
            Speed = 0;
            UpdateVelocity();
        }

        public void ChangeDirection(float angle)
        {
            Angle = angle;

            UpdateVelocity();
        }

        public void SpeedUp(float deltaSpeed)
        {
            if (deltaSpeed <= 0)
            {
                return;
            }

            Speed = Math.Min(Speed + deltaSpeed, MaxSpeed);

            UpdateVelocity();
        }

        public void SlowDown(float deltaSpeed)
        {
            if (deltaSpeed <= 0)
            {
                return;
            }

            Speed = Math.Max(0, Speed - deltaSpeed);

            UpdateVelocity();
        }

        protected override void UpdateEntity(GameTime gameTime)
        {
            Velocity = Velocity + Vector2.Multiply(Acceleration, (float) gameTime.ElapsedGameTime.TotalSeconds);
            Position = Position + Vector2.Multiply(Velocity, (float) gameTime.ElapsedGameTime.TotalSeconds);

            UpdateRectangle();
            CheckBounds();
        }

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

        public virtual void HitBounds(Vector2 change, Vector2 normal, RectangleCollisionPoint collisionPoint)
        {
            // Base implementation -- DO NOTHING
        }
    }
}
