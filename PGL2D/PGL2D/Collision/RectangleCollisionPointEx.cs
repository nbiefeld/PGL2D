using Microsoft.Xna.Framework;

namespace PGL2D.Collision
{
    public static class RectangleCollisionPointEx
    {
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

        public static Vector2 GetCornerNormal(Vector2 delta)
        {
            var n = delta;
            n.Normalize();

            return n;
        }

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

        public static RectangleCollisionSide GetCollisionSide(this RectangleCollisionPoint source)
        {
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
