using System;
using Microsoft.Xna.Framework;
using PGL2D.GameObject;

namespace PGL2D.Collision
{
    public static class Collision
    {
        /// <summary>
        /// Checks to see if two entities have collided based on their rectangles
        /// </summary>
        /// <param name="a">The first entity to check</param>
        /// <param name="b">The second entity to check</param>
        /// <returns>True of two objects are collided based on their rectangles</returns>
        public static bool RectangleCollision(MoveableEntity a, MoveableEntity b)
        {
            return RectangularCollision(a.Rectangle, b.Rectangle);
        }

        /// <summary>
        /// Checks to see if one rectangle intersects another rectangle
        /// </summary>
        /// <param name="a">The first rectangle to check</param>
        /// <param name="b">The second rectangle to check</param>
        /// <returns>True if both rectangles intersect</returns>
        public static bool RectangularCollision(Rectangle a, Rectangle b)
        {
            return a.Intersects(b);
        }

        /// <summary>
        /// Determines if a rectangle collision has occurred between two entities and provides a normal
        /// based off where the collision occurred
        /// </summary>
        /// <param name="a">The first entity to check</param>
        /// <param name="b">The second entity to check</param>
        /// <param name="normal">The collision normal based off the collision point, or Vector2.Zero if no collision</param>
        /// <returns>True if there is a rectangle collision between two entities</returns>
        public static bool RectangularCollision(MoveableEntity a, MoveableEntity b, out Vector2 normal)
        {
            normal = Vector2.Zero;
            if (!RectangularCollision(a.Rectangle, b.Rectangle)) return false;

            var delta = a.Position - b.Position;
            var absDeltax = Math.Abs(delta.X);
            var absDeltay = Math.Abs(delta.Y);

            //Right side
            if(delta.X > 0 && absDeltay < b.Rectangle.Height / 2.0 && a.Velocity.X < 0) normal = new Vector2(1, 0);

            //Left side
            if(delta.X < 0 && absDeltay < b.Rectangle.Height / 2.0 && a.Velocity.X > 0) normal = new Vector2(-1, 0);
            
            //Top side
            if (delta.Y < 0 && absDeltax < b.Rectangle.Width / 2.0 && a.Velocity.Y > 0) normal = new Vector2(0, -1);

            //Bottom side
            if(delta.Y > 0 && absDeltax < b.Rectangle.Width / 2.0 && a.Velocity.Y < 0) normal = new Vector2(0, 1);

            //All corners
            var epsilon = 0.1;
            if ((Math.Abs(delta.X - -b.Rectangle.Width / 2.0) < epsilon &&
                 Math.Abs(delta.Y - b.Rectangle.Height / 2.0) < epsilon) ||
                (Math.Abs(delta.X - b.Rectangle.Width / 2.0) < epsilon &&
                 Math.Abs(delta.Y - b.Rectangle.Height / 2.0) < epsilon) ||
                (Math.Abs(delta.X - -b.Rectangle.Width / 2.0) < epsilon &&
                 Math.Abs(delta.Y - -b.Rectangle.Height / 2.0) < epsilon) ||
                (Math.Abs(delta.X - b.Rectangle.Width / 2.0) < epsilon &&
                 Math.Abs(delta.Y - -b.Rectangle.Height / 2.0) < epsilon))
            {
                normal = new Vector2(delta.X, delta.Y);
                normal.Normalize();
            }

            return true;
        }
    }
}
