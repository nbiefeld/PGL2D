using Microsoft.Xna.Framework;

namespace PGL2D.Collision
{
    public static class RectangleCollisionPointEx
    {
        /// <summary>
        /// Used to combine two rectangle collision points together.
        /// 
        /// Example: Top and Right would be combined to TopRight
        /// </summary>
        /// <param name="source">The original rectangle collision point</param>
        /// <param name="target">The secondary rectangle collision point to combine with the original</param>
        /// <returns>The combined rectangle collision point or none if they cannot be combined</returns>
        public static RectangleCollisionPoint Combine(this RectangleCollisionPoint source,
            RectangleCollisionPoint target)
        {
            if ((source == RectangleCollisionPoint.Left && target == RectangleCollisionPoint.Top) ||
                (source == RectangleCollisionPoint.Top && target == RectangleCollisionPoint.Left))
            {
                return RectangleCollisionPoint.TopLeftCorner;
            }
            if ((source == RectangleCollisionPoint.Right && target == RectangleCollisionPoint.Top) ||
                (source == RectangleCollisionPoint.Top && target == RectangleCollisionPoint.Right))
            {
                return RectangleCollisionPoint.TopRightCorner;
            }

            if ((source == RectangleCollisionPoint.Left && target == RectangleCollisionPoint.Bottom) ||
                (source == RectangleCollisionPoint.Bottom && target == RectangleCollisionPoint.Left))
            {
                return RectangleCollisionPoint.BottomLeftCorner;
            }
            if ((source == RectangleCollisionPoint.Right && target == RectangleCollisionPoint.Bottom) ||
                (source == RectangleCollisionPoint.Bottom && target == RectangleCollisionPoint.Right))
            {
                return RectangleCollisionPoint.BottomRightCorner;
            }

            return RectangleCollisionPoint.None;
        }

        /// <summary>
        /// Used to get the normal from a vector
        /// </summary>
        /// <param name="delta">The rectangle to use to calculate the normal</param>
        /// <returns>The normal of the provided vector</returns>
        public static Vector2 GetCornerNormal(Vector2 delta)
        {
            var n = delta;
            n.Normalize();

            return n;
        }

        /// <summary>
        /// Gets a normal vector based off of a rectangle collision point
        /// </summary>
        /// <param name="point">The rectangle collision point of where this collision occurred</param>
        /// <param name="inside">Value indicating if the collision occured while inside the rectangle (defaulted to false)</param>
        /// <returns>The normal vector of the collision</returns>
        public static Vector2 GetNormal(this RectangleCollisionPoint point, bool inside = false)
        {
            switch (point)
            {
                case RectangleCollisionPoint.Top:
                    return new Vector2(0, inside ? 1 : -1);
                case RectangleCollisionPoint.Left:
                    return new Vector2(inside ? 1 : -1, 0);
                case RectangleCollisionPoint.Bottom:
                    return new Vector2(0, inside ? -1 : 1);
                case RectangleCollisionPoint.Right:
                    return new Vector2(inside ? -1 : 1, 0);
                case RectangleCollisionPoint.TopLeftCorner:
                    return GetCornerNormal(new Vector2(inside ? -1 : 1, inside ? -1 : 1));
                case RectangleCollisionPoint.TopRightCorner:
                    return GetCornerNormal(new Vector2(inside ? 1 : -1, inside ? -1 : 1));
                case RectangleCollisionPoint.BottomLeftCorner:
                    return GetCornerNormal(new Vector2(inside ? -1 : 1, inside ? 1 : -1));
                case RectangleCollisionPoint.BottomRightCorner:
                    return GetCornerNormal(new Vector2(inside ? 1 : -1, inside ? 1 : -1));
                default:
                    return Vector2.Zero;
            }
        }

        /// <summary>
        /// Determines the rectangle collision side from the collision point
        /// </summary>
        /// <param name="source">The rectangle collision point</param>
        /// <returns>The side of the rectangle collision</returns>
        public static RectangleCollisionSide GetCollisionSide(this RectangleCollisionPoint source)
        {
            //TODO:  Change to return an array for beta release

            switch (source)
            {
                case RectangleCollisionPoint.BottomLeftCorner:
                case RectangleCollisionPoint.Left:
                case RectangleCollisionPoint.TopLeftCorner:
                    return RectangleCollisionSide.Left;
                case RectangleCollisionPoint.BottomRightCorner:
                case RectangleCollisionPoint.Right:
                case RectangleCollisionPoint.TopRightCorner:
                    return RectangleCollisionSide.Right;
                case RectangleCollisionPoint.Top:
                    return RectangleCollisionSide.Top;
                case RectangleCollisionPoint.Bottom:
                    return RectangleCollisionSide.Bottom;
                default:
                    return RectangleCollisionSide.None;
            }
        }
    }
}
